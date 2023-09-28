Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.playlist
Imports Crunchyroll_Downloader.hls.segment

Namespace hls.parsing.tags.media
    Public Class MediaInitializationTagParser
        Inherits TagParser(Of MediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As MediaPlaylistBuilder)
            Dim Uri = attributes.GetAttribute("URI")
            If Uri Is Nothing Then
                Throw New HlsFormatException($"{GetTagName()} requires URI to be set.")
            End If

            Dim ByteRangeString = attributes.GetAttribute("BYTERANGE")
            Dim bytes As ByteRange = Nothing
            If ByteRangeString IsNot Nothing Then
                bytes = New ByteRange(ByteRangeString)
            End If

            Dim initialization = New MediaInitialization(Uri, bytes)
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-MAP"
        End Function
    End Class
End Namespace
