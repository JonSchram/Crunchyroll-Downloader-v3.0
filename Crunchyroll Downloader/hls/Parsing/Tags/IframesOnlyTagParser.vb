Namespace hls.parsing.tags
    Public Class IframesOnlyTagParser
        Inherits TagParser(Of HlsMediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As HlsMediaPlaylistBuilder)
            playlist.SetIFramesOnly()
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-I-FRAMES-ONLY"
        End Function
    End Class
End Namespace