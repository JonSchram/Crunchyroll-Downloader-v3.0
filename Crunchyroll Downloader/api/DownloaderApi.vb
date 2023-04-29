Imports Crunchyroll_Downloader.api
Imports Crunchyroll_Downloader.api.client

Public Class DownloaderApi

    Private Sub New()
    End Sub

    Public Shared Function IsFunimationUrl(downloadUrl As String) As Boolean
        Return UrlUtilities.IsFunimationUrl(downloadUrl)
    End Function

    Public Shared Function IsCrunchyrollUrl(downloadUrl As String) As Boolean
        Return UrlUtilities.IsCrunchyrollUrl(downloadUrl)
    End Function

    ' TODO:
    ' Unify API such that all calls to a website go through the same class.
    ' There aren't many endpoints to call, and it would allow sharing the same API reference.

    Public Shared Function GetMetadataDownloader(url As String) As IMetadataDownloader
        ' TODO: Choose CR or Funi metadata downloader
        Dim cookieManager = Browser.GetInstance.GetCookieManager()
        If (IsFunimationUrl(url)) Then
            Return New FunimationClient(cookieManager)
        ElseIf IsCrunchyrollUrl(url) Then
            ' TODO
            Return Nothing
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function getEpisodeDownloader(Episode As Episode) As IEpisodeDownloader
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
