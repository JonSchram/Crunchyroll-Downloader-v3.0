<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DebugForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TabControlOperations = New System.Windows.Forms.TabControl()
        Me.TabPageEpisode = New System.Windows.Forms.TabPage()
        Me.ResultLabel = New System.Windows.Forms.Label()
        Me.OutputTextBox = New System.Windows.Forms.TextBox()
        Me.ParseJsonButton = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.EpisodeInfoRadioButton = New System.Windows.Forms.RadioButton()
        Me.SeasonInfoRadioButton = New System.Windows.Forms.RadioButton()
        Me.SeriesInfoRadioButton = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.FunimationRadioButton = New System.Windows.Forms.RadioButton()
        Me.CrunchyrollRadioButton = New System.Windows.Forms.RadioButton()
        Me.InputLabel = New System.Windows.Forms.Label()
        Me.inputTextBox = New System.Windows.Forms.TextBox()
        Me.TabPagePlaylist = New System.Windows.Forms.TabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PlaylistTextBox = New System.Windows.Forms.TextBox()
        Me.ParsePlaylistButton = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PlaylistOutputTextBox = New System.Windows.Forms.TextBox()
        Me.TabControlOperations.SuspendLayout()
        Me.TabPageEpisode.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPagePlaylist.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControlOperations
        '
        Me.TabControlOperations.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControlOperations.Controls.Add(Me.TabPageEpisode)
        Me.TabControlOperations.Controls.Add(Me.TabPagePlaylist)
        Me.TabControlOperations.Location = New System.Drawing.Point(12, 12)
        Me.TabControlOperations.Name = "TabControlOperations"
        Me.TabControlOperations.SelectedIndex = 0
        Me.TabControlOperations.Size = New System.Drawing.Size(776, 426)
        Me.TabControlOperations.TabIndex = 0
        '
        'TabPageEpisode
        '
        Me.TabPageEpisode.Controls.Add(Me.ResultLabel)
        Me.TabPageEpisode.Controls.Add(Me.OutputTextBox)
        Me.TabPageEpisode.Controls.Add(Me.ParseJsonButton)
        Me.TabPageEpisode.Controls.Add(Me.GroupBox2)
        Me.TabPageEpisode.Controls.Add(Me.GroupBox1)
        Me.TabPageEpisode.Controls.Add(Me.InputLabel)
        Me.TabPageEpisode.Controls.Add(Me.inputTextBox)
        Me.TabPageEpisode.Location = New System.Drawing.Point(4, 22)
        Me.TabPageEpisode.Name = "TabPageEpisode"
        Me.TabPageEpisode.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageEpisode.Size = New System.Drawing.Size(768, 400)
        Me.TabPageEpisode.TabIndex = 0
        Me.TabPageEpisode.Text = "Episode API"
        Me.TabPageEpisode.UseVisualStyleBackColor = True
        '
        'ResultLabel
        '
        Me.ResultLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ResultLabel.AutoSize = True
        Me.ResultLabel.Location = New System.Drawing.Point(303, 296)
        Me.ResultLabel.Name = "ResultLabel"
        Me.ResultLabel.Size = New System.Drawing.Size(62, 13)
        Me.ResultLabel.TabIndex = 6
        Me.ResultLabel.Text = "Parse result"
        '
        'OutputTextBox
        '
        Me.OutputTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OutputTextBox.Location = New System.Drawing.Point(302, 315)
        Me.OutputTextBox.Multiline = True
        Me.OutputTextBox.Name = "OutputTextBox"
        Me.OutputTextBox.ReadOnly = True
        Me.OutputTextBox.Size = New System.Drawing.Size(361, 79)
        Me.OutputTextBox.TabIndex = 5
        '
        'ParseJsonButton
        '
        Me.ParseJsonButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ParseJsonButton.Location = New System.Drawing.Point(669, 326)
        Me.ParseJsonButton.Name = "ParseJsonButton"
        Me.ParseJsonButton.Size = New System.Drawing.Size(93, 40)
        Me.ParseJsonButton.TabIndex = 4
        Me.ParseJsonButton.Text = "Parse JSON"
        Me.ParseJsonButton.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.EpisodeInfoRadioButton)
        Me.GroupBox2.Controls.Add(Me.SeasonInfoRadioButton)
        Me.GroupBox2.Controls.Add(Me.SeriesInfoRadioButton)
        Me.GroupBox2.Location = New System.Drawing.Point(162, 296)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(134, 98)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "API method"
        '
        'EpisodeInfoRadioButton
        '
        Me.EpisodeInfoRadioButton.AutoSize = True
        Me.EpisodeInfoRadioButton.Location = New System.Drawing.Point(6, 65)
        Me.EpisodeInfoRadioButton.Name = "EpisodeInfoRadioButton"
        Me.EpisodeInfoRadioButton.Size = New System.Drawing.Size(83, 17)
        Me.EpisodeInfoRadioButton.TabIndex = 2
        Me.EpisodeInfoRadioButton.TabStop = True
        Me.EpisodeInfoRadioButton.Text = "Episode info"
        Me.EpisodeInfoRadioButton.UseVisualStyleBackColor = True
        '
        'SeasonInfoRadioButton
        '
        Me.SeasonInfoRadioButton.AutoSize = True
        Me.SeasonInfoRadioButton.Location = New System.Drawing.Point(6, 42)
        Me.SeasonInfoRadioButton.Name = "SeasonInfoRadioButton"
        Me.SeasonInfoRadioButton.Size = New System.Drawing.Size(81, 17)
        Me.SeasonInfoRadioButton.TabIndex = 1
        Me.SeasonInfoRadioButton.TabStop = True
        Me.SeasonInfoRadioButton.Text = "Season info"
        Me.SeasonInfoRadioButton.UseVisualStyleBackColor = True
        '
        'SeriesInfoRadioButton
        '
        Me.SeriesInfoRadioButton.AutoSize = True
        Me.SeriesInfoRadioButton.Location = New System.Drawing.Point(6, 19)
        Me.SeriesInfoRadioButton.Name = "SeriesInfoRadioButton"
        Me.SeriesInfoRadioButton.Size = New System.Drawing.Size(74, 17)
        Me.SeriesInfoRadioButton.TabIndex = 0
        Me.SeriesInfoRadioButton.TabStop = True
        Me.SeriesInfoRadioButton.Text = "Series info"
        Me.SeriesInfoRadioButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.FunimationRadioButton)
        Me.GroupBox1.Controls.Add(Me.CrunchyrollRadioButton)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 296)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(149, 98)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Site"
        '
        'FunimationRadioButton
        '
        Me.FunimationRadioButton.AutoSize = True
        Me.FunimationRadioButton.Location = New System.Drawing.Point(6, 42)
        Me.FunimationRadioButton.Name = "FunimationRadioButton"
        Me.FunimationRadioButton.Size = New System.Drawing.Size(76, 17)
        Me.FunimationRadioButton.TabIndex = 1
        Me.FunimationRadioButton.TabStop = True
        Me.FunimationRadioButton.Text = "Funimation"
        Me.FunimationRadioButton.UseVisualStyleBackColor = True
        '
        'CrunchyrollRadioButton
        '
        Me.CrunchyrollRadioButton.AutoSize = True
        Me.CrunchyrollRadioButton.Location = New System.Drawing.Point(6, 19)
        Me.CrunchyrollRadioButton.Name = "CrunchyrollRadioButton"
        Me.CrunchyrollRadioButton.Size = New System.Drawing.Size(77, 17)
        Me.CrunchyrollRadioButton.TabIndex = 0
        Me.CrunchyrollRadioButton.TabStop = True
        Me.CrunchyrollRadioButton.Text = "Crunchyroll"
        Me.CrunchyrollRadioButton.UseVisualStyleBackColor = True
        '
        'InputLabel
        '
        Me.InputLabel.AutoSize = True
        Me.InputLabel.Location = New System.Drawing.Point(6, 3)
        Me.InputLabel.Name = "InputLabel"
        Me.InputLabel.Size = New System.Drawing.Size(86, 13)
        Me.InputLabel.TabIndex = 1
        Me.InputLabel.Text = "JSON Response"
        '
        'inputTextBox
        '
        Me.inputTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.inputTextBox.Location = New System.Drawing.Point(6, 19)
        Me.inputTextBox.Multiline = True
        Me.inputTextBox.Name = "inputTextBox"
        Me.inputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.inputTextBox.Size = New System.Drawing.Size(756, 270)
        Me.inputTextBox.TabIndex = 0
        '
        'TabPagePlaylist
        '
        Me.TabPagePlaylist.Controls.Add(Me.Label2)
        Me.TabPagePlaylist.Controls.Add(Me.PlaylistOutputTextBox)
        Me.TabPagePlaylist.Controls.Add(Me.ParsePlaylistButton)
        Me.TabPagePlaylist.Controls.Add(Me.Label1)
        Me.TabPagePlaylist.Controls.Add(Me.PlaylistTextBox)
        Me.TabPagePlaylist.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePlaylist.Name = "TabPagePlaylist"
        Me.TabPagePlaylist.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePlaylist.Size = New System.Drawing.Size(768, 400)
        Me.TabPagePlaylist.TabIndex = 1
        Me.TabPagePlaylist.Text = "M3u8 playlist"
        Me.TabPagePlaylist.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Playlist Response"
        '
        'PlaylistTextBox
        '
        Me.PlaylistTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PlaylistTextBox.Location = New System.Drawing.Point(9, 19)
        Me.PlaylistTextBox.Multiline = True
        Me.PlaylistTextBox.Name = "PlaylistTextBox"
        Me.PlaylistTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.PlaylistTextBox.Size = New System.Drawing.Size(753, 215)
        Me.PlaylistTextBox.TabIndex = 2
        '
        'ParsePlaylistButton
        '
        Me.ParsePlaylistButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ParsePlaylistButton.Location = New System.Drawing.Point(679, 349)
        Me.ParsePlaylistButton.Name = "ParsePlaylistButton"
        Me.ParsePlaylistButton.Size = New System.Drawing.Size(83, 45)
        Me.ParsePlaylistButton.TabIndex = 4
        Me.ParsePlaylistButton.Text = "Parse M3U8"
        Me.ParsePlaylistButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 237)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Parse result"
        '
        'PlaylistOutputTextBox
        '
        Me.PlaylistOutputTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PlaylistOutputTextBox.Location = New System.Drawing.Point(9, 253)
        Me.PlaylistOutputTextBox.Multiline = True
        Me.PlaylistOutputTextBox.Name = "PlaylistOutputTextBox"
        Me.PlaylistOutputTextBox.ReadOnly = True
        Me.PlaylistOutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.PlaylistOutputTextBox.Size = New System.Drawing.Size(664, 141)
        Me.PlaylistOutputTextBox.TabIndex = 7
        '
        'DebugForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TabControlOperations)
        Me.Name = "DebugForm"
        Me.Text = "DebugForm"
        Me.TabControlOperations.ResumeLayout(False)
        Me.TabPageEpisode.ResumeLayout(False)
        Me.TabPageEpisode.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPagePlaylist.ResumeLayout(False)
        Me.TabPagePlaylist.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControlOperations As TabControl
    Friend WithEvents TabPageEpisode As TabPage
    Friend WithEvents TabPagePlaylist As TabPage
    Friend WithEvents InputLabel As Label
    Friend WithEvents inputTextBox As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents FunimationRadioButton As RadioButton
    Friend WithEvents CrunchyrollRadioButton As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents EpisodeInfoRadioButton As RadioButton
    Friend WithEvents SeasonInfoRadioButton As RadioButton
    Friend WithEvents SeriesInfoRadioButton As RadioButton
    Friend WithEvents ParseJsonButton As Button
    Friend WithEvents ResultLabel As Label
    Friend WithEvents OutputTextBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents PlaylistTextBox As TextBox
    Friend WithEvents ParsePlaylistButton As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents PlaylistOutputTextBox As TextBox
End Class
