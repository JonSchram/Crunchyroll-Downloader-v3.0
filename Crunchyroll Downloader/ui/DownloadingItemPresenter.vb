Imports System.Timers
Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.pipeline
Imports Crunchyroll_Downloader.utilities

Namespace ui
    Public Class DownloadingItemPresenter

        Private WithEvents View As DownloadingItemView

        Private ReadOnly task As DownloadTask
        Private ReadOnly estimator As ProgressEstimator
        Private ReadOnly ProgressReporter As IProgress(Of PipelineProgress)


        Private EstimateSmoothingTimer As Timer
        Private TotalPercent As Integer?
        Private CurrentStagePercent As Integer?
        Private CurrentEstimate As TimeSpan
        Private CurrentAction As PipelineStage

        Public Event RemoveTask(view As DownloadingItemView)
        Public Event CompleteTask(task As DownloadTask)



        Public Sub New(view As DownloadingItemView, task As DownloadTask)
            Me.View = view
            Me.task = task

            CurrentAction = PipelineStage.INITIALIZING

            ' Only update timer every 2 seconds so the estimate is a little more stable.
            EstimateSmoothingTimer = New Timer With {
                .SynchronizingObject = view,
                .AutoReset = True,
                .Interval = 2000
            }
            AddHandler EstimateSmoothingTimer.Elapsed, AddressOf HandleEstimateTimerElapsed


            estimator = New ProgressEstimator()
            ProgressReporter = New Progress(Of PipelineProgress)(AddressOf HandleProgressReport)

            view.SetTask(task)
        End Sub

        Private Sub HandleProgressReport(progress As PipelineProgress)
            Debug.WriteLine($"Progress reported: {progress}")

            If progress.Stage.HasValue Then
                CurrentAction = progress.Stage.Value
            End If

            If progress.StageStarted.HasValue Then
                estimator.Start()
            End If

            If progress.TotalPercent.HasValue Then
                TotalPercent = progress.TotalPercent
            End If

            If progress.StagePercent.HasValue Then
                CurrentStagePercent = progress.StagePercent
                View.UpdateProgress(progress.TotalPercent.Value, progress.StagePercent.Value, CurrentEstimate)
            End If

            If progress.StageCompleted.HasValue Then
                estimator.Reset()
                CurrentEstimate = Nothing
                CurrentStagePercent = 0
            End If

            If progress.Completed Then
                EstimateSmoothingTimer.Stop()
                CurrentEstimate = Nothing
                RaiseEvent CompleteTask(task)
            End If
        End Sub

        Public Sub StartDownload()
            Dim download = New DownloadThread(task, ProgressReporter)
            download.Start()
            EstimateSmoothingTimer.Start()
        End Sub

        Private Sub HandleCancelDownload() Handles View.CancelDownload
            EstimateSmoothingTimer.Stop()
            RaiseEvent RemoveTask(View)
        End Sub

        Private Sub HandlePauseDownload() Handles View.PauseDownload
            EstimateSmoothingTimer.Stop()
            ' TODO: Allow resuming paused download
            MessageBox.Show("Download paused (TODO)")
        End Sub

        Private Sub HandleEstimateTimerElapsed(sender As Object, args As ElapsedEventArgs)
            If CurrentStagePercent.HasValue Then
                ' It is safe to update these values because the timer is executing on the same thread as the IProgress instance (the UI thread).
                CurrentEstimate = estimator.GetRemainingTime(CurrentStagePercent.Value)
                View.UpdateProgress(TotalPercent.Value, CurrentStagePercent.Value, CurrentEstimate)
            End If
        End Sub
    End Class
End Namespace