Namespace settings
    Public Class FfmpegSettings
        Private ReadOnly _Encoder As VideoEncoder
        Public ReadOnly Property VideoCopy As Boolean

        Public ReadOnly Property AudioCopy As Boolean = True

        Public ReadOnly Property AudioBitstreamFilterName As String = "aac_adtstoasc"

        Private Sub New(commandBuilder As Builder)
            VideoCopy = commandBuilder.Copy

            If commandBuilder.IncludeUnusedSettings Or Not VideoCopy Then
                _Encoder = New VideoEncoder(commandBuilder.VideoCodec, commandBuilder.Hardware, commandBuilder.PresetSpeed,
                                            commandBuilder.UseVideoBitrate, commandBuilder.VideoBitrate)
            End If

        End Sub

        ''' <summary>
        ''' Gets the encoder that should be used for encoding a video stream. Returns Nothing if copy mode is set.
        ''' </summary>
        ''' <returns></returns>
        Public Function GetActiveEncoder() As VideoEncoder
            If VideoCopy Then
                Return Nothing
            End If
            Return _Encoder
        End Function

        ''' <summary>
        ''' Gets the encoder selected when constructing the object, even if the encoder is disabled.
        ''' May still return Nothing if copy mode is set and this object was constructed with no encoder.
        ''' </summary>
        ''' <returns></returns>
        Public Function GetSavedEncoder() As VideoEncoder
            Return _Encoder
        End Function

        Public Function GetFfmpegArguments() As String
            Dim command As String
            If VideoCopy Then
                command = "-c copy"
            Else
                command = _Encoder.GetFfmpegArguments()
                If AudioCopy Then
                    command += " -c:a copy"
                End If
            End If

            ' Always need the audio stream filter at the end
            command += " -bsf:a " + AudioBitstreamFilterName

            Return command
        End Function

        Public Class VideoEncoder
            Public ReadOnly Property VideoCodec As Codec
            Public ReadOnly Property Hardware As EncoderImplementation
            ' Preset only exists for h.264/h.265
            Public ReadOnly Property Preset As Speed
            Public ReadOnly UseTargetBitrate As Boolean
            ' Target bitrate, in KBit/sec
            Public ReadOnly Property TargetBitrate As Integer

            Public Sub New(videoCodec As Codec, hardware As EncoderImplementation, preset As Speed, useTargetBitrate As Boolean, bitrate As Integer)
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
                If Preset = Speed.NO_PRESET Then
                    Return ""
                Else
                    Return "-preset " + GetPresetText()
                End If
            End Function

            Private Function GetPresetText() As String
                Select Case Preset
                    Case Speed.VERY_FAST
                        Return "veryfast"
                    Case Speed.FASTER
                        Return "faster"
                    Case Speed.FAST
                        Return "fast"
                    Case Speed.MEDIUM
                        Return "medium"
                    Case Speed.SLOW
                        Return "slow"
                    Case Speed.SLOWER
                        Return "slower"
                    Case Speed.VERY_SLOW
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

            Public Enum Codec As Integer
                H_264 = 0
                H_265 = 1
                AV1 = 2
            End Enum

            Public Enum EncoderImplementation As Integer
                SOFTWARE = 0
                NVIDIA = 1
                AMD = 2
                INTEL = 3
            End Enum

            Public Enum Speed As Integer
                NO_PRESET = 0
                VERY_SLOW = 1
                SLOWER = 2
                SLOW = 3
                MEDIUM = 4
                FAST = 5
                FASTER = 6
                VERY_FAST = 7
            End Enum
        End Class

        Public Class Builder
            Friend Copy As Boolean
            Friend VideoCodec As VideoEncoder.Codec
            Friend Hardware As VideoEncoder.EncoderImplementation
            Friend PresetSpeed As VideoEncoder.Speed
            Friend UseVideoBitrate As Boolean = False
            Friend VideoBitrate As Integer
            Friend IncludeUnusedSettings As Boolean

            Public Function SetCopyMode(copy As Boolean) As Builder
                Me.Copy = copy
                Return Me
            End Function

            Public Function SetVideoCodec(videoCodec As VideoEncoder.Codec) As Builder
                Me.VideoCodec = videoCodec
                Return Me
            End Function

            Public Function SetEncoderHardware(hardware As VideoEncoder.EncoderImplementation) As Builder
                Me.Hardware = hardware
                Return Me
            End Function

            Public Function SetPresetSpeed(presetSpeed As VideoEncoder.Speed) As Builder
                Me.PresetSpeed = presetSpeed
                Return Me
            End Function

            Public Function SetUseTargetBitrate(useBitrate As Boolean) As Builder
                UseVideoBitrate = useBitrate
                Return Me
            End Function

            Public Function SetVideoBitrate(bitrate As Integer) As Builder
                Me.VideoBitrate = bitrate
                Return Me
            End Function

            Public Function SetIncludeUnusedVideoSettings(UseAllSettings As Boolean) As Builder
                IncludeUnusedSettings = UseAllSettings
                Return Me
            End Function

            Public Function Build() As FfmpegSettings
                Return New FfmpegSettings(Me)
            End Function
        End Class
    End Class
End Namespace
