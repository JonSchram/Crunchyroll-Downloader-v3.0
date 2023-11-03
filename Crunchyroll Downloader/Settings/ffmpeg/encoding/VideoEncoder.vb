Namespace settings.ffmpeg.encoding
    Public Class VideoEncoder
        Public ReadOnly Property VideoCodec As Codec
        Public ReadOnly Property Hardware As EncoderImplementation
        ' Preset only exists for h.264/h.265
        Public ReadOnly Property Preset As SpeedSetting
        Public ReadOnly UseTargetBitrate As Boolean
        ' Target bitrate, in KBit/sec
        Public ReadOnly Property TargetBitrate As Integer

        Public Sub New(videoCodec As Codec, hardware As EncoderImplementation, preset As SpeedSetting, useTargetBitrate As Boolean, bitrate As Integer)
            Me.VideoCodec = videoCodec
            Me.Hardware = hardware
            Me.Preset = preset
            Me.UseTargetBitrate = useTargetBitrate
            TargetBitrate = bitrate
        End Sub

        Public Function GetFfmpegArguments() As String
            Dim arguments As New List(Of String) From {
            GetCodecArgument(),
            GetPresetArgument(),
            GetBitrateArgument()
        }

            Dim commandString = ""
            For Each argument As String In arguments
                If argument <> "" Then
                    If commandString.Length > 0 Then
                        commandString += " "
                    End If
                    commandString += argument
                End If
            Next

            Return commandString
        End Function

        Public Function GetCodecArgument() As String
            Return "-c:v " + GetEncoderName()
        End Function

        Private Function GetEncoderName() As String
            Select Case VideoCodec
                Case Codec.H_264
                    Return GetNameForH264Video()
                Case Codec.H_265
                    Return GetNameForH265Video()
                Case Codec.AV1
                    Return GetNameForAv1Video()
                Case Else
                    Return ""
            End Select
        End Function

        Private Function GetNameForH264Video() As String
            Select Case Hardware
                Case EncoderImplementation.SOFTWARE
                    Return "libx264"
                Case EncoderImplementation.NVIDIA
                    Return "h264_nvenc"
                Case EncoderImplementation.AMD
                    Return "h264_amf"
                Case EncoderImplementation.INTEL
                    Return "h264_qsv"
                Case Else
                    Return "libx264"
            End Select
        End Function

        Private Function GetNameForH265Video() As String
            Select Case Hardware
                Case EncoderImplementation.SOFTWARE
                    Return "libx265"
                Case EncoderImplementation.NVIDIA
                    Return "hevc_nvenc"
                Case EncoderImplementation.AMD
                    Return "hevc_amf"
                Case EncoderImplementation.INTEL
                    Return "hevc_qsv"
                Case Else
                    Return "libx265"
            End Select
        End Function

        Private Function GetNameForAv1Video() As String
            Select Case Hardware
                Case EncoderImplementation.SOFTWARE
                    Return "libsvtav1"
                Case EncoderImplementation.NVIDIA
                    Return "av1_nvenc"
                Case EncoderImplementation.AMD
                    ' Seems to be in progress but this is the current name
                    Return "av1_amf"
                Case EncoderImplementation.INTEL
                    Return "av1_qsv"
                Case Else
                    Return "libsvtav1"
            End Select
        End Function

        Public Function GetPresetArgument() As String
            If Preset = SpeedSetting.NO_PRESET Then
                Return ""
            Else
                Return "-preset " + GetPresetText()
            End If
        End Function

        Private Function GetPresetText() As String
            Select Case Preset
                Case SpeedSetting.VERY_FAST
                    Return "veryfast"
                Case SpeedSetting.FASTER
                    Return "faster"
                Case SpeedSetting.FAST
                    Return "fast"
                Case SpeedSetting.MEDIUM
                    Return "medium"
                Case SpeedSetting.SLOW
                    Return "slow"
                Case SpeedSetting.SLOWER
                    Return "slower"
                Case SpeedSetting.VERY_SLOW
                    Return "veryslow"
                Case Else
                    Return ""
            End Select
        End Function

        Private Function GetBitrateArgument() As String
            If UseTargetBitrate Then
                Return "-b:v " + CStr(TargetBitrate) + "k"
            Else
                Return ""
            End If
        End Function
    End Class
End Namespace