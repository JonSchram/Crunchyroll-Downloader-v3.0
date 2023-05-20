Imports Crunchyroll_Downloader.hls.playlist
Imports Crunchyroll_Downloader.hls.writer

Namespace api.client.stream
    Public Class HlsPlaylist
        Inherits SegmentedMedia

        Private ReadOnly Playlist As MediaPlaylist

        Public Sub New(type As MediaType)
            MyBase.New(type)
        End Sub

        Public Overrides Sub WritePlaylistTo(output As IO.Stream)
            ' TODO: Make async
            Dim writer = New PlaylistWriter()
            writer.WriteToStream(Playlist, output)
        End Sub
        Public Overrides Function GetSegment(segmentNumber As Integer) As StreamSegment
            Dim segment = Playlist.Segments.Item(segmentNumber)
            Dim bytes = segment.Bytes
            Return New StreamSegment(segment.Uri, New ByteRange(bytes.Length, bytes.Offset), segmentNumber)
        End Function

        Public Overrides Function GetSegmentCount() As Integer
            Return Playlist.Segments.Count()
        End Function
    End Class
End Namespace