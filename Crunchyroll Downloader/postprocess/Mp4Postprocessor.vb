﻿Imports System.IO
Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.settings
Imports Crunchyroll_Downloader.settings.ffmpeg
Imports Crunchyroll_Downloader.settings.ffmpeg.encoding
Imports Crunchyroll_Downloader.utilities
Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports Crunchyroll_Downloader.utilities.ffmpeg.codec
Imports Crunchyroll_Downloader.utilities.ffmpeg.preset
Imports SiteAPI.api.common

Namespace postprocess

    ''' <summary>
    ''' Class that remuxes or reencodes any video / audio files if desired.
    ''' </summary>
    Public Class Mp4Postprocessor

        Private ReadOnly Preferences As ReencodePreferences
        Private ReadOnly FfmpegRunner As IFfmpegAdapter
        Private ReadOnly FilesystemApi As IFilesystem

        Public Sub New(prefs As ReencodePreferences, ffmpegRunner As IFfmpegAdapter, fileSystemApi As IFilesystem)
            Preferences = prefs
            Me.FfmpegRunner = ffmpegRunner
            Me.FilesystemApi = fileSystemApi
        End Sub

        Public Async Function ProcessInputs(files As List(Of MediaFileEntry)) As Task(Of List(Of MediaFileEntry))
            ' TODO: Do nothing if the file is currently mp4 and the subtitles shouldn't be merged.

            Dim outputFiles As New List(Of MediaFileEntry)

            Dim videoFile = GetVideoOrAudioInput(files)
            If videoFile IsNot Nothing Then
                Dim combinedFileName = $"{Path.GetFileNameWithoutExtension(videoFile.Location)}-reencode"

                ' Ensure that the output file won't exist.
                Dim temporaryOutput = GetUniqueFilename(FilesystemApi, Preferences.TemporaryOutputPath, combinedFileName, ".mp4")

                Dim args As New FfmpegArguments(temporaryOutput)

                Dim combinedMediaTypes As MediaType

                Dim willCreateOutput = False
                Dim inputFileIndex = 0
                For fileNumber = 0 To files.Count - 1
                    Dim file As MediaFileEntry = files(fileNumber)

                    If ProcessFile(file, args, inputFileIndex) Then
                        willCreateOutput = True
                        inputFileIndex += 1
                        combinedMediaTypes = combinedMediaTypes Or file.ContainedMedia
                    Else
                        outputFiles.Add(file)
                    End If
                Next

                ApplyPreset(args)

                If willCreateOutput Then
                    outputFiles.Add(New MediaFileEntry(temporaryOutput, combinedMediaTypes))
                End If

                AddHandler FfmpegRunner.ReportProgress, AddressOf HandleFfmpegProgress
                Dim statusCode As Integer = Await FfmpegRunner.Run(args)

                RemoveHandler FfmpegRunner.ReportProgress, AddressOf HandleFfmpegProgress
            Else
                ' If there was no video or audio file found, either there are no files or there is only a subtitle file.
                ' In either case, we can just return what we were given because this was a no-op.
                outputFiles = files
            End If

            ' TODO: Delete input files that have been reencoded.

            Return outputFiles
        End Function

        Private Sub HandleFfmpegProgress(amount As Integer)
            Debug.WriteLine($"Mp4 postprocessor ffmpeg progress reported: {amount}")
        End Sub

        Private Sub ApplyPreset(args As FfmpegArguments)
            If Preferences.PostprocessSettings Is Nothing Then
                Return
            End If

            Dim activeEncoder = Preferences.PostprocessSettings.GetActiveEncoder()

            If activeEncoder IsNot Nothing And Not Preferences.PostprocessSettings.VideoCopy Then
                ' TODO: Use quality preset for av1.
                Dim c As Codec = activeEncoder.VideoCodec
                If activeEncoder.Preset <> SpeedSetting.NO_PRESET And (c = Codec.H_264 Or c = Codec.H_265) Then
                    args.Preset = SpeedPresetArgument.FromSetting(activeEncoder.Preset)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Gets the video or audio file from the MediaFileEntry list, preferring the video file.
        ''' </summary>
        ''' <param name="files"></param>
        ''' <returns></returns>
        Private Function GetVideoOrAudioInput(files As List(Of MediaFileEntry)) As MediaFileEntry
            Dim preferredFile As MediaFileEntry = Nothing
            For Each file In files
                If (preferredFile Is Nothing AndAlso file.ContainedMedia.HasFlag(MediaType.Audio)) OrElse
                        file.ContainedMedia.HasFlag(MediaType.Video) Then
                    preferredFile = file
                End If
            Next
            Return preferredFile
        End Function

        Private Function ProcessFile(entry As MediaFileEntry, args As FfmpegArguments, inputNumber As Integer) As Boolean
            Dim useFile As Boolean = False

            ' TODO: Maybe only select individual tracks if there is a need? Seems very verbose.
            ' TODO: Allow setting a global copy flag, and allow mp4 to use subtitle copy codec.

            If entry.ContainedMedia.HasFlag(MediaType.Audio) Then
                useFile = True
                args.SelectedStreams.Add(New MapArgument() With {
                    .InputFileNumber = inputNumber,
                    .Selector = New StreamSpecifier() With {
                        .Type = StreamType.AUDIO
                    }
                })
                args.Codecs.Add(GetAudioCodec(inputNumber))
            End If
            If entry.ContainedMedia.HasFlag(MediaType.Video) Then
                useFile = True
                args.SelectedStreams.Add(New MapArgument() With {
                    .InputFileNumber = inputNumber,
                    .Selector = New StreamSpecifier() With {
                            .Type = StreamType.VIDEO_AND_ATTACHMENTS
                    }
                })
                args.Codecs.Add(GetVideoCodec(Preferences.PostprocessSettings))
            End If

            If Preferences.SubtitleBehavior <> SubtitleMerge.DISABLED Then
                If entry.ContainedMedia.HasFlag(MediaType.Subtitles) Then
                    useFile = True
                    args.SelectedStreams.Add(New MapArgument() With {
                        .InputFileNumber = inputNumber,
                        .Selector = New StreamSpecifier() With {
                                .Type = StreamType.SUBTITLE
                        }
                    })
                    args.Codecs.Add(New SubtitleCodecArgument(New StreamSpecifier() With {
                            .Type = StreamType.SUBTITLE
                        },
                        GetCodecName(Preferences.SubtitleBehavior)))
                End If
            End If

            If useFile Then
                args.InputFiles.Add(entry.Location)
            End If

            Return useFile
        End Function

        Private Function GetVideoCodec(options As FfmpegOptions) As ICodecArgument
            Dim vCodec As VideoCodec = VideoCodec.COPY

            If options IsNot Nothing AndAlso Not options.VideoCopy Then
                Dim encoder = options.GetActiveEncoder()
                vCodec = VideoCodecArgument.CodecFromEncoderSettings(encoder)
            End If

            Return New VideoCodecArgument(New StreamSpecifier() With {
                                            .Type = StreamType.VIDEO_ONLY
                                          }, vCodec)
        End Function

        Private Function GetAudioCodec(streamNumber As Integer) As ICodecArgument
            Dim aCodec As AudioCodec = AudioCodec.COPY
            Return New AudioCodecArgument(New StreamSpecifier() With {
                                              .Type = StreamType.AUDIO,
                                              .StreamIndex = streamNumber
                                          }, aCodec)
        End Function

        Private Function GetCodecName(mergeBehavior As SubtitleMerge) As SubtitleCodec
            Select Case mergeBehavior
                Case SubtitleMerge.COPY
                    Return SubtitleCodec.COPY
                Case SubtitleMerge.MOV_TEXT
                    Return SubtitleCodec.MOV_TEXT
                Case SubtitleMerge.SRT
                    Return SubtitleCodec.SRT
                Case Else
                    ' There is no format for DISABLED
                    Return SubtitleCodec.COPY
            End Select
        End Function
    End Class
End Namespace