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
        ''' How many digits should be used in total when printing the season number in the output path.
        ''' </summary>
        ''' <returns></returns>
        Public Property SeasonDigitPadding As Integer

        ''' <summary>
        ''' How many digits should be used in total when printing the episode number in the output path.
        ''' </summary>
        ''' <returns></returns>
        Public Property EpisodeDigitPadding As Integer

        ' TODO: Add preference for appending language to subtitle.
    End Class
End Namespace
