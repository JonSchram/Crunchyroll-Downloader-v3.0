Imports Crunchyroll_Downloader.utilities.ffmpeg.codec

Namespace utilities.ffmpeg

    Public Class SubtitleCodecArgument
        Implements ICodecArgument

        Private ReadOnly _AppliedStream As StreamSpecifier
        Public ReadOnly Codec As SubtitleCodec

        Public Sub New(stream As StreamSpecifier, codec As SubtitleCodec)
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
                Case SubtitleCodec.COPY
                    Return "copy"
                Case SubtitleCodec.ASS
                    Return "ass"
                Case SubtitleCodec.SRT
                    Return "srt"
                Case SubtitleCodec.SSA
                    Return "ssa"
                Case SubtitleCodec.MOV_TEXT
                    Return "mov_text"
                Case Else
                    Return ""
            End Select
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            Dim argument = TryCast(obj, SubtitleCodecArgument)
            Return argument IsNot Nothing AndAlso
                   Codec = argument.Codec AndAlso
                   EqualityComparer(Of StreamSpecifier).Default.Equals(AppliedStream, argument.AppliedStream)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return (Codec, AppliedStream).GetHashCode()
        End Function
    End Class
End Namespace
