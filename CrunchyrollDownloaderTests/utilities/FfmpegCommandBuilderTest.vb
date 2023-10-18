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

            Assert.AreEqual($" -headers ""Cookie: Cookie1=value1; Cookie2=value2{vbCrLf}"" ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestBuildProgramMap()
            Dim cookies As New Dictionary(Of String, String)
            Dim ffmpegArgs As New FfmpegArguments("test_path") With {
                .PlaylistLocation = "input_path",
                .ProgramNumber = 4
            }

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs, cookies, Nothing)

            Assert.AreEqual(" -i ""input_path"" -map 0:p:4 ""test_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestSimpleInputOutput()
            Dim cookies As New Dictionary(Of String, String)
            Dim ffmpegArgs As New FfmpegArguments("output_path") With {
                .PlaylistLocation = "C:\path\to\file"
            }

            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim args = commandBuilder.BuildCommandLineArguments(ffmpegArgs, cookies, Nothing)

            Assert.AreEqual(" -i ""C:\path\to\file"" ""output_path""", args)
        End Sub

        <TestMethod>
        Public Sub TestUserAgent()
            Dim cookies As New Dictionary(Of String, String)
            Dim Ffmpegargs As New FfmpegArguments("output_path")
            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim commandString = commandBuilder.BuildCommandLineArguments(Ffmpegargs, cookies, "my_user_agent")

            Assert.AreEqual(" -user_agent ""my_user_agent"" ""output_path""", commandString)
        End Sub
    End Class
End Namespace