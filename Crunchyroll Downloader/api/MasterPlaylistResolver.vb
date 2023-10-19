Imports System.Net.Http
Imports Crunchyroll_Downloader.api.common
Imports Crunchyroll_Downloader.hls.parsing

Namespace api
    Public Class MasterPlaylistResolver
        Implements IMediaLinkResolver(Of HlsMasterPlaylistLink, MasterPlaylistMedia)

        Private ReadOnly DownloadClient As HttpClient

        Public Sub New(client As HttpClient)
            DownloadClient = client
        End Sub

        Public Async Function ResolveMedia(link As HlsMasterPlaylistLink) As Task(Of MasterPlaylistMedia) Implements IMediaLinkResolver(Of HlsMasterPlaylistLink, MasterPlaylistMedia).ResolveMedia
            Using response = Await DownloadClient.GetAsync(link.Location)
                response.EnsureSuccessStatusCode()

                Dim parser = New PlaylistParser()
                Dim contentStream = Await response.Content.ReadAsStreamAsync()
                Dim parsedPlaylist = parser.ParseMasterPlaylist(contentStream)

                Dim result = New MasterPlaylistMedia(link.Type, link.MediaLanguage, link.Location, parsedPlaylist)
                Return result
            End Using
        End Function
    End Class
End Namespace