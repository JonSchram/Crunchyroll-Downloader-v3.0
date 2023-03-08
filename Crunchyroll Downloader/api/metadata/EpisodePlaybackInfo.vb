Imports Newtonsoft.Json.Linq

Public Class EpisodePlaybackInfo

    Private Property Primary As Playback

    Private Property Fallbacks As List(Of Playback)

    Public Sub New(Primary As Playback, Fallbacks As List(Of Playback))
        Me.Primary = Primary
        Me.Fallbacks = Fallbacks
    End Sub

    Public Function getPrimaryPlayback() As Playback
        Return Primary
    End Function

    Public Function getFallbacks() As List(Of Playback)
        Return Fallbacks
    End Function

    Public Shared Function CreateFromJson(Json As String) As EpisodePlaybackInfo
        Dim playbackObject = JObject.Parse(Json)

        Dim primaryPlayback = playbackObject.Item("primary")
        Dim fallbackPlaybacks = playbackObject.Item("fallback").ToList

        Dim fallbackList As New List(Of Playback)
        For Each fallback As JToken In fallbackPlaybacks
            fallbackList.Add(BuildPlayback(fallback))
        Next

        Return New EpisodePlaybackInfo(BuildPlayback(primaryPlayback), fallbackList)
    End Function


    Private Shared Function BuildPlayback(playbackToken As JToken) As Playback
        Dim videoId = playbackToken.Item("venueVideoId")
        Dim playlistPath = playbackToken.Item("manifestPath")
        Dim subtitlesToken = playbackToken.Item("subtitles")
        Dim subtitlesList = BuildSubtitles(subtitlesToken.AsEnumerable())

        Dim PlaybackObject = New Playback() With {
               .VideoId = videoId.Value(Of String),
               .PlaylistPath = playlistPath.Value(Of String),
               .Subtitles = subtitlesList
        }

        Return PlaybackObject
    End Function

    Private Shared Function BuildSubtitles(SubtitlesList As IEnumerable(Of JToken)) As List(Of Subtitle)
        Dim result = New List(Of Subtitle)
        For Each Subtitle In SubtitlesList
            Dim path = Subtitle.Item("filePath")
            Dim language = Subtitle.Item("languageCode")
            Dim format = Subtitle.Item("fileExt")

            result.Add(New Subtitle() With {
            .Path = path.Value(Of String),
            .Format = format.Value(Of String),
            .Language = language.Value(Of String)
            })
        Next
        Return result
    End Function

End Class
