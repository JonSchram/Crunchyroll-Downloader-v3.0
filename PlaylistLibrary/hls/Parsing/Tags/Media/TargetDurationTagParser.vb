Imports System.IO
Imports PlaylistLibrary.hls.playlist

Namespace hls.parsing.tags.media
    Public Class TargetDurationTagParser
        Inherits TagParser(Of MediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As TextReader, attributes As ParsedTag, playlist As MediaPlaylistBuilder)
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
