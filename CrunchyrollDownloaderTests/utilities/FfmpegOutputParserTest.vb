Imports Crunchyroll_Downloader.utilities
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace utilities
    <TestClass>
    Public Class FfmpegOutputParserTest
        <TestMethod>
        Public Sub TestHandleFfmepgOutput_Duration()
            Dim outputLine = "  Duration: 00:22:28.12, start: 0.000000, bitrate: 9001 kb/s"

            Dim outputParser As New FfmpegOutputParser()
            outputParser.HandleFfmpegOutput(outputLine)

            Dim expectedDuration As Double = 22 + 28.12 / 60
            Assert.AreEqual(expectedDuration, outputParser.GetDuration(), 0.01)
        End Sub
    End Class
End Namespace