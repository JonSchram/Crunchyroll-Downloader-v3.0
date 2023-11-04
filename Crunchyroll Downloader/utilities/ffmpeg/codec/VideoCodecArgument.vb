Imports System.ComponentModel
Imports Crunchyroll_Downloader.settings.ffmpeg.encoding

Namespace utilities.ffmpeg.codec
    Public Class VideoCodecArgument
        Implements ICodecArgument

        Private ReadOnly _AppliedStream As StreamSpecifier
        Public ReadOnly Codec As VideoCodec

        Public Sub New(stream As StreamSpecifier, codec As VideoCodec)
            _AppliedStream = stream
            Me.Codec = codec
        End Sub

        Public ReadOnly Property AppliedStream As StreamSpecifier Implements ICodecArgument.AppliedStream
            Get
                Return _AppliedStream
            End Get
        End Property

        Public Function GetCodecString() As String Implements ICodecArgument.GetCodecString
            Select Case Codec
                Case VideoCodec.COPY
                    Return "copy"
                Case VideoCodec.LIBX264
                    Return "libx264"
                Case VideoCodec.LIBX265
                    Return "libx265"
                Case VideoCodec.LIBXAVS2
                    Return "libxavs2"
                Case VideoCodec.LIBSVTAV1
                    Return "libsvtav1"
                Case VideoCodec.H264_NVENC
                    Return "h264_nvenc"
                Case VideoCodec.HEVC_NVENC
                    Return "hevc_nvenc"
                Case VideoCodec.AV1_NVENC
                    Return "av1_nvenc"
                Case VideoCodec.H264_AMF
                    Return "h264_amf"
                Case VideoCodec.HEVC_AMF
                    Return "hevc_amf"
                Case VideoCodec.AV1_AMF
                    Return "av1_amf"
                Case VideoCodec.H264_QSV
                    Return "h264_qsv"
                Case VideoCodec.HEVC_QSV
                    Return "hevc_qsv"
                Case VideoCodec.AV1_QSV
                    Return "av1_qsv"
                Case Else
                    Return ""
            End Select
        End Function

        Public Shared Function CodecFromEncoderSettings(encoder As VideoEncoder) As VideoCodec
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

        Public Overrides Function Equals(obj As Object) As Boolean
            Dim argument = TryCast(obj, VideoCodecArgument)
            Return argument IsNot Nothing AndAlso
                   Codec = argument.Codec AndAlso
                   EqualityComparer(Of StreamSpecifier).Default.Equals(AppliedStream, argument.AppliedStream)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return (Codec, AppliedStream).GetHashCode()
        End Function
    End Class
End Namespace