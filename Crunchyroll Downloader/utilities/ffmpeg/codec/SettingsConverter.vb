Imports Crunchyroll_Downloader.settings.ffmpeg.encoding
Imports Crunchyroll_Downloader.utilities.ffmpeg.codec
Imports Crunchyroll_Downloader.utilities.ffmpeg.preset
Imports System.ComponentModel

Namespace utilities.ffmpeg
    Public Class SettingsConverter

        Public Shared Function VideoCodecFromSettings(encoder As VideoEncoder) As VideoCodec
            If encoder Is Nothing Then
                Return VideoCodec.COPY
            End If

            Select Case encoder.VideoCodec
                Case settings.ffmpeg.encoding.Codec.AV1
                    Return SelectAv1Encoder(encoder.Hardware)
                Case settings.ffmpeg.encoding.Codec.H_264
                    Return SelectH264Encoder(encoder.Hardware)
                Case settings.ffmpeg.encoding.Codec.H_265
                    Return SelectH265Encoder(encoder.Hardware)
                Case Else
                    Throw New InvalidEnumArgumentException("Invalid enum used when selecting codec.")
            End Select
        End Function

        Private Shared Function SelectH264Encoder(implementation As EncoderImplementation) As VideoCodec
            Select Case implementation
                Case EncoderImplementation.SOFTWARE
                    Return VideoCodec.LIBX264
                Case EncoderImplementation.NVIDIA
                    Return VideoCodec.H264_NVENC
                Case EncoderImplementation.AMD
                    Return VideoCodec.H264_AMF
                Case EncoderImplementation.INTEL
                    Return VideoCodec.H264_QSV
                Case Else
                    Throw New InvalidEnumArgumentException("Invalid enum used when selecting h.264 encoder")
            End Select
        End Function
        Private Shared Function SelectH265Encoder(implementation As EncoderImplementation) As VideoCodec
            Select Case implementation
                Case EncoderImplementation.SOFTWARE
                    Return VideoCodec.LIBX265
                Case EncoderImplementation.NVIDIA
                    Return VideoCodec.HEVC_NVENC
                Case EncoderImplementation.AMD
                    Return VideoCodec.HEVC_AMF
                Case EncoderImplementation.INTEL
                    Return VideoCodec.HEVC_QSV
                Case Else
                    Throw New InvalidEnumArgumentException("Invalid enum used when selecting h.265 encoder")
            End Select
        End Function

        Private Shared Function SelectAv1Encoder(implementation As EncoderImplementation) As VideoCodec
            Select Case implementation
                Case EncoderImplementation.SOFTWARE
                    Return VideoCodec.LIBSVTAV1
                Case EncoderImplementation.NVIDIA
                    Return VideoCodec.AV1_NVENC
                Case EncoderImplementation.AMD
                    Return VideoCodec.AV1_AMF
                Case EncoderImplementation.INTEL
                    Return VideoCodec.AV1_QSV
                Case Else
                    Throw New InvalidEnumArgumentException("Invalid enum used when selecting av1 encoder")
            End Select
        End Function

        Public Shared Function PresetFromSettings(encoder As VideoEncoder) As IPreset
            If encoder Is Nothing OrElse encoder.Preset = SpeedSetting.NO_PRESET Then
                Return Nothing
            End If

            Dim s As Speed = SpeedFromSpeedSetting(encoder.Preset)
            Return New SpeedPreset(s)
        End Function

        Private Shared Function SpeedFromSpeedSetting(setting As SpeedSetting) As Speed
            Select Case setting
                Case SpeedSetting.VERY_SLOW
                    Return Speed.VERYSLOW
                Case SpeedSetting.SLOWER
                    Return Speed.SLOWER
                Case SpeedSetting.SLOW
                    Return Speed.SLOW
                Case SpeedSetting.MEDIUM
                    Return Speed.MEDIUM
                Case SpeedSetting.FAST
                    Return Speed.FAST
                Case SpeedSetting.FASTER
                    Return Speed.FASTER
                Case SpeedSetting.VERY_FAST
                    Return Speed.VERYFAST
                Case Else
                    Return Nothing
            End Select
        End Function
    End Class
End Namespace