Imports Crunchyroll_Downloader.hls.playlist

Namespace hls.parsing.tags.media
    Public Class DiscontinuityTagParser
        Inherits TagParser(Of MediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As ParsedTag, playlist As MediaPlaylistBuilder)
            playlist.AddDiscontinuity()
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-DISCONTINUITY"
        End Function
    End Class
End Namespace