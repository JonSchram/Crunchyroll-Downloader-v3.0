Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.settings.general

Namespace ui
    Public Class MainPresenter
        Private ReadOnly MainView As Main

        Private DisplayedTasks As List(Of DownloadTask)
        Private Scheduler As DownloadScheduler

        Public Sub New(mainView As Main)
            Me.MainView = mainView
            DisplayedTasks = New List(Of DownloadTask)
        End Sub

        Public Sub Initialize()

            Dim settings = ProgramSettings.GetInstance()
            If settings.NeedsUpgrade() Then
                settings.UpgradeSettings()
            End If

            Dim mySettings As New DirectorySettings With {
                .DirectoryName = Application.StartupPath,
                .FileName = "User.config.dat"
            }
            mySettings.Save() ' muss explizit gepeichert werden...


            If settings.OutputPath Is Nothing Or settings.OutputPath = "" Then
                settings.OutputPath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            End If

            If settings.TemporaryFolder = Nothing Then
                settings.TemporaryFolder = My.Computer.FileSystem.SpecialDirectories.Temp
            End If

            Scheduler = DownloadScheduler.GetInstance()
            AddHandler Scheduler.ScheduleTask, AddressOf AddDownloadTask
        End Sub

        Public Sub AddDownloadTask(task As DownloadTask)
            DisplayedTasks.Add(task)
            ' Allow the main form to create the download view to keep it conceptually separate from the presenter even though there is no view interface.
            Dim itemView = MainView.DisplayDownloadTask(task)
            Dim itemPresenter = New DownloadingItemPresenter(itemView, task)
            AddHandler itemPresenter.RemoveTask, AddressOf RemoveDownloadTask
            AddHandler itemPresenter.CompleteTask, AddressOf HandleTaskCompleted
            itemPresenter.StartDownload()
        End Sub

        Private Sub HandleTaskCompleted(task As DownloadTask)
            Scheduler.OnTaskCompleted(task)
        End Sub

        Public Sub RemoveDownloadTask(view As DownloadingItemView)
            MainView.RemoveDownloadTask(view)
        End Sub
    End Class
End Namespace