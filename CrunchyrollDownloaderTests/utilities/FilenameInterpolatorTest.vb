Imports Crunchyroll_Downloader.debugging
Imports Crunchyroll_Downloader.utilities
Imports Microsoft.VisualStudio.TestTools.UnitTesting

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

            Dim result = interpolator.CreateName(ep, False)

            Assert.AreEqual("A fake series S01E04 Fake kodi episode", result)
        End Sub

        <TestMethod>
        Public Sub TestEpisodeName()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "A fake episode",
                .SeasonNumber = 1
            }
            Dim interpolator As New FilenameInterpolator("My file EpisodeName;")

            Dim result = interpolator.CreateName(ep, False)

            Assert.AreEqual("My file A fake episode", result)
        End Sub

        <TestMethod>
        Public Sub TestCreateName_PadSeasonNumber()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "A fake episode",
                .SeasonNumber = 1,
                .EpisodeNumber = 2
            }
            Dim interpolator As New FilenameInterpolator("Test file SSeason;EEpisodeNR; - EpisodeName;", 2, 1)

            Dim result = interpolator.CreateName(ep, False)

            Assert.AreEqual("Test file S01E2 - A fake episode", result)
        End Sub

        <TestMethod>
        Public Sub TestCreateName_PadEpisodeNumber()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "A fake episode",
                .SeasonNumber = 1,
                .EpisodeNumber = 2
            }
            Dim interpolator As New FilenameInterpolator("Test file SSeason;EEpisodeNR; - EpisodeName;", 1, 2)

            Dim result = interpolator.CreateName(ep, False)

            Assert.AreEqual("Test file S1E02 - A fake episode", result)
        End Sub

        <TestMethod>
        Public Sub TestCreateName_PadFractionalEpisodeNumber()
            Dim ep As New FakeEpisode() With {
                .EpisodeName = "A fake episode",
                .SeasonNumber = 1,
                .EpisodeNumber = 6.5
            }
            Dim interpolator As New FilenameInterpolator("Episode EpisodeNR;", 1, 2)

            Dim result = interpolator.CreateName(ep, False)

            Assert.AreEqual("Episode 06.5", result)
        End Sub
    End Class
End Namespace