Imports Crunchyroll_Downloader.api.metadata.video
Imports Crunchyroll_Downloader.processing
Imports Crunchyroll_Downloader.settings.funimation
Imports Crunchyroll_Downloader.settings.general

Namespace api.client.stream
    Public Class PlaybackSelector
        Private playback As EpisodePlaybackInfo

        Public Sub New(playback As EpisodePlaybackInfo)
            Me.playback = playback
        End Sub


        Public Function ChooseFunimationPlayback(AudioLanguage As Language) As Playback
            Dim preferredLanguage = LocaleConverter.ConvertLanguageToFunimationLanguage(AudioLanguage)
            Dim bestMatch = playback.getPrimaryPlayback()
            Dim bestLanguage = LocaleConverter.ConvertFunimationLanguageCodeToLanguage(bestMatch.AudioLanguage)

            ' TODO: Also chooose based on version when it can be selected.
            For Each fallback In playback.getFallbacks()
                Dim fallbackLanguage = LocaleConverter.ConvertFunimationLanguageCodeToLanguage(fallback.AudioLanguage)
                If IsBetterLanguage(preferredLanguage, bestLanguage, fallbackLanguage) Then
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

        Private Function IsBetterLanguage(preferredLanguage As FunimationLanguage, currentBest As FunimationLanguage,
                                          testLanguage As FunimationLanguage) As Boolean
            If preferredLanguage = FunimationLanguage.NONE Then
                ' If there is no preference, don't factor language into consideration.
                ' This forces it to choose based on the file type only
                Return False
            End If
            If preferredLanguage = currentBest Then
                ' Test if the test playback is strictly better than the current best. Equal language match defaults to current best.
                ' This is to preserve matches from a better file type.
                Return False
            End If
            If preferredLanguage = testLanguage Then
                ' The test file wins only if the preferred language is unequal to the current best AND the test language equals the preferred language.
                Return True
            End If
            Return False
        End Function
    End Class
End Namespace