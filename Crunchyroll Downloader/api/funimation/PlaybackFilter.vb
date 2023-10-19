Imports Crunchyroll_Downloader.api.common
Imports Crunchyroll_Downloader.api.conversion
Imports Crunchyroll_Downloader.api.metadata.video
Imports Crunchyroll_Downloader.settings.funimation

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

                If currentLanguage = preferredFunimationLanguage Or preferredFunimationLanguage = FunimationLanguage.NONE Then
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

        ''' <summary>
        ''' Gets all media streams from the playback that match the preferences.
        ''' </summary>
        ''' <param name="p"></param>
        Public Function GetMatchingMedia(p As Playback) As List(Of MediaLink)
            If TypeOf Preferences IsNot FunimationDownloadPreferences Then
                Throw New Exception("Not using correct download preferences for funimation playback filter.")
            End If

            Dim funimationPreferences = CType(Preferences, FunimationDownloadPreferences)
            Dim media As New List(Of MediaLink)
            If Preferences.DownloadTypes.HasFlag(MediaType.Video) Or Preferences.DownloadTypes.HasFlag(MediaType.Audio) Then
                media.Add(CreateMediaFromManifestPath(p))
            End If

            If Preferences.DownloadTypes.HasFlag(MediaType.Subtitles) And Preferences.SubtitleLanguages?.Count > 0 Then
                For Each subtitle In p.Subtitles
                    Dim language As Language = LocaleConverter.ConvertFunimationLanguageCodeToUniversalLanguage(subtitle.Language)
                    Dim format As SubFormat = FormatConverter.ConvertFormatStringToSubtitleFormat(subtitle.Format)
                    If Preferences?.SubtitleLanguages.Contains(language) And
                            funimationPreferences.SubtitleFormats.Contains(format) Then
                        media.Add(New FileMediaLink(MediaType.Subtitles, language, subtitle.Path))
                    End If
                Next
            End If

            Return media
        End Function

        Private Function CreateMediaFromManifestPath(p As Playback) As MediaLink
            Dim language = LocaleConverter.ConvertFunimationLanguageCodeToUniversalLanguage(p.AudioLanguage)
            Dim mediaFlags = MediaType.Audio Or MediaType.Video
            If p.FileExtension = "m3u8" Then
                Return New HlsMasterPlaylistLink(mediaFlags, language, p.PlaylistPath)
            Else
                ' Mp4 or an unknown file type. Add as a file media.
                Return New FileMediaLink(mediaFlags, language, p.PlaylistPath)
            End If
        End Function
    End Class
End Namespace