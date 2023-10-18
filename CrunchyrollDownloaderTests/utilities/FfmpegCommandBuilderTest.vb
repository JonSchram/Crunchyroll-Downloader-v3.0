Imports Crunchyroll_Downloader.utilities
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace utilities
    <TestClass>
    Public Class FfmpegCommandBuilderTest
        <TestMethod>
        Public Sub TestBuildCookies()
            Dim cookies As New Dictionary(Of String, String) From {
                {"Cookie1", "value1"},
                {"Cookie2", "value2"}
            }
            Dim commandBuilder As New FfmpegCommandBuilder()

            Dim args = commandBuilder.BuildCommandLineArguments(New FfmpegArguments("test_path"), cookies, Nothing)

            Assert.AreEqual($"-headers ""Cookie: Cookie1=value1; Cookie2=value2{vbCrLf}"" ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildProgramMap()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.SelectedStreams.Add(New FfmpegArguments.MapArgument() With {
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .ProgramNumber = 4
                }
            })

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs, Nothing, Nothing)

            Assert.AreEqual("-i ""input_path"" -map 0:p:4 ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildVideoMap()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.SelectedStreams.Add(New FfmpegArguments.MapArgument() With {
                .InputFileNumber = 1,
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.VIDEO_ONLY,
                    .StreamIndex = 2
                }
            })

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs, Nothing, Nothing)

            Assert.AreEqual("-i ""input_path"" -map 1:V:2 ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildExcludeAudioMap()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.SelectedStreams.Add(New FfmpegArguments.MapArgument())
            ffmpegArgs.SelectedStreams.Add(New FfmpegArguments.MapArgument() With {
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.AUDIO
                },
                .Exclude = True
            })

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs, Nothing, Nothing)

            Assert.AreEqual("-i ""input_path"" -map 0 -map -0:a ""test_path""", args)
        End Sub

        ''' <summary>
        ''' Tests that if a selector is provided to a map argument, the map arguments are formatted correctly.
        ''' </summary>
        <TestMethod>
        Public Sub TestBuildEmptyMapSelector()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.SelectedStreams.Add(New FfmpegArguments.MapArgument() With {
                .Selector = New FfmpegArguments.StreamSpecifier(),
                .InputFileNumber = 1
            })
            ffmpegArgs.SelectedStreams.Add(New FfmpegArguments.MapArgument() With {
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.DATA
                }
            })

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs, Nothing, Nothing)

            Assert.AreEqual("-i ""input_path"" -map 1 -map 0:d ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildOptionalMap()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.SelectedStreams.Add(New FfmpegArguments.MapArgument() With {
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.SUBTITLE,
                    .StreamIndex = 3
                },
                .IsOptional = True
            })

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs, Nothing, Nothing)

            Assert.AreEqual("-i ""input_path"" -map 0:s:3? ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildCodecCopy()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.Codecs.Add(New FfmpegArguments.CodecArgument() With {
                .Name = FfmpegArguments.CodecName.COPY
            })

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs, Nothing, Nothing)

            Assert.AreEqual("-i ""input_path"" -c copy ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildCodecLibx264Video()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.Codecs.Add(New FfmpegArguments.CodecArgument() With {
                .Name = FfmpegArguments.CodecName.VIDEO_LIBX264,
                .AppliedStream = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.VIDEO_AND_ATTACHMENTS
                }
            })

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs, Nothing, Nothing)

            Assert.AreEqual("-i ""input_path"" -c:v libx264 ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildMultipleCodecs()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input_path")
            ffmpegArgs.Codecs.Add(New FfmpegArguments.CodecArgument() With {
                .Name = FfmpegArguments.CodecName.SUBTITLE_MOV_TEXT,
                .AppliedStream = New FfmpegArguments.StreamSpecifier() With {
                    .Type = FfmpegArguments.StreamType.SUBTITLE
                }
            })
            ffmpegArgs.Codecs.Add(New FfmpegArguments.CodecArgument() With {
                .Name = FfmpegArguments.CodecName.AUDIO_AAC,
                .AppliedStream = New FfmpegArguments.StreamSpecifier() With {
                    .StreamIndex = 1
                }
            })

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs, Nothing, Nothing)

            Assert.AreEqual("-i ""input_path"" -c:s mov_text -c:1 aac ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestMultipleInputFiles()
            Dim ffmpegArgs As New FfmpegArguments("test_path")
            ffmpegArgs.InputFiles.Add("input1")
            ffmpegArgs.InputFiles.Add("input2")

            ffmpegArgs.Codecs.Add(New FfmpegArguments.CodecArgument() With {
                .Name = FfmpegArguments.CodecName.COPY
            })

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs, Nothing, Nothing)

            Assert.AreEqual("-i ""input1"" -i ""input2"" -c copy ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestSimpleInputOutput()
            Dim ffmpegArgs As New FfmpegArguments("output_path")
            ffmpegArgs.InputFiles.Add("C:\path\to\file")

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs, Nothing, Nothing)

            Assert.AreEqual("-i ""C:\path\to\file"" ""output_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestUserAgent()
            Dim Ffmpegargs As New FfmpegArguments("output_path")
            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim commandString = commandBuilder.BuildCommandLineArguments(Ffmpegargs, Nothing, "my_user_agent")

            Assert.AreEqual("-user_agent ""my_user_agent"" ""output_path""", commandString)
        End Sub
    End Class
End Namespace