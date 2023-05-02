Imports System.Net
Imports System.Net.Http
Imports Crunchyroll_Downloader.api.authentication
Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.hls
Imports Crunchyroll_Downloader.hls.playlist
Imports Crunchyroll_Downloader.hls.rewriter
Imports Microsoft.Web.WebView2.Core

Namespace debugging
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

            ElseIf MediaPlaylistRadioButton.Checked() Then
                Dim playlist = parser.ParseMediaPlaylist(playlistText)
                PlaylistOutputTextBox.Text = playlist.ToString()

                If RewriteUrlsCheckBox.Checked() Then
                    Dim rewriteDictionary = New Dictionary(Of Integer, String)
                    Dim urlCount = RewriteUrlsCountNumericInput.Value
                    Dim replacementUrl = RewriteUrlTextBox.Text
                    For i As Integer = 0 To CInt(urlCount)
                        rewriteDictionary.Item(i) = replacementUrl
                    Next
                    Dim rewriter = New FileSegmentRewriter(rewriteDictionary)

                    playlist = New MediaPlaylist(playlist, rewriter)
                    PlaylistOutputTextBox.Text = playlist.ToString()
                End If
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

        Private Async Sub GetLoginTokenButton_Click(sender As Object, e As EventArgs) Handles GetLoginTokenButton.Click
            Dim authenticator As ICookieBasedAuth = Nothing
            Dim cookieManager = Browser.GetInstance().GetCookieManager()
            If FunimationAuthRadioButton.Checked Then
                authenticator = New FunimationAuthenticator(cookieManager)
            ElseIf CrunchyrollAuthRadioButton.Checked Then
                authenticator = New CrunchyrollAuthenticator(cookieManager)
            End If
            If authenticator IsNot Nothing Then
                Dim cookie = Await authenticator.GetLoginCookie()
                If cookie Is Nothing Then
                    AuthenticationOutputTextBox.Text = "Cookie not found"
                Else
                    AuthenticationOutputTextBox.Text = cookie.Name + ":" + vbTab + cookie.Value
                End If
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

        Private Async Sub AuthenticateButton_Click(sender As Object, e As EventArgs) Handles AuthenticateButton.Click
            If FunimationAuthRadioButton.Checked Then
                Dim url = AuthenticateUrlTextBox.Text
                Dim token = LoginTokenTextBox.Text

                Dim authenticator As FunimationAuthenticator
                If token = "" Then
                    authenticator = New FunimationAuthenticator(Browser.GetInstance().GetCookieManager())
                Else
                    authenticator = New FunimationAuthenticator(token)
                End If

                Dim result = Await authenticator.SendAuthenticatedRequest(url)
                AuthenticationOutputTextBox.Text = result
            ElseIf CrunchyrollAuthRadioButton.Checked Then

            End If
        End Sub

        Private Sub AddQueueItemButton_Click(sender As Object, e As EventArgs) Handles AddQueueItemButton.Click
            Dim queue As DownloadQueue = DownloadQueue.GetInstance()

            Dim showName = ShowNameTextBox.Text
            Dim seasonNumber = SeasonNumberInput.Value
            Dim episodeName = EpisodeTitleTextBox.Text
            Dim episodeNumber = EpisodeNumberInput.Value


            Dim fakeEpisode = New FakeEpisode() With {
                .VideoId = "1234",
                .ShowName = showName,
                .SeasonNumber = CInt(seasonNumber),
                .EpisodeName = episodeName,
                .EpisodeNumber = episodeNumber
            }

            Dim fakeClient = New FakeDownloadClient(True, fakeEpisode)

            Dim task = New DownloadTask(fakeEpisode, "/", fakeClient)

            queue.Enqueue(task)
        End Sub
    End Class
End Namespace