Imports Crunchyroll_Downloader.hls.common

Namespace hls.parsing.tags
    Public Class DateTimeTagParser
        Inherits TagParser(Of HlsMediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As HlsMediaPlaylistBuilder)
            If attributes.GetValues().Count = 0 Then
                Throw New HlsFormatException($"Parse failure - {GetTagName()} requires a date/time")
            End If

            playlist.AddDateTime(New SegmentDateTime(attributes.GetValues(0)))
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-PROGRAM-DATE-TIME"
        End Function
    End Class
End Namespace
