Imports Crunchyroll_Downloader.hls.playlist

Namespace hls.parsing.tags.encryption
    Public Class KeyTagParser
        Inherits TagParser(Of MediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As MediaPlaylistBuilder)
            Dim encryptionKey = KeyParser.ParseEncryptionKey(attributes, GetTagName())
            playlist.AddKey(encryptionKey)
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-KEY"
        End Function
    End Class
End Namespace