Imports System.IO
Imports PlaylistLibrary.hls.playlist

Namespace hls.parsing
    Public Interface ITagParser
        Function GetTagName() As String

        Sub ParseInto(reader As TextReader,
                      attributes As ParsedTag,
                      playlist As AbstractPlaylist.AbstractPlaylistBuilder)
    End Interface
End Namespace