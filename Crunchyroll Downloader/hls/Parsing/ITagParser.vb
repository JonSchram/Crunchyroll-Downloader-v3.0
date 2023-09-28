Imports System.IO
Imports Crunchyroll_Downloader.hls.playlist

Namespace hls.parsing
    Public Interface ITagParser
        Function GetTagName() As String

        Sub ParseInto(reader As TextReader, attributes As TagAttributes, playlist As AbstractPlaylist.AbstractPlaylistBuilder)
    End Interface
End Namespace