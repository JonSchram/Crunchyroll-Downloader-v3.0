Imports System.IO
Imports System.Net
Imports System.Threading
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Status
Imports Crunchyroll_Downloader.CRD_Classes
Imports Crunchyroll_Downloader.settings
Imports Newtonsoft.Json.Linq

Public Class CrunchyrollDownloader
    Public Async Sub DownloadBetaSeasons(episodeJson As String, startEpisode As Integer, endEpisode As Integer)
        Try
            Dim ListOfEpisodes As New List(Of String)

            For i As Integer = 0 To Main.CR_MassEpisodes.Count - 1

                If Main.Url_locale = "" Then
                    ListOfEpisodes.Add("https://www.crunchyroll.com/watch/" + Main.CR_MassEpisodes.Item(i).guid + "/" + Main.CR_MassEpisodes.Item(i).audio_locale + "/")
                Else
                    ListOfEpisodes.Add("https://www.crunchyroll.com/" + Main.Url_locale + "/" + "watch/" + Main.CR_MassEpisodes.Item(i).guid + "/" + Main.CR_MassEpisodes.Item(i).audio_locale + "/")
                End If

            Next
            Dim First As Integer = Math.Min(startEpisode, endEpisode)
            Dim Last As Integer = Math.Max(startEpisode, endEpisode)
            Dim numberOfEpisodes As Integer = Last - First + 1

            Dim settings As ProgramSettings = ProgramSettings.GetInstance()

            ' TODO:
            ' Seems to be an improvsed task that updates the number of downloading episodes?
            ' Very much duplicated from funimation code. Needs to be shared.
            For i As Integer = First To Last
                For e As Integer = 0 To Integer.MaxValue
                    If Main.Grapp_RDY = True Then
                        Try
                            Dim ItemFinshedCount As Integer = 0 '
                            Dim Item As New List(Of CRD_List_Item)
                            Item.AddRange(Main.Panel1.Controls.OfType(Of CRD_List_Item))
                            Item.Reverse()

                            For i2 As Integer = 0 To Item.Count - 1
                                If Item(i2).GetIsStatusFinished() = True Then
                                    ItemFinshedCount = ItemFinshedCount + 1
                                End If
                            Next
                            Main.RunningDownloads = Main.Panel1.Controls.Count - ItemFinshedCount
                        Catch ex As Exception
                            Main.RunningDownloads = Main.Panel1.Controls.Count
                        End Try
                        If Main.RunningDownloads < settings.SimultaneousDownloads Then
                            Exit For
                        Else
                            'MsgBox(e)
                            Await Task.Delay(1000)
                        End If
                    Else
                        Await Task.Delay(5000)
                    End If
                Next
                'If Anime_Add.Mass_DL_Cancel = False Then
                '    Main.b = True
                '    Exit For
                '    Main.Grapp_Abord = True
                '    Main.Grapp_RDY = True
                'End If

                If settings.UseDownloadQueue Then
                    'Anime_Add.ListBox1.Items.Add(ListOfEpisodes(i))
                    Main.ListBoxList.Add(ListOfEpisodes(i))
                    ' Color is #8D1D2C
                    'Anime_Add.Add_Display.ForeColor = Color.FromArgb(9248044)
                    Pause(1)
                    'Anime_Add.Add_Display.ForeColor = Color.Black
                Else
                    Main.Grapp_RDY = False
                    Main.b = False
                    Debug.WriteLine("b: " + Main.b.ToString)
                    Main.LoadBrowser(ListOfEpisodes(i))
                End If
                'Anime_Add.Add_Display.Text = (i - First + 1).ToString + " / " + (Last - First + 1).ToString
            Next
        Catch ex As Exception
            If Main.Debug2 = True Then
                MsgBox(ex.ToString)
            End If
            'Anime_Add.comboBox4.Items.Clear()
            'Anime_Add.comboBox3.Items.Clear()
            'Anime_Add.btn_dl.Text = "Download" 'btn_dl.BackgroundImage = My.Resources.main_button_download_default
        End Try
        Pause(5)
        'Anime_Add.groupBox1.Visible = True
        'Anime_Add.groupBox2.Visible = False
        'Anime_Add.Mass_DL_Cancel = False
        'Anime_Add.btn_dl.Text = "Download" 'Anime_Add.btn_dl.BackgroundImage = My.Resources.main_button_download_default
    End Sub

    Public Sub GetBetaSeasons(ByVal AnimeUrl As String, ByVal JsonUrl As String, ByVal Auth As String, Optional ByVal BrowserData As String = Nothing) ', ByVal SeasonJson As String)
        'Anime_Add.groupBox2.Visible = True
        'Anime_Add.bt_Cancel_mass.Enabled = True
        'Anime_Add.bt_Cancel_mass.Visible = True
        'Anime_Add.groupBox1.Visible = False
        'Anime_Add.ComboBox1.Items.Clear()
        'Anime_Add.comboBox3.Items.Clear()
        'Anime_Add.comboBox4.Items.Clear()
        'Anime_Add.ComboBox1.Text = Nothing
        'Anime_Add.comboBox3.Text = Nothing
        'Anime_Add.comboBox4.Text = Nothing
        'Anime_Add.ComboBox1.Enabled = True
        'Anime_Add.comboBox3.Enabled = True
        'Anime_Add.comboBox4.Enabled = True
        Dim SeasonJson As String = Nothing
        Main.CR_MassSeasons.Clear()
        If BrowserData = Nothing Then

            Dim Loc_CR_Cookies = " -H " + """" + Main.CR_Cookies.Replace("""", "").Replace(" -H ", "") + """"

            SeasonJson = Main.CurlAuth(JsonUrl, Loc_CR_Cookies, Auth)

            If CBool(InStr(SeasonJson, "curl:")) = True Then
                'Browser.WebView2.CoreWebView2.Navigate(Url)
                Main.SetStatusLabel("Status: Critical error. #1091")
                Exit Sub
            End If


        Else
            SeasonJson = BrowserData
            Debug.WriteLine("BrowserData: " + BrowserData)
        End If


        SeasonJson = CleanJSON(SeasonJson)

        Dim SeasonJObject As JObject = JObject.Parse(SeasonJson)
        Dim SeasonData As List(Of JToken) = SeasonJObject.Children().ToList

        For Each item As JProperty In SeasonData
            item.CreateReader()
            Select Case item.Name
                Case "data" 'each record is inside the entries array
                    For Each Entry As JObject In item.Values
                        Dim SeasonSubData As List(Of JToken) = Entry.Children().ToList
                        Dim localSeasons As New List(Of CR_Seasons)
                        Dim season_number As String = Nothing
                        Dim id As String = Nothing
                        Dim audio_localeMain As String = Nothing


                        For Each SeasonSubItem As JProperty In SeasonSubData
                            SeasonSubItem.CreateReader()

                            Select Case SeasonSubItem.Name
                                Case "versions"
                                    Try
                                        For Each VersionItem As JObject In SeasonSubItem.Values

                                            Dim guid As String = VersionItem.GetValue("guid").ToString
                                            Dim audio_locale As String = VersionItem.GetValue("audio_locale").ToString

                                            localSeasons.Add(New CR_Seasons(guid, audio_locale, Auth))
                                        Next
                                    Catch ex As Exception
                                        Debug.WriteLine("Error getting season data")
                                    End Try
                                Case "season_number"
                                    season_number = SeasonSubItem.Value.ToString
                                Case "id"
                                    id = SeasonSubItem.Value.ToString
                                Case "audio_locale"
                                    audio_localeMain = SeasonSubItem.Value.ToString
                            End Select
                        Next

                        If localSeasons.Count = 0 Then
                            ' TODO
                            'Anime_Add.ComboBox1.Items.Add(Main.HardSubValuesToDisplay(audio_localeMain) + " - Season " + season_number)
                            Main.CR_MassSeasons.Add(New CR_Seasons(id, audio_localeMain, Auth))
                        End If

                        If localSeasons.Count > 0 Then
                            For i As Integer = 0 To Main.CR_MassSeasons.Count - 1
                                If Main.CR_MassSeasons.Item(i).guid = localSeasons.Item(0).guid Then
                                    localSeasons.Clear()
                                    Exit For
                                End If
                            Next
                        End If


                        If localSeasons.Count > 0 Then
                            For i As Integer = 0 To localSeasons.Count - 1
                                ' TODO
                                'Anime_Add.ComboBox1.Items.Add(Main.HardSubValuesToDisplay(localSeasons.Item(i).audio_locale) + " - Season " + season_number)
                                Main.CR_MassSeasons.Add(localSeasons.Item(i))
                            Next
                        End If

                    Next
            End Select
        Next

    End Sub


    Public Sub GetBetaVideoProxy(ByVal requesturl As String, ByVal AuthToken As String, ByVal WebsiteURL As String)
        Dim Evaluator = New Thread(Sub() Me.GetBetaVideo(requesturl, AuthToken, WebsiteURL))
        Evaluator.Start()
    End Sub

    Public Sub GetBetaVideo(ByVal Streams As String, ByVal AuthToken As String, ByVal WebsiteURL As String)
        If Main.b = False Then
            Main.b = True
        End If
        'Debug.WriteLine(Streams)
        'Debug.WriteLine(vbCrLf)
        Debug.WriteLine("Website: " + WebsiteURL)

        Try
            Dim settings = ProgramSettings.GetInstance()
            Main.Grapp_RDY = False
            Dim ffmpeg_command_temp As String = Main.ffmpeg_command
            Dim CR_MetadataUsage As Boolean = False
            Dim CR_Streams As New List(Of CR_Beta_Stream)
            Dim CR_series_title As String = Nothing
            Dim CR_season_number As String = Nothing
            Dim CR_FolderSeason As String = Nothing
            Dim CR_episode As String = Nothing
            Dim CR_episode_duration_ms As String = "60000000"
            Dim CR_episode2 As String = Nothing
            Dim CR_Anime_Staffel_int As String = Nothing
            Dim CR_episode_int As String = Nothing
            Dim CR_title As String = Nothing
            Dim CR_audio_locale As String = Nothing
            Dim CR_audio_isDubbed As Boolean = False
            Dim ResoUsed As String = "x" + settings.DownloadResolution.ToString
            Dim ffmpegInput As String = "-i [Subtitles only]"


            Dim Pfad2 As String
            Dim TextBox2_Text As String = Nothing
            Dim CR_FilenName As String = Nothing
            Dim ObjectJson As String = Nothing
            ' TODO
            Main.Invoke(Sub()
                            TextBox2_Text = "TODO - get anime being added" 'Anime_Add.TextBox2.Text
                        End Sub)


            Dim ObjectsURLBuilder() As String = Streams.Split(New String() {"videos"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ObjectsURLBuilder2() As String = ObjectsURLBuilder(1).Split(New String() {"/streams"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ObjectsURLBuilder3() As String = WebsiteURL.Split(New String() {"watch/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ObjectsURLBuilder4() As String = ObjectsURLBuilder3(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ObjectsURL As String = ObjectsURLBuilder(0) + "objects/" + ObjectsURLBuilder4(0) + ObjectsURLBuilder2(1)
            Debug.WriteLine(ObjectsURL)

            Dim Loc_CR_Cookies = " -H " + """" + Main.CR_Cookies + """"

            Dim Loc_AuthToken = " -H " + """" + "Authorization: " + AuthToken + """"

            If CBool(InStr(AuthToken, "Authorization")) = True Then
                Loc_AuthToken = AuthToken
            End If


            ObjectJson = Main.CurlAuth(ObjectsURL, Loc_CR_Cookies, Loc_AuthToken)



            'ObjectJson = Curl(ObjectsURL)

            'MsgBox(ObjectJson)

            If CBool(InStr(ObjectJson, "curl:")) = True Then
                ObjectJson = Main.CurlAuth(ObjectsURL, Loc_CR_Cookies, Loc_AuthToken)
            End If



            If CBool(InStr(ObjectJson, "curl:")) = True And CBool(InStr(Main.CR_ObjectsJson.Url, ObjectsURLBuilder4(0))) Then
                Debug.WriteLine("curl error, using CR_ObjectsJson " + vbNewLine + ObjectJson)

                ObjectJson = Main.CR_ObjectsJson.Content
                Main.CR_ObjectsJson = New UrlJson("", "")
            ElseIf CBool(InStr(ObjectJson, "curl:")) Then

                Throw New System.Exception("Error - Getting ObjectJson data" + vbNewLine + ObjectJson)

                'MsgBox("Error - Getting ObjectJson data" + vbNewLine + ObjectJson)
                'Exit Sub
            End If



            'Filter JSON esqaped characters
            'Debug.WriteLine(Date.Now.ToString + "before:" + ObjectJson)
            Debug.WriteLine("1288: " + ObjectJson)
            ObjectJson = CleanJSON(ObjectJson)
            'Debug.WriteLine(Date.Now.ToString + "after:" + ObjectJson)

            Dim ser As JObject = JObject.Parse(ObjectJson)
            Dim data As List(Of JToken) = ser.Children().ToList

            For Each item As JProperty In data
                item.CreateReader()
                Select Case item.Name

                    Case "data" 'each record is inside the entries array
                        For Each Entry As JObject In item.Values
                            Try
                                Dim Title As String = Entry("title").ToString
                                CR_title = String.Join(" ", Title.Split(Main.invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c).Replace("""", "").Replace("\", "").Replace("/", "").Replace(":", "")
                                Debug.WriteLine(Date.Now.ToString + " CR-Title: " + CR_title)
                            Catch ex As Exception
                            End Try
                            Dim SubData As List(Of JToken) = Entry.Children().ToList
                            For Each SubItem As JProperty In SubData
                                'SubItem.CreateReader()
                                Select Case SubItem.Name
                                    Case "episode_metadata"
                                        For Each SubEntry As JProperty In SubItem.Values
                                            Select Case SubEntry.Name
                                                Case "series_title"
                                                    CR_series_title = SubEntry.Value.ToString.Replace("""", "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                    'Case "season_title"
                                                    '    CR_season_title = SubEntry.Value.ToString
                                                Case "season_number"
                                                    CR_season_number = SubEntry.Value.ToString.Replace("""", "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                Case "episode_number"
                                                    CR_episode2 = SubEntry.Value.ToString.Replace("""", "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                Case "episode"
                                                    CR_episode = SubEntry.Value.ToString.Replace("""", "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                Case "duration_ms"
                                                    CR_episode_duration_ms = SubEntry.Value.ToString.Replace("""", "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                Case "is_dubbed"
                                                    CR_audio_isDubbed = CBool(SubEntry.Value.ToString.Replace("""", "").Replace("\", "").Replace("/", "").Replace(":", ""))
                                            End Select
                                        Next '
                                End Select
                            Next
                        Next
                End Select
            Next

#Region "VideoJson"
            Dim VideoJson As String = Nothing

            VideoJson = Main.CurlAuth(Streams, Loc_CR_Cookies, Loc_AuthToken)



            'VideoJson = Curl(Streams)

            If CBool(InStr(VideoJson, "curl:")) = True Then
                VideoJson = Main.CurlAuth(Streams, Loc_CR_Cookies, Loc_AuthToken)

            End If

            Dim StreamsUrlBuilder() As String = ObjectJson.Split(New String() {"videos/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim StreamsUrlBuilder2() As String = StreamsUrlBuilder(1).Split(New String() {"/streams"}, System.StringSplitOptions.RemoveEmptyEntries)



            If CBool(InStr(VideoJson, "curl:")) = True And CBool(InStr(Main.CR_VideoJson.Url, StreamsUrlBuilder2(0))) Then
                Debug.WriteLine("curl error, using CR_VideoJson " + vbNewLine + VideoJson)
                VideoJson = Main.CR_VideoJson.Content
                Main.CR_VideoJson = New UrlJson("", "")
            ElseIf CBool(InStr(VideoJson, "curl:")) = True Then
                Throw New System.Exception("Error - Getting VideoJson data" + vbNewLine + VideoJson) 'VideoJson = Nothing
                ' MsgBox("Error - Getting VideoJson data" + vbNewLine + VideoJson)
                '  Exit Sub
            End If


            Debug.WriteLine("VideoJson: " + VideoJson)
            Debug.WriteLine("VideoStreams: " + Streams)


            Dim CR_HardSubLang As String = Main.SubSprache.CR_Value
#End Region

#Region "m3u8 suche"


            VideoJson = CleanJSON(VideoJson)

            Dim VideoJObject As JObject = JObject.Parse(VideoJson)
            Dim VideoData As List(Of JToken) = VideoJObject.Children().ToList

            For Each item As JProperty In VideoData
                item.CreateReader()
                Select Case item.Name
                    Case "data" 'each record is inside the entries array
                        For Each Entry As JObject In item.Values

                            Dim VideoSubData As List(Of JToken) = Entry.Children().ToList
                            For Each VideoSubItem As JProperty In VideoSubData

                                Dim JsonEntryFormat As String = VideoSubItem.Name
                                If CBool(InStr(JsonEntryFormat, "drm")) Or CBool(InStr(JsonEntryFormat, "dash")) Or CBool(InStr(JsonEntryFormat, "download")) Or CBool(InStr(JsonEntryFormat, "urls")) Then '
                                    Continue For
                                End If

                                Dim SubData As List(Of JToken) = VideoSubItem.Children().ToList
                                For Each SubItem As JObject In SubData
                                    SubItem.CreateReader()

                                    Dim StreamFormats As List(Of JToken) = SubItem.Children().ToList


                                    For Each HardsubStreams As JProperty In StreamFormats
                                        HardsubStreams.CreateReader()
                                        Dim SubLang As String = HardsubStreams.Name
                                        Dim Url As String = HardsubStreams.Value("url").ToString
                                        If SubLang = Nothing Or SubLang = "" Then
                                            SubLang = ""
                                        End If

                                        CR_Streams.Add(New CR_Beta_Stream(SubLang, JsonEntryFormat, Url))

                                    Next
                                Next


                            Next
                        Next
                    Case "meta" 'each record is inside the entries array
                        For Each MetaEntrys As JProperty In item.Values
                            Select Case MetaEntrys.Name
                                Case "audio_locale"
                                    If CR_audio_isDubbed = True Then
                                        Dim AudioTag As String = MetaEntrys.Value.ToString
                                        CR_audio_locale = String.Join(" ", AudioTag.Split(Main.invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c).Replace("""", "").Replace("\", "").Replace("/", "").Replace(":", "")
                                    Else
                                        CR_audio_locale = "ja-JP"
                                    End If

                            End Select

                        Next

                End Select
            Next

            Dim CR_URI_Master As New List(Of String)


            Dim RawStream As New List(Of String)



            For c As Integer = 0 To CR_Streams.Count - 1
                Dim i As Integer = c
                'Debug.WriteLine("1457: " + i.ToString + "/" + CR_Streams.Count.ToString + " " + CR_Streams.Item(i).subLang + " " + CR_Streams.Item(i).Format)
                If CR_Streams.Item(i).subLang = CR_HardSubLang Then
                    CR_URI_Master.Add(CR_Streams.Item(i).Url)
                ElseIf CR_Streams.Item(i).subLang = "" And CR_audio_locale IsNot "ja-JP" And Main.DubMode = True Then 'nothing/raw
                    RawStream.Add(CR_Streams.Item(i).Url)
                End If
            Next

            If CR_URI_Master.Count = 0 And RawStream.Count > 0 Then
                CR_URI_Master.Clear()
                CR_URI_Master.AddRange(RawStream)

            ElseIf CR_URI_Master.Count = 0 Then
                Main.Invoke(Sub()
                                Main.ResoNotFoundString = VideoJson
                                Main.DialogTaskString = "Language_CR_Beta"
                                ErrorDialog.ShowDialog()
                            End Sub)
                If Main.UserCloseDialog = True Then
                    Throw New System.Exception("""" + "UserAbort" + """")
                Else
                    'MsgBox(CR_HardSubLang)
                    CR_HardSubLang = Main.ResoBackString
                    Main.ResoBackString = Nothing
                    'MsgBox(CR_Streams.Count.ToString)
                    For i As Integer = 0 To CR_Streams.Count - 1
                        Debug.WriteLine("1484: " + CR_Streams.Item(i).subLang)
                        If CR_Streams.Item(i).subLang = CR_HardSubLang Then
                            CR_URI_Master.Add(CR_Streams.Item(i).Url)
                        End If

                    Next

                End If
            End If

            'MsgBox(CR_URI_Master.Count.ToString)

            If CBool(InStr(CR_URI_Master(0), "master.m3u8")) Then
                ' TODO
                Main.Invoke(Sub()
                                ' TODO
                                'Anime_Add.StatusLabel.Text = "Status: m3u8 found, looking for resolution"
                                Main.Text = "Status: m3u8 found, looking for resolution"
                                Main.Invalidate()
                            End Sub)
            Else
                Throw New System.Exception("Premium Episode")
            End If

#End Region

#Region "Name"

#Region "Name von Crunchyroll"


            If CR_episode = Nothing Or CR_episode = "" And CR_episode2 = Nothing Then
                CR_episode_int = "0"
            ElseIf CR_episode IsNot Nothing And CR_episode IsNot "" Then
                CR_episode_int = CR_episode
            ElseIf CR_episode2 IsNot Nothing Then
                CR_episode_int = CR_episode2
            End If
            CR_Anime_Staffel_int = CR_season_number



            If TextBox2_Text = Nothing Or TextBox2_Text = "Use Custom Name" Or CBool(InStr(TextBox2_Text, "++")) = True Then


                If Main.Season_Prefix = "[default season prefix]" Then
                    If CR_episode = Nothing And CR_episode2 = Nothing Then 'no episode number means most likey a movie 
                        CR_season_number = Nothing
                    ElseIf CR_season_number = Nothing Then
                    Else
                        CR_season_number = "Season " + CR_season_number
                    End If
                Else
                    If CR_episode = Nothing And CR_episode2 = Nothing Then 'no episode number means most likey a movie 
                        CR_season_number = Nothing
                    ElseIf CR_season_number = Nothing Then
                    Else
                        CR_season_number = Main.Season_Prefix + CR_season_number
                    End If
                End If

                CR_FolderSeason = CR_season_number

                If Main.IgnoreSeason = 1 And CR_season_number = "1" Or Main.IgnoreSeason = 1 And CR_season_number = "0" Then
                    CR_season_number = Nothing
                ElseIf Main.IgnoreSeason = 2 Then
                    CR_season_number = Nothing
                End If


                If Main.Episode_Prefix = "[default episode prefix]" Then
                    If CR_episode = Nothing Or CR_episode = "" And CR_episode2 = Nothing Then
                        CR_episode = CR_title
                    ElseIf CR_episode IsNot Nothing And CR_episode IsNot "" Then
                        CR_episode = "Episode " + Main.AddLeadingZeros(CR_episode)
                    ElseIf CR_episode2 IsNot Nothing Then
                        CR_episode = "Episode " + Main.AddLeadingZeros(CR_episode2)
                    End If
                    'CR_episode = "Episode " + AddLeadingZeros(CR_episode)
                Else
                    CR_episode = Main.Episode_Prefix + Main.AddLeadingZeros(CR_episode)
                End If


                Dim NameParts As String() = Main.NameBuilder.Split(New String() {";"}, System.StringSplitOptions.RemoveEmptyEntries)

                For i As Integer = 0 To NameParts.Count - 1

                    If NameParts(i) = "AnimeTitle" Then
                        CR_FilenName = CR_FilenName + " " + CR_series_title
                    ElseIf NameParts(i) = "Season" Then
                        CR_FilenName = CR_FilenName + " " + CR_season_number
                    ElseIf NameParts(i) = "EpisodeNR" Then
                        CR_FilenName = CR_FilenName + " " + CR_episode
                    ElseIf NameParts(i) = "EpisodeName" Then
                        CR_FilenName = CR_FilenName + " " + CR_title
                    ElseIf NameParts(i) = "AnimeDub" Then
                        CR_FilenName = CR_FilenName + " " + Main.HardSubValuesToDisplay(CR_audio_locale)
                    ElseIf NameParts(i) = "AnimeSub" Then
                        ' CR_FilenName = CR_FilenName + " RepSub" 'to be done
                    End If

                Next

                If CBool(InStr(TextBox2_Text, "++")) = True Then
                    Dim Backup_CR_FilenName As String = CR_FilenName
                    Try
                        Dim AddDef As String = "CRD"
                        Dim TestString As String = AddDef + TextBox2_Text + AddDef
                        Dim PrePost As String() = TestString.Split(New String() {"++"}, System.StringSplitOptions.RemoveEmptyEntries)
                        CR_FilenName = PrePost(0) + CR_FilenName + PrePost(1)
                        CR_FilenName = CR_FilenName.Replace(AddDef, "")
                    Catch ex As Exception
                        CR_FilenName = Backup_CR_FilenName
                    End Try
                End If






#End Region
            Else
                CR_FilenName = Main.RemoveExtraSpaces(String.Join(" ", TextBox2_Text.Split(Main.invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)).Replace("""", "").Replace("\", "").Replace("/", "") 'System.Text.RegularExpressions.Regex.Replace(TextBox2_Text, "[^\w\\-]", " "))
            End If

            If Main.KodiNaming = True Then
                Dim KodiString As String = "[S"
                If CR_Anime_Staffel_int = "0" Then
                    CR_Anime_Staffel_int = "01"
                Else
                    CR_Anime_Staffel_int = "0" + CR_Anime_Staffel_int
                End If

                KodiString = KodiString + CR_Anime_Staffel_int + " E" + Main.AddLeadingZeros(CR_episode_int) ' CR_episode_nr
                KodiString = KodiString + "] "
                CR_FilenName = KodiString + CR_FilenName
            End If
            Debug.WriteLine(CR_FilenName)

            CR_FilenName = String.Join(" ", CR_FilenName.Split(Main.invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c).Replace("""", "").Replace("\", "").Replace("/", "") 'System.Text.RegularExpressions.Regex.Replace(CR_FilenName, "[^\w\\-]", " ")
            CR_FilenName = Main.RemoveExtraSpaces(CR_FilenName)
            'My.Computer.FileSystem.WriteAllText("log.log", WebbrowserText, False)
            Pfad2 = UseSubfolder(CR_series_title, CR_FolderSeason, Main.Pfad)
            Dim extension = settings.OutputFormat.GetFileExtension()
            If Not Directory.Exists(Path.GetDirectoryName(Pfad2)) Then
                ' Nein! Jetzt erstellen...
                Try
                    Directory.CreateDirectory(Path.GetDirectoryName(Pfad2))
                    Pfad2 = """" + Pfad2 + CR_FilenName + "." + extension + """"
                Catch ex As Exception
                    ' Ordner wurde nich erstellt
                    Pfad2 = """" + Main.Pfad + "\" + CR_FilenName + "." + extension + """"
                    Pfad2 = Pfad2.Replace("\\", "\")
                End Try
            Else
                Pfad2 = """" + Pfad2 + CR_FilenName + "." + extension + """"
            End If
#End Region



#Region "Chapters"
            Dim Mdata_File As String = Application.StartupPath + "\" + ObjectsURLBuilder4(0) + "-mdata.txt"

            If Main.CR_Chapters = True Then


                Dim ChaptersUrl As String = "https://static.crunchyroll.com/datalab-intro-v2/" + ObjectsURLBuilder4(0) + ".json"
                Dim ChaptersJson As String = Nothing


                ChaptersJson = Main.Curl(ChaptersUrl)

                If CBool(InStr(ChaptersJson, "curl:")) = True Then
                    ChaptersJson = Main.Curl(ChaptersUrl)
                End If

                If CBool(InStr(ChaptersJson, "curl:")) = True Then
                    ChaptersJson = Nothing
                    Debug.WriteLine("no Chapter data... ignoring")
                End If
                If ChaptersJson IsNot Nothing Then

                    Dim StartTime As String() = ChaptersJson.Split(New String() {"""" + "startTime" + """" + ": "}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim StartTime2 As String() = StartTime(1).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim StartTime3 As String() = StartTime2(0).Split(New String() {"."}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim StartTime4 As String = StartTime3(1)

                    For i As Integer = StartTime4.Length To 2
                        StartTime4 = StartTime4 + "0"
                    Next

                    Dim StartTime_ms As String = StartTime3(0) + StartTime4


                    Dim EndTime As String() = ChaptersJson.Split(New String() {"""" + "endTime" + """" + ": "}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim EndTime2 As String() = EndTime(1).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim EndTime3 As String() = EndTime2(0).Split(New String() {"."}, System.StringSplitOptions.RemoveEmptyEntries)

                    Dim EndTime4 As String = EndTime3(1)
                    Dim AfterTime As String = Nothing

                    For i As Integer = EndTime4.Length To 2
                        If EndTime4.Length = 2 Then
                            AfterTime = EndTime4 + "1"
                        End If
                        EndTime4 = EndTime4 + "0"
                    Next

                    Dim EndTime_ms As String = EndTime3(0) + EndTime4
                    Dim AfterTime_ms As String = EndTime3(0) + AfterTime
                    Dim Metadata As String = Nothing

                    If CInt(CR_episode_duration_ms) < CInt(StartTime_ms) Then
                        'Totaly invalid...
                    ElseIf CInt(CR_episode_duration_ms) < CInt(EndTime_ms) Then
                        'it's not an Intro it's an outro 
                        Dim DeCh As Integer = CInt(StartTime_ms) - 1
                        Metadata = My.Resources.ffmpeg_metadata_out.Replace("[Titel]", CR_FilenName).Replace("[Start-1]", DeCh.ToString).Replace("[Start]", StartTime_ms).Replace("[duration_ms]", CR_episode_duration_ms)

                    Else
                        Metadata = My.Resources.ffmpeg_metadata.Replace("[Titel]", CR_FilenName).Replace("[Start]", StartTime_ms).Replace("[END]", EndTime_ms).Replace("[after]", AfterTime_ms).Replace("[duration_ms]", CR_episode_duration_ms)

                    End If

                    If Metadata = Nothing Then
                    Else
                        Dim utf8WithoutBom2 As New System.Text.UTF8Encoding(False)
                        Using sink As New StreamWriter(Mdata_File, False, utf8WithoutBom2)
                            sink.WriteLine(Metadata)
                            CR_MetadataUsage = True
                        End Using
                    End If


                End If
            End If
#End Region





#Region "lösche doppel download"
            'MsgBox(Pfad2)
            Dim Pfad5 As String = Pfad2.Replace("""", "")
            Dim Pfad6 As String = Pfad5
            Dim MergeAudio As Boolean = False

            If My.Computer.FileSystem.FileExists(Pfad5) And Main.DownloadScope = DownloadScopeEnum.OldDefault Then 'Pfad = Kompeltter Pfad mit Dateinamen + ENdung
                ' TODO
                Main.Invoke(Sub()
                                ' Anime_Add.StatusLabel.Text = "Status: The file already exists."
                                Main.Text = "Status: The file already exists."
                                Main.Invalidate()
                            End Sub)
                If MessageBox.Show("The file " + Pfad5 + " already exists." + vbNewLine + "You want to override it?", "File exists!", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Try
                        My.Computer.FileSystem.DeleteFile(Pfad5)
                    Catch ex As Exception
                    End Try
                Else
                    Main.Grapp_RDY = True
                    Exit Sub
                End If

            ElseIf My.Computer.FileSystem.FileExists(Pfad5) And Main.DownloadScope = DownloadScopeEnum.MergeAudio Then


                Pfad6 = Path.GetDirectoryName(Pfad5) + "\" + GeräteID() + Path.GetExtension(Pfad5) '+ "."
                FileSystem.Rename(Pfad5, Pfad6)
                MergeAudio = True

            ElseIf My.Computer.FileSystem.FileExists(Path.GetDirectoryName(Pfad5) + "\" + Path.GetFileNameWithoutExtension(Pfad5) + "aac") And Main.DownloadScope = DownloadScopeEnum.AudioOnly Then

                ' TODO
                Main.Invoke(Sub()
                                'Anime_Add.StatusLabel.Text = "Status: The file already exists."
                                Main.Text = "Status: The file already exists."
                                Main.Invalidate()
                            End Sub)
                If MessageBox.Show("The file " + Path.GetDirectoryName(Pfad5) + "\" + Path.GetFileNameWithoutExtension(Pfad5) + "aac" + " already exists." + vbNewLine + "You want to override it?", "File exists!", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Try
                        My.Computer.FileSystem.DeleteFile(Path.GetDirectoryName(Pfad5) + "\" + Path.GetFileNameWithoutExtension(Pfad5) + "aac")
                    Catch ex As Exception
                    End Try
                Else
                    Main.Grapp_RDY = True
                    Exit Sub
                End If

            ElseIf Main.DownloadScope = DownloadScopeEnum.AudioOnly Then
                ' TODO: Build better path from the start.
                'replace format with aac
                Pfad2 = Pfad2.Replace("." + extension, ".aac")

                'replace command for aac
                Dim ffmpeg_command_Builder() As String = Main.ffmpeg_command.Split(New String() {"-c:a copy"}, System.StringSplitOptions.RemoveEmptyEntries)
                ffmpeg_command_temp = "-c:a copy" + ffmpeg_command_Builder(1)

            End If
#End Region


#Region "GetResolution"

            Dim downloadMode = settings.DownloadMode
            Dim resolution = settings.DownloadResolution
            If resolution = ProgramSettings.Resolution.AUTO And downloadMode = ProgramSettings.DownloadModeOptions.FFMPEG Then

                ffmpegInput = "-i " + """" + CR_URI_Master(0) + """"

            ElseIf Main.DownloadScope = DownloadScopeEnum.SubsOnly Then
                ffmpegInput = "-i [Subtitles only]"
            Else

                Dim str As String = Nothing




                For i As Integer = 0 To CR_URI_Master.Count - 1
                    str = Main.Curl(CR_URI_Master(i))

                    If CBool(InStr(str, "curl:")) = False Then
                        Exit For
                    End If
                Next


                If CBool(InStr(str, "curl:")) = True Then

                    MsgBox("Unable to get master.m3u8" + vbNewLine + str, MsgBoxStyle.Critical)
                ElseIf Main.DownloadScope = DownloadScopeEnum.AudioOnly Or MergeAudio = True Then

                    If CBool(InStr(str, "x480,")) Then
                        ResoUsed = "x480"
                    ElseIf CBool(InStr(str, "x" + settings.DownloadResolution.ToString + ",")) Then
                        ResoUsed = "x" + settings.DownloadResolution.ToString
                    End If

                ElseIf CBool(InStr(str, "x" + settings.DownloadResolution.ToString + ",")) Then
                    ResoUsed = "x" + settings.DownloadResolution.ToString
                Else
                    If CBool(InStr(str, Main.ResoSave + ",")) Then
                        ResoUsed = Main.ResoSave
                    Else
                        ' TODO
                        Main.Invoke(Sub()
                                        Main.DialogTaskString = "Resolution"
                                        Main.ResoNotFoundString = str
                                        ErrorDialog.ShowDialog()
                                    End Sub)
                        If Main.UserCloseDialog = True Then
                            Throw New System.Exception("""" + "UserAbort" + """")
                        Else
                            ResoUsed = Main.ResoBackString
                            Main.ResoSave = Main.ResoBackString
                        End If
                    End If
                End If

                Dim ffmpeg_url_3 As String = Nothing
                Dim LineChar As String = vbLf
                If CBool(InStr(str, vbCrLf)) Then
                    LineChar = vbCrLf
                ElseIf CBool(InStr(str, vbCr)) Then
                    LineChar = vbCr
                End If
                Dim ffmpeg_url_1 As String() = str.Split(New String() {LineChar}, System.StringSplitOptions.RemoveEmptyEntries)

                For i As Integer = 0 To ffmpeg_url_1.Count - 2 'Step 2
                    If CBool(InStr(ffmpeg_url_1(i), ResoUsed + ",")) Then
                        ffmpeg_url_3 = ffmpeg_url_1(i + 1)
                    End If
                Next

                ffmpegInput = "-i " + """" + ffmpeg_url_3.Trim() + """"

            End If



#End Region

#Region "GetSoftsubs"
            Dim SoftSubsAvailable As New List(Of String)

            Dim SoftSubsList As New List(Of CR_Subtiles)

            If Main.SoftSubs.Count > 0 Then
                For i As Integer = 0 To Main.SoftSubs.Count - 1
                    If CBool(InStr(VideoJson, """" + "locale" + """" + ":" + """" + Main.SoftSubs(i) + """" + "," + """" + "url" + """" + ":" + """")) Then
                        SoftSubsAvailable.Add(Main.SoftSubs(i))
                    End If
                Next
            End If


            If Main.DownloadScope = DownloadScopeEnum.AudioOnly Then


            ElseIf MergeAudio = True Then


            ElseIf SoftSubsAvailable.Count > 0 Then
                Dim outputFormat = settings.OutputFormat
                Dim MergeSubsNow As Boolean = outputFormat.GetSubtitleFormat() <> Format.SubtitleMerge.DISABLED

                If Main.DownloadScope = DownloadScopeEnum.SubsOnly Then
                    MergeSubsNow = False
                End If

                Debug.WriteLine("Softsubs Default: " + Main.DefaultSubCR)

                For i As Integer = 0 To SoftSubsAvailable.Count - 1

                    Dim SoftSub As String() = VideoJson.Split(New String() {"""" + "locale" + """" + ":" + """" + SoftSubsAvailable(i) + """" + "," + """" + "url" + """" + ":" + """"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim SoftSub_2 As String() = SoftSub(1).Split(New [Char]() {""""})
                    Dim SoftSub_3 As String = SoftSub_2(0).Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
                    SoftSubsList.Add(New CR_Subtiles(SoftSubsAvailable(i), Main.HardSubValuesToDisplay(SoftSubsAvailable(i)), " -i " + """" + SoftSub_3 + """", i.ToString, SoftSubsAvailable(i) = Main.DefaultSubCR))

                Next


                If MergeSubsNow = True Then
                    Dim DispositionIndex As Integer = 69
                    Dim SoftSubMergeURLs As String = ""
                    Dim SoftSubMergeMaps As String = " -map 0:v -map 0:a"
                    Dim SoftSubMergeMetatata As String = ""
                    Dim IndexMoveMap As Integer = 1
                    If CR_MetadataUsage = True Then
                        IndexMoveMap = 2
                    End If

                    For i As Integer = 0 To SoftSubsList.Count - 1

                        SoftSubMergeURLs = SoftSubMergeURLs + " " + SoftSubsList(i).Url
                        SoftSubMergeMaps = SoftSubMergeMaps + " -map " + (i + IndexMoveMap).ToString
                        SoftSubMergeMetatata = SoftSubMergeMetatata + " -metadata:s:s:" + i.ToString + " language=" + Main.CCtoMP4CC(SoftSubsList(i).SubLangValue) + " -metadata:s:s:" + i.ToString + " title=" + """" + SoftSubsList(i).SubLangName + """" + " -metadata:s:s:" + i.ToString + " handler_name=" + """" + SoftSubsList(i).SubLangName + """"

                        If SoftSubsList(i).DefaultSub = True Then
                            DispositionIndex = i
                        End If

                    Next

                    Debug.WriteLine("-disposition:s: " + DispositionIndex.ToString)

                    If DispositionIndex < 69 Then
                        SoftSubMergeMetatata = SoftSubMergeMetatata + " -disposition:s:" + DispositionIndex.ToString + " default"
                    End If

                    If CR_MetadataUsage = False Then
                        ffmpegInput = ffmpegInput + " " + SoftSubMergeURLs + SoftSubMergeMaps + " " + ffmpeg_command_temp + " -c:s " + Main.MergeSubsFormat + SoftSubMergeMetatata + " -metadata:s:a:0 language=" + Main.CCtoMP4CC(CR_audio_locale)
                    Else
                        ffmpegInput = ffmpegInput + " -i " + """" + Mdata_File + """" + SoftSubMergeURLs + SoftSubMergeMaps + " -map_metadata 1 " + ffmpeg_command_temp + " -c:s " + Main.MergeSubsFormat + SoftSubMergeMetatata + " -metadata:s:a:0 language=" + Main.CCtoMP4CC(CR_audio_locale)

                    End If


                Else

                    For i As Integer = 0 To SoftSubsList.Count - 1
                        Dim i2 As Integer = i
                        Main.Invoke(Sub()
                                        ' TODO
                                        'Anime_Add.StatusLabel.Text = "Status: downloading subtitle file " + SoftSubsList(i2).SubLangName
                                        Main.Text = "Status: downloading subtitle file " + SoftSubsList(i2).SubLangName
                                        Main.Invalidate()
                                    End Sub)

                        Dim SubText As String = ""
                        SubText = Main.Curl(SoftSubsList(i2).Url.Replace(" -i ", "").Replace("""", ""))
                        Dim Pfad3 As String = Pfad2.Replace("""", "")
                        Dim FN As String = Path.ChangeExtension(Path.Combine(Path.GetFileNameWithoutExtension(Pfad3) + "." + Main.GetSubFileLangName(SoftSubsList(i2).SubLangValue) + Path.GetExtension(Pfad3)), "ass")
                        If i = 0 And Main.IncludeLangName = False Then
                            FN = Path.ChangeExtension(Path.GetFileName(Pfad3), "ass")
                        End If
                        Dim Pfad4 As String = Path.Combine(Path.GetDirectoryName(Pfad3), FN)
                        WriteText(Pfad4, SubText)
                        Pause(3)

                    Next

                    If CR_MetadataUsage = False Then
                        ffmpegInput = ffmpegInput + " -metadata:s:a:0 language=" + Main.CCtoMP4CC(CR_audio_locale) + " " + ffmpeg_command_temp
                    Else
                        ffmpegInput = ffmpegInput + " -i " + """" + Mdata_File + """" + " -map_metadata 1" + " -metadata:s:a:0 language=" + Main.CCtoMP4CC(CR_audio_locale) + " " + ffmpeg_command_temp
                    End If
                End If

            End If

            ffmpegInput = Main.RemoveExtraSpaces(ffmpegInput)
#End Region

#Region "thumbnail"
            Dim thumbnail As String() = ObjectJson.Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim thumbnail3 As String = ""
            For i As Integer = 0 To thumbnail.Count - 1
                If CBool(InStr(thumbnail(i), ".jpg" + """")) Then
                    Dim thumbnail2 As String() = thumbnail(i).Split(New String() {".jpg" + """"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                    thumbnail3 = "https://" + thumbnail2(0).Replace("\/", "/") + ".jpg"
                    Exit For
                ElseIf CBool(InStr(thumbnail(i), ".jpeg" + """")) Then
                    Dim thumbnail2 As String() = thumbnail(i).Split(New String() {".jpeg" + """"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                    thumbnail3 = "https://" + thumbnail2(0).Replace("\/", "/") + ".jpeg"
                    Exit For
                ElseIf CBool(InStr(thumbnail(i), ".jpe" + """")) Then
                    Dim thumbnail2 As String() = thumbnail(i).Split(New String() {".jpe" + """"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                    thumbnail3 = "https://" + thumbnail2(0).Replace("\/", "/") + ".jpe"
                    Exit For
                End If
            Next

#End Region

#Region "item constructor"

#Region "Display Hard_Softsubs"
            Dim SubType_Value As String = Nothing
            If Not CR_HardSubLang = "" Then
                SubType_Value = "Hardsub: " + Main.HardSubValuesToDisplay(CR_HardSubLang)
            End If
            If SoftSubsList.Count > 0 And CR_HardSubLang = "" Then
                SubType_Value = "Softsubs: "
                For i As Integer = 0 To SoftSubsList.Count - 1
                    SubType_Value = SubType_Value + SoftSubsList(i).SubLangName
                    If i < SoftSubsList.Count - 1 Then
                        SubType_Value = SubType_Value + ", "
                    End If
                Next
            End If
#End Region


#Region "Display Resolution"


            Dim ResoHTMLDisplay As String = Nothing
            Dim ResoHTML As String() = ResoUsed.Split(New String() {"x"}, System.StringSplitOptions.RemoveEmptyEntries)
            If ResoHTML.Count > 1 Then
                ResoHTMLDisplay = ResoHTML(1) + "p"
            Else
                ResoHTMLDisplay = ResoHTML(0) + "p"
            End If

            Dim L2Name As String = String.Join(" ", CR_FilenName.Split(Main.invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) 'System.Text.RegularExpressions.Regex.Replace(CR_FilenName_Backup, "[^\w\\-]", " ")

            ' TODO: After this class has been refactored, clean up the multiple accesses to ffmpeg mode
            If settings.DownloadResolution = ProgramSettings.Resolution.AUTO And settings.DownloadMode = ProgramSettings.DownloadModeOptions.FFMPEG Then
                ResoHTMLDisplay = "[Auto]"
            End If
#End Region


            Dim L1Name_Split As String() = WebsiteURL.Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim L1Name As String = L1Name_Split(1).Replace("www.", "") + " | Dub : " + Main.HardSubValuesToDisplay(CR_audio_locale)

            'MsgBox(URL_DL)

            ' TODO
            Main.Invoke(Sub()
                            Main.ListItemAdd(Path.GetFileName(Pfad2.Replace("""", "")), L1Name, L2Name, ResoHTMLDisplay, SubType_Value, thumbnail3, ffmpegInput, Pfad2)
                        End Sub)
            'liList.Add(My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + CR_title + " <br> " + CR_season_number + " " + CR_episode + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + L2Name + "-->")
            'Form1.RichTextBox1.Text = My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + CR_Anime_Titel + " <br> " + CR_Anime_Staffel + " " + CR_Anime_Folge + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + L2Name + "-->"
#End Region
            Main.Grapp_RDY = True
            ' TODO
            Main.Invoke(Sub()
                            ' Anime_Add.StatusLabel.Text = "Status: idle"
                            Main.Text = "Crunchyroll Downloader"
                            Main.ResoBackString = Nothing
                            Main.Invalidate()
                        End Sub)

        Catch ex As Exception
            ' TODO
            Main.Invoke(Sub()
                            ' Anime_Add.StatusLabel.Text = "Status: idle"
                            Main.Text = "Crunchyroll Downloader"
                            Main.ResoBackString = Nothing
                            Main.Invalidate()
                        End Sub)
            Main.Grapp_RDY = True
            If CBool(InStr(ex.ToString, "Could not find the sub language")) Then
                MsgBox(Main.Sub_language_NotFound + Main.SubSprache.Name)
            ElseIf CBool(InStr(ex.ToString, "RESOLUTION Not Found")) Then
                MsgBox(Main.Resolution_NotFound)
            ElseIf CBool(InStr(ex.ToString, "Premium Episode")) Then
                MsgBox(Main.Premium_Stream, MsgBoxStyle.Information)
            ElseIf CBool(InStr(ex.ToString, "System.UnauthorizedAccessException")) Then
                MsgBox(Main.ErrorNoPermisson + vbNewLine + ex.ToString, MsgBoxStyle.Information)
            ElseIf CBool(InStr(ex.ToString, """" + "UserAbort" + """")) Then
                MsgBox(ex.ToString, MsgBoxStyle.Information)
            ElseIf CBool(InStr(ex.ToString, "Error - Getting")) Then
                Main.Navigate(WebsiteURL)
            Else
                MsgBox(ex.ToString, MsgBoxStyle.Information)
            End If
        End Try '
    End Sub


    Function Convert_locale(ByVal locale As String) As String
        Try
            If locale = "de" Then
                Return "de-DE"
            ElseIf locale = "" Then
                Return "en-US"
            ElseIf locale = "pt-br" Then
                Return "pt-BR"
            ElseIf locale = "es" Then
                Return "es-419"
            ElseIf locale = "fr" Then
                Return "fr-FR"
            ElseIf locale = "ar" Then
                Return "ar-SA"
            ElseIf locale = "ru" Then
                Return "ru-RU"
            ElseIf locale = "it" Then
                Return "it-IT"
            ElseIf locale = "es-es" Then
                Return "es-ES"
            ElseIf locale = "pt-pt" Then
                Return "pt-PT"
            Else
                Return "en-US"
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
