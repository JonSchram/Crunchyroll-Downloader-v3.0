Imports System.IO
Imports Crunchyroll_Downloader.hls.playlist.AbstractPlaylist

Namespace hls.parsing.tags
    Public Class VersionTagParser
        Inherits TagParser(Of AbstractPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As TextReader, attributes As TagAttributes, playlist As AbstractPlaylistBuilder)
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