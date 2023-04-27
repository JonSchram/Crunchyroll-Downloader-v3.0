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
            Dim preferredDub As FunimationLanguage = FunSettings.DubLanguage

            Dim bestMatch = playback.getPrimaryPlayback()

            ' TODO: Also chooose based on version when it can be selected.
            For Each fallback In playback.getFallbacks()
                Dim funLanguage = LocaleConverter.ConvertFunimationLanguageCodeToLanguage(fallback.AudioLanguage)
                If IsBetterLanguage(preferredDub, bestMatch, fallback) Then
                    bestMatch = fallback
                ElseIf IsBetterFileExtension(bestMatch, fallback) Then
                    bestMatch = fallback
                End If
            Next

            Return bestMatch
        End Function

        Private Function IsBetterFileExtension(CurrentBest As Playback, test As Playback) As Boolean
            Return CurrentBest.FileExtension <> "m3u8" And test.FileExtension = "m3u8"
        End Function

        Private Function IsBetterLanguage(preferredLanguage As FunimationLanguage, CurrentBest As Playback, test As Playback) As Boolean
            If preferredLanguage = FunimationLanguage.NONE Then
                ' If there is no preference, don't factor language into consideration.
                ' This forces it to choose based on the file type only
                Return False
            End If
            If preferredLanguage = LocaleConverter.ConvertFunimationLanguageCodeToLanguage(CurrentBest.AudioLanguage) Then
                ' Test if the test playback is strictly better than the current best. Equal language match defaults to current best.
                ' This is to preserve matches from a better file type.
                Return False
            End If
            If preferredLanguage = LocaleConverter.ConvertFunimationLanguageCodeToLanguage(test.AudioLanguage) Then
                ' The test file wins only if the preferred language is unequal to the current best AND the test language equals the preferred language.
                Return True
            End If
            Return False
        End Function
    End Class
End Namespace