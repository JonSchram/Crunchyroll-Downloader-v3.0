Imports Newtonsoft.Json.Linq

' TODO: This is designed specifically for Funimation. It isn't clear that Crunchyroll would even have an equivalent
' file, so this and the API need to go through some additional design.
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
        ' TODO: Handle invalid responses because of failure to retrieve playback
        ' (Most likely because of premium subscription requirement)
        Dim playbackObject = JObject.Parse(Json)

        Dim primaryPlayback = playbackObject.Item("primary")
        Dim fallbackPlaybacks = playbackObject.Item("fallback").ToList

        Dim fallbackList As New List(Of Playback)
        For Each fallback As JToken In fallbackPlaybacks
            fallbackList.Add(Playback.CreateFromJToken(fallback))
        Next

        Return New EpisodePlaybackInfo(Playback.CreateFromJToken(primaryPlayback), fallbackList)
    End Function
End Class
