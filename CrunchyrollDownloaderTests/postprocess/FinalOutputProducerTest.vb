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

            Dim output As List(Of MediaFileEntry) = outputProducer.ProcessInputs(inputFiles, episode)

            Assert.AreEqual(1, fileSystem.MovedFiles.Count)
            Assert.AreEqual("\temporary\path\tempfile.mp4", fileSystem.MovedFiles.Item(0).OldPath)
            Assert.AreEqual("\final\output\path\Test name.mp4", fileSystem.MovedFiles.Item(0).NewPath)

            Assert.AreEqual(1, output.Count)
            Assert.AreEqual("\final\output\path\Test name.mp4", output.Item(0).Location)
        End Sub

        ''' <summary>
        ''' Tests that the output producer can rename a video and subtitle file without adding subdirectories.
        ''' </summary>
        <TestMethod>
        Public Sub TestRenameMp4AndSrt()
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

            Dim output As List(Of MediaFileEntry) = outputProducer.ProcessInputs(inputFiles, episode)

            Dim movedFiles As List(Of FakeFileSystem.ModifiedFile) = fileSystem.MovedFiles
            Assert.AreEqual(2, movedFiles.Count)
            Assert.AreEqual("\temporary\path\tempfile.mp4", movedFiles(0).OldPath)
            Assert.AreEqual("\final\output\path\Test name.mp4", movedFiles(0).NewPath)
            Assert.AreEqual("\temporary\path\subtitles.srt", movedFiles(1).OldPath)
            Assert.AreEqual("\final\output\path\Test name.srt", movedFiles(1).NewPath)

            Assert.AreEqual(2, output.Count)
            Assert.AreEqual("\final\output\path\Test name.mp4", output.Item(0).Location)
            Assert.AreEqual("\final\output\path\Test name.srt", output.Item(1).Location)
        End Sub


        <TestMethod>
        Public Sub TestRenameMp4_WithShowPath()
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


        <TestMethod>
        Public Sub TestRenameMkv_IncludeAudioLanguage()
            Dim prefs As New OutputPreferences() With {
                .NameTemplate = "AnimeTitle; AnimeDub;",
                .OutputPath = "\final\output\path",
                .UseSeasonPath = False,
                .UseShowPath = False,
                .UseIso639Codes = False
            }
            Dim fileSystem As New FakeFileSystem()
            Dim outputProducer As New FinalOutputProducer(prefs, fileSystem)

            Dim inputFiles As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\temporary\path\tempfile.mkv", MediaType.Video Or MediaType.Audio, New Locale(Language.FRENCH)),
                New MediaFileEntry("\temporary\path\subs.vtt", MediaType.Subtitles, New Locale(Language.PORTUGUESE))
            }
            Dim episode As New FakeEpisode() With {
                .ShowName = "A test show",
                .SeasonNumber = 1,
                .EpisodeName = "The episode"
            }

            outputProducer.ProcessInputs(inputFiles, episode)

            Assert.AreEqual(2, fileSystem.MovedFiles.Count)
            Assert.AreEqual("\temporary\path\tempfile.mkv", fileSystem.MovedFiles.Item(0).OldPath)
            Assert.AreEqual("\final\output\path\A test show French.mkv", fileSystem.MovedFiles.Item(0).NewPath)
            Assert.AreEqual("\temporary\path\subs.vtt", fileSystem.MovedFiles.Item(1).OldPath)
            Assert.AreEqual("\final\output\path\A test show French.vtt", fileSystem.MovedFiles.Item(1).NewPath)
        End Sub

        <TestMethod>
        Public Sub TestRenameMkv_AppendSubtitleLanguage()
            Dim prefs As New OutputPreferences() With {
                .NameTemplate = "AnimeTitle; AnimeDub;",
                .OutputPath = "\final\output\path",
                .UseSeasonPath = False,
                .UseShowPath = False,
                .UseIso639Codes = False,
                .AppendLanguageToSingleSubtitles = True
            }
            Dim fileSystem As New FakeFileSystem()
            Dim outputProducer As New FinalOutputProducer(prefs, fileSystem)

            Dim inputFiles As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\temporary\path\tempfile.mkv", MediaType.Video Or MediaType.Audio, New Locale(Language.FRENCH)),
                New MediaFileEntry("\temporary\path\subs.vtt", MediaType.Subtitles, New Locale(Language.PORTUGUESE))
            }
            Dim episode As New FakeEpisode() With {
                .ShowName = "A test show",
                .SeasonNumber = 1,
                .EpisodeName = "The episode"
            }

            outputProducer.ProcessInputs(inputFiles, episode)

            Assert.AreEqual(2, fileSystem.MovedFiles.Count)
            Assert.AreEqual("\temporary\path\tempfile.mkv", fileSystem.MovedFiles.Item(0).OldPath)
            Assert.AreEqual("\final\output\path\A test show French.mkv", fileSystem.MovedFiles.Item(0).NewPath)
            Assert.AreEqual("\temporary\path\subs.vtt", fileSystem.MovedFiles.Item(1).OldPath)
            Assert.AreEqual("\final\output\path\A test show French.Portuguese.vtt", fileSystem.MovedFiles.Item(1).NewPath)
        End Sub

        <TestMethod>
        Public Sub TestRenameMkv_DoNotAppendSubtitleLanguage()
            Dim prefs As New OutputPreferences() With {
                .NameTemplate = "AnimeTitle; AnimeDub;",
                .OutputPath = "\final\output\path",
                .UseSeasonPath = False,
                .UseShowPath = False,
                .UseIso639Codes = False,
                .AppendLanguageToSingleSubtitles = False
            }
            Dim fileSystem As New FakeFileSystem()
            Dim outputProducer As New FinalOutputProducer(prefs, fileSystem)

            Dim inputFiles As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\temporary\path\tempfile.mkv", MediaType.Video Or MediaType.Audio, New Locale(Language.FRENCH)),
                New MediaFileEntry("\temporary\path\subs.vtt", MediaType.Subtitles, New Locale(Language.PORTUGUESE))
            }
            Dim episode As New FakeEpisode() With {
                .ShowName = "A test show",
                .SeasonNumber = 1,
                .EpisodeName = "The episode"
            }

            outputProducer.ProcessInputs(inputFiles, episode)

            Assert.AreEqual(2, fileSystem.MovedFiles.Count)
            Assert.AreEqual("\temporary\path\tempfile.mkv", fileSystem.MovedFiles.Item(0).OldPath)
            Assert.AreEqual("\final\output\path\A test show French.mkv", fileSystem.MovedFiles.Item(0).NewPath)
            Assert.AreEqual("\temporary\path\subs.vtt", fileSystem.MovedFiles.Item(1).OldPath)
            Assert.AreEqual("\final\output\path\A test show French.vtt", fileSystem.MovedFiles.Item(1).NewPath)
        End Sub

        <TestMethod>
        Public Sub TestRenameMkv_AppendSubtitleLanguage_NoSubtitles()
            Dim prefs As New OutputPreferences() With {
                .NameTemplate = "AnimeTitle; AnimeDub;",
                .OutputPath = "\final\output\path",
                .UseSeasonPath = False,
                .UseShowPath = False,
                .UseIso639Codes = False,
                .AppendLanguageToSingleSubtitles = True
            }
            Dim fileSystem As New FakeFileSystem()
            Dim outputProducer As New FinalOutputProducer(prefs, fileSystem)

            Dim inputFiles As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\temporary\path\tempfile.mkv", MediaType.Video Or MediaType.Audio, New Locale(Language.FRENCH))
            }
            Dim episode As New FakeEpisode() With {
                .ShowName = "A test show",
                .SeasonNumber = 1,
                .EpisodeName = "The episode"
            }

            outputProducer.ProcessInputs(inputFiles, episode)

            Assert.AreEqual(1, fileSystem.MovedFiles.Count)
            Assert.AreEqual("\temporary\path\tempfile.mkv", fileSystem.MovedFiles.Item(0).OldPath)
            Assert.AreEqual("\final\output\path\A test show French.mkv", fileSystem.MovedFiles.Item(0).NewPath)
        End Sub

        <TestMethod>
        Public Sub TestRenameMkv_AppendSubtitleLanguage_ISO()
            Dim prefs As New OutputPreferences() With {
               .NameTemplate = "AnimeTitle; AnimeDub;",
               .OutputPath = "\final\output\path",
               .UseSeasonPath = False,
               .UseShowPath = False,
               .UseIso639Codes = True,
               .AppendLanguageToSingleSubtitles = True
           }
            Dim fileSystem As New FakeFileSystem()
            Dim outputProducer As New FinalOutputProducer(prefs, fileSystem)

            Dim inputFiles As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\temporary\path\tempfile.mkv", MediaType.Video Or MediaType.Audio, New Locale(Language.FRENCH)),
                New MediaFileEntry("\temporary\path\subs.vtt", MediaType.Subtitles, New Locale(Language.PORTUGUESE))
            }
            Dim episode As New FakeEpisode() With {
                .ShowName = "A test show",
                .SeasonNumber = 1,
                .EpisodeName = "The episode"
            }

            outputProducer.ProcessInputs(inputFiles, episode)

            Assert.AreEqual(2, fileSystem.MovedFiles.Count)
            Assert.AreEqual("\temporary\path\tempfile.mkv", fileSystem.MovedFiles.Item(0).OldPath)
            Assert.AreEqual("\final\output\path\A test show fr.mkv", fileSystem.MovedFiles.Item(0).NewPath)
            Assert.AreEqual("\temporary\path\subs.vtt", fileSystem.MovedFiles.Item(1).OldPath)
            Assert.AreEqual("\final\output\path\A test show fr.pt.vtt", fileSystem.MovedFiles.Item(1).NewPath)
        End Sub

        <TestMethod>
        Public Sub TestRenameMkv_DoNotAppendLangaugeToSingleFile_MultipleFiles()
            Dim prefs As New OutputPreferences() With {
                .NameTemplate = "AnimeTitle; AnimeDub;",
                .OutputPath = "\final\output\path",
                .UseSeasonPath = False,
                .UseShowPath = False,
                .UseIso639Codes = False,
                .AppendLanguageToSingleSubtitles = False
            }
            Dim fileSystem As New FakeFileSystem()
            Dim outputProducer As New FinalOutputProducer(prefs, fileSystem)

            Dim inputFiles As New List(Of MediaFileEntry) From {
                New MediaFileEntry("\temporary\path\tempfile.mkv", MediaType.Video Or MediaType.Audio, New Locale(Language.FRENCH)),
                New MediaFileEntry("\temporary\path\subs_p.vtt", MediaType.Subtitles, New Locale(Language.PORTUGUESE)),
                New MediaFileEntry("\temporary\path\subs_m.vtt", MediaType.Subtitles, New Locale(Language.MANDARIN))
            }
            Dim episode As New FakeEpisode() With {
                .ShowName = "A test show",
                .SeasonNumber = 1,
                .EpisodeName = "The episode"
            }

            outputProducer.ProcessInputs(inputFiles, episode)

            Assert.AreEqual(3, fileSystem.MovedFiles.Count)
            Dim file1 = fileSystem.MovedFiles.Item(0)
            Assert.AreEqual("\temporary\path\tempfile.mkv", file1.OldPath)
            Assert.AreEqual("\final\output\path\A test show French.mkv", file1.NewPath)

            Dim file2 = fileSystem.MovedFiles.Item(1)
            Assert.AreEqual("\temporary\path\subs_p.vtt", file2.OldPath)
            Assert.AreEqual("\final\output\path\A test show French.Portuguese.vtt", file2.NewPath)

            Dim file3 = fileSystem.MovedFiles.Item(2)
            Assert.AreEqual("\temporary\path\subs_m.vtt", file3.OldPath)
            Assert.AreEqual("\final\output\path\A test show French.Mandarin.vtt", file3.NewPath)
        End Sub
    End Class
End Namespace