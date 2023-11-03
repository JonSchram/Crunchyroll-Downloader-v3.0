Imports Crunchyroll_Downloader.utilities.ffmpeg.codec

Namespace utilities.ffmpeg
    Partial Public Class FfmpegArguments

        Public ReadOnly Property InputFiles As New List(Of String)

        Public ReadOnly Property SelectedStreams As New List(Of MapArgument)

        Public ReadOnly Property Codecs As New List(Of ICodecArgument)

        Public ReadOnly Property OutputPath As String

        Public Sub New(OutputLocation As String)
            OutputPath = OutputLocation
        End Sub

    End Class
End Namespace