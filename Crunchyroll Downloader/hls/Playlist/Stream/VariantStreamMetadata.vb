Imports Crunchyroll_Downloader.hls.common

Namespace hls.playlist.stream
    ''' <summary>
    ''' Raw information about a variant stream.
    ''' 
    ''' References alternative renditions by group ID in order to lazily filter them and build a complete variant
    ''' stream when requested. Therefore does not contain any alternate renditions at this stage.
    ''' </summary>
    Public Class VariantStreamMetadata
        Inherits BaseStreamMetadata

        Public ReadOnly Property AudioGroup As String
        Public ReadOnly Property SubtitleGroup As String
        Public ReadOnly Property ClosedCaptionsGroup As String

        Public ReadOnly Property FrameRate As Double

        Public Sub New(uri As String, bandwidth As Integer, averageBandwidth As Integer,
                   videoResolution As Resolution, videoGroup As String, hdcpLevel As Hdcp,
                   codecs As List(Of String), audioGroup As String, subtitleGroup As String,
                   closedCaptionsGroup As String, frameRate As Double)
            MyBase.New(uri, bandwidth, averageBandwidth, videoResolution, videoGroup, hdcpLevel, codecs)
            Me.AudioGroup = audioGroup
            Me.SubtitleGroup = subtitleGroup
            Me.ClosedCaptionsGroup = closedCaptionsGroup
            Me.FrameRate = frameRate
        End Sub
    End Class
End Namespace