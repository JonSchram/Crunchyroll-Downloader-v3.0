Namespace download
    ''' <summary>
    ''' A downloader that manually gets all fragments of a playlist and only uses ffmpeg to merge them together.
    ''' </summary>
    Public Class FragmentDownloader
        Implements IPlaybackDownloader

        Private Function IPlaybackDownloader_DownloadPlaybacks(playbacks As List(Of Selection)) As Task(Of Integer) Implements IPlaybackDownloader.DownloadPlaybacks
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace