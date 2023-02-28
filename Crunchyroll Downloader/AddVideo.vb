Option Strict On

Public Class AddVideo
    Public Property OutputPath As String = ""
    Public Property OutputSubFolder As String

    Public Property downloadUrl As String

    Private Sub AddVideo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updateDirectoryComboBox(GetSubDirectories(OutputPath))
        subfolderComboBox.SelectedItem = OutputSubFolder
    End Sub

    Private Sub downloadButton_Click(sender As Object, e As EventArgs) Handles downloadButton.Click
        downloadUrl = downloadUrlTextBox.Text

        Dim Api As New DownloaderApi(downloadUrl)
        Dim MetadataApi As IMetadataDownloader = Api.GetMetadataDownloader()

        If Not MetadataApi.IsVideoUrl() Then
            'MsgBox("Downloading season information")
            Dim SeasonList = MetadataApi.ListSeasons()
            Dim seasonSelectorForm = New SeasonSelector(MetadataApi, SeasonList)
            'For Each Season In SeasonList
            '    ' TODO: make a method in season select class
            '    seasonSelectorForm.seasonSelectComboBox.Items.Add(Season.Name)
            'Next
            If seasonSelectorForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim episodeList = seasonSelectorForm.episodeList
                Dim startEpisode = seasonSelectorForm.startEpisode
                Dim endEpisode = seasonSelectorForm.endEpisode
                ' StartEpisode and endEpisode are indices into episodeList
                Dim downloadQueue As QueueDownloads = New QueueDownloads(OutputPath, OutputSubFolder)
                downloadQueue.enqueue(downloadUrl)
            End If
            seasonSelectorForm.Dispose()
        End If

    End Sub

    'Private Function isFunimationUrl(url As String) As Boolean
    '    Return True
    'End Function

    ''' <summary>
    ''' Show the selection dialog for save location
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub outputTextBox_Click(sender As Object, e As EventArgs) Handles outputTextBox.Click
        Dim FolderBrowserDialog1 As New FolderBrowserDialog With {
            .RootFolder = Environment.SpecialFolder.MyComputer
        }
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            updateDirectoryComboBox(GetSubDirectories(OutputPath))
            subfolderComboBox.SelectedItem = SubFolder_Nothing
            Main.Pfad = FolderBrowserDialog1.SelectedPath
            outputTextBox.Text = FolderBrowserDialog1.SelectedPath
            My.Settings.Pfad = Main.Pfad
            My.Settings.Save()
        End If
    End Sub

    Private Sub updateDirectoryComboBox(directoryList As List(Of String))
        subfolderComboBox.Items.Clear()
        subfolderComboBox.Items.Add(SubFolder_Nothing)
        subfolderComboBox.Items.Add(SubFolder_automatic)
        subfolderComboBox.Items.Add(SubFolder_automatic2)
        subfolderComboBox.Items.AddRange(directoryList.ToArray)
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Sub MinimizeButton_Click(sender As Object, e As EventArgs)
        WindowState = FormWindowState.Minimized
    End Sub
End Class

' TODO:
' - Display "# episodes selected" when retrieving episodes
' - Set the display text box to color "#8D1D2C" for 1 second and return to black each time an epsiode is added
' - Set the display text box to display "<current #> / <total #>" when adding episodes