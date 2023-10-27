Imports System.IO
Imports PlaylistLibrary.hls.playlist

Namespace hls.parsing.tags.universal
    Public Class IndependentSegmentsTagParser
        Inherits TagParser(Of AbstractPlaylist.AbstractPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As TextReader, attributes As ParsedTag, playlist As AbstractPlaylist.AbstractPlaylistBuilder)
            playlist.SetIndependentSegments()
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-INDEPENDENT-SEGMENTS"
        End Function
    End Class
End Namespace