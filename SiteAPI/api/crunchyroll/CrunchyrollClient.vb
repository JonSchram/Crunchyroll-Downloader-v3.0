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
                Dim seriesInfo = CrunchyrollSeasonList.CreateFromSeriesJson(seriesJson)
                Return seriesInfo.Seasons
            Else
                Throw New ArgumentException("Must provide a URL for a series")
            End If
        End Function

        Public Async Function ListEpisodes(Season As SeasonOverview) As Task(Of IEnumerable(Of EpisodeOverview)) Implements IDownloadClient.ListEpisodes
            Dim apiUrl = BuildEpisodeListUrl(Season.ApiID, REGION)
            Dim episodeListJson = Await Authenticator.SendAuthenticatedRequest(apiUrl)
            Dim episodes = CrunchyrollEpisodeList.CreateFromJson(episodeListJson)
            Return episodes.Episodes
        End Function

        Public Async Function GetEpisodeInfo(Overview As EpisodeOverview) As Task(Of Episode) Implements IDownloadClient.GetEpisodeInfo
            Return Await GetEpisodeInfoFromId(Overview.EpisodeId)
        End Function

        Public Async Function GetEpisodeInfo(Url As String) As Task(Of Episode) Implements IDownloadClient.GetEpisodeInfo
            If Not IsVideoUrl(Url) Then
                Throw New ArgumentException($"Must be a video URL. Received ""{Url}""")
            End If

            Return Await GetEpisodeInfoFromId(ExtractEpisodeSlug(Url))
        End Function

        Private Async Function GetEpisodeInfoFromId(Id As String) As Task(Of Episode)
            Dim episodeInfoUrl = BuildEpisodeInfoUrl(Id, REGION)
            Dim episodeJson = Await Authenticator.SendAuthenticatedRequest(episodeInfoUrl)
            Return CrunchyrollEpisode.CreateFromJson(episodeJson)
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

        Private Function ExtractEpisodeSlug(url As String) As String
            Dim episodeUri As New Uri(url)
            Dim episodeId = Regex.Match(episodeUri.AbsolutePath, "watch/([^/]*)/?").Groups(1).Value
            Return episodeId
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

        Private Shared Function BuildEpisodeListUrl(seasonId As String, locale As Locale) As String
            Return $"https://www.crunchyroll.com/content/v2/cms/seasons/{seasonId}/episodes?locale={locale.GetAbbreviatedString()}"
        End Function

        Private Shared Function BuildEpisodeInfoUrl(episodeId As String, locale As Locale) As String
            Return $"https://www.crunchyroll.com/content/v2/cms/objects/{episodeId}?ratings=true&locale={locale.GetAbbreviatedString()}"
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

        Public Async Function GetAvailableMedia(ep As Episode, preferences As MediaPreferences) As Task(Of List(Of MediaLink)) Implements IDownloadClient.GetAvailableMedia
            If preferences Is Nothing Then
                Throw New Exception("Must set media preferences.")
            End If
            ' TODO: Check if the stream is free and whether the user is logged in.

            Dim streams As StreamsResult = Await GetStreams(ep)
            Return Nothing
        End Function

        Private Async Function GetStreams(ep As CrunchyrollEpisode) As Task(Of StreamsResult)
            Dim url = BuildStreamsUrl(ep.StreamLink, REGION)
            Dim streamJson As String = Await Authenticator.SendAuthenticatedRequest(url)
            Return StreamsResult.CreateFromJson(streamJson)
        End Function

        Private Function BuildStreamsUrl(streamsPath As String, locale As Locale) As String
            Dim b = New UriBuilder("https", "www.crunchyroll.com") With {
                .Path = streamsPath,
                .Query = $"locale={locale.GetAbbreviatedString()}"
            }
            Return b.ToString()
        End Function

    End Class
End Namespace