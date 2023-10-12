Imports Crunchyroll_Downloader.api.common

Namespace processing
    ''' <summary>
    ''' The items that should be downloaded from the stream selector.
    ''' </summary>
    Public Class DownloadPreferences
        Public Property AudioLanguage As Language
        Public Property SubtitleLanguages As List(Of Language)

        ''' <summary>
        ''' A bitwise-or of each download to perform.
        ''' </summary>
        ''' <returns></returns>
        Public Property DownloadTypes As MediaType
    End Class
End Namespace