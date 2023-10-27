Imports Crunchyroll_Downloader.api.common
Imports Crunchyroll_Downloader.settings.funimation

Namespace api.funimation
    Public Class FunimationDownloadPreferences
        Inherits DownloadPreferences

        Public ReadOnly Property SubtitleFormats As HashSet(Of SubFormat)
        Public Sub New(audioLanguage As Language, subtitleLanguages As IEnumerable(Of Language), media As MediaType,
                       formats As IEnumerable(Of SubFormat))
            MyBase.New(audioLanguage, subtitleLanguages, media)
            SubtitleFormats = New HashSet(Of SubFormat)(formats)
        End Sub
    End Class
End Namespace