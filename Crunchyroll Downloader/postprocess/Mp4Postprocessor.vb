Imports System.IO
Imports Crunchyroll_Downloader.api.common
Imports Crunchyroll_Downloader.api.metadata
Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.settings
Imports Crunchyroll_Downloader.utilities

Namespace postprocess

    ''' <summary>
    ''' Class that remuxes any video/audio as an mp4 file.
    ''' </summary>
    Public Class Mp4Postprocessor

        Private ReadOnly Preferences As OutputPreferences
        Private ReadOnly FfmpegRunner As IFfmpegAdapter

        Public Sub New(prefs As OutputPreferences, ffmpegRunner As IFfmpegAdapter)
            Preferences = prefs
            Me.FfmpegRunner = ffmpegRunner
        End Sub

        Public Sub ProcessInputs(files As List(Of DownloadEntry), ep As Episode)
            Dim nameGenerator = If(Preferences.UseKodiNaming,
                FilenameInterpolator.CreateKodiNamingInstance(),
                New FilenameInterpolator(Preferences.NameTemplate))

            ' TODO: Append language name according to subtitle naming preference.
            Dim filename = nameGenerator.CreateName(ep, False)
            Dim outputPath As String = Preferences.OutputPath

            Dim completeOutput As String = Path.Combine(outputPath, filename)

            ' TODO: Handle existing file.
            Dim args As FfmpegArguments = New FfmpegArguments($"{completeOutput}.mp4")

            For fileNumber = 0 To files.Count - 1
                Dim file As DownloadEntry = files(fileNumber)

                ProcessFile(file, args, fileNumber)
            Next

            FfmpegRunner.Run(args)

            ' TODO: Copy subtitle if not merged.

        End Sub

        Private Sub ProcessFile(entry As DownloadEntry, args As FfmpegArguments, inputNumber As Integer)
            args.InputFiles.Add(entry.Uri)

            ' TODO: Maybe only select individual tracks if there is a need? Seems very verbose.
            ' Also have to consider what happens if the output file exists already.

            If entry.ContainedMedia.HasFlag(MediaType.Audio) Then
                args.SelectedStreams.Add(New FfmpegArguments.MapArgument() With {
                    .InputFileNumber = inputNumber,
                    .Selector = New FfmpegArguments.StreamSpecifier() With {
                        .Type = FfmpegArguments.StreamType.AUDIO
                    }
                })
            End If
            If entry.ContainedMedia.HasFlag(MediaType.Video) Then
                args.SelectedStreams.Add(New FfmpegArguments.MapArgument() With {
                    .InputFileNumber = inputNumber,
                    .Selector = New FfmpegArguments.StreamSpecifier() With {
                            .Type = FfmpegArguments.StreamType.VIDEO_AND_ATTACHMENTS
                    }
                })
            End If

            If Preferences.SubtitleBehavior <> Format.SubtitleMerge.DISABLED Then
                If entry.ContainedMedia.HasFlag(MediaType.Subtitles) Then
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
        End Sub

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