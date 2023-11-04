﻿Imports Crunchyroll_Downloader.utilities.ffmpeg

Namespace utilities.ffmpeg
    Public Class FakeFfmpegAdapter
        Implements IFfmpegAdapter

        Public Property RunArguments As FfmpegArguments
        Public Event ReportProgress As IFfmpegAdapter.ReportProgressEventHandler Implements IFfmpegAdapter.ReportProgress

        Public Sub AddCookie(name As String, value As String) Implements IFfmpegAdapter.AddCookie
            Throw New NotImplementedException()
        End Sub

        Public Sub SetUserAgent(userAgent As String) Implements IFfmpegAdapter.SetUserAgent
            Throw New NotImplementedException()
        End Sub

        Public Function Run(arguments As FfmpegArguments) As Task(Of Integer) Implements IFfmpegAdapter.Run
            RunArguments = arguments
            Return Task.FromResult(0)
        End Function

    End Class
End Namespace