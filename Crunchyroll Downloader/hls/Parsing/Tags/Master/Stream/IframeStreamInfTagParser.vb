Imports System.IO
Imports Crunchyroll_Downloader.hls.parsing.tags.stream
Imports Crunchyroll_Downloader.hls.playlist
Imports Crunchyroll_Downloader.hls.playlist.stream

Namespace hls.parsing.tags.master.stream
    ''' <summary>
    ''' Represents an I-frame only video stream.
    ''' </summary>
    Public Class IframeStreamInfTagParser
        Inherits AbstractStreamTagParser

        Public Overrides Sub ParseInner(reader As TextReader, attributes As ParsedTag, playlist As MasterPlaylist.Builder)
            Dim iframeRenditionBuilder = New IFrameStreamMetadata.Builder()
            ParseToRendition(attributes, iframeRenditionBuilder)

            Dim uri As String = attributes.GetAttribute("URI")?.Value
            If uri Is Nothing Then
                Throw New HlsFormatException($"{GetTagName()} requires a URI.")
            End If
            iframeRenditionBuilder.SetUri(uri)

            playlist.AddIframeStream(iframeRenditionBuilder.Build())
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-I-FRAME-STREAM-INF"
        End Function
    End Class
End Namespace