Imports Crunchyroll_Downloader.download

Namespace ui
    Public Class DownloadingItemPresenter

        Private WithEvents View As DownloadingItemView

        Private ReadOnly task As DownloadTask

        Private ReadOnly ProgressReporter As IProgress(Of PipelineProgress)

        Public Event RemoveTask(view As DownloadingItemView)
        Public Event CompleteTask(task As DownloadTask)

        Public Sub New(view As DownloadingItemView, task As DownloadTask)
            Me.View = view
            Me.task = task

            ProgressReporter = New Progress(Of PipelineProgress)(AddressOf HandleProgressReport)

            view.SetTask(task)
        End Sub

        Private Sub HandleProgressReport(progress As PipelineProgress)
            Debug.WriteLine($"Progress reported: {progress}")
            View.UpdateProgressBars(progress.TotalPercent, progress.StagePercent)
            If progress.Completed Then
                RaiseEvent CompleteTask(task)
            End If
        End Sub

        Private Sub HandleDownloadStart() Handles View.StartDownload
            ' TODO: Delete. I think this isn't needed. The main form starts the download, therefore this doesn't come as an event from the view,
            ' but instead comes from main as a method call.
            Debug.WriteLine("Starting download")
            Dim download = New DownloadThread(task, ProgressReporter)
            download.Start()
        End Sub

        Public Sub StartDownload()
            Dim download = New DownloadThread(task, ProgressReporter)
            download.Start()
        End Sub

        Private Sub HandleCancelDownload() Handles View.CancelDownload
            RaiseEvent RemoveTask(View)
        End Sub

        Private Sub HandlePauseDownload() Handles View.PauseDownload
            MessageBox.Show("Download paused (TODO)")
        End Sub

        Public Class PipelineProgress
            Public Property TotalPercent As Integer
            Public Property StagePercent As Integer

            Public Property Completed As Boolean

            Public Sub New()
                Completed = False
            End Sub

            Public Sub New(totalPercent As Integer, stagePercent As Integer)
                Me.TotalPercent = totalPercent
                Me.StagePercent = stagePercent
                Completed = False
            End Sub

            Public Sub New(completed As Boolean)
                Me.Completed = completed
                If completed Then
                    TotalPercent = 100
                    StagePercent = 100
                Else
                    TotalPercent = 0
                    StagePercent = 0
                End If
            End Sub
        End Class
    End Class
End Namespace