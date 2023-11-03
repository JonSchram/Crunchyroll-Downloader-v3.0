Imports Crunchyroll_Downloader.settings.ffmpeg.encoding

Namespace settings.ffmpeg
    Public Class FfmpegOptions
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

        Public Class Builder
            Friend Copy As Boolean
            Friend VideoCodec As Codec
            Friend Hardware As EncoderImplementation
            Friend PresetSpeed As SpeedSetting
            Friend UseVideoBitrate As Boolean = False
            Friend VideoBitrate As Integer
            Friend IncludeUnusedSettings As Boolean

            Public Function SetCopyMode(copy As Boolean) As Builder
                Me.Copy = copy
                Return Me
            End Function

            Public Function SetVideoCodec(videoCodec As Codec) As Builder
                Me.VideoCodec = videoCodec
                Return Me
            End Function

            Public Function SetEncoderHardware(hardware As EncoderImplementation) As Builder
                Me.Hardware = hardware
                Return Me
            End Function

            Public Function SetPresetSpeed(presetSpeed As SpeedSetting) As Builder
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

            Public Function Build() As FfmpegOptions
                Return New FfmpegOptions(Me)
            End Function
        End Class
    End Class
End Namespace
