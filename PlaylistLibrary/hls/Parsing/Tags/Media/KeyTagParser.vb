Imports PlaylistLibrary.hls.playlist

Namespace hls.parsing.tags.media
    Public Class KeyTagParser
        Inherits TagParser(Of MediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As ParsedTag, playlist As MediaPlaylistBuilder)
            Dim encryptionKey = KeyParser.ParseEncryptionKey(attributes, GetTagName())
            playlist.AddKey(encryptionKey)
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-KEY"
        End Function
    End Class
End Namespace