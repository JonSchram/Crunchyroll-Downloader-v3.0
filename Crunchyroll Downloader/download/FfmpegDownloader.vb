Namespace download

    ''' <summary>
    ''' A downloader that is backed by ffmpeg.
    ''' </summary>
    Public Class FfmpegDownloader
        Inherits AbstractPlaybackDownloader
        Implements IPlaybackDownloader

        Public Sub New(tempDir As String, finalDir As String)
            MyBase.New(tempDir, finalDir)
        End Sub

        Public Overrides Sub DownloadPlaybacks(playbacks As List(Of Playback))
            Throw New NotImplementedException()
        End Sub
    End Class
End Namespace
