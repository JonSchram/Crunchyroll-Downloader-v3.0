Imports System.Net
Imports Newtonsoft.Json
''' <summary>
''' Gets information about episodes from Funimation
''' </summary>
Public Class FunimationExtractor
    Private apiUrl As String = Nothing

    Public Sub New(showPath As String, region As String)
        apiUrl = "https://title-api.prd.funimationsvc.com/v2/shows/" + showPath + region
    End Sub

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
