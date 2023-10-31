Imports Crunchyroll_Downloader.data

Namespace download
    ''' <summary>
    ''' A downloader that manually gets all fragments of a playlist and only uses ffmpeg to merge them together.
    ''' </summary>
    Public Class FragmentDownloader
        Implements IPlaybackDownloader

        Public Function DownloadSelection(playbacks As Selection) As Task(Of MediaFileEntry()) Implements IPlaybackDownloader.DownloadSelection
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace