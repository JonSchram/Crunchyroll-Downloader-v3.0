Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.playlist

Namespace hls.parsing.tags.media
    Public Class PlaylistTypeTagParser
        Inherits TagParser(Of MediaPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As MediaPlaylistBuilder)
            Dim values = attributes.GetValues()
            If values.Count = 0 Then
                Throw New HlsFormatException($"{GetTagName()} must specify either EVENT or VOD")
            End If
            Dim Type As PlaylistType = CType([Enum].Parse(GetType(PlaylistType), values(0)), PlaylistType)

            playlist.SetPlaylistType(Type)
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-PLAYLIST-TYPE"
        End Function
    End Class
End Namespace
