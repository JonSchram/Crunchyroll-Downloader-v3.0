Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.settings.general

Namespace ui
    Public Class MainPresenter
        Private ReadOnly MainView As Main

        Private DisplayedTasks As List(Of DownloadTask)
        Private Downloader As DownloadScheduler

        Public Sub New(mainView As Main)
            Me.MainView = mainView
            DisplayedTasks = New List(Of DownloadTask)
        End Sub

        Public Sub Initialize()

            Dim settings = ProgramSettings.GetInstance()
            If settings.NeedsUpgrade() Then
                settings.UpgradeSettings()
            End If

            Dim mySettings As New DirectorySettings()
            mySettings.DirectoryName = Application.StartupPath
            mySettings.FileName = "User.config.dat"
            mySettings.Save() ' muss explizit gepeichert werden...


            If settings.OutputPath Is Nothing Or settings.OutputPath = "" Then
                settings.OutputPath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            End If

            If settings.TemporaryFolder = Nothing Then
                settings.TemporaryFolder = My.Computer.FileSystem.SpecialDirectories.Temp
            End If

            Downloader = DownloadScheduler.GetInstance()
            AddHandler Downloader.ScheduleTask, AddressOf AddDownloadTask
        End Sub

        Public Sub AddDownloadTask(task As DownloadTask)
            DisplayedTasks.Add(task)
            Dim itemView = MainView.DisplayDownloadTask(task)
            Dim itemPresenter = New DownloadingItemPresenter(itemView, task)
        End Sub
    End Class
End Namespace