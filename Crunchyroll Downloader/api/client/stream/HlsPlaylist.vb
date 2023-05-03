Imports Crunchyroll_Downloader.hls.playlist

Namespace api.client.stream
    Public Class HlsPlaylist
        Inherits SegmentedMedia

        Private ReadOnly Playlist As MediaPlaylist

        Public Sub New(type As MediaType)
            MyBase.New(type)
        End Sub

        Public Overrides Sub WritePlaylistTo(s As IO.Stream)
            Throw New NotImplementedException()
        End Sub
    End Class
End Namespace