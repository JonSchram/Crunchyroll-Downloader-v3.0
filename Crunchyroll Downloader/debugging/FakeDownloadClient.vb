Imports Crunchyroll_Downloader.api
Imports Crunchyroll_Downloader.api.metadata
Imports Crunchyroll_Downloader.api.metadata.video

Namespace debugging
    Public Class FakeDownloadClient
        Implements IDownloadClient

        Private IsVideo As Boolean
        Private ep As Episode

        Public Sub New(isVideo As Boolean, ep As Episode)
            Me.IsVideo = isVideo
            Me.ep = ep
        End Sub

        Public Function Initialize() As Task Implements IDownloadClient.Initialize
            Throw New NotImplementedException()
        End Function

        Public Function ListSeasons(Url As String) As Task(Of IEnumerable(Of SeasonOverview)) Implements IDownloadClient.ListSeasons
            Throw New NotImplementedException()
        End Function

        Public Function ListEpisodes(Season As SeasonOverview) As Task(Of IEnumerable(Of EpisodeOverview)) Implements IDownloadClient.ListEpisodes
            Throw New NotImplementedException()
        End Function

        Public Function GetEpisodeInfo(Overview As EpisodeOverview) As Task(Of Episode) Implements IDownloadClient.GetEpisodeInfo
            Return New Task(Of Episode)(Function() ep)
        End Function

        Public Function GetEpisodeInfo(Url As String) As Task(Of Episode) Implements IDownloadClient.GetEpisodeInfo
            Return New Task(Of Episode)(Function() ep)
        End Function

        Public Function GetEpisodePlayback(ep As Episode) As Task(Of EpisodePlaybackInfo) Implements IDownloadClient.GetEpisodePlayback
            Return Task.FromResult(New EpisodePlaybackInfo(CreateFakePlayback(ep), New List(Of Playback)))
        End Function

        Private Function CreateFakePlayback(ep As Episode) As Playback
            Return New Playback() With {
                .VideoId = ep.VideoId,
                .AudioLanguage = "Fake language",
                .FileExtension = "m3u8",
                .PlaylistPath = "https://www.example.com",
                .AccessType = "",
                .Version = "fake",
                .Subtitles = New List(Of Subtitle)
            }
        End Function

        Public Function IsSeriesUrl(Url As String) As Boolean Implements IDownloadClient.IsSeriesUrl
            Return Not IsVideo
        End Function

        Public Function IsVideoUrl(Url As String) As Boolean Implements IDownloadClient.IsVideoUrl
            Return IsVideo
        End Function

        Public Function GetSiteName() As String Implements IDownloadClient.GetSiteName
            Return "Dummy site"
        End Function

        Public Function GetStreamSelector(ep As Episode) As Task(Of IStreamSelector) Implements IDownloadClient.GetStreamSelector
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace