Namespace hls.parsing.tags
    Public Class DiscontinuityTagParser
        Inherits TagParser(Of HlsMediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As HlsMediaPlaylistBuilder)
            playlist.AddDiscontinuity()
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-DISCONTINUITY"
        End Function
    End Class
End Namespace