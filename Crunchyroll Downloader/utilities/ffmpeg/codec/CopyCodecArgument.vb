Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports Crunchyroll_Downloader.utilities.ffmpeg.codec

Namespace utilities
    Public Class CopyCodecArgument
        Implements ICodecArgument

        Private ReadOnly _AppliedStream As StreamSpecifier

        Public Sub New(stream As StreamSpecifier)
            _AppliedStream = stream
        End Sub

        Public Sub New()
            _AppliedStream = Nothing
        End Sub

        Public ReadOnly Property AppliedStream As StreamSpecifier Implements ICodecArgument.AppliedStream
            Get
                Return _AppliedStream
            End Get
        End Property

        Public Function GetCodecString() As String Implements ICodecArgument.GetCodecString
            Return "copy"
        End Function
    End Class
End Namespace