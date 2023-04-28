Namespace download
    Public Class DownloadingItemPresenter

        Private WithEvents View As DownloadingItemView

        Private ReadOnly task As DownloadTask

        Public Sub New(view As DownloadingItemView, task As DownloadTask)
            Me.View = view
            Me.task = task

            view.setTask(task)
        End Sub

        Private Sub HandleCancelDownload() Handles View.CancelDownload
            MessageBox.Show("Download cancelled")
        End Sub

        Private Sub HandlePauseDownload() Handles View.PauseDownload
            MessageBox.Show("Download paused")
        End Sub
    End Class
End Namespace