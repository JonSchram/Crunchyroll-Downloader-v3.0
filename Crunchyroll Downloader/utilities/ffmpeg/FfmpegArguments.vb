Imports Crunchyroll_Downloader.utilities.ffmpeg.codec
Imports Crunchyroll_Downloader.utilities.ffmpeg.preset

Namespace utilities.ffmpeg
    Public Class FfmpegArguments

        Public ReadOnly Property InputFiles As New List(Of String)

        Public ReadOnly Property SelectedStreams As New List(Of MapArgument)

        Public ReadOnly Property Codecs As New List(Of ICodecArgument)

        Public ReadOnly Property OutputPath As String

        Public Property Preset As PresetArgument

        Public Property UserAgent As String

        Public Property Cookies As Dictionary(Of String, String)

        Public Sub New(OutputLocation As String)
            OutputPath = OutputLocation
        End Sub

    End Class
End Namespace