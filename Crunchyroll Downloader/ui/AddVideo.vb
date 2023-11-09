Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.settings
Imports Crunchyroll_Downloader.settings.general
Imports SiteAPI.api

Namespace ui
    ''' <summary>
    ''' A form that allows the user to enter a URL for a season or episode and set download settings for that video.
    ''' </summary>
    Public Class AddVideo
        Public Property OutputPath As String
        Public Property OutputSubFolder As String

        Private Queue As DownloadQueue = DownloadQueue.GetInstance()
        Private client As IDownloadClient

        Private ReadOnly SubfolderBehaviorTextList As New EnumTextList(Of SubfolderBehavior)()

        Public Sub New(outputPath As String, lastSubfolderBehavior As SubfolderBehavior)
            InitializeComponent()

            Me.OutputPath = outputPath
            outputTextBox.Text = outputPath

            With SubfolderBehaviorTextList
                .Add(SubfolderBehavior.USE_SERIES_AND_SEASON, "Use folders for series and season")
                .Add(SubfolderBehavior.USE_SERIES, "Use folder for series")
                .Add(SubfolderBehavior.NO_SUBFOLDER, "Do not use any folders")
                .Add(SubfolderBehavior.OVERRIDE_FOLDER, "Use selected subfolder")
            End With

            subfolderBehaviorComboBox.DataSource = SubfolderBehaviorTextList.GetDisplayItems()
            subfolderBehaviorComboBox.SelectedItem = SubfolderBehaviorTextList.GetItemForEnum(lastSubfolderBehavior)

            UpdateSubfolderComboBoxItems(GetSubDirectories(outputPath))
            UpdateSubfolderComboBoxEnabledState()
        End Sub

        Private Async Sub downloadButton_Click(sender As Object, e As EventArgs) Handles downloadButton.Click
            Dim subfolderBehavior As SubfolderBehavior = SubfolderBehaviorTextList.GetEnumForItem(subfolderBehaviorComboBox.SelectedItem)
            ProgramSettings.GetInstance().LastSubfolderBehavior = subfolderBehavior
            My.Settings.Save()

            Dim downloadUrl = downloadUrlTextBox.Text

            client = DownloaderApi.GetMetadataDownloader(downloadUrl, Browser.GetInstance(), My.Resources.user_agent)
            If client Is Nothing Then
                Return
            End If
            downloadButton.Enabled = False
            Await client.Initialize()

            If Not client.IsVideoUrl(downloadUrl) Then
                Try
                    ' Disable the add video form to simulate a modal dialog. Disable first so that the user can't change the combo boxes.
                    Enabled = False
                    Dim SeasonList = Await client.ListSeasons(downloadUrl)
                    Dim seasonSelectorForm = New SeasonSelector(client, SeasonList)

                    AddHandler seasonSelectorForm.FormClosed, AddressOf SeasonSelectFormClosed
                    ' Can't use ShowDialog because it would block all forms in the app.
                    seasonSelectorForm.Show(Me)
                Catch ex As Exception
                    Console.Error.WriteLine("Error downloading season information.")
                    Console.Error.WriteLine(ex)
                End Try
            Else
                ' Individual video
                Dim episodeInfo = Await client.GetEpisodeInfo(downloadUrl)
                Dim manualFolder As String = CStr(subfolderComboBox.SelectedItem)
                Queue.Enqueue(New DownloadTask(episodeInfo, OutputPath, client, manualFolder, subfolderBehavior))
                downloadButton.Enabled = True
            End If
        End Sub

        Private Async Sub SeasonSelectFormClosed(sender As Object, args As FormClosedEventArgs)
            Enabled = True

            Dim selectForm = CType(sender, SeasonSelector)

            If selectForm.DialogResult = DialogResult.OK Then
                Dim manualFolder As String = CStr(subfolderComboBox.SelectedItem)
                Dim subfolderBehavior As SubfolderBehavior = SubfolderBehaviorTextList.GetEnumForItem(subfolderBehaviorComboBox.SelectedItem)

                Dim episodeList = selectForm.episodeList
                Dim startEpisode = selectForm.startEpisode
                Dim endEpisode = selectForm.endEpisode

                Dim minEpisode = Math.Min(startEpisode, endEpisode)
                Dim maxEpisode = Math.Max(startEpisode, endEpisode)
                Dim episodes = episodeList.ToList()
                For episodeNum = startEpisode To endEpisode
                    Dim Episode = episodes.Item(episodeNum)
                    Dim EpisodeInfo = Await client.GetEpisodeInfo(Episode)
                    Queue.Enqueue(New DownloadTask(EpisodeInfo, OutputPath, client, manualFolder, subfolderBehavior))
                Next
            End If
            selectForm.Dispose()
            downloadButton.Enabled = True
        End Sub

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
                OutputPath = FolderBrowserDialog1.SelectedPath
                UpdateSubfolderComboBoxItems(GetSubDirectories(OutputPath))
                outputTextBox.Text = OutputPath
                ProgramSettings.GetInstance().OutputPath = OutputPath
                My.Settings.Save()
            End If
        End Sub

        Private Sub UpdateSubfolderComboBoxItems(directoryList As List(Of String))
            subfolderComboBox.Items.Clear()
            ' Add a blank entry so the user can clear the option. The combo box is ignored if the combo box is set to ignore subfolders,
            ' but this is less confusing.
            subfolderComboBox.Items.Add("")
            subfolderComboBox.Items.AddRange(directoryList.ToArray)
        End Sub

        Private Sub subfolderBehaviorComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles subfolderBehaviorComboBox.SelectedIndexChanged
            UpdateSubfolderComboBoxEnabledState()
        End Sub

        Private Sub UpdateSubfolderComboBoxEnabledState()
            If SubfolderBehaviorTextList.GetEnumForItem(subfolderBehaviorComboBox.SelectedItem) = SubfolderBehavior.OVERRIDE_FOLDER Then
                subfolderComboBox.Enabled = True
            Else
                ' This will always exist because it is always inserted when updating combo box items.
                subfolderComboBox.SelectedItem = ""
                subfolderComboBox.Enabled = False
            End If
        End Sub
    End Class

    ' TODO:
    ' - Display "# episodes selected" when retrieving episodes
    ' - Set the display text box to color "#8D1D2C" for 1 second and return to black each time an epsiode is added
    ' - Set the display text box to display "<current #> / <total #>" when adding episodes
    ' - Populate download type as seen in https://github.com/hama3254/Crunchyroll-Downloader-v3.0/discussions/701
    ' - Count down from 30 to 0 if it needs to wait for a URL to load.

End Namespace