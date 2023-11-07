Imports System.IO
Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.utilities
Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports Crunchyroll_Downloader.utilities.ffmpeg.codec
Imports Crunchyroll_Downloader.utilities.ffmpeg.preset
Imports SiteAPI.api.common

Namespace postprocess
    ''' <summary>
    ''' A postprocessor that produces video files. If only an audio file is passed in, this does nothing. It is not designed to handle audio-only input.
    ''' </summary>
    Public MustInherit Class VideoPostprocessor


        Private ReadOnly Preferences As VideoReencodePreferences
        Private ReadOnly FfmpegRunner As IFfmpegAdapter
        Private ReadOnly FilesystemApi As IFilesystem

        Public Sub New(prefs As VideoReencodePreferences, ffmpegRunner As IFfmpegAdapter, fileSystemApi As IFilesystem)
            Preferences = prefs
            Me.FfmpegRunner = ffmpegRunner
            Me.FilesystemApi = fileSystemApi
        End Sub

        ''' <summary>
        ''' The format of the output file. Must include a leading '.'
        ''' </summary>
        ''' <returns></returns>
        Protected MustOverride Function GetFileExtension() As String

        Protected MustOverride Function GetAllowedSubtitleCodecs() As List(Of SubtitleCodec)

        Public Async Function ProcessInputs(files As List(Of MediaFileEntry)) As Task(Of List(Of MediaFileEntry))
            If IsNoOP(files) Then
                Return files
            End If

            Dim outputFiles As New List(Of MediaFileEntry)

            ' Ensure that the output file won't exist.
            Dim temporaryOutput = GetUniqueFilename(FilesystemApi, Preferences.TemporaryOutputPath, "video-reencode", GetFileExtension())

            Dim args As New FfmpegArguments(temporaryOutput)
            Dim audioExists As Boolean = GetNumberOfInputsOfType(files, MediaType.Audio) > 0
            Dim mergeSubtitles As Boolean = Preferences.MergeSoftSubtitles And Preferences.SoftSubCodec.HasValue
            AddCodecs(args, audioExists, mergeSubtitles)

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

            ' TODO: Delete input files that have been reencoded.

            Return outputFiles
        End Function

        Private Function ProcessFile(entry As MediaFileEntry, args As FfmpegArguments, inputNumber As Integer) As Boolean
            Dim useFile As Boolean = False

            ' TODO: Maybe only select individual tracks if there is a need? Seems very verbose.

            ' Map video stream first because the order of streams in the output file is determined by the map order.
            ' It should work in any order but by convention video is the first stream.
            If entry.ContainedMedia.HasFlag(MediaType.Video) Then
                useFile = True
                args.SelectedStreams.Add(New MapArgument() With {
                        .InputFileNumber = inputNumber,
                        .Selector = New StreamSpecifier() With {
                                .Type = StreamType.VIDEO_AND_ATTACHMENTS
                        }
                })
            End If

            If entry.ContainedMedia.HasFlag(MediaType.Audio) Then
                useFile = True
                args.SelectedStreams.Add(New MapArgument() With {
                        .InputFileNumber = inputNumber,
                        .Selector = New StreamSpecifier() With {
                                .Type = StreamType.AUDIO
                        }
                })
            End If

            If Preferences.MergeSoftSubtitles And entry.ContainedMedia.HasFlag(MediaType.Subtitles) Then
                useFile = True
                args.SelectedStreams.Add(New MapArgument() With {
                        .InputFileNumber = inputNumber,
                        .Selector = New StreamSpecifier() With {
                                .Type = StreamType.SUBTITLE
                        }
                })
            End If

            If useFile Then
                args.InputFiles.Add(entry.Location)
            End If

            Return useFile
        End Function

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

        ''' <summary>
        ''' Determines whether the postprocessor will do anything. No operation is needed when the input file is in the correct
        ''' format and there is no re-encoding or remuxing needs to be done.
        ''' </summary>
        ''' <param name="files"></param>
        ''' <returns></returns>
        Private Function IsNoOP(files As List(Of MediaFileEntry)) As Boolean
            Dim numberOfVideoInputs = GetNumberOfInputsOfType(files, MediaType.Video)
            If numberOfVideoInputs = 0 Then
                ' No video files, nothing to do.
                Return True
            End If

            Dim videoIsCorrectFormat = VideoFileIsCorrectExtension(files)
            If numberOfVideoInputs = 1 And videoIsCorrectFormat Then
                ' Guaranteed to not be null because numberOfVideoInputs > 0.
                Dim videoFile = GetFirstVideoFile(files)
                If Not videoFile.ContainedMedia.HasFlag(MediaType.Audio) And GetNumberOfInputsOfType(files, MediaType.Audio) > 0 Then
                    ' There is audio, but it isn't in the video file. It is already clear a remux is needed.
                    Return False
                End If
                Dim numberOfSubtitleInputs = GetNumberOfInputsOfType(files, MediaType.Subtitles)
                If Preferences.VideoCodec = VideoCodec.COPY And Preferences.AudioCodec = AudioCodec.COPY Then
                    If (numberOfSubtitleInputs > 0 And Not Preferences.MergeSoftSubtitles) Or numberOfSubtitleInputs = 0 Then
                        ' One video file in the correct format, with both video codecs set to copy, with no subtitles to merge.
                        ' There is nothing to do.
                        Return True
                    ElseIf numberOfSubtitleInputs = 1 And videoFile.ContainedMedia.HasFlag(MediaType.Subtitles) Then
                        ' If the video file also contains the only subtitle stream, no encoding necessary.
                        Return True
                    End If
                End If
            End If
            ' There is one or more video files that may or may not be in the wrong format, and possibly needing re-encoding,
            ' or there are one or more subtitle files needing to be merged.
            Return False
        End Function

        Private Function GetNumberOfInputsOfType(files As List(Of MediaFileEntry), streamType As MediaType) As Integer
            Dim numberOfStreams As Integer = 0

            For Each file In files
                If file.ContainedMedia.HasFlag(streamType) Then
                    numberOfStreams += 1
                End If
            Next

            Return numberOfStreams
        End Function

        Private Function GetFirstVideoFile(files As List(Of MediaFileEntry)) As MediaFileEntry
            For Each file In files
                If file.ContainedMedia.HasFlag(MediaType.Video) Then
                    Return file
                End If
            Next
            Return Nothing
        End Function

        Private Function VideoFileIsCorrectExtension(files As List(Of MediaFileEntry)) As Boolean
            For Each file In files
                If file.ContainedMedia.HasFlag(MediaType.Video) Then
                    Return GetFileExtension().Equals(Path.GetExtension(file.Location))
                End If
            Next

            ' If no video is found, this is vacuously true.
            Return True
        End Function

        Private Sub AddCodecs(options As FfmpegArguments, addAudioCodec As Boolean, addSubtitleCodec As Boolean)
            options.Codecs.Add(New VideoCodecArgument(New StreamSpecifier() With {
                    .Type = StreamType.VIDEO_ONLY
                }, Preferences.VideoCodec))
            If addAudioCodec Then
                options.Codecs.Add(New AudioCodecArgument(New StreamSpecifier() With {
                    .Type = StreamType.AUDIO
                }, Preferences.AudioCodec))
            End If
            If addSubtitleCodec Then
                options.Codecs.Add(New SubtitleCodecArgument(New StreamSpecifier() With {
                    .Type = StreamType.SUBTITLE
                }, Preferences.SoftSubCodec.Value))
            End If
        End Sub

        Private Sub ApplyPreset(args As FfmpegArguments)
            Dim preset = Preferences.VideoPreset
            If preset Is Nothing Then
                Return
            End If
            If TypeOf preset Is SpeedPreset Then
                args.Preset = New SpeedPresetArgument(CType(preset, SpeedPreset))
            ElseIf TypeOf preset Is QualityPreset Then
                args.Preset = New QualityPresetArgument(CType(preset, QualityPreset))
            End If
        End Sub


        Private Sub HandleFfmpegProgress(amount As Integer)
            Debug.WriteLine($"Mp4 postprocessor ffmpeg progress reported: {amount}")
        End Sub

    End Class
End Namespace