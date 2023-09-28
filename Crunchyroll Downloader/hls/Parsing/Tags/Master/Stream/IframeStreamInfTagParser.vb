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

        Public Overrides Sub ParseInner(reader As TextReader, attributes As TagAttributes, playlist As MasterPlaylist.Builder)
            ' The problem is that this and the stream tag parser both need the same block of code
            ' to parse the same attributes. The rendition produced needs to be a specific type,
            ' so there can't be a superclass doing the build operation, and the storage object
            ' (that has a superclass) is immutable, so we can't pass an empty one in.
            ' But it could be a factory....makes a lot more code though.

            Dim iframeRenditionBuilder = New IFrameStreamMetadata.Builder()
            ParseToRendition(attributes, iframeRenditionBuilder)

            Dim uri = attributes.GetAttribute("URI")
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