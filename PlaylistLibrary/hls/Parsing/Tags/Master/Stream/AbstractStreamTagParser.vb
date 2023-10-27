Imports PlaylistLibrary.hls.playlist
Imports PlaylistLibrary.hls.playlist.stream

Namespace hls.parsing.tags.master.stream

    ''' <summary>
    ''' Common attributes of video streams in master playlist.
    ''' </summary>
    Public MustInherit Class AbstractStreamTagParser
        Inherits TagParser(Of MasterPlaylist.Builder)

        Protected Sub ParseToRendition(attributes As ParsedTag, builder As IBaseStreamBuilder(Of BaseStreamMetadata))
            ' Error messages and comments adapted from IETF RFC 8216: https://www.rfc-editor.org/rfc/rfc8216.html

            ' Required ------
            Dim BandwidthString As String = attributes.GetAttribute("BANDWIDTH")?.Value
            If BandwidthString Is Nothing Then
                Throw New HlsFormatException($"{GetTagName()} requires bandwidth to be set.")
            End If
            builder.SetBandwidth(CInt(BandwidthString))
            ' ---------------

            ' Optional ------
            Dim AverageBandwidthString = attributes.GetAttribute("AVERAGE-BANDWIDTH")?.Value
            If AverageBandwidthString IsNot Nothing Then
                builder.SetAverageBandwidth(CInt(BandwidthString))
            End If

            Dim ResolutionString = attributes.GetAttribute("RESOLUTION")?.Value
            If ResolutionString IsNot Nothing Then
                builder.SetVideoResolution(HlsHelpers.ParseResolution(ResolutionString))
            End If

            ' Must match a group ID of an EXT-X-MEDIA tag with type VIDEO in the master playlist
            builder.SetVideoGroup(attributes.GetAttribute("VIDEO")?.Value)

            Dim hdcpString = attributes.GetAttribute("HDCP-LEVEL")?.Value
            If hdcpString IsNot Nothing Then
                builder.SetHdcpLevel(HlsHelpers.ParseHdcp(hdcpString))
            End If
            ' ---------------

            ' Should exist, but not required
            Dim codecString = attributes.GetAttribute("CODECS")?.Value
            If codecString IsNot Nothing Then
                builder.SetCodecs(codecString.Split(","c))
            End If
        End Sub


    End Class
End Namespace