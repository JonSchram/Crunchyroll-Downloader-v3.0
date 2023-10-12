Imports Crunchyroll_Downloader.api.common
Imports Crunchyroll_Downloader.hls.parsing

Namespace api
    Public Class MasterPlaylistResolver
        Implements IMediaLinkResolver(Of HlsMasterPlaylistLink, MasterPlaylistMedia)

        Public Async Function ResolveMedia(link As HlsMasterPlaylistLink) As Task(Of MasterPlaylistMedia) Implements IMediaLinkResolver(Of HlsMasterPlaylistLink, MasterPlaylistMedia).ResolveMedia
            Dim parser = New PlaylistParser()
            ' TODO: use http client to get a stream for the playlist.
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace