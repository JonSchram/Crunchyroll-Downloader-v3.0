Imports Crunchyroll_Downloader.utilities.ffmpeg.preset
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace utilities.ffmpeg.preset

    ''' <summary>
    ''' Tests that a quality preset cannot be created with an invalid value.
    ''' </summary>
    <TestClass>
    Public Class QualityPresetTest
        <TestMethod>
        <DataRow(-1)>
        <DataRow(9)>
        Public Sub TestInvalidQuality(value As Integer)
            Assert.ThrowsException(Of ArgumentException)(Sub()
                                                             Dim preset As New QualityPreset(value)
                                                         End Sub)
        End Sub
    End Class
End Namespace