Imports Crunchyroll_Downloader.hls.playlist

Namespace hls.parsing.tags.media
    Public Class EndlistTagParser
        Inherits TagParser(Of MediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As MediaPlaylistBuilder)
            playlist.SetEndlist()
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-ENDLIST"
        End Function
    End Class
End Namespace