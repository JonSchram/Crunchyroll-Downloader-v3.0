Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.debugging
Imports Crunchyroll_Downloader.postprocess
Imports Crunchyroll_Downloader.preferences
Imports CrunchyrollDownloaderTests.utilities
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports SiteAPI.api
Imports SiteAPI.api.common

Namespace postprocess

    <TestClass>
    Public Class FinalOutputProducerTest

        ''' <summary>
        ''' Tests that the output producer can rename a file without adding any extra directories to the path.
        ''' </summary>
        <TestMethod>
        Public Sub TestRenameMp4()
            Debug.WriteLine("Running test")

            Dim prefs As New OutputPreferences() With {
                .NameTemplate = "Test name",
                .OutputPath = "\final\output\path",
                .UseSeasonPath = False,
                .UseShowPath = False
            }
            Dim fileSystem As New FakeFileSystem()
            Dim outputProducer As New FinalOutputProducer(prefs, fileSystem)

            Dim inputFiles As New List(Of MediaFileEntry) From {New MediaFileEntry("\temporary\path\tempfile.mp4", MediaType.Video, New Locale(Language.FRENCH))}
            Dim episode As New FakeEpisode() With {
                .ShowName = "A test show",
                .SeasonNumber = 1,
                .EpisodeName = "The episode"
            }

            outputProducer.ProcessInputs(inputFiles, episode)

            Assert.AreEqual(1, fileSystem.MovedFiles.Count)
            Assert.AreEqual("\temporary\path\tempfile.mp4", fileSystem.MovedFiles.Item(0).OldPath)
            Assert.AreEqual("\final\output\path\Test name.mp4", fileSystem.MovedFiles.Item(0).NewPath)
        End Sub

        ''' <summary>
        ''' Tests that the output producer can rename a video and subtitle file without adding subdirectories.
        ''' </summary>
        <TestMethod>
        Public Sub TestRenameMp4AndSrt()
            Debug.WriteLine("Running test")

            Dim prefs As New OutputPreferences() With {
                .NameTemplate = "Test name",
                .OutputPath = "\final\output\path",
                .UseSeasonPath = False,
                .UseShowPath = False
            }
            Dim fileSystem As New FakeFileSystem()
            Dim outputProducer As New FinalOutputProducer(prefs, fileSystem)

            Dim inputFiles As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\temporary\path\tempfile.mp4", MediaType.Video, New Locale(Language.NONE)),
                New MediaFileEntry("\temporary\path\subtitles.srt", MediaType.Subtitles, New Locale(Language.GERMAN))
            }
            Dim episode As New FakeEpisode() With {
                .ShowName = "A test show",
                .SeasonNumber = 1,
                .EpisodeName = "The episode"
            }

            outputProducer.ProcessInputs(inputFiles, episode)

            Dim movedFiles As List(Of FakeFileSystem.ModifiedFile) = fileSystem.MovedFiles
            Assert.AreEqual(2, movedFiles.Count)
            Assert.AreEqual("\temporary\path\tempfile.mp4", movedFiles(0).OldPath)
            Assert.AreEqual("\final\output\path\Test name.mp4", movedFiles(0).NewPath)
            Assert.AreEqual("\temporary\path\subtitles.srt", movedFiles(1).OldPath)
            Assert.AreEqual("\final\output\path\Test name.srt", movedFiles(1).NewPath)
        End Sub


        <TestMethod>
        Public Sub TestRenameMp4_WithShowPath()
            Debug.WriteLine("Running test")

            Dim prefs As New OutputPreferences() With {
                .NameTemplate = "Test name",
                .OutputPath = "\final\output\path",
                .UseSeasonPath = False,
                .UseShowPath = True
            }
            Dim fileSystem As New FakeFileSystem()
            Dim outputProducer As New FinalOutputProducer(prefs, fileSystem)

            Dim inputFiles As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\temporary\path\tempfile.mp4", MediaType.Video, New Locale(Language.ITALIAN))
            }
            Dim episode As New FakeEpisode() With {
                .ShowName = "A test show",
                .SeasonNumber = 1,
                .EpisodeName = "The episode"
            }

            outputProducer.ProcessInputs(inputFiles, episode)

            Assert.AreEqual(1, fileSystem.MovedFiles.Count)
            Assert.AreEqual("\temporary\path\tempfile.mp4", fileSystem.MovedFiles.Item(0).OldPath)
            Assert.AreEqual("\final\output\path\A test show\Test name.mp4", fileSystem.MovedFiles.Item(0).NewPath)
        End Sub

        <TestMethod>
        Public Sub TestRenameMp4_WithShowAndSeasonPath()
            Debug.WriteLine("Running test")

            Dim prefs As New OutputPreferences() With {
                .NameTemplate = "Test name",
                .OutputPath = "\final\output\path",
                .UseSeasonPath = True,
                .UseShowPath = True,
                .SeasonDigitPadding = 3
            }
            Dim fileSystem As New FakeFileSystem()
            Dim outputProducer As New FinalOutputProducer(prefs, fileSystem)

            Dim inputFiles As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\temporary\path\tempfile.mp4", MediaType.Video, New Locale(Language.JAPANESE))
            }
            Dim episode As New FakeEpisode() With {
                .ShowName = "A test show",
                .SeasonNumber = 1,
                .EpisodeName = "The episode"
            }

            outputProducer.ProcessInputs(inputFiles, episode)

            Assert.AreEqual(1, fileSystem.MovedFiles.Count)
            Assert.AreEqual("\temporary\path\tempfile.mp4", fileSystem.MovedFiles.Item(0).OldPath)
            Assert.AreEqual("\final\output\path\A test show\Season 001\Test name.mp4", fileSystem.MovedFiles.Item(0).NewPath)
        End Sub

        <TestMethod>
        Public Sub TestRenameMp4_OutputFileExists()
            Debug.WriteLine("Running test")

            Dim prefs As New OutputPreferences() With {
                .NameTemplate = "Test name",
                .OutputPath = "\final\output\path",
                .UseSeasonPath = False,
                .UseShowPath = False,
                .SeasonDigitPadding = 3
            }
            Dim fileSystem As New FakeFileSystem()
            fileSystem.AddFile("\final\output\path\Test name.mp4")
            Dim outputProducer As New FinalOutputProducer(prefs, fileSystem)

            Dim inputFiles As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\temporary\path\tempfile.mp4", MediaType.Video, New Locale(Language.MANDARIN))
            }
            Dim episode As New FakeEpisode() With {
                .ShowName = "A test show",
                .SeasonNumber = 1,
                .EpisodeName = "The episode"
            }

            outputProducer.ProcessInputs(inputFiles, episode)

            Assert.AreEqual(1, fileSystem.MovedFiles.Count)
            Assert.AreEqual("\temporary\path\tempfile.mp4", fileSystem.MovedFiles.Item(0).OldPath)
            Assert.AreEqual("\final\output\path\Test name (1).mp4", fileSystem.MovedFiles.Item(0).NewPath)
        End Sub

    End Class
End Namespace