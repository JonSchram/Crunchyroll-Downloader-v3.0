Namespace hls.parsing.tags
    Public Class MediaSequenceNumberTagParser
        Inherits TagParser(Of HlsMediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As HlsMediaPlaylistBuilder)
            Dim values = attributes.GetValues()
            If values.Count = 0 Then
                Throw New HlsFormatException($"Start sequence number must be set for {GetTagName()}")
            End If

            playlist.SetStartSequenceNumber(CInt(values(0)))
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-MEDIA-SEQUENCE"
        End Function
    End Class
End Namespace