Imports Crunchyroll_Downloader.api.common
Imports Crunchyroll_Downloader.download

Namespace api
    Public Class FileMediaResolver
        Implements IMediaLinkResolver(Of FileMediaLink, FileMedia)

        Public Function ResolveMedia(link As FileMediaLink) As FileMedia Implements IMediaLinkResolver(Of FileMediaLink, FileMedia).ResolveMedia
            Return New FileMedia(link.Type, link.MediaLanguage, link.Location)
        End Function
    End Class
End Namespace