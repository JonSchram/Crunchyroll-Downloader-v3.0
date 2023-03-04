''' <summary>
''' Common attributes of video streams in master playlists.
''' </summary>
Public MustInherit Class AbstractStream
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
    ' Comma-separated list but this will probably never be parsed
    Public ReadOnly Property Codecs As String

    Public ReadOnly Property Uri As String
        Get
            Return _uri
        End Get
    End Property

    Protected MustOverride Function GetTagName() As String

    Public Sub New(SourceTag As Tag)

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
            StreamResolution = ParseResolution(ResolutionString)
        End If


        Video = SourceTag.GetAttribute("VIDEO")

        Dim hdcpString = SourceTag.GetAttribute("HDCP-LEVEL")
        If hdcpString IsNot Nothing Then
            HdcpLevel = parseHdcp(hdcpString)
        End If

        Codecs = SourceTag.GetAttribute("CODECS")
    End Sub

    Private Function ParseResolution(resolutionString As String) As Resolution
        Dim dimensions() = Split(resolutionString, "x")
        If dimensions.Length = 0 Then
            Throw New HlsFormatException($"{GetTagName()} resolution format error, expected format <horizontal>x<vertical>")
        End If
        Return New Resolution(CInt(dimensions(0)), CInt(dimensions(1)))
    End Function

    Public Class Resolution
        Public Sub New(horizontalResolution As Integer, VerticalResolution As Integer)
            Horizontal = horizontalResolution
            Vertical = VerticalResolution
        End Sub

        Public ReadOnly Property Horizontal As Double
        Public ReadOnly Property Vertical As Double

        Public Overrides Function ToString() As String
            Return $"{Horizontal}x{Vertical}"
        End Function
    End Class

    Private Function parseHdcp(HdcpString As String) As Hdcp
        Select Case HdcpString
            Case "TYPE-0"
                Return Hdcp.TYPE_0
            Case "NONE"
                Return Hdcp.NONE
            Case Else
                Throw New HlsFormatException($"{GetTagName()} HDCP level format error, expected NONE or TYPE-0")
        End Select
    End Function

    Public Enum Hdcp
        NONE
        TYPE_0
    End Enum
End Class
