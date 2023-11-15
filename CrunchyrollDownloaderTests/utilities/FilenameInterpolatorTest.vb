Imports System.Xml
Imports Crunchyroll_Downloader.debugging
Imports Crunchyroll_Downloader.settings.general
Imports Crunchyroll_Downloader.utilities
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports SiteAPI.api
Imports SiteAPI.api.common

Namespace utilities
    <TestClass>
    Public Class FilenameInterpolatorTest
        <TestMethod>
        Public Sub TestKodiNaming()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "Fake kodi episode",
                .SeasonNumber = 1,
                .EpisodeNumber = 4,
                .ShowName = "A fake series"
            }

            Dim interpolator As FilenameInterpolator = FilenameInterpolator.CreateKodiNamingInstance()

            Dim result = interpolator.CreateName(ep, Nothing)

            Assert.AreEqual("A fake series S01E04 Fake kodi episode", result)
        End Sub

        <TestMethod>
        Public Sub TestName_EmptyBraces()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "Fake kodi episode",
                .SeasonNumber = 1,
                .EpisodeNumber = 4,
                .ShowName = "A fake series"
            }

            Dim interpolator As FilenameInterpolator = New FilenameInterpolator("The template {} suffix")

            Dim result = interpolator.CreateName(ep, Nothing)

            Assert.AreEqual("The template {} suffix", result)
        End Sub

        <TestMethod>
        Public Sub TestName_InvalidField()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "Fake episode",
                .SeasonNumber = 1,
                .EpisodeNumber = 4,
                .ShowName = "A fake series"
            }

            Dim interpolator As FilenameInterpolator = New FilenameInterpolator("{invalid}")

            Dim result = interpolator.CreateName(ep, Nothing)

            Assert.AreEqual("{invalid}", result)
        End Sub


        <TestMethod>
        Public Sub TestEpisodeName()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "A fake episode",
                .SeasonNumber = 1,
                .ShowName = "My file"
            }
            Dim interpolator As New FilenameInterpolator("{SeriesName} {EpisodeName}")

            Dim result = interpolator.CreateName(ep, Nothing)

            Assert.AreEqual("My file A fake episode", result)
        End Sub

        <TestMethod>
        Public Sub TestCreateName_PadSeasonNumber()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "A fake episode",
                .SeasonNumber = 1,
                .EpisodeNumber = 2
            }
            Dim interpolator As New FilenameInterpolator(
                "Test file {S:SeasonNumber}{E:EpisodeNumber} - {EpisodeName}", 2, 1, False, SeasonNumberBehavior.USE_SEASON_NUMBERS)

            Dim result = interpolator.CreateName(ep, Nothing)

            Assert.AreEqual("Test file S01E2 - A fake episode", result)
        End Sub

        <TestMethod>
        Public Sub TestCreateName_PadEpisodeNumber()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "A fake episode",
                .SeasonNumber = 1,
                .EpisodeNumber = 2
            }
            Dim interpolator As New FilenameInterpolator(
                "Test file {S:SeasonNumber}{E:EpisodeNumber} - {EpisodeName}", 1, 2, False, SeasonNumberBehavior.USE_SEASON_NUMBERS)

            Dim result = interpolator.CreateName(ep, Nothing)

            Assert.AreEqual("Test file S1E02 - A fake episode", result)
        End Sub

        <TestMethod>
        Public Sub TestCreateName_PadFractionalEpisodeNumber()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "A fake episode",
                .SeasonNumber = 1,
                .EpisodeNumber = 6.5
            }
            Dim interpolator As New FilenameInterpolator("Episode {EpisodeNumber}", 1, 2, False, SeasonNumberBehavior.USE_SEASON_NUMBERS)

            Dim result = interpolator.CreateName(ep, Nothing)

            Assert.AreEqual("Episode 06.5", result)
        End Sub

        <TestMethod>
        Public Sub TestCreateName_HumanReadableLanguageName()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "A fake episode",
                .SeasonNumber = 1,
                .EpisodeNumber = 2
            }
            Dim interpolator As New FilenameInterpolator(
                "Test file S{SeasonNumber}E{EpisodeNumber} - {EpisodeName} - {AudioLanguage}", 1, 2, False, SeasonNumberBehavior.USE_SEASON_NUMBERS)

            Dim result = interpolator.CreateName(ep, New Locale(Language.ENGLISH, Region.UNITED_STATES))

            Assert.AreEqual("Test file S1E02 - A fake episode - English (United States)", result)
        End Sub

        <TestMethod>
        Public Sub TestCreateName_IsoLanguageName()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "A fake episode",
                .SeasonNumber = 1,
                .EpisodeNumber = 2
            }
            Dim interpolator As New FilenameInterpolator(
                "Test file S{SeasonNumber}E{EpisodeNumber} - {EpisodeName} - {AudioLanguage}", 1, 2, True, SeasonNumberBehavior.USE_SEASON_NUMBERS)

            Dim result = interpolator.CreateName(ep, New Locale(Language.ENGLISH, Region.UNITED_STATES))

            Assert.AreEqual("Test file S1E02 - A fake episode - en-US", result)
        End Sub

        <TestMethod>
        Public Sub TestCreateName_HumanReadable_NoRegion()
            Dim ep As New FakeEpisode() With {
               .EpisodeName = "A fake episode",
               .SeasonNumber = 1,
               .EpisodeNumber = 2
           }
            Dim interpolator As New FilenameInterpolator(
                "Test file S{SeasonNumber}E{EpisodeNumber} - {EpisodeName} - {AudioLanguage}", 1, 2, False, SeasonNumberBehavior.USE_SEASON_NUMBERS)

            Dim result = interpolator.CreateName(ep, New Locale(Language.GERMAN))

            Assert.AreEqual("Test file S1E02 - A fake episode - German", result)
        End Sub

        <TestMethod>
        Public Sub TestSeason1_IgnoreSeason1()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "A fake episode",
                .SeasonNumber = 1
            }
            Dim interpolator As New FilenameInterpolator("My file {Prefix:SeasonNumber}", 1, 1, True, SeasonNumberBehavior.IGNORE_SEASON_1)

            Dim result = interpolator.CreateName(ep, Nothing)

            Assert.AreEqual("My file ", result)
        End Sub

        <TestMethod>
        Public Sub TestSeason2_IgnoreSeason1()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "A fake episode",
                .SeasonNumber = 2
            }
            Dim interpolator As New FilenameInterpolator("My file {Prefix:SeasonNumber}", 1, 1, True, SeasonNumberBehavior.IGNORE_SEASON_1)

            Dim result = interpolator.CreateName(ep, Nothing)

            Assert.AreEqual("My file Prefix2", result)
        End Sub

        <TestMethod>
        Public Sub TestSeason2_IgnoreAllSeasons()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "A fake episode",
                .SeasonNumber = 2
            }
            Dim interpolator As New FilenameInterpolator("My file {Prefix:SeasonNumber}", 1, 1, True, SeasonNumberBehavior.IGNORE_ALL_SEASON_NUMBERS)

            Dim result = interpolator.CreateName(ep, Nothing)

            Assert.AreEqual("My file ", result)
        End Sub

    End Class
End Namespace