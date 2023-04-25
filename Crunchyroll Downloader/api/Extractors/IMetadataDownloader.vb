Public Interface IMetadataDownloader
    ''' <summary>
    ''' Gets information about all seasons available in a series.
    ''' </summary>
    ''' <returns></returns>
    Function ListSeasons(Url As String) As IEnumerable(Of SeasonOverview)

    ''' <summary>
    ''' Gets the information about all episodes in a season and how to get more information about
    ''' an individual episode.
    ''' </summary>
    ''' <param name="Season"></param>
    ''' <returns></returns>
    Function ListEpisodes(Season As SeasonOverview) As IEnumerable(Of EpisodeOverview)

    ''' <summary>
    ''' Gets the information required to locate the episode playback
    ''' Also can be used to format the output episode
    ''' </summary>
    ''' <param name="Overview"></param>
    ''' <returns></returns>
    Function GetEpisodeInfo(Overview As EpisodeOverview) As Episode

    ''' <summary>
    ''' Downloads the episode info from the download URL, assumed to be a link to an episode.
    ''' </summary>
    ''' <returns></returns>
    Function GetEpisodeInfo(Url As String) As Episode

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
End Interface
