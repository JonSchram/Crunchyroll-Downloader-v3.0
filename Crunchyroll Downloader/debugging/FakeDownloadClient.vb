Imports Crunchyroll_Downloader.api
Imports Crunchyroll_Downloader.api.client

Namespace debugging
    Public Class FakeDownloadClient
        Implements IMetadataDownloader

        Private IsVideo As Boolean
        Private ep As Episode

        Public Sub New(isVideo As Boolean, ep As Episode)
            Me.IsVideo = isVideo
            Me.ep = ep
        End Sub

        Public Function Initialize() As Task Implements IMetadataDownloader.Initialize
            Throw New NotImplementedException()
        End Function

        Public Function ListSeasons(Url As String) As Task(Of IEnumerable(Of SeasonOverview)) Implements IMetadataDownloader.ListSeasons
            Throw New NotImplementedException()
        End Function

        Public Function ListEpisodes(Season As SeasonOverview) As Task(Of IEnumerable(Of EpisodeOverview)) Implements IMetadataDownloader.ListEpisodes
            Throw New NotImplementedException()
        End Function

        Public Function GetEpisodeInfo(Overview As EpisodeOverview) As Task(Of Episode) Implements IMetadataDownloader.GetEpisodeInfo
            Return New Task(Of Episode)(Function() ep)
        End Function

        Public Function GetEpisodeInfo(Url As String) As Task(Of Episode) Implements IMetadataDownloader.GetEpisodeInfo
            Return New Task(Of Episode)(Function() ep)
        End Function

        Public Function GetEpisodePlayback(ep As Episode) As Task(Of EpisodePlaybackInfo) Implements IMetadataDownloader.GetEpisodePlayback
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

        Public Function IsSeriesUrl(Url As String) As Boolean Implements IMetadataDownloader.IsSeriesUrl
            Return Not IsVideo
        End Function

        Public Function IsVideoUrl(Url As String) As Boolean Implements IMetadataDownloader.IsVideoUrl
            Return IsVideo
        End Function

        Public Function GetSiteName() As String Implements IMetadataDownloader.GetSiteName
            Return "Dummy site"
        End Function
    End Class
End Namespace