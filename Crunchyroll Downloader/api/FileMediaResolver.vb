Imports Crunchyroll_Downloader.api.common

Namespace api
    Public Class FileMediaResolver
        Implements IMediaLinkResolver(Of FileMediaLink, FileMedia)

        Public Function ResolveMedia(link As FileMediaLink) As Task(Of FileMedia) Implements IMediaLinkResolver(Of FileMediaLink, FileMedia).ResolveMedia
            Return Task.FromResult(New FileMedia(link.Type, link.MediaLocale, link.Location))
        End Function
    End Class
End Namespace