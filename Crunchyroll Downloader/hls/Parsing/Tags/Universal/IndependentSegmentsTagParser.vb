Imports System.IO
Imports Crunchyroll_Downloader.hls.playlist.AbstractPlaylist

Namespace hls.parsing.tags.universal
    Public Class IndependentSegmentsTagParser
        Inherits TagParser(Of AbstractPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As TextReader, attributes As ParsedTag, playlist As AbstractPlaylistBuilder)
            playlist.SetIndependentSegments()
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-INDEPENDENT-SEGMENTS"
        End Function
    End Class
End Namespace