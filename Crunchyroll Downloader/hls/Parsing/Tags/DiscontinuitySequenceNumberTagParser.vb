Namespace hls.parsing.tags
    Public Class DiscontinuitySequenceNumberTagParser
        Inherits TagParser(Of HlsMediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As HlsMediaPlaylistBuilder)
            Dim values = attributes.GetValues()
            If values.Count = 0 Then
                Throw New HlsFormatException($"Start sequence number must be set for {GetTagName()}")
            End If
            playlist.SetDiscontinuitySequenceNumber(CInt(values(0)))
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-DISCONTINUITY-SEQUENCE"
        End Function
    End Class
End Namespace