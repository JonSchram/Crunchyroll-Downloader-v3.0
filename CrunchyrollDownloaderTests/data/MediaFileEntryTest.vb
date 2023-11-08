Imports Crunchyroll_Downloader.data
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports SiteAPI.api
Imports SiteAPI.api.common

Namespace data
    <TestClass>
    Public Class MediaFileEntryTest

        <TestMethod>
        Public Sub TestOnlyContainsSubtitles()
            Dim entry As New MediaFileEntry("\test\location", MediaType.Subtitles, New Locale(Language.ENGLISH))
            Assert.IsTrue(entry.OnlyContainsMedia(MediaType.Subtitles))
        End Sub


        <TestMethod>
        Public Sub TestOnlyContainsSubtitles_ContainsMultipleTracks()
            Dim entry As New MediaFileEntry("\test\location", MediaType.Video Or MediaType.Subtitles, New Locale(Language.ENGLISH))
            Assert.IsFalse(entry.OnlyContainsMedia(MediaType.Subtitles))
        End Sub


        <TestMethod>
        Public Sub TestOnlyContainsSubtitles_DoesNotContainSubtitles()
            Dim entry As New MediaFileEntry("\test\location", MediaType.Audio, New Locale(Language.ENGLISH))
            Assert.IsFalse(entry.OnlyContainsMedia(MediaType.Subtitles))
        End Sub
    End Class
End Namespace