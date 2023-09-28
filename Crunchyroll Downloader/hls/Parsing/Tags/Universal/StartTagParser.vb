Imports System.IO
Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.playlist.AbstractPlaylist

Namespace hls.parsing.tags.universal
    Public Class StartTagParser
        Inherits TagParser(Of AbstractPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As TextReader, attributes As TagAttributes, playlist As AbstractPlaylistBuilder)
            ' Required
            Dim offsetString = attributes.GetAttribute("TIME-OFFSET")
            If offsetString Is Nothing Then
                Throw New HlsFormatException($"{GetTagName()} requires TIME-OFFSET to be set")
            End If
            Dim TimeOffset = CDbl(offsetString)

            ' Optional
            Dim PreciseString = attributes.GetAttribute("PRECISE")
            Dim precise As Boolean = False
            If offsetString IsNot Nothing Then
                precise = HlsHelpers.ParseYesNoValue(PreciseString, "PRECISE")
            End If

            playlist.SetStart(New PlaylistStartTime(TimeOffset, precise))
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-START"
        End Function
    End Class

End Namespace
