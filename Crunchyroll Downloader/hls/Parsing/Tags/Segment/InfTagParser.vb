Namespace hls.parsing.tags.segment
    Public Class InfTagParser
        Inherits TagParser(Of HlsMediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As HlsMediaPlaylistBuilder)
            ' TODO: This looks really ugly, not sure how to improve it. Maybe add a getter to the Tag class?
            ' Like getValue(int)  & hasValue(int)

            Dim Values = attributes.GetValues()
            If Values.Count = 0 Then
                Throw New HlsFormatException($"Parse failure: {GetTagName()} requires a duration to be set.")
            End If

            Dim duration As Double = CDbl(Values(0))
            Dim title As String = Nothing
            If Values.Count >= 2 Then
                title = Values(1)
            End If

            playlist.AddSegmentInfo(duration, title)
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXTINF"
        End Function
    End Class
End Namespace
