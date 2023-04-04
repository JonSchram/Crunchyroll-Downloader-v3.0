Imports System.Net
Imports System.Net.Http
Imports Crunchyroll_Downloader.api.authentication
Imports Crunchyroll_Downloader.hls
Imports Microsoft.Web.WebView2.Core

Public Class DebugForm
    Private Sub ParseJsonButton_Click(sender As Object, e As EventArgs) Handles ParseJsonButton.Click
        If FunimationRadioButton.Checked Then
            parseFunimation()
        End If
    End Sub

    Private Sub parseFunimation()
        Dim json = inputTextBox.Text
        If SeriesInfoRadioButton.Checked Then
            Dim series = FunimationSeries.CreateFromJson(json)
            OutputTextBox.Text = series.ToString()
        ElseIf SeasonInfoRadioButton.Checked Then
            Dim SeasonInfo = FunimationSeason.CreateFromJson(json)
            OutputTextBox.Text = SeasonInfo.ToString()
        ElseIf EpisodeInfoRadioButton.Checked Then
            Dim episodeInfo = FunimationEpisode.CreateFromJson(json)
            OutputTextBox.Text = episodeInfo.ToString()
        ElseIf EpisodePlaybackRadioButton.Checked Then
            Dim playbackInfo = EpisodePlaybackInfo.CreateFromJson(json)
            OutputTextBox.Text = playbackInfo.ToString()
        End If
    End Sub

    Private Sub ParsePlaylistButton_Click(sender As Object, e As EventArgs) Handles ParsePlaylistButton.Click
        Dim parser = New PlaylistParser()
        Dim playlistText = PlaylistTextBox.Text

        If MasterPlaylistRadioButton.Checked() Then
            Dim playlist = parser.parseMasterPlaylist(playlistText)
            PlaylistOutputTextBox.Text = playlist.ToString()

        ElseIf MediaPlaylistRadioButton.Checked() Then
            Dim playlist = parser.ParseMediaPlaylist(playlistText)
            PlaylistOutputTextBox.Text = playlist.ToString()
        End If

    End Sub

    Private Async Sub GetBrowserCookiesButton_Click(sender As Object, e As EventArgs) Handles GetBrowserCookiesButton.Click
        Dim cookies As List(Of CoreWebView2Cookie) = Await Browser.GetInstance().GetCookies(CookieDomainTextBox.Text)
        CookiesOutputTextBox.Text = ConvertCookiesToText(cookies)
    End Sub

    Private Function ConvertCookiesToText(cookies As List(Of CoreWebView2Cookie)) As String
        Dim result As String = ""
        For Each cookie In cookies
            result += CookieToText(cookie) + vbCrLf
        Next

        Return result
    End Function

    Private Function CookieToText(cookie As CoreWebView2Cookie) As String
        Dim result As String = ""

        result += "Cookie: " + cookie.Name + vbCrLf
        result += vbTab + "Domain: " + cookie.Domain + vbCrLf
        result += vbTab + "Secure: " + CStr(cookie.IsSecure) + vbCrLf
        result += vbTab + "Value: " + cookie.Value

        Return result
    End Function

    Private Async Sub AuthenticateButton_Click(sender As Object, e As EventArgs) Handles AuthenticateButton.Click
        Dim authenticator As ICookieBasedAuth = Nothing
        Dim cookieManager = Browser.GetInstance().GetCookieManager()
        If FunimationAuthRadioButton.Checked Then
            authenticator = New FunimationAuthenticator(cookieManager)
        ElseIf CrunchyrollAuthRadioButton.Checked Then
            authenticator = New CrunchyrollAuthenticator(cookieManager)
        End If
        If authenticator IsNot Nothing Then
            Dim cookie = Await authenticator.GetLoginCookie()
            AuthenticationOutputTextBox.Text = cookie.Name + ":" + vbTab + cookie.Value
        End If

    End Sub

    Private Async Sub IsPaidAccountButton_Click(sender As Object, e As EventArgs) Handles IsPaidAccountButton.Click
        If FunimationAuthRadioButton.Checked Then
            Dim authenticator = New FunimationAuthenticator(Browser.GetInstance().GetCookieManager())
            Dim isPaid = Await authenticator.IsPaidAccount()
            AuthenticationOutputTextBox.Text = "Is paid account: " + CStr(isPaid)
        End If
    End Sub

    Private Sub DoHttpRequest()
        Dim handler = New HttpClientHandler()
        Dim cookieContainer = New CookieContainer()
        'If Cookie IsNot Nothing Then
        '    cookieContainer.Add(Cookie)
        'End If
        handler.CookieContainer = cookieContainer

        ' Microsoft recommends not using WebClient for new development (so use HttpClient)
        Dim hClient = New HttpClient()
        'hClient.DefaultRequestHeaders.
    End Sub
End Class