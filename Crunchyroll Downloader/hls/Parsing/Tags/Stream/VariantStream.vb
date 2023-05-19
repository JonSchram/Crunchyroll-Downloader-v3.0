Namespace hls.parsing.tags.stream
    ''' <summary>
    ''' Represents a variant stream that can be combined with other streams for a single playback.
    ''' </summary>
    Public Class VariantStream
        Inherits AbstractStream

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


        Public Sub New(SourceTag As TagAttributes, Uri As String)
            MyBase.New(SourceTag)

            If GetTagName() <> SourceTag.getTagName() Then
                Throw New ArgumentException($"Tag {SourceTag.getTagName()} is incorrect for a variant stream, expected {GetTagName()}")
            End If

            ' TODO: For some of these optional attributes, consider replacing the exception with a log statement.
            ' For development, helps catch errors in programming, but when using the program, don't necessarily
            ' want the whole program to fail from external input
            ' Wonder if the same could be said for "Required" properties that wouldn't block downloading a stream

            If Uri Is Nothing Then
                Throw New HlsFormatException($"{GetTagName()} requires a URI.")
            End If
            Me._uri = Uri

            Audio = SourceTag.GetAttribute("AUDIO")
            Subtitles = SourceTag.GetAttribute("SUBTITLES")
            ClosedCaptions = SourceTag.GetAttribute("CLOSED-CAPTIONS")

            Dim FrameRateString = SourceTag.GetAttribute("FRAME-RATE")
            If FrameRateString IsNot Nothing Then
                FrameRate = CDbl(FrameRateString)
            End If

        End Sub



        Public Overrides Function ToString() As String
            Return $"{{
  Resolution: {StreamResolution.ToString()},
  Bandwidth: {Bandwidth},
  AverageBandwidth: {AverageBandwidth},
  FrameRate: {FrameRate},
  Uri: {Uri}
  Audio: {Audio},
  Video: {Video},
  Subtitles: {Subtitles},
  ClosedCaptions: {ClosedCaptions}
  HdcpLevel: {HdcpLevel},
  Codecs: {Codecs}
}}"
        End Function

        Protected Overrides Function GetTagName() As String
            Return "EXT-X-STREAM-INF"
        End Function

    End Class

End Namespace