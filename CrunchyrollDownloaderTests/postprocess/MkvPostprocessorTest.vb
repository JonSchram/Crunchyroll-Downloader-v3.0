Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.postprocess
Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports Crunchyroll_Downloader.utilities.ffmpeg.codec
Imports Crunchyroll_Downloader.utilities.ffmpeg.preset
Imports CrunchyrollDownloaderTests.utilities
Imports CrunchyrollDownloaderTests.utilities.ffmpeg
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports SiteAPI.api
Imports SiteAPI.api.common

Namespace postprocess
    <TestClass>
    Public Class MkvPostprocessorTest
        <TestMethod>
        Public Async Function TestProcessInputs_SingleFileMultipleStreams() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .MergeSoftSubtitles = False,
                .AudioCodec = AudioCodec.COPY,
                .VideoCodec = VideoCodec.COPY,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\file.ts", MediaType.Video Or MediaType.Audio, New Locale(Language.JAPANESE))
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mkv", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\file.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\video-reencode.mkv", args.OutputPath)

            Assert.AreEqual(2, args.SelectedStreams.Count)

            Dim videoStream As MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.VIDEO_AND_ATTACHMENTS
                }
            }, videoStream)

            Dim audioStream As MapArgument = args.SelectedStreams.Item(1)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.AUDIO
                }
            }, audioStream)

            Assert.AreEqual(2, args.Codecs.Count)

            AssertVideoCopy(args.Codecs.Item(0))
            AssertAudioCopy(args.Codecs.Item(1))

            Assert.AreEqual(Nothing, args.Preset)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_OutputFileExists() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            fakeFilesystem.AddFile("\temporary\path\video-reencode.mkv")
            fakeFilesystem.AddFile("\temporary\path\video-reencode (1).mkv")
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\file.ts", MediaType.Video Or MediaType.Audio, New Locale(Language.GERMAN))
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode (2).mkv", outputFiles.Item(0).Location)
            Assert.AreEqual("\temporary\path\video-reencode (2).mkv", adapter.RunArguments.OutputPath)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_MuxVideoAndAudio() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .MergeSoftSubtitles = False,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim mediaLocales As New Dictionary(Of MediaType, Locale) From {{MediaType.Audio, New Locale(Language.JAPANESE)}}
            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\audio.ts", MediaType.Audio, mediaLocales),
                New MediaFileEntry("\path\to\video.ts", MediaType.Video, mediaLocales)
            }

            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mkv", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(2, args.InputFiles.Count)
            Assert.AreEqual("\path\to\audio.ts", args.InputFiles.Item(0))
            Assert.AreEqual("\path\to\video.ts", args.InputFiles.Item(1))

            Assert.AreEqual("\temporary\path\video-reencode.mkv", args.OutputPath)

            Assert.AreEqual(2, args.SelectedStreams.Count)

            Dim audioStream As MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.AUDIO
                }
            }, audioStream)

            Dim videoStream As MapArgument = args.SelectedStreams.Item(1)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 1,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.VIDEO_AND_ATTACHMENTS
                }
            }, videoStream)

            Assert.AreEqual(2, args.Codecs.Count)

            AssertVideoCopy(args.Codecs.Item(0))
            AssertAudioCopy(args.Codecs.Item(1))

            Assert.AreEqual(Nothing, args.Preset)

        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_MuxAudioIntoMp4() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .MergeSoftSubtitles = False,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\audio.aac", MediaType.Audio, New Locale(Language.ITALIAN)),
                New MediaFileEntry("\path\to\video.mp4", MediaType.Video, New Locale(Language.PORTUGUESE))
            }

            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mkv", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(2, args.InputFiles.Count)
            Assert.AreEqual("\path\to\audio.aac", args.InputFiles.Item(0))
            Assert.AreEqual("\path\to\video.mp4", args.InputFiles.Item(1))

            Assert.AreEqual("\temporary\path\video-reencode.mkv", args.OutputPath)

            Assert.AreEqual(2, args.SelectedStreams.Count)

            Dim audioStream As MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.AUDIO
                }
            }, audioStream)

            Dim videoStream As MapArgument = args.SelectedStreams.Item(1)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 1,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.VIDEO_AND_ATTACHMENTS
                }
            }, videoStream)

            Assert.AreEqual(2, args.Codecs.Count)

            AssertVideoCopy(args.Codecs.Item(0))
            AssertAudioCopy(args.Codecs.Item(1))

            Assert.AreEqual(Nothing, args.Preset)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_MuxSubtitles_Copy() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .MergeSoftSubtitles = True,
                .SoftSubCodec = SubtitleCodec.COPY,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim locales As New Dictionary(Of MediaType, Locale) From {{MediaType.Audio, New Locale(Language.FRENCH)}}
            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\video_segments.ts", MediaType.Video Or MediaType.Audio, locales),
                New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles, New Locale(Language.JAPANESE))
            }

            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mkv", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video Or MediaType.Subtitles, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(2, args.InputFiles.Count)
            Assert.AreEqual("\path\to\video_segments.ts", args.InputFiles.Item(0))
            Assert.AreEqual("\path\to\subtitles.vtt", args.InputFiles.Item(1))

            Assert.AreEqual("\temporary\path\video-reencode.mkv", args.OutputPath)

            Assert.AreEqual(3, args.SelectedStreams.Count)

            Dim videoStream As MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.VIDEO_AND_ATTACHMENTS
                }
            }, videoStream)

            Dim audioStream As MapArgument = args.SelectedStreams.Item(1)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.AUDIO
                }
            }, audioStream)

            Dim subtitleStream As MapArgument = args.SelectedStreams.Item(2)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 1,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.SUBTITLE
                }
            }, subtitleStream)

            Assert.AreEqual(3, args.Codecs.Count)

            AssertVideoCopy(args.Codecs.Item(0))
            AssertAudioCopy(args.Codecs.Item(1))

            Assert.IsInstanceOfType(args.Codecs.Item(2), GetType(SubtitleCodecArgument))
            Dim sCodec As SubtitleCodecArgument = args.Codecs.Item(2)
            Assert.AreEqual(New StreamSpecifier() With {.Type = StreamType.SUBTITLE}, sCodec.AppliedStream)
            Assert.AreEqual(SubtitleCodec.COPY, sCodec.Codec)

            Assert.AreEqual(Nothing, args.Preset)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_MuxSubtitles_Ssa() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .MergeSoftSubtitles = True,
                .SoftSubCodec = SubtitleCodec.SSA,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\video_segments.ts", MediaType.Video Or MediaType.Audio, New Locale(Language.MANDARIN)),
                New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles, New Locale(Language.ENGLISH))
            }

            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mkv", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video Or MediaType.Subtitles, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(2, args.InputFiles.Count)
            Assert.AreEqual("\path\to\video_segments.ts", args.InputFiles.Item(0))
            Assert.AreEqual("\path\to\subtitles.vtt", args.InputFiles.Item(1))

            Assert.AreEqual("\temporary\path\video-reencode.mkv", args.OutputPath)

            Assert.AreEqual(3, args.SelectedStreams.Count)

            Dim videoStream As MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.VIDEO_AND_ATTACHMENTS
                }
            }, videoStream)

            Dim audioStream As MapArgument = args.SelectedStreams.Item(1)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.AUDIO
                }
            }, audioStream)

            Dim subtitleStream As MapArgument = args.SelectedStreams.Item(2)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 1,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.SUBTITLE
                }
            }, subtitleStream)

            Assert.AreEqual(3, args.Codecs.Count)

            AssertVideoCopy(args.Codecs.Item(0))
            AssertAudioCopy(args.Codecs.Item(1))

            Assert.IsInstanceOfType(args.Codecs.Item(2), GetType(SubtitleCodecArgument))
            Dim sCodec As SubtitleCodecArgument = args.Codecs.Item(2)
            Assert.AreEqual(New StreamSpecifier() With {.Type = StreamType.SUBTITLE}, sCodec.AppliedStream)
            Assert.AreEqual(SubtitleCodec.SSA, sCodec.Codec)

            Assert.AreEqual(Nothing, args.Preset)
        End Function

        <TestMethod>
        Public Function TestProcessInputs_MuxSubtitles_Movtext() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .MergeSoftSubtitles = True,
                .SoftSubCodec = SubtitleCodec.MOV_TEXT,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\video_segments.ts", MediaType.Video Or MediaType.Audio, New Locale(Language.ARABIC)),
                New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles, New Locale(Language.SPANISH))
            }

            Return Assert.ThrowsExceptionAsync(Of ArgumentException)(Async Function() As Task
                                                                         Await postProcessor.ProcessInputs(files)
                                                                     End Function)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_NoMuxSubtitles() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .MergeSoftSubtitles = False,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\file.ts", MediaType.Video Or MediaType.Audio, New Locale(Language.RUSSIAN)),
                New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles, New Locale(Language.RUSSIAN))
            }

            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(2, outputFiles.Count)
            Assert.IsTrue(outputFiles.Contains(
                          New MediaFileEntry("\temporary\path\video-reencode.mkv", MediaType.Audio Or MediaType.Video, New Locale(Language.RUSSIAN))))
            Assert.IsTrue(outputFiles.Contains(New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles, New Locale(Language.RUSSIAN))))

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\file.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\video-reencode.mkv", args.OutputPath)

            Assert.AreEqual(2, args.SelectedStreams.Count)

            Dim videoStream As MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.VIDEO_AND_ATTACHMENTS
                }
            }, videoStream)

            Dim audioStream As MapArgument = args.SelectedStreams.Item(1)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.AUDIO
                }
            }, audioStream)

            Assert.AreEqual(2, args.Codecs.Count)

            AssertVideoCopy(args.Codecs.Item(0))
            AssertAudioCopy(args.Codecs.Item(1))

            Assert.AreEqual(Nothing, args.Preset)
        End Function

        ''' <summary>
        ''' Tests that the correct video stream number is passed in the ffmpeg args when the subtitles are the first media item.
        ''' </summary>
        <TestMethod>
        Public Async Function TestProcessInputs_NoMuxSubtitles_SubtitlesFirst() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .MergeSoftSubtitles = False,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles, New Locale(Language.RUSSIAN)),
                New MediaFileEntry("\path\to\file.ts", MediaType.Video Or MediaType.Audio, New Locale(Language.FRENCH))
            }

            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(2, outputFiles.Count)
            Assert.IsTrue(outputFiles.Contains(
                          New MediaFileEntry("\temporary\path\video-reencode.mkv", MediaType.Audio Or MediaType.Video, New Locale(Language.FRENCH))))
            Assert.IsTrue(outputFiles.Contains(New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles, New Locale(Language.RUSSIAN))))

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\file.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\video-reencode.mkv", args.OutputPath)

            Assert.AreEqual(2, args.SelectedStreams.Count)

            Dim videoStream As MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New MapArgument() With {
            .InputFileNumber = 0,
            .Selector = New StreamSpecifier() With {
            .Type = StreamType.VIDEO_AND_ATTACHMENTS
                }
            }, videoStream)

            Dim audioStream As MapArgument = args.SelectedStreams.Item(1)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.AUDIO
                }
            }, audioStream)

            Assert.AreEqual(2, args.Codecs.Count)

            AssertVideoCopy(args.Codecs.Item(0))
            AssertAudioCopy(args.Codecs.Item(1))

            Assert.AreEqual(Nothing, args.Preset)
        End Function

        ''' <summary>
        ''' Tests that if the only input is audio, that the postprocessor does nothing.
        ''' </summary>
        ''' <returns></returns>
        <TestMethod>
        Public Async Function TestProcessInputs_AudioOnly() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .AudioCodec = AudioCodec.COPY,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\audio.ts", MediaType.Audio, New Locale(Language.ENGLISH, Region.UNITED_STATES))
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\path\to\audio.ts", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio, outputFiles.Item(0).ContainedMedia)
            Assert.AreEqual(New Locale(Language.ENGLISH, Region.UNITED_STATES), outputFiles.Item(0).StreamLocales.Item(MediaType.Audio))

            Assert.IsNull(adapter.RunArguments)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_VideoOnly() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .VideoCodec = VideoCodec.COPY,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\video.ts", MediaType.Video, New Locale(Language.SPANISH, Region.SPAIN))
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mkv", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Video, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\video.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\video-reencode.mkv", args.OutputPath)

            Assert.AreEqual(1, args.SelectedStreams.Count)

            Dim videoStream As MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.VIDEO_AND_ATTACHMENTS
                }
            }, videoStream)

            Assert.AreEqual(1, args.Codecs.Count)

            AssertVideoCopy(args.Codecs.Item(0))

            Assert.AreEqual(Nothing, args.Preset)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_SubtitlesOnly() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .MergeSoftSubtitles = False,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\subtitles.srt", MediaType.Subtitles, New Locale(Language.ITALIAN))
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\path\to\subtitles.srt", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Subtitles, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments
            ' Ffmpeg shouldn't have run
            Assert.IsNull(args)
        End Function

        ''' <summary>
        ''' Tests that the mp4 postprocessor doesn't run ffmpeg on a single file that contains video, audio, and subtitle streams,
        ''' even when subtitle merge is set to true (which would require ffmpeg if subtitles weren't already merged).
        ''' </summary>
        ''' <returns></returns>
        <TestMethod>
        Public Async Function TestProcessInputs_VideoAudioAndSubtitles() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .MergeSoftSubtitles = False,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim locales As New Dictionary(Of MediaType, Locale) From {
                {MediaType.Audio, New Locale(Language.JAPANESE)},
                {MediaType.Subtitles, New Locale(Language.ENGLISH)}
            }

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\everything.mkv", MediaType.Audio Or MediaType.Video Or MediaType.Subtitles, locales)
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\path\to\everything.mkv", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video Or MediaType.Subtitles, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments
            ' Ffmpeg shouldn't have run
            Assert.IsNull(args)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_ReencodeAsH265() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .VideoCodec = VideoCodec.LIBX265,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\video.ts", MediaType.Video Or MediaType.Audio, New Locale(Language.ARABIC))
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mkv", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Video Or MediaType.Audio, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\video.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\video-reencode.mkv", args.OutputPath)

            Assert.AreEqual(2, args.SelectedStreams.Count)

            Dim videoStream As MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.VIDEO_AND_ATTACHMENTS
                }
            }, videoStream)

            Dim audioStream As MapArgument = args.SelectedStreams.Item(1)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.AUDIO
                }
            }, audioStream)

            Assert.AreEqual(2, args.Codecs.Count)

            Assert.IsInstanceOfType(args.Codecs.Item(0), GetType(VideoCodecArgument))
            Dim vCodec As VideoCodecArgument = args.Codecs.Item(0)
            Assert.AreEqual(New StreamSpecifier() With {.Type = StreamType.VIDEO_ONLY}, vCodec.AppliedStream)
            Assert.AreEqual(VideoCodec.LIBX265, vCodec.Codec)

            AssertAudioCopy(args.Codecs.Item(1))

            Assert.AreEqual(Nothing, args.Preset)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_ReencodeAsH264_FastPreset() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .VideoCodec = VideoCodec.H264_NVENC,
                .VideoPreset = New SpeedPreset(Speed.FAST),
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\video.ts", MediaType.Video Or MediaType.Audio, New Locale(Language.ENGLISH))
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mkv", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\video.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\video-reencode.mkv", args.OutputPath)

            Assert.AreEqual(2, args.SelectedStreams.Count)

            Dim videoStream As MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New MapArgument() With {
                    .InputFileNumber = 0,
                    .Selector = New StreamSpecifier() With {
                            .Type = StreamType.VIDEO_AND_ATTACHMENTS
                    }
                }, videoStream)

            Dim audioStream As MapArgument = args.SelectedStreams.Item(1)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.AUDIO
                }
            }, audioStream)

            Assert.AreEqual(2, args.Codecs.Count)

            Assert.IsInstanceOfType(args.Codecs.Item(0), GetType(VideoCodecArgument))
            Dim vCodec As VideoCodecArgument = CType(args.Codecs.Item(0), VideoCodecArgument)
            Assert.AreEqual(New StreamSpecifier() With {.Type = StreamType.VIDEO_ONLY}, vCodec.AppliedStream)
            Assert.AreEqual(VideoCodec.H264_NVENC, vCodec.Codec)

            AssertAudioCopy(args.Codecs.Item(1))

            Assert.AreEqual(New SpeedPresetArgument(New SpeedPreset(Speed.FAST)), args.Preset)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_MultipleSubtitles() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .MergeSoftSubtitles = True,
                .SoftSubCodec = SubtitleCodec.COPY,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim locales As New Dictionary(Of MediaType, Locale) From {{MediaType.Audio, New Locale(Language.FRENCH)}}
            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\video_segments.ts", MediaType.Video Or MediaType.Audio, locales),
                New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles, New Locale(Language.JAPANESE)),
                New MediaFileEntry("\path\to\second_subtitles.vtt", MediaType.Subtitles, New Locale(Language.FRENCH))
            }

            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mkv", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video Or MediaType.Subtitles, outputFiles.Item(0).ContainedMedia)

            Assert.IsFalse(outputFiles.Item(0).StreamLocales.ContainsKey(MediaType.Video))
            Assert.AreEqual(New Locale(Language.FRENCH), outputFiles.Item(0).StreamLocales.Item(MediaType.Audio))
            Assert.AreEqual(New Locale(Language.MULTIPLE), outputFiles.Item(0).StreamLocales.Item(MediaType.Subtitles))

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(3, args.InputFiles.Count)
            Assert.AreEqual("\path\to\video_segments.ts", args.InputFiles.Item(0))
            Assert.AreEqual("\path\to\subtitles.vtt", args.InputFiles.Item(1))
            Assert.AreEqual("\path\to\second_subtitles.vtt", args.InputFiles.Item(2))

            Assert.AreEqual("\temporary\path\video-reencode.mkv", args.OutputPath)

            Assert.AreEqual(4, args.SelectedStreams.Count)

            Dim videoStream As MapArgument = args.SelectedStreams.Item(0)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.VIDEO_AND_ATTACHMENTS
                }
            }, videoStream)

            Dim audioStream As MapArgument = args.SelectedStreams.Item(1)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 0,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.AUDIO
                }
            }, audioStream)

            Dim subtitleStream As MapArgument = args.SelectedStreams.Item(2)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 1,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.SUBTITLE
                }
            }, subtitleStream)

            Dim subtitleStream2 As MapArgument = args.SelectedStreams.Item(3)
            Assert.AreEqual(New MapArgument() With {
                .InputFileNumber = 2,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.SUBTITLE
                }
            }, subtitleStream2)

            Assert.AreEqual(3, args.Codecs.Count)

            AssertVideoCopy(args.Codecs.Item(0))
            AssertAudioCopy(args.Codecs.Item(1))

            Assert.IsInstanceOfType(args.Codecs.Item(2), GetType(SubtitleCodecArgument))
            Dim sCodec As SubtitleCodecArgument = args.Codecs.Item(2)
            Assert.AreEqual(New StreamSpecifier() With {.Type = StreamType.SUBTITLE}, sCodec.AppliedStream)
            Assert.AreEqual(SubtitleCodec.COPY, sCodec.Codec)

            Assert.AreEqual(Nothing, args.Preset)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_MultipleSubtitles_SameLanguage() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .MergeSoftSubtitles = True,
                .SoftSubCodec = SubtitleCodec.COPY,
                .TemporaryOutputPath = "\temporary\path"
            }

            Dim adapter = New FakeFfmpegAdapter()
            Dim fakeFilesystem = New FakeFileSystem()
            Dim postProcessor As New MkvPostprocessor(prefs, adapter, fakeFilesystem)

            Dim locales As New Dictionary(Of MediaType, Locale) From {{MediaType.Audio, New Locale(Language.FRENCH)}}
            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\video_segments.ts", MediaType.Video Or MediaType.Audio, locales),
                New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles, New Locale(Language.JAPANESE)),
                New MediaFileEntry("\path\to\second_subtitles.vtt", MediaType.Subtitles, New Locale(Language.JAPANESE))
            }

            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mkv", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video Or MediaType.Subtitles, outputFiles.Item(0).ContainedMedia)

            Assert.IsFalse(outputFiles.Item(0).StreamLocales.ContainsKey(MediaType.Video))
            Assert.AreEqual(New Locale(Language.FRENCH), outputFiles.Item(0).StreamLocales.Item(MediaType.Audio))
            Assert.AreEqual(New Locale(Language.JAPANESE), outputFiles.Item(0).StreamLocales.Item(MediaType.Subtitles))
        End Function

        Private Shared Sub AssertAudioCopy(argument As ICodecArgument)
            Assert.IsInstanceOfType(argument, GetType(AudioCodecArgument))
            Dim aCodec As AudioCodecArgument = argument
            Assert.AreEqual(New StreamSpecifier() With {
                                .Type = StreamType.AUDIO
                            }, aCodec.AppliedStream)
            Assert.AreEqual(AudioCodec.COPY, aCodec.Codec)
        End Sub

        Private Shared Sub AssertVideoCopy(argument As ICodecArgument)
            Assert.IsInstanceOfType(argument, GetType(VideoCodecArgument))
            Dim vCodec As VideoCodecArgument = argument
            Assert.AreEqual(New StreamSpecifier() With {
                                .Type = StreamType.VIDEO_ONLY
                            }, vCodec.AppliedStream)
            Assert.AreEqual(VideoCodec.COPY, vCodec.Codec)
        End Sub

    End Class
End Namespace