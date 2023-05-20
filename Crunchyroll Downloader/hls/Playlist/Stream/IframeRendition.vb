Imports Crunchyroll_Downloader.hls.common

Namespace hls.playlist.stream
    ''' <summary>
    ''' Raw information about an I-frame rendition that can be used to create a playlist.
    ''' </summary>
    Public Class IframeRendition
        Inherits Rendition

        Public Sub New(uri As String, bandwidth As Integer, averageBandwidth As Integer,
                       videoResolution As Resolution, videoGroup As String, hdcpLevel As Hdcp,
                       codecs As List(Of String))
            MyBase.New(uri, bandwidth, averageBandwidth, videoResolution, videoGroup, hdcpLevel, codecs)
        End Sub
    End Class
End Namespace