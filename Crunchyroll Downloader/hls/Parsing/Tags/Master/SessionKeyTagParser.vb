Imports Crunchyroll_Downloader.hls.playlist
Imports Crunchyroll_Downloader.hls.segment.encryption

Namespace hls.parsing.tags.master
    Public Class SessionKeyTagParser
        Inherits TagParser(Of MasterPlaylist.Builder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As ParsedTag, playlist As MasterPlaylist.Builder)
            Dim encryptionKey = KeyParser.ParseEncryptionKey(attributes, GetTagName())
            If encryptionKey.Method = EncryptionMethod.NONE Then
                Throw New HlsFormatException($"{GetTagName()} requires the encryption method is not NONE")
            End If

            playlist.AddKey(encryptionKey)
        End Sub


        Public Overrides Function GetTagName() As String
            Return "EXT-X-SESSION-KEY"
        End Function
    End Class
End Namespace