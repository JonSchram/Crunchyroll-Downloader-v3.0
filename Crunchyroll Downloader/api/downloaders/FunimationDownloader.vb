Imports Crunchyroll_Downloader.settings
Imports Microsoft.Web.WebView2.Core
Imports Newtonsoft.Json.Linq
Imports System.Collections.Specialized
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Threading

' TODO: The code uses a lot of global state for things
' For example, checking whether the download code is busy
' The goal is to treat this class like a pure API that does nothing but fetch from Funimation
' Busy status will be somewhere else
' Ideally have a download task class that contains all information required to do the download.
' Then maintain a list of these somewhere. It could even be an abstract class so that funimation and crunchyroll can have site-specific logic
Public Class FunimationDownloader
    Implements IEpisodeDownloader

    ' Seems to indicate that the app isn't busy downloading from funimation
    Public Funimation_Grapp_RDY As Boolean = True
    Public FunimationAPIRegion As String = Nothing
    Public FunimationRegion As String = Nothing
    ' URL formatted device region
    Public FunimationDeviceRegion As String = Nothing
    Public FunimationToken As String = Nothing
    Public FunimationShowPath As String = Nothing
    Public FunimationEpisodeJSON As String = Nothing
    Public FunimationAPISeasonID As New List(Of String)
    Public FunimationSeasonList As New List(Of FunimationOverview)
    Public FunimationSeasonAPIUrl As String = Nothing
    ' Seems to be a state machine about what is currently being downloaded?
    ' Hopefully not needed if the API layer can be sorted out
    Public FunimationJsonBrowser As String = Nothing

    ' Portion of the URL identifying the whole season.
    ' Comes after "www.funimation.com/v/"
    Private SeasonUrlSlug As String = Nothing

    ' It seems the web browser needs to navigate to the currently downloading episode so it can get cookies

    Public Sub New()

    End Sub

    ' Ideal api:
    ' - Instantiate using URL
    ' - Allows you to download the episode.
    ' - Support downloading a season via link or force you to format as episodes first?
    ' - Maybe allow you to give a season and episode number and it knows how to get it?
    ' - New idea: pass a FunimationEpisodeInfo so it can pick out what it needs

    ' TODO:
    ' - Download JSON from url "https://playback.prd.funimationsvc.com/v1/play"
    ' - Parse this JSON into a set of primary and fallback sources (use debug window)
    ' - (Using parsed file) download subs & video playlists
    ' - Find the resolution matching the user's choice

    Public Sub DownloadEpisode(Episode As Episode) Implements IEpisodeDownloader.DownloadEpisode
        Throw New NotImplementedException()
    End Sub

    Public Function GetPlaybacks(Episode As Episode) As EpisodePlaybackInfo
        ' A playback file contains a primary and fallbacks. Not sure what they do but maybe it's in case the primary doesn't respond?
        ' File contents seem to be exactly the same format but with different bandwidth metadata / video download URLs (there is a slug that seems to be a GUID)
        ' API paths are the same
        ' So if we get a list of fallbacks, might as well parse them in case they're needed
        Dim playbackUrl = buildPlaybackUrl(Episode.VideoId)
        Dim playbackJson = DownloadJson(playbackUrl)
        Dim playbackList = EpisodePlaybackInfo.CreateFromJson(playbackJson)

        Return playbackList
    End Function


    Private Function buildPlaybackUrl(episodeId As String) As String
        ' Original API call has playbackStreamId set
        Dim TemplateUrl = $"https://playback.prd.funimationsvc.com/v1/play/{episodeId}?deviceType=Web"
        ' May want to have another URL here. If the login token is absent, uses an anonymous URL:
        ' https://playback.prd.funimationsvc.com/v1/play/anonymous/
        Return TemplateUrl
    End Function


    ' TODO: Refactor this to be shared with extractor
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

    ''' <summary>
    ''' Sends an OPTIONS Message to the json URL and returns cookies.
    ''' This might not be needed, if the API accepts the GET request.
    ''' </summary>
    ''' <param name="JsonUrl"></param>
    ''' <returns></returns>
    Private Async Function DoJsonOptions(JsonUrl As String) As Task(Of CookieCollection)
        Dim cookies = New CookieContainer()
        Try
            Dim messageHandler = New HttpClientHandler With {
                .UseCookies = True,
                .CookieContainer = cookies
            }
            Dim myHttpClient As New HttpClient(messageHandler)
            myHttpClient.DefaultRequestHeaders.Add("User-Agent", My.Resources.ffmpeg_user_agend.Replace("""", ""))

            Dim message = New HttpRequestMessage(HttpMethod.Options, JsonUrl)
            Dim response = Await myHttpClient.SendAsync(message)

            Debug.WriteLine(cookies.ToString())


            'Using client As New WebClient()
            '    client.Encoding = System.Text.Encoding.UTF8
            '    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace("""", ""))
            '    Dim optionsValues As New NameValueCollection From {
            '        {"deviceType", "web"}
            '    }
            '    ' This may need to be replaced with "OPTIONS" but will test
            '    client.UploadValues(JsonUrl, HttpMethod.Options.ToString(), optionsValues)
            '    Return client.DownloadString(JsonUrl)
            'End Using
        Catch ex As Exception
            Debug.WriteLine("error- getting funimation SeasonJson data")
            ' Main.Navigate needed?
        End Try
        Return cookies.GetCookies(New Uri(".prd.funimationsvc.com"))
    End Function




    ' Need to write new code to handle the m3u8 downloads.
    ' ---------------- EVERYTHING BELOW THIS LINE IS OLD. -------------------
    ' Only preserved for reference
    '------------------------------------------------------------------------

    ''' Need to call GetFunimationJS_Seasons with "https://title-api.prd.funimationsvc.com/v2/shows/" + ShowPath + Main.FunimationAPIRegion
    ''' 
    ''' 

    '''
    ''' Gets the...what?
    ''' Seems to want the episode json but the name makes no sense
    '''
    Public Sub DownloadFunimationJS_Seasons(episodeJson As String, startEpisode As Integer, endEpisode As Integer)
        Try
#Region "JS"
            Debug.WriteLine("EpisodeJson: " + episodeJson)
            'TODO: This should cache the episode list. It was needed to populate the episode # combo boxes
            ' Anime_Add.Add_Display.Text = "preparing ...."
            Dim ListOfEpisodes As New List(Of String)
            Dim BaseURL As String = "https://www.funimation.com/v/" + SeasonUrlSlug + "/"
            Dim ser As JObject = JObject.Parse(episodeJson)
            Dim data As List(Of JToken) = ser.Children().ToList
            For Each item As JProperty In data
                item.CreateReader()
                Select Case item.Name
                    Case "episodes" 'each record is inside the entries array
                        For Each Entry As JObject In item.Values
                            Dim slug As String = Entry("slug").ToString
                            Debug.WriteLine(BaseURL + slug)
                            ListOfEpisodes.Add(BaseURL + slug)
                        Next
                End Select
            Next

            Dim First As Integer = Math.Min(startEpisode, endEpisode)
            Dim Last As Integer = Math.Max(startEpisode, endEpisode)
            Dim numberOfEpisodes As Integer = Last - First + 1

            ' All of this
            ' - updates the number of running downloads
            ' - Adds new episodes to a queue or begins downloads immediately
            ' - Exits if the user has decided to cancel a mass download
            ' snip: big loop that did all these things. Need to place elsewhere

#End Region
        Catch ex As Exception
            If Main.Debug2 = True Then
                MsgBox(ex.ToString)
            End If
            ' TODO: Reset add season controls
        End Try
        FunimationEpisodeJSON = Nothing
        ' Why pause?
        Pause(5)
        ' Change to "Add video" dialog
    End Sub

    Private Sub getJsForEpisode(currentEpisode As String)
        If CBool(InStr(currentEpisode, "funimation.com/v/")) Then
            Dim Episode0() As String = currentEpisode.Split(New String() {"?"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Episode() As String = Episode0(0).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim v1JsonUrl As String = "https://d33et77evd9bgg.cloudfront.net/data/v1/episodes/" + Episode(Episode.Length - 1) + ".json"
            Dim v1Json As String = Nothing
            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    v1Json = client.DownloadString(v1JsonUrl)
                End Using
                Main.WebbrowserURL = currentEpisode
                GetFunimationNewJS_VideoProxy(Nothing, v1Json)
            Catch ex As Exception
                Debug.WriteLine("error- getting v1Json data for the bypasss")
                Debug.WriteLine(ex.ToString)
                Main.Navigate(currentEpisode)
            End Try
        Else
            Main.Navigate(currentEpisode)
        End If
    End Sub

    ''' <summary>
    ''' Populates a list of seasons
    ''' </summary>
    ''' <param name="JsonUrl"></param>
    ''' <param name="Json"></param>
    Public Function GetFunimationJS_Seasons(Optional ByVal JsonUrl As String = Nothing, Optional ByVal Json As String = Nothing) As List(Of FunimationOverview)
        Dim SeasonList As New List(Of FunimationOverview)
        Dim SeasonJson As String = Nothing
        Debug.WriteLine("JsonUrl: " + JsonUrl)
        If JsonUrl = Nothing Then
            SeasonJson = Json
        Else
            FunimationSeasonAPIUrl = JsonUrl
            Try
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    SeasonJson = client.DownloadString(JsonUrl)
                End Using
            Catch ex As Exception
                Debug.WriteLine("error- getting funimation SeasonJson data")
                FunimationJsonBrowser = "SeasonJson"
                Main.Navigate(JsonUrl)
                Return SeasonList
            End Try
        End If
        Debug.WriteLine("SeasonJson: " + SeasonJson)
        Dim ser As JObject = JObject.Parse(SeasonJson)
        Dim data As List(Of JToken) = ser.Children().ToList
        Dim Slug As String = Nothing
        Dim Title As String = Nothing
        Dim ID As String = Nothing
        For Each item As JProperty In data
            item.CreateReader()
            'MsgBox(item.Name)
            Select Case item.Name
                Case "slug"
                    Slug = item.Value.ToString
                Case "index" 'each record is inside the entries array
                    Dim SubData2 As List(Of JToken) = item.Values("seasons").Children().ToList
                    For i As Integer = 0 To SubData2.Count - 1
                        Dim SubItem As JToken = SubData2.Item(i)
                        Dim SeasonSubData As List(Of JToken) = SubItem.Children().ToList
                        For Each SeasonSubItem As JProperty In SeasonSubData
                            SeasonSubItem.CreateReader()
                            Select Case SeasonSubItem.Name
                                Case "contentId"
                                    'MsgBox(SeasonSubItem.Value.ToString)
                                    ID = SeasonSubItem.Value.ToString
                                Case "title"
                                    ' MsgBox(SeasonSubItem.Value.Item("en").ToString)
                                    Title = SeasonSubItem.Value.Item("en").ToString
                                    SeasonList.Add(New FunimationOverview(Slug, ID, Title))
                            End Select
                        Next
                    Next
            End Select
        Next
        'Debug.WriteLine("SeasonJson: ")
        ' Todo: Reset "add video" dialog
        Main.WebbrowserURL = "https://funimation.com/js"
        Debug.WriteLine("Count: " + FunimationSeasonList.Count.ToString)
        Return SeasonList
    End Function


#Region "Funimation JS "


    Private Function ConvertFunimationDub(ByVal Dub As String) As String
        If Dub = "english" Then
            Return "English"
        ElseIf Dub = "spanish(Mexico)" Then
            Return "Spanish (Latin Am)"
        ElseIf Dub = "portuguese(Brazil)" Then
            Return "Portuguese (Brazil)"
        ElseIf Dub = "japanese" Then
            Return "Japanese"
        Else
            Return "N/A"
        End If
    End Function
    Private Function ConvertFunimationDubToJson(ByVal Dub As String) As String
        If Dub = "english" Then
            Return "en"
        ElseIf Dub = "spanish(Mexico)" Then
            Return "es"
        ElseIf Dub = "portuguese(Brazil)" Then
            Return "pt"
        ElseIf Dub = "japanese" Then 'japanese
            Return "ja"
        Else
            Return "N/A"
        End If
    End Function
    Private Function ConvertJsonToFunimationDub(ByVal Dub As String) As String
        If Dub = "en" Then
            Return "english"
        ElseIf Dub = "es" Then
            Return "spanish(Mexico)"
        ElseIf Dub = "pt" Then
            Return "portuguese(Brazil)"
        ElseIf Dub = "ja" Then
            Return "japanese"
        Else
            Return "N/A"
        End If
    End Function
    Public Async Sub GetFunimationNewJS_VideoProxy(Optional ByVal v1JsonURL As String = Nothing, Optional ByVal v1JsonData As String = Nothing)
        Try
            Dim list As List(Of CoreWebView2Cookie) = Await Browser.WebView2.CoreWebView2.CookieManager.GetCookiesAsync("https://www.funimation.com/")
            Dim Cookie As String = ""
            For i As Integer = 0 To list.Count - 1
                If CBool(InStr(list.Item(i).Domain, "funimation.com")) Then 'list.Item(i).Domain = "funimation.com" Then
                    Cookie = Cookie + list.Item(i).Name + "=" + list.Item(i).Value + ";"
                End If
                If CBool(InStr(list.Item(i).Domain, "funimation.com")) And CBool(InStr(list.Item(i).Name, "src_token")) Then 'list.Item(i).Domain = "funimation.com" Then
                    FunimationToken = "Token " + list.Item(i).Value
                End If
                If CBool(InStr(list.Item(i).Domain, "funimation.com")) And CBool(InStr(list.Item(i).Name, "region")) Then 'list.Item(i).Domain = "funimation.com" Then
                    FunimationDeviceRegion = "?deviceType=web&" + list.Item(i).Name + "=" + list.Item(i).Value
                End If
            Next

        Catch ex As Exception

        End Try
        ' region=US;
        ' Clear loaded urls?
        ' TODO: Use a unified downloader with a thread pool
        ' When downloading, will probably want to get cookies from the browser or find the API method to log in.
        ' src_token is your login token?
        Dim Evaluator = New Thread(Sub() Me.GetFunimationNewJS_Video(v1JsonURL, v1JsonData))
        Evaluator.Start()
    End Sub

    ' This is where it gets the information about an individual video
    Public Sub GetFunimationNewJS_Video(ByVal v1JsonUrl As String, ByVal v1JsonData As String) ', ByVal WebsiteURL As String
        Debug.WriteLine(v1JsonUrl)
        Dim v1Json As String = Nothing
        If v1JsonUrl = Nothing Then
            v1Json = v1JsonData
        Else
            Try
                'Throw New Exception("TEst")
                Using client As New WebClient()
                    client.Encoding = System.Text.Encoding.UTF8
                    client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                    v1Json = client.DownloadString(v1JsonUrl)
                End Using
            Catch ex As Exception
                Debug.WriteLine("error- getting v1Json data")
                Debug.WriteLine(ex.ToString)
                ' TODO: Make a method in main that does the correct things on the UI thread (probably invoke())
                Main.Invoke(Sub()
                                'Me.Text = "Status: error - getting v1Json data"
                                FunimationJsonBrowser = "v1Json"
                                Main.Navigate(v1JsonUrl)
                                'Anime_Add.StatusLabel.Text = "Status: error - getting v1Json data"
                                Main.Invalidate()
                            End Sub)
                Exit Sub
            End Try
        End If
        'Debug.WriteLine("v1Json: " + v1Json)
        If v1Json = Nothing Then
            ' TODO: Make method in main that sets an error message
            Main.Invoke(Sub()
                            Main.Text = "Status: error - getting v1Json data"
                            'Anime_Add.StatusLabel.Text = "Status: error - getting v1Json data"
                            Main.Invalidate()
                        End Sub)
            Exit Sub
        End If
        Try
            Dim ffmpeg_command_temp As String = Main.ffmpeg_command

            ' TODO: Make method in Main that sets a status
            Main.Invoke(Sub()
                            Main.Text = "Status: looking for video file"
                            ' TODO: Set status label in "Add video" dialog to "Status: looking for video file"
                            Main.Invalidate()
                        End Sub)
            Funimation_Grapp_RDY = False
#Region "Name"
            Dim DownloadPfad As String = Nothing
            Dim FunimationSeason As String = Nothing
            Dim FunimationEpisode As String = Nothing
            Dim FunimationTitle As String = Nothing
            Dim FunimationEpisodeTitle As String = Nothing
            Dim FunimationDub As String = Nothing
            Dim FunimationAudioMap As String = Nothing
            Dim FunimationEpisodeJson As String = Nothing
            Dim thumbnail4 As String = ""
            Dim ser As JObject = JObject.Parse(v1Json)
            Dim data As List(Of JToken) = ser.Children().ToList
            For Each item As JProperty In data
                item.CreateReader()
                Select Case item.Name
                    Case "images" 'each record is inside the entries array
                        For Each Entry As JObject In item.Values
                            Dim key As String = Entry("key").ToString
                            If key = "Key Art - Official Video Image" Or key = "Episode Thumbnail" Then
                                Dim path As String = Entry("path").ToString
                                thumbnail4 = path
                            End If
                        Next
                    Case "id"  'id.json for video
                        FunimationEpisodeJson = item.Value.ToString
                    Case "episodeNumber"
                        Dim FunimationEpisode3 As String = Main.RemoveExtraSpaces(item.Value.ToString)
                        If Main.Episode_Prefix = "[default episode prefix]" Then
                            FunimationEpisode = "Episode " + Main.AddLeadingZeros(FunimationEpisode3)
                        Else
                            FunimationEpisode = Main.Episode_Prefix + Main.AddLeadingZeros(FunimationEpisode3)
                        End If
                    Case "name"
                        Dim NameData As List(Of JToken) = item.Values.ToList()
                        For Each Name As JProperty In NameData
                            Select Case Name.Name
                                Case "en"
                                    FunimationEpisodeTitle = Name.Value.ToString
                                    Debug.WriteLine("FunimationEpisodeTitle: " + FunimationEpisodeTitle)
                            End Select
                        Next
                    Case "season" 'each record is inside the entries array
                        Dim SubData As List(Of JToken) = item.Values.ToList()
                        For Each SubItem As JProperty In SubData
                            Select Case SubItem.Name
                                Case "name"
                                    If Main.Season_Prefix = "[default season prefix]" Then
                                        Dim SeasonNameData As List(Of JToken) = SubItem.Values.ToList()
                                        For Each SeasonName As JProperty In SeasonNameData
                                            Select Case SeasonName.Name
                                                Case "en"
                                                    FunimationSeason = SeasonName.Value.ToString
                                                    Debug.WriteLine("FunimationSeason: " + FunimationSeason)
                                            End Select
                                        Next
                                    End If
                                Case "number"
                                    If Main.Season_Prefix = "[default season prefix]" Then
                                        'FunimationSeason = Entry("name")
                                    Else
                                        Dim EpisodeNumer As String = SubItem.Value.ToString
                                        FunimationSeason = Main.Season_Prefix + " " + EpisodeNumer
                                        Debug.WriteLine("FunimationSeason: " + FunimationSeason)
                                    End If
                            End Select
                        Next
                    Case "show" 'each record is inside the entries array
                        Dim SubData As List(Of JToken) = item.Values.ToList()
                        For Each SubItem As JProperty In SubData
                            Select Case SubItem.Name
                                Case "name"
                                    Dim SeasonNameData As List(Of JToken) = SubItem.Values.ToList()
                                    For Each SeasonName As JProperty In SeasonNameData
                                        Select Case SeasonName.Name
                                            Case "en"
                                                FunimationTitle = SeasonName.Value.ToString
                                                Debug.WriteLine("FunimationTitle: " + FunimationTitle)
                                        End Select
                                    Next
                            End Select
                        Next
                End Select
            Next
            FunimationTitle = Main.RemoveExtraSpaces(String.Join(" ", FunimationTitle.Split(Main.invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)).Replace(Chr(34), "").Replace("\", "").Replace("/", "")
            FunimationEpisodeTitle = String.Join(" ", FunimationEpisodeTitle.Split(Main.invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c).Replace(Chr(34), "").Replace("\", "").Replace("/", "")
            FunimationDub = ConvertFunimationDub(Main.DubFunimation) 'FunimationDub2(0)
            Dim DefaultName As String = Main.RemoveExtraSpaces(FunimationTitle + " " + FunimationSeason + " " + FunimationEpisode)

            Dim NameParts As String() = Main.NameBuilder.Split(New String() {";"}, System.StringSplitOptions.RemoveEmptyEntries)

            ' TODO: Refactor this when refactoring renaming code
            For i As Integer = 0 To NameParts.Count - 1

                If NameParts(i) = "AnimeTitle" Then
                    DefaultName = DefaultName + " " + FunimationTitle
                ElseIf NameParts(i) = "Season" Then
                    DefaultName = DefaultName + " " + FunimationSeason
                ElseIf NameParts(i) = "EpisodeNR" Then
                    DefaultName = DefaultName + " " + FunimationEpisode
                ElseIf NameParts(i) = "EpisodeName" Then
                    DefaultName = DefaultName + " " + FunimationEpisodeTitle
                ElseIf NameParts(i) = "AnimeDub" Then
                    DefaultName = DefaultName + " RepDub"
                ElseIf NameParts(i) = "AnimeSub" Then
                    DefaultName = DefaultName + " RepSub"
                End If

            Next


            If CBool(InStr(DefaultName, " RepDub")) Then
                DefaultName = DefaultName.Replace(" RepDub", "")
            End If

            If CBool(InStr(DefaultName, " RepSub")) Then
                DefaultName = DefaultName.Replace(" RepSub", "")
            End If

            DefaultName = DefaultName.Replace("&#x27;", "'")
            'Dim DefaultPath As String = Pfad + "\" + DefaultName + VideoFormat
            'DefaultPath = DefaultPath.Replace("\\", "\")
#End Region
#Region "Pfad"
            Dim TextBox2_Text As String = Nothing
            ' TODO: Make a method in main that sets the text box text on UI thread
            Main.Invoke(Sub()
                            TextBox2_Text = "TODO: Get anime being downloaded" 'Anime_Add.TextBox2.Text
                        End Sub)
            If TextBox2_Text = Nothing Or TextBox2_Text = "Use Custom Name" Then
            Else
                ' Does nothing?
                Main.Invoke(Sub()
                            End Sub)
            End If
            DefaultName = DefaultName.Replace(":", "")
            DownloadPfad = Main.RemoveExtraSpaces(UseSubfolder(FunimationTitle, FunimationSeason, Main.Pfad))
            Dim extension = ProgramSettings.GetInstance().OutputFormat.GetFileExtension()
            If Not Directory.Exists(Path.GetDirectoryName(DownloadPfad)) Then
                ' Nein! Jetzt erstellen...
                Try
                    Directory.CreateDirectory(Path.GetDirectoryName(DownloadPfad))
                    DownloadPfad = Main.RemoveExtraSpaces(Chr(34) + DownloadPfad + DefaultName + "." + extension + Chr(34))
                Catch ex As Exception
                    ' Ordner wurde nich erstellt
                    DownloadPfad = Main.RemoveExtraSpaces(Chr(34) + Main.Pfad + DefaultName + "." + extension + Chr(34))
                End Try
            Else
                DownloadPfad = Main.RemoveExtraSpaces(Chr(34) + DownloadPfad + DefaultName + "." + extension + Chr(34))
            End If
#Region "lösche doppel download / Delete double download"
            Dim Pfad5 As String = DownloadPfad.Replace(Chr(34), "")
            If My.Computer.FileSystem.FileExists(Pfad5) Then 'Pfad = Kompeltter Pfad mit Dateinamen + ENdung
                ' TODO: Make method in main that sets main window and status text
                Main.Invoke(Sub()
                                Main.Text = "Status: File already exists."
                                ' Anime_Add.StatusLabel.Text = "Status: File already exists."
                                Main.Invalidate()
                            End Sub)
                If MessageBox.Show("The file " + Pfad5 + " already exists." + vbNewLine + "You want to override it?", "File exists!", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Try
                        My.Computer.FileSystem.DeleteFile(Pfad5)
                        Main.Invoke(Sub()
                                        Main.Text = "Status: Old file overwritten."
                                        ' Anime_Add.StatusLabel.Text = "Status: Old file overwritten."
                                        Main.Invalidate()
                                    End Sub)
                    Catch ex As Exception
                    End Try
                Else
                    Main.Invoke(Sub()
                                    Main.Text = "Crunchyroll Downloader"
                                    ' Anime_Add.StatusLabel.Text = "idle"
                                    Main.Invalidate()
                                End Sub)
                    Funimation_Grapp_RDY = True
                    Exit Sub
                End If
            End If
#End Region
#End Region
#Region "json"
            ' This is where it downloads the video
            ' The site does an OPTIONS call to v1/play/{ID} first and it has a cookie set. Can do that first if it doesn't work for some reason.
            ' There's also a playbackStreamId at the end of the video URL that I can't find a source for.
            Dim EpisodeJsonString As String = Nothing
            Dim PlayerClient As New WebClient()
            PlayerClient.Encoding = Encoding.UTF8
            PlayerClient.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
            PlayerClient.Headers.Add(HttpRequestHeader.Accept, "application/json, text/plain, */*")
            PlayerClient.Headers.Add("origin: https://www.funimation.com/")
            PlayerClient.Headers.Add(HttpRequestHeader.Referer, "https://www.funimation.com/")
            Dim BaseUrl As String = "https://playback.prd.funimationsvc.com/v1/play/"
            Debug.WriteLine(PlayerClient.Headers.ToString)
            If FunimationToken = Nothing Then
                Debug.WriteLine("FunimationToken: false")
                BaseUrl = "https://playback.prd.funimationsvc.com/v1/play/anonymous/"
            Else
                Debug.WriteLine("FunimationToken: true")
                PlayerClient.Headers.Add(HttpRequestHeader.Authorization, FunimationToken)
            End If
            'FunimationToken
            'MsgBox(WebbrowserCookie)
            'BaseUrl + FunimationEpisodeJson + FunimationDeviceRegion
            If FunimationDeviceRegion = Nothing Then
                FunimationDeviceRegion = "?deviceType=web"
            End If
            Debug.WriteLine(BaseUrl + FunimationEpisodeJson + FunimationDeviceRegion)
            If Main.WebbrowserCookie = Nothing Then
            Else
                PlayerClient.Headers.Add(HttpRequestHeader.Cookie, Main.WebbrowserCookie)
            End If
            If Main.SystemWebBrowserCookie = Nothing Then
            Else
                PlayerClient.Headers.Add(HttpRequestHeader.Cookie, Main.SystemWebBrowserCookie)
            End If
            Try
                EpisodeJsonString = PlayerClient.DownloadString(BaseUrl + FunimationEpisodeJson + FunimationDeviceRegion)
            Catch ex As Exception
                Debug.WriteLine(ex.ToString)
                Pause(2)
                Debug.WriteLine("showexperience 2nd try")
                EpisodeJsonString = PlayerClient.DownloadString(BaseUrl + FunimationEpisodeJson + FunimationDeviceRegion)
                'ErrorBrowserUrl = "https://www.funimation.com/api/showexperience/" + ExperienceID + "/?pinst_id=fzQc9p9f"
            End Try
            ' This parses the playlist file
            Dim SubsFiles As New List(Of FunimationSubs)
            Dim VideoStreams As New List(Of FunimationStream)
            Dim EpisodeJson As JObject = JObject.Parse(EpisodeJsonString)
            Dim EpisodeJsonData As List(Of JToken) = EpisodeJson.Children().ToList
            Dim PrimaryVersion As String = Nothing ' item.Item("version").ToString
            Dim PrimaryaudioLanguage As String = Nothing ' item.Item("audioLanguage").ToString
            Dim PrimarymanifesUrl As String = Nothing 'item.Item("manifestPath").ToString
            For Each item As JProperty In EpisodeJsonData
                item.CreateReader()
                Select Case item.Name
                    Case "fallback"
                        'MsgBox(SubItem.Value.ToString())
                        Dim SubData2 As List(Of JToken) = item.Values.ToList
                        'MsgBox(SubData2.Count.ToString)
                        For i As Integer = 0 To SubData2.Count - 1
                            Dim audioLanguage As String = Nothing
                            Dim Format As String = Nothing
                            Try
                                Dim Version As String = SubData2(i).Item("version").ToString
                                audioLanguage = SubData2(i).Item("audioLanguage").ToString
                                Dim Url As String = SubData2(i).Item("manifestPath").ToString
                                Debug.WriteLine(Version)
                                Debug.WriteLine(audioLanguage)
                                Debug.WriteLine(Url)
                                Format = SubData2(i).Item("fileExt").ToString
                                If Format = "m3u8" Then
                                    VideoStreams.Add(New FunimationStream(audioLanguage, Version, Url, False))
                                End If
                            Catch ex As Exception
                            End Try
                            Try
                                Dim SubData3 As List(Of JToken) = SubData2(i).Item("subtitles").Children.ToList
                                'MsgBox(SubData2.Count.ToString)
                                For i3 As Integer = 0 To SubData2.Count - 1
                                    Try
                                        Dim LangCode As String = SubData3(i3).Item("languageCode").ToString
                                        Dim CCFormat As String = SubData3(i3).Item("fileExt").ToString
                                        Dim Url As String = SubData3(i3).Item("filePath").ToString
                                        If audioLanguage = "ja" And Format = "m3u8" Then
                                            SubsFiles.Add(New FunimationSubs(LangCode, CCFormat, Url))
                                        End If
                                    Catch ex As Exception
                                    End Try
                                Next
                            Catch ex As Exception
                            End Try
                        Next
                    Case "primary" 'each record is inside the entries array
                        Dim SubData As List(Of JToken) = item.Values.ToList()
                        For Each SubItem As JProperty In SubData
                            Select Case SubItem.Name
                               ' Case "manifestPath"
                               '     Funimation_m3u8_Main = SubItem.Value.ToString
                               ''MsgBox()
                                Case "version"
                                    PrimaryVersion = SubItem.Value.ToString
                                Case "audioLanguage"
                                    PrimaryaudioLanguage = SubItem.Value.ToString
                                Case "manifestPath"
                                    PrimarymanifesUrl = SubItem.Value.ToString
                                Case "subtitles"
                                    Dim SubData2 As List(Of JToken) = SubItem.Values.ToList
                                    For i As Integer = 0 To SubData2.Count - 1
                                        Try
                                            Dim LangCode As String = SubData2(i).Item("languageCode").ToString
                                            Dim Format As String = SubData2(i).Item("fileExt").ToString
                                            Dim Url As String = SubData2(i).Item("filePath").ToString
                                            SubsFiles.Add(New FunimationSubs(LangCode, Format, Url))
                                        Catch ex As Exception
                                        End Try
                                    Next
                            End Select
                        Next
                        Debug.WriteLine("primary version: " + PrimaryVersion)
                        Debug.WriteLine("primary audioLanguage: " + PrimaryaudioLanguage)
                        Debug.WriteLine("primary manifesUrl: " + PrimarymanifesUrl)
                        VideoStreams.Add(New FunimationStream(PrimaryaudioLanguage, PrimaryVersion, PrimarymanifesUrl, True))
                End Select
            Next
#End Region
#Region "m3u8 URL"
            Dim Funimation_m3u8_Main As String = Nothing
            Dim Funimation_m3u8_MainVersion As String = Nothing
            Dim Funimation_m3u8_Primary_Version As String = Nothing
            Dim Funimation_m3u8_Primary As String = Nothing
            Dim Funimation_m3u8_Primary_audioLanguage As String = Nothing
            Dim Funimation_m3u8_final As String = Nothing
            Dim client0 As New WebClient
            client0.Encoding = Encoding.UTF8
            If Main.DownloadScope = 1 Then
                For i As Integer = 0 To VideoStreams.Count - 1
                    If VideoStreams(i).Primary = True Then
                        Funimation_m3u8_Primary = VideoStreams(i).Url
                        Funimation_m3u8_Primary_Version = VideoStreams(i).version
                        Funimation_m3u8_Primary_audioLanguage = VideoStreams(i).audioLanguage
                    End If
                    If VideoStreams(i).audioLanguage = ConvertFunimationDubToJson(Main.DubFunimation) And Funimation_m3u8_Main = Nothing Then
                        Funimation_m3u8_Main = VideoStreams(i).Url
                        Funimation_m3u8_MainVersion = VideoStreams(i).version
                    ElseIf VideoStreams(i).audioLanguage = ConvertFunimationDubToJson(Main.DubFunimation) And VideoStreams(i).version = "uncut" Then
                        Funimation_m3u8_Main = VideoStreams(i).Url
                        Funimation_m3u8_MainVersion = VideoStreams(i).version
                    End If
                Next

                If Funimation_m3u8_Main = Nothing Then
                    Funimation_m3u8_Main = Funimation_m3u8_Primary
                    Funimation_m3u8_MainVersion = Funimation_m3u8_Primary_Version
                    FunimationDub = ConvertFunimationDub(ConvertJsonToFunimationDub(Funimation_m3u8_Primary_audioLanguage))
                End If
                If Funimation_m3u8_Main = Nothing Then
                    If MessageBox.Show("No media matching your settings." + vbNewLine + "Avalible: Not implimentented, press 'Yes' to copy the data into the clipboard.", "No media", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        Main.Invoke(Sub()
                                        Try
                                            My.Computer.Clipboard.SetText(EpisodeJsonString)
                                        Catch ex As Exception
                                        End Try
                                    End Sub)
                        Exit Sub
                    Else
                        Funimation_Grapp_RDY = True
                        Exit Sub
                    End If
                End If
                ' TODO: Replace with method in Main that sets text from UI thread
                Main.Invoke(Sub()
                                Main.Text = "Status: Video found!"
                                ' Anime_Add.StatusLabel.Text = "Status: Video found!"
                                Main.Invalidate()
                            End Sub)
                Dim str1 As String = client0.DownloadString(Funimation_m3u8_Main.Replace(Chr(34), ""))
                If CBool(InStr(str1, "# AUDIO groups")) Then
                    Dim FunimationAudio() As String = str1.Split(New String() {"# AUDIO groups"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim FunimationAudio2() As String = FunimationAudio(1).Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim FunimationAudio3() As String = FunimationAudio2(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    FunimationAudioMap = " -headers " + My.Resources.ffmpeg_user_agend + " -i " + Chr(34) + FunimationAudio3(0) + Chr(34)
                End If

                Dim str2() As String = str1.Split(New String() {"# keyframes"}, System.StringSplitOptions.RemoveEmptyEntries)


                Dim Streams() As String = str2(0).Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)

                Dim FunimationBackupm3u8 As String = Nothing

                Dim Tartegt_m3u8_list As New List(Of String)

                Dim Secondary_m3u8_list As New List(Of String)


                For i As Integer = 0 To Streams.Length - 1


                    If CBool(InStr(Streams(i), "x" + ProgramSettings.getInstance().DownloadResolution.ToString)) Then

                        Tartegt_m3u8_list.Add(Streams(i) + vbCrLf + Streams(i + 1))
                        FunimationBackupm3u8 = Streams(i + 1)

                    ElseIf CBool(InStr(Streams(i), Main.ResoFunBackup)) And FunimationBackupm3u8 = Nothing Then

                        Secondary_m3u8_list.Add(Streams(i) + vbCrLf + Streams(i + 1))
                        FunimationBackupm3u8 = Streams(i + 1)

                    End If

                Next

                If Tartegt_m3u8_list.Count = 0 And Secondary_m3u8_list.Count > 0 Then
                    Tartegt_m3u8_list = Secondary_m3u8_list
                End If

                If Tartegt_m3u8_list.Count > 1 Then
                    Dim HigestBitrate As Integer = 0
                    For i2 As Integer = 0 To Tartegt_m3u8_list.Count - 1
                        Dim Bandwidth_String As String = Nothing
                        If CBool(InStr(Tartegt_m3u8_list.Item(i2), "AVERAGE-BANDWIDTH=")) = True Then
                            Bandwidth_String = "AVERAGE-BANDWIDTH="
                        ElseIf CBool(InStr(Tartegt_m3u8_list.Item(i2), "BANDWIDTH=")) = True Then
                            Bandwidth_String = "BANDWIDTH="
                        Else
                            Continue For
                        End If

                        Dim BitRate() As String = Tartegt_m3u8_list.Item(i2).Split(New String() {Bandwidth_String}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim BitRate2() As String = BitRate(1).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                        If Main.Funimation_Bitrate = 0 Then
                            If CInt(BitRate2(0)) > HigestBitrate Then
                                HigestBitrate = CInt(BitRate2(0))
                            End If
                        Else

                            If HigestBitrate > CInt(BitRate2(0)) Then
                                HigestBitrate = CInt(BitRate2(0))
                            ElseIf HigestBitrate = 0 Then
                                HigestBitrate = CInt(BitRate2(0))
                            End If
                        End If
                    Next

                    For i2 As Integer = 0 To Tartegt_m3u8_list.Count - 1
                        If CBool(InStr(Tartegt_m3u8_list.Item(i2), HigestBitrate.ToString)) = True Then
                            Dim new_m3u8_2() As String = Tartegt_m3u8_list.Item(i2).Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)
                            Funimation_m3u8_final = new_m3u8_2(1)
                            FunimationBackupm3u8 = new_m3u8_2(1)
                        End If
                    Next
                ElseIf Tartegt_m3u8_list.Count = 1 Then
                    Dim new_m3u8_2() As String = Tartegt_m3u8_list.Item(0).Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)
                    Funimation_m3u8_final = new_m3u8_2(1)
                    FunimationBackupm3u8 = new_m3u8_2(1)
                End If



                If Funimation_m3u8_final = Nothing And FunimationBackupm3u8 = Nothing Then
                    ' TODO: Replace with Main setter method.
                    ' TODO: Maybe automatically select closest resolution?
                    Main.Invoke(Sub()
                                    Main.Text = "Status: Resolution not found!"
                                    ' Anime_Add.StatusLabel.Text = "Status: Resolution not found!"
                                    Main.Invalidate()
                                    Main.DialogTaskString = "Funimation_Resolution"
                                    Main.ResoNotFoundString = str1
                                    ErrorDialog.ShowDialog()
                                End Sub)
                    Main.ResoFunBackup = Main.ResoBackString
                    For i As Integer = 0 To Streams.Length - 1
                        If CBool(InStr(Streams(i), Main.ResoBackString)) Then
                            Dim Streams2() As String = Streams(i).Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim Streams3() As String = Streams2(1).Split(New String() {"#EXT-"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim StreamURL As String = "https://" + Streams3(0).Trim
                            Dim CheckClient As New WebClient
                            CheckClient.Encoding = Encoding.UTF8
                            If Not Main.WebbrowserCookie = Nothing Then
                                CheckClient.Headers.Add(HttpRequestHeader.Cookie, Main.WebbrowserCookie)
                            ElseIf Not Main.SystemWebBrowserCookie = Nothing Then
                                CheckClient.Headers.Add(HttpRequestHeader.Cookie, Main.SystemWebBrowserCookie)
                            End If
                            Dim m3u8String As String = CheckClient.DownloadString(StreamURL)
                            'MsgBox(textLenght(i))
                            Dim keyfileurl() As String = m3u8String.Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim keyfileurl2() As String = keyfileurl(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim keyfileurl3 As String = keyfileurl2(0)
                            If CBool(InStr(keyfileurl2(0), "https://")) Then
                            Else
                                Dim c() As String = New Uri(StreamURL).Segments
                                Dim path As String = "https://" + New Uri(StreamURL).Host
                                For i3 As Integer = 0 To c.Count - 2
                                    path = path + c(i3)
                                Next
                                keyfileurl3 = path + keyfileurl2(0) 'New Uri(textLenght(i)).LocalPath + keyfileurl2(0)
                            End If
                            Try
                                Dim CheckClient2 As New WebClient
                                CheckClient2.Encoding = System.Text.Encoding.UTF8
                                Dim testdl As String = CheckClient2.DownloadString(keyfileurl3)
                                Funimation_m3u8_final = StreamURL
                                Exit For
                            Catch ex As Exception
                                Debug.WriteLine(keyfileurl3 + vbNewLine + ex.ToString)
                            End Try
                            'Funimation_m3u8_final = textLenght(i)
                            'Exit For
                        End If
                    Next
                ElseIf Funimation_m3u8_final = Nothing Then
                    Funimation_m3u8_final = FunimationBackupm3u8
                Else
                    ' TODO
                    Main.Invoke(Sub()
                                    Main.Text = "Status: Resolution found!"
                                    ' Anime_Add.StatusLabel.Text = "Status: Resolution found!"
                                    Main.Invalidate()
                                End Sub)
                End If
                Debug.WriteLine("Funimation_m3u8_final: " + Funimation_m3u8_final)
                Funimation_m3u8_final = Funimation_m3u8_final.Replace(Chr(34), "")
            Else
                ' TODO
                Main.Invoke(Sub()
                                Main.Text = "Status: Substitles only mode - skipped video"
                                ' Anime_Add.StatusLabel.Text = "Status: Substitles only mode - skipped video"
                                Main.Invalidate()
                            End Sub)
            End If
            'MsgBox(FunimationName3)
            Dim ResoHTMLDisplay As String = ProgramSettings.GetInstance().DownloadResolution.ToString + "p"
#Region "Subs"
            Dim HardSubFound As Boolean = False
            Dim HardSubSplittString As String = Nothing
            Dim UsedSub As String = Nothing
            Dim UsedSubs As New List(Of String)
            Dim ffmpeg_hardsub As String = Nothing
            For i As Integer = 0 To SubsFiles.Count - 1
                Debug.WriteLine(SubsFiles(i).LangugageCode + "-" + SubsFiles(i).Format)
                If Main.SubFunimation.Count = 0 Then
                    Exit For
                End If
                If Main.Funimation_vtt = True And SubsFiles(i).Format = "vtt" And CBool(InStr(Main.SubFunimationString, SubsFiles(i).LangugageCode)) Then
                    UsedSubs.Add(SubsFiles(i).Url + " , " + SubsFiles(i).LangugageCode)
                ElseIf Main.Funimation_srt = True And SubsFiles(i).Format = "srt" And CBool(InStr(Main.SubFunimationString, SubsFiles(i).LangugageCode)) Then
                    UsedSubs.Add(SubsFiles(i).Url + " , " + SubsFiles(i).LangugageCode)
                End If
            Next
            '
            Dim SoftSubMergeURLs As String = Nothing
            Dim SoftSubMergeMaps As String = " -map 0:v -map 0:a"
            If Not FunimationAudioMap = Nothing Then
                SoftSubMergeMaps = " -map 0:v -map 1:a"
            End If
            Dim SoftSubMergeMetatata As String = Nothing
            Dim settings = ProgramSettings.GetInstance()
            Dim outputFormat = settings.OutputFormat
            If UsedSubs.Count > 0 Then
                If outputFormat.GetSubtitleFormat() <> Format.SubtitleMerge.DISABLED And Main.DownloadScope = 0 Then
                    Dim DispositionIndex As Integer = 999
                    Dim LastMerged As String = Nothing
                    Dim MapCount As Integer = -1
                    For i As Integer = 0 To UsedSubs.Count - 1
                        Dim SoftSub As String() = UsedSubs.Item(i).Split(New String() {" , "}, System.StringSplitOptions.RemoveEmptyEntries)
                        If Main.CCtoMP4CC(SoftSub(1)) = LastMerged Then
                            Continue For
                        Else
                            LastMerged = Main.CCtoMP4CC(SoftSub(1))
                        End If
                        MapCount = MapCount + 1
                        If Main.DefaultSubFunimation = SoftSub(1) Then
                            'Debug.WriteLine(SoftSub(1))
                            DispositionIndex = MapCount
                        End If
                        If SoftSubMergeURLs = Nothing Then
                            SoftSubMergeURLs = " -headers " + My.Resources.ffmpeg_user_agend + " -i " + Chr(34) + SoftSub(0) + Chr(34)
                        Else
                            SoftSubMergeURLs = SoftSubMergeURLs + " -headers " + My.Resources.ffmpeg_user_agend + " -i " + Chr(34) + SoftSub(0) + Chr(34)
                        End If
                        If FunimationAudioMap = Nothing Then
                            SoftSubMergeMaps = SoftSubMergeMaps + " -map " + (MapCount + 1).ToString
                        Else
                            SoftSubMergeMaps = SoftSubMergeMaps + " -map " + (MapCount + 2).ToString
                        End If
                        If SoftSubMergeMetatata = Nothing Then
                            'SoftSubMergeMetatata = " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSub(1))
                            SoftSubMergeMetatata = " -metadata:s:s:" + MapCount.ToString + " language=" + Main.CCtoMP4CC(SoftSub(1)) + " -metadata:s:s:" + MapCount.ToString + " title=" + Chr(34) + Main.HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34) + " -metadata:s:s:" + MapCount.ToString + " handler_name=" + Chr(34) + Main.HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34)
                        Else
                            SoftSubMergeMetatata = SoftSubMergeMetatata + " -metadata:s:s:" + MapCount.ToString + " language=" + Main.CCtoMP4CC(SoftSub(1)) + " -metadata:s:s:" + MapCount.ToString + " title=" + Chr(34) + Main.HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34) + " -metadata:s:s:" + MapCount.ToString + " handler_name=" + Chr(34) + Main.HardSubValuesToDisplay(Chr(34) + SoftSub(1) + Chr(34)) + Chr(34)
                            'SoftSubMergeMetatata + " -metadata:s:s:" + i.ToString + " language=" + CCtoMP4CC(SoftSubs2(i))
                        End If
                    Next
                    If DispositionIndex < 999 Then
                        SoftSubMergeMetatata = SoftSubMergeMetatata + " -disposition:s:" + DispositionIndex.ToString + " default"
                    End If
                Else
                    Dim SubsClient As New WebClient
                    SubsClient.Encoding = Encoding.UTF8
                    If Not Main.WebbrowserCookie = Nothing Then
                        SubsClient.Headers.Add(HttpRequestHeader.Cookie, Main.WebbrowserCookie)
                    ElseIf Not Main.SystemWebBrowserCookie = Nothing Then
                        SubsClient.Headers.Add(HttpRequestHeader.Cookie, Main.SystemWebBrowserCookie)
                    End If
                    For i As Integer = 0 To UsedSubs.Count - 1
                        Main.LabelUpdate = "Status: downloading subtitle file"
                        Main.LabelEpisode = UsedSubs(i)
                        Dim SoftSub As String() = UsedSubs.Item(i).Split(New String() {" , "}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim SoftSub_3 As String = SoftSub(0).Replace("\/", "/")
                        Dim Subfile As String = SubsClient.DownloadString(SoftSub_3)
                        Dim Pfad3 As String = DownloadPfad.Replace(Chr(34), "")
                        'MsgBox(FN)
                        Dim SubtitelFormat As String = "srt"
                        If CBool(InStr(SoftSub_3, ".vtt")) Then
                            SubtitelFormat = "vtt"
                        End If
                        Dim FN As String = Path.ChangeExtension(Path.Combine(Path.GetFileNameWithoutExtension(Pfad3) + " " + SoftSub(1) + Path.GetExtension(Pfad3)), SubtitelFormat)
                        If i = 0 Then
                            FN = Path.ChangeExtension(Path.GetFileName(Pfad3), SubtitelFormat)
                            'MsgBox(FN)
                        End If
                        Dim Pfad4 As String = Path.Combine(Path.GetDirectoryName(Pfad3), FN)
                        'MsgBox(Pfad4)
                        Debug.WriteLine(Pfad4)
                        'File.WriteAllText(Pfad4, Subfile, Encoding.UTF8)
                        WriteText(Pfad4, Subfile)
                        Pause(1)
                    Next
                End If
            End If
#End Region
#Region "ffmpeg command"
            Dim DubMetatata As String = Nothing
            If FunimationDub = "Japanese" Then
                DubMetatata = " -metadata:s:a:0 language=jpn"
            ElseIf FunimationDub = "Portuguese (Brazil)" Then
                DubMetatata = " -metadata:s:a:0 language=por"
            ElseIf FunimationDub = "Spanish (Latin Am)" Then
                DubMetatata = " -metadata:s:a:0 language=spa"
            Else '
                DubMetatata = " -metadata:s:a:0 language=eng"
            End If
            Dim mergeSubs = outputFormat.GetSubtitleFormat() <> Format.SubtitleMerge.DISABLED
            Dim isAudioOnly = outputFormat.GetVideoFormat() = Format.MediaFormat.AAC_AUDIO_ONLY
            If HardSubFound = True And Not isAudioOnly Then
                Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + FunimationAudioMap + " -vf subtitles=" + Chr(34) + UsedSub + Chr(34) + " " + ffmpeg_hardsub
            ElseIf mergeSubs Then
                Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + FunimationAudioMap + SoftSubMergeURLs + SoftSubMergeMaps + " " + Main.ffmpeg_command + " -c:s " + Main.MergeSubsFormat + SoftSubMergeMetatata + DubMetatata
            ElseIf isAudioOnly Then
                If FunimationAudioMap = Nothing Then
                    Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + DubMetatata + " " + ffmpeg_command_temp
                Else
                    Funimation_m3u8_final = FunimationAudioMap.Replace(" -headers " + My.Resources.ffmpeg_user_agend + " ", "") + DubMetatata + " " + ffmpeg_command_temp
                End If
            Else
                Funimation_m3u8_final = "-i " + Chr(34) + Funimation_m3u8_final + Chr(34) + FunimationAudioMap + DubMetatata + " " + Main.ffmpeg_command
            End If
            Funimation_m3u8_final = Funimation_m3u8_final + " -metadata:g encoding_tool=CrD_Funimation_JS"
#End Region
            If Main.DownloadScope = 1 Then
                Funimation_m3u8_final = "-i [Subtitles only]"
            End If
            Dim L1Name_Split As String() = Main.WebbrowserURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim L1Name As String = L1Name_Split(1).Replace("www.", "") + " | Dub : " + FunimationDub
            Main.Invoke(Sub()
                            Main.ListItemAdd(DownloadPfad, L1Name, DefaultName, ResoHTMLDisplay, Funimation_m3u8_MainVersion, thumbnail4, Funimation_m3u8_final, DownloadPfad, "FM")
                        End Sub)
            'liList.Add(My.Resources.htmlvorThumbnail + thumbnail4 + My.Resources.htmlnachTumbnail + FunimationTitle + " <br> " + FunimationSeason + " " + FunimationEpisode + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + "null" + My.Resources.htmlnachHardSubs + "<!-- " + DefaultName + "-->")
#End Region
            ' TODO: Maybe want to have a setIdle() method in main for this?
            Main.Invoke(Sub()
                            Main.Text = "Crunchyroll Downloader"
                            ' Anime_Add.StatusLabel.Text = "idle"
                            Main.ResoBackString = Nothing
                            Main.Invalidate()
                        End Sub)
        Catch ex As Exception
            Main.Invoke(Sub()
                            Main.Text = "Crunchyroll Downloader!"
                            'Anime_Add.StatusLabel.Text = "idle"
                            Main.ResoBackString = Nothing
                            Main.Invalidate()
                        End Sub)
            MsgBox(ex.ToString)
        End Try
        Funimation_Grapp_RDY = True
    End Sub



#End Region

End Class
