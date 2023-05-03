Imports Crunchyroll_Downloader.api.client

Public Class SeasonSelector
    Private SeasonList As List(Of SeasonOverview)

    ' The list of episodes to download
    Public Property episodeList As IEnumerable(Of EpisodeOverview)

    ' Using list indices because there is technically no guarantee that the episode number is an integer
    ' or even unique in the list. At least by using indices, the combo box answer is unambiguous


    ' The start episode, as relative to the list of episodes retrieved from the API
    Public Property startEpisode As Integer
    ' The end episode, as relative to the list of episodes retrieved from the API
    Public Property endEpisode As Integer


    Private MetadataApi As IDownloadClient

    Public Sub New(MetadataApi As IDownloadClient, SeasonList As IEnumerable(Of SeasonOverview))
        InitializeComponent()

        Me.MetadataApi = MetadataApi
        Me.SeasonList = SeasonList.ToList()
        updateControlsForSeason(Me.SeasonList)
    End Sub

    Public Sub updateControlsForSeason(seasonList As List(Of SeasonOverview))
        resetComboBox(seasonSelectComboBox)
        resetComboBox(startEpisodeComboBox)
        resetComboBox(endEpisodeComboBox)

        startEpisode = 0
        endEpisode = 0

        seasonSelectComboBox.Items.AddRange(seasonList.ToArray)
        'For Each item In seasonList
        '    seasonSelectComboBox.Items.Add(item)
        'Next
    End Sub

    Private Sub downloadButton_Click(sender As Object, e As EventArgs) Handles downloadButton.Click
        ' TODO: validate what start and end episode are
        ' Might want to confirm that the list is set?
        startEpisode = startEpisodeComboBox.SelectedIndex
        endEpisode = endEpisodeComboBox.SelectedIndex
        DialogResult = Windows.Forms.DialogResult.OK
        Close()
    End Sub

    Private Sub resetComboBox(comboBox As ComboBox)
        comboBox.Items.Clear()
        comboBox.Text = Nothing
    End Sub

    Private Async Sub seasonSelectComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles seasonSelectComboBox.SelectedIndexChanged
        ' Get a list of episodes for the new season
        resetComboBox(startEpisodeComboBox)
        resetComboBox(endEpisodeComboBox)

        Dim selectedIndex As Integer = seasonSelectComboBox.SelectedIndex
        Dim selectedItem As SeasonOverview = CType(seasonSelectComboBox.SelectedItem, SeasonOverview)

        episodeList = Await MetadataApi.ListEpisodes(selectedItem)
        setComboBoxEpisodes(startEpisodeComboBox, episodeList)
        setComboBoxEpisodes(endEpisodeComboBox, episodeList)
    End Sub

    Private Sub setComboBoxEpisodes(comboBox As ComboBox, episodeList As IEnumerable(Of EpisodeOverview))
        comboBox.Items.AddRange(episodeList.ToArray)
        'episodeList.ForEach(Sub(episode)
        '                        comboBox.Items.Add(formatEpisodeNumber(episode.EpisodeNumber))
        '                    End Sub)
    End Sub

    Private Function formatEpisodeNumber(episodeNumber As String) As String
        Return "Episode " + episodeNumber
    End Function

    Private Sub startEpisodeComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles startEpisodeComboBox.SelectedIndexChanged
        startEpisode = startEpisodeComboBox.SelectedIndex
    End Sub

    Private Sub endEpisodeComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles endEpisodeComboBox.SelectedIndexChanged
        endEpisode = endEpisodeComboBox.SelectedIndex
    End Sub

    Private Sub CancelDialogButton_Click(sender As Object, e As EventArgs) Handles CancelDialogButton.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
End Class