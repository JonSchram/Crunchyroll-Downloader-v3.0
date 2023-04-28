Imports MetroFramework.Controls

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
        Me.MetroStyleManager1 = New MetroFramework.Components.MetroStyleManager(Me.components)
        Me.DownloadProgress = New MetroFramework.Controls.MetroProgressBar()
        Me.SaveToFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogTocClipboard = New System.Windows.Forms.ToolStripMenuItem()
        Me.PlaybackVideoFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewInExplorerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.StatusLabel = New MetroFramework.Controls.MetroLabel()
        Me.HardsubLabel = New MetroFramework.Controls.MetroLabel()
        Me.ResolutionLabel = New MetroFramework.Controls.MetroLabel()
        Me.WebsiteLabel = New MetroFramework.Controls.MetroLabel()
        Me.AnimeDetailsLabel = New MetroFramework.Controls.MetroLabel()
        Me.bt_del = New System.Windows.Forms.PictureBox()
        Me.bt_pause = New System.Windows.Forms.PictureBox()
        Me.ThumbnailPictureBox = New System.Windows.Forms.PictureBox()
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.bt_del, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bt_pause, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ThumbnailPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MetroStyleManager1
        '
        Me.MetroStyleManager1.Owner = Nothing
        '
        'DownloadProgress
        '
        Me.DownloadProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DownloadProgress.Location = New System.Drawing.Point(202, 64)
        Me.DownloadProgress.Name = "DownloadProgress"
        Me.DownloadProgress.Size = New System.Drawing.Size(639, 20)
        Me.DownloadProgress.TabIndex = 77
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
        Me.StatusLabel.Location = New System.Drawing.Point(472, 88)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(378, 27)
        Me.StatusLabel.TabIndex = 76
        Me.StatusLabel.Text = "Status Label : speed, size and percent"
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'HardsubLabel
        '
        Me.HardsubLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.HardsubLabel.AutoSize = True
        Me.HardsubLabel.BackColor = System.Drawing.Color.Transparent
        Me.HardsubLabel.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.HardsubLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.HardsubLabel.Location = New System.Drawing.Point(271, 90)
        Me.HardsubLabel.Name = "HardsubLabel"
        Me.HardsubLabel.Size = New System.Drawing.Size(126, 25)
        Me.HardsubLabel.TabIndex = 75
        Me.HardsubLabel.Text = "Hardsub Label"
        '
        'ResolutionLabel
        '
        Me.ResolutionLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ResolutionLabel.AutoSize = True
        Me.ResolutionLabel.BackColor = System.Drawing.Color.Transparent
        Me.ResolutionLabel.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.ResolutionLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.ResolutionLabel.Location = New System.Drawing.Point(202, 90)
        Me.ResolutionLabel.Name = "ResolutionLabel"
        Me.ResolutionLabel.Size = New System.Drawing.Size(63, 25)
        Me.ResolutionLabel.TabIndex = 74
        Me.ResolutionLabel.Text = "1080p"
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
        'bt_del
        '
        Me.bt_del.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bt_del.BackColor = System.Drawing.Color.Transparent
        Me.bt_del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.bt_del.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_del
        Me.bt_del.Location = New System.Drawing.Point(815, 16)
        Me.bt_del.Name = "bt_del"
        Me.bt_del.Size = New System.Drawing.Size(35, 29)
        Me.bt_del.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.bt_del.TabIndex = 71
        Me.bt_del.TabStop = False
        '
        'bt_pause
        '
        Me.bt_pause.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bt_pause.BackColor = System.Drawing.Color.Transparent
        Me.bt_pause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.bt_pause.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_pause
        Me.bt_pause.Location = New System.Drawing.Point(784, 18)
        Me.bt_pause.Name = "bt_pause"
        Me.bt_pause.Size = New System.Drawing.Size(25, 24)
        Me.bt_pause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.bt_pause.TabIndex = 70
        Me.bt_pause.TabStop = False
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
        'DownloadingItemView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ThumbnailPictureBox)
        Me.Controls.Add(Me.DownloadProgress)
        Me.Controls.Add(Me.WebsiteLabel)
        Me.Controls.Add(Me.HardsubLabel)
        Me.Controls.Add(Me.bt_del)
        Me.Controls.Add(Me.ResolutionLabel)
        Me.Controls.Add(Me.AnimeDetailsLabel)
        Me.Controls.Add(Me.StatusLabel)
        Me.Controls.Add(Me.bt_pause)
        Me.Name = "DownloadingItemView"
        Me.Size = New System.Drawing.Size(863, 134)
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.bt_del, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bt_pause, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ThumbnailPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MetroStyleManager1 As MetroFramework.Components.MetroStyleManager
    Friend WithEvents DownloadProgress As MetroProgressBar
    Friend WithEvents SaveToFile As ToolStripMenuItem
    Friend WithEvents LogTocClipboard As ToolStripMenuItem
    Friend WithEvents PlaybackVideoFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewInExplorerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents StatusLabel As MetroLabel
    Friend WithEvents HardsubLabel As MetroLabel
    Friend WithEvents ResolutionLabel As MetroLabel
    Friend WithEvents WebsiteLabel As MetroLabel
    Friend WithEvents AnimeDetailsLabel As MetroLabel
    Friend WithEvents bt_del As PictureBox
    Friend WithEvents bt_pause As PictureBox
    Friend WithEvents ThumbnailPictureBox As PictureBox
End Class
