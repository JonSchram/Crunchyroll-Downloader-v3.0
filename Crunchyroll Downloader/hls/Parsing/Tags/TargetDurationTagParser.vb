Imports System.IO

Namespace hls.parsing.tags
    Public Class TargetDurationTagParser
        Inherits TagParser(Of HlsMediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As TextReader, attributes As TagAttributes, playlist As HlsMediaPlaylistBuilder)
            Dim values = attributes.GetValues()
            If values.Count = 0 Then
                Throw New HlsFormatException($"Target duration must be set for {GetTagName()}")
            End If

            playlist.SetTargetDuration(CInt(values(0)))
        End Sub


        Public Overrides Function GetTagName() As String
            Return "EXT-X-TARGETDURATION"
        End Function
    End Class
End Namespace
