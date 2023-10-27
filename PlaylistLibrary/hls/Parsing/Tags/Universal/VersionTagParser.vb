Imports System.IO
Imports PlaylistLibrary.hls.playlist

Namespace hls.parsing.tags.universal
    Public Class VersionTagParser
        Inherits TagParser(Of AbstractPlaylist.AbstractPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As TextReader, attributes As ParsedTag, playlist As AbstractPlaylist.AbstractPlaylistBuilder)
            Dim values = attributes.GetValues()
            If values.Count() = 0 Then
                Throw New HlsFormatException("Version tag must specify a version number")
            End If

            playlist.SetVersion(CInt(values.Item(0)))
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-VERSION"
        End Function
    End Class
End Namespace