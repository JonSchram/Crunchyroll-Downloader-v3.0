Imports System.IO
Imports Crunchyroll_Downloader.hls.playlist

Namespace hls.parsing
    Public MustInherit Class TagParser(Of T As AbstractPlaylist.AbstractPlaylistBuilder)
        Implements ITagParser

        Public MustOverride Sub ParseInner(reader As TextReader, attributes As TagAttributes, playlist As T)

        Public MustOverride Function GetTagName() As String Implements ITagParser.GetTagName

        Public Sub ParseInto(reader As TextReader, attributes As TagAttributes, playlist As AbstractPlaylist.AbstractPlaylistBuilder) Implements ITagParser.ParseInto
            If TypeOf playlist Is T Then
                ParseInner(reader, attributes, CType(playlist, T))
            Else
                Console.WriteLine($"TagParser type mismatch: {attributes.getTagName()} cannot be parsed into {GetType(T).Name}")
            End If
        End Sub

    End Class
End Namespace
