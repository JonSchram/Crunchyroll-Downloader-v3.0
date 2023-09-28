Imports Crunchyroll_Downloader.hls.playlist

Namespace hls.parsing.tags.media
    Public Class IframesOnlyTagParser
        Inherits TagParser(Of MediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As MediaPlaylistBuilder)
            playlist.SetIFramesOnly()
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-I-FRAMES-ONLY"
        End Function
    End Class
End Namespace