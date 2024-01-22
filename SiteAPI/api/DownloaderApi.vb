
Imports SiteAPI.api.crunchyroll
Imports SiteAPI.api.funimation

Namespace api
    Public Class DownloaderApi

        Private Sub New()
        End Sub

        Public Shared Function IsFunimationUrl(downloadUrl As String) As Boolean
            Return UrlUtilities.IsFunimationUrl(downloadUrl)
        End Function

        Public Shared Function IsCrunchyrollUrl(downloadUrl As String) As Boolean
            Return UrlUtilities.IsCrunchyrollUrl(downloadUrl)
        End Function

        Public Shared Function GetMetadataDownloader(url As String, cookieProvider As IInteractiveCookieProvider, userAgent As String) As IDownloadClient
            ' TODO: Choose CR or Funi metadata downloader
            If IsFunimationUrl(url) Then
                Return New FunimationClient(cookieProvider, userAgent)
            ElseIf IsCrunchyrollUrl(url) Then
                Return New CrunchyrollClient(cookieProvider, userAgent)
            Else
                Return Nothing
            End If
        End Function
    End Class
End Namespace