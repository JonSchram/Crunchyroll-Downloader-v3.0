Public MustInherit Class Episode
    ' The ID of the corresponding video playback info
    Public Property VideoId As String

    ' The ID that the API uses to refer to this episode
    Public Property ApiId As Integer

    ' The slug added to the URL to get to the user-facing episode player
    Public Property UrlSlug As String

    Public Property ShowName As String

    Public Property SeasonNumber As Integer

    ' Episode number in season. Needs to be a double because some episodes like filler / recap episodes are numbered as ".5"
    Public Property EpisodeNumber As Double

    Public Property ImageUrl As String

    ' Episode type. Usually "episode" but could be any descriptive string
    Public Property Type As String

    Public Property IsFree As Boolean

    Public Overrides Function ToString() As String
        Return $"{ShowName} - Season {SeasonNumber} Episode {EpisodeNumber}"
    End Function

End Class
