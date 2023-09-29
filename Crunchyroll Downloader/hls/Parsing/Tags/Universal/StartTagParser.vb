Imports System.IO
Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.playlist.AbstractPlaylist

Namespace hls.parsing.tags.universal
    Public Class StartTagParser
        Inherits TagParser(Of AbstractPlaylistBuilder)

        Public Overrides Sub ParseInner(reader As TextReader, attributes As ParsedTag, playlist As AbstractPlaylistBuilder)
            ' Required
            Dim offsetString As String = attributes.GetAttribute("TIME-OFFSET")?.Value
            If offsetString Is Nothing Then
                Throw New HlsFormatException($"{GetTagName()} requires TIME-OFFSET to be set")
            End If
            Dim TimeOffset = CDbl(offsetString)

            ' Optional
            Dim PreciseString As String = attributes.GetAttribute("PRECISE")?.Value
            Dim precise As Boolean = False
            If PreciseString IsNot Nothing Then
                precise = HlsHelpers.ParseYesNoValue(PreciseString, "PRECISE")
            End If

            playlist.SetStart(New PlaylistStartTime(TimeOffset, precise))
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-START"
        End Function
    End Class

End Namespace
