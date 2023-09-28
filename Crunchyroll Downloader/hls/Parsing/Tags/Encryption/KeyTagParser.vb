Namespace hls.parsing.tags.encryption
    Public Class KeyTagParser
        Inherits TagParser(Of HlsMediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As HlsMediaPlaylistBuilder)
            Dim encryptionKey = KeyParser.ParseEncryptionKey(attributes, GetTagName())
            playlist.AddKey(encryptionKey)
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-KEY"
        End Function
    End Class
End Namespace