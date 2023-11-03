Namespace utilities.ffmpeg.codec
    Public Interface ICodecArgument
        ReadOnly Property AppliedStream As StreamSpecifier
        Function GetCodecString() As String
    End Interface
End Namespace