Imports System.Diagnostics.Eventing
Imports System.Net
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
''' <summary>
''' Gets information about episodes from Funimation
''' </summary>
Public Class FunimationExtractor
    Implements IMetadataDownloader

    Private downloadUrl As String = Nothing

    Private apiUrl As String = Nothing

    Private Region As String = Nothing

    ' Ideal API:
    ' - Construct using a URL
    ' - It can decide whether the URL is for a season or one episode
    ' - It can list episodes for seasons
    ' - This class should be about metadata, so maybe needs renaming.

    ' This API is feeling inconsistent, holding onto a download URL  but requiring the API user to pass
    ' the correct season or episode ID? 
    ' Maybe pass the API response back to further API methods?
    ' Still feels a little weird, because nothing in this class requires any state.
    ' AND if you need to go back and get more details about an episode, you can't do it
    ' unless you have a URL, and then you never need that URL ever again

    Public Sub New(downloadUrl As String)
        Me.downloadUrl = downloadUrl
    End Sub


    Public Function ListSeasons() As IEnumerable(Of SeasonOverview) Implements IMetadataDownloader.ListSeasons
        Main.Navigate(downloadUrl)
        If (IsSeriesUrl()) Then
            'Dim ListSeasonUrl = BuildSeasonListUrl(ShowPath)
            'Debug.WriteLine("URL to retrieve seasons: " + ListSeasonUrl)
            Dim SeriesJson = GetSeriesJson(downloadUrl)
            Dim SeriesInfo = FunimationSeries.CreateFromJson(SeriesJson)
            Return SeriesInfo.GetSeasons()
        End If
        Return New List(Of FunimationSeasonOverview)
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
            ' Main.Navigate needed?
        End Try
        ' Return parseable but empty object
        Return "{}"
    End Function

    Private Function GetSeriesJson(SeriesUrl As String) As String
        Dim ShowPath As String = Regex.Match(downloadUrl, "/shows/(.*)/?").Groups(1).Value
        Debug.WriteLine("Show path: " + ShowPath)
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


    Public Function ListEpisodes(SeasonName As String) As IEnumerable(Of EpisodeOverview) Implements IMetadataDownloader.ListEpisodes
        ' Implement this now.
        ' Might need to retrieve https://d33et77evd9bgg.cloudfront.net/data/v2/seasons/ first to get season info
        ' After, use https://d33et77evd9bgg.cloudfront.net/data/v2/episodes to get episode info
        ' Can access season info by taking the contentId from title-api and adding it to end of /v2/seasons/, adding .json

        Dim SeasonUrl = BuildSeasonInfoUrl(SeasonName)
        Dim SeasonJson = DownloadJson(SeasonUrl)
        Dim Season = FunimationSeason.CreateFromJson(SeasonJson)
        Dim EpisodeList = Season.GetEpisodes()
        Return EpisodeList
    End Function

    ' TODO: Get info for a single episode.
    ' Definitely need to get the path to the video playlist
    ' Probably want the episode name for name formatting after the download
    ' Might want the list of langauges for audio & subtitles.
    ' Not sure about anything else, need to see what the existing code gets.
    ' Checked with existing code, want to get:
    ' - Images for the episode ("images" in episode JSON)
    '    - Either key art or episode thumbnail
    ' - Name
    ' - ID
    ' - Episode Number
    '    - Seems that for extras, it's possible there is only an English name
    '    - Number seems to exist (but for extras is usually 99)
    ' - Season
    ' - Show (only English version, but could get others)
    '
    ' If it retrieves the show name as well, this could make it much easier to rename the file afterwards.
    ' Each download task has everything it needs in the episode object to name the file.


    ' TODO: This assumes that you are getting the episode info from the results of the season API.
    ' Need a way to get episode info from the episode URL
    ' This class has a download URL as a class member so maybe that is good enough

    Public Function getEpisodeInfo(EpisodeId As String) As Episode Implements IMetadataDownloader.getEpisodeInfo
        Dim infoUrl = BuildEpisodeInfoUrl(EpisodeId)
        Dim EpisodeInfoJson = DownloadJson(infoUrl)
        Dim EpisodeInfo = FunimationEpisode.CreateFromJson(EpisodeInfoJson)

        Return EpisodeInfo
    End Function

    Public Function getEpisodeInfo() As Episode Implements IMetadataDownloader.getEpisodeInfo
        If Not IsVideoUrl() Then
            Return Nothing
        End If

        Dim episodeSlug = extractEpisodeSlug(downloadUrl)
        Dim episodeInfoUrl = BuildEpisodeInfoUrl(episodeSlug)
        Dim EpisodeJson = DownloadJson(episodeInfoUrl)
        Return FunimationEpisode.CreateFromJson(EpisodeJson)
    End Function

    Private Function extractEpisodeSlug(url As String) As String
        Dim episodeName = Regex.Match(url, "funimation.com/v/.*/(.*)/?").Groups(1).Value
        Return episodeName
    End Function

    Public Function IsVideoUrl() As Boolean Implements IMetadataDownloader.IsVideoUrl
        Return SafeContains(downloadUrl, "funimation.com/v/")
    End Function

    Private Function IsSeriesUrl() As Boolean
        Return SafeContains(downloadUrl, "funimation.com/shows")
    End Function

    ''' <summary>
    ''' Builds a URL that will retreive a list of seasons for a given show. This is required to get more information about individual seasons.
    ''' </summary>
    ''' <param name="showPath"></param>
    ''' <param name="Region"></param>
    ''' <returns></returns>
    Private Function BuildTitleInfoUrl(showPath As String, Region As String) As String
        ' TODO: Probably want all URLs in one class for funimation so that if they change, it's easy to update
        ' The alternative is to parse the HTML for the series page, find the app JS, and parse until you find the
        ' projectorService that serves season & episode info. Maybe do this and have a hardcoded path as a fallback?
        Dim TemplateUrl = $"https://title-api.prd.funimationsvc.com/v2/shows/{showPath}?deviceType=web&region={Region}"
        Return TemplateUrl
    End Function

    Private Function BuildSeasonInfoUrl(SeasonId As String) As String
        Dim TemplateUrl = $"https://d33et77evd9bgg.cloudfront.net/data/v2/seasons/{SeasonId}.json"
        Return TemplateUrl
    End Function

    Private Function BuildEpisodeInfoUrl(EpisodeId As String) As String
        Dim TemplateUrl = $"https://d33et77evd9bgg.cloudfront.net/data/v2/episodes/{EpisodeId}.json"
        Return TemplateUrl
    End Function


    ' ------- EVERYTHING BELOW THIS LINE IS BASED ON ORIGINAL CODE --------
    ' Organization is not very good so it is just for reference
    ' It should be re-written or sparingly copied from in case it does exactly what is needed in the new design

    Public Function extractEpisodesFromJson(ByVal EpisodeJson As String) As List(Of String)
        Dim EpisodeSplit() As String = EpisodeJson.Split(New String() {"""episodeNumber"":"""}, System.StringSplitOptions.RemoveEmptyEntries)
        ' TODO: Use JSON library to do this in the future, like: JsonConvert.DeserializeObject(EpisodeJson)
        ' Or maybe JObject.parse
        Debug.WriteLine(EpisodeSplit.Count.ToString)
        Dim ReturnValue As New List(Of String)
        For i As Integer = 1 To EpisodeSplit.Count - 1
            Dim EpisodeSplit2() As String = EpisodeSplit(i).Split(New String() {""""}, System.StringSplitOptions.RemoveEmptyEntries)
            ReturnValue.Add(EpisodeSplit2(0))
        Next
        'Main.WebbrowserURL = "https://funimation.com/js"
        Return ReturnValue
    End Function

    Public Function getFunimationEpisodesJson(season As FunimationOverview) As String
        Dim ContentID As String = season.ID

        If ContentID = Nothing Then
            MsgBox("error during season selection")
            Return Nothing
        End If

        Dim BaseUrl() As String = apiUrl.Split(New String() {"/shows/"}, System.StringSplitOptions.RemoveEmptyEntries)

        Dim EpisodeJsonURL As String = BaseUrl(0) + "/seasons/" + ContentID + ".json"
        Dim EpisodeJson As String = Nothing
        Debug.WriteLine(EpisodeJsonURL)

        Try
            Using client As New WebClient()
                client.Encoding = System.Text.Encoding.UTF8
                client.Headers.Add(My.Resources.ffmpeg_user_agend)
                EpisodeJson = client.DownloadString(EpisodeJsonURL)
            End Using
        Catch ex As Exception
            Debug.WriteLine("error- getting EpisodeJson data")
            Debug.WriteLine(ex.ToString)
            Main.LoadBrowser(EpisodeJsonURL)
            Return Nothing
        End Try

        Return EpisodeJson
    End Function





    ' Unused, so won't deal with it for now
    'Public Sub ProcessFunimationJS(ByVal InputURL As String)
    '    Dim FunUri As String = Nothing
    '    If CBool(InStr(InputURL, "?")) Then
    '        Dim ClearUri As String() = InputURL.Split(New String() {"?"}, System.StringSplitOptions.RemoveEmptyEntries)
    '        FunUri = ClearUri(0)
    '    Else
    '        FunUri = InputURL
    '    End If
    '    Dim ShowPath As String = Nothing
    '    Dim EpisodePath As String = Nothing
    '    Dim ShowPath1 As String() = FunUri.Split(New String() {"/shows/"}, System.StringSplitOptions.RemoveEmptyEntries)
    '    'If CBool(InStr(ShowPath1(1), "/") Then
    '    Dim ShowPath2 As String() = ShowPath1(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)

    '    If ShowPath2.Count > 1 Then

    '        ShowPath = ShowPath2(0).Replace("/", "")
    '        EpisodePath = ShowPath2(1).Replace("/", "")
    '    Else
    '        ShowPath = ShowPath1(1).Replace("/", "")
    '    End If
    '    Main.FunimationShowPath = ShowPath + "/"
    '    Debug.WriteLine(ShowPath)
    '    Debug.WriteLine(Main.FunimationAPIRegion)
    '    If EpisodePath = Nothing Then 'overview site
    '        Main.GetFunimationJS_Seasons("https://title-api.prd.funimationsvc.com/v2/shows/" + ShowPath + Main.FunimationAPIRegion)

    '    Else 'single episode


    '    End If

    '    Dim FunimationCC As String() = ShowPath1(0).Split(New String() {"funimation.com"}, System.StringSplitOptions.RemoveEmptyEntries)
    '    If FunimationCC.Count > 1 Then
    '        Main.FunimationRegion = FunimationCC(1).Replace("/", "")
    '    End If
    'End Sub
End Class
