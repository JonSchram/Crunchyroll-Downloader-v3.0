Imports Crunchyroll_Downloader.hls.common

Namespace hls.parsing.tags.stream

    ''' <summary>
    ''' Common attributes of video streams in master playlists.
    ''' </summary>
    Public MustInherit Class AbstractStreamTag
        ' Parameter comments adapted from IETF RFC 8216: https://www.rfc-editor.org/rfc/rfc8216.html

        ' Required
        Public ReadOnly Property Bandwidth As Integer
        Protected _uri As String

        ' Optional
        Public ReadOnly Property AverageBandwidth As Integer
        Public ReadOnly Property StreamResolution As Resolution

        ' Must match a group ID of an EXT-X-MEDIA tag with type VIDEO in the master playlist
        Public ReadOnly Property Video As String

        Public ReadOnly Property HdcpLevel As Hdcp = Hdcp.NONE

        ' Other

        ' Should exist, but not required
        Public ReadOnly Property Codecs As String()

        Public ReadOnly Property Uri As String
            Get
                Return _uri
            End Get
        End Property

        Protected MustOverride Function GetTagName() As String

        Public Sub New(SourceTag As TagAttributes)

            Dim BandwidthString = SourceTag.GetAttribute("BANDWIDTH")
            If BandwidthString Is Nothing Then
                Throw New HlsFormatException($"{GetTagName()} requires bandwidth to be set.")
            End If
            Bandwidth = CInt(BandwidthString)

            Dim AverageBandwidthString = SourceTag.GetAttribute("AVERAGE-BANDWIDTH")
            If AverageBandwidthString IsNot Nothing Then
                AverageBandwidth = CInt(AverageBandwidthString)
            End If

            Dim ResolutionString = SourceTag.GetAttribute("RESOLUTION")
            If ResolutionString IsNot Nothing Then
                StreamResolution = HlsHelpers.ParseResolution(ResolutionString)
            End If


            Video = SourceTag.GetAttribute("VIDEO")

            Dim hdcpString = SourceTag.GetAttribute("HDCP-LEVEL")
            If hdcpString IsNot Nothing Then
                HdcpLevel = HlsHelpers.ParseHdcp(hdcpString)
            End If

            Dim codecString = SourceTag.GetAttribute("CODECS")
            If codecString IsNot Nothing Then
                Codecs = codecString.Split(","c)
            Else
                Codecs = New String() {}
            End If
        End Sub


    End Class
End Namespace