Imports SiteAPI.api
Imports SiteAPI.api.common
Imports SiteAPI.api.metadata

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

        Public Function IsSeriesUrl(Url As String) As Boolean Implements IDownloadClient.IsSeriesUrl
            Return Not IsVideo
        End Function

        Public Function IsVideoUrl(Url As String) As Boolean Implements IDownloadClient.IsVideoUrl
            Return IsVideo
        End Function

        Public Function GetSiteName() As String Implements IDownloadClient.GetSiteName
            Return "Dummy site"
        End Function

        Public Function ResolveMediaLink(link As MediaLink) As Task(Of Media) Implements IDownloadClient.ResolveMediaLink
            Throw New NotImplementedException()
        End Function

        Public Function GetAvailableMedia(ep As Episode, preferences As MediaPreferences) As Task(Of List(Of MediaLink)) Implements IDownloadClient.GetAvailableMedia
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace