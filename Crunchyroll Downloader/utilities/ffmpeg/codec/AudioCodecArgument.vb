Namespace utilities.ffmpeg.codec
    Public Class AudioCodecArgument
        Implements ICodecArgument

        Private ReadOnly _AppliedStream As StreamSpecifier
        Private ReadOnly Codec As AudioCodec

        Public Sub New(stream As StreamSpecifier, codec As AudioCodec)
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
                Case AudioCodec.COPY
                    Return "copy"
                Case AudioCodec.AAC
                    Return "aac"
                Case AudioCodec.AC3
                    Return "ac3"
                Case AudioCodec.FLAC
                    Return "flac"
                Case AudioCodec.OPUS
                    Return "libopus"
                Case Else
                    Return ""
            End Select
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace