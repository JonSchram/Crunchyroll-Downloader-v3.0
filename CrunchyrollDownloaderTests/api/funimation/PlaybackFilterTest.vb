Imports Crunchyroll_Downloader.api.common
Imports Crunchyroll_Downloader.api.funimation
Imports Crunchyroll_Downloader.api.metadata.video
Imports Crunchyroll_Downloader.processing
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace api.funimation
    <TestClass>
    Public Class PlaybackFilterTest

        <TestMethod>
        Public Sub TestSelectM3u8()
            Dim m3u8Playback As New Playback() With {
                .AudioLanguage = "ja",
                .FileExtension = "m3u8",
                .Version = "simulcast"
            }

            Dim mp4Playback As New Playback() With {
                .AudioLanguage = "ja",
                .FileExtension = "mp4",
                .Version = "simulcast"
            }

            Dim playbacks = New List(Of Playback) From {m3u8Playback, mp4Playback}

            Dim preferences = New DownloadPreferences() With {
                .AudioLanguage = Language.JAPANESE
            }
            Dim filter = New PlaybackFilter(preferences)

            Dim bestPlayback = filter.GetBestPlayback(playbacks)

            Assert.AreEqual(m3u8Playback, bestPlayback)
        End Sub

        <TestMethod>
        Public Sub TestSelectCorrectLanguage()
            Dim japanesePlayback As New Playback() With {
                .AudioLanguage = "ja",
                .FileExtension = "m3u8",
                .Version = "simulcast"
            }

            Dim englishPlayback As New Playback() With {
                .AudioLanguage = "en",
                .FileExtension = "m3u8",
                .Version = "simulcast"
            }

            Dim playbacks = New List(Of Playback) From {japanesePlayback, englishPlayback}

            Dim preferences = New DownloadPreferences() With {
                .AudioLanguage = Language.JAPANESE
            }
            Dim filter = New PlaybackFilter(preferences)

            Dim bestPlayback = filter.GetBestPlayback(playbacks)

            Assert.AreEqual(japanesePlayback, bestPlayback)
        End Sub

        <TestMethod>
        Public Sub GetUncutPlayback()
            Dim simulcast As New Playback() With {
                .AudioLanguage = "ja",
                .FileExtension = "m3u8",
                .Version = "simulcast"
            }

            Dim uncutPlayback As New Playback() With {
                .AudioLanguage = "ja",
                .FileExtension = "m3u8",
                .Version = "uncut"
            }

            Dim playbacks = New List(Of Playback) From {simulcast, uncutPlayback}

            Dim preferences = New DownloadPreferences() With {
                .AudioLanguage = Language.JAPANESE
            }
            Dim filter = New PlaybackFilter(preferences)

            Dim bestPlayback = filter.GetBestPlayback(playbacks)

            Assert.AreEqual(uncutPlayback, bestPlayback)
        End Sub

        <TestMethod>
        Public Sub GetNoPlayback()
            Dim spanish As New Playback() With {
                .AudioLanguage = "es",
                .FileExtension = "m3u8",
                .Version = "simulcast"
            }

            Dim english As New Playback() With {
                .AudioLanguage = "en",
                .FileExtension = "m3u8",
                .Version = "simulcast"
            }

            Dim portuguese As New Playback() With {
                .AudioLanguage = "pt",
                .FileExtension = "m3u8",
                .Version = "simulcast"
            }

            Dim playbacks = New List(Of Playback) From {spanish, english, portuguese}

            Dim preferences = New DownloadPreferences() With {
                .AudioLanguage = Language.JAPANESE
            }
            Dim filter = New PlaybackFilter(preferences)

            Dim bestPlayback = filter.GetBestPlayback(playbacks)

            Assert.IsNull(bestPlayback)
        End Sub

    End Class
End Namespace