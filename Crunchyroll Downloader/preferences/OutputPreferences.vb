Imports Crunchyroll_Downloader.settings.general

Namespace preferences
    Public Class OutputPreferences
        ''' <summary>
        ''' The path to save the completed file, minus the file name.
        ''' </summary>
        ''' <returns></returns>
        Public Property OutputPath As String
        Public Property NameTemplate As String

        ''' <summary>
        ''' Whether to create a directory for the season number
        ''' </summary>
        ''' <returns></returns>
        Public Property UseSeasonPath As Boolean

        ''' <summary>
        ''' Whether to create a directory for the show.
        ''' </summary>
        ''' <returns></returns>
        Public Property UseShowPath As Boolean

        ''' <summary>
        ''' A folder within OutputPath to save the output in instead of saving in a season or show path.
        ''' </summary>
        ''' <returns></returns>
        Public Property OverriddenFolder As String

        ''' <summary>
        ''' How many digits should be used in total when printing the season number in the output path.
        ''' </summary>
        ''' <returns></returns>
        Public Property SeasonDigitPadding As Integer

        ''' <summary>
        ''' How many digits should be used in total when printing the episode number in the output path.
        ''' </summary>
        ''' <returns></returns>
        Public Property EpisodeDigitPadding As Integer

        Public Property UseSeasonsInFilename As SeasonNumberBehavior

        ''' <summary>
        ''' Whether to use ISO 639-1 codes when generating filenames. If false, uses the full langauge name.
        ''' </summary>
        ''' <returns></returns>
        Public Property UseIso639Codes As Boolean

        ''' <summary>
        ''' Whether to append the language name to a subtitle file if only a single subtitle is being downloaded.
        ''' </summary>
        ''' <returns></returns>
        Public Property AppendLanguageToSingleSubtitles As Boolean
    End Class
End Namespace
