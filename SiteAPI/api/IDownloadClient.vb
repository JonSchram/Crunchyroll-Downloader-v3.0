Imports SiteAPI.api.common
Imports SiteAPI.api.metadata

Namespace api
    Public Interface IDownloadClient
        Function Initialize() As Task

        ''' <summary>
        ''' Gets information about all seasons available in a series.
        ''' </summary>
        ''' <returns></returns>
        Function ListSeasons(Url As String) As Task(Of IEnumerable(Of SeasonOverview))

        ''' <summary>
        ''' Gets the information about all episodes in a season and how to get more information about
        ''' an individual episode.
        ''' </summary>
        ''' <param name="Season"></param>
        ''' <returns></returns>
        Function ListEpisodes(Season As SeasonOverview) As Task(Of IEnumerable(Of EpisodeOverview))

        ''' <summary>
        ''' Gets the information required to locate the episode playback
        ''' Also can be used to format the output episode
        ''' </summary>
        ''' <param name="Overview"></param>
        ''' <returns></returns>
        Function GetEpisodeInfo(Overview As EpisodeOverview) As Task(Of Episode)

        ''' <summary>
        ''' Downloads the episode info from the download URL, assumed to be a link to an episode.
        ''' </summary>
        ''' <returns></returns>
        Function GetEpisodeInfo(Url As String) As Task(Of Episode)

        ''' <summary>
        ''' Gets whether the URL corresponds to a series (not an individual season or an episode).
        ''' </summary>
        ''' <returns></returns>
        Function IsSeriesUrl(Url As String) As Boolean

        ''' <summary>
        ''' Gets whether the URL corresponds to a video (such as one episode).
        ''' </summary>
        ''' <returns></returns>
        Function IsVideoUrl(Url As String) As Boolean

        ''' <summary>
        ''' Returns a human-readable name that this downloader is associated with.
        ''' Mainly used to display the correct string in the UI.
        ''' </summary>
        ''' <returns></returns>
        Function GetSiteName() As String

        Function GetAvailableMedia(ep As Episode, preferences As DownloadPreferences) As Task(Of List(Of MediaLink))

        Function ResolveMediaLink(link As MediaLink) As Task(Of Media)

    End Interface
End Namespace