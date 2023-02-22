Public Class SeasonSelector
    Private SeasonList As List(Of FunimationOverview)

    Public Property episodeList() As List(Of String)
    Public Property startEpisode As String
    Public Property endEpisode As String

    Private downloadUrl As String

    Public Sub New(downloadUrl As String)
        InitializeComponent()

        Me.downloadUrl = downloadUrl
    End Sub

    Public Sub updateControlsForFunimation(seasonList As List(Of FunimationOverview))
        resetComboBox(seasonSelectComboBox)
        resetComboBox(startEpisodeComboBox)
        resetComboBox(endEpisodeComboBox)
        For Each item In seasonList
            seasonSelectComboBox.Items.Add(item.Title)
        Next
    End Sub

    Private Sub downloadButton_Click(sender As Object, e As EventArgs) Handles downloadButton.Click
        ' TODO: validate what start and end episode are
        startEpisode = startEpisodeComboBox.SelectedItem.ToString()
        endEpisode = endEpisodeComboBox.SelectedItem.ToString()
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub resetComboBox(comboBox As ComboBox)
        comboBox.Items.Clear()
        comboBox.Text = Nothing
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) 
        Close()
    End Sub

    Private Sub MinimizeButton_Click(sender As Object, e As EventArgs) 
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub seasonSelectComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles seasonSelectComboBox.SelectedIndexChanged
        ' Get a list of episodes for the new season
        resetComboBox(startEpisodeComboBox)
        resetComboBox(endEpisodeComboBox)

        Dim selectedIndex As Integer = seasonSelectComboBox.SelectedIndex
        Dim funimationExtractor As FunimationExtractor = New FunimationExtractor("TODO: showPath", "TODO: region")
        Dim episodesJson As String = funimationExtractor.getFunimationEpisodesJson(SeasonList.Item(selectedIndex))
        Dim episodes As List(Of String) = funimationExtractor.extractEpisodesFromJson(episodesJson)
        setComboBoxEpisodes(startEpisodeComboBox, episodes)
        setComboBoxEpisodes(endEpisodeComboBox, episodes)
    End Sub

    Private Sub setComboBoxEpisodes(comboBox As ComboBox, episodeList As List(Of String))
        episodeList.ForEach(Sub(episodeString)
                                comboBox.Items.Add(formatEpisodeNumber(episodeString))
                            End Sub)
    End Sub

    Private Function formatEpisodeNumber(episodeNumber As String) As String
        Return "Episode " + episodeNumber
    End Function
End Class