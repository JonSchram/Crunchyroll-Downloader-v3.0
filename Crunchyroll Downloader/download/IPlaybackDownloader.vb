Imports Crunchyroll_Downloader.data

Namespace download
    Public Interface IPlaybackDownloader

        ''' <summary>
        ''' Downloads all playbacks in the list of selections.
        ''' </summary>
        ''' <param name="playbacks"></param>
        ''' <returns>A status code.</returns>
        Function DownloadPlaybacks(playbacks As List(Of Selection)) As Task(Of DownloadEntry())
    End Interface
End Namespace