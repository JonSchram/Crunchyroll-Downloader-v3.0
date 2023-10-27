Imports PlaylistLibrary.hls.common
Imports PlaylistLibrary.hls.playlist

Namespace hls.parsing.tags.media
    Public Class ByteRangeTagParser
        Inherits TagParser(Of MediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As ParsedTag, playlist As MediaPlaylistBuilder)
            Dim values = attributes.GetValues()
            If values.Count = 0 Then
                Throw New HlsFormatException($"{GetTagName()} requires a value indicating the length and/or offset")
            End If
            Dim Bytes = New ByteRange(values(0))

            playlist.AddSegmentByteRange(Bytes)
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-BYTERANGE"
        End Function
    End Class
End Namespace

