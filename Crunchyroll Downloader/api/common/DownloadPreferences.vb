Namespace api.common
    ''' <summary>
    ''' The items that should be downloaded from the stream selector.
    ''' </summary>
    Public Class DownloadPreferences
        Public Property AudioLanguage As Language
        Public Property SubtitleLanguages As ISet(Of Language)

        ''' <summary>
        ''' A bitwise-or of each download to perform.
        ''' </summary>
        ''' <returns></returns>
        Public Property DownloadTypes As MediaType
        Public Sub New(audioLanguage As Language, subtitleLanguages As ISet(Of Language), media As MediaType)
            Me.AudioLanguage = audioLanguage
            Me.SubtitleLanguages = subtitleLanguages
            DownloadTypes = media
        End Sub
    End Class
End Namespace