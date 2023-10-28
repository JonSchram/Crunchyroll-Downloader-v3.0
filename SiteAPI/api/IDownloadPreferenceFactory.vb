Imports SiteAPI.api.common

Namespace api
    Public Interface IDownloadPreferenceFactory
        Function GetCurrentPreferences() As DownloadPreferences
    End Interface
End Namespace