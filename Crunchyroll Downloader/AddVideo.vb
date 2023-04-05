Option Strict On
Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.settings.general

Public Class AddVideo
    Public Property OutputPath As String = ""
    Public Property OutputSubFolder As String

    Public Property downloadUrl As String

    Private Queue As DownloadQueue = DownloadQueue.GetInstance()

    Public Sub New()
        InitializeComponent()
    End Sub

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

            ' Disable the add video form to simulate a modal dialog. 
            Enabled = False
            AddHandler seasonSelectorForm.FormClosed, AddressOf SeasonSelectFormClosed
            ' Can't use ShowDialog because it would block all forms in the app.
            seasonSelectorForm.Show(Me)
        Else
            ' Individual video
            Dim episodeInfo = MetadataApi.getEpisodeInfo()
            Queue.Enqueue(episodeInfo, OutputPath)
        End If
    End Sub

    Private Sub SeasonSelectFormClosed(sender As Object, args As FormClosedEventArgs)
        Enabled = True

        Dim selectForm = CType(sender, SeasonSelector)

        Dim Api As New DownloaderApi(downloadUrl)
        Dim MetadataApi As IMetadataDownloader = Api.GetMetadataDownloader()

        If selectForm.DialogResult = DialogResult.OK Then
            Dim episodeList = selectForm.episodeList
            Dim startEpisode = selectForm.startEpisode
            Dim endEpisode = selectForm.endEpisode

            Dim minEpisode = Math.Min(startEpisode, endEpisode)
            Dim maxEpisode = Math.Max(startEpisode, endEpisode)
            Dim episodes = episodeList.ToList
            For episodeNum = startEpisode To endEpisode
                Dim Episode = episodes.Item(episodeNum)
                Dim EpisodeInfo = MetadataApi.getEpisodeInfo(Episode.ApiUrlSlug)
                Queue.Enqueue(EpisodeInfo, OutputPath)
            Next
            ' StartEpisode and endEpisode are indices into episodeList
        End If
        selectForm.Dispose()
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
            Dim selectedPath = FolderBrowserDialog1.SelectedPath
            outputTextBox.Text = selectedPath
            ProgramSettings.GetInstance().OutputPath = selectedPath
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
' - Populate download type as seen in https://github.com/hama3254/Crunchyroll-Downloader-v3.0/discussions/701
' - Count down from 30 to 0 if it needs to wait for a URL to load.