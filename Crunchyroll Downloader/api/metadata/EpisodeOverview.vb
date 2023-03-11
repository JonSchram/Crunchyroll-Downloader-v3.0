' TODO: Might want to add episode download details here too?
' Could build out details as they are retrieved from the API. The downside is that it becomes more confusing what is available at any given time.
' If this is done, might need a flag like "detailsAvailable"

''' <summary>
''' An "overview" of an episode. Contains metadata about the episode and how to get more information about it, but no further.
''' Necessary because season lists don't contain all the details we need when listing all episodes in a season.
''' </summary>
Public MustInherit Class EpisodeOverview
    ' An internal ID used to refer to the episode
    Public Property EpisodeId As String

    ' A slug to add to the URL to get the web viewer for this episode
    Public Property EpisodeUrlSlug As String

    ' A slug to add to the URL to get episode details through the API
    Public Property ApiUrlSlug As String

    Public Property IsFree As Boolean

    ' Episode number is a string in the API so keep it that way
    Public Property EpisodeNumber As String

    Public Overrides Function ToString() As String
        Return "Episode " + EpisodeNumber
    End Function

End Class
