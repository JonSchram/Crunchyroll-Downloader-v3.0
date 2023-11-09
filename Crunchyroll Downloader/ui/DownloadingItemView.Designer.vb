Imports MetroFramework.Controls

Namespace ui
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class DownloadingItemView
        Inherits MetroUserControl

        'UserControl overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.DownloadItemStyleManager = New MetroFramework.Components.MetroStyleManager(Me.components)
            Me.StageProgress = New MetroFramework.Controls.MetroProgressBar()
            Me.SaveToFile = New System.Windows.Forms.ToolStripMenuItem()
            Me.LogTocClipboard = New System.Windows.Forms.ToolStripMenuItem()
            Me.PlaybackVideoFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ViewInExplorerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.StatusLabel = New MetroFramework.Controls.MetroLabel()
            Me.FormatLabel = New MetroFramework.Controls.MetroLabel()
            Me.WebsiteLabel = New MetroFramework.Controls.MetroLabel()
            Me.AnimeDetailsLabel = New MetroFramework.Controls.MetroLabel()
            Me.DeleteButton = New System.Windows.Forms.PictureBox()
            Me.PauseButton = New System.Windows.Forms.PictureBox()
            Me.ThumbnailPictureBox = New System.Windows.Forms.PictureBox()
            Me.TotalProgress = New MetroFramework.Controls.MetroProgressBar()
            CType(Me.DownloadItemStyleManager, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ContextMenuStrip1.SuspendLayout()
            CType(Me.DeleteButton, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PauseButton, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ThumbnailPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'DownloadItemStyleManager
            '
            Me.DownloadItemStyleManager.Owner = Nothing
            '
            'StageProgress
            '
            Me.StageProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.StageProgress.Location = New System.Drawing.Point(202, 74)
            Me.StageProgress.Name = "StageProgress"
            Me.StageProgress.Size = New System.Drawing.Size(637, 16)
            Me.StageProgress.TabIndex = 77
            '
            'SaveToFile
            '
            Me.SaveToFile.Name = "SaveToFile"
            Me.SaveToFile.Size = New System.Drawing.Size(189, 22)
            Me.SaveToFile.Text = "Save log to file"
            '
            'LogTocClipboard
            '
            Me.LogTocClipboard.Name = "LogTocClipboard"
            Me.LogTocClipboard.Size = New System.Drawing.Size(189, 22)
            Me.LogTocClipboard.Text = "Copy log to clipboard"
            '
            'PlaybackVideoFileToolStripMenuItem
            '
            Me.PlaybackVideoFileToolStripMenuItem.Name = "PlaybackVideoFileToolStripMenuItem"
            Me.PlaybackVideoFileToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
            Me.PlaybackVideoFileToolStripMenuItem.Text = "playback video file"
            '
            'ViewInExplorerToolStripMenuItem
            '
            Me.ViewInExplorerToolStripMenuItem.Name = "ViewInExplorerToolStripMenuItem"
            Me.ViewInExplorerToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
            Me.ViewInExplorerToolStripMenuItem.Text = "View in explorer"
            '
            'ContextMenuStrip1
            '
            Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewInExplorerToolStripMenuItem, Me.PlaybackVideoFileToolStripMenuItem, Me.LogTocClipboard, Me.SaveToFile})
            Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
            Me.ContextMenuStrip1.Size = New System.Drawing.Size(190, 92)
            '
            'StatusLabel
            '
            Me.StatusLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.StatusLabel.BackColor = System.Drawing.Color.Transparent
            Me.StatusLabel.FontSize = MetroFramework.MetroLabelSize.Tall
            Me.StatusLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.StatusLabel.Location = New System.Drawing.Point(470, 90)
            Me.StatusLabel.Name = "StatusLabel"
            Me.StatusLabel.Size = New System.Drawing.Size(378, 27)
            Me.StatusLabel.TabIndex = 76
            Me.StatusLabel.Text = "Status Label : speed, size and percent"
            Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'FormatLabel
            '
            Me.FormatLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.FormatLabel.AutoSize = True
            Me.FormatLabel.BackColor = System.Drawing.Color.Transparent
            Me.FormatLabel.FontSize = MetroFramework.MetroLabelSize.Tall
            Me.FormatLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.FormatLabel.Location = New System.Drawing.Point(202, 92)
            Me.FormatLabel.Name = "FormatLabel"
            Me.FormatLabel.Size = New System.Drawing.Size(137, 25)
            Me.FormatLabel.TabIndex = 74
            Me.FormatLabel.Text = "1080p, hardsub"
            '
            'WebsiteLabel
            '
            Me.WebsiteLabel.AutoSize = True
            Me.WebsiteLabel.BackColor = System.Drawing.Color.Transparent
            Me.WebsiteLabel.FontSize = MetroFramework.MetroLabelSize.Tall
            Me.WebsiteLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.WebsiteLabel.Location = New System.Drawing.Point(202, 11)
            Me.WebsiteLabel.Name = "WebsiteLabel"
            Me.WebsiteLabel.Size = New System.Drawing.Size(75, 25)
            Me.WebsiteLabel.TabIndex = 73
            Me.WebsiteLabel.Text = "Website"
            '
            'AnimeDetailsLabel
            '
            Me.AnimeDetailsLabel.AutoSize = True
            Me.AnimeDetailsLabel.BackColor = System.Drawing.Color.Transparent
            Me.AnimeDetailsLabel.FontSize = MetroFramework.MetroLabelSize.Tall
            Me.AnimeDetailsLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.AnimeDetailsLabel.Location = New System.Drawing.Point(202, 36)
            Me.AnimeDetailsLabel.Name = "AnimeDetailsLabel"
            Me.AnimeDetailsLabel.Size = New System.Drawing.Size(238, 25)
            Me.AnimeDetailsLabel.TabIndex = 72
            Me.AnimeDetailsLabel.Text = "Anime Title, Season, Episode"
            '
            'DeleteButton
            '
            Me.DeleteButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DeleteButton.BackColor = System.Drawing.Color.Transparent
            Me.DeleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.DeleteButton.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_del
            Me.DeleteButton.Location = New System.Drawing.Point(813, 16)
            Me.DeleteButton.Name = "DeleteButton"
            Me.DeleteButton.Size = New System.Drawing.Size(35, 29)
            Me.DeleteButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
            Me.DeleteButton.TabIndex = 71
            Me.DeleteButton.TabStop = False
            '
            'PauseButton
            '
            Me.PauseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PauseButton.BackColor = System.Drawing.Color.Transparent
            Me.PauseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
            Me.PauseButton.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_pause
            Me.PauseButton.Location = New System.Drawing.Point(782, 18)
            Me.PauseButton.Name = "PauseButton"
            Me.PauseButton.Size = New System.Drawing.Size(25, 24)
            Me.PauseButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.PauseButton.TabIndex = 70
            Me.PauseButton.TabStop = False
            '
            'ThumbnailPictureBox
            '
            Me.ThumbnailPictureBox.BackColor = System.Drawing.SystemColors.Desktop
            Me.ThumbnailPictureBox.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.main_del
            Me.ThumbnailPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.ThumbnailPictureBox.Location = New System.Drawing.Point(16, 20)
            Me.ThumbnailPictureBox.Name = "ThumbnailPictureBox"
            Me.ThumbnailPictureBox.Size = New System.Drawing.Size(168, 95)
            Me.ThumbnailPictureBox.TabIndex = 69
            Me.ThumbnailPictureBox.TabStop = False
            '
            'TotalProgress
            '
            Me.TotalProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TotalProgress.Location = New System.Drawing.Point(202, 64)
            Me.TotalProgress.Name = "TotalProgress"
            Me.TotalProgress.Size = New System.Drawing.Size(637, 10)
            Me.TotalProgress.TabIndex = 78
            '
            'DownloadingItemView
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Controls.Add(Me.TotalProgress)
            Me.Controls.Add(Me.ThumbnailPictureBox)
            Me.Controls.Add(Me.StageProgress)
            Me.Controls.Add(Me.WebsiteLabel)
            Me.Controls.Add(Me.DeleteButton)
            Me.Controls.Add(Me.FormatLabel)
            Me.Controls.Add(Me.AnimeDetailsLabel)
            Me.Controls.Add(Me.StatusLabel)
            Me.Controls.Add(Me.PauseButton)
            Me.Name = "DownloadingItemView"
            Me.Size = New System.Drawing.Size(861, 132)
            CType(Me.DownloadItemStyleManager, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ContextMenuStrip1.ResumeLayout(False)
            CType(Me.DeleteButton, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PauseButton, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ThumbnailPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents DownloadItemStyleManager As MetroFramework.Components.MetroStyleManager
        Friend WithEvents StageProgress As MetroProgressBar
        Friend WithEvents SaveToFile As ToolStripMenuItem
        Friend WithEvents LogTocClipboard As ToolStripMenuItem
        Friend WithEvents PlaybackVideoFileToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents ViewInExplorerToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
        Friend WithEvents StatusLabel As MetroLabel
        Friend WithEvents FormatLabel As MetroLabel
        Friend WithEvents WebsiteLabel As MetroLabel
        Friend WithEvents AnimeDetailsLabel As MetroLabel
        Friend WithEvents DeleteButton As PictureBox
        Friend WithEvents PauseButton As PictureBox
        Friend WithEvents ThumbnailPictureBox As PictureBox
        Friend WithEvents TotalProgress As MetroProgressBar
    End Class
End Namespace