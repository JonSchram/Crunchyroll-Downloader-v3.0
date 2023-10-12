Imports Crunchyroll_Downloader.api.common

Namespace processing
    ' TODO: Put this in a more appropriate location.
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

        Public Property TempFolder As String
        Public Property OutputFolder As String

        ' TODO: Use correct type. This is here for now as a reminder that this is needed.
        Public Property NameTemplate As String
    End Class
End Namespace