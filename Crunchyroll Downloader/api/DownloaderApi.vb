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
End Class
