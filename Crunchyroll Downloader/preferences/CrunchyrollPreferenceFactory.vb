Imports SiteAPI.api
Imports SiteAPI.api.common

Namespace preferences
    Public Class CrunchyrollPreferenceFactory
        Public Function GetCurrentPreferences() As MediaPreferences
            ' TODO: Create real preferences. For now, this gets everything
            Return New MediaPreferences(New Locale(Language.MULTIPLE),
                                        New List(Of Locale) From {New Locale(Language.ENGLISH)},
                                        MediaType.Audio Or MediaType.Video Or MediaType.Subtitles)
        End Function
    End Class
End Namespace