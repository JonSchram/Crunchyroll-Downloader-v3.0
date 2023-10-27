Imports PlaylistLibrary.hls.playlist

Namespace hls.parsing.tags.media
    Public Class InfTagParser
        Inherits TagParser(Of MediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As ParsedTag, playlist As MediaPlaylistBuilder)
            Dim Values = attributes.GetValues()
            If Values.Count = 0 Then
                Throw New HlsFormatException($"Parse failure: {GetTagName()} requires a duration to be set.")
            End If

            Dim duration As Double = CDbl(Values(0))
            Dim title As String = Nothing
            If Values.Count >= 2 Then
                title = String.Join(",", Values.Skip(1))
            End If

            playlist.AddSegmentInfo(duration, title)
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXTINF"
        End Function
    End Class
End Namespace
