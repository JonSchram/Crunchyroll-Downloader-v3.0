Imports Crunchyroll_Downloader.api.conversion
Imports Crunchyroll_Downloader.api.metadata.video
Imports Crunchyroll_Downloader.processing

Namespace api.funimation
    Public Class PlaybackFilter
        Private ReadOnly Preferences As DownloadPreferences
        Public Sub New(preferences As DownloadPreferences)
            Me.Preferences = preferences
        End Sub

        Public Function GetBestPlayback(playbacks As List(Of Playback)) As Playback
            Dim preferredFunimationLanguage = LocaleConverter.ConvertLanguageToFunimationLanguage(Preferences.AudioLanguage)

            Dim bestPlayback As Playback = Nothing
            For Each playback In playbacks
                Dim currentLanguage = LocaleConverter.ConvertFunimationLanguageCodeToLanguage(playback.AudioLanguage)

                If currentLanguage = preferredFunimationLanguage Then
                    If playback.FileExtension = "m3u8" Then
                        ' 'uncut' version is usually the best version, so take it if it is available.
                        If playback.Version = "uncut" Or bestPlayback Is Nothing Then
                            bestPlayback = playback
                        End If
                    ElseIf playback.FileExtension = "mp4" And bestPlayback Is Nothing Then
                        ' I don't expect mp4 to ever be the only version, but it is an option as a fallback.
                        bestPlayback = playback
                    End If
                End If
            Next

            Return bestPlayback
        End Function
    End Class
End Namespace