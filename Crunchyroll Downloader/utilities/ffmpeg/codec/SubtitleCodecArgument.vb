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
    End Class
End Namespace
