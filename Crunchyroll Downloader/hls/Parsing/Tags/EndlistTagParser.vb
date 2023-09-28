Namespace hls.parsing.tags
    Public Class EndlistTagParser
        Inherits TagParser(Of HlsMediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As HlsMediaPlaylistBuilder)
            playlist.SetEndlist()
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-ENDLIST"
        End Function
    End Class
End Namespace