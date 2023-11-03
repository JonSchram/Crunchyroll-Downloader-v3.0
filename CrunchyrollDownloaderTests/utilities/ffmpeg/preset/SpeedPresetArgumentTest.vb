Imports Crunchyroll_Downloader.utilities.ffmpeg.preset
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace utilities.ffmpeg.preset
    <TestClass>
    Public Class SpeedPresetArgumentTest
        <TestMethod>
        <DataRow(Speed.SLOW, "-preset slow")>
        <DataRow(Speed.MEDIUM, "-preset medium")>
        <DataRow(Speed.ULTRAFAST, "-preset ultrafast")>
        Public Sub TestCommandLine(s As Speed, expected As String)
            Dim preset As New SpeedPresetArgument(New SpeedPreset(s))
            Dim commandString As String = preset.BuildCommandLineArgument()

            Assert.AreEqual(expected, commandString)
        End Sub
    End Class
End Namespace