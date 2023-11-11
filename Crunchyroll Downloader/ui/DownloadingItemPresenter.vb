Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.pipeline
Imports Crunchyroll_Downloader.utilities

Namespace ui
    Public Class DownloadingItemPresenter

        Private WithEvents View As DownloadingItemView

        Private ReadOnly task As DownloadTask
        Private ReadOnly estimator As ProgressEstimator


        Private ReadOnly ProgressReporter As IProgress(Of PipelineProgress)

        Public Event RemoveTask(view As DownloadingItemView)
        Public Event CompleteTask(task As DownloadTask)

        Private CurrentAction As PipelineStage

        Public Sub New(view As DownloadingItemView, task As DownloadTask)
            Me.View = view
            Me.task = task

            CurrentAction = PipelineStage.INITIALIZING

            estimator = New ProgressEstimator()
            ProgressReporter = New Progress(Of PipelineProgress)(AddressOf HandleProgressReport)

            view.SetTask(task)
        End Sub

        Private Sub HandleProgressReport(progress As PipelineProgress)
            Debug.WriteLine($"Progress reported: {progress}")

            If progress.Stage.HasValue Then
                CurrentAction = progress.Stage.Value
            End If

            If progress.StagePercent.HasValue Then
                Dim remainingTime As TimeSpan = estimator.GetRemainingTime(progress.StagePercent.Value)
                View.UpdateProgress(progress.TotalPercent.Value, progress.StagePercent.Value, remainingTime)
            End If

            If progress.StageStarted.HasValue Then
                estimator.Start()
            End If

            If progress.StageCompleted.HasValue Then
                estimator.Reset()
            End If

            If progress.Completed Then
                RaiseEvent CompleteTask(task)
            End If
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
    End Class
End Namespace