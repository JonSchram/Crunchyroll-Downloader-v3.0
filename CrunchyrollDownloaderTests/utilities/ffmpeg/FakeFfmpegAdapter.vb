Imports Crunchyroll_Downloader.utilities.ffmpeg

Namespace utilities.ffmpeg
    Public Class FakeFfmpegAdapter
        Implements IFfmpegAdapter

        Public Property RunArguments As FfmpegArguments
        Public Event ReportProgress As IFfmpegAdapter.ReportProgressEventHandler Implements IFfmpegAdapter.ReportProgress

        Public Function Run(arguments As FfmpegArguments) As Task(Of Integer) Implements IFfmpegAdapter.Run
            RunArguments = arguments
            Return Task.FromResult(0)
        End Function

    End Class
End Namespace