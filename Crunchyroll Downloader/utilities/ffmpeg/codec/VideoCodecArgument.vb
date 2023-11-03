Namespace utilities.ffmpeg.codec
    Public Class VideoCodecArgument
        Implements ICodecArgument

        Private ReadOnly _AppliedStream As StreamSpecifier
        Private ReadOnly Codec As VideoCodec

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
                Case VideoCodec.H264_NVENC
                    Return "h264_nvenc"
                Case VideoCodec.HEVC_NVENC
                    Return "hevc_nvenc "
                Case VideoCodec.H264_AMF
                    Return "h264_amf"
                Case VideoCodec.HEVC_AMF
                    Return "hevc_amf"
                Case Else
                    Return ""
            End Select
        End Function
    End Class
End Namespace