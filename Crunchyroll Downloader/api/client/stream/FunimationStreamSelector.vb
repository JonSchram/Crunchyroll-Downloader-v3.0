Imports Crunchyroll_Downloader.api.metadata.video
Imports Crunchyroll_Downloader.processing
Imports Crunchyroll_Downloader.settings.funimation

Namespace api.client.stream
    Public Class FunimationStreamSelector
        Implements IStreamSelector

        Private EpisodePlayback As EpisodePlaybackInfo
        Private selector As PlaybackSelector

        Public Sub New(episodePlayback As EpisodePlaybackInfo)
            Me.EpisodePlayback = episodePlayback
            selector = New PlaybackSelector(episodePlayback)
        End Sub


        Public Function GetStreams(type As MediaType, langauges As List(Of Language)) As List(Of MediaStream) Implements IStreamSelector.GetStreams
            Throw New NotImplementedException()
        End Function

        Public Function GetStreams(audioLanguage As Language, subtitleLanguages As List(Of Language), streamTypeFlags As MediaType) As List(Of MediaStream) Implements IStreamSelector.GetStreams
            Dim bestPlayback = selector.ChooseFunimationPlayback(audioLanguage)

            Dim obtainedPlaybacks As MediaType = Nothing
            Dim streams As New List(Of MediaStream)

            If streamTypeFlags.HasFlag(MediaType.subtitles) Then
                Dim subResult = GetSubtitles(bestPlayback, subtitleLanguages)
                streams.AddRange(subResult.FoundMedia)
                obtainedPlaybacks = obtainedPlaybacks Or subResult.MediaTypes
            End If

            If streamTypeFlags.HasFlag(MediaType.Video) And
                Not obtainedPlaybacks.HasFlag(MediaType.Video) Then

                ' Problem: at this stage, it assumes you want to get a stream
                ' If using the ffmpeg downloader, it will want the raw file. Can it create a stream
                ' without doing too much work that will be thrown away?

                obtainedPlaybacks = obtainedPlaybacks Or MediaType.Video
            End If

            If streamTypeFlags.HasFlag(MediaType.Audio) And
                Not obtainedPlaybacks.HasFlag(MediaType.Audio) Then

                ' Problem: HLS file specifies alternate versions and those point to an audio rendition.
                ' Can't really get the audio separately and comply with the spec.
                obtainedPlaybacks = obtainedPlaybacks Or MediaType.Audio
            End If

            Return Nothing
        End Function

        Private Function GetSubtitles(playback As Playback, subLanguages As List(Of Language)) As MediaResult
            Dim subtitleList As New List(Of MediaStream)
            For Each subtitleInfo In playback.Subtitles
                Dim parsedLanguage As FunimationLanguage =
                    LocaleConverter.ConvertFunimationLanguageCodeToLanguage(subtitleInfo.Language)
                If LocaleConverter.ListContainsFunimationLanguage(parsedLanguage, subLanguages) Then
                    Dim subtitleStream = New FileMedia(MediaType.Subtitles, subtitleInfo.Path)
                    subtitleList.Add(subtitleStream)
                End If
            Next

            Return New MediaResult() With {
                .MediaTypes = MediaType.Subtitles,
                .FoundMedia = subtitleList
            }
        End Function

        Private Sub GetAudio(playback As Playback)

        End Sub

        Private Sub GetVideo(playback As Playback)

        End Sub

        Private Class MediaResult
            Public Property MediaTypes As MediaType
            Public Property FoundMedia As List(Of MediaStream)
        End Class
    End Class
End Namespace