Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.debugging
Imports Crunchyroll_Downloader.postprocess
Imports Crunchyroll_Downloader.settings
Imports Crunchyroll_Downloader.settings.ffmpeg
Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports CrunchyrollDownloaderTests.utilities.ffmpeg
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports SiteAPI.api.common

Namespace postprocess
    <TestClass>
    Public Class Mp4PostprocessorTest
        <TestMethod>
        Public Sub TestProcessInputs_SingleFileMultipleStreams()

            Dim commandBuilder As New FfmpegOptions.Builder()
            commandBuilder.SetCopyMode(True)

            Dim prefs As New OutputPreferences() With {
                .OutputPath = "final\output\path",
                .NameTemplate = "EpisodeName;",
                .UseKodiNaming = False,
                .OutputFormat = Format.MediaFormat.MP4,
                .PostprocessSettings = commandBuilder.Build(),
                .SubtitleBehavior = Format.SubtitleMerge.DISABLED
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("path\to\file.ts", MediaType.Video Or MediaType.Audio)
            }


            Dim ep As New FakeEpisode() With {
                .EpisodeName = "Fake episode name",
                .ShowName = "Fake show",
                .SeasonNumber = 1,
                .EpisodeNumber = 1
            }
            postProcessor.ProcessInputs(files, ep)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("path\to\file.ts", args.InputFiles.Item(0))

            Assert.AreEqual("final\output\path\Fake episode name.mp4", args.OutputPath)

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
        End Sub

        <TestMethod>
        Public Sub TestProcessInputs_MuxVideoAndAudio()

            Dim commandBuilder As New FfmpegOptions.Builder()
            commandBuilder.SetCopyMode(True)

            Dim prefs As New OutputPreferences() With {
                .OutputPath = "final\output\path",
                .NameTemplate = "EpisodeName;",
                .UseKodiNaming = False,
                .OutputFormat = Format.MediaFormat.MP4,
                .PostprocessSettings = commandBuilder.Build(),
                .SubtitleBehavior = Format.SubtitleMerge.DISABLED
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("path\to\audio.ts", MediaType.Audio),
                New MediaFileEntry("path\to\video.ts", MediaType.Video)
            }


            Dim ep As New FakeEpisode() With {
                .EpisodeName = "Fake episode name",
                .ShowName = "Fake show",
                .SeasonNumber = 1,
                .EpisodeNumber = 1
            }
            postProcessor.ProcessInputs(files, ep)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(2, args.InputFiles.Count)
            Assert.AreEqual("path\to\audio.ts", args.InputFiles.Item(0))
            Assert.AreEqual("path\to\video.ts", args.InputFiles.Item(1))

            Assert.AreEqual("final\output\path\Fake episode name.mp4", args.OutputPath)

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
        End Sub

        <TestMethod>
        Public Sub TestProcessInputs_MuxSubtitles()

            Dim commandBuilder As New FfmpegOptions.Builder()
            commandBuilder.SetCopyMode(True)

            Dim prefs As New OutputPreferences() With {
                .OutputPath = "final\output\path",
                .NameTemplate = "EpisodeName;",
                .UseKodiNaming = False,
                .OutputFormat = Format.MediaFormat.MP4,
                .PostprocessSettings = commandBuilder.Build(),
                .SubtitleBehavior = Format.SubtitleMerge.COPY
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("path\to\file.ts", MediaType.Video Or MediaType.Audio),
                New MediaFileEntry("path\to\subtitles.vtt", MediaType.Subtitles)
            }


            Dim ep As New FakeEpisode() With {
                .EpisodeName = "Fake episode name",
                .ShowName = "Fake show",
                .SeasonNumber = 1,
                .EpisodeNumber = 1
            }
            postProcessor.ProcessInputs(files, ep)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(2, args.InputFiles.Count)
            Assert.AreEqual("path\to\file.ts", args.InputFiles.Item(0))
            Assert.AreEqual("path\to\subtitles.vtt", args.InputFiles.Item(1))

            Assert.AreEqual("final\output\path\Fake episode name.mp4", args.OutputPath)

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
        End Sub

        <TestMethod>
        Public Sub TestProcessInputs_NoMuxSubtitles()

            Dim commandBuilder As New FfmpegOptions.Builder()
            commandBuilder.SetCopyMode(True)

            Dim prefs As New OutputPreferences() With {
                .OutputPath = "final\output\path",
                .NameTemplate = "EpisodeName;",
                .UseKodiNaming = False,
                .OutputFormat = Format.MediaFormat.MP4,
                .PostprocessSettings = commandBuilder.Build(),
                .SubtitleBehavior = Format.SubtitleMerge.DISABLED
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("path\to\file.ts", MediaType.Video Or MediaType.Audio),
                New MediaFileEntry("path\to\subtitles.vtt", MediaType.Subtitles)
            }


            Dim ep As New FakeEpisode() With {
                .EpisodeName = "Fake episode name",
                .ShowName = "Fake show",
                .SeasonNumber = 1,
                .EpisodeNumber = 1
            }
            postProcessor.ProcessInputs(files, ep)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(2, args.InputFiles.Count)
            Assert.AreEqual("path\to\file.ts", args.InputFiles.Item(0))
            Assert.AreEqual("path\to\subtitles.vtt", args.InputFiles.Item(1))

            Assert.AreEqual("final\output\path\Fake episode name.mp4", args.OutputPath)

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
        End Sub
    End Class
End Namespace