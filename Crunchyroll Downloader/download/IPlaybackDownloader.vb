Imports Crunchyroll_Downloader.data

Namespace download
    Public Interface IPlaybackDownloader

        ''' <summary>
        ''' Downloads all playbacks in the list of selections.
        ''' </summary>
        ''' <param name="playbacks"></param>
        ''' <returns>A status code.</returns>
        Function DownloadSelection(playbacks As Selection) As Task(Of List(Of MediaFileEntry))
    End Interface
End Namespace