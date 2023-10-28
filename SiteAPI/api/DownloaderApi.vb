
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

        Public Shared Function GetMetadataDownloader(url As String, cookieProvider As ICookieProvider, userAgent As String) As IDownloadClient
            ' TODO: Choose CR or Funi metadata downloader
            If IsFunimationUrl(url) Then
                Return New FunimationClient(cookieProvider, userAgent)
            ElseIf IsCrunchyrollUrl(url) Then
                ' TODO
                Return Nothing
            Else
                Return Nothing
            End If
        End Function
    End Class
End Namespace