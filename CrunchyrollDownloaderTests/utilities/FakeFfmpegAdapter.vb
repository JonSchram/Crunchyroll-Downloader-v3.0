Imports Crunchyroll_Downloader.utilities

Namespace utilities
    Public Class FakeFfmpegAdapter
        Implements IFfmpegAdapter

        Public Property RunArguments As FfmpegArguments

        Public Sub AddCookie(name As String, value As String) Implements IFfmpegAdapter.AddCookie
            Throw New NotImplementedException()
        End Sub

        Public Sub SetUserAgent(userAgent As String) Implements IFfmpegAdapter.SetUserAgent
            Throw New NotImplementedException()
        End Sub

        Public Sub Run(arguments As FfmpegArguments) Implements IFfmpegAdapter.Run
            RunArguments = arguments
        End Sub

    End Class
End Namespace