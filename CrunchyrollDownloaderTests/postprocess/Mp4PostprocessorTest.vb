Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.postprocess
Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports Crunchyroll_Downloader.utilities.ffmpeg.codec
Imports Crunchyroll_Downloader.utilities.ffmpeg.preset
Imports CrunchyrollDownloaderTests.utilities
Imports CrunchyrollDownloaderTests.utilities.ffmpeg
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports SiteAPI.api.common

Namespace postprocess
    <TestClass>
    Public Class Mp4PostprocessorTest
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
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\file.ts", MediaType.Video Or MediaType.Audio)
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mp4", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\file.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\video-reencode.mp4", args.OutputPath)

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
            fakeFilesystem.AddFile("\temporary\path\video-reencode.mp4")
            fakeFilesystem.AddFile("\temporary\path\video-reencode (1).mp4")
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\file.ts", MediaType.Video Or MediaType.Audio)
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode (2).mp4", outputFiles.Item(0).Location)
            Assert.AreEqual("\temporary\path\video-reencode (2).mp4", adapter.RunArguments.OutputPath)
        End Function

        <TestMethod>
        Public Async Function TestProcessInputs_MuxVideoAndAudio() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .MergeSoftSubtitles = False,
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
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\audio.aac", MediaType.Audio),
                New MediaFileEntry("\path\to\video.mp4", MediaType.Video)
            }

            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mp4", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(2, args.InputFiles.Count)
            Assert.AreEqual("\path\to\audio.aac", args.InputFiles.Item(0))
            Assert.AreEqual("\path\to\video.mp4", args.InputFiles.Item(1))

            Assert.AreEqual("\temporary\path\video-reencode.mp4", args.OutputPath)

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
        Public Async Function TestProcessInputs_MuxSubtitles() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .MergeSoftSubtitles = True,
                .SoftSubCodec = SubtitleCodec.COPY,
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
            Assert.AreEqual("\temporary\path\video-reencode.mp4", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video Or MediaType.Subtitles, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(2, args.InputFiles.Count)
            Assert.AreEqual("\path\to\video_segments.ts", args.InputFiles.Item(0))
            Assert.AreEqual("\path\to\subtitles.vtt", args.InputFiles.Item(1))

            Assert.AreEqual("\temporary\path\video-reencode.mp4", args.OutputPath)

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
        Public Async Function TestProcessInputs_NoMuxSubtitles() As Task
            Dim prefs As New VideoReencodePreferences() With {
                .MergeSoftSubtitles = False,
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
            Assert.IsTrue(outputFiles.Contains(New MediaFileEntry("\temporary\path\video-reencode.mp4", MediaType.Audio Or MediaType.Video)))
            Assert.IsTrue(outputFiles.Contains(New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles)))

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\file.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\video-reencode.mp4", args.OutputPath)

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
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles),
                New MediaFileEntry("\path\to\file.ts", MediaType.Video Or MediaType.Audio)
            }

            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(2, outputFiles.Count)
            Assert.IsTrue(outputFiles.Contains(New MediaFileEntry("\temporary\path\video-reencode.mp4", MediaType.Audio Or MediaType.Video)))
            Assert.IsTrue(outputFiles.Contains(New MediaFileEntry("\path\to\subtitles.vtt", MediaType.Subtitles)))

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\file.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\video-reencode.mp4", args.OutputPath)

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
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\audio.ts", MediaType.Audio)
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\path\to\audio.ts", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio, outputFiles.Item(0).ContainedMedia)

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
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\video.ts", MediaType.Video)
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mp4", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Video, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\video.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\video-reencode.mp4", args.OutputPath)

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
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\everything.mp4", MediaType.Audio Or MediaType.Video Or MediaType.Subtitles)
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\path\to\everything.mp4", outputFiles.Item(0).Location)
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
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\video.ts", MediaType.Video Or MediaType.Audio)
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mp4", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Video Or MediaType.Audio, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\video.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\video-reencode.mp4", args.OutputPath)

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
            Dim postProcessor As New Mp4Postprocessor(prefs, adapter, fakeFilesystem)

            Dim files As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\path\to\video.ts", MediaType.Video Or MediaType.Audio)
            }
            Dim outputFiles As List(Of MediaFileEntry) = Await postProcessor.ProcessInputs(files)

            Assert.AreEqual(1, outputFiles.Count)
            Assert.AreEqual("\temporary\path\video-reencode.mp4", outputFiles.Item(0).Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video, outputFiles.Item(0).ContainedMedia)

            Dim args As FfmpegArguments = adapter.RunArguments

            Assert.AreEqual(1, args.InputFiles.Count)
            Assert.AreEqual("\path\to\video.ts", args.InputFiles.Item(0))

            Assert.AreEqual("\temporary\path\video-reencode.mp4", args.OutputPath)

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