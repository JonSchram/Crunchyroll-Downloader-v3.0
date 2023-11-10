﻿Imports Crunchyroll_Downloader.download
Imports MetroFramework.Controls

Namespace ui

    Public Class DownloadingItemView
        Inherits MetroUserControl

        Public Event CancelDownload()
        Public Event PauseDownload()
        Public Event StartDownload()

        Private Task As DownloadTask

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub PauseButton_Click(sender As Object, e As EventArgs) Handles PauseButton.Click
            RaiseEvent PauseDownload()
        End Sub

        Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
            RaiseEvent CancelDownload()
        End Sub

        Public Sub SetTask(task As DownloadTask)
            Me.Task = task
            UpdateLabels()
        End Sub

        Public Sub UpdateProgressBars(totalProgress As Integer, stageProgress As Integer)
            Me.TotalProgress.Value = totalProgress
            Me.StageProgress.Value = stageProgress
        End Sub

        Private Sub UpdateLabels()
            If InvokeRequired Then
                Invoke(Sub() UpdateLabels())
            Else
                SetSiteName()
                AnimeDetailsLabel.Text = FormatAnimeTitle()
            End If
        End Sub

        Private Sub SetSiteName()
            Dim client = Task.Client
            WebsiteLabel.Text = client.GetSiteName()
        End Sub

        Private Function FormatAnimeTitle() As String
            Dim episode = Task.DownloadEpisode
            Return $"{episode.ShowName}, Season {episode.SeasonNumber}, episode {episode.EpisodeNumber}"
        End Function

        Private Sub SetResolutionLabel()
            ' TODO: Set resolution when it is found
        End Sub
        Private Sub SetHardsubLabel()
            ' TODO: Set hardsub label
        End Sub

    End Class
End Namespace