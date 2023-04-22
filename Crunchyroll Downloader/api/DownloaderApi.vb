Public Class DownloaderApi
    Private downloadUrl As String

    Public Sub New(url As String)
        downloadUrl = url
    End Sub

    Public Function IsFunimationUrl() As Boolean
        Return UrlUtilities.IsFunimationUrl(downloadUrl)
    End Function

    Public Function IsCrunchyrollUrl() As Boolean
        Return UrlUtilities.IsCrunchyrollUrl(downloadUrl)
    End Function

    ' TODO:
    ' Split API into:
    '  - Series metadata
    '  - Season metadata
    '  - Episode metadata
    '  - Episode playback
    '
    ' This makes it clearer what an API class exists for,
    ' solves the problem of creating a metadata extractor with a URL that might be for a season or an episode,
    ' and should hopefully make the API a little cleaner by reducing the temptation to tightly couple code.

    Public Function GetMetadataDownloader() As IMetadataDownloader
        ' TODO: Choose CR or Funi metadata downloader
        If (Me.IsFunimationUrl()) Then
            Return New FunimationExtractor(downloadUrl)
        ElseIf Me.IsCrunchyrollUrl() Then
            ' TODO
            Return Nothing
        Else
            Return Nothing
        End If
    End Function

    Public Function getEpisodeDownloader(Episode As Episode) As IEpisodeDownloader
        ' TODO: See if there's a better way to do this. Seems silly to pass in an episode to get the downloader and then
        ' another for actually downloading.
        ' Maybe add the episode as a constructor and pass it to the downloader.
        ' The only problem here is if it needs cookies to download the video, each instance has to set new ones.
        If TypeOf Episode Is FunimationEpisode Then
            Return New FunimationDownloader()
        End If
        Return Nothing
    End Function
End Class
