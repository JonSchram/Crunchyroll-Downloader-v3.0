Imports Crunchyroll_Downloader.settings.funimation
Imports SiteAPI.api
Imports SiteAPI.api.common

Namespace preferences
    Public Class FunimationPreferenceFactory
        Implements IDownloadPreferenceFactory

        ' TODO: Pass in preferences another way so this is testable.

        Public Function GetCurrentPreferences() As DownloadPreferences Implements IDownloadPreferenceFactory.GetCurrentPreferences
            Dim funSettings = FunimationSettings.GetInstance()

            Dim audioLanguage As Locale = ConvertToLocale(funSettings.DubLanguage)
            Dim subtitleLanguages As ISet(Of Locale) = ConvertLanguageList(funSettings.SoftSubtitleLanguages)
            Dim formats As IEnumerable(Of SubtitleFormat) = ConvertSubtitleSet(funSettings.SubtitleFormats, subtitleLanguages.Count > 0)
            ' TODO: Get audio-only preference. This used to be a global preference but was changed to per-episode.
            Dim media As MediaType = MediaType.Audio Or MediaType.Video
            If formats.Count > 0 And subtitleLanguages.Count > 0 Then
                media = media Or MediaType.Subtitles
            End If

            Return New DownloadPreferences(audioLanguage, subtitleLanguages, media, formats)
        End Function

        Private Function ConvertSubtitleSet(formats As ISet(Of SubFormat), downloadSoftSubs As Boolean) As IEnumerable(Of SubtitleFormat)
            If Not downloadSoftSubs Then
                Return New HashSet(Of SubtitleFormat) From {SubtitleFormat.NONE}
            End If

            If formats.Count = 0 Then
                ' Always select at least one subtitle format if soft subtitles are selected.
                Return New HashSet(Of SubtitleFormat) From {SubtitleFormat.ANY}
            End If

            Dim result = New HashSet(Of SubtitleFormat)()
            For Each f In formats
                result.Add(ConvertToSubtitleFormat(f))
            Next

            Return result
        End Function

        Private Function ConvertToSubtitleFormat(funFormat As SubFormat) As SubtitleFormat
            Select Case funFormat
                Case SubFormat.SRT
                    Return SubtitleFormat.SRT
                Case SubFormat.VTT
                    Return SubtitleFormat.VTT
                Case Else
                    Return SubtitleFormat.NONE
            End Select
        End Function

        Private Function ConvertLanguageList(funLanguages As ISet(Of FunimationLanguage)) As ISet(Of Locale)
            Dim result As New HashSet(Of Locale)
            For Each lang In funLanguages
                result.Add(ConvertToLocale(lang))
            Next
            Return result
        End Function
    End Class
End Namespace
