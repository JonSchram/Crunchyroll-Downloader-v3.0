﻿Namespace debugging
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class DebugForm
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
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
            Me.TabControlOperations = New System.Windows.Forms.TabControl()
            Me.TabPageEpisode = New System.Windows.Forms.TabPage()
            Me.ResultLabel = New System.Windows.Forms.Label()
            Me.OutputTextBox = New System.Windows.Forms.TextBox()
            Me.ParseJsonButton = New System.Windows.Forms.Button()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.EpisodePlaybackRadioButton = New System.Windows.Forms.RadioButton()
            Me.EpisodeInfoRadioButton = New System.Windows.Forms.RadioButton()
            Me.SeasonInfoRadioButton = New System.Windows.Forms.RadioButton()
            Me.SeriesInfoRadioButton = New System.Windows.Forms.RadioButton()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.FunimationRadioButton = New System.Windows.Forms.RadioButton()
            Me.CrunchyrollRadioButton = New System.Windows.Forms.RadioButton()
            Me.InputLabel = New System.Windows.Forms.Label()
            Me.inputTextBox = New System.Windows.Forms.TextBox()
            Me.TabPagePlaylist = New System.Windows.Forms.TabPage()
            Me.RewritePlaylistCheckBox = New System.Windows.Forms.CheckBox()
            Me.RewriteUrlsCountNumericInput = New System.Windows.Forms.NumericUpDown()
            Me.RewriteUrlsCheckBox = New System.Windows.Forms.CheckBox()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.RewriteUrlTextBox = New System.Windows.Forms.TextBox()
            Me.GroupBox3 = New System.Windows.Forms.GroupBox()
            Me.MediaPlaylistRadioButton = New System.Windows.Forms.RadioButton()
            Me.MasterPlaylistRadioButton = New System.Windows.Forms.RadioButton()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.PlaylistOutputTextBox = New System.Windows.Forms.TextBox()
            Me.ParsePlaylistButton = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.PlaylistTextBox = New System.Windows.Forms.TextBox()
            Me.TabPagePlaylistSelection = New System.Windows.Forms.TabPage()
            Me.Label12 = New System.Windows.Forms.Label()
            Me.SelectedVariantStreamOutput = New System.Windows.Forms.TextBox()
            Me.HigherResolutionCheckBox = New System.Windows.Forms.CheckBox()
            Me.HighBandwidthCheckBox = New System.Windows.Forms.CheckBox()
            Me.TargetResolutionInput = New System.Windows.Forms.NumericUpDown()
            Me.SelectVariantButton = New System.Windows.Forms.Button()
            Me.GroupBox5 = New System.Windows.Forms.GroupBox()
            Me.TargetResolutionRadioButton = New System.Windows.Forms.RadioButton()
            Me.HighResolutionRadioButton = New System.Windows.Forms.RadioButton()
            Me.LowResolutionRadioButton = New System.Windows.Forms.RadioButton()
            Me.Label11 = New System.Windows.Forms.Label()
            Me.MasterPlaylistTextBox = New System.Windows.Forms.TextBox()
            Me.TabPageBrowser = New System.Windows.Forms.TabPage()
            Me.CookiesOutputTextBox = New System.Windows.Forms.TextBox()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.CookieDomainTextBox = New System.Windows.Forms.TextBox()
            Me.GetBrowserCookiesButton = New System.Windows.Forms.Button()
            Me.TabPageAuthentication = New System.Windows.Forms.TabPage()
            Me.LoginTokenTextBox = New System.Windows.Forms.TextBox()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.AuthenticateUrlTextBox = New System.Windows.Forms.TextBox()
            Me.AuthenticateButton = New System.Windows.Forms.Button()
            Me.AuthenticationOutputTextBox = New System.Windows.Forms.TextBox()
            Me.IsPaidAccountButton = New System.Windows.Forms.Button()
            Me.GetLoginTokenButton = New System.Windows.Forms.Button()
            Me.GroupBox4 = New System.Windows.Forms.GroupBox()
            Me.FunimationAuthRadioButton = New System.Windows.Forms.RadioButton()
            Me.CrunchyrollAuthRadioButton = New System.Windows.Forms.RadioButton()
            Me.TabPageQueue = New System.Windows.Forms.TabPage()
            Me.EpisodeNumberInput = New System.Windows.Forms.NumericUpDown()
            Me.SeasonNumberInput = New System.Windows.Forms.NumericUpDown()
            Me.EpisodeTitleTextBox = New System.Windows.Forms.TextBox()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.ShowNameTextBox = New System.Windows.Forms.TextBox()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.AddQueueItemButton = New System.Windows.Forms.Button()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.TabPagePlayback = New System.Windows.Forms.TabPage()
            Me.PlaybackDownloadStatusLabel = New System.Windows.Forms.Label()
            Me.DownloadHlsMasterPlaylistButton = New System.Windows.Forms.Button()
            Me.DownloadHlsMediaButton = New System.Windows.Forms.Button()
            Me.DownloadCompleteMediaButton = New System.Windows.Forms.Button()
            Me.Label13 = New System.Windows.Forms.Label()
            Me.MediaUrlTextBox = New System.Windows.Forms.TextBox()
            Me.SelectOutputFolderButton = New System.Windows.Forms.Button()
            Me.OutputFolderTextBox = New System.Windows.Forms.TextBox()
            Me.SelectTemporaryFolderButton = New System.Windows.Forms.Button()
            Me.TemporaryFolderTextBox = New System.Windows.Forms.TextBox()
            Me.PlaybackFolderDialog = New System.Windows.Forms.FolderBrowserDialog()
            Me.TabControlOperations.SuspendLayout()
            Me.TabPageEpisode.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.TabPagePlaylist.SuspendLayout()
            CType(Me.RewriteUrlsCountNumericInput, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox3.SuspendLayout()
            Me.TabPagePlaylistSelection.SuspendLayout()
            CType(Me.TargetResolutionInput, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox5.SuspendLayout()
            Me.TabPageBrowser.SuspendLayout()
            Me.TabPageAuthentication.SuspendLayout()
            Me.GroupBox4.SuspendLayout()
            Me.TabPageQueue.SuspendLayout()
            CType(Me.EpisodeNumberInput, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.SeasonNumberInput, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.TabPagePlayback.SuspendLayout()
            Me.SuspendLayout()
            '
            'TabControlOperations
            '
            Me.TabControlOperations.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TabControlOperations.Controls.Add(Me.TabPageEpisode)
            Me.TabControlOperations.Controls.Add(Me.TabPagePlaylist)
            Me.TabControlOperations.Controls.Add(Me.TabPagePlaylistSelection)
            Me.TabControlOperations.Controls.Add(Me.TabPageBrowser)
            Me.TabControlOperations.Controls.Add(Me.TabPageAuthentication)
            Me.TabControlOperations.Controls.Add(Me.TabPageQueue)
            Me.TabControlOperations.Controls.Add(Me.TabPagePlayback)
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
            Me.ResultLabel.Location = New System.Drawing.Point(299, 270)
            Me.ResultLabel.Name = "ResultLabel"
            Me.ResultLabel.Size = New System.Drawing.Size(62, 13)
            Me.ResultLabel.TabIndex = 6
            Me.ResultLabel.Text = "Parse result"
            '
            'OutputTextBox
            '
            Me.OutputTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OutputTextBox.Location = New System.Drawing.Point(302, 289)
            Me.OutputTextBox.Multiline = True
            Me.OutputTextBox.Name = "OutputTextBox"
            Me.OutputTextBox.ReadOnly = True
            Me.OutputTextBox.Size = New System.Drawing.Size(361, 105)
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
            Me.GroupBox2.Controls.Add(Me.EpisodePlaybackRadioButton)
            Me.GroupBox2.Controls.Add(Me.EpisodeInfoRadioButton)
            Me.GroupBox2.Controls.Add(Me.SeasonInfoRadioButton)
            Me.GroupBox2.Controls.Add(Me.SeriesInfoRadioButton)
            Me.GroupBox2.Location = New System.Drawing.Point(162, 270)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(134, 124)
            Me.GroupBox2.TabIndex = 3
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "API method"
            '
            'EpisodePlaybackRadioButton
            '
            Me.EpisodePlaybackRadioButton.AutoSize = True
            Me.EpisodePlaybackRadioButton.Location = New System.Drawing.Point(7, 89)
            Me.EpisodePlaybackRadioButton.Name = "EpisodePlaybackRadioButton"
            Me.EpisodePlaybackRadioButton.Size = New System.Drawing.Size(109, 17)
            Me.EpisodePlaybackRadioButton.TabIndex = 3
            Me.EpisodePlaybackRadioButton.TabStop = True
            Me.EpisodePlaybackRadioButton.Text = "Episode playback"
            Me.EpisodePlaybackRadioButton.UseVisualStyleBackColor = True
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
            Me.GroupBox1.Location = New System.Drawing.Point(7, 270)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(149, 124)
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
            Me.inputTextBox.MaxLength = 9999999
            Me.inputTextBox.Multiline = True
            Me.inputTextBox.Name = "inputTextBox"
            Me.inputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.inputTextBox.Size = New System.Drawing.Size(756, 245)
            Me.inputTextBox.TabIndex = 0
            '
            'TabPagePlaylist
            '
            Me.TabPagePlaylist.Controls.Add(Me.RewritePlaylistCheckBox)
            Me.TabPagePlaylist.Controls.Add(Me.RewriteUrlsCountNumericInput)
            Me.TabPagePlaylist.Controls.Add(Me.RewriteUrlsCheckBox)
            Me.TabPagePlaylist.Controls.Add(Me.Label10)
            Me.TabPagePlaylist.Controls.Add(Me.RewriteUrlTextBox)
            Me.TabPagePlaylist.Controls.Add(Me.GroupBox3)
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
            'RewritePlaylistCheckBox
            '
            Me.RewritePlaylistCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RewritePlaylistCheckBox.AutoSize = True
            Me.RewritePlaylistCheckBox.Location = New System.Drawing.Point(649, 325)
            Me.RewritePlaylistCheckBox.Name = "RewritePlaylistCheckBox"
            Me.RewritePlaylistCheckBox.Size = New System.Drawing.Size(110, 17)
            Me.RewritePlaylistCheckBox.TabIndex = 15
            Me.RewritePlaylistCheckBox.Text = "Rewrite as playlist"
            Me.RewritePlaylistCheckBox.UseVisualStyleBackColor = True
            '
            'RewriteUrlsCountNumericInput
            '
            Me.RewriteUrlsCountNumericInput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RewriteUrlsCountNumericInput.Location = New System.Drawing.Point(649, 299)
            Me.RewriteUrlsCountNumericInput.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
            Me.RewriteUrlsCountNumericInput.Name = "RewriteUrlsCountNumericInput"
            Me.RewriteUrlsCountNumericInput.Size = New System.Drawing.Size(113, 20)
            Me.RewriteUrlsCountNumericInput.TabIndex = 14
            '
            'RewriteUrlsCheckBox
            '
            Me.RewriteUrlsCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RewriteUrlsCheckBox.AutoSize = True
            Me.RewriteUrlsCheckBox.Location = New System.Drawing.Point(656, 276)
            Me.RewriteUrlsCheckBox.Name = "RewriteUrlsCheckBox"
            Me.RewriteUrlsCheckBox.Size = New System.Drawing.Size(103, 17)
            Me.RewriteUrlsCheckBox.TabIndex = 13
            Me.RewriteUrlsCheckBox.Text = "Rewrite N URLs"
            Me.RewriteUrlsCheckBox.UseVisualStyleBackColor = True
            '
            'Label10
            '
            Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Label10.AutoSize = True
            Me.Label10.Location = New System.Drawing.Point(6, 358)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(102, 13)
            Me.Label10.TabIndex = 12
            Me.Label10.Text = "Replace URLs with:"
            '
            'RewriteUrlTextBox
            '
            Me.RewriteUrlTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RewriteUrlTextBox.Location = New System.Drawing.Point(9, 374)
            Me.RewriteUrlTextBox.MaxLength = 999999
            Me.RewriteUrlTextBox.Name = "RewriteUrlTextBox"
            Me.RewriteUrlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.RewriteUrlTextBox.Size = New System.Drawing.Size(634, 20)
            Me.RewriteUrlTextBox.TabIndex = 10
            '
            'GroupBox3
            '
            Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox3.Controls.Add(Me.MediaPlaylistRadioButton)
            Me.GroupBox3.Controls.Add(Me.MasterPlaylistRadioButton)
            Me.GroupBox3.Location = New System.Drawing.Point(649, 200)
            Me.GroupBox3.Name = "GroupBox3"
            Me.GroupBox3.Size = New System.Drawing.Size(113, 70)
            Me.GroupBox3.TabIndex = 9
            Me.GroupBox3.TabStop = False
            Me.GroupBox3.Text = "Parse as"
            '
            'MediaPlaylistRadioButton
            '
            Me.MediaPlaylistRadioButton.AutoSize = True
            Me.MediaPlaylistRadioButton.Location = New System.Drawing.Point(7, 43)
            Me.MediaPlaylistRadioButton.Name = "MediaPlaylistRadioButton"
            Me.MediaPlaylistRadioButton.Size = New System.Drawing.Size(89, 17)
            Me.MediaPlaylistRadioButton.TabIndex = 1
            Me.MediaPlaylistRadioButton.TabStop = True
            Me.MediaPlaylistRadioButton.Text = "Media Playlist"
            Me.MediaPlaylistRadioButton.UseVisualStyleBackColor = True
            '
            'MasterPlaylistRadioButton
            '
            Me.MasterPlaylistRadioButton.AutoSize = True
            Me.MasterPlaylistRadioButton.Location = New System.Drawing.Point(7, 20)
            Me.MasterPlaylistRadioButton.Name = "MasterPlaylistRadioButton"
            Me.MasterPlaylistRadioButton.Size = New System.Drawing.Size(91, 17)
            Me.MasterPlaylistRadioButton.TabIndex = 0
            Me.MasterPlaylistRadioButton.TabStop = True
            Me.MasterPlaylistRadioButton.Text = "Master playlist"
            Me.MasterPlaylistRadioButton.UseVisualStyleBackColor = True
            '
            'Label2
            '
            Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(6, 200)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(62, 13)
            Me.Label2.TabIndex = 8
            Me.Label2.Text = "Parse result"
            '
            'PlaylistOutputTextBox
            '
            Me.PlaylistOutputTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PlaylistOutputTextBox.Location = New System.Drawing.Point(9, 216)
            Me.PlaylistOutputTextBox.MaxLength = 999999
            Me.PlaylistOutputTextBox.Multiline = True
            Me.PlaylistOutputTextBox.Name = "PlaylistOutputTextBox"
            Me.PlaylistOutputTextBox.ReadOnly = True
            Me.PlaylistOutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.PlaylistOutputTextBox.Size = New System.Drawing.Size(634, 139)
            Me.PlaylistOutputTextBox.TabIndex = 7
            '
            'ParsePlaylistButton
            '
            Me.ParsePlaylistButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ParsePlaylistButton.Location = New System.Drawing.Point(649, 349)
            Me.ParsePlaylistButton.Name = "ParsePlaylistButton"
            Me.ParsePlaylistButton.Size = New System.Drawing.Size(113, 45)
            Me.ParsePlaylistButton.TabIndex = 4
            Me.ParsePlaylistButton.Text = "Parse M3U8"
            Me.ParsePlaylistButton.UseVisualStyleBackColor = True
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
            Me.PlaylistTextBox.MaxLength = 999999
            Me.PlaylistTextBox.Multiline = True
            Me.PlaylistTextBox.Name = "PlaylistTextBox"
            Me.PlaylistTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.PlaylistTextBox.Size = New System.Drawing.Size(753, 174)
            Me.PlaylistTextBox.TabIndex = 2
            '
            'TabPagePlaylistSelection
            '
            Me.TabPagePlaylistSelection.Controls.Add(Me.Label12)
            Me.TabPagePlaylistSelection.Controls.Add(Me.SelectedVariantStreamOutput)
            Me.TabPagePlaylistSelection.Controls.Add(Me.HigherResolutionCheckBox)
            Me.TabPagePlaylistSelection.Controls.Add(Me.HighBandwidthCheckBox)
            Me.TabPagePlaylistSelection.Controls.Add(Me.TargetResolutionInput)
            Me.TabPagePlaylistSelection.Controls.Add(Me.SelectVariantButton)
            Me.TabPagePlaylistSelection.Controls.Add(Me.GroupBox5)
            Me.TabPagePlaylistSelection.Controls.Add(Me.Label11)
            Me.TabPagePlaylistSelection.Controls.Add(Me.MasterPlaylistTextBox)
            Me.TabPagePlaylistSelection.Location = New System.Drawing.Point(4, 22)
            Me.TabPagePlaylistSelection.Name = "TabPagePlaylistSelection"
            Me.TabPagePlaylistSelection.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPagePlaylistSelection.Size = New System.Drawing.Size(768, 400)
            Me.TabPagePlaylistSelection.TabIndex = 5
            Me.TabPagePlaylistSelection.Text = "m3u8 playlist selection"
            Me.TabPagePlaylistSelection.UseVisualStyleBackColor = True
            '
            'Label12
            '
            Me.Label12.AutoSize = True
            Me.Label12.Location = New System.Drawing.Point(9, 200)
            Me.Label12.Name = "Label12"
            Me.Label12.Size = New System.Drawing.Size(121, 13)
            Me.Label12.TabIndex = 12
            Me.Label12.Text = "Selected Variant Stream"
            '
            'SelectedVariantStreamOutput
            '
            Me.SelectedVariantStreamOutput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SelectedVariantStreamOutput.Location = New System.Drawing.Point(9, 219)
            Me.SelectedVariantStreamOutput.MaxLength = 999999
            Me.SelectedVariantStreamOutput.Multiline = True
            Me.SelectedVariantStreamOutput.Name = "SelectedVariantStreamOutput"
            Me.SelectedVariantStreamOutput.ReadOnly = True
            Me.SelectedVariantStreamOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.SelectedVariantStreamOutput.Size = New System.Drawing.Size(605, 175)
            Me.SelectedVariantStreamOutput.TabIndex = 11
            '
            'HigherResolutionCheckBox
            '
            Me.HigherResolutionCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.HigherResolutionCheckBox.AutoSize = True
            Me.HigherResolutionCheckBox.Location = New System.Drawing.Point(620, 324)
            Me.HigherResolutionCheckBox.Name = "HigherResolutionCheckBox"
            Me.HigherResolutionCheckBox.Size = New System.Drawing.Size(133, 17)
            Me.HigherResolutionCheckBox.TabIndex = 10
            Me.HigherResolutionCheckBox.Text = "Next highest resolution"
            Me.HigherResolutionCheckBox.UseVisualStyleBackColor = True
            '
            'HighBandwidthCheckBox
            '
            Me.HighBandwidthCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.HighBandwidthCheckBox.AutoSize = True
            Me.HighBandwidthCheckBox.Location = New System.Drawing.Point(620, 301)
            Me.HighBandwidthCheckBox.Name = "HighBandwidthCheckBox"
            Me.HighBandwidthCheckBox.Size = New System.Drawing.Size(132, 17)
            Me.HighBandwidthCheckBox.TabIndex = 9
            Me.HighBandwidthCheckBox.Text = "Prefer High Bandwidth"
            Me.HighBandwidthCheckBox.UseVisualStyleBackColor = True
            '
            'TargetResolutionInput
            '
            Me.TargetResolutionInput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TargetResolutionInput.Location = New System.Drawing.Point(694, 347)
            Me.TargetResolutionInput.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
            Me.TargetResolutionInput.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.TargetResolutionInput.Name = "TargetResolutionInput"
            Me.TargetResolutionInput.Size = New System.Drawing.Size(68, 20)
            Me.TargetResolutionInput.TabIndex = 8
            Me.TargetResolutionInput.Value = New Decimal(New Integer() {1, 0, 0, 0})
            '
            'SelectVariantButton
            '
            Me.SelectVariantButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SelectVariantButton.Location = New System.Drawing.Point(620, 374)
            Me.SelectVariantButton.Name = "SelectVariantButton"
            Me.SelectVariantButton.Size = New System.Drawing.Size(142, 23)
            Me.SelectVariantButton.TabIndex = 7
            Me.SelectVariantButton.Text = "Parse && Select"
            Me.SelectVariantButton.UseVisualStyleBackColor = True
            '
            'GroupBox5
            '
            Me.GroupBox5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox5.Controls.Add(Me.TargetResolutionRadioButton)
            Me.GroupBox5.Controls.Add(Me.HighResolutionRadioButton)
            Me.GroupBox5.Controls.Add(Me.LowResolutionRadioButton)
            Me.GroupBox5.Location = New System.Drawing.Point(620, 199)
            Me.GroupBox5.Name = "GroupBox5"
            Me.GroupBox5.Size = New System.Drawing.Size(142, 96)
            Me.GroupBox5.TabIndex = 6
            Me.GroupBox5.TabStop = False
            Me.GroupBox5.Text = "Resolution"
            '
            'TargetResolutionRadioButton
            '
            Me.TargetResolutionRadioButton.AutoSize = True
            Me.TargetResolutionRadioButton.Location = New System.Drawing.Point(7, 66)
            Me.TargetResolutionRadioButton.Name = "TargetResolutionRadioButton"
            Me.TargetResolutionRadioButton.Size = New System.Drawing.Size(109, 17)
            Me.TargetResolutionRadioButton.TabIndex = 2
            Me.TargetResolutionRadioButton.TabStop = True
            Me.TargetResolutionRadioButton.Text = "Target Resolution"
            Me.TargetResolutionRadioButton.UseVisualStyleBackColor = True
            '
            'HighResolutionRadioButton
            '
            Me.HighResolutionRadioButton.AutoSize = True
            Me.HighResolutionRadioButton.Location = New System.Drawing.Point(7, 43)
            Me.HighResolutionRadioButton.Name = "HighResolutionRadioButton"
            Me.HighResolutionRadioButton.Size = New System.Drawing.Size(120, 17)
            Me.HighResolutionRadioButton.TabIndex = 1
            Me.HighResolutionRadioButton.TabStop = True
            Me.HighResolutionRadioButton.Text = "Get High Resolution"
            Me.HighResolutionRadioButton.UseVisualStyleBackColor = True
            '
            'LowResolutionRadioButton
            '
            Me.LowResolutionRadioButton.AutoSize = True
            Me.LowResolutionRadioButton.Location = New System.Drawing.Point(7, 20)
            Me.LowResolutionRadioButton.Name = "LowResolutionRadioButton"
            Me.LowResolutionRadioButton.Size = New System.Drawing.Size(132, 17)
            Me.LowResolutionRadioButton.TabIndex = 0
            Me.LowResolutionRadioButton.TabStop = True
            Me.LowResolutionRadioButton.Text = "Get Lowest Resolution"
            Me.LowResolutionRadioButton.UseVisualStyleBackColor = True
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Location = New System.Drawing.Point(6, 3)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(73, 13)
            Me.Label11.TabIndex = 5
            Me.Label11.Text = "Master playlist"
            '
            'MasterPlaylistTextBox
            '
            Me.MasterPlaylistTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MasterPlaylistTextBox.Location = New System.Drawing.Point(9, 19)
            Me.MasterPlaylistTextBox.MaxLength = 999999
            Me.MasterPlaylistTextBox.Multiline = True
            Me.MasterPlaylistTextBox.Name = "MasterPlaylistTextBox"
            Me.MasterPlaylistTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.MasterPlaylistTextBox.Size = New System.Drawing.Size(753, 174)
            Me.MasterPlaylistTextBox.TabIndex = 4
            '
            'TabPageBrowser
            '
            Me.TabPageBrowser.Controls.Add(Me.CookiesOutputTextBox)
            Me.TabPageBrowser.Controls.Add(Me.Label3)
            Me.TabPageBrowser.Controls.Add(Me.CookieDomainTextBox)
            Me.TabPageBrowser.Controls.Add(Me.GetBrowserCookiesButton)
            Me.TabPageBrowser.Location = New System.Drawing.Point(4, 22)
            Me.TabPageBrowser.Name = "TabPageBrowser"
            Me.TabPageBrowser.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPageBrowser.Size = New System.Drawing.Size(768, 400)
            Me.TabPageBrowser.TabIndex = 2
            Me.TabPageBrowser.Text = "Browser"
            Me.TabPageBrowser.UseVisualStyleBackColor = True
            '
            'CookiesOutputTextBox
            '
            Me.CookiesOutputTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CookiesOutputTextBox.Location = New System.Drawing.Point(9, 31)
            Me.CookiesOutputTextBox.Multiline = True
            Me.CookiesOutputTextBox.Name = "CookiesOutputTextBox"
            Me.CookiesOutputTextBox.ReadOnly = True
            Me.CookiesOutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.CookiesOutputTextBox.Size = New System.Drawing.Size(753, 363)
            Me.CookiesOutputTextBox.TabIndex = 3
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(6, 8)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(43, 13)
            Me.Label3.TabIndex = 2
            Me.Label3.Text = "Domain"
            '
            'CookieDomainTextBox
            '
            Me.CookieDomainTextBox.Location = New System.Drawing.Point(54, 5)
            Me.CookieDomainTextBox.Name = "CookieDomainTextBox"
            Me.CookieDomainTextBox.Size = New System.Drawing.Size(313, 20)
            Me.CookieDomainTextBox.TabIndex = 1
            '
            'GetBrowserCookiesButton
            '
            Me.GetBrowserCookiesButton.Location = New System.Drawing.Point(373, 3)
            Me.GetBrowserCookiesButton.Name = "GetBrowserCookiesButton"
            Me.GetBrowserCookiesButton.Size = New System.Drawing.Size(75, 23)
            Me.GetBrowserCookiesButton.TabIndex = 0
            Me.GetBrowserCookiesButton.Text = "Get Cookies"
            Me.GetBrowserCookiesButton.UseVisualStyleBackColor = True
            '
            'TabPageAuthentication
            '
            Me.TabPageAuthentication.Controls.Add(Me.LoginTokenTextBox)
            Me.TabPageAuthentication.Controls.Add(Me.Label5)
            Me.TabPageAuthentication.Controls.Add(Me.Label4)
            Me.TabPageAuthentication.Controls.Add(Me.AuthenticateUrlTextBox)
            Me.TabPageAuthentication.Controls.Add(Me.AuthenticateButton)
            Me.TabPageAuthentication.Controls.Add(Me.AuthenticationOutputTextBox)
            Me.TabPageAuthentication.Controls.Add(Me.IsPaidAccountButton)
            Me.TabPageAuthentication.Controls.Add(Me.GetLoginTokenButton)
            Me.TabPageAuthentication.Controls.Add(Me.GroupBox4)
            Me.TabPageAuthentication.Location = New System.Drawing.Point(4, 22)
            Me.TabPageAuthentication.Name = "TabPageAuthentication"
            Me.TabPageAuthentication.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPageAuthentication.Size = New System.Drawing.Size(768, 400)
            Me.TabPageAuthentication.TabIndex = 3
            Me.TabPageAuthentication.Text = "Authentication"
            Me.TabPageAuthentication.UseVisualStyleBackColor = True
            '
            'LoginTokenTextBox
            '
            Me.LoginTokenTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LoginTokenTextBox.Location = New System.Drawing.Point(121, 205)
            Me.LoginTokenTextBox.Name = "LoginTokenTextBox"
            Me.LoginTokenTextBox.Size = New System.Drawing.Size(638, 20)
            Me.LoginTokenTextBox.TabIndex = 9
            '
            'Label5
            '
            Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(44, 208)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(63, 13)
            Me.Label5.TabIndex = 8
            Me.Label5.Text = "Login token"
            '
            'Label4
            '
            Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(7, 181)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(100, 13)
            Me.Label4.TabIndex = 7
            Me.Label4.Text = "Authentication URL"
            '
            'AuthenticateUrlTextBox
            '
            Me.AuthenticateUrlTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AuthenticateUrlTextBox.Location = New System.Drawing.Point(120, 178)
            Me.AuthenticateUrlTextBox.Name = "AuthenticateUrlTextBox"
            Me.AuthenticateUrlTextBox.Size = New System.Drawing.Size(639, 20)
            Me.AuthenticateUrlTextBox.TabIndex = 6
            '
            'AuthenticateButton
            '
            Me.AuthenticateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.AuthenticateButton.Location = New System.Drawing.Point(121, 353)
            Me.AuthenticateButton.Name = "AuthenticateButton"
            Me.AuthenticateButton.Size = New System.Drawing.Size(75, 23)
            Me.AuthenticateButton.TabIndex = 5
            Me.AuthenticateButton.Text = "Authenticate"
            Me.AuthenticateButton.UseVisualStyleBackColor = True
            '
            'AuthenticationOutputTextBox
            '
            Me.AuthenticationOutputTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AuthenticationOutputTextBox.Location = New System.Drawing.Point(6, 6)
            Me.AuthenticationOutputTextBox.Multiline = True
            Me.AuthenticationOutputTextBox.Name = "AuthenticationOutputTextBox"
            Me.AuthenticationOutputTextBox.ReadOnly = True
            Me.AuthenticationOutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.AuthenticationOutputTextBox.Size = New System.Drawing.Size(753, 168)
            Me.AuthenticationOutputTextBox.TabIndex = 4
            '
            'IsPaidAccountButton
            '
            Me.IsPaidAccountButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.IsPaidAccountButton.Location = New System.Drawing.Point(121, 324)
            Me.IsPaidAccountButton.Name = "IsPaidAccountButton"
            Me.IsPaidAccountButton.Size = New System.Drawing.Size(75, 23)
            Me.IsPaidAccountButton.TabIndex = 2
            Me.IsPaidAccountButton.Text = "Get is paid"
            Me.IsPaidAccountButton.UseVisualStyleBackColor = True
            '
            'GetLoginTokenButton
            '
            Me.GetLoginTokenButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.GetLoginTokenButton.Location = New System.Drawing.Point(120, 294)
            Me.GetLoginTokenButton.Name = "GetLoginTokenButton"
            Me.GetLoginTokenButton.Size = New System.Drawing.Size(90, 23)
            Me.GetLoginTokenButton.TabIndex = 1
            Me.GetLoginTokenButton.Text = "Get login token"
            Me.GetLoginTokenButton.UseVisualStyleBackColor = True
            '
            'GroupBox4
            '
            Me.GroupBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.GroupBox4.Controls.Add(Me.FunimationAuthRadioButton)
            Me.GroupBox4.Controls.Add(Me.CrunchyrollAuthRadioButton)
            Me.GroupBox4.Location = New System.Drawing.Point(7, 294)
            Me.GroupBox4.Name = "GroupBox4"
            Me.GroupBox4.Size = New System.Drawing.Size(107, 100)
            Me.GroupBox4.TabIndex = 0
            Me.GroupBox4.TabStop = False
            Me.GroupBox4.Text = "Site"
            '
            'FunimationAuthRadioButton
            '
            Me.FunimationAuthRadioButton.AutoSize = True
            Me.FunimationAuthRadioButton.Location = New System.Drawing.Point(7, 44)
            Me.FunimationAuthRadioButton.Name = "FunimationAuthRadioButton"
            Me.FunimationAuthRadioButton.Size = New System.Drawing.Size(76, 17)
            Me.FunimationAuthRadioButton.TabIndex = 1
            Me.FunimationAuthRadioButton.TabStop = True
            Me.FunimationAuthRadioButton.Text = "Funimation"
            Me.FunimationAuthRadioButton.UseVisualStyleBackColor = True
            '
            'CrunchyrollAuthRadioButton
            '
            Me.CrunchyrollAuthRadioButton.AutoSize = True
            Me.CrunchyrollAuthRadioButton.Location = New System.Drawing.Point(7, 20)
            Me.CrunchyrollAuthRadioButton.Name = "CrunchyrollAuthRadioButton"
            Me.CrunchyrollAuthRadioButton.Size = New System.Drawing.Size(77, 17)
            Me.CrunchyrollAuthRadioButton.TabIndex = 0
            Me.CrunchyrollAuthRadioButton.TabStop = True
            Me.CrunchyrollAuthRadioButton.Text = "Crunchyroll"
            Me.CrunchyrollAuthRadioButton.UseVisualStyleBackColor = True
            '
            'TabPageQueue
            '
            Me.TabPageQueue.Controls.Add(Me.EpisodeNumberInput)
            Me.TabPageQueue.Controls.Add(Me.SeasonNumberInput)
            Me.TabPageQueue.Controls.Add(Me.EpisodeTitleTextBox)
            Me.TabPageQueue.Controls.Add(Me.Label9)
            Me.TabPageQueue.Controls.Add(Me.Label8)
            Me.TabPageQueue.Controls.Add(Me.ShowNameTextBox)
            Me.TabPageQueue.Controls.Add(Me.Label7)
            Me.TabPageQueue.Controls.Add(Me.AddQueueItemButton)
            Me.TabPageQueue.Controls.Add(Me.Label6)
            Me.TabPageQueue.Location = New System.Drawing.Point(4, 22)
            Me.TabPageQueue.Name = "TabPageQueue"
            Me.TabPageQueue.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPageQueue.Size = New System.Drawing.Size(768, 400)
            Me.TabPageQueue.TabIndex = 4
            Me.TabPageQueue.Text = "Download queue"
            Me.TabPageQueue.UseVisualStyleBackColor = True
            '
            'EpisodeNumberInput
            '
            Me.EpisodeNumberInput.Location = New System.Drawing.Point(109, 89)
            Me.EpisodeNumberInput.Name = "EpisodeNumberInput"
            Me.EpisodeNumberInput.Size = New System.Drawing.Size(120, 20)
            Me.EpisodeNumberInput.TabIndex = 6
            Me.EpisodeNumberInput.Value = New Decimal(New Integer() {1, 0, 0, 0})
            '
            'SeasonNumberInput
            '
            Me.SeasonNumberInput.Location = New System.Drawing.Point(109, 35)
            Me.SeasonNumberInput.Name = "SeasonNumberInput"
            Me.SeasonNumberInput.Size = New System.Drawing.Size(120, 20)
            Me.SeasonNumberInput.TabIndex = 4
            Me.SeasonNumberInput.Value = New Decimal(New Integer() {1, 0, 0, 0})
            '
            'EpisodeTitleTextBox
            '
            Me.EpisodeTitleTextBox.Location = New System.Drawing.Point(109, 61)
            Me.EpisodeTitleTextBox.Name = "EpisodeTitleTextBox"
            Me.EpisodeTitleTextBox.Size = New System.Drawing.Size(268, 20)
            Me.EpisodeTitleTextBox.TabIndex = 5
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Location = New System.Drawing.Point(7, 91)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(85, 13)
            Me.Label9.TabIndex = 5
            Me.Label9.Text = "Episode Number"
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Location = New System.Drawing.Point(7, 64)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(68, 13)
            Me.Label8.TabIndex = 4
            Me.Label8.Text = "Episode Title"
            '
            'ShowNameTextBox
            '
            Me.ShowNameTextBox.Location = New System.Drawing.Point(109, 7)
            Me.ShowNameTextBox.Name = "ShowNameTextBox"
            Me.ShowNameTextBox.Size = New System.Drawing.Size(268, 20)
            Me.ShowNameTextBox.TabIndex = 3
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(6, 37)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(83, 13)
            Me.Label7.TabIndex = 2
            Me.Label7.Text = "Season Number"
            '
            'AddQueueItemButton
            '
            Me.AddQueueItemButton.Location = New System.Drawing.Point(10, 115)
            Me.AddQueueItemButton.Name = "AddQueueItemButton"
            Me.AddQueueItemButton.Size = New System.Drawing.Size(121, 23)
            Me.AddQueueItemButton.TabIndex = 7
            Me.AddQueueItemButton.Text = "Add to Queue"
            Me.AddQueueItemButton.UseVisualStyleBackColor = True
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(7, 10)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(65, 13)
            Me.Label6.TabIndex = 0
            Me.Label6.Text = "Show Name"
            '
            'TabPagePlayback
            '
            Me.TabPagePlayback.Controls.Add(Me.PlaybackDownloadStatusLabel)
            Me.TabPagePlayback.Controls.Add(Me.DownloadHlsMasterPlaylistButton)
            Me.TabPagePlayback.Controls.Add(Me.DownloadHlsMediaButton)
            Me.TabPagePlayback.Controls.Add(Me.DownloadCompleteMediaButton)
            Me.TabPagePlayback.Controls.Add(Me.Label13)
            Me.TabPagePlayback.Controls.Add(Me.MediaUrlTextBox)
            Me.TabPagePlayback.Controls.Add(Me.SelectOutputFolderButton)
            Me.TabPagePlayback.Controls.Add(Me.OutputFolderTextBox)
            Me.TabPagePlayback.Controls.Add(Me.SelectTemporaryFolderButton)
            Me.TabPagePlayback.Controls.Add(Me.TemporaryFolderTextBox)
            Me.TabPagePlayback.Location = New System.Drawing.Point(4, 22)
            Me.TabPagePlayback.Name = "TabPagePlayback"
            Me.TabPagePlayback.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPagePlayback.Size = New System.Drawing.Size(768, 400)
            Me.TabPagePlayback.TabIndex = 6
            Me.TabPagePlayback.Text = "Playback Download"
            Me.TabPagePlayback.UseVisualStyleBackColor = True
            '
            'PlaybackDownloadStatusLabel
            '
            Me.PlaybackDownloadStatusLabel.AutoSize = True
            Me.PlaybackDownloadStatusLabel.Location = New System.Drawing.Point(9, 206)
            Me.PlaybackDownloadStatusLabel.Name = "PlaybackDownloadStatusLabel"
            Me.PlaybackDownloadStatusLabel.Size = New System.Drawing.Size(0, 13)
            Me.PlaybackDownloadStatusLabel.TabIndex = 9
            '
            'DownloadHlsMasterPlaylistButton
            '
            Me.DownloadHlsMasterPlaylistButton.Location = New System.Drawing.Point(338, 175)
            Me.DownloadHlsMasterPlaylistButton.Name = "DownloadHlsMasterPlaylistButton"
            Me.DownloadHlsMasterPlaylistButton.Size = New System.Drawing.Size(172, 23)
            Me.DownloadHlsMasterPlaylistButton.TabIndex = 8
            Me.DownloadHlsMasterPlaylistButton.Text = "Download HLS Master playlist"
            Me.DownloadHlsMasterPlaylistButton.UseVisualStyleBackColor = True
            '
            'DownloadHlsMediaButton
            '
            Me.DownloadHlsMediaButton.Location = New System.Drawing.Point(167, 176)
            Me.DownloadHlsMediaButton.Name = "DownloadHlsMediaButton"
            Me.DownloadHlsMediaButton.Size = New System.Drawing.Size(165, 23)
            Me.DownloadHlsMediaButton.TabIndex = 7
            Me.DownloadHlsMediaButton.Text = "Download HLS Media Playlist"
            Me.DownloadHlsMediaButton.UseVisualStyleBackColor = True
            '
            'DownloadCompleteMediaButton
            '
            Me.DownloadCompleteMediaButton.Location = New System.Drawing.Point(6, 176)
            Me.DownloadCompleteMediaButton.Name = "DownloadCompleteMediaButton"
            Me.DownloadCompleteMediaButton.Size = New System.Drawing.Size(155, 23)
            Me.DownloadCompleteMediaButton.TabIndex = 6
            Me.DownloadCompleteMediaButton.Text = "Download Complete Media"
            Me.DownloadCompleteMediaButton.UseVisualStyleBackColor = True
            '
            'Label13
            '
            Me.Label13.AutoSize = True
            Me.Label13.Location = New System.Drawing.Point(6, 133)
            Me.Label13.Name = "Label13"
            Me.Label13.Size = New System.Drawing.Size(61, 13)
            Me.Label13.TabIndex = 5
            Me.Label13.Text = "Media URL"
            '
            'MediaUrlTextBox
            '
            Me.MediaUrlTextBox.Location = New System.Drawing.Point(6, 149)
            Me.MediaUrlTextBox.Name = "MediaUrlTextBox"
            Me.MediaUrlTextBox.Size = New System.Drawing.Size(756, 20)
            Me.MediaUrlTextBox.TabIndex = 4
            '
            'SelectOutputFolderButton
            '
            Me.SelectOutputFolderButton.Location = New System.Drawing.Point(6, 88)
            Me.SelectOutputFolderButton.Name = "SelectOutputFolderButton"
            Me.SelectOutputFolderButton.Size = New System.Drawing.Size(182, 23)
            Me.SelectOutputFolderButton.TabIndex = 3
            Me.SelectOutputFolderButton.Text = "Select Output Folder"
            Me.SelectOutputFolderButton.UseVisualStyleBackColor = True
            '
            'OutputFolderTextBox
            '
            Me.OutputFolderTextBox.Location = New System.Drawing.Point(6, 62)
            Me.OutputFolderTextBox.Name = "OutputFolderTextBox"
            Me.OutputFolderTextBox.ReadOnly = True
            Me.OutputFolderTextBox.Size = New System.Drawing.Size(756, 20)
            Me.OutputFolderTextBox.TabIndex = 2
            '
            'SelectTemporaryFolderButton
            '
            Me.SelectTemporaryFolderButton.Location = New System.Drawing.Point(6, 33)
            Me.SelectTemporaryFolderButton.Name = "SelectTemporaryFolderButton"
            Me.SelectTemporaryFolderButton.Size = New System.Drawing.Size(182, 23)
            Me.SelectTemporaryFolderButton.TabIndex = 1
            Me.SelectTemporaryFolderButton.Text = "Select Temporary Folder"
            Me.SelectTemporaryFolderButton.UseVisualStyleBackColor = True
            '
            'TemporaryFolderTextBox
            '
            Me.TemporaryFolderTextBox.Location = New System.Drawing.Point(6, 6)
            Me.TemporaryFolderTextBox.Name = "TemporaryFolderTextBox"
            Me.TemporaryFolderTextBox.ReadOnly = True
            Me.TemporaryFolderTextBox.Size = New System.Drawing.Size(756, 20)
            Me.TemporaryFolderTextBox.TabIndex = 0
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
            CType(Me.RewriteUrlsCountNumericInput, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox3.ResumeLayout(False)
            Me.GroupBox3.PerformLayout()
            Me.TabPagePlaylistSelection.ResumeLayout(False)
            Me.TabPagePlaylistSelection.PerformLayout()
            CType(Me.TargetResolutionInput, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox5.ResumeLayout(False)
            Me.GroupBox5.PerformLayout()
            Me.TabPageBrowser.ResumeLayout(False)
            Me.TabPageBrowser.PerformLayout()
            Me.TabPageAuthentication.ResumeLayout(False)
            Me.TabPageAuthentication.PerformLayout()
            Me.GroupBox4.ResumeLayout(False)
            Me.GroupBox4.PerformLayout()
            Me.TabPageQueue.ResumeLayout(False)
            Me.TabPageQueue.PerformLayout()
            CType(Me.EpisodeNumberInput, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.SeasonNumberInput, System.ComponentModel.ISupportInitialize).EndInit()
            Me.TabPagePlayback.ResumeLayout(False)
            Me.TabPagePlayback.PerformLayout()
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
        Friend WithEvents GroupBox3 As GroupBox
        Friend WithEvents MediaPlaylistRadioButton As RadioButton
        Friend WithEvents MasterPlaylistRadioButton As RadioButton
        Friend WithEvents EpisodePlaybackRadioButton As RadioButton
        Friend WithEvents TabPageBrowser As TabPage
        Friend WithEvents CookiesOutputTextBox As TextBox
        Friend WithEvents Label3 As Label
        Friend WithEvents CookieDomainTextBox As TextBox
        Friend WithEvents GetBrowserCookiesButton As Button
        Friend WithEvents TabPageAuthentication As TabPage
        Friend WithEvents GetLoginTokenButton As Button
        Friend WithEvents GroupBox4 As GroupBox
        Friend WithEvents FunimationAuthRadioButton As RadioButton
        Friend WithEvents CrunchyrollAuthRadioButton As RadioButton
        Friend WithEvents IsPaidAccountButton As Button
        Friend WithEvents AuthenticationOutputTextBox As TextBox
        Friend WithEvents AuthenticateButton As Button
        Friend WithEvents Label4 As Label
        Friend WithEvents AuthenticateUrlTextBox As TextBox
        Friend WithEvents LoginTokenTextBox As TextBox
        Friend WithEvents Label5 As Label
        Friend WithEvents TabPageQueue As TabPage
        Friend WithEvents EpisodeNumberInput As NumericUpDown
        Friend WithEvents SeasonNumberInput As NumericUpDown
        Friend WithEvents EpisodeTitleTextBox As TextBox
        Friend WithEvents Label9 As Label
        Friend WithEvents Label8 As Label
        Friend WithEvents ShowNameTextBox As TextBox
        Friend WithEvents Label7 As Label
        Friend WithEvents AddQueueItemButton As Button
        Friend WithEvents Label6 As Label
        Friend WithEvents RewriteUrlsCheckBox As CheckBox
        Friend WithEvents Label10 As Label
        Friend WithEvents RewriteUrlTextBox As TextBox
        Friend WithEvents RewriteUrlsCountNumericInput As NumericUpDown
        Friend WithEvents RewritePlaylistCheckBox As CheckBox
        Friend WithEvents TabPagePlaylistSelection As TabPage
        Friend WithEvents SelectVariantButton As Button
        Friend WithEvents GroupBox5 As GroupBox
        Friend WithEvents TargetResolutionRadioButton As RadioButton
        Friend WithEvents HighResolutionRadioButton As RadioButton
        Friend WithEvents LowResolutionRadioButton As RadioButton
        Friend WithEvents Label11 As Label
        Friend WithEvents MasterPlaylistTextBox As TextBox
        Friend WithEvents TargetResolutionInput As NumericUpDown
        Friend WithEvents HigherResolutionCheckBox As CheckBox
        Friend WithEvents HighBandwidthCheckBox As CheckBox
        Friend WithEvents Label12 As Label
        Friend WithEvents SelectedVariantStreamOutput As TextBox
        Friend WithEvents TabPagePlayback As TabPage
        Friend WithEvents SelectOutputFolderButton As Button
        Friend WithEvents OutputFolderTextBox As TextBox
        Friend WithEvents SelectTemporaryFolderButton As Button
        Friend WithEvents TemporaryFolderTextBox As TextBox
        Friend WithEvents PlaybackFolderDialog As FolderBrowserDialog
        Friend WithEvents DownloadCompleteMediaButton As Button
        Friend WithEvents Label13 As Label
        Friend WithEvents MediaUrlTextBox As TextBox
        Friend WithEvents DownloadHlsMediaButton As Button
        Friend WithEvents DownloadHlsMasterPlaylistButton As Button
        Friend WithEvents PlaybackDownloadStatusLabel As Label
    End Class
End Namespace