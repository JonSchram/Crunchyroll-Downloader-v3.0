Namespace settings
    Public Class FfmpegCommand
        Public ReadOnly Property Copy As Boolean

        Public ReadOnly Property VideoCodec As String

        Public ReadOnly Property PresetSpeed As Speed

        ' Bitrate of video, in thousands of bits per second
        Public ReadOnly Property VideoBitrate As Integer

        Public ReadOnly Property AudioCopy As Boolean = True

        Public ReadOnly Property AudioBitstreamFilterName As String = "aac_adtstoasc"

        Private Sub New(commandBuilder As Builder)
            Copy = commandBuilder.Copy

            If commandBuilder.Copy Then
                VideoCodec = ""
                PresetSpeed = Speed.NO_PRESET
                VideoBitrate = 0
            Else
                VideoCodec = commandBuilder.VideoCodec
                PresetSpeed = commandBuilder.PresetSpeed
                VideoBitrate = commandBuilder.VideoBitrate
            End If
        End Sub

        Public Enum Speed
            NO_PRESET
            SLOW
            FAST
        End Enum

        Public Class Builder
            Friend Copy As Boolean
            Friend VideoCodec As String
            Friend PresetSpeed As Speed
            Friend VideoBitrate As Integer

            Public Sub SetCopyMode(copy As Boolean)
                Me.Copy = copy
            End Sub

            Public Sub SetVideoCodec(codec As String)
                Me.VideoCodec = codec
            End Sub

            Public Sub SetPresetSpeed(presetSpeed As Speed)
                Me.PresetSpeed = presetSpeed
            End Sub

            Public Sub SetVideoBitrate(bitrate As Integer)
                Me.VideoBitrate = bitrate
            End Sub

            Public Function Build() As FfmpegCommand
                Return New FfmpegCommand(Me)
            End Function
        End Class
    End Class
End Namespace
