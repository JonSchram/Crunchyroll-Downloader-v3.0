Imports PlaylistLibrary.hls.common

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
        Public ReadOnly Property HasClosedCaptions As Boolean

        Public ReadOnly Property FrameRate As Double


        Public Sub New(uri As String, bandwidth As Integer, averageBandwidth As Integer,
            videoResolution As Resolution, videoGroup As String, hdcpLevel As Hdcp,
            codecs As IEnumerable(Of String), audioGroup As String, subtitleGroup As String,
            closedCaptionsGroup As String, HasClosedCaptions As Boolean, frameRate As Double)
            MyBase.New(uri, bandwidth, averageBandwidth, videoResolution, videoGroup, hdcpLevel, codecs)
            Me.AudioGroup = audioGroup
            Me.SubtitleGroup = subtitleGroup
            Me.ClosedCaptionsGroup = closedCaptionsGroup
            Me.HasClosedCaptions = HasClosedCaptions
            Me.FrameRate = frameRate
        End Sub

        Public Overrides Function ToString() As String
            Return $"[VariantStreamMetadata: " +
                $"Video resolution: {VideoResolution}, Framerate: {FrameRate}, " +
                $"Maximum bandwidth: {Bandwidth}, Average bandwidth: {AverageBandwidth}, " +
                $"URI: {Uri}, " +
                $"Codecs: {FormatList(Codecs)}, " +
                $"Video group: ""{VideoGroup}"", Audio group: ""{AudioGroup}"", Subtitle Group: ""{SubtitleGroup}"", " +
                $"Closed Captions Group: ""{ClosedCaptionsGroup}"", " +
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
            Inherits Builder(Of VariantStreamMetadata)

            Private AudioGroup As String

            Private SubtitleGroup As String
            Public Property ClosedCaptionsGroup As String
            Public Property HasClosedCaptions As Boolean

            Public Property FrameRate As Double

            Public Sub SetAudioGroup(groupId As String)
                AudioGroup = groupId
            End Sub

            Public Sub SetSubtitleGroup(groupId As String)
                SubtitleGroup = groupId
            End Sub

            Public Sub SetClosedCaptionGroup(groupId As String)
                ClosedCaptionsGroup = groupId
            End Sub

            Public Sub SetHasClosedCaptions(hasClosedCaptions As Boolean)
                Me.HasClosedCaptions = hasClosedCaptions
            End Sub

            Public Sub SetFrameRate(frameRate As Double)
                Me.FrameRate = frameRate
            End Sub

            Public Overrides Function Build() As VariantStreamMetadata
                Return New VariantStreamMetadata(Uri, Bandwidth, AverageBandwidth, VideoResolution, VideoGroupId,
                    HdcpLevel, Codecs, AudioGroup, SubtitleGroup, ClosedCaptionsGroup, HasClosedCaptions, FrameRate)
            End Function
        End Class
    End Class
End Namespace