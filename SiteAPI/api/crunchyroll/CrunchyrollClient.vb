Imports SiteAPI.api.common
Imports SiteAPI.api.metadata

Namespace api.crunchyroll
    Public Class CrunchyrollClient
        Implements IDownloadClient

        Private ReadOnly CookieProvider As IInteractiveCookieProvider

        Private ReadOnly Authenticator As CrunchyrollAuthenticator

        Public Sub New(cookieProvider As IInteractiveCookieProvider, userAgent As String)
            Authenticator = New CrunchyrollAuthenticator(cookieProvider, userAgent)
        End Sub

        Public Async Function Initialize() As Task Implements IDownloadClient.Initialize
            Await Authenticator.Initialize()
        End Function

        Public Function ListSeasons(Url As String) As Task(Of IEnumerable(Of SeasonOverview)) Implements IDownloadClient.ListSeasons
            Throw New NotImplementedException()
        End Function

        Public Function ListEpisodes(Season As SeasonOverview) As Task(Of IEnumerable(Of EpisodeOverview)) Implements IDownloadClient.ListEpisodes
            Throw New NotImplementedException()
        End Function

        Public Function GetEpisodeInfo(Overview As EpisodeOverview) As Task(Of Episode) Implements IDownloadClient.GetEpisodeInfo
            Throw New NotImplementedException()
        End Function

        Public Function GetEpisodeInfo(Url As String) As Task(Of Episode) Implements IDownloadClient.GetEpisodeInfo
            Throw New NotImplementedException()
        End Function

        Public Function IsSeriesUrl(Url As String) As Boolean Implements IDownloadClient.IsSeriesUrl
            Throw New NotImplementedException()
        End Function

        Public Function IsVideoUrl(Url As String) As Boolean Implements IDownloadClient.IsVideoUrl
            Throw New NotImplementedException()
        End Function


        Private Shared Function BuildSeriesInfoUrl(seriesId As String, locale As Locale) As String
            Return $"https://www.crunchyroll.com/content/v2/cms/series/{seriesId}?locale={locale.GetAbbreviatedString()}"
        End Function


        Public Function GetSiteName() As String Implements IDownloadClient.GetSiteName
            Return "Crunchyroll"
        End Function


        Public Function GetSite() As Site Implements IDownloadClient.GetSite
            Return Site.CRUNCHYROLL
        End Function


        Public Function ResolveMediaLink(link As MediaLink) As Task(Of Media) Implements IDownloadClient.ResolveMediaLink
            Throw New NotImplementedException()
        End Function

        Public Function GetAvailableMedia(ep As Episode, preferences As MediaPreferences) As Task(Of List(Of MediaLink)) Implements IDownloadClient.GetAvailableMedia
            Throw New NotImplementedException()
        End Function

    End Class
End Namespace