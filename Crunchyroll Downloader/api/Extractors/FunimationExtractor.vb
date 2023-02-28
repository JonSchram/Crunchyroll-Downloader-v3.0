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

    Public Sub New(downloadUrl As String)
        Me.downloadUrl = downloadUrl
    End Sub


    Public Function ListSeasons() As IEnumerable(Of Season) Implements IMetadataDownloader.ListSeasons
        Main.Navigate(downloadUrl)
        If (IsSeriesUrl()) Then
            'Dim ListSeasonUrl = BuildSeasonListUrl(ShowPath)
            'Debug.WriteLine("URL to retrieve seasons: " + ListSeasonUrl)
            Dim SeriesJson = GetSeriesJson(downloadUrl)
            Dim SeriesInfo = ParseSeriesJson(SeriesJson)
            Return SeriesInfo.Seasons
        End If
        Return New List(Of FunimationSeason)
    End Function

    Private Function DownloadJson(JsonUrl As String) As String
        Try
            Using client As New WebClient()
                client.Encoding = System.Text.Encoding.UTF8
                client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace("""", ""))
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

    Private Function ParseSeriesJson(SeriesJson As String) As FunimationSeries
        Dim Result As New FunimationSeries()
        Dim SeriesInfo = JObject.Parse(SeriesJson)

        Dim SeriesName As String = SeriesInfo.Item("name").Value(Of String)
        Result.Name = SeriesName

        Dim SeasonsList = SeriesInfo.Item("seasons").Children()
        For Each Season In SeasonsList
            Dim Name = Season.Item("name")
            Dim Id = Season.Item("id")
            Dim Type = Season.Item("type")
            Dim Number = Season.Item("number")
            Dim EntitledSeason = Season.Item("entitledSeason")
            Dim ContentId = Season.Item("contentId")
            Dim SeasonObject = New FunimationSeason With {
                    .Name = Name.Value(Of String),
                    .Id = Id.Value(Of Integer),
                    .ApiID = ContentId.Value(Of String),
                    .Type = Type.Value(Of String),
                    .Number = Number.Value(Of Integer),
                    .IsFree = EntitledSeason.Value(Of Boolean)
                }
            Result.Seasons.Add(SeasonObject)
        Next
        Return Result
    End Function

    Async Sub getCookies()
        Dim Region = GetRegion()

        Dim List = Await Browser.WebView2.CoreWebView2.CookieManager.GetCookiesAsync("https://www.funimation.com")


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


    Public Function ListEpisodes(SeasonName As String) As List(Of Episode) Implements IMetadataDownloader.ListEpisodes
        ' Implement this now.
        ' Might need to retrieve https://d33et77evd9bgg.cloudfront.net/data/v2/seasons/ first to get season info
        ' After, use https://d33et77evd9bgg.cloudfront.net/data/v2/episodes to get episode info
        ' Can access season info by taking the contentId from title-api and adding it to end of /v2/seasons/, adding .json

        Dim SeasonUrl = BuildSeasonInfoUrl(SeasonName)
        Dim SeasonJson = DownloadJson(SeasonUrl)
        Dim EpisodeList = ParseSeasonJson(SeasonJson)

        Return EpisodeList
    End Function

    Private Function ParseSeasonJson(SeasonJson As String) As List(Of Episode)
        Dim Result = New List(Of Episode)
        Dim SeasonInfo = JObject.Parse(SeasonJson)

        Dim EpisodeList = SeasonInfo.Item("episodes").Children()
        For Each Episode In EpisodeList
            Dim Id = Episode.Item("id")
            Dim Slug = Episode.Item("slug")
            Dim ApiSlug = Episode.Item("venueId")
            Dim Number = Episode.Item("episodeNumber")
            Dim IsSubRequired = Episode.Item("isSubRequired")
            Dim EpisodeObject = New Episode With {
                    .EpisodeId = Id.Value(Of String),
                    .EpisodeUrlSlug = Slug.Value(Of String),
                    .ApiUrlSlug = ApiSlug.Value(Of String),
                    .IsFree = Not IsSubRequired.Value(Of Boolean),
                    .EpisodeNumber = Number.Value(Of String)
                }
            Result.Add(EpisodeObject)
        Next
        Return Result
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

    Public Function getEpisodeInfo(EpisodeId As String) As EpisodeInfo Implements IMetadataDownloader.getEpisodeInfo
        Dim infoUrl = BuildEpisodeInfoUrl(EpisodeId)
        Dim EpisodeInfoJson = DownloadJson(infoUrl)
        Dim EpisodeInfo = ParseEpisodeInfoJson(EpisodeInfoJson)

        Return EpisodeInfo
    End Function

    Private Function ParseEpisodeInfoJson(EpisodeInfoJson As String) As EpisodeInfo
        Dim episodeInfo As JObject = JObject.Parse(EpisodeInfoJson)

        Dim Id = episodeInfo.Item("id")
        Dim slug = episodeInfo.Item("slug")
        Dim apiId = episodeInfo.Item("venueId")
        Dim episodeNumber = episodeInfo.Item("episodeNumber")
        Dim subRequired = episodeInfo.Item("isSubRequired")

        Dim SeasonInfo = episodeInfo.Item("season")
        Dim seasonNumber = SeasonInfo.Item("number")

        Dim ShowInfo = episodeInfo.Item("show")
        Dim showNameList = ShowInfo.Item("name")
        Dim showName = extractShowName(showNameList)

        Dim imagesList = episodeInfo.Item("images")
        Dim imageUrl = extractEpisodeImageUrl(imagesList.Values)

        Dim Episode As New EpisodeInfo With {
            .VideoId = Id.Value(Of String),
            .ApiId = apiId.Value(Of Integer),
            .UrlSlug = slug.Value(Of String),
            .EpisodeNumber = episodeNumber.Value(Of Integer),
            .SeasonNumber = seasonNumber.Value(Of Integer),
            .ShowName = showName,
            .ImageUrl = imageUrl,
            .IsFree = Not subRequired.Value(Of Boolean)
        }

        Return Episode
    End Function

    Private Function extractShowName(nameObject As JToken) As String
        ' Prefer English, then Spanish, then Portuguese
        Dim languageList = {"en", "es", "pt"}
        For Each language In languageList
            Dim title = nameObject.Item(language).Value(Of String)
            If title IsNot Nothing And title.Length > 0 Then
                Return title
            End If
        Next
        Return "UNDEFINED TITLE"
    End Function

    Private Function extractEpisodeImageUrl(imageList As IEnumerable(Of JToken)) As String
        ' TODO Might want to more intelligently select the episode image than to take the first one that matches
        For Each image As JObject In imageList
            Dim key As String = image("key").Value(Of String)
            If key = "Key Art - Official Video Image" Or key = "Episode Thumbnail" Then
                Dim path As String = image("path").Value(Of String)
                Return path
            End If
        Next
        Return ""
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
        Dim TemplateUrl = $"https://d33et77evd9bgg.cloudfront.net/data/v2/episodes/{EpisodeId}"
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
        Main.WebbrowserURL = "https://funimation.com/js"
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
                client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
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
