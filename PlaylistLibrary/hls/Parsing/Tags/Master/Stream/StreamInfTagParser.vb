Imports System.IO
Imports PlaylistLibrary.hls.playlist
Imports PlaylistLibrary.hls.playlist.stream

Namespace hls.parsing.tags.master.stream
    ''' <summary>
    ''' Represents a variant stream that can be combined with other streams for a single playback.
    ''' </summary>
    Public Class StreamInfTagParser
        Inherits AbstractStreamTagParser

        Public Overrides Function GetTagName() As String
            Return "EXT-X-STREAM-INF"
        End Function

        Public Overrides Sub ParseInner(reader As TextReader, attributes As ParsedTag, playlist As MasterPlaylist.Builder)
            ' Error messages and comments adapted from IETF RFC 8216: https://www.rfc-editor.org/rfc/rfc8216.html

            Dim renditionBuilder = New VariantStreamMetadata.Builder()
            ParseToRendition(attributes, renditionBuilder)

            ' URI should be on the following line, but the RFC says blank lines should be ignored, so it's not clear
            ' whether the requirement this line could be blank or if it must be the URL. Gracefully handle it just in case.
            Dim uri As String = ""
            While "".Equals(uri)
                uri = reader.ReadLine()
                If uri Is Nothing Then
                    Throw New HlsFormatException($"{GetTagName()} requires a URI on the following line.")
                End If
            End While
            renditionBuilder.SetUri(uri)

            ' Must match a group ID of an EXT-X-MEDIA tag with type AUDIO in the master playlist
            renditionBuilder.SetAudioGroup(attributes.GetAttribute("AUDIO")?.Value)
            renditionBuilder.SetSubtitleGroup(attributes.GetAttribute("SUBTITLES")?.Value)

            ' Only closed captions may have an enumerated value of NONE
            Dim closedCaptions As PlaylistData = attributes.GetAttribute("CLOSED-CAPTIONS")
            If closedCaptions IsNot Nothing Then
                If closedCaptions.Quoted Then
                    renditionBuilder.SetClosedCaptionGroup(closedCaptions.Value)
                    renditionBuilder.SetHasClosedCaptions(True)
                ElseIf "NONE".Equals(closedCaptions.Value) Then
                    renditionBuilder.SetHasClosedCaptions(False)
                Else
                    Throw New HlsFormatException($"In {GetTagName()}, the value of CLOSED-CAPTIONS must either be the enumerated value NONE or a quoted string.")
                End If
            End If

            ' Optional but recommended
            Dim FrameRateString = attributes.GetAttribute("FRAME-RATE")?.Value
            If FrameRateString IsNot Nothing Then
                renditionBuilder.SetFrameRate(CDbl(FrameRateString))
            End If

            playlist.AddStreamVariant(renditionBuilder.Build())
        End Sub
    End Class

End Namespace