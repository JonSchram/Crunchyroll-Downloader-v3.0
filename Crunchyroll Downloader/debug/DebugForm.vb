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
        Dim cookies As List(Of CoreWebView2Cookie) = Await Browser.GetCookies(CookieDomainTextBox.Text)
        Dim mainCookies = Main.CookieList
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
End Class