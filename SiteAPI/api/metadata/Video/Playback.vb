Imports Newtonsoft.Json.Linq
Namespace api.metadata.video

    ''' <summary>
    ''' A video playback option sent by the server.
    ''' </summary>
    Public Class Playback
        Public Property VideoId As String

        ' The URL that must be used to download the playlist for this video
        Public Property PlaylistPath As String

        Public Property AudioLanguage As String

        Public Property AccessType As String

        Public Property Version As String

        Public Property FileExtension As String

        Public Property Subtitles As List(Of Subtitle)

        Public Shared Function CreateFromJToken(PlaybackToken As JToken) As Playback
            Dim videoId = PlaybackToken.Item("venueVideoId")
            Dim playlistPath = PlaybackToken.Item("manifestPath")
            Dim accessTypeToken = PlaybackToken.Item("accessType")
            Dim version = PlaybackToken.Item("version")
            Dim audioLanguageToken = PlaybackToken.Item("audioLanguage")
            Dim extensionToken = PlaybackToken.Item("fileExt")

            Dim subtitlesToken = PlaybackToken.Item("subtitles")
            Dim subtitlesList = BuildSubtitles(subtitlesToken.AsEnumerable())

            Dim PlaybackObject = New Playback() With {
               .VideoId = videoId.Value(Of String),
               .PlaylistPath = playlistPath.Value(Of String),
               .Subtitles = subtitlesList,
               .AudioLanguage = audioLanguageToken.Value(Of String),
               .AccessType = accessTypeToken.Value(Of String),
               .Version = version.Value(Of String),
               .FileExtension = extensionToken.Value(Of String)
        }

            Return PlaybackObject
        End Function

        Private Shared Function BuildSubtitles(SubtitlesList As IEnumerable(Of JToken)) As List(Of Subtitle)
            Dim result = New List(Of Subtitle)
            For Each SubtitleItem In SubtitlesList
                result.Add(Subtitle.CreateFromJToken(SubtitleItem))
            Next
            Return result
        End Function


        Public Overrides Function ToString() As String
            Return $"Video ID: {VideoId}, Format: {FileExtension}, version: {Version}, Language: {AudioLanguage}, URI: {PlaylistPath}"
        End Function
    End Class
End Namespace