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

            ' TODO: Pad season and episode numbers
            Assert.AreEqual("A fake series S1E4 Fake kodi episode", result)
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

    End Class
End Namespace