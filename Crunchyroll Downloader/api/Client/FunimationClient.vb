Imports System.Net
Imports System.Text.RegularExpressions

Namespace api.client
    ''' <summary>
    ''' Gets information about episodes from Funimation
    ''' </summary>
    Public Class FunimationClient
        Implements IMetadataDownloader

        Private Region As String = Nothing

        ' Ideal API:
        ' - Construct using a URL
        ' - It can decide whether the URL is for a season or one episode
        ' - It can list episodes for seasons
        ' - This class should be about metadata, so maybe needs renaming.

        Public Sub New()
        End Sub


        Public Function ListSeasons(Url As String) As IEnumerable(Of SeasonOverview) Implements IMetadataDownloader.ListSeasons
            If (IsSeriesUrl(Url)) Then
                'Dim ListSeasonUrl = BuildSeasonListUrl(ShowPath)
                'Debug.WriteLine("URL to retrieve seasons: " + ListSeasonUrl)
                Dim SeriesJson = GetSeriesJson(Url)
                Dim SeriesInfo = FunimationSeries.CreateFromJson(SeriesJson)
                Return SeriesInfo.GetSeasons()
            Else
                Throw New ArgumentException("Must provide a URL for a series")
            End If
        End Function

        Private Function DownloadJson(JsonUrl As String) As String
            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend)
                    Return client.DownloadString(JsonUrl)
                End Using
            Catch ex As Exception
                Debug.WriteLine("error- getting funimation SeasonJson data")
            End Try
            ' Return parseable but empty object
            Return "{}"
        End Function

        Private Function GetSeriesJson(SeriesUrl As String) As String
            Dim ShowPath = ExtractShowSlug(SeriesUrl)
            Dim Region = GetRegion()
            Dim JsonUrl = BuildTitleInfoUrl(ShowPath, Region)

            Dim SeriesJson = DownloadJson(JsonUrl)

            Debug.WriteLine("Series JSON: ")
            Debug.WriteLine(SeriesJson)
            Return SeriesJson
        End Function

        Async Sub getCookies()
            Dim Region = GetRegion()

            Dim List = Await Browser.GetInstance().GetCookies("https://www.funimation.com")

            Dim FunimationToken As String = Nothing
            Dim FunimationDeviceRegion As String = Nothing
            Debug.WriteLine(List)
            Dim Cookie As String = ""
            ' This copies all the cookies from the web view into a URL query string
            For i As Integer = 0 To List.Count - 1
                If CBool(InStr(List.Item(i).Domain, "funimation.com")) Then 'list.Item(i).Domain = "funimation.com" Then
                    Cookie = Cookie + List.Item(i).Name + "=" + List.Item(i).Value + ";"
                End If
                If CBool(InStr(List.Item(i).Domain, "funimation.com")) And CBool(InStr(List.Item(i).Name, "src_token")) Then 'list.Item(i).Domain = "funimation.com" Then
                    FunimationToken = "Token " + List.Item(i).Value
                End If
                If CBool(InStr(List.Item(i).Domain, "funimation.com")) And CBool(InStr(List.Item(i).Name, "region")) Then 'list.Item(i).Domain = "funimation.com" Then
                    FunimationDeviceRegion = "?deviceType=web&" + List.Item(i).Name + "=" + List.Item(i).Value
                End If
            Next

        End Sub

        Private Function GetRegion() As String
            If Region Is Nothing Then
                Dim request = WebRequest.Create("http://www.funimation.com")
                Dim response = request.GetResponse()
                Dim cookieString = response.Headers.Get("Set-Cookie")
                Dim cookies = Split(cookieString, ";")

                Dim RegionCookie = cookies.Where(Function(item)
                                                     Return item.Contains("region=")
                                                 End Function)
                ' The regex should match because the filtered list contains the regex value
                Region = Regex.Match(RegionCookie.First, "region=(.*)").Groups(1).Value
            End If
            Return Region
        End Function


        Public Function ListEpisodes(Overview As SeasonOverview) As IEnumerable(Of EpisodeOverview) Implements IMetadataDownloader.ListEpisodes
            Dim SeasonUrl = BuildSeasonInfoUrl(Overview.ApiID)
            Dim SeasonJson = DownloadJson(SeasonUrl)
            Dim Season = FunimationSeason.CreateFromJson(SeasonJson)
            Dim EpisodeList = Season.GetEpisodes()
            Return EpisodeList
        End Function

        Public Function GetEpisodeInfo(Overview As EpisodeOverview) As Episode Implements IMetadataDownloader.GetEpisodeInfo
            Return GetEpisodeInfoFromId(Overview.EpisodeId)
        End Function

        Public Function GetEpisodeInfo(Url As String) As Episode Implements IMetadataDownloader.GetEpisodeInfo
            If Not IsVideoUrl(Url) Then
                Throw New ArgumentException($"Must be video URL. Received ""{Url}""")
            End If

            Return GetEpisodeInfoFromId(ExtractEpisodeSlug(Url))
        End Function

        Private Function GetEpisodeInfoFromId(Id As String) As Episode
            Dim episodeInfoUrl = BuildEpisodeInfoUrl(Id)
            Dim EpisodeJson = DownloadJson(episodeInfoUrl)
            Return FunimationEpisode.CreateFromJson(EpisodeJson)
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
            Return $"https://title-api.prd.funimationsvc.com/v2/shows/{showPath}?deviceType=web&region={Region}"
        End Function

        Private Function BuildSeasonInfoUrl(SeasonId As String) As String
            Return $"https://d33et77evd9bgg.cloudfront.net/data/v2/seasons/{SeasonId}.json"
        End Function

        Private Function BuildEpisodeInfoUrl(EpisodeId As String) As String
            Return $"https://d33et77evd9bgg.cloudfront.net/data/v2/episodes/{EpisodeId}.json"
        End Function

    End Class
End Namespace