﻿Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.postprocess
Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.settings
Imports Crunchyroll_Downloader.settings.ffmpeg
Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports CrunchyrollDownloaderTests.utilities
Imports CrunchyrollDownloaderTests.utilities.ffmpeg
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports SiteAPI.api.common

Namespace postprocess
    <TestClass>
    Public Class Mp4PostprocessorTest
        <TestMethod>
        Public Async Function TestProcessInputs_SingleFileMultipleStreams() As Task

            Dim commandBuilder As New FfmpegOptions.Builder()
            commandBuilder.SetCopyMode(True)

            Dim prefs As New ReencodePreferences() With {
                .OutputFormat = Format.ContainerFormat.MP4,
                .PostprocessSettings = commandBuilder.Build(),
                .SubtitleBehavior = Format.SubtitleMerge.DISABLED,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\file.ts", MediaType.Video Or MediaType.Audio)
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\file-reencode.mp4", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\file.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\file-reencode.mp4", args.OutputPath)

            Assert.AreEqual(2, args.SelectedStreams.Count)

            Dim audioStream As FfmpegArguments.MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New FfmpegArguments.MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.AUDIO
                }
            }, audioStream)

            Dim videoStream As FfmpegArguments.MapArgument = args.SelectedStreams.Item(1)
            Assert.AreEqual(New FfmpegArguments.MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.VIDEO_AND_ATTACHMENTS
                }
            }, videoStream)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_OutputFileExists() As Task

            Dim commandBuilder As New FfmpegOptions.Builder()
            commandBuilder.SetCopyMode(True)

            Dim prefs As New ReencodePreferences() With {
                .OutputFormat = Format.ContainerFormat.MP4,
                .PostprocessSettings = commandBuilder.Build(),
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            fakeFilesystem.AddFile("\temporary\path\file-reencode.mp4")
            fakeFilesystem.AddFile("\temporary\path\file-reencode (1).mp4")
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\file.ts", MediaType.Video Or MediaType.Audio)
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\file-reencode (2).mp4", outputFiles.Item(0).Location)
            Assert.AreEqual("\temporary\path\file-reencode (2).mp4", adapter.RunArguments.OutputPath)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_MuxVideoAndAudio() As Task

            Dim commandBuilder As New FfmpegOptions.Builder()
            commandBuilder.SetCopyMode(True)

            Dim prefs As New ReencodePreferences() With {
                .OutputFormat = Format.ContainerFormat.MP4,
                .PostprocessSettings = commandBuilder.Build(),
                .SubtitleBehavior = Format.SubtitleMerge.DISABLED,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\audio.ts", MediaType.Audio),
                New MediaFileEntry("\path\to\video.ts", MediaType.Video)
            }

            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mp4", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(2, args.InputFiles.Count)
            Assert.AreEqual("\path\to\audio.ts", args.InputFiles.Item(0))
            Assert.AreEqual("\path\to\video.ts", args.InputFiles.Item(1))

            Assert.AreEqual("\temporary\path\video-reencode.mp4", args.OutputPath)

            Assert.AreEqual(2, args.SelectedStreams.Count)

            Dim audioStream As FfmpegArguments.MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New FfmpegArguments.MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.AUDIO
                }
            }, audioStream)

            Dim videoStream As FfmpegArguments.MapArgument = args.SelectedStreams.Item(1)
            Assert.AreEqual(New FfmpegArguments.MapArgument() With {
                .InputFileNumber = 1,
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.VIDEO_AND_ATTACHMENTS
                }
            }, videoStream)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_MuxSubtitles() As Task

            Dim commandBuilder As New FfmpegOptions.Builder()
            commandBuilder.SetCopyMode(True)

            Dim prefs As New ReencodePreferences() With {
                .OutputFormat = Format.ContainerFormat.MP4,
                .PostprocessSettings = commandBuilder.Build(),
                .SubtitleBehavior = Format.SubtitleMerge.COPY,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\video_segments.ts", MediaType.Video Or MediaType.Audio),
                New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles)
            }

            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video_segments-reencode.mp4", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video Or MediaType.Subtitles, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(2, args.InputFiles.Count)
            Assert.AreEqual("\path\to\video_segments.ts", args.InputFiles.Item(0))
            Assert.AreEqual("\path\to\subtitles.vtt", args.InputFiles.Item(1))

            Assert.AreEqual("\temporary\path\video_segments-reencode.mp4", args.OutputPath)

            Assert.AreEqual(3, args.SelectedStreams.Count)

            Dim audioStream As FfmpegArguments.MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New FfmpegArguments.MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.AUDIO
                }
            }, audioStream)

            Dim videoStream As FfmpegArguments.MapArgument = args.SelectedStreams.Item(1)
            Assert.AreEqual(New FfmpegArguments.MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.VIDEO_AND_ATTACHMENTS
                }
            }, videoStream)

            Dim subtitleStream As FfmpegArguments.MapArgument = args.SelectedStreams.Item(2)
            Assert.AreEqual(New FfmpegArguments.MapArgument() With {
                .InputFileNumber = 1,
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.SUBTITLE
                }
            }, subtitleStream)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_NoMuxSubtitles() As Task

            Dim commandBuilder As New FfmpegOptions.Builder()
            commandBuilder.SetCopyMode(True)

            Dim prefs As New ReencodePreferences() With {
                .OutputFormat = Format.ContainerFormat.MP4,
                .PostprocessSettings = commandBuilder.Build(),
                .SubtitleBehavior = Format.SubtitleMerge.DISABLED,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\file.ts", MediaType.Video Or MediaType.Audio),
                New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles)
            }

            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(2, outputFiles.Count)
            Assert.IsTrue(outputFiles.Contains(New MediaFileEntry("\temporary\path\file-reencode.mp4", MediaType.Audio Or MediaType.Video)))
            Assert.IsTrue(outputFiles.Contains(New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles)))

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\file.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\file-reencode.mp4", args.OutputPath)

            Assert.AreEqual(2, args.SelectedStreams.Count)

            Dim audioStream As FfmpegArguments.MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New FfmpegArguments.MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.AUDIO
                }
            }, audioStream)

            Dim videoStream As FfmpegArguments.MapArgument = args.SelectedStreams.Item(1)
            Assert.AreEqual(New FfmpegArguments.MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.VIDEO_AND_ATTACHMENTS
                }
            }, videoStream)
        End Function

        ''' <summary>
        ''' Tests that the correct video stream number is passed in the ffmpeg args when the subtitles are the first media item.
        ''' </summary>
        <TestMethod>
        Public Async Function TestProcessInputs_NoMuxSubtitles_SubtitlesFirst() As Task
            Dim commandBuilder As New FfmpegOptions.Builder()
            commandBuilder.SetCopyMode(True)

            Dim prefs As New ReencodePreferences() With {
                .OutputFormat = Format.ContainerFormat.MP4,
                .PostprocessSettings = commandBuilder.Build(),
                .SubtitleBehavior = Format.SubtitleMerge.DISABLED,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles),
                New MediaFileEntry("\path\to\file.ts", MediaType.Video Or MediaType.Audio)
            }

            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(2, outputFiles.Count)
            Assert.IsTrue(outputFiles.Contains(New MediaFileEntry("\temporary\path\file-reencode.mp4", MediaType.Audio Or MediaType.Video)))
            Assert.IsTrue(outputFiles.Contains(New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles)))

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\file.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\file-reencode.mp4", args.OutputPath)

            Assert.AreEqual(2, args.SelectedStreams.Count)

            Dim audioStream As FfmpegArguments.MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New FfmpegArguments.MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.AUDIO
                }
            }, audioStream)

            Dim videoStream As FfmpegArguments.MapArgument = args.SelectedStreams.Item(1)
            Assert.AreEqual(New FfmpegArguments.MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.VIDEO_AND_ATTACHMENTS
                }
            }, videoStream)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_AudioOnly() As Task

            Dim commandBuilder As New FfmpegOptions.Builder()
            commandBuilder.SetCopyMode(True)

            Dim prefs As New ReencodePreferences() With {
                .OutputFormat = Format.ContainerFormat.MP4,
                .PostprocessSettings = commandBuilder.Build(),
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\audio.ts", MediaType.Audio)
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\audio-reencode.mp4", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\audio.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\audio-reencode.mp4", args.OutputPath)

            Assert.AreEqual(1, args.SelectedStreams.Count)

            Dim audioStream As FfmpegArguments.MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New FfmpegArguments.MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.AUDIO
                }
            }, audioStream)

        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_SubtitlesOnly() As Task
            Dim commandBuilder As New FfmpegOptions.Builder()
            commandBuilder.SetCopyMode(True)

            Dim prefs As New ReencodePreferences() With {
                .SubtitleBehavior = Format.SubtitleMerge.DISABLED,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\subtitles.srt", MediaType.Subtitles)
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\path\to\subtitles.srt", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Subtitles, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments
            ' Ffmpeg shouldn't have run
            Assert.IsNull(args)
        End Function
    End Class
End Namespace