Public Class DebugForm
    Private Sub ParseButton_Click(sender As Object, e As EventArgs) Handles ParseButton.Click
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



End Class