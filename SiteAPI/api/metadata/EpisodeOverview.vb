Namespace api.metadata
    ''' <summary>
    ''' An "overview" of an episode. Contains metadata about the episode and how to get more information about it, but no further.
    ''' Necessary because season lists don't contain all the details we need when listing all episodes in a season.
    ''' </summary>
    Public MustInherit Class EpisodeOverview
        ''' <summary>
        ''' An internal ID used to refer to the episode
        ''' </summary>
        ''' <returns></returns>
        Public Property EpisodeId As String

        ''' <summary>
        ''' A slug to add to the URL to get episode details through the API
        ''' </summary>
        ''' <returns></returns>
        Public Property ApiUrlSlug As String

        ''' <summary>
        ''' Whether the episode is available without a paid subscription.
        ''' </summary>
        ''' <returns></returns>
        Public Property IsFree As Boolean

        ''' <summary>
        ''' Episode number. Is a string because episode APIs return a string, and they may sometimes be a
        ''' string of text instead of a number.
        ''' </summary>
        ''' <returns></returns>
        Public Property EpisodeNumber As String

        Public Overrides Function ToString() As String
            Return "Episode " + EpisodeNumber
        End Function

    End Class
End Namespace