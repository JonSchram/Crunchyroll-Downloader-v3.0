Imports MetroFramework.Controls

Public Class DownloadingItemView
    Inherits MetroUserControl

    Public Event CancelDownload()
    Public Event PauseDownload()

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub PauseButton_Click(sender As Object, e As EventArgs) Handles PauseButton.Click
        RaiseEvent PauseDownload()
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        RaiseEvent CancelDownload()
    End Sub
End Class
