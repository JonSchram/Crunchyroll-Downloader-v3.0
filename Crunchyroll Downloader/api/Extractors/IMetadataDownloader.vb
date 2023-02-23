Public Interface IMetadataDownloader
    Function ListSeasons() As IEnumerable(Of Season)

    Function ListEpisodes(SeasonName As String) As List(Of Episode)

    Function IsVideoUrl() As Boolean
End Interface
