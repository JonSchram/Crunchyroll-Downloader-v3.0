Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.preferences
Imports CrunchyrollDownloaderTests.utilities
Imports CrunchyrollDownloaderTests.utilities.ffmpeg
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports SiteAPI.api
Imports SiteAPI.api.common

Namespace download
    <TestClass>
    Public Class FfmpegDownloaderTest
        <TestMethod>
        Public Async Function TestDownloadSubtitles() As Task
            Dim prefs As New DownloadPreferences() With {
                .TemporaryDirectory = "\temporary\path"
            }

            Dim ffmpeg As New FakeFfmpegAdapter()
            Dim fakeFilesystem As New FakeFileSystem()
            Dim fakeHttpClient As New FakeHttpClient()
            Dim downloader As New FfmpegDownloader(prefs, ffmpeg, fakeFilesystem, fakeHttpClient)

            fakeHttpClient.SetStatusCode("https://www.example.com/somefile.vtt", Net.HttpStatusCode.OK)
            fakeHttpClient.SetContent("https://www.example.com/somefile.vtt", "test VTT file")

            Dim sources As New List(Of Media) From {New FileMedia(MediaType.Subtitles, New Locale(Language.JAPANESE), "https://www.example.com/somefile.vtt")}
            Dim playback As New Selection(sources)
            Dim outputFiles As MediaFileEntry() = Await downloader.DownloadSelection(playback)

            Debug.WriteLine(outputFiles)

            Assert.AreEqual("test VTT file", fakeFilesystem.StreamedContent.Item("\temporary\path\somefile.vtt"))
        End Function
    End Class
End Namespace
