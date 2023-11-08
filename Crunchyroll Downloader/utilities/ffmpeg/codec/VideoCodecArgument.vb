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