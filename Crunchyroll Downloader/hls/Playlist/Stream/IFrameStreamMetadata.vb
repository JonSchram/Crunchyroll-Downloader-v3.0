Imports Crunchyroll_Downloader.hls.common

Namespace hls.playlist.stream
    ''' <summary>
    ''' Raw information about an I-frame rendition that can be used to create a playlist.
    ''' </summary>
    Public Class IFrameStreamMetadata
        Inherits BaseStreamMetadata

        Public Sub New(uri As String, bandwidth As Integer, averageBandwidth As Integer,
                       videoResolution As Resolution, videoGroup As String, hdcpLevel As Hdcp,
                       codecs As IEnumerable(Of String))
            MyBase.New(uri, bandwidth, averageBandwidth, videoResolution, videoGroup, hdcpLevel, codecs)
        End Sub

        Public Overrides Function ToString() As String
            Return $"[IFrameStreamMetadata: " +
                $"Video resolution: {VideoResolution}, " +
                $"Maximum bandwidth: {Bandwidth}, " +
                $"Average bandwidth: {AverageBandwidth}, " +
                $"URI: {Uri}, " +
                $"Codecs: {FormatList(Codecs)}, " +
                $"Video group: ""{VideoGroup}""" +
                $"HDCP level: {HdcpLevel}]"
        End Function

        Private Function FormatList(PropertyList As IEnumerable(Of Object)) As String
            Dim output As String = "["

            For Each streamItem In PropertyList
                output += streamItem.ToString() + ","
            Next

            output += "]"
            Return output
        End Function

        Public Class Builder
            Inherits Builder(Of IFrameStreamMetadata)

            Public Overrides Function Build() As IFrameStreamMetadata
                Return New IFrameStreamMetadata(Uri, Bandwidth, AverageBandwidth, VideoResolution, VideoGroupId, HdcpLevel, Codecs)
            End Function
        End Class
    End Class
End Namespace