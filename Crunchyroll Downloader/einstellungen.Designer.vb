<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Einstellungen
    Inherits MetroFramework.Forms.MetroForm

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CR_SoftSubDefault = New MetroFramework.Controls.MetroComboBox()
        Me.SoftSubs = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CrunchyrollSoftSubsCheckedListBox = New System.Windows.Forms.CheckedListBox()
        Me.GB_SubLanguage = New System.Windows.Forms.GroupBox()
        Me.CrunchyrollHardsubComboBox = New MetroFramework.Controls.MetroComboBox()
        Me.TabPage2 = New MetroFramework.Controls.MetroTabPage()
        Me.GroupBox18 = New System.Windows.Forms.GroupBox()
        Me.UseQueueCheckbox = New MetroFramework.Controls.MetroCheckBox()
        Me.GroupBox16 = New System.Windows.Forms.GroupBox()
        Me.MetroLabel3 = New MetroFramework.Controls.MetroLabel()
        Me.TemporaryFolderTextBox = New MetroFramework.Controls.MetroTextBox()
        Me.DownloadModeDropdown = New MetroFramework.Controls.MetroComboBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.CB_Merge = New MetroFramework.Controls.MetroComboBox()
        Me.CB_Format = New MetroFramework.Controls.MetroComboBox()
        Me.FfmpegCommandGroupBox = New System.Windows.Forms.GroupBox()
        Me.FfmpegCopyCheckBox = New MetroFramework.Controls.MetroCheckBox()
        Me.TargetBitrateCheckBox = New MetroFramework.Controls.MetroCheckBox()
        Me.VideoCodecComboBox = New MetroFramework.Controls.MetroComboBox()
        Me.MetroLabel6 = New MetroFramework.Controls.MetroLabel()
        Me.VideoEncoderComboBox = New MetroFramework.Controls.MetroComboBox()
        Me.MetroLabel5 = New MetroFramework.Controls.MetroLabel()
        Me.FfmpegPresetComboBox = New MetroFramework.Controls.MetroComboBox()
        Me.MetroLabel4 = New MetroFramework.Controls.MetroLabel()
        Me.BitrateNumericInput = New System.Windows.Forms.NumericUpDown()
        Me.MetroLabel2 = New MetroFramework.Controls.MetroLabel()
        Me.FfmpegCommandPreviewTextBox = New MetroFramework.Controls.MetroTextBox()
        Me.GB_Resolution = New System.Windows.Forms.GroupBox()
        Me.AAuto = New MetroFramework.Controls.MetroRadioButton()
        Me.A480p = New MetroFramework.Controls.MetroRadioButton()
        Me.A360p = New MetroFramework.Controls.MetroRadioButton()
        Me.A720p = New MetroFramework.Controls.MetroRadioButton()
        Me.A1080p = New MetroFramework.Controls.MetroRadioButton()
        Me.TabPage1 = New MetroFramework.Controls.MetroTabPage()
        Me.GroupBox21 = New System.Windows.Forms.GroupBox()
        Me.CB_HideSF = New MetroFramework.Controls.MetroComboBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
        Me.CheckBox2 = New MetroFramework.Controls.MetroCheckBox()
        Me.Label2 = New MetroFramework.Controls.MetroLabel()
        Me.ErrorLimitInput = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.CustomServerPortInput = New System.Windows.Forms.NumericUpDown()
        Me.ServerPortLabel = New MetroFramework.Controls.MetroLabel()
        Me.Chb_Ign_tls = New MetroFramework.Controls.MetroCheckBox()
        Me.ServerPortInput = New MetroFramework.Controls.MetroComboBox()
        Me.DarkMode = New MetroFramework.Controls.MetroCheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New MetroFramework.Controls.MetroLabel()
        Me.DefaultWebsiteTextBox = New MetroFramework.Controls.MetroTextBox()
        Me.DL_Count_simultaneous = New System.Windows.Forms.GroupBox()
        Me.SimultaneousDownloadsInput = New System.Windows.Forms.NumericUpDown()
        Me.TabControl1 = New MetroFramework.Controls.MetroTabControl()
        Me.MetroTabPage2 = New MetroFramework.Controls.MetroTabPage()
        Me.GroupBox17 = New System.Windows.Forms.GroupBox()
        Me.LeadingZerosComboBox = New MetroFramework.Controls.MetroComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.IncludeLanguageNameCheckBox = New MetroFramework.Controls.MetroCheckBox()
        Me.SubLanguageNamingComboBox = New MetroFramework.Controls.MetroComboBox()
        Me.GB_Filename_Pre = New System.Windows.Forms.GroupBox()
        Me.SeasonPrefixTextBox = New MetroFramework.Controls.MetroTextBox()
        Me.EpisodePrefixTextBox = New MetroFramework.Controls.MetroTextBox()
        Me.SeasonNumberBehaviorComboBox = New MetroFramework.Controls.MetroComboBox()
        Me.GroupBox12 = New System.Windows.Forms.GroupBox()
        Me.EpisodeTitleCheckBox = New MetroFramework.Controls.MetroCheckBox()
        Me.AudioLanguageCheckBox = New MetroFramework.Controls.MetroCheckBox()
        Me.EpisodeNumberCheckBox = New MetroFramework.Controls.MetroCheckBox()
        Me.KodiNamingCheckBox = New MetroFramework.Controls.MetroCheckBox()
        Me.SeasonNumberCheckBox = New MetroFramework.Controls.MetroCheckBox()
        Me.SeriesNameCheckBox = New MetroFramework.Controls.MetroCheckBox()
        Me.FilenameTemplatePreview = New MetroFramework.Controls.MetroTextBox()
        Me.MetroTabPage1 = New MetroFramework.Controls.MetroTabPage()
        Me.GroupBox20 = New System.Windows.Forms.GroupBox()
        Me.ChB_Chapters = New MetroFramework.Controls.MetroCheckBox()
        Me.GroupBox19 = New System.Windows.Forms.GroupBox()
        Me.CrunchyrollAudioLanguageComboBox = New MetroFramework.Controls.MetroComboBox()
        Me.CrunchyrollAcceptHardsubsCheckBox = New MetroFramework.Controls.MetroCheckBox()
        Me.TabPage6 = New MetroFramework.Controls.MetroTabPage()
        Me.GroupBox15 = New System.Windows.Forms.GroupBox()
        Me.Bitrate_Funi = New MetroFramework.Controls.MetroComboBox()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.Fun_Dub_Over = New MetroFramework.Controls.MetroComboBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.CB_Fun_HardSubs = New MetroFramework.Controls.MetroComboBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.GroupBox13 = New System.Windows.Forms.GroupBox()
        Me.FunSubDef = New MetroFramework.Controls.MetroComboBox()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.CB_srt = New MetroFramework.Controls.MetroCheckBox()
        Me.CB_vtt = New MetroFramework.Controls.MetroCheckBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.CB_fun_eng = New MetroFramework.Controls.MetroCheckBox()
        Me.CB_fun_es = New MetroFramework.Controls.MetroCheckBox()
        Me.CB_fun_ptbr = New MetroFramework.Controls.MetroCheckBox()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.LastVersion = New MetroFramework.Controls.MetroLabel()
        Me.Label8 = New MetroFramework.Controls.MetroLabel()
        Me.MetroFrameworkLabel = New MetroFramework.Controls.MetroLabel()
        Me.WebviewLabel = New MetroFramework.Controls.MetroLabel()
        Me.FfmpegLabel = New MetroFramework.Controls.MetroLabel()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New MetroFramework.Controls.MetroLabel()
        Me.CurrentVersionLabel = New MetroFramework.Controls.MetroLabel()
        Me.Label5 = New MetroFramework.Controls.MetroLabel()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Btn_Save = New System.Windows.Forms.Button()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SoftSubs.SuspendLayout()
        Me.GB_SubLanguage.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox18.SuspendLayout()
        Me.GroupBox16.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.FfmpegCommandGroupBox.SuspendLayout()
        CType(Me.BitrateNumericInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GB_Resolution.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox21.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.ErrorLimitInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.CustomServerPortInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.DL_Count_simultaneous.SuspendLayout()
        CType(Me.SimultaneousDownloadsInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.MetroTabPage2.SuspendLayout()
        Me.GroupBox17.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GB_Filename_Pre.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        Me.MetroTabPage1.SuspendLayout()
        Me.GroupBox20.SuspendLayout()
        Me.GroupBox19.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox13.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 8000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.IsBalloon = True
        Me.ToolTip1.ReshowDelay = 100
        '
        'pictureBox1
        '
        Me.pictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox1.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_close
        Me.pictureBox1.Location = New System.Drawing.Point(494, 1)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(50, 40)
        Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pictureBox1.TabIndex = 7
        Me.pictureBox1.TabStop = False
        '
        'CR_SoftSubDefault
        '
        Me.CR_SoftSubDefault.DropDownHeight = 250
        Me.CR_SoftSubDefault.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CR_SoftSubDefault.FormattingEnabled = True
        Me.CR_SoftSubDefault.IntegralHeight = False
        Me.CR_SoftSubDefault.ItemHeight = 23
        Me.CR_SoftSubDefault.Items.AddRange(New Object() {"[Disabled]"})
        Me.CR_SoftSubDefault.Location = New System.Drawing.Point(241, 40)
        Me.CR_SoftSubDefault.Name = "CR_SoftSubDefault"
        Me.CR_SoftSubDefault.Size = New System.Drawing.Size(238, 29)
        Me.CR_SoftSubDefault.Sorted = True
        Me.CR_SoftSubDefault.TabIndex = 30
        Me.CR_SoftSubDefault.UseSelectable = True
        '
        'SoftSubs
        '
        Me.SoftSubs.BackColor = System.Drawing.Color.Transparent
        Me.SoftSubs.Controls.Add(Me.Label6)
        Me.SoftSubs.Controls.Add(Me.Label3)
        Me.SoftSubs.Controls.Add(Me.CR_SoftSubDefault)
        Me.SoftSubs.Controls.Add(Me.CrunchyrollSoftSubsCheckedListBox)
        Me.SoftSubs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SoftSubs.Location = New System.Drawing.Point(5, 190)
        Me.SoftSubs.Name = "SoftSubs"
        Me.SoftSubs.Size = New System.Drawing.Size(490, 172)
        Me.SoftSubs.TabIndex = 20
        Me.SoftSubs.TabStop = False
        Me.SoftSubs.Text = "SoftSubs"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(134, 16)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Subtitles to download"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(241, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 16)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Default Subtitle"
        '
        'CrunchyrollSoftSubsCheckedListBox
        '
        Me.CrunchyrollSoftSubsCheckedListBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CrunchyrollSoftSubsCheckedListBox.CheckOnClick = True
        Me.CrunchyrollSoftSubsCheckedListBox.FormattingEnabled = True
        Me.CrunchyrollSoftSubsCheckedListBox.Location = New System.Drawing.Point(7, 40)
        Me.CrunchyrollSoftSubsCheckedListBox.Name = "CrunchyrollSoftSubsCheckedListBox"
        Me.CrunchyrollSoftSubsCheckedListBox.Size = New System.Drawing.Size(228, 123)
        Me.CrunchyrollSoftSubsCheckedListBox.Sorted = True
        Me.CrunchyrollSoftSubsCheckedListBox.TabIndex = 0
        '
        'GB_SubLanguage
        '
        Me.GB_SubLanguage.BackColor = System.Drawing.Color.Transparent
        Me.GB_SubLanguage.Controls.Add(Me.CrunchyrollHardsubComboBox)
        Me.GB_SubLanguage.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GB_SubLanguage.ForeColor = System.Drawing.Color.Black
        Me.GB_SubLanguage.Location = New System.Drawing.Point(5, 120)
        Me.GB_SubLanguage.Name = "GB_SubLanguage"
        Me.GB_SubLanguage.Size = New System.Drawing.Size(490, 65)
        Me.GB_SubLanguage.TabIndex = 10
        Me.GB_SubLanguage.TabStop = False
        Me.GB_SubLanguage.Text = "Hardsub language"
        '
        'CrunchyrollHardsubComboBox
        '
        Me.CrunchyrollHardsubComboBox.DropDownHeight = 275
        Me.CrunchyrollHardsubComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CrunchyrollHardsubComboBox.FormattingEnabled = True
        Me.CrunchyrollHardsubComboBox.IntegralHeight = False
        Me.CrunchyrollHardsubComboBox.ItemHeight = 23
        Me.CrunchyrollHardsubComboBox.Location = New System.Drawing.Point(85, 25)
        Me.CrunchyrollHardsubComboBox.Name = "CrunchyrollHardsubComboBox"
        Me.CrunchyrollHardsubComboBox.Size = New System.Drawing.Size(320, 29)
        Me.CrunchyrollHardsubComboBox.TabIndex = 20
        Me.CrunchyrollHardsubComboBox.UseSelectable = True
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.GroupBox18)
        Me.TabPage2.Controls.Add(Me.GroupBox16)
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.FfmpegCommandGroupBox)
        Me.TabPage2.Controls.Add(Me.GB_Resolution)
        Me.TabPage2.HorizontalScrollbarBarColor = True
        Me.TabPage2.HorizontalScrollbarHighlightOnWheel = False
        Me.TabPage2.HorizontalScrollbarSize = 10
        Me.TabPage2.Location = New System.Drawing.Point(4, 35)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(501, 528)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Output"
        Me.TabPage2.VerticalScrollbarBarColor = True
        Me.TabPage2.VerticalScrollbarHighlightOnWheel = False
        Me.TabPage2.VerticalScrollbarSize = 10
        Me.TabPage2.Visible = False
        '
        'GroupBox18
        '
        Me.GroupBox18.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox18.Controls.Add(Me.UseQueueCheckbox)
        Me.GroupBox18.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox18.ForeColor = System.Drawing.Color.Black
        Me.GroupBox18.Location = New System.Drawing.Point(5, 150)
        Me.GroupBox18.Name = "GroupBox18"
        Me.GroupBox18.Size = New System.Drawing.Size(490, 59)
        Me.GroupBox18.TabIndex = 32
        Me.GroupBox18.TabStop = False
        Me.GroupBox18.Text = "Multi-Download"
        '
        'UseQueueCheckbox
        '
        Me.UseQueueCheckbox.AutoSize = True
        Me.UseQueueCheckbox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.UseQueueCheckbox.Location = New System.Drawing.Point(119, 21)
        Me.UseQueueCheckbox.Name = "UseQueueCheckbox"
        Me.UseQueueCheckbox.Size = New System.Drawing.Size(255, 19)
        Me.UseQueueCheckbox.TabIndex = 5
        Me.UseQueueCheckbox.Text = "redirect multi-download to the queue"
        Me.UseQueueCheckbox.UseSelectable = True
        '
        'GroupBox16
        '
        Me.GroupBox16.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox16.Controls.Add(Me.MetroLabel3)
        Me.GroupBox16.Controls.Add(Me.TemporaryFolderTextBox)
        Me.GroupBox16.Controls.Add(Me.DownloadModeDropdown)
        Me.GroupBox16.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox16.ForeColor = System.Drawing.Color.Black
        Me.GroupBox16.Location = New System.Drawing.Point(5, 11)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.Size = New System.Drawing.Size(490, 133)
        Me.GroupBox16.TabIndex = 31
        Me.GroupBox16.TabStop = False
        Me.GroupBox16.Text = "Download Mode"
        '
        'MetroLabel3
        '
        Me.MetroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.MetroLabel3.Location = New System.Drawing.Point(6, 64)
        Me.MetroLabel3.Name = "MetroLabel3"
        Me.MetroLabel3.Size = New System.Drawing.Size(469, 22)
        Me.MetroLabel3.TabIndex = 19
        Me.MetroLabel3.Text = "Temporary Folder"
        Me.MetroLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TemporaryFolderTextBox
        '
        '
        '
        '
        Me.TemporaryFolderTextBox.CustomButton.Image = Nothing
        Me.TemporaryFolderTextBox.CustomButton.Location = New System.Drawing.Point(441, 1)
        Me.TemporaryFolderTextBox.CustomButton.Name = ""
        Me.TemporaryFolderTextBox.CustomButton.Size = New System.Drawing.Size(23, 23)
        Me.TemporaryFolderTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.TemporaryFolderTextBox.CustomButton.TabIndex = 1
        Me.TemporaryFolderTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.TemporaryFolderTextBox.CustomButton.UseSelectable = True
        Me.TemporaryFolderTextBox.CustomButton.Visible = False
        Me.TemporaryFolderTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.TemporaryFolderTextBox.Lines = New String(-1) {}
        Me.TemporaryFolderTextBox.Location = New System.Drawing.Point(10, 90)
        Me.TemporaryFolderTextBox.MaxLength = 32767
        Me.TemporaryFolderTextBox.Name = "TemporaryFolderTextBox"
        Me.TemporaryFolderTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TemporaryFolderTextBox.ReadOnly = True
        Me.TemporaryFolderTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TemporaryFolderTextBox.SelectedText = ""
        Me.TemporaryFolderTextBox.SelectionLength = 0
        Me.TemporaryFolderTextBox.SelectionStart = 0
        Me.TemporaryFolderTextBox.ShortcutsEnabled = True
        Me.TemporaryFolderTextBox.Size = New System.Drawing.Size(465, 25)
        Me.TemporaryFolderTextBox.TabIndex = 20
        Me.TemporaryFolderTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TemporaryFolderTextBox.UseSelectable = True
        Me.TemporaryFolderTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TemporaryFolderTextBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'DownloadModeDropdown
        '
        Me.DownloadModeDropdown.DropDownHeight = 250
        Me.DownloadModeDropdown.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DownloadModeDropdown.FormattingEnabled = True
        Me.DownloadModeDropdown.IntegralHeight = False
        Me.DownloadModeDropdown.ItemHeight = 23
        Me.DownloadModeDropdown.Items.AddRange(New Object() {"Default - ffmpeg", "Hybrid Mode", "Hybrid Mode - keep cache"})
        Me.DownloadModeDropdown.Location = New System.Drawing.Point(119, 21)
        Me.DownloadModeDropdown.Name = "DownloadModeDropdown"
        Me.DownloadModeDropdown.Size = New System.Drawing.Size(225, 29)
        Me.DownloadModeDropdown.TabIndex = 18
        Me.DownloadModeDropdown.UseSelectable = True
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.CB_Merge)
        Me.GroupBox4.Controls.Add(Me.CB_Format)
        Me.GroupBox4.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox4.ForeColor = System.Drawing.Color.Black
        Me.GroupBox4.Location = New System.Drawing.Point(5, 270)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(490, 70)
        Me.GroupBox4.TabIndex = 40
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Format"
        '
        'CB_Merge
        '
        Me.CB_Merge.DropDownHeight = 250
        Me.CB_Merge.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_Merge.FormattingEnabled = True
        Me.CB_Merge.IntegralHeight = False
        Me.CB_Merge.ItemHeight = 23
        Me.CB_Merge.Items.AddRange(New Object() {"[merge disabled]"})
        Me.CB_Merge.Location = New System.Drawing.Point(265, 25)
        Me.CB_Merge.Name = "CB_Merge"
        Me.CB_Merge.Size = New System.Drawing.Size(175, 29)
        Me.CB_Merge.TabIndex = 19
        Me.CB_Merge.UseSelectable = True
        '
        'CB_Format
        '
        Me.CB_Format.DropDownHeight = 250
        Me.CB_Format.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_Format.FormattingEnabled = True
        Me.CB_Format.IntegralHeight = False
        Me.CB_Format.ItemHeight = 23
        Me.CB_Format.Location = New System.Drawing.Point(50, 25)
        Me.CB_Format.Name = "CB_Format"
        Me.CB_Format.Size = New System.Drawing.Size(175, 29)
        Me.CB_Format.TabIndex = 17
        Me.CB_Format.UseSelectable = True
        '
        'FfmpegCommandGroupBox
        '
        Me.FfmpegCommandGroupBox.BackColor = System.Drawing.Color.Transparent
        Me.FfmpegCommandGroupBox.Controls.Add(Me.FfmpegCopyCheckBox)
        Me.FfmpegCommandGroupBox.Controls.Add(Me.TargetBitrateCheckBox)
        Me.FfmpegCommandGroupBox.Controls.Add(Me.VideoCodecComboBox)
        Me.FfmpegCommandGroupBox.Controls.Add(Me.MetroLabel6)
        Me.FfmpegCommandGroupBox.Controls.Add(Me.VideoEncoderComboBox)
        Me.FfmpegCommandGroupBox.Controls.Add(Me.MetroLabel5)
        Me.FfmpegCommandGroupBox.Controls.Add(Me.FfmpegPresetComboBox)
        Me.FfmpegCommandGroupBox.Controls.Add(Me.MetroLabel4)
        Me.FfmpegCommandGroupBox.Controls.Add(Me.BitrateNumericInput)
        Me.FfmpegCommandGroupBox.Controls.Add(Me.MetroLabel2)
        Me.FfmpegCommandGroupBox.Controls.Add(Me.FfmpegCommandPreviewTextBox)
        Me.FfmpegCommandGroupBox.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.FfmpegCommandGroupBox.ForeColor = System.Drawing.Color.Black
        Me.FfmpegCommandGroupBox.Location = New System.Drawing.Point(5, 350)
        Me.FfmpegCommandGroupBox.Name = "FfmpegCommandGroupBox"
        Me.FfmpegCommandGroupBox.Size = New System.Drawing.Size(490, 111)
        Me.FfmpegCommandGroupBox.TabIndex = 50
        Me.FfmpegCommandGroupBox.TabStop = False
        Me.FfmpegCommandGroupBox.Text = "ffmpeg command"
        '
        'FfmpegCopyCheckBox
        '
        Me.FfmpegCopyCheckBox.AutoSize = True
        Me.FfmpegCopyCheckBox.Location = New System.Drawing.Point(10, 21)
        Me.FfmpegCopyCheckBox.Name = "FfmpegCopyCheckBox"
        Me.FfmpegCopyCheckBox.Size = New System.Drawing.Size(51, 15)
        Me.FfmpegCopyCheckBox.TabIndex = 52
        Me.FfmpegCopyCheckBox.Text = "Copy"
        Me.FfmpegCopyCheckBox.UseSelectable = True
        '
        'TargetBitrateCheckBox
        '
        Me.TargetBitrateCheckBox.AutoSize = True
        Me.TargetBitrateCheckBox.Location = New System.Drawing.Point(362, 22)
        Me.TargetBitrateCheckBox.Name = "TargetBitrateCheckBox"
        Me.TargetBitrateCheckBox.Size = New System.Drawing.Size(92, 15)
        Me.TargetBitrateCheckBox.TabIndex = 51
        Me.TargetBitrateCheckBox.Text = "Target bitrate"
        Me.TargetBitrateCheckBox.UseSelectable = True
        '
        'VideoCodecComboBox
        '
        Me.VideoCodecComboBox.FormattingEnabled = True
        Me.VideoCodecComboBox.ItemHeight = 23
        Me.VideoCodecComboBox.Location = New System.Drawing.Point(90, 40)
        Me.VideoCodecComboBox.Name = "VideoCodecComboBox"
        Me.VideoCodecComboBox.Size = New System.Drawing.Size(65, 29)
        Me.VideoCodecComboBox.TabIndex = 50
        Me.VideoCodecComboBox.UseSelectable = True
        '
        'MetroLabel6
        '
        Me.MetroLabel6.AutoSize = True
        Me.MetroLabel6.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.MetroLabel6.Location = New System.Drawing.Point(90, 17)
        Me.MetroLabel6.Name = "MetroLabel6"
        Me.MetroLabel6.Size = New System.Drawing.Size(47, 19)
        Me.MetroLabel6.TabIndex = 49
        Me.MetroLabel6.Text = "Codec"
        '
        'VideoEncoderComboBox
        '
        Me.VideoEncoderComboBox.FormattingEnabled = True
        Me.VideoEncoderComboBox.ItemHeight = 23
        Me.VideoEncoderComboBox.Location = New System.Drawing.Point(161, 40)
        Me.VideoEncoderComboBox.Name = "VideoEncoderComboBox"
        Me.VideoEncoderComboBox.Size = New System.Drawing.Size(80, 29)
        Me.VideoEncoderComboBox.TabIndex = 48
        Me.VideoEncoderComboBox.UseSelectable = True
        '
        'MetroLabel5
        '
        Me.MetroLabel5.AutoSize = True
        Me.MetroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.MetroLabel5.Location = New System.Drawing.Point(161, 18)
        Me.MetroLabel5.Name = "MetroLabel5"
        Me.MetroLabel5.Size = New System.Drawing.Size(58, 19)
        Me.MetroLabel5.TabIndex = 47
        Me.MetroLabel5.Text = "Encoder"
        '
        'FfmpegPresetComboBox
        '
        Me.FfmpegPresetComboBox.FormattingEnabled = True
        Me.FfmpegPresetComboBox.ItemHeight = 23
        Me.FfmpegPresetComboBox.Location = New System.Drawing.Point(247, 40)
        Me.FfmpegPresetComboBox.Name = "FfmpegPresetComboBox"
        Me.FfmpegPresetComboBox.Size = New System.Drawing.Size(109, 29)
        Me.FfmpegPresetComboBox.TabIndex = 46
        Me.FfmpegPresetComboBox.UseSelectable = True
        '
        'MetroLabel4
        '
        Me.MetroLabel4.AutoSize = True
        Me.MetroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.MetroLabel4.Location = New System.Drawing.Point(247, 18)
        Me.MetroLabel4.Name = "MetroLabel4"
        Me.MetroLabel4.Size = New System.Drawing.Size(88, 19)
        Me.MetroLabel4.TabIndex = 45
        Me.MetroLabel4.Text = "Speed preset"
        '
        'BitrateNumericInput
        '
        Me.BitrateNumericInput.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.BitrateNumericInput.Location = New System.Drawing.Point(362, 41)
        Me.BitrateNumericInput.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.BitrateNumericInput.Minimum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.BitrateNumericInput.Name = "BitrateNumericInput"
        Me.BitrateNumericInput.Size = New System.Drawing.Size(67, 22)
        Me.BitrateNumericInput.TabIndex = 43
        Me.BitrateNumericInput.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        '
        'MetroLabel2
        '
        Me.MetroLabel2.AutoSize = True
        Me.MetroLabel2.Location = New System.Drawing.Point(435, 41)
        Me.MetroLabel2.Name = "MetroLabel2"
        Me.MetroLabel2.Size = New System.Drawing.Size(49, 19)
        Me.MetroLabel2.TabIndex = 44
        Me.MetroLabel2.Text = "(KBit/s)"
        '
        'FfmpegCommandPreviewTextBox
        '
        '
        '
        '
        Me.FfmpegCommandPreviewTextBox.CustomButton.Image = Nothing
        Me.FfmpegCommandPreviewTextBox.CustomButton.Location = New System.Drawing.Point(443, 1)
        Me.FfmpegCommandPreviewTextBox.CustomButton.Name = ""
        Me.FfmpegCommandPreviewTextBox.CustomButton.Size = New System.Drawing.Size(21, 21)
        Me.FfmpegCommandPreviewTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.FfmpegCommandPreviewTextBox.CustomButton.TabIndex = 1
        Me.FfmpegCommandPreviewTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.FfmpegCommandPreviewTextBox.CustomButton.UseSelectable = True
        Me.FfmpegCommandPreviewTextBox.CustomButton.Visible = False
        Me.FfmpegCommandPreviewTextBox.Lines = New String() {"(ffmpeg command)"}
        Me.FfmpegCommandPreviewTextBox.Location = New System.Drawing.Point(10, 82)
        Me.FfmpegCommandPreviewTextBox.MaxLength = 32767
        Me.FfmpegCommandPreviewTextBox.Name = "FfmpegCommandPreviewTextBox"
        Me.FfmpegCommandPreviewTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.FfmpegCommandPreviewTextBox.ReadOnly = True
        Me.FfmpegCommandPreviewTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.FfmpegCommandPreviewTextBox.SelectedText = ""
        Me.FfmpegCommandPreviewTextBox.SelectionLength = 0
        Me.FfmpegCommandPreviewTextBox.SelectionStart = 0
        Me.FfmpegCommandPreviewTextBox.ShortcutsEnabled = True
        Me.FfmpegCommandPreviewTextBox.Size = New System.Drawing.Size(465, 23)
        Me.FfmpegCommandPreviewTextBox.TabIndex = 38
        Me.FfmpegCommandPreviewTextBox.Text = "(ffmpeg command)"
        Me.FfmpegCommandPreviewTextBox.UseSelectable = True
        Me.FfmpegCommandPreviewTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.FfmpegCommandPreviewTextBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'GB_Resolution
        '
        Me.GB_Resolution.BackColor = System.Drawing.Color.Transparent
        Me.GB_Resolution.Controls.Add(Me.AAuto)
        Me.GB_Resolution.Controls.Add(Me.A480p)
        Me.GB_Resolution.Controls.Add(Me.A360p)
        Me.GB_Resolution.Controls.Add(Me.A720p)
        Me.GB_Resolution.Controls.Add(Me.A1080p)
        Me.GB_Resolution.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GB_Resolution.ForeColor = System.Drawing.Color.Black
        Me.GB_Resolution.Location = New System.Drawing.Point(5, 210)
        Me.GB_Resolution.Name = "GB_Resolution"
        Me.GB_Resolution.Size = New System.Drawing.Size(490, 59)
        Me.GB_Resolution.TabIndex = 30
        Me.GB_Resolution.TabStop = False
        Me.GB_Resolution.Text = "Resolution"
        '
        'AAuto
        '
        Me.AAuto.AutoSize = True
        Me.AAuto.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.AAuto.ForeColor = System.Drawing.Color.Black
        Me.AAuto.Location = New System.Drawing.Point(377, 21)
        Me.AAuto.Name = "AAuto"
        Me.AAuto.Size = New System.Drawing.Size(63, 19)
        Me.AAuto.TabIndex = 16
        Me.AAuto.TabStop = True
        Me.AAuto.Text = "[Auto]"
        Me.AAuto.UseSelectable = True
        '
        'A480p
        '
        Me.A480p.AutoSize = True
        Me.A480p.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.A480p.ForeColor = System.Drawing.Color.Black
        Me.A480p.Location = New System.Drawing.Point(198, 21)
        Me.A480p.Name = "A480p"
        Me.A480p.Size = New System.Drawing.Size(57, 19)
        Me.A480p.TabIndex = 14
        Me.A480p.TabStop = True
        Me.A480p.Text = "480p"
        Me.A480p.UseSelectable = True
        '
        'A360p
        '
        Me.A360p.AutoSize = True
        Me.A360p.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.A360p.ForeColor = System.Drawing.Color.Black
        Me.A360p.Location = New System.Drawing.Point(286, 21)
        Me.A360p.Name = "A360p"
        Me.A360p.Size = New System.Drawing.Size(57, 19)
        Me.A360p.TabIndex = 15
        Me.A360p.TabStop = True
        Me.A360p.Text = "360p"
        Me.A360p.UseSelectable = True
        '
        'A720p
        '
        Me.A720p.AutoSize = True
        Me.A720p.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.A720p.ForeColor = System.Drawing.Color.Black
        Me.A720p.Location = New System.Drawing.Point(119, 21)
        Me.A720p.Name = "A720p"
        Me.A720p.Size = New System.Drawing.Size(57, 19)
        Me.A720p.TabIndex = 13
        Me.A720p.TabStop = True
        Me.A720p.Text = "720p"
        Me.A720p.UseSelectable = True
        '
        'A1080p
        '
        Me.A1080p.AutoSize = True
        Me.A1080p.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.A1080p.ForeColor = System.Drawing.Color.Black
        Me.A1080p.Location = New System.Drawing.Point(25, 21)
        Me.A1080p.Name = "A1080p"
        Me.A1080p.Size = New System.Drawing.Size(65, 19)
        Me.A1080p.TabIndex = 12
        Me.A1080p.TabStop = True
        Me.A1080p.Text = "1080p"
        Me.A1080p.UseSelectable = True
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = True
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.GroupBox21)
        Me.TabPage1.Controls.Add(Me.GroupBox6)
        Me.TabPage1.Controls.Add(Me.GroupBox5)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.DL_Count_simultaneous)
        Me.TabPage1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.HorizontalScrollbar = True
        Me.TabPage1.HorizontalScrollbarBarColor = True
        Me.TabPage1.HorizontalScrollbarHighlightOnWheel = False
        Me.TabPage1.HorizontalScrollbarSize = 10
        Me.TabPage1.Location = New System.Drawing.Point(4, 35)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(501, 528)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "  Main"
        Me.TabPage1.VerticalScrollbar = True
        Me.TabPage1.VerticalScrollbarBarColor = True
        Me.TabPage1.VerticalScrollbarHighlightOnWheel = False
        Me.TabPage1.VerticalScrollbarSize = 10
        '
        'GroupBox21
        '
        Me.GroupBox21.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox21.Controls.Add(Me.CB_HideSF)
        Me.GroupBox21.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox21.ForeColor = System.Drawing.Color.Black
        Me.GroupBox21.Location = New System.Drawing.Point(5, 417)
        Me.GroupBox21.Name = "GroupBox21"
        Me.GroupBox21.Size = New System.Drawing.Size(490, 67)
        Me.GroupBox21.TabIndex = 81
        Me.GroupBox21.TabStop = False
        Me.GroupBox21.Text = "Subfolder"
        '
        'CB_HideSF
        '
        Me.CB_HideSF.DropDownHeight = 275
        Me.CB_HideSF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_HideSF.FormattingEnabled = True
        Me.CB_HideSF.IntegralHeight = False
        Me.CB_HideSF.ItemHeight = 23
        Me.CB_HideSF.Location = New System.Drawing.Point(82, 25)
        Me.CB_HideSF.Name = "CB_HideSF"
        Me.CB_HideSF.Size = New System.Drawing.Size(326, 29)
        Me.CB_HideSF.TabIndex = 21
        Me.CB_HideSF.UseSelectable = True
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox6.Controls.Add(Me.MetroLabel1)
        Me.GroupBox6.Controls.Add(Me.CheckBox2)
        Me.GroupBox6.Controls.Add(Me.Label2)
        Me.GroupBox6.Controls.Add(Me.ErrorLimitInput)
        Me.GroupBox6.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox6.ForeColor = System.Drawing.Color.Black
        Me.GroupBox6.Location = New System.Drawing.Point(5, 299)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(490, 112)
        Me.GroupBox6.TabIndex = 80
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Error Handling"
        '
        'MetroLabel1
        '
        Me.MetroLabel1.AutoSize = True
        Me.MetroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.MetroLabel1.Location = New System.Drawing.Point(194, 44)
        Me.MetroLabel1.Name = "MetroLabel1"
        Me.MetroLabel1.Size = New System.Drawing.Size(113, 19)
        Me.MetroLabel1.TabIndex = 45
        Me.MetroLabel1.Text = "(0 = deactivated)"
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(4, 70)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(125, 15)
        Me.CheckBox2.TabIndex = 44
        Me.CheckBox2.Text = "ignore future errors"
        Me.CheckBox2.UseSelectable = True
        Me.CheckBox2.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.Label2.Location = New System.Drawing.Point(65, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(386, 19)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "The amout of error(s) until the Download process get paused "
        '
        'ErrorLimitInput
        '
        Me.ErrorLimitInput.Location = New System.Drawing.Point(135, 70)
        Me.ErrorLimitInput.Name = "ErrorLimitInput"
        Me.ErrorLimitInput.Size = New System.Drawing.Size(200, 22)
        Me.ErrorLimitInput.TabIndex = 6
        Me.ErrorLimitInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.CustomServerPortInput)
        Me.GroupBox5.Controls.Add(Me.ServerPortLabel)
        Me.GroupBox5.Controls.Add(Me.Chb_Ign_tls)
        Me.GroupBox5.Controls.Add(Me.ServerPortInput)
        Me.GroupBox5.Controls.Add(Me.DarkMode)
        Me.GroupBox5.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(5, 179)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(490, 114)
        Me.GroupBox5.TabIndex = 70
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Other"
        '
        'CustomServerPortInput
        '
        Me.CustomServerPortInput.Enabled = False
        Me.CustomServerPortInput.Location = New System.Drawing.Point(355, 25)
        Me.CustomServerPortInput.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.CustomServerPortInput.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.CustomServerPortInput.Name = "CustomServerPortInput"
        Me.CustomServerPortInput.Size = New System.Drawing.Size(120, 22)
        Me.CustomServerPortInput.TabIndex = 48
        Me.CustomServerPortInput.Value = New Decimal(New Integer() {80, 0, 0, 0})
        '
        'ServerPortLabel
        '
        Me.ServerPortLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.ServerPortLabel.Location = New System.Drawing.Point(6, 21)
        Me.ServerPortLabel.Name = "ServerPortLabel"
        Me.ServerPortLabel.Size = New System.Drawing.Size(145, 29)
        Me.ServerPortLabel.TabIndex = 47
        Me.ServerPortLabel.Text = "Add-on server port"
        Me.ServerPortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Chb_Ign_tls
        '
        Me.Chb_Ign_tls.AutoSize = True
        Me.Chb_Ign_tls.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.Chb_Ign_tls.Location = New System.Drawing.Point(228, 75)
        Me.Chb_Ign_tls.Name = "Chb_Ign_tls"
        Me.Chb_Ign_tls.Size = New System.Drawing.Size(223, 19)
        Me.Chb_Ign_tls.TabIndex = 46
        Me.Chb_Ign_tls.Text = "add ""--insecure"" to curl requests"
        Me.Chb_Ign_tls.UseSelectable = True
        '
        'ServerPortInput
        '
        Me.ServerPortInput.DropDownHeight = 250
        Me.ServerPortInput.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ServerPortInput.FormattingEnabled = True
        Me.ServerPortInput.IntegralHeight = False
        Me.ServerPortInput.ItemHeight = 23
        Me.ServerPortInput.Location = New System.Drawing.Point(157, 21)
        Me.ServerPortInput.Name = "ServerPortInput"
        Me.ServerPortInput.Size = New System.Drawing.Size(192, 29)
        Me.ServerPortInput.TabIndex = 45
        Me.ServerPortInput.UseSelectable = True
        '
        'DarkMode
        '
        Me.DarkMode.AutoSize = True
        Me.DarkMode.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.DarkMode.Location = New System.Drawing.Point(56, 75)
        Me.DarkMode.Name = "DarkMode"
        Me.DarkMode.Size = New System.Drawing.Size(135, 19)
        Me.DarkMode.TabIndex = 5
        Me.DarkMode.Text = "enable dark mode"
        Me.DarkMode.UseSelectable = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.DefaultWebsiteTextBox)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(5, 85)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(490, 88)
        Me.GroupBox1.TabIndex = 60
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Browser Settings"
        '
        'Label1
        '
        Me.Label1.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.Label1.Location = New System.Drawing.Point(6, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(469, 22)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Default Website"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DefaultWebsiteTextBox
        '
        '
        '
        '
        Me.DefaultWebsiteTextBox.CustomButton.Image = Nothing
        Me.DefaultWebsiteTextBox.CustomButton.Location = New System.Drawing.Point(445, 1)
        Me.DefaultWebsiteTextBox.CustomButton.Name = ""
        Me.DefaultWebsiteTextBox.CustomButton.Size = New System.Drawing.Size(23, 23)
        Me.DefaultWebsiteTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.DefaultWebsiteTextBox.CustomButton.TabIndex = 1
        Me.DefaultWebsiteTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.DefaultWebsiteTextBox.CustomButton.UseSelectable = True
        Me.DefaultWebsiteTextBox.CustomButton.Visible = False
        Me.DefaultWebsiteTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.DefaultWebsiteTextBox.Lines = New String() {"https://www.crunchyroll.com/"}
        Me.DefaultWebsiteTextBox.Location = New System.Drawing.Point(6, 45)
        Me.DefaultWebsiteTextBox.MaxLength = 32767
        Me.DefaultWebsiteTextBox.Name = "DefaultWebsiteTextBox"
        Me.DefaultWebsiteTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.DefaultWebsiteTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.DefaultWebsiteTextBox.SelectedText = ""
        Me.DefaultWebsiteTextBox.SelectionLength = 0
        Me.DefaultWebsiteTextBox.SelectionStart = 0
        Me.DefaultWebsiteTextBox.ShortcutsEnabled = True
        Me.DefaultWebsiteTextBox.Size = New System.Drawing.Size(469, 25)
        Me.DefaultWebsiteTextBox.TabIndex = 2
        Me.DefaultWebsiteTextBox.Text = "https://www.crunchyroll.com/"
        Me.DefaultWebsiteTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DefaultWebsiteTextBox.UseSelectable = True
        Me.DefaultWebsiteTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.DefaultWebsiteTextBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'DL_Count_simultaneous
        '
        Me.DL_Count_simultaneous.BackColor = System.Drawing.Color.Transparent
        Me.DL_Count_simultaneous.Controls.Add(Me.SimultaneousDownloadsInput)
        Me.DL_Count_simultaneous.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.DL_Count_simultaneous.ForeColor = System.Drawing.Color.Black
        Me.DL_Count_simultaneous.Location = New System.Drawing.Point(5, 11)
        Me.DL_Count_simultaneous.Name = "DL_Count_simultaneous"
        Me.DL_Count_simultaneous.Size = New System.Drawing.Size(490, 68)
        Me.DL_Count_simultaneous.TabIndex = 50
        Me.DL_Count_simultaneous.TabStop = False
        Me.DL_Count_simultaneous.Text = "simultaneous downloads"
        '
        'SimultaneousDownloadsInput
        '
        Me.SimultaneousDownloadsInput.Location = New System.Drawing.Point(98, 30)
        Me.SimultaneousDownloadsInput.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.SimultaneousDownloadsInput.Name = "SimultaneousDownloadsInput"
        Me.SimultaneousDownloadsInput.Size = New System.Drawing.Size(265, 22)
        Me.SimultaneousDownloadsInput.TabIndex = 1
        Me.SimultaneousDownloadsInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.SimultaneousDownloadsInput.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.MetroTabPage2)
        Me.TabControl1.Controls.Add(Me.MetroTabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.FontSize = MetroFramework.MetroTabControlSize.Tall
        Me.TabControl1.FontWeight = MetroFramework.MetroTabControlWeight.Regular
        Me.TabControl1.Location = New System.Drawing.Point(22, 60)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 3
        Me.TabControl1.Size = New System.Drawing.Size(509, 567)
        Me.TabControl1.TabIndex = 0
        Me.TabControl1.UseSelectable = True
        '
        'MetroTabPage2
        '
        Me.MetroTabPage2.Controls.Add(Me.GroupBox17)
        Me.MetroTabPage2.Controls.Add(Me.GroupBox3)
        Me.MetroTabPage2.Controls.Add(Me.GB_Filename_Pre)
        Me.MetroTabPage2.Controls.Add(Me.GroupBox12)
        Me.MetroTabPage2.HorizontalScrollbarBarColor = True
        Me.MetroTabPage2.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroTabPage2.HorizontalScrollbarSize = 10
        Me.MetroTabPage2.Location = New System.Drawing.Point(4, 35)
        Me.MetroTabPage2.Name = "MetroTabPage2"
        Me.MetroTabPage2.Size = New System.Drawing.Size(501, 528)
        Me.MetroTabPage2.TabIndex = 8
        Me.MetroTabPage2.Text = "Naming"
        Me.MetroTabPage2.VerticalScrollbarBarColor = True
        Me.MetroTabPage2.VerticalScrollbarHighlightOnWheel = False
        Me.MetroTabPage2.VerticalScrollbarSize = 10
        '
        'GroupBox17
        '
        Me.GroupBox17.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox17.Controls.Add(Me.LeadingZerosComboBox)
        Me.GroupBox17.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox17.ForeColor = System.Drawing.Color.Black
        Me.GroupBox17.Location = New System.Drawing.Point(5, 300)
        Me.GroupBox17.Name = "GroupBox17"
        Me.GroupBox17.Size = New System.Drawing.Size(490, 67)
        Me.GroupBox17.TabIndex = 40
        Me.GroupBox17.TabStop = False
        Me.GroupBox17.Text = "Filename Prefix"
        '
        'LeadingZerosComboBox
        '
        Me.LeadingZerosComboBox.DropDownHeight = 250
        Me.LeadingZerosComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LeadingZerosComboBox.FormattingEnabled = True
        Me.LeadingZerosComboBox.IntegralHeight = False
        Me.LeadingZerosComboBox.ItemHeight = 23
        Me.LeadingZerosComboBox.Items.AddRange(New Object() {"1", "01", "001", "0001"})
        Me.LeadingZerosComboBox.Location = New System.Drawing.Point(123, 21)
        Me.LeadingZerosComboBox.Name = "LeadingZerosComboBox"
        Me.LeadingZerosComboBox.Size = New System.Drawing.Size(225, 29)
        Me.LeadingZerosComboBox.TabIndex = 20
        Me.LeadingZerosComboBox.UseSelectable = True
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.IncludeLanguageNameCheckBox)
        Me.GroupBox3.Controls.Add(Me.SubLanguageNamingComboBox)
        Me.GroupBox3.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox3.ForeColor = System.Drawing.Color.Black
        Me.GroupBox3.Location = New System.Drawing.Point(5, 380)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(490, 129)
        Me.GroupBox3.TabIndex = 52
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Subtitle File naming"
        '
        'IncludeLanguageNameCheckBox
        '
        Me.IncludeLanguageNameCheckBox.AutoSize = True
        Me.IncludeLanguageNameCheckBox.Location = New System.Drawing.Point(128, 36)
        Me.IncludeLanguageNameCheckBox.Name = "IncludeLanguageNameCheckBox"
        Me.IncludeLanguageNameCheckBox.Size = New System.Drawing.Size(244, 15)
        Me.IncludeLanguageNameCheckBox.TabIndex = 32
        Me.IncludeLanguageNameCheckBox.Text = "Include language name in first subtitle file"
        Me.IncludeLanguageNameCheckBox.UseSelectable = True
        '
        'SubLanguageNamingComboBox
        '
        Me.SubLanguageNamingComboBox.DropDownHeight = 250
        Me.SubLanguageNamingComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubLanguageNamingComboBox.FormattingEnabled = True
        Me.SubLanguageNamingComboBox.IntegralHeight = False
        Me.SubLanguageNamingComboBox.ItemHeight = 23
        Me.SubLanguageNamingComboBox.Items.AddRange(New Object() {"Crunchyroll language names", "ISO639-2 language codes", "Crunchyroll + ISO639-2 language codes"})
        Me.SubLanguageNamingComboBox.Location = New System.Drawing.Point(87, 80)
        Me.SubLanguageNamingComboBox.Name = "SubLanguageNamingComboBox"
        Me.SubLanguageNamingComboBox.Size = New System.Drawing.Size(326, 29)
        Me.SubLanguageNamingComboBox.TabIndex = 31
        Me.SubLanguageNamingComboBox.UseSelectable = True
        '
        'GB_Filename_Pre
        '
        Me.GB_Filename_Pre.BackColor = System.Drawing.Color.Transparent
        Me.GB_Filename_Pre.Controls.Add(Me.SeasonPrefixTextBox)
        Me.GB_Filename_Pre.Controls.Add(Me.EpisodePrefixTextBox)
        Me.GB_Filename_Pre.Controls.Add(Me.SeasonNumberBehaviorComboBox)
        Me.GB_Filename_Pre.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GB_Filename_Pre.ForeColor = System.Drawing.Color.Black
        Me.GB_Filename_Pre.Location = New System.Drawing.Point(5, 172)
        Me.GB_Filename_Pre.Name = "GB_Filename_Pre"
        Me.GB_Filename_Pre.Size = New System.Drawing.Size(490, 123)
        Me.GB_Filename_Pre.TabIndex = 22
        Me.GB_Filename_Pre.TabStop = False
        Me.GB_Filename_Pre.Text = "Filename Extras"
        '
        'SeasonPrefixTextBox
        '
        '
        '
        '
        Me.SeasonPrefixTextBox.CustomButton.Image = Nothing
        Me.SeasonPrefixTextBox.CustomButton.Location = New System.Drawing.Point(197, 1)
        Me.SeasonPrefixTextBox.CustomButton.Name = ""
        Me.SeasonPrefixTextBox.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.SeasonPrefixTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.SeasonPrefixTextBox.CustomButton.TabIndex = 1
        Me.SeasonPrefixTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.SeasonPrefixTextBox.CustomButton.UseSelectable = True
        Me.SeasonPrefixTextBox.CustomButton.Visible = False
        Me.SeasonPrefixTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.SeasonPrefixTextBox.Lines = New String(-1) {}
        Me.SeasonPrefixTextBox.Location = New System.Drawing.Point(6, 69)
        Me.SeasonPrefixTextBox.MaxLength = 32767
        Me.SeasonPrefixTextBox.Name = "SeasonPrefixTextBox"
        Me.SeasonPrefixTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.SeasonPrefixTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.SeasonPrefixTextBox.SelectedText = ""
        Me.SeasonPrefixTextBox.SelectionLength = 0
        Me.SeasonPrefixTextBox.SelectionStart = 0
        Me.SeasonPrefixTextBox.ShortcutsEnabled = True
        Me.SeasonPrefixTextBox.Size = New System.Drawing.Size(225, 29)
        Me.SeasonPrefixTextBox.TabIndex = 33
        Me.SeasonPrefixTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.SeasonPrefixTextBox.UseSelectable = True
        Me.SeasonPrefixTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.SeasonPrefixTextBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'EpisodePrefixTextBox
        '
        '
        '
        '
        Me.EpisodePrefixTextBox.CustomButton.Image = Nothing
        Me.EpisodePrefixTextBox.CustomButton.Location = New System.Drawing.Point(197, 1)
        Me.EpisodePrefixTextBox.CustomButton.Name = ""
        Me.EpisodePrefixTextBox.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.EpisodePrefixTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.EpisodePrefixTextBox.CustomButton.TabIndex = 1
        Me.EpisodePrefixTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.EpisodePrefixTextBox.CustomButton.UseSelectable = True
        Me.EpisodePrefixTextBox.CustomButton.Visible = False
        Me.EpisodePrefixTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.EpisodePrefixTextBox.Lines = New String(-1) {}
        Me.EpisodePrefixTextBox.Location = New System.Drawing.Point(248, 69)
        Me.EpisodePrefixTextBox.MaxLength = 32767
        Me.EpisodePrefixTextBox.Name = "EpisodePrefixTextBox"
        Me.EpisodePrefixTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.EpisodePrefixTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.EpisodePrefixTextBox.SelectedText = ""
        Me.EpisodePrefixTextBox.SelectionLength = 0
        Me.EpisodePrefixTextBox.SelectionStart = 0
        Me.EpisodePrefixTextBox.ShortcutsEnabled = True
        Me.EpisodePrefixTextBox.Size = New System.Drawing.Size(225, 29)
        Me.EpisodePrefixTextBox.TabIndex = 34
        Me.EpisodePrefixTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.EpisodePrefixTextBox.UseSelectable = True
        Me.EpisodePrefixTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.EpisodePrefixTextBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'SeasonNumberBehaviorComboBox
        '
        Me.SeasonNumberBehaviorComboBox.DropDownHeight = 250
        Me.SeasonNumberBehaviorComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SeasonNumberBehaviorComboBox.FormattingEnabled = True
        Me.SeasonNumberBehaviorComboBox.IntegralHeight = False
        Me.SeasonNumberBehaviorComboBox.ItemHeight = 23
        Me.SeasonNumberBehaviorComboBox.Items.AddRange(New Object() {"[Default] use season numbers", "ignore Season 1", "ignore all season numbers"})
        Me.SeasonNumberBehaviorComboBox.Location = New System.Drawing.Point(123, 21)
        Me.SeasonNumberBehaviorComboBox.Name = "SeasonNumberBehaviorComboBox"
        Me.SeasonNumberBehaviorComboBox.Size = New System.Drawing.Size(225, 29)
        Me.SeasonNumberBehaviorComboBox.TabIndex = 40
        Me.SeasonNumberBehaviorComboBox.UseSelectable = True
        '
        'GroupBox12
        '
        Me.GroupBox12.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox12.Controls.Add(Me.EpisodeTitleCheckBox)
        Me.GroupBox12.Controls.Add(Me.AudioLanguageCheckBox)
        Me.GroupBox12.Controls.Add(Me.EpisodeNumberCheckBox)
        Me.GroupBox12.Controls.Add(Me.KodiNamingCheckBox)
        Me.GroupBox12.Controls.Add(Me.SeasonNumberCheckBox)
        Me.GroupBox12.Controls.Add(Me.SeriesNameCheckBox)
        Me.GroupBox12.Controls.Add(Me.FilenameTemplatePreview)
        Me.GroupBox12.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox12.ForeColor = System.Drawing.Color.Black
        Me.GroupBox12.Location = New System.Drawing.Point(5, 11)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(490, 155)
        Me.GroupBox12.TabIndex = 21
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "Filename"
        '
        'EpisodeTitleCheckBox
        '
        Me.EpisodeTitleCheckBox.AutoSize = True
        Me.EpisodeTitleCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.EpisodeTitleCheckBox.ForeColor = System.Drawing.Color.Black
        Me.EpisodeTitleCheckBox.Location = New System.Drawing.Point(153, 124)
        Me.EpisodeTitleCheckBox.Name = "EpisodeTitleCheckBox"
        Me.EpisodeTitleCheckBox.Size = New System.Drawing.Size(101, 19)
        Me.EpisodeTitleCheckBox.TabIndex = 31
        Me.EpisodeTitleCheckBox.Text = "Episode Title"
        Me.EpisodeTitleCheckBox.UseSelectable = True
        '
        'AudioLanguageCheckBox
        '
        Me.AudioLanguageCheckBox.AutoSize = True
        Me.AudioLanguageCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.AudioLanguageCheckBox.ForeColor = System.Drawing.Color.Black
        Me.AudioLanguageCheckBox.Location = New System.Drawing.Point(320, 86)
        Me.AudioLanguageCheckBox.Name = "AudioLanguageCheckBox"
        Me.AudioLanguageCheckBox.Size = New System.Drawing.Size(125, 19)
        Me.AudioLanguageCheckBox.TabIndex = 29
        Me.AudioLanguageCheckBox.Text = "Audio Language"
        Me.AudioLanguageCheckBox.UseSelectable = True
        '
        'EpisodeNumberCheckBox
        '
        Me.EpisodeNumberCheckBox.AutoSize = True
        Me.EpisodeNumberCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.EpisodeNumberCheckBox.ForeColor = System.Drawing.Color.Black
        Me.EpisodeNumberCheckBox.Location = New System.Drawing.Point(153, 86)
        Me.EpisodeNumberCheckBox.Name = "EpisodeNumberCheckBox"
        Me.EpisodeNumberCheckBox.Size = New System.Drawing.Size(126, 19)
        Me.EpisodeNumberCheckBox.TabIndex = 28
        Me.EpisodeNumberCheckBox.Text = "Episode Number"
        Me.EpisodeNumberCheckBox.UseSelectable = True
        '
        'KodiNamingCheckBox
        '
        Me.KodiNamingCheckBox.AutoSize = True
        Me.KodiNamingCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.KodiNamingCheckBox.ForeColor = System.Drawing.Color.Black
        Me.KodiNamingCheckBox.Location = New System.Drawing.Point(320, 124)
        Me.KodiNamingCheckBox.Name = "KodiNamingCheckBox"
        Me.KodiNamingCheckBox.Size = New System.Drawing.Size(102, 19)
        Me.KodiNamingCheckBox.TabIndex = 32
        Me.KodiNamingCheckBox.Text = "Kodi naming"
        Me.KodiNamingCheckBox.UseSelectable = True
        '
        'SeasonNumberCheckBox
        '
        Me.SeasonNumberCheckBox.AutoSize = True
        Me.SeasonNumberCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.SeasonNumberCheckBox.ForeColor = System.Drawing.Color.Black
        Me.SeasonNumberCheckBox.Location = New System.Drawing.Point(17, 124)
        Me.SeasonNumberCheckBox.Name = "SeasonNumberCheckBox"
        Me.SeasonNumberCheckBox.Size = New System.Drawing.Size(122, 19)
        Me.SeasonNumberCheckBox.TabIndex = 30
        Me.SeasonNumberCheckBox.Text = "Season Number"
        Me.SeasonNumberCheckBox.UseSelectable = True
        '
        'SeriesNameCheckBox
        '
        Me.SeriesNameCheckBox.AutoSize = True
        Me.SeriesNameCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.SeriesNameCheckBox.ForeColor = System.Drawing.Color.Black
        Me.SeriesNameCheckBox.Location = New System.Drawing.Point(17, 86)
        Me.SeriesNameCheckBox.Name = "SeriesNameCheckBox"
        Me.SeriesNameCheckBox.Size = New System.Drawing.Size(100, 19)
        Me.SeriesNameCheckBox.TabIndex = 27
        Me.SeriesNameCheckBox.Text = "Series Name"
        Me.SeriesNameCheckBox.UseSelectable = True
        '
        'FilenameTemplatePreview
        '
        '
        '
        '
        Me.FilenameTemplatePreview.CustomButton.Image = Nothing
        Me.FilenameTemplatePreview.CustomButton.Location = New System.Drawing.Point(439, 1)
        Me.FilenameTemplatePreview.CustomButton.Name = ""
        Me.FilenameTemplatePreview.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.FilenameTemplatePreview.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.FilenameTemplatePreview.CustomButton.TabIndex = 1
        Me.FilenameTemplatePreview.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.FilenameTemplatePreview.CustomButton.UseSelectable = True
        Me.FilenameTemplatePreview.CustomButton.Visible = False
        Me.FilenameTemplatePreview.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.FilenameTemplatePreview.Lines = New String(-1) {}
        Me.FilenameTemplatePreview.Location = New System.Drawing.Point(6, 35)
        Me.FilenameTemplatePreview.MaxLength = 32767
        Me.FilenameTemplatePreview.Name = "FilenameTemplatePreview"
        Me.FilenameTemplatePreview.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.FilenameTemplatePreview.ReadOnly = True
        Me.FilenameTemplatePreview.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.FilenameTemplatePreview.SelectedText = ""
        Me.FilenameTemplatePreview.SelectionLength = 0
        Me.FilenameTemplatePreview.SelectionStart = 0
        Me.FilenameTemplatePreview.ShortcutsEnabled = True
        Me.FilenameTemplatePreview.Size = New System.Drawing.Size(467, 29)
        Me.FilenameTemplatePreview.TabIndex = 21
        Me.FilenameTemplatePreview.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.FilenameTemplatePreview.UseSelectable = True
        Me.FilenameTemplatePreview.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.FilenameTemplatePreview.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'MetroTabPage1
        '
        Me.MetroTabPage1.Controls.Add(Me.GroupBox20)
        Me.MetroTabPage1.Controls.Add(Me.GroupBox19)
        Me.MetroTabPage1.Controls.Add(Me.GB_SubLanguage)
        Me.MetroTabPage1.Controls.Add(Me.SoftSubs)
        Me.MetroTabPage1.HorizontalScrollbarBarColor = True
        Me.MetroTabPage1.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroTabPage1.HorizontalScrollbarSize = 10
        Me.MetroTabPage1.Location = New System.Drawing.Point(4, 44)
        Me.MetroTabPage1.Name = "MetroTabPage1"
        Me.MetroTabPage1.Size = New System.Drawing.Size(501, 519)
        Me.MetroTabPage1.TabIndex = 7
        Me.MetroTabPage1.Text = "Crunchyroll"
        Me.MetroTabPage1.VerticalScrollbarBarColor = True
        Me.MetroTabPage1.VerticalScrollbarHighlightOnWheel = False
        Me.MetroTabPage1.VerticalScrollbarSize = 10
        '
        'GroupBox20
        '
        Me.GroupBox20.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox20.Controls.Add(Me.ChB_Chapters)
        Me.GroupBox20.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox20.ForeColor = System.Drawing.Color.Black
        Me.GroupBox20.Location = New System.Drawing.Point(5, 368)
        Me.GroupBox20.Name = "GroupBox20"
        Me.GroupBox20.Size = New System.Drawing.Size(490, 65)
        Me.GroupBox20.TabIndex = 34
        Me.GroupBox20.TabStop = False
        Me.GroupBox20.Text = "Chapters"
        '
        'ChB_Chapters
        '
        Me.ChB_Chapters.AutoSize = True
        Me.ChB_Chapters.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.ChB_Chapters.Location = New System.Drawing.Point(158, 23)
        Me.ChB_Chapters.Name = "ChB_Chapters"
        Me.ChB_Chapters.Size = New System.Drawing.Size(145, 19)
        Me.ChB_Chapters.TabIndex = 5
        Me.ChB_Chapters.Text = "enable CR Chapters"
        Me.ChB_Chapters.UseSelectable = True
        '
        'GroupBox19
        '
        Me.GroupBox19.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox19.Controls.Add(Me.CrunchyrollAudioLanguageComboBox)
        Me.GroupBox19.Controls.Add(Me.CrunchyrollAcceptHardsubsCheckBox)
        Me.GroupBox19.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox19.ForeColor = System.Drawing.Color.Black
        Me.GroupBox19.Location = New System.Drawing.Point(5, 15)
        Me.GroupBox19.Name = "GroupBox19"
        Me.GroupBox19.Size = New System.Drawing.Size(490, 100)
        Me.GroupBox19.TabIndex = 33
        Me.GroupBox19.TabStop = False
        Me.GroupBox19.Text = "Dubbed"
        '
        'CrunchyrollAudioLanguageComboBox
        '
        Me.CrunchyrollAudioLanguageComboBox.DropDownHeight = 275
        Me.CrunchyrollAudioLanguageComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CrunchyrollAudioLanguageComboBox.FormattingEnabled = True
        Me.CrunchyrollAudioLanguageComboBox.IntegralHeight = False
        Me.CrunchyrollAudioLanguageComboBox.ItemHeight = 23
        Me.CrunchyrollAudioLanguageComboBox.Location = New System.Drawing.Point(85, 55)
        Me.CrunchyrollAudioLanguageComboBox.Name = "CrunchyrollAudioLanguageComboBox"
        Me.CrunchyrollAudioLanguageComboBox.Size = New System.Drawing.Size(320, 29)
        Me.CrunchyrollAudioLanguageComboBox.TabIndex = 21
        Me.CrunchyrollAudioLanguageComboBox.UseSelectable = True
        '
        'CrunchyrollAcceptHardsubsCheckBox
        '
        Me.CrunchyrollAcceptHardsubsCheckBox.BackColor = System.Drawing.Color.Transparent
        Me.CrunchyrollAcceptHardsubsCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.CrunchyrollAcceptHardsubsCheckBox.Location = New System.Drawing.Point(85, 21)
        Me.CrunchyrollAcceptHardsubsCheckBox.Name = "CrunchyrollAcceptHardsubsCheckBox"
        Me.CrunchyrollAcceptHardsubsCheckBox.Size = New System.Drawing.Size(320, 28)
        Me.CrunchyrollAcceptHardsubsCheckBox.TabIndex = 5
        Me.CrunchyrollAcceptHardsubsCheckBox.Text = "Accept hardsubs for dubbed shows"
        Me.CrunchyrollAcceptHardsubsCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CrunchyrollAcceptHardsubsCheckBox.UseCustomBackColor = True
        Me.CrunchyrollAcceptHardsubsCheckBox.UseSelectable = True
        '
        'TabPage6
        '
        Me.TabPage6.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.TabPage6.Controls.Add(Me.GroupBox15)
        Me.TabPage6.Controls.Add(Me.GroupBox10)
        Me.TabPage6.Controls.Add(Me.GroupBox7)
        Me.TabPage6.Controls.Add(Me.GroupBox9)
        Me.TabPage6.HorizontalScrollbarBarColor = True
        Me.TabPage6.HorizontalScrollbarHighlightOnWheel = False
        Me.TabPage6.HorizontalScrollbarSize = 10
        Me.TabPage6.Location = New System.Drawing.Point(4, 35)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(501, 528)
        Me.TabPage6.TabIndex = 4
        Me.TabPage6.Text = " Funimation"
        Me.TabPage6.VerticalScrollbarBarColor = True
        Me.TabPage6.VerticalScrollbarHighlightOnWheel = False
        Me.TabPage6.VerticalScrollbarSize = 10
        Me.TabPage6.Visible = False
        '
        'GroupBox15
        '
        Me.GroupBox15.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox15.Controls.Add(Me.Bitrate_Funi)
        Me.GroupBox15.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox15.ForeColor = System.Drawing.Color.Black
        Me.GroupBox15.Location = New System.Drawing.Point(0, 365)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(490, 69)
        Me.GroupBox15.TabIndex = 41
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "Bitrate"
        '
        'Bitrate_Funi
        '
        Me.Bitrate_Funi.DropDownHeight = 250
        Me.Bitrate_Funi.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bitrate_Funi.FormattingEnabled = True
        Me.Bitrate_Funi.IntegralHeight = False
        Me.Bitrate_Funi.ItemHeight = 23
        Me.Bitrate_Funi.Items.AddRange(New Object() {"prefer high bitrate", "prefer low bitrate"})
        Me.Bitrate_Funi.Location = New System.Drawing.Point(79, 24)
        Me.Bitrate_Funi.Name = "Bitrate_Funi"
        Me.Bitrate_Funi.Size = New System.Drawing.Size(326, 29)
        Me.Bitrate_Funi.Sorted = True
        Me.Bitrate_Funi.TabIndex = 32
        Me.Bitrate_Funi.UseSelectable = True
        '
        'GroupBox10
        '
        Me.GroupBox10.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox10.Controls.Add(Me.Fun_Dub_Over)
        Me.GroupBox10.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox10.ForeColor = System.Drawing.Color.Black
        Me.GroupBox10.Location = New System.Drawing.Point(0, 6)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(490, 69)
        Me.GroupBox10.TabIndex = 80
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Funimation Dub"
        '
        'Fun_Dub_Over
        '
        Me.Fun_Dub_Over.DropDownHeight = 250
        Me.Fun_Dub_Over.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Fun_Dub_Over.FormattingEnabled = True
        Me.Fun_Dub_Over.IntegralHeight = False
        Me.Fun_Dub_Over.ItemHeight = 23
        Me.Fun_Dub_Over.Items.AddRange(New Object() {"Disabled", "english", "japanese", "portuguese(Brazil)", "spanish(Mexico)"})
        Me.Fun_Dub_Over.Location = New System.Drawing.Point(79, 30)
        Me.Fun_Dub_Over.Name = "Fun_Dub_Over"
        Me.Fun_Dub_Over.Size = New System.Drawing.Size(326, 29)
        Me.Fun_Dub_Over.Sorted = True
        Me.Fun_Dub_Over.TabIndex = 40
        Me.Fun_Dub_Over.UseSelectable = True
        '
        'GroupBox7
        '
        Me.GroupBox7.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox7.Controls.Add(Me.CB_Fun_HardSubs)
        Me.GroupBox7.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox7.ForeColor = System.Drawing.Color.Black
        Me.GroupBox7.Location = New System.Drawing.Point(0, 440)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(490, 69)
        Me.GroupBox7.TabIndex = 40
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Hard Subtitle (post-processed)"
        '
        'CB_Fun_HardSubs
        '
        Me.CB_Fun_HardSubs.DropDownHeight = 250
        Me.CB_Fun_HardSubs.Enabled = False
        Me.CB_Fun_HardSubs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_Fun_HardSubs.FormattingEnabled = True
        Me.CB_Fun_HardSubs.IntegralHeight = False
        Me.CB_Fun_HardSubs.ItemHeight = 23
        Me.CB_Fun_HardSubs.Items.AddRange(New Object() {"Disabled", "English", "Español (LA)", "Português (Brasil)"})
        Me.CB_Fun_HardSubs.Location = New System.Drawing.Point(79, 30)
        Me.CB_Fun_HardSubs.Name = "CB_Fun_HardSubs"
        Me.CB_Fun_HardSubs.Size = New System.Drawing.Size(326, 29)
        Me.CB_Fun_HardSubs.Sorted = True
        Me.CB_Fun_HardSubs.TabIndex = 32
        Me.CB_Fun_HardSubs.UseSelectable = True
        '
        'GroupBox9
        '
        Me.GroupBox9.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox9.Controls.Add(Me.GroupBox13)
        Me.GroupBox9.Controls.Add(Me.GroupBox11)
        Me.GroupBox9.Controls.Add(Me.GroupBox8)
        Me.GroupBox9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox9.Location = New System.Drawing.Point(0, 80)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(490, 275)
        Me.GroupBox9.TabIndex = 50
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Soft-Subtitle"
        '
        'GroupBox13
        '
        Me.GroupBox13.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox13.Controls.Add(Me.FunSubDef)
        Me.GroupBox13.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox13.ForeColor = System.Drawing.Color.Black
        Me.GroupBox13.Location = New System.Drawing.Point(10, 180)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(474, 82)
        Me.GroupBox13.TabIndex = 70
        Me.GroupBox13.TabStop = False
        Me.GroupBox13.Text = "Default Subtitle"
        '
        'FunSubDef
        '
        Me.FunSubDef.DropDownHeight = 250
        Me.FunSubDef.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FunSubDef.FormattingEnabled = True
        Me.FunSubDef.IntegralHeight = False
        Me.FunSubDef.ItemHeight = 23
        Me.FunSubDef.Items.AddRange(New Object() {"[Disabled]"})
        Me.FunSubDef.Location = New System.Drawing.Point(69, 30)
        Me.FunSubDef.Name = "FunSubDef"
        Me.FunSubDef.Size = New System.Drawing.Size(326, 29)
        Me.FunSubDef.Sorted = True
        Me.FunSubDef.TabIndex = 39
        Me.FunSubDef.UseSelectable = True
        '
        'GroupBox11
        '
        Me.GroupBox11.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox11.Controls.Add(Me.CB_srt)
        Me.GroupBox11.Controls.Add(Me.CB_vtt)
        Me.GroupBox11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox11.Location = New System.Drawing.Point(10, 100)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(474, 75)
        Me.GroupBox11.TabIndex = 60
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "Format"
        '
        'CB_srt
        '
        Me.CB_srt.AutoSize = True
        Me.CB_srt.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.CB_srt.ForeColor = System.Drawing.Color.Black
        Me.CB_srt.Location = New System.Drawing.Point(139, 35)
        Me.CB_srt.Name = "CB_srt"
        Me.CB_srt.Size = New System.Drawing.Size(41, 19)
        Me.CB_srt.TabIndex = 36
        Me.CB_srt.Text = "srt"
        Me.CB_srt.UseSelectable = True
        '
        'CB_vtt
        '
        Me.CB_vtt.AutoSize = True
        Me.CB_vtt.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.CB_vtt.ForeColor = System.Drawing.Color.Black
        Me.CB_vtt.Location = New System.Drawing.Point(261, 35)
        Me.CB_vtt.Name = "CB_vtt"
        Me.CB_vtt.Size = New System.Drawing.Size(42, 19)
        Me.CB_vtt.TabIndex = 37
        Me.CB_vtt.Text = "vtt"
        Me.CB_vtt.UseSelectable = True
        '
        'GroupBox8
        '
        Me.GroupBox8.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox8.Controls.Add(Me.CB_fun_eng)
        Me.GroupBox8.Controls.Add(Me.CB_fun_es)
        Me.GroupBox8.Controls.Add(Me.CB_fun_ptbr)
        Me.GroupBox8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox8.Location = New System.Drawing.Point(10, 21)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(474, 75)
        Me.GroupBox8.TabIndex = 61
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Language"
        '
        'CB_fun_eng
        '
        Me.CB_fun_eng.AutoSize = True
        Me.CB_fun_eng.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.CB_fun_eng.ForeColor = System.Drawing.Color.Black
        Me.CB_fun_eng.Location = New System.Drawing.Point(34, 35)
        Me.CB_fun_eng.Name = "CB_fun_eng"
        Me.CB_fun_eng.Size = New System.Drawing.Size(68, 19)
        Me.CB_fun_eng.TabIndex = 33
        Me.CB_fun_eng.Text = "English"
        Me.CB_fun_eng.UseSelectable = True
        '
        'CB_fun_es
        '
        Me.CB_fun_es.AutoSize = True
        Me.CB_fun_es.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.CB_fun_es.ForeColor = System.Drawing.Color.Black
        Me.CB_fun_es.Location = New System.Drawing.Point(148, 35)
        Me.CB_fun_es.Name = "CB_fun_es"
        Me.CB_fun_es.Size = New System.Drawing.Size(100, 19)
        Me.CB_fun_es.TabIndex = 34
        Me.CB_fun_es.Text = "Español (LA)"
        Me.CB_fun_es.UseSelectable = True
        '
        'CB_fun_ptbr
        '
        Me.CB_fun_ptbr.AutoSize = True
        Me.CB_fun_ptbr.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.CB_fun_ptbr.ForeColor = System.Drawing.Color.Black
        Me.CB_fun_ptbr.Location = New System.Drawing.Point(270, 35)
        Me.CB_fun_ptbr.Name = "CB_fun_ptbr"
        Me.CB_fun_ptbr.Size = New System.Drawing.Size(131, 19)
        Me.CB_fun_ptbr.TabIndex = 35
        Me.CB_fun_ptbr.Text = "Português (Brasil)"
        Me.CB_fun_ptbr.UseSelectable = True
        '
        'TabPage7
        '
        Me.TabPage7.BackColor = System.Drawing.Color.Transparent
        Me.TabPage7.Controls.Add(Me.LastVersion)
        Me.TabPage7.Controls.Add(Me.Label8)
        Me.TabPage7.Controls.Add(Me.MetroFrameworkLabel)
        Me.TabPage7.Controls.Add(Me.WebviewLabel)
        Me.TabPage7.Controls.Add(Me.FfmpegLabel)
        Me.TabPage7.Controls.Add(Me.PictureBox7)
        Me.TabPage7.Controls.Add(Me.Label4)
        Me.TabPage7.Controls.Add(Me.CurrentVersionLabel)
        Me.TabPage7.Controls.Add(Me.Label5)
        Me.TabPage7.Location = New System.Drawing.Point(4, 35)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(501, 528)
        Me.TabPage7.TabIndex = 5
        Me.TabPage7.Text = " About  "
        '
        'LastVersion
        '
        Me.LastVersion.BackColor = System.Drawing.Color.Transparent
        Me.LastVersion.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.LastVersion.ForeColor = System.Drawing.Color.Black
        Me.LastVersion.Location = New System.Drawing.Point(1, 205)
        Me.LastVersion.Name = "LastVersion"
        Me.LastVersion.Size = New System.Drawing.Size(491, 45)
        Me.LastVersion.TabIndex = 48
        Me.LastVersion.Text = "last release v3.7.2"
        Me.LastVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(29, 450)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(100, 30)
        Me.Label8.TabIndex = 44
        Me.Label8.Text = "libraries: "
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MetroFrameworkLabel
        '
        Me.MetroFrameworkLabel.BackColor = System.Drawing.Color.Transparent
        Me.MetroFrameworkLabel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MetroFrameworkLabel.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.MetroFrameworkLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.MetroFrameworkLabel.Location = New System.Drawing.Point(127, 450)
        Me.MetroFrameworkLabel.Name = "MetroFrameworkLabel"
        Me.MetroFrameworkLabel.Size = New System.Drawing.Size(144, 30)
        Me.MetroFrameworkLabel.TabIndex = 47
        Me.MetroFrameworkLabel.Text = "MetroFramework"
        Me.MetroFrameworkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'WebviewLabel
        '
        Me.WebviewLabel.BackColor = System.Drawing.Color.Transparent
        Me.WebviewLabel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.WebviewLabel.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.WebviewLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.WebviewLabel.Location = New System.Drawing.Point(277, 450)
        Me.WebviewLabel.Name = "WebviewLabel"
        Me.WebviewLabel.Size = New System.Drawing.Size(100, 30)
        Me.WebviewLabel.TabIndex = 46
        Me.WebviewLabel.Text = "WebView2"
        Me.WebviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FfmpegLabel
        '
        Me.FfmpegLabel.BackColor = System.Drawing.Color.Transparent
        Me.FfmpegLabel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.FfmpegLabel.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.FfmpegLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.FfmpegLabel.Location = New System.Drawing.Point(383, 450)
        Me.FfmpegLabel.Name = "FfmpegLabel"
        Me.FfmpegLabel.Size = New System.Drawing.Size(80, 30)
        Me.FfmpegLabel.TabIndex = 45
        Me.FfmpegLabel.Text = "ffmpeg"
        Me.FfmpegLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox7
        '
        Me.PictureBox7.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.about_icon
        Me.PictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox7.Location = New System.Drawing.Point(0, 50)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(493, 137)
        Me.PictureBox7.TabIndex = 43
        Me.PictureBox7.TabStop = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(1, -5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(491, 45)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "Crunchyroll Downloader"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CurrentVersionLabel
        '
        Me.CurrentVersionLabel.BackColor = System.Drawing.Color.Transparent
        Me.CurrentVersionLabel.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.CurrentVersionLabel.ForeColor = System.Drawing.Color.Black
        Me.CurrentVersionLabel.Location = New System.Drawing.Point(1, 275)
        Me.CurrentVersionLabel.Name = "CurrentVersionLabel"
        Me.CurrentVersionLabel.Size = New System.Drawing.Size(491, 45)
        Me.CurrentVersionLabel.TabIndex = 37
        Me.CurrentVersionLabel.Text = "You have:"
        Me.CurrentVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(1, 350)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(491, 45)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Created by hama3254"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BackgroundWorker1
        '
        '
        'Btn_Save
        '
        Me.Btn_Save.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Btn_Save.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Save.FlatAppearance.BorderSize = 0
        Me.Btn_Save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Save.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.crdSettings_Button_SafeExit
        Me.Btn_Save.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Btn_Save.Location = New System.Drawing.Point(96, 645)
        Me.Btn_Save.Name = "Btn_Save"
        Me.Btn_Save.Size = New System.Drawing.Size(355, 30)
        Me.Btn_Save.TabIndex = 9
        Me.Btn_Save.UseVisualStyleBackColor = False
        '
        'Einstellungen
        '
        Me.ApplyImageInvert = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle
        Me.ClientSize = New System.Drawing.Size(554, 698)
        Me.Controls.Add(Me.Btn_Save)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.pictureBox1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Einstellungen"
        Me.Style = MetroFramework.MetroColorStyle.Orange
        Me.Text = " Settings"
        Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SoftSubs.ResumeLayout(False)
        Me.SoftSubs.PerformLayout()
        Me.GB_SubLanguage.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox18.ResumeLayout(False)
        Me.GroupBox18.PerformLayout()
        Me.GroupBox16.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.FfmpegCommandGroupBox.ResumeLayout(False)
        Me.FfmpegCommandGroupBox.PerformLayout()
        CType(Me.BitrateNumericInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GB_Resolution.ResumeLayout(False)
        Me.GB_Resolution.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox21.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.ErrorLimitInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.CustomServerPortInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.DL_Count_simultaneous.ResumeLayout(False)
        CType(Me.SimultaneousDownloadsInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.MetroTabPage2.ResumeLayout(False)
        Me.GroupBox17.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GB_Filename_Pre.ResumeLayout(False)
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox12.PerformLayout()
        Me.MetroTabPage1.ResumeLayout(False)
        Me.GroupBox20.ResumeLayout(False)
        Me.GroupBox20.PerformLayout()
        Me.GroupBox19.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox13.ResumeLayout(False)
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.TabPage7.ResumeLayout(False)
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip1 As ToolTip
    Private WithEvents pictureBox1 As PictureBox
    Friend WithEvents ToolTip2 As ToolTip
    Friend WithEvents FfmpegCommandGroupBox As GroupBox
    Friend WithEvents GB_Resolution As GroupBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DL_Count_simultaneous As GroupBox
    Friend WithEvents SimultaneousDownloadsInput As NumericUpDown
    Friend WithEvents SoftSubs As GroupBox
    Friend WithEvents GB_SubLanguage As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents ErrorLimitInput As NumericUpDown
    Private WithEvents TabPage2 As MetroFramework.Controls.MetroTabPage
    Private WithEvents TabPage1 As MetroFramework.Controls.MetroTabPage
    Private WithEvents TabControl1 As MetroFramework.Controls.MetroTabControl
    Private WithEvents TabPage6 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents TabPage7 As TabPage
    Friend WithEvents PictureBox7 As PictureBox
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents GroupBox9 As GroupBox
    Friend WithEvents GroupBox10 As GroupBox
    Friend WithEvents GroupBox11 As GroupBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label2 As MetroFramework.Controls.MetroLabel
    Public WithEvents Label4 As MetroFramework.Controls.MetroLabel
    Public WithEvents CurrentVersionLabel As MetroFramework.Controls.MetroLabel
    Public WithEvents Label5 As MetroFramework.Controls.MetroLabel
    Friend WithEvents CheckBox2 As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents CB_fun_ptbr As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents CB_fun_es As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents CB_fun_eng As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents CB_srt As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents CB_vtt As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents AAuto As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents A480p As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents A360p As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents A720p As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents A1080p As MetroFramework.Controls.MetroRadioButton
    Public WithEvents Label8 As MetroFramework.Controls.MetroLabel
    Public WithEvents MetroFrameworkLabel As MetroFramework.Controls.MetroLabel
    Public WithEvents WebviewLabel As MetroFramework.Controls.MetroLabel
    Public WithEvents FfmpegLabel As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel1 As MetroFramework.Controls.MetroLabel
    Friend WithEvents DarkMode As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents DefaultWebsiteTextBox As MetroFramework.Controls.MetroTextBox
    Friend WithEvents CrunchyrollHardsubComboBox As MetroFramework.Controls.MetroComboBox
    Friend WithEvents CB_Fun_HardSubs As MetroFramework.Controls.MetroComboBox
    Friend WithEvents Fun_Dub_Over As MetroFramework.Controls.MetroComboBox
    Public WithEvents LastVersion As MetroFramework.Controls.MetroLabel
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ServerPortInput As MetroFramework.Controls.MetroComboBox
    Friend WithEvents GroupBox13 As GroupBox
    Friend WithEvents FunSubDef As MetroFramework.Controls.MetroComboBox
    Friend WithEvents CB_Format As MetroFramework.Controls.MetroComboBox
    Friend WithEvents MetroTabPage1 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents Btn_Save As Button
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents GroupBox15 As GroupBox
    Friend WithEvents Bitrate_Funi As MetroFramework.Controls.MetroComboBox
    Friend WithEvents GroupBox16 As GroupBox
    Friend WithEvents DownloadModeDropdown As MetroFramework.Controls.MetroComboBox
    Friend WithEvents MetroTabPage2 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents GB_Filename_Pre As GroupBox
    Friend WithEvents GroupBox12 As GroupBox
    Friend WithEvents LeadingZerosComboBox As MetroFramework.Controls.MetroComboBox
    Friend WithEvents GroupBox17 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents SubLanguageNamingComboBox As MetroFramework.Controls.MetroComboBox
    Friend WithEvents GroupBox18 As GroupBox
    Friend WithEvents UseQueueCheckbox As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents MetroLabel3 As MetroFramework.Controls.MetroLabel
    Friend WithEvents TemporaryFolderTextBox As MetroFramework.Controls.MetroTextBox
    Friend WithEvents SeasonNumberBehaviorComboBox As MetroFramework.Controls.MetroComboBox
    Friend WithEvents GroupBox19 As GroupBox
    Friend WithEvents CrunchyrollAcceptHardsubsCheckBox As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents CB_Merge As MetroFramework.Controls.MetroComboBox
    Friend WithEvents Chb_Ign_tls As MetroFramework.Controls.MetroCheckBox
    Public WithEvents EpisodeTitleCheckBox As MetroFramework.Controls.MetroCheckBox
    Public WithEvents AudioLanguageCheckBox As MetroFramework.Controls.MetroCheckBox
    Public WithEvents EpisodeNumberCheckBox As MetroFramework.Controls.MetroCheckBox
    Public WithEvents KodiNamingCheckBox As MetroFramework.Controls.MetroCheckBox
    Public WithEvents SeasonNumberCheckBox As MetroFramework.Controls.MetroCheckBox
    Public WithEvents SeriesNameCheckBox As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents FilenameTemplatePreview As MetroFramework.Controls.MetroTextBox
    Friend WithEvents EpisodePrefixTextBox As MetroFramework.Controls.MetroTextBox
    Friend WithEvents SeasonPrefixTextBox As MetroFramework.Controls.MetroTextBox
    Friend WithEvents CrunchyrollAudioLanguageComboBox As MetroFramework.Controls.MetroComboBox
    Friend WithEvents GroupBox21 As GroupBox
    Friend WithEvents CB_HideSF As MetroFramework.Controls.MetroComboBox
    Friend WithEvents GroupBox20 As GroupBox
    Friend WithEvents ChB_Chapters As MetroFramework.Controls.MetroCheckBox
    Public WithEvents CR_SoftSubDefault As MetroFramework.Controls.MetroComboBox
    Friend WithEvents CustomServerPortInput As NumericUpDown
    Friend WithEvents ServerPortLabel As MetroFramework.Controls.MetroLabel
    Friend WithEvents FfmpegCommandPreviewTextBox As MetroFramework.Controls.MetroTextBox
    Friend WithEvents MetroLabel2 As MetroFramework.Controls.MetroLabel
    Friend WithEvents BitrateNumericInput As NumericUpDown
    Friend WithEvents FfmpegPresetComboBox As MetroFramework.Controls.MetroComboBox
    Friend WithEvents MetroLabel4 As MetroFramework.Controls.MetroLabel
    Friend WithEvents VideoCodecComboBox As MetroFramework.Controls.MetroComboBox
    Friend WithEvents MetroLabel6 As MetroFramework.Controls.MetroLabel
    Friend WithEvents VideoEncoderComboBox As MetroFramework.Controls.MetroComboBox
    Friend WithEvents MetroLabel5 As MetroFramework.Controls.MetroLabel
    Friend WithEvents TargetBitrateCheckBox As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents FfmpegCopyCheckBox As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents IncludeLanguageNameCheckBox As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents CrunchyrollSoftSubsCheckedListBox As CheckedListBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
End Class
