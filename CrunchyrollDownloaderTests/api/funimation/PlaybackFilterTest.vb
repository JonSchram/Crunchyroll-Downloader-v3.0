Imports Crunchyroll_Downloader.api
Imports Crunchyroll_Downloader.api.common
Imports Crunchyroll_Downloader.api.funimation
Imports Crunchyroll_Downloader.api.metadata.video
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

            Dim preferences = New DownloadPreferences(New Locale(Language.JAPANESE, Region.JAPAN), New List(Of Locale), MediaType.Video)
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

            Dim preferences = New DownloadPreferences(New Locale(Language.JAPANESE, Region.JAPAN), New List(Of Locale), MediaType.Video)
            Dim filter = New PlaybackFilter(preferences)

            Dim bestPlayback = filter.GetBestPlayback(playbacks)

            Assert.AreEqual(japanesePlayback, bestPlayback)
        End Sub

        <TestMethod>
        Public Sub TestSelectNoPreferredLanguage()
            Dim japanesePlayback As New Playback() With {
                .AudioLanguage = "ja",
                .FileExtension = "m3u8",
                .Version = "simulcast"
            }

            Dim englishPlayback As New Playback() With {
                .AudioLanguage = "en",
                .FileExtension = "mp4",
                .Version = "simulcast"
            }

            Dim playbacks = New List(Of Playback) From {japanesePlayback, englishPlayback}

            Dim preferences = New DownloadPreferences(New Locale(Language.NONE, Region.NOT_SPECIFIED), New List(Of Locale), MediaType.Video)
            Dim filter = New PlaybackFilter(preferences)

            Dim bestPlayback = filter.GetBestPlayback(playbacks)

            ' If no dub preference is set, choose an arbitrary m3u8.
            Assert.AreEqual(japanesePlayback, bestPlayback)
        End Sub

        <TestMethod>
        Public Sub TestGetUncutPlayback_EnglishUS()
            Dim simulcast As New Playback() With {
                .AudioLanguage = "en",
                .FileExtension = "m3u8",
                .Version = "simulcast"
            }

            Dim uncutPlayback As New Playback() With {
                .AudioLanguage = "en",
                .FileExtension = "m3u8",
                .Version = "uncut"
            }

            Dim playbacks = New List(Of Playback) From {simulcast, uncutPlayback}

            Dim preferences = New DownloadPreferences(New Locale(Language.ENGLISH, Region.UNITED_STATES), New List(Of Locale), MediaType.Video)
            Dim filter = New PlaybackFilter(preferences)

            Dim bestPlayback = filter.GetBestPlayback(playbacks)

            Assert.AreEqual(uncutPlayback, bestPlayback)
        End Sub

        <TestMethod>
        Public Sub TestGetUncutPlayback_NoRegion()
            Dim simulcast As New Playback() With {
                .AudioLanguage = "en",
                .FileExtension = "m3u8",
                .Version = "simulcast"
            }

            Dim uncutPlayback As New Playback() With {
                .AudioLanguage = "en",
                .FileExtension = "m3u8",
                .Version = "uncut"
            }

            Dim playbacks = New List(Of Playback) From {simulcast, uncutPlayback}

            Dim preferences = New DownloadPreferences(New Locale(Language.ENGLISH, Region.NOT_SPECIFIED), New List(Of Locale), MediaType.Video)
            Dim filter = New PlaybackFilter(preferences)

            Dim bestPlayback = filter.GetBestPlayback(playbacks)

            Assert.AreEqual(uncutPlayback, bestPlayback)
        End Sub

        <TestMethod>
        Public Sub TestGetNoPlayback()
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

            Dim preferences = New DownloadPreferences(New Locale(Language.JAPANESE, Region.JAPAN), New List(Of Locale), MediaType.Video)
            Dim filter = New PlaybackFilter(preferences)

            Dim bestPlayback = filter.GetBestPlayback(playbacks)

            Assert.IsNull(bestPlayback)
        End Sub

        <TestMethod>
        Public Sub TestGetVttSubtitles()
            Dim englishVtt = New Subtitle() With {
                .Language = "en",
                .Format = "vtt",
                .Path = "english.vtt"
            }
            Dim spanishVtt = New Subtitle() With {
                .Language = "es",
                .Format = "vtt",
                .Path = "spanish.vtt"
            }

            Dim englishSrt = New Subtitle() With {
                .Language = "en",
                .Format = "srt",
                .Path = "english.srt"
            }
            Dim spanishSrt = New Subtitle() With {
                .Language = "es",
                .Format = "srt",
                .Path = "spanish.srt"
            }

            Dim p As New Playback() With {
                .AudioLanguage = "ja",
                .Subtitles = New List(Of Subtitle) From {englishVtt, spanishVtt, englishSrt, spanishSrt},
                .PlaylistPath = "test",
                .FileExtension = "m3u8"
            }

            Dim subtitleLanguages = New List(Of Locale) From {New Locale(Language.ENGLISH), New Locale(Language.SPANISH)}
            Dim subtitleFormats = New List(Of SubtitleFormat) From {SubtitleFormat.VTT}
            Dim preferences = New DownloadPreferences(New Locale(Language.NONE, Region.NOT_SPECIFIED),
                                                      subtitleLanguages, MediaType.Subtitles, subtitleFormats)
            Dim filter = New PlaybackFilter(preferences)

            Dim streams As List(Of MediaLink) = filter.GetMatchingMedia(p)
            Assert.AreEqual(2, streams.Count)
            Assert.IsTrue(TypeOf streams(0) Is FileMediaLink)
            Assert.IsTrue(TypeOf streams(1) Is FileMediaLink)

            Assert.IsTrue(streams(0).Location.EndsWith("vtt"))
            Assert.IsTrue(streams(1).Location.EndsWith("vtt"))
        End Sub

        <TestMethod>
        Public Sub TestGetEnglishSubtitles()
            Dim englishVtt = New Subtitle() With {
                .Language = "en",
                .Format = "vtt",
                .Path = "english.vtt"
            }
            Dim spanishVtt = New Subtitle() With {
                .Language = "es",
                .Format = "vtt",
                .Path = "spanish.vtt"
            }

            Dim englishSrt = New Subtitle() With {
                .Language = "en",
                .Format = "srt",
                .Path = "english.srt"
            }
            Dim spanishSrt = New Subtitle() With {
                .Language = "es",
                .Format = "srt",
                .Path = "spanish.srt"
            }

            Dim p As New Playback() With {
                .AudioLanguage = "ja",
                .Subtitles = New List(Of Subtitle) From {englishVtt, spanishVtt, englishSrt, spanishSrt},
                .PlaylistPath = "test",
                .FileExtension = "m3u8"
            }

            Dim subtitleLanguages = New List(Of Locale) From {New Locale(Language.ENGLISH)}
            Dim subtitleFormats = New List(Of SubtitleFormat) From {SubtitleFormat.VTT, SubtitleFormat.SRT}
            Dim preferences = New DownloadPreferences(New Locale(Language.JAPANESE, Region.JAPAN), subtitleLanguages,
                                                      MediaType.Subtitles, subtitleFormats)
            Dim filter = New PlaybackFilter(preferences)

            Dim streams As List(Of MediaLink) = filter.GetMatchingMedia(p)
            Assert.AreEqual(2, streams.Count)
            Assert.IsTrue(TypeOf streams(0) Is FileMediaLink)
            Assert.IsTrue(TypeOf streams(1) Is FileMediaLink)

            Assert.AreEqual(New Locale(Language.ENGLISH), streams(0).MediaLocale)
            Assert.AreEqual(MediaType.Subtitles, streams(0).Type)

            Assert.AreEqual(New Locale(Language.ENGLISH), streams(1).MediaLocale)
            Assert.AreEqual(MediaType.Subtitles, streams(1).Type)
        End Sub

        <TestMethod>
        Public Sub TestGetVideoPlayback()
            Dim englishVtt = New Subtitle() With {
                .Language = "en",
                .Format = "vtt",
                .Path = "english.vtt"
            }

            Dim p As New Playback() With {
                .AudioLanguage = "ja",
                .Subtitles = New List(Of Subtitle) From {englishVtt},
                .PlaylistPath = "m3u8_location",
                .FileExtension = "m3u8"
            }

            Dim preferences = New DownloadPreferences(New Locale(Language.JAPANESE, Region.JAPAN),
                                                      New List(Of Locale), MediaType.Video, New List(Of SubtitleFormat))
            Dim filter = New PlaybackFilter(preferences)

            Dim streams = filter.GetMatchingMedia(p)

            Assert.AreEqual(1, streams.Count)
            Assert.IsTrue(TypeOf streams(0) Is HlsMasterPlaylistLink)

            Dim playlistStream = CType(streams(0), HlsMasterPlaylistLink)
            Assert.AreEqual(New Locale(Language.JAPANESE), playlistStream.MediaLocale)
            Assert.AreEqual("m3u8_location", playlistStream.Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video, playlistStream.Type)
        End Sub

        <TestMethod>
        Public Sub TestGetVideoAndSubtitles()
            Dim englishVtt = New Subtitle() With {
                .Language = "en",
                .Format = "vtt",
                .Path = "english.vtt"
            }

            Dim p As New Playback() With {
                .AudioLanguage = "ja",
                .Subtitles = New List(Of Subtitle) From {englishVtt},
                .PlaylistPath = "m3u8_location",
                .FileExtension = "m3u8"
            }

            Dim preferences = New DownloadPreferences(
                New Locale(Language.JAPANESE, Region.JAPAN), New List(Of Locale) From {New Locale(Language.ENGLISH)},
                MediaType.Video Or MediaType.Audio Or MediaType.Subtitles,
                New List(Of SubtitleFormat) From {SubtitleFormat.VTT})
            Dim filter = New PlaybackFilter(preferences)

            Dim streams = filter.GetMatchingMedia(p)

            Assert.AreEqual(2, streams.Count)
            Assert.IsTrue(TypeOf streams(0) Is HlsMasterPlaylistLink)

            Dim playlistStream = CType(streams(0), HlsMasterPlaylistLink)
            Assert.AreEqual(New Locale(Language.JAPANESE), playlistStream.MediaLocale)
            Assert.AreEqual("m3u8_location", playlistStream.Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video, playlistStream.Type)

            Assert.IsTrue(TypeOf streams(1) Is FileMediaLink)
            Dim subtitleStream = CType(streams(1), FileMediaLink)
            Assert.AreEqual(New Locale(Language.ENGLISH), subtitleStream.MediaLocale)
            Assert.AreEqual("english.vtt", subtitleStream.Location)
            Assert.AreEqual(MediaType.Subtitles, subtitleStream.Type)
        End Sub

        <TestMethod>
        Public Sub TestGetMp4Fallback()
            Dim p As New Playback() With {
                .AudioLanguage = "ja",
                .Subtitles = New List(Of Subtitle),
                .PlaylistPath = "mp4_location",
                .FileExtension = "mp4"
            }

            Dim preferences = New DownloadPreferences(New Locale(Language.JAPANESE, Region.JAPAN),
                                                      New List(Of Locale), MediaType.Video, New List(Of SubtitleFormat))
            Dim filter = New PlaybackFilter(preferences)

            Dim streams = filter.GetMatchingMedia(p)

            Assert.AreEqual(1, streams.Count)
            Assert.IsTrue(TypeOf streams(0) Is FileMediaLink)

            Dim mp4Stream = CType(streams(0), FileMediaLink)
            Assert.AreEqual(New Locale(Language.JAPANESE), mp4Stream.MediaLocale)
            Assert.AreEqual("mp4_location", mp4Stream.Location)
            Assert.AreEqual(MediaType.Audio Or MediaType.Video, mp4Stream.Type)
        End Sub
    End Class
End Namespace