Imports Crunchyroll_Downloader.api
Imports Crunchyroll_Downloader.settings.funimation
Imports Crunchyroll_Downloader.settings.general

Namespace download
    Public Class PlaybackSelector
        Private Settings As ProgramSettings
        Private FunSettings As FunimationSettings

        Public Sub New()
            Settings = ProgramSettings.GetInstance()
            FunSettings = Settings.Funimation
        End Sub


        Public Function ChooseFunimationPlayback(playback As EpisodePlaybackInfo) As Playback
            Dim settingsDub As FunimationLanguage = FunSettings.DubLanguage
            Dim preferredDub = If(settingsDub = FunimationLanguage.NONE, FunimationLanguage.JAPANESE, settingsDub)

            ' TODO: Also chooose based on version when it can be selected.
            For Each fallback In playback.getFallbacks()
                Dim funLanguage = LocaleConverter.ConvertFunimationLanguageCodeToLanguage(fallback.AudioLanguage)
                If preferredDub = funLanguage Then
                    Return fallback
                End If
            Next

            Return playback.getPrimaryPlayback()
        End Function
    End Class
End Namespace