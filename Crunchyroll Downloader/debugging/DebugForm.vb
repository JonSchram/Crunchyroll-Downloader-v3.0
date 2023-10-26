Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports Crunchyroll_Downloader.api
Imports Crunchyroll_Downloader.api.common
Imports Crunchyroll_Downloader.api.crunchyroll
Imports Crunchyroll_Downloader.api.funimation
Imports Crunchyroll_Downloader.api.funimation.metadata
Imports Crunchyroll_Downloader.api.metadata
Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.hls.parsing
Imports Crunchyroll_Downloader.hls.playlist
Imports Crunchyroll_Downloader.hls.playlist.comparer
Imports Crunchyroll_Downloader.hls.playlist.stream
Imports Crunchyroll_Downloader.hls.rewriter
Imports Crunchyroll_Downloader.hls.writer

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
                Dim playlist = parser.ParseMasterPlaylist(playlistText)
                PlaylistOutputTextBox.Text = playlist.ToString()

            ElseIf MediaPlaylistRadioButton.Checked() Then
                Dim startTime As Date
                Dim endTime As Date
                startTime = Now
                Dim playlist = parser.ParseMediaPlaylist(playlistText)
                endTime = Now

                Dim elapsedTime = endTime - startTime
                PlaylistOutputTextBox.Text = $"Parse time: {elapsedTime.TotalMilliseconds} ms" + vbCrLf

                If RewriteUrlsCheckBox.Checked() Then
                    Dim rewriteDictionary = New Dictionary(Of Integer, String)
                    Dim urlCount = RewriteUrlsCountNumericInput.Value
                    Dim replacementUrl = RewriteUrlTextBox.Text
                    For i As Integer = 0 To CInt(urlCount)
                        rewriteDictionary.Item(i) = replacementUrl
                    Next
                    Dim rewriter = New FileSegmentRewriter(rewriteDictionary)
                    Dim playlistRewriter = New MediaPlaylistRewriter(rewriter)

                    playlist = playlistRewriter.RewritePlaylist(playlist)
                End If

                Dim outputText As String
                If RewritePlaylistCheckBox.Checked Then
                    Dim writer = New PlaylistWriter()
                    Dim playlistOutput = New MemoryStream(playlistText.Length)
                    writer.WriteToStream(playlist, playlistOutput)
                    playlistOutput.Flush()
                    playlistOutput.Position = 0

                    Dim reader = New StreamReader(playlistOutput)
                    outputText = reader.ReadToEnd()
                    reader.Close()
                    playlistOutput.Close()
                Else
                    outputText = playlist.ToString()
                End If

                PlaylistOutputTextBox.Text += outputText
            End If
        End Sub

        Private Async Sub GetBrowserCookiesButton_Click(sender As Object, e As EventArgs) Handles GetBrowserCookiesButton.Click
            Dim cookies As List(Of Cookie) = Await Browser.GetInstance().GetCookies(CookieDomainTextBox.Text)
            CookiesOutputTextBox.Text = ConvertCookiesToText(cookies)
        End Sub

        Private Function ConvertCookiesToText(cookies As List(Of Cookie)) As String
            Dim result As String = ""
            For Each cookie In cookies
                result += CookieToText(cookie) + vbCrLf
            Next

            Return result
        End Function

        Private Function CookieToText(cookie As Cookie) As String
            Dim result As String = ""

            result += "Cookie: " + cookie.Name + vbCrLf
            result += vbTab + "Domain: " + cookie.Domain + vbCrLf
            result += vbTab + "Secure: " + CStr(cookie.Secure) + vbCrLf
            result += vbTab + "Value: " + cookie.Value

            Return result
        End Function

        Private Async Sub GetLoginTokenButton_Click(sender As Object, e As EventArgs) Handles GetLoginTokenButton.Click
            Dim authenticator As ICookieBasedAuth = Nothing
            Dim webBrowser = Browser.GetInstance()
            If FunimationAuthRadioButton.Checked Then
                authenticator = New FunimationAuthenticator(webBrowser)
            ElseIf CrunchyrollAuthRadioButton.Checked Then
                ' TODO: This will likely need to use the web browser directly as well.
                authenticator = New CrunchyrollAuthenticator(webBrowser.GetCookieManager())
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
                Dim authenticator = New FunimationAuthenticator(Browser.GetInstance())
                Dim isPaid = Await authenticator.GetLoginType()
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
                    authenticator = New FunimationAuthenticator(Browser.GetInstance())
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

        Private Sub SelectVariantButton_Click(sender As Object, e As EventArgs) Handles SelectVariantButton.Click
            Dim parser = New PlaylistParser()
            Dim playlistText = MasterPlaylistTextBox.Text

            Dim playlist = parser.ParseMasterPlaylist(playlistText)
            PlaylistOutputTextBox.Text = playlist.ToString()

            Dim preferHighBandwidth = HighBandwidthCheckBox.Checked
            Dim preferHigherResolution = HigherResolutionCheckBox.Checked

            Dim playlistMatcher As IComparer(Of VariantStreamMetadata) = Nothing
            If LowResolutionRadioButton.Checked Then
                playlistMatcher = New LowestResolutionComparer()
            ElseIf HighResolutionRadioButton.Checked Then
                playlistMatcher = New HighestResolutionComparer()
            ElseIf TargetResolutionRadioButton.Checked Then
                Dim targetResolution = TargetResolutionInput.Value
                playlistMatcher = New ClosestResolutionComparer(CInt(targetResolution), preferHigherResolution, preferHighBandwidth)
            End If

            If playlistMatcher Is Nothing Then
                SelectedVariantStreamOutput.Text = "Must select a resolution option"
            Else
                Dim stream = playlist.GetClosestMatch(playlistMatcher)
                If stream Is Nothing Then
                    SelectedVariantStreamOutput.Text = "Error: No stream could be selected"
                Else
                    SelectedVariantStreamOutput.Text = stream.ToString()
                End If
            End If

        End Sub

        Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles DownloadCompleteMediaButton.Click
            Dim temporaryDirectory = TemporaryFolderTextBox.Text
            Dim outputDirectory = OutputFolderTextBox.Text

            Dim downloader As New FfmpegDownloader(temporaryDirectory, outputDirectory)


            Dim playbacks As New List(Of Selection)
            Dim mediaList As New List(Of Media)
            Dim media = New FileMedia(MediaType.Subtitles, Language.JAPANESE, MediaUrlTextBox.Text)
            mediaList.Add(media)
            playbacks.Add(New Selection(mediaList))

            Await downloader.DownloadPlaybacks(playbacks)
        End Sub

        Private Sub SelectTemporaryFolderButton_Click(sender As Object, e As EventArgs) Handles SelectTemporaryFolderButton.Click
            Dim result As DialogResult = PlaybackFolderDialog.ShowDialog()

            If result = DialogResult.OK Then
                TemporaryFolderTextBox.Text = PlaybackFolderDialog.SelectedPath
            End If
        End Sub

        Private Sub SelectOutputFolderButton_Click(sender As Object, e As EventArgs) Handles SelectOutputFolderButton.Click
            Dim result As DialogResult = PlaybackFolderDialog.ShowDialog()

            If result = DialogResult.OK Then
                OutputFolderTextBox.Text = PlaybackFolderDialog.SelectedPath
            End If
        End Sub

        Private Async Sub DownloadHlsMediaButton_Click(sender As Object, e As EventArgs) Handles DownloadHlsMediaButton.Click
            Dim temporaryDirectory = TemporaryFolderTextBox.Text
            Dim outputDirectory = OutputFolderTextBox.Text
            Dim mediaUrl As String = MediaUrlTextBox.Text
            Dim client As HttpClient = New HttpClient()

            Dim response As HttpResponseMessage = Await client.GetAsync(mediaUrl)
            Dim contents As HttpContent = response.Content
            Dim playlistStream = Await contents.ReadAsStreamAsync()

            Dim parser As New PlaylistParser()
            Dim playlist As MediaPlaylist = parser.ParseMediaPlaylist(playlistStream)

            Dim downloader As New FfmpegDownloader(temporaryDirectory, outputDirectory)
        End Sub

        Private Async Sub DownloadHlsMasterPlaylistButton_Click(sender As Object, e As EventArgs) Handles DownloadHlsMasterPlaylistButton.Click
            Dim temporaryDirectory = TemporaryFolderTextBox.Text
            Dim outputDirectory = OutputFolderTextBox.Text
            Dim mediaUrl As String = MediaUrlTextBox.Text
            Dim client As New HttpClient()

            PlaybackDownloadStatusLabel.Text = $"Downloading {mediaUrl}..."

            Try
                Dim response As HttpResponseMessage = Await client.GetAsync(mediaUrl)
                Dim contents As HttpContent = response.Content
                Dim playlistStream = Await contents.ReadAsStreamAsync()

                Dim parser As New PlaylistParser()
                Dim playlist As MasterPlaylist = parser.ParseMasterPlaylist(playlistStream)

                Dim mediaList As New List(Of MasterPlaylistMedia) From {
                    New MasterPlaylistMedia(MediaType.Video, Language.JAPANESE, mediaUrl, playlist)
                }

                Dim playlistSelection As New Selection(mediaList)
                Dim downloader As New FfmpegDownloader(temporaryDirectory, outputDirectory)
                PlaybackDownloadStatusLabel.Text = $"Playlist selected. Running ffmpeg..."
                Await downloader.DownloadPlaybacks(New List(Of Selection) From {playlistSelection})
                PlaybackDownloadStatusLabel.Text = $"Ffmpeg completed. Playlist saved to file."
            Catch err As Exception
                Debug.WriteLine("Could not download master playlist. ")
                Debug.WriteLine(err.Message)
                Debug.WriteLine(err.StackTrace)
            End Try
        End Sub
    End Class
End Namespace