Imports Crunchyroll_Downloader.utilities.ffmpeg.preset
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace utilities.ffmpeg.preset
    <TestClass>
    Public Class QualityPresetArgumentTest
        <TestMethod>
        <DataRow(0, "-preset 0")>
        <DataRow(1, "-preset 1")>
        <DataRow(2, "-preset 2")>
        <DataRow(8, "-preset 8")>
        Public Sub TestCommandLine(quality As Integer, expected As String)
            Dim preset As New QualityPresetArgument(New QualityPreset(quality))
            Dim commandString As String = preset.BuildCommandLineArgument()

            Assert.AreEqual(expected, commandString)
        End Sub

    End Class
End Namespace