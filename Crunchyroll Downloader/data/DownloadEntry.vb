Imports Crunchyroll_Downloader.api.common

Namespace data
    Public Class DownloadEntry
        Public ReadOnly Property Uri As String
        Public ReadOnly Property ContainedMedia As MediaType

        Public Sub New(location As String, media As MediaType)
            Uri = location
            ContainedMedia = media
        End Sub
    End Class
End Namespace