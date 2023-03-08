Imports Crunchyroll_Downloader.hls

Public Class DebugForm
    Private Sub ParseJsonButton_Click(sender As Object, e As EventArgs) Handles ParseJsonButton.Click
        If FunimationRadioButton.Checked Then
            parseFunimation()
        End If
    End Sub

    Private Sub parseFunimation()
        ' TODO: Can't do anything with this. All JSON is expected to be downloaded, and the debug window is trying to let you parse from a text box
        If SeriesInfoRadioButton.Checked Then
            Dim parser = New FunimationExtractor("funimation.com/shows")
        ElseIf SeasonInfoRadioButton.Checked Then
            Dim parser = New FunimationExtractor("funimation.com/shows")
        ElseIf EpisodeInfoRadioButton.Checked Then
            Dim parser = New FunimationExtractor("funimation.com/v/")
            Dim episodeInfo = parser.getEpisodeInfo()
            OutputTextBox.Text = episodeInfo.ToString()
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
End Class