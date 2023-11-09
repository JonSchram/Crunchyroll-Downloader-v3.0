Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.settings.general
Imports Crunchyroll_Downloader.utilities

Namespace preferences
    Public Class OutputPreferenceFactory

        Public Function GetPreferences(task As DownloadTask) As OutputPreferences
            Dim settings As ProgramSettings = ProgramSettings.GetInstance()

            Dim outputDir = settings.OutputPath
            Dim template = If(settings.KodiNaming, FilenameInterpolator.CreateKodiNamingTemplate(), settings.FilenameFormat)
            Dim subfolderCreation As SubfolderBehavior = task.SubfolderCreation
            Dim manualSubfolder = If(subfolderCreation = SubfolderBehavior.OVERRIDE_FOLDER, task.OverriddenSubfolder, Nothing)
            Dim createSeasonFolder = subfolderCreation = SubfolderBehavior.USE_SERIES_AND_SEASON
            Dim createShowFolder = subfolderCreation = SubfolderBehavior.USE_SERIES_AND_SEASON Or subfolderCreation = SubfolderBehavior.USE_SERIES
            Dim numberPaddingLength As Integer = settings.ZeroPaddingLength
            Dim nameBehavior As SeasonNumberBehavior = settings.SeasonNumberNaming
            Dim subtitleLanguageNaming As LanguageNameMethod = settings.SubLanguageNaming
            Dim includeSubtitleLanguage As Boolean = settings.IncludeLangaugeIfOneSubtitleFile

            Return New OutputPreferences() With {
                .OutputPath = outputDir,
                .NameTemplate = template,
                .OverriddenFolder = manualSubfolder,
                .UseSeasonPath = createSeasonFolder,
                .UseShowPath = createShowFolder,
                .SeasonDigitPadding = numberPaddingLength,
                .EpisodeDigitPadding = numberPaddingLength,
                .UseIso639Codes = subtitleLanguageNaming = LanguageNameMethod.ISO639_2_CODES,
                .AppendLanguageToSingleSubtitles = includeSubtitleLanguage,
                .UseSeasonsInFilename = nameBehavior
            }
        End Function
    End Class
End Namespace