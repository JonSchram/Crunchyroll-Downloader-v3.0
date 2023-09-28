Imports Crunchyroll_Downloader.hls.playlist

Namespace hls.parsing.tags.media
    Public Class DiscontinuitySequenceNumberTagParser
        Inherits TagParser(Of MediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As MediaPlaylistBuilder)
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