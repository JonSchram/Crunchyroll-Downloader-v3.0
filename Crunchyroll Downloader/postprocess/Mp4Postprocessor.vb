Imports System.IO
Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.settings
Imports Crunchyroll_Downloader.utilities
Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports SiteAPI.api.common
Imports SiteAPI.api.metadata

Namespace postprocess

    ''' <summary>
    ''' Class that remuxes or reencodes any video / audio files if desired.
    ''' </summary>
    Public Class Mp4Postprocessor

        Private ReadOnly Preferences As ReencodePreferences
        Private ReadOnly FfmpegRunner As IFfmpegAdapter

        Public Sub New(prefs As ReencodePreferences, ffmpegRunner As IFfmpegAdapter)
            Preferences = prefs
            Me.FfmpegRunner = ffmpegRunner
        End Sub

        Public Async Function ProcessInputs(files As List(Of MediaFileEntry)) As Task(Of List(Of MediaFileEntry))
            ' TODO: Do nothing if the file is currently mp4 and the subtitles shouldn't be merged.

            Dim outputFiles As New List(Of MediaFileEntry)

            ' TODO: Either use input file name to generate the output name, or generate randomly.
            Dim combinedFileName = "mp4reencode.mp4"
            Dim temporaryOutput = Path.Combine(Preferences.TemporaryOutputPath, combinedFileName)
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

            If willCreateOutput Then
                outputFiles.Add(New MediaFileEntry(temporaryOutput, combinedMediaTypes))
            End If

            Await FfmpegRunner.Run(args)

            ' TODO: Copy subtitle if not merged.
            ' TODO: Delete input files that have been reencoded.

            Return outputFiles
        End Function

        Private Function ProcessFile(entry As MediaFileEntry, args As FfmpegArguments, inputNumber As Integer) As Boolean
            Dim useFile As Boolean = False

            ' TODO: Maybe only select individual tracks if there is a need? Seems very verbose.
            ' Also have to consider what happens if the output file exists already.

            If entry.ContainedMedia.HasFlag(MediaType.Audio) Then
                useFile = True
                args.SelectedStreams.Add(New FfmpegArguments.MapArgument() With {
                    .InputFileNumber = inputNumber,
                    .Selector = New FfmpegArguments.StreamSpecifier() With {
                        .Type = FfmpegArguments.StreamType.AUDIO
                    }
                })
            End If
            If entry.ContainedMedia.HasFlag(MediaType.Video) Then
                useFile = True
                args.SelectedStreams.Add(New FfmpegArguments.MapArgument() With {
                    .InputFileNumber = inputNumber,
                    .Selector = New FfmpegArguments.StreamSpecifier() With {
                            .Type = FfmpegArguments.StreamType.VIDEO_AND_ATTACHMENTS
                    }
                })
            End If

            If Preferences.SubtitleBehavior <> Format.SubtitleMerge.DISABLED Then
                If entry.ContainedMedia.HasFlag(MediaType.Subtitles) Then
                    useFile = True
                    args.SelectedStreams.Add(New FfmpegArguments.MapArgument() With {
                        .InputFileNumber = inputNumber,
                        .Selector = New FfmpegArguments.StreamSpecifier() With {
                                .Type = FfmpegArguments.StreamType.SUBTITLE
                        }
                    })
                    args.Codecs.Add(New FfmpegArguments.CodecArgument() With {
                        .Name = GetCodecName(Preferences.SubtitleBehavior),
                        .AppliedStream = New FfmpegArguments.StreamSpecifier() With {
                            .Type = FfmpegArguments.StreamType.SUBTITLE
                        }
                    })
                End If
            End If

            If useFile Then
                args.InputFiles.Add(entry.Location)
            End If

            Return useFile
        End Function

        Private Function GetCodecName(subtitleCodec As Format.SubtitleMerge) As FfmpegArguments.CodecName
            Select Case subtitleCodec
                Case Format.SubtitleMerge.COPY
                    Return FfmpegArguments.CodecName.COPY
                Case Format.SubtitleMerge.MOV_TEXT
                    Return FfmpegArguments.CodecName.SUBTITLE_MOV_TEXT
                Case Format.SubtitleMerge.SRT
                    Return FfmpegArguments.CodecName.SUBTITLE_SRT
                Case Else
                    ' There is no format for DISABLED
                    Return FfmpegArguments.CodecName.COPY
            End Select
        End Function

        Private Sub RunFfmpeg()
            ' TODO: Also re-encode if desired. Take care not to re-encode if merging into an existing video.


        End Sub
    End Class
End Namespace