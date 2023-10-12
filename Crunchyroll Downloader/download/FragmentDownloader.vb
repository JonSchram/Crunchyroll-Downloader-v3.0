Namespace download
    ''' <summary>
    ''' A downloader that manually gets all fragments of a playlist and only uses ffmpeg to merge them together.
    ''' </summary>
    Public Class FragmentDownloader
        Implements IPlaybackDownloader

        Public Sub DownloadPlaybacks(playbacks As List(Of Selection)) Implements IPlaybackDownloader.DownloadPlaybacks
            Throw New NotImplementedException()
        End Sub
    End Class
End Namespace