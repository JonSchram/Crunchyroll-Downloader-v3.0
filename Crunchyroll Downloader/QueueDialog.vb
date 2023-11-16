Option Strict On
Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.settings.general
Imports Crunchyroll_Downloader.ui

Public Class QueueDialog
    Private ReadOnly episodeQueue As DownloadQueue = DownloadQueue.GetInstance()
    Private ReadOnly downloader As DownloadScheduler = DownloadScheduler.GetInstance()

    Private ReadOnly settings As ProgramSettings = ProgramSettings.GetInstance()

    Public Sub New()
        InitializeComponent()

        ' Set data source update mode to OnPropertyChanged so the control doesn't have to lose focus to update the property.
        Dim dataBinding = New Binding("Checked", downloader, "IsProcessingQueue") With {
            .DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        }
        RunQueueToggle.DataBindings.Add(dataBinding)
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler ProgramSettings.DarkModeChanged, AddressOf HandleDarkModeChanged
        StyleManager = MetroStyleManager1
        MetroStyleExtender1.SetApplyMetroTheme(QueueDisplayListBox, True)
        HandleDarkModeChanged(settings.DarkMode)

        QueueDisplayListBox.DataSource = episodeQueue
    End Sub

    Private Sub HandleDarkModeChanged(isDarkMode As Boolean)
        If isDarkMode Then
            MetroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark
        Else
            MetroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light
        End If
    End Sub
End Class