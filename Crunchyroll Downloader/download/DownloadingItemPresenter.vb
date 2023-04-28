Namespace download
    Public Class DownloadingItemPresenter

        Private WithEvents View As DownloadingItemView

        Public Sub New(view As DownloadingItemView)
            Me.View = view
        End Sub

        Private Sub HandleCancelDownload() Handles View.CancelDownload
            MessageBox.Show("Download cancelled")
        End Sub

        Private Sub HandlePauseDownload() Handles View.PauseDownload
            MessageBox.Show("Download paused")
        End Sub
    End Class
End Namespace