Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports System.Text.RegularExpressions
Imports Crunchyroll_Downloader.api.authentication
Imports Microsoft.Web.WebView2.Core

Namespace api.client
    ''' <summary>
    ''' Gets information about episodes from Funimation
    ''' </summary>
    Public Class FunimationClient
        Implements IMetadataDownloader

        Private ReadOnly CookieManager As CoreWebView2CookieManager

        Private Region As String = Nothing
        Private UnauthenticatedHttpClient As HttpClient
        Private Authenticator As FunimationAuthenticator
        Private PlaybackId As String


        ' Ideal API:
        ' - Construct using a URL
        ' - It can decide whether the URL is for a season or one episode
        ' - It can list episodes for seasons
        ' - This class should be about metadata, so maybe needs renaming.

        Public Sub New(CookieManager As CoreWebView2CookieManager)
            Me.CookieManager = CookieManager
            If CookieManager IsNot Nothing Then
                Authenticator = New FunimationAuthenticator(CookieManager)
            End If

            UnauthenticatedHttpClient = New HttpClient()
            UnauthenticatedHttpClient.DefaultRequestHeaders.UserAgent.ParseAdd(My.Resources.user_agent)

            PlaybackId = GenerateGuid()
        End Sub

        Private Async Function Initialize() As Task Implements IMetadataDownloader.Initialize
            Await RefreshCookies()
        End Function

        Public Async Function RefreshCookies() As Task
            If CookieManager IsNot Nothing Then
                Dim cookieList = Await CookieManager.GetCookiesAsync("https://www.funimation.com")
                For Each cookie In cookieList
                    If cookie.Name = "region" Then
                        Region = cookie.Value
                    End If
                Next
                Await Authenticator.RefreshCookies()
            End If
        End Function


        Public Async Function ListSeasons(Url As String) As Task(Of IEnumerable(Of SeasonOverview)) Implements IMetadataDownloader.ListSeasons
            If (IsSeriesUrl(Url)) Then
                'Dim ListSeasonUrl = BuildSeasonListUrl(ShowPath)
                'Debug.WriteLine("URL to retrieve seasons: " + ListSeasonUrl)
                Dim SeriesJson = Await GetSeriesJson(Url)
                Dim SeriesInfo = FunimationSeries.CreateFromJson(SeriesJson)
                Return SeriesInfo.GetSeasons()
            Else
                Throw New ArgumentException("Must provide a URL for a series")
            End If
        End Function

        Private Async Function DownloadJson(JsonUrl As String) As Task(Of String)
            Try
                Using response = Await UnauthenticatedHttpClient.GetAsync(JsonUrl)
                    response.EnsureSuccessStatusCode()
                    Return Await response.Content.ReadAsStringAsync()
                End Using
            Catch ex As Exception
                Debug.WriteLine($"Error getting funimation Json data - {JsonUrl}")
            End Try
            ' Return parseable but empty object
            Return "{}"
        End Function

        Private Async Function GetSeriesJson(SeriesUrl As String) As Task(Of String)
            Dim ShowPath = ExtractShowSlug(SeriesUrl)
            Dim JsonUrl = BuildTitleInfoUrl(ShowPath, Region)

            Dim SeriesJson = Await DownloadJson(JsonUrl)

            Debug.WriteLine("Series JSON: ")
            Debug.WriteLine(SeriesJson)
            Return SeriesJson
        End Function

        Public Async Function ListEpisodes(Overview As SeasonOverview) As Task(Of IEnumerable(Of EpisodeOverview)) Implements IMetadataDownloader.ListEpisodes
            Dim SeasonUrl = BuildSeasonInfoUrl(Overview.ApiID)
            Dim SeasonJson = Await DownloadJson(SeasonUrl)
            Dim Season = FunimationSeason.CreateFromJson(SeasonJson)
            Dim EpisodeList = Season.GetEpisodes()
            Return EpisodeList
        End Function

        Public Async Function GetEpisodeInfo(Overview As EpisodeOverview) As Task(Of Episode) Implements IMetadataDownloader.GetEpisodeInfo
            Return Await GetEpisodeInfoFromId(Overview.EpisodeId)
        End Function

        Public Async Function GetEpisodeInfo(Url As String) As Task(Of Episode) Implements IMetadataDownloader.GetEpisodeInfo
            If Not IsVideoUrl(Url) Then
                Throw New ArgumentException($"Must be video URL. Received ""{Url}""")
            End If

            Return Await GetEpisodeInfoFromId(ExtractEpisodeSlug(Url))
        End Function

        Private Async Function GetEpisodeInfoFromId(Id As String) As Task(Of Episode)
            Dim episodeInfoUrl = BuildEpisodeInfoUrl(Id)
            Dim EpisodeJson = Await DownloadJson(episodeInfoUrl)
            Return FunimationEpisode.CreateFromJson(EpisodeJson)
        End Function

        Public Async Function GetEpisodePlayback(ep As Episode) As Task(Of EpisodePlaybackInfo) Implements IMetadataDownloader.GetEpisodePlayback
            Dim url = BuildPlaybackUrl(ep)
            Dim result = Await Authenticator.SendAuthenticatedRequest(url)

            Return EpisodePlaybackInfo.CreateFromJson(result)
        End Function

        Private Function ExtractShowSlug(url As String) As String
            Dim ShowPath As String = Regex.Match(url, "/shows/(.*)/?").Groups(1).Value
            Debug.WriteLine("Show path: " + ShowPath)
            Return ShowPath
        End Function

        Private Function ExtractEpisodeSlug(url As String) As String
            Dim episodeName = Regex.Match(url, "funimation.com/v/.*/(.*)/?").Groups(1).Value
            Return episodeName
        End Function

        Public Function IsVideoUrl(Url As String) As Boolean Implements IMetadataDownloader.IsVideoUrl
            Return SafeContains(Url, "funimation.com/v/")
        End Function

        Public Function IsSeriesUrl(Url As String) As Boolean Implements IMetadataDownloader.IsSeriesUrl
            Return SafeContains(Url, "funimation.com/shows")
        End Function

        ''' <summary>
        ''' Builds a URL that will retreive a list of seasons for a given show. This is required to get more information about individual seasons.
        ''' </summary>
        ''' <param name="showPath"></param>
        ''' <param name="Region"></param>
        ''' <returns></returns>
        Private Function BuildTitleInfoUrl(showPath As String, Region As String) As String
            ' It might be possible to parse the HTML for the series page, find the app JS, and parse until you find the
            ' projectorService that serves season & episode info.
            ' The only problem is that it is highly dependent on the implementation and it would have to be hardcoded just like
            ' a URL would be.
            ' TODO: handle if the region isn't set.
            Return $"https://title-api.prd.funimationsvc.com/v2/shows/{showPath}?deviceType=web&region={Region}"
        End Function

        Private Function BuildSeasonInfoUrl(SeasonId As String) As String
            Return $"https://d33et77evd9bgg.cloudfront.net/data/v2/seasons/{SeasonId}.json"
        End Function

        Private Function BuildEpisodeInfoUrl(EpisodeId As String) As String
            Return $"https://d33et77evd9bgg.cloudfront.net/data/v2/episodes/{EpisodeId}.json"
        End Function

        Private Function BuildPlaybackUrl(ep As Episode) As String
            Return $"https://playback.prd.funimationsvc.com/v1/play/{ep.VideoId}?deviceType=web&playbackStreamId={PlaybackId}"
        End Function

        Private Function GenerateGuid() As String
            Return Guid.NewGuid().ToString()
        End Function

        Public Function GetSiteName() As String Implements IMetadataDownloader.GetSiteName
            Return "Funimation"
        End Function
    End Class
End Namespace