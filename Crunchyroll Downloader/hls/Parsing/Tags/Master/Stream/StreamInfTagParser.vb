Imports System.IO
Imports Crunchyroll_Downloader.hls.parsing.tags.stream
Imports Crunchyroll_Downloader.hls.playlist
Imports Crunchyroll_Downloader.hls.playlist.stream

Namespace hls.parsing.tags.master.stream
    ''' <summary>
    ''' Represents a variant stream that can be combined with other streams for a single playback.
    ''' </summary>
    Public Class StreamInfTagParser
        Inherits AbstractStreamTagParser

        ' TODO: Seems that the current version does care about the bandwidth / average bandwidth.
        ' Seems to choose the highest bandwidth stream
        ' Need to check whether the stream at the playist URI contains audio or if it needs to load the audio

        ' TODO: Make convenience method for getting a playback rendition.
        ' Would probably be at a layer up (in whatever object contains the variant stream list)


        ' Parameter comments adapted from IETF RFC 8216: https://www.rfc-editor.org/rfc/rfc8216.html

        ' Optional
        ' Must match a group ID of an EXT-X-MEDIA tag with type AUDIO in the master playlist
        Public ReadOnly Property Audio As String
        ' Must match a group ID of an EXT-X-MEDIA tag with type SUBTITLES in the master playlist
        Public ReadOnly Property Subtitles As String
        ' Must match a group ID of an EXT-X-MEDIA tag with type CLOSED-CAPTIONS in the master playlist
        Public ReadOnly Property ClosedCaptions As String

        ' Optional but recommended
        Public ReadOnly Property FrameRate As Double

        Public Overrides Function GetTagName() As String
            Return "EXT-X-STREAM-INF"
        End Function

        Public Overrides Sub ParseInner(reader As TextReader, attributes As TagAttributes, playlist As MasterPlaylist.Builder)
            ' Error messages and comments adapted from IETF RFC 8216: https://www.rfc-editor.org/rfc/rfc8216.html

            Dim renditionBuilder = New VariantStreamMetadata.Builder()
            ParseToRendition(attributes, renditionBuilder)

            ' TODO: For some of these optional attributes, consider replacing the exception with a log statement.
            ' For development, helps catch errors in programming, but when using the program, don't necessarily
            ' want the whole program to fail from external input
            ' Wonder if the same could be said for "Required" properties that wouldn't block downloading a stream.

            ' TODO: This could fail if there is a blank line between the EXT-X-STREAM-INF and the URL
            ' RFC 8216 says the URI line is required, but also says blank lines are ignored in section 4.1.
            Dim uri As String = reader.ReadLine()
            If uri Is Nothing Then
                Throw New HlsFormatException($"{GetTagName()} requires a URI on the following line.")
            End If
            renditionBuilder.SetUri(uri)

            renditionBuilder.SetAudioGroup(attributes.GetAttribute("AUDIO"))
            renditionBuilder.SetSubtitleGroup(attributes.GetAttribute("SUBTITLES"))

            ' TODO: The unquoted value NONE is technically different from a quoted value (which would be to reference an EXT-X-MEDIA tag)
            ' I don't think this is likely to happen but it might be worth enforcing.
            ' Only closed captions may have an enumerated value of NONE
            renditionBuilder.SetClosedCaptionGroup(attributes.GetAttribute("CLOSED-CAPTIONS"))

            Dim FrameRateString = attributes.GetAttribute("FRAME-RATE")
            If FrameRateString IsNot Nothing Then
                renditionBuilder.SetFrameRate(CDbl(FrameRateString))
            End If

            playlist.AddStreamVariant(renditionBuilder.Build())
        End Sub
    End Class

End Namespace