Namespace api.common
    ''' <summary>
    ''' The items that should be downloaded from the stream selector.
    ''' </summary>
    Public Class DownloadPreferences
        Public ReadOnly Property AudioLocale As Locale
        Public ReadOnly Property SubtitleLanguages As ISet(Of Language)

        Public ReadOnly Property SubtitleFormats As ISet(Of SubtitleFormat)

        ''' <summary>
        ''' A bitwise-or of each download to perform.
        ''' </summary>
        ''' <returns></returns>
        Public Property DownloadTypes As MediaType
        Public Sub New(audioLocale As Locale, subtitleLanguages As IEnumerable(Of Language), media As MediaType)
            Me.New(audioLocale, subtitleLanguages, media, SubtitleFormat.ANY)
        End Sub

        Public Sub New(audioLocale As Locale, subtitleLanguages As IEnumerable(Of Language), media As MediaType,
                       format As SubtitleFormat)
            Me.New(audioLocale, subtitleLanguages, media, New List(Of SubtitleFormat) From {format})
        End Sub

        Public Sub New(audioLocale As Locale, subtitleLanguages As IEnumerable(Of Language), media As MediaType,
                       formats As IEnumerable(Of SubtitleFormat))
            Me.AudioLocale = audioLocale
            Me.SubtitleLanguages = New HashSet(Of Language)(subtitleLanguages)
            DownloadTypes = media
            SubtitleFormats = New HashSet(Of SubtitleFormat)(formats)
        End Sub

    End Class
End Namespace