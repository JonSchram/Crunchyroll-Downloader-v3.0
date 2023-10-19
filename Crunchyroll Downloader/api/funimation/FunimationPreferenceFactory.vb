Imports Crunchyroll_Downloader.api.common
Imports Crunchyroll_Downloader.api.conversion
Imports Crunchyroll_Downloader.settings.funimation

Namespace api.funimation
    Public Class FunimationPreferenceFactory
        Implements IDownloadPreferenceFactory

        ' TODO: Pass in preferences another way so this is testable.

        Public Function GetCurrentPreferences() As DownloadPreferences Implements IDownloadPreferenceFactory.GetCurrentPreferences
            Dim funSettings = FunimationSettings.GetInstance()

            Dim audioLanguage As Language = LocaleConverter.ConvertFunimationLanguageToLanguage(funSettings.DubLanguage)
            Dim subtitleLanguages As ISet(Of Language) = ConvertLanguageList(funSettings.SoftSubtitleLanguages)
            Dim formats As ISet(Of SubFormat) = funSettings.SubtitleFormats
            Dim media As MediaType = MediaType.Audio Or MediaType.Video
            If formats.Count > 0 And subtitleLanguages.Count > 0 Then
                media = media Or MediaType.Subtitles
            End If

            Return New FunimationDownloadPreferences(audioLanguage, subtitleLanguages, media, formats)
        End Function

        Private Function ConvertLanguageList(funLanguages As ISet(Of FunimationLanguage)) As ISet(Of Language)
            Dim result As New HashSet(Of Language)
            For Each lang In funLanguages
                result.Add(LocaleConverter.ConvertFunimationLanguageToLanguage(lang))
            Next
            Return result
        End Function
    End Class
End Namespace
