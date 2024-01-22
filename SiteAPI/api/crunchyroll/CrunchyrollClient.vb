Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports SiteAPI.api.common
Imports SiteAPI.api.crunchyroll.metadata
Imports SiteAPI.api.metadata

Namespace api.crunchyroll
    Public Class CrunchyrollClient
        Implements IDownloadClient

        ' TODO: Get region properly.
        Private Shared ReadOnly REGION As New Locale(Language.ENGLISH, common.Region.UNITED_STATES)

        Private ReadOnly CookieProvider As IInteractiveCookieProvider

        Private ReadOnly Authenticator As CrunchyrollAuthenticator

        Public Sub New(cookieProvider As IInteractiveCookieProvider, userAgent As String)
            Authenticator = New CrunchyrollAuthenticator(cookieProvider, userAgent)
        End Sub

        Public Async Function Initialize() As Task Implements IDownloadClient.Initialize
            Await Authenticator.Initialize()
        End Function

        Public Async Function ListSeasons(Url As String) As Task(Of IEnumerable(Of SeasonOverview)) Implements IDownloadClient.ListSeasons
            If IsSeriesUrl(Url) Then
                Dim seriesJson = Await GetSeriesJson(Url)
                ' TODO: Crunchyroll doesn't provide the series name at the series list API. Either remove property from base object
                ' or specifically retrieve it, if it is needed anywhere.
                Dim seriesInfo = CrunchyrollSeries.CreateFromSeriesJson(seriesJson, "")
                Return seriesInfo.GetSeasons()
            Else
                Throw New ArgumentException("Must provide a URL for a series")
            End If
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

        Private Async Function GetSeriesJson(SeriesUrl As String) As Task(Of String)
            Dim ShowPath = ExtractShowSlug(SeriesUrl)
            Dim JsonUrl = BuildSeasonListUrl(ShowPath, REGION)

            Dim SeriesJson = Await Authenticator.SendAuthenticatedRequest(JsonUrl)

            Debug.WriteLine("Series JSON: ")
            Debug.WriteLine(SeriesJson)
            Return SeriesJson
        End Function

        Private Function ExtractShowSlug(url As String) As String
            ' Parse the URL first in case there are query parameters.
            Dim parsedUrl = New Uri(url)
            Dim ShowPath As String = Regex.Match(parsedUrl.AbsolutePath, "/series/([^/]*)/?").Groups(1).Value
            Debug.WriteLine("Show path: " + ShowPath)
            Return ShowPath
        End Function

        Public Function IsSeriesUrl(Url As String) As Boolean Implements IDownloadClient.IsSeriesUrl
            Return SafeContains(Url, "crunchyroll.com/series")
        End Function

        Public Function IsVideoUrl(Url As String) As Boolean Implements IDownloadClient.IsVideoUrl
            Return SafeContains(Url, "crunchyroll.com/watch")
        End Function


        Private Shared Function BuildSeriesInfoUrl(seriesId As String, locale As Locale) As String
            Return $"https://www.crunchyroll.com/content/v2/cms/series/{seriesId}?locale={locale.GetAbbreviatedString()}"
        End Function

        Private Shared Function BuildSeasonListUrl(seriesId As String, locale As Locale) As String
            Return $"https://www.crunchyroll.com/content/v2/cms/series/{seriesId}/seasons?locale={locale.GetAbbreviatedString()}"
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