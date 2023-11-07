Imports Crunchyroll_Downloader.utilities
Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports Crunchyroll_Downloader.utilities.ffmpeg.codec
Imports Crunchyroll_Downloader.utilities.ffmpeg.preset
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PlaylistLibrary.hls.common

Namespace utilities.ffmpeg
    <TestClass>
    Public Class FfmpegCommandBuilderTest
        <TestMethod>
        Public Sub TestBuildCookies()
            Dim cookies As New Dictionary(Of String, String) From {
                {"Cookie1", "value1"},
                {"Cookie2", "value2"}
            }
            Dim ffmpegArgs As New FfmpegArguments("test_path") With {
                .Cookies = cookies
            }

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs)

            Assert.AreEqual($"-headers ""Cookie: Cookie1=value1; Cookie2=value2{vbCrLf}"" ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildProgramMap()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.SelectedStreams.Add(New MapArgument() With {
                .Selector = New StreamSpecifier() With {
                    .ProgramNumber = 4
                }
            })

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs)

            Assert.AreEqual("-i ""input_path"" -map 0:p:4 ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildVideoMap()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.SelectedStreams.Add(New MapArgument() With {
                .InputFileNumber = 1,
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.VIDEO_ONLY,
                    .StreamIndex = 2
                }
            })

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs)

            Assert.AreEqual("-i ""input_path"" -map 1:V:2 ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildExcludeAudioMap()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.SelectedStreams.Add(New MapArgument())
            ffmpegArgs.SelectedStreams.Add(New MapArgument() With {
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.AUDIO
                },
                .Exclude = True
            })

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs)

            Assert.AreEqual("-i ""input_path"" -map 0 -map -0:a ""test_path""", args)
        End Sub

        ''' <summary>
        ''' Tests that if a selector is provided to a map argument, the map arguments are formatted correctly.
        ''' </summary>
        <TestMethod>
        Public Sub TestBuildEmptyMapSelector()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.SelectedStreams.Add(New MapArgument() With {
                .Selector = New StreamSpecifier(),
                .InputFileNumber = 1
            })
            ffmpegArgs.SelectedStreams.Add(New MapArgument() With {
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.DATA
                }
            })

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs)

            Assert.AreEqual("-i ""input_path"" -map 1 -map 0:d ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildOptionalMap()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.SelectedStreams.Add(New MapArgument() With {
                .Selector = New StreamSpecifier() With {
                    .Type = StreamType.SUBTITLE,
                    .StreamIndex = 3
                },
                .IsOptional = True
            })

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs)

            Assert.AreEqual("-i ""input_path"" -map 0:s:3? ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildCodecCopyAll()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.Codecs.Add(New CopyCodecArgument(New StreamSpecifier()))

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs)

            Assert.AreEqual("-i ""input_path"" -c copy ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildCodecCopyAll_NoStreamSpecifier()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.Codecs.Add(New CopyCodecArgument())

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs)

            Assert.AreEqual("-i ""input_path"" -c copy ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildCodecLibx264Video()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.Codecs.Add(New VideoCodecArgument(New StreamSpecifier() With {
                    .Type = StreamType.VIDEO_AND_ATTACHMENTS
                }, VideoCodec.LIBX264))

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs)

            Assert.AreEqual("-i ""input_path"" -c:v libx264 ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildCodecHEVCVideoStream0()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.Codecs.Add(New VideoCodecArgument(New StreamSpecifier() With {
                    .Type = StreamType.VIDEO_AND_ATTACHMENTS,
                    .StreamIndex = 0
                }, VideoCodec.HEVC_NVENC))

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs)

            Assert.AreEqual("-i ""input_path"" -c:v:0 hevc_nvenc ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildMultipleCodecs()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.Codecs.Add(New SubtitleCodecArgument(New StreamSpecifier() With {
                    .Type = StreamType.SUBTITLE
                }, SubtitleCodec.MOV_TEXT))

            ffmpegArgs.Codecs.Add(New AudioCodecArgument(New StreamSpecifier() With {
                    .StreamIndex = 1
            }, AudioCodec.AAC))

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs)

            Assert.AreEqual("-i ""input_path"" -c:s mov_text -c:1 aac ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestMultipleInputFiles()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input1")
            ffmpegArgs.InputFiles.Add("input2")

            ffmpegArgs.Codecs.Add(New CopyCodecArgument())

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs)

            Assert.AreEqual("-i ""input1"" -i ""input2"" -c copy ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestSimpleInputOutput()
            Dim ffmpegArgs As New FfmpegArguments("output_path")
            ffmpegArgs.InputFiles.Add("C:\path\to\file")

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs)

            Assert.AreEqual("-i ""C:\path\to\file"" ""output_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestUserAgent()
            Dim Ffmpegargs As New FfmpegArguments("output_path")
            Ffmpegargs.UserAgent = "my_user_agent"
            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim commandString = commandBuilder.BuildCommandLineArguments(Ffmpegargs)

            Assert.AreEqual("-user_agent ""my_user_agent"" ""output_path""", commandString)
        End Sub

        <TestMethod>
        Public Sub TestBuildSpeedPreset()
            Dim ffmpegArgs As New FfmpegArguments("output_path") With {
                .Preset = New SpeedPresetArgument(New SpeedPreset(Speed.VERYFAST))
            }

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim command = commandBuilder.BuildCommandLineArguments(ffmpegArgs)

            Assert.AreEqual("-preset veryfast ""output_path""", command)
        End Sub

        <TestMethod>
        Public Sub TestBuildQualityPreset()
            Dim ffmpegArgs As New FfmpegArguments("output_path") With {
                .Preset = New QualityPresetArgument(New QualityPreset(3))
            }

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim command = commandBuilder.BuildCommandLineArguments(ffmpegArgs)

            Assert.AreEqual("-preset 3 ""output_path""", command)
        End Sub
    End Class
End Namespace