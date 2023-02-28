Public Interface IMetadataDownloader
    ''' <summary>
    ''' Gets information about all seasons available in a series.
    ''' </summary>
    ''' <returns></returns>
    Function ListSeasons() As IEnumerable(Of Season)

    ''' <summary>
    ''' Gets the information about all episodes in a season and how to get more information about
    ''' an individual episode.
    ''' </summary>
    ''' <param name="SeasonName"></param>
    ''' <returns></returns>
    Function ListEpisodes(SeasonName As String) As List(Of Episode)

    ''' <summary>
    ''' Gets the information required to locate the episode playback
    ''' Also can be used to format the output episode
    ''' </summary>
    ''' <param name="EpisodeId"></param>
    ''' <returns></returns>
    Function getEpisodeInfo(EpisodeId As String) As EpisodeInfo

    ''' <summary>
    ''' Downloads the episode info from the download URL, assumed to be a link to an episode.
    ''' </summary>
    ''' <returns></returns>
    Function getEpisodeInfo() As EpisodeInfo

    Function IsVideoUrl() As Boolean
End Interface
