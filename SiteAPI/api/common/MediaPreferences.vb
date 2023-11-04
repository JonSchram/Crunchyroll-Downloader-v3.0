Namespace api.common
    ''' <summary>
    ''' The media items that should be downloaded from the stream selector.
    ''' </summary>
    Public Class MediaPreferences
        Public ReadOnly Property AudioLocale As Locale
        Public ReadOnly Property SubtitleLocales As ISet(Of Locale)

        Public ReadOnly Property SubtitleFormats As ISet(Of SubtitleFormat)

        ''' <summary>
        ''' A bitwise-or of each download to perform.
        ''' </summary>
        ''' <returns></returns>
        Public Property DownloadTypes As MediaType
        Public Sub New(audioLocale As Locale, subtitleLocales As IEnumerable(Of Locale), media As MediaType)
            Me.New(audioLocale, subtitleLocales, media, SubtitleFormat.ANY)
        End Sub

        Public Sub New(audioLocale As Locale, subtitleLocales As IEnumerable(Of Locale), media As MediaType,
                       format As SubtitleFormat)
            Me.New(audioLocale, subtitleLocales, media, New List(Of SubtitleFormat) From {format})
        End Sub

        Public Sub New(audioLocale As Locale, subtitleLocales As IEnumerable(Of Locale), media As MediaType,
                       formats As IEnumerable(Of SubtitleFormat))
            Me.AudioLocale = audioLocale
            Me.SubtitleLocales = New HashSet(Of Locale)(subtitleLocales)
            DownloadTypes = media
            SubtitleFormats = New HashSet(Of SubtitleFormat)(formats)
        End Sub

    End Class
End Namespace