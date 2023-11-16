Namespace ui
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class SettingsDialog
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingsDialog))
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.KodiNamingTemplateButton = New MetroFramework.Controls.MetroButton()
            Me.AudioLanguageTemplateButton = New MetroFramework.Controls.MetroButton()
            Me.EpisodeTitleTemplateButton = New MetroFramework.Controls.MetroButton()
            Me.EpisodeNumberTemplateButton = New MetroFramework.Controls.MetroButton()
            Me.SeasonNumberTemplateButton = New MetroFramework.Controls.MetroButton()
            Me.SeriesNameTemplateButton = New MetroFramework.Controls.MetroButton()
            Me.MetroLabel9 = New MetroFramework.Controls.MetroLabel()
            Me.FilenameTemplateInput = New MetroFramework.Controls.MetroTextBox()
            Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
            Me.CR_SoftSubDefault = New MetroFramework.Controls.MetroComboBox()
            Me.CrunchyrollSoftSubsGroupBox = New System.Windows.Forms.GroupBox()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.CrunchyrollSoftSubsCheckedListBox = New System.Windows.Forms.CheckedListBox()
            Me.CrunchyrollHardsubGroupBox = New System.Windows.Forms.GroupBox()
            Me.CrunchyrollHardsubComboBox = New MetroFramework.Controls.MetroComboBox()
            Me.BrowserSettingsGroupBox = New System.Windows.Forms.GroupBox()
            Me.Label1 = New MetroFramework.Controls.MetroLabel()
            Me.DefaultWebsiteTextBox = New MetroFramework.Controls.MetroTextBox()
            Me.DownloadCountGroupBox = New System.Windows.Forms.GroupBox()
            Me.SimultaneousDownloadsInput = New System.Windows.Forms.NumericUpDown()
            Me.TabControl = New MetroFramework.Controls.MetroTabControl()
            Me.MainTabPage = New MetroFramework.Controls.MetroTabPage()
            Me.SubfolderGroupBox = New System.Windows.Forms.GroupBox()
            Me.HideSubfoldersComboBox = New MetroFramework.Controls.MetroComboBox()
            Me.ErrorHandlingGroupBox = New System.Windows.Forms.GroupBox()
            Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
            Me.CheckBox2 = New MetroFramework.Controls.MetroCheckBox()
            Me.Label2 = New MetroFramework.Controls.MetroLabel()
            Me.ErrorLimitInput = New System.Windows.Forms.NumericUpDown()
            Me.ServerGroupBox = New System.Windows.Forms.GroupBox()
            Me.CustomServerPortInput = New System.Windows.Forms.NumericUpDown()
            Me.ServerPortLabel = New MetroFramework.Controls.MetroLabel()
            Me.IgnoreTlsCheckBox = New MetroFramework.Controls.MetroCheckBox()
            Me.ServerPortInput = New MetroFramework.Controls.MetroComboBox()
            Me.DarkModeCheckBox = New MetroFramework.Controls.MetroCheckBox()
            Me.OutputTabPage = New MetroFramework.Controls.MetroTabPage()
            Me.QualityGroupBox = New System.Windows.Forms.GroupBox()
            Me.LowerResolutionRadioButton = New MetroFramework.Controls.MetroRadioButton()
            Me.HigherResolutionRadioButton = New MetroFramework.Controls.MetroRadioButton()
            Me.BitratePanel = New System.Windows.Forms.Panel()
            Me.MetroLabel7 = New MetroFramework.Controls.MetroLabel()
            Me.HigherBitrateRadioButton = New MetroFramework.Controls.MetroRadioButton()
            Me.LowerBitrateRadioButton = New MetroFramework.Controls.MetroRadioButton()
            Me.ResolutionComboBox = New MetroFramework.Controls.MetroComboBox()
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
            Me.GroupBox4 = New System.Windows.Forms.GroupBox()
            Me.SubtitleFormatComboBox = New MetroFramework.Controls.MetroComboBox()
            Me.VideoFormatComboBox = New MetroFramework.Controls.MetroComboBox()
            Me.GroupBox16 = New System.Windows.Forms.GroupBox()
            Me.MetroLabel3 = New MetroFramework.Controls.MetroLabel()
            Me.TemporaryFolderTextBox = New MetroFramework.Controls.MetroTextBox()
            Me.DownloadModeDropdown = New MetroFramework.Controls.MetroComboBox()
            Me.NamingTabPage = New MetroFramework.Controls.MetroTabPage()
            Me.GroupBox3 = New System.Windows.Forms.GroupBox()
            Me.IncludeLanguageNameCheckBox = New MetroFramework.Controls.MetroCheckBox()
            Me.SubLanguageNamingComboBox = New MetroFramework.Controls.MetroComboBox()
            Me.FilenameExtrasGroupBox = New System.Windows.Forms.GroupBox()
            Me.LeadingZerosComboBox = New MetroFramework.Controls.MetroComboBox()
            Me.SeasonNumberBehaviorComboBox = New MetroFramework.Controls.MetroComboBox()
            Me.GroupBox12 = New System.Windows.Forms.GroupBox()
            Me.MetroLabel8 = New MetroFramework.Controls.MetroLabel()
            Me.FilenamePreviewTextBox = New MetroFramework.Controls.MetroTextBox()
            Me.CrunchyrollTabPage = New MetroFramework.Controls.MetroTabPage()
            Me.GroupBox20 = New System.Windows.Forms.GroupBox()
            Me.CrunchyrollChaptersCheckBox = New MetroFramework.Controls.MetroCheckBox()
            Me.GroupBox19 = New System.Windows.Forms.GroupBox()
            Me.CrunchyrollAudioLanguageComboBox = New MetroFramework.Controls.MetroComboBox()
            Me.CrunchyrollAcceptHardsubsCheckBox = New MetroFramework.Controls.MetroCheckBox()
            Me.FunimationTabPage = New MetroFramework.Controls.MetroTabPage()
            Me.GroupBox10 = New System.Windows.Forms.GroupBox()
            Me.FunimationDubComboBox = New MetroFramework.Controls.MetroComboBox()
            Me.GroupBox7 = New System.Windows.Forms.GroupBox()
            Me.FunimationHardSubComboBox = New MetroFramework.Controls.MetroComboBox()
            Me.GroupBox9 = New System.Windows.Forms.GroupBox()
            Me.GroupBox13 = New System.Windows.Forms.GroupBox()
            Me.FunimationDefaultSubComboBox = New MetroFramework.Controls.MetroComboBox()
            Me.GroupBox11 = New System.Windows.Forms.GroupBox()
            Me.FunimationSubSrtCheckBox = New MetroFramework.Controls.MetroCheckBox()
            Me.FunimationSubVttCheckBox = New MetroFramework.Controls.MetroCheckBox()
            Me.GroupBox8 = New System.Windows.Forms.GroupBox()
            Me.FunimationEnglishCheckBox = New MetroFramework.Controls.MetroCheckBox()
            Me.FunimationSpanishCheckBox = New MetroFramework.Controls.MetroCheckBox()
            Me.FunimationPortugueseCheckBox = New MetroFramework.Controls.MetroCheckBox()
            Me.AboutTabPage = New MetroFramework.Controls.MetroTabPage()
            Me.Label8 = New MetroFramework.Controls.MetroLabel()
            Me.LastVersion = New MetroFramework.Controls.MetroLabel()
            Me.MetroFrameworkLabel = New MetroFramework.Controls.MetroLabel()
            Me.Label5 = New MetroFramework.Controls.MetroLabel()
            Me.CurrentVersionLabel = New MetroFramework.Controls.MetroLabel()
            Me.WebviewLabel = New MetroFramework.Controls.MetroLabel()
            Me.FfmpegLabel = New MetroFramework.Controls.MetroLabel()
            Me.PictureBox7 = New System.Windows.Forms.PictureBox()
            Me.Label4 = New MetroFramework.Controls.MetroLabel()
            Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
            Me.Btn_Save = New System.Windows.Forms.Button()
            Me.MetroStyleManager1 = New MetroFramework.Components.MetroStyleManager(Me.components)
            Me.StyleExtender = New MetroFramework.Components.MetroStyleExtender(Me.components)
            Me.MetroLabel10 = New MetroFramework.Controls.MetroLabel()
            Me.MetroLabel11 = New MetroFramework.Controls.MetroLabel()
            Me.CrunchyrollSoftSubsGroupBox.SuspendLayout()
            Me.CrunchyrollHardsubGroupBox.SuspendLayout()
            Me.BrowserSettingsGroupBox.SuspendLayout()
            Me.DownloadCountGroupBox.SuspendLayout()
            CType(Me.SimultaneousDownloadsInput, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.TabControl.SuspendLayout()
            Me.MainTabPage.SuspendLayout()
            Me.SubfolderGroupBox.SuspendLayout()
            Me.ErrorHandlingGroupBox.SuspendLayout()
            CType(Me.ErrorLimitInput, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ServerGroupBox.SuspendLayout()
            CType(Me.CustomServerPortInput, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.OutputTabPage.SuspendLayout()
            Me.QualityGroupBox.SuspendLayout()
            Me.BitratePanel.SuspendLayout()
            Me.FfmpegCommandGroupBox.SuspendLayout()
            CType(Me.BitrateNumericInput, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox4.SuspendLayout()
            Me.GroupBox16.SuspendLayout()
            Me.NamingTabPage.SuspendLayout()
            Me.GroupBox3.SuspendLayout()
            Me.FilenameExtrasGroupBox.SuspendLayout()
            Me.GroupBox12.SuspendLayout()
            Me.CrunchyrollTabPage.SuspendLayout()
            Me.GroupBox20.SuspendLayout()
            Me.GroupBox19.SuspendLayout()
            Me.FunimationTabPage.SuspendLayout()
            Me.GroupBox10.SuspendLayout()
            Me.GroupBox7.SuspendLayout()
            Me.GroupBox9.SuspendLayout()
            Me.GroupBox13.SuspendLayout()
            Me.GroupBox11.SuspendLayout()
            Me.GroupBox8.SuspendLayout()
            Me.AboutTabPage.SuspendLayout()
            CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'ToolTip1
            '
            Me.ToolTip1.AutoPopDelay = 8000
            Me.ToolTip1.InitialDelay = 500
            Me.ToolTip1.IsBalloon = True
            Me.ToolTip1.ReshowDelay = 100
            '
            'KodiNamingTemplateButton
            '
            Me.KodiNamingTemplateButton.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.KodiNamingTemplateButton.Location = New System.Drawing.Point(361, 114)
            Me.KodiNamingTemplateButton.Name = "KodiNamingTemplateButton"
            Me.KodiNamingTemplateButton.Size = New System.Drawing.Size(96, 23)
            Me.KodiNamingTemplateButton.TabIndex = 38
            Me.KodiNamingTemplateButton.Text = "Kodi naming"
            Me.ToolTip1.SetToolTip(Me.KodiNamingTemplateButton, "Sets the template to use Kodi naming conventions.")
            Me.KodiNamingTemplateButton.UseSelectable = True
            '
            'AudioLanguageTemplateButton
            '
            Me.AudioLanguageTemplateButton.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.AudioLanguageTemplateButton.Location = New System.Drawing.Point(361, 85)
            Me.AudioLanguageTemplateButton.Name = "AudioLanguageTemplateButton"
            Me.AudioLanguageTemplateButton.Size = New System.Drawing.Size(96, 23)
            Me.AudioLanguageTemplateButton.TabIndex = 37
            Me.AudioLanguageTemplateButton.Text = "Audio language"
            Me.ToolTip1.SetToolTip(Me.AudioLanguageTemplateButton, "Inserts the audio language at the current cursor position.")
            Me.AudioLanguageTemplateButton.UseSelectable = True
            '
            'EpisodeTitleTemplateButton
            '
            Me.EpisodeTitleTemplateButton.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.EpisodeTitleTemplateButton.Location = New System.Drawing.Point(207, 114)
            Me.EpisodeTitleTemplateButton.Name = "EpisodeTitleTemplateButton"
            Me.EpisodeTitleTemplateButton.Size = New System.Drawing.Size(96, 23)
            Me.EpisodeTitleTemplateButton.TabIndex = 36
            Me.EpisodeTitleTemplateButton.Text = "Episode title"
            Me.ToolTip1.SetToolTip(Me.EpisodeTitleTemplateButton, "Inserts the episode title at the current cursor position.")
            Me.EpisodeTitleTemplateButton.UseSelectable = True
            '
            'EpisodeNumberTemplateButton
            '
            Me.EpisodeNumberTemplateButton.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.EpisodeNumberTemplateButton.Location = New System.Drawing.Point(207, 85)
            Me.EpisodeNumberTemplateButton.Name = "EpisodeNumberTemplateButton"
            Me.EpisodeNumberTemplateButton.Size = New System.Drawing.Size(96, 23)
            Me.EpisodeNumberTemplateButton.TabIndex = 35
            Me.EpisodeNumberTemplateButton.Text = "Episode number"
            Me.ToolTip1.SetToolTip(Me.EpisodeNumberTemplateButton, "Inserts the episode number at the current cursor position.")
            Me.EpisodeNumberTemplateButton.UseSelectable = True
            '
            'SeasonNumberTemplateButton
            '
            Me.SeasonNumberTemplateButton.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.SeasonNumberTemplateButton.Location = New System.Drawing.Point(53, 114)
            Me.SeasonNumberTemplateButton.Name = "SeasonNumberTemplateButton"
            Me.SeasonNumberTemplateButton.Size = New System.Drawing.Size(96, 23)
            Me.SeasonNumberTemplateButton.TabIndex = 34
            Me.SeasonNumberTemplateButton.Text = "Season number"
            Me.ToolTip1.SetToolTip(Me.SeasonNumberTemplateButton, "Inserts the season number at the current cursor position.")
            Me.SeasonNumberTemplateButton.UseSelectable = True
            '
            'SeriesNameTemplateButton
            '
            Me.SeriesNameTemplateButton.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.SeriesNameTemplateButton.Location = New System.Drawing.Point(53, 85)
            Me.SeriesNameTemplateButton.Name = "SeriesNameTemplateButton"
            Me.SeriesNameTemplateButton.Size = New System.Drawing.Size(96, 23)
            Me.SeriesNameTemplateButton.TabIndex = 33
            Me.SeriesNameTemplateButton.Text = "Series name"
            Me.ToolTip1.SetToolTip(Me.SeriesNameTemplateButton, "Inserts the series name at the current cursor position.")
            Me.SeriesNameTemplateButton.UseSelectable = True
            '
            'MetroLabel9
            '
            Me.MetroLabel9.AutoSize = True
            Me.MetroLabel9.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.MetroLabel9.Location = New System.Drawing.Point(6, 18)
            Me.MetroLabel9.Name = "MetroLabel9"
            Me.MetroLabel9.Size = New System.Drawing.Size(90, 19)
            Me.MetroLabel9.TabIndex = 41
            Me.MetroLabel9.Text = "Edit template"
            Me.ToolTip1.SetToolTip(Me.MetroLabel9, resources.GetString("MetroLabel9.ToolTip"))
            '
            'FilenameTemplateInput
            '
            Me.FilenameTemplateInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            '
            '
            '
            Me.FilenameTemplateInput.CustomButton.Image = Nothing
            Me.FilenameTemplateInput.CustomButton.Location = New System.Drawing.Point(470, 1)
            Me.FilenameTemplateInput.CustomButton.Name = ""
            Me.FilenameTemplateInput.CustomButton.Size = New System.Drawing.Size(27, 27)
            Me.FilenameTemplateInput.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
            Me.FilenameTemplateInput.CustomButton.TabIndex = 1
            Me.FilenameTemplateInput.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
            Me.FilenameTemplateInput.CustomButton.UseSelectable = True
            Me.FilenameTemplateInput.CustomButton.Visible = False
            Me.FilenameTemplateInput.FontSize = MetroFramework.MetroTextBoxSize.Medium
            Me.FilenameTemplateInput.Lines = New String(-1) {}
            Me.FilenameTemplateInput.Location = New System.Drawing.Point(6, 40)
            Me.FilenameTemplateInput.MaxLength = 32767
            Me.FilenameTemplateInput.Name = "FilenameTemplateInput"
            Me.FilenameTemplateInput.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
            Me.FilenameTemplateInput.ScrollBars = System.Windows.Forms.ScrollBars.None
            Me.FilenameTemplateInput.SelectedText = ""
            Me.FilenameTemplateInput.SelectionLength = 0
            Me.FilenameTemplateInput.SelectionStart = 0
            Me.FilenameTemplateInput.ShortcutsEnabled = True
            Me.FilenameTemplateInput.Size = New System.Drawing.Size(498, 29)
            Me.FilenameTemplateInput.TabIndex = 21
            Me.FilenameTemplateInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.FilenameTemplateInput.UseSelectable = True
            Me.FilenameTemplateInput.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
            Me.FilenameTemplateInput.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
            '
            'CR_SoftSubDefault
            '
            Me.CR_SoftSubDefault.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.CR_SoftSubDefault.DropDownHeight = 250
            Me.CR_SoftSubDefault.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CR_SoftSubDefault.FormattingEnabled = True
            Me.CR_SoftSubDefault.IntegralHeight = False
            Me.CR_SoftSubDefault.ItemHeight = 23
            Me.CR_SoftSubDefault.Items.AddRange(New Object() {"[Disabled]"})
            Me.CR_SoftSubDefault.Location = New System.Drawing.Point(264, 40)
            Me.CR_SoftSubDefault.Name = "CR_SoftSubDefault"
            Me.CR_SoftSubDefault.Size = New System.Drawing.Size(238, 29)
            Me.CR_SoftSubDefault.TabIndex = 30
            Me.CR_SoftSubDefault.UseSelectable = True
            '
            'CrunchyrollSoftSubsGroupBox
            '
            Me.CrunchyrollSoftSubsGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CrunchyrollSoftSubsGroupBox.BackColor = System.Drawing.Color.Transparent
            Me.CrunchyrollSoftSubsGroupBox.Controls.Add(Me.Label6)
            Me.CrunchyrollSoftSubsGroupBox.Controls.Add(Me.Label3)
            Me.CrunchyrollSoftSubsGroupBox.Controls.Add(Me.CR_SoftSubDefault)
            Me.CrunchyrollSoftSubsGroupBox.Controls.Add(Me.CrunchyrollSoftSubsCheckedListBox)
            Me.CrunchyrollSoftSubsGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CrunchyrollSoftSubsGroupBox.Location = New System.Drawing.Point(5, 173)
            Me.CrunchyrollSoftSubsGroupBox.Name = "CrunchyrollSoftSubsGroupBox"
            Me.CrunchyrollSoftSubsGroupBox.Size = New System.Drawing.Size(510, 253)
            Me.CrunchyrollSoftSubsGroupBox.TabIndex = 20
            Me.CrunchyrollSoftSubsGroupBox.TabStop = False
            Me.CrunchyrollSoftSubsGroupBox.Text = "SoftSubs"
            '
            'Label6
            '
            Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(8, 21)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(134, 16)
            Me.Label6.TabIndex = 32
            Me.Label6.Text = "Subtitles to download"
            '
            'Label3
            '
            Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(264, 21)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(96, 16)
            Me.Label3.TabIndex = 31
            Me.Label3.Text = "Default Subtitle"
            '
            'CrunchyrollSoftSubsCheckedListBox
            '
            Me.CrunchyrollSoftSubsCheckedListBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
            Me.CrunchyrollSoftSubsCheckedListBox.CheckOnClick = True
            Me.CrunchyrollSoftSubsCheckedListBox.FormattingEnabled = True
            Me.CrunchyrollSoftSubsCheckedListBox.Location = New System.Drawing.Point(8, 40)
            Me.CrunchyrollSoftSubsCheckedListBox.Name = "CrunchyrollSoftSubsCheckedListBox"
            Me.CrunchyrollSoftSubsCheckedListBox.Size = New System.Drawing.Size(238, 191)
            Me.CrunchyrollSoftSubsCheckedListBox.Sorted = True
            Me.CrunchyrollSoftSubsCheckedListBox.TabIndex = 0
            '
            'CrunchyrollHardsubGroupBox
            '
            Me.CrunchyrollHardsubGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CrunchyrollHardsubGroupBox.BackColor = System.Drawing.Color.Transparent
            Me.CrunchyrollHardsubGroupBox.Controls.Add(Me.CrunchyrollHardsubComboBox)
            Me.CrunchyrollHardsubGroupBox.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.CrunchyrollHardsubGroupBox.ForeColor = System.Drawing.Color.Black
            Me.CrunchyrollHardsubGroupBox.Location = New System.Drawing.Point(5, 108)
            Me.CrunchyrollHardsubGroupBox.Name = "CrunchyrollHardsubGroupBox"
            Me.CrunchyrollHardsubGroupBox.Size = New System.Drawing.Size(510, 65)
            Me.CrunchyrollHardsubGroupBox.TabIndex = 10
            Me.CrunchyrollHardsubGroupBox.TabStop = False
            Me.CrunchyrollHardsubGroupBox.Text = "Hardsub language"
            '
            'CrunchyrollHardsubComboBox
            '
            Me.CrunchyrollHardsubComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.CrunchyrollHardsubComboBox.DropDownHeight = 275
            Me.CrunchyrollHardsubComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CrunchyrollHardsubComboBox.FormattingEnabled = True
            Me.CrunchyrollHardsubComboBox.IntegralHeight = False
            Me.CrunchyrollHardsubComboBox.ItemHeight = 23
            Me.CrunchyrollHardsubComboBox.Location = New System.Drawing.Point(95, 25)
            Me.CrunchyrollHardsubComboBox.Name = "CrunchyrollHardsubComboBox"
            Me.CrunchyrollHardsubComboBox.Size = New System.Drawing.Size(320, 29)
            Me.CrunchyrollHardsubComboBox.TabIndex = 20
            Me.CrunchyrollHardsubComboBox.UseSelectable = True
            '
            'BrowserSettingsGroupBox
            '
            Me.BrowserSettingsGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BrowserSettingsGroupBox.BackColor = System.Drawing.Color.Transparent
            Me.BrowserSettingsGroupBox.Controls.Add(Me.Label1)
            Me.BrowserSettingsGroupBox.Controls.Add(Me.DefaultWebsiteTextBox)
            Me.BrowserSettingsGroupBox.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.BrowserSettingsGroupBox.ForeColor = System.Drawing.Color.Black
            Me.BrowserSettingsGroupBox.Location = New System.Drawing.Point(8, 76)
            Me.BrowserSettingsGroupBox.Name = "BrowserSettingsGroupBox"
            Me.BrowserSettingsGroupBox.Size = New System.Drawing.Size(507, 88)
            Me.BrowserSettingsGroupBox.TabIndex = 60
            Me.BrowserSettingsGroupBox.TabStop = False
            Me.BrowserSettingsGroupBox.Text = "Browser Settings"
            '
            'Label1
            '
            Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.Label1.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.Label1.Location = New System.Drawing.Point(11, 20)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(469, 22)
            Me.Label1.TabIndex = 2
            Me.Label1.Text = "Default Website"
            Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'DefaultWebsiteTextBox
            '
            Me.DefaultWebsiteTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top
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
            Me.DefaultWebsiteTextBox.Location = New System.Drawing.Point(11, 45)
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
            'DownloadCountGroupBox
            '
            Me.DownloadCountGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DownloadCountGroupBox.BackColor = System.Drawing.Color.Transparent
            Me.DownloadCountGroupBox.Controls.Add(Me.SimultaneousDownloadsInput)
            Me.DownloadCountGroupBox.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.DownloadCountGroupBox.ForeColor = System.Drawing.Color.Black
            Me.DownloadCountGroupBox.Location = New System.Drawing.Point(8, 8)
            Me.DownloadCountGroupBox.Name = "DownloadCountGroupBox"
            Me.DownloadCountGroupBox.Size = New System.Drawing.Size(507, 68)
            Me.DownloadCountGroupBox.TabIndex = 50
            Me.DownloadCountGroupBox.TabStop = False
            Me.DownloadCountGroupBox.Text = "Simultaneous Downloads"
            '
            'SimultaneousDownloadsInput
            '
            Me.SimultaneousDownloadsInput.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.SimultaneousDownloadsInput.Location = New System.Drawing.Point(121, 23)
            Me.SimultaneousDownloadsInput.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
            Me.SimultaneousDownloadsInput.Name = "SimultaneousDownloadsInput"
            Me.SimultaneousDownloadsInput.Size = New System.Drawing.Size(265, 22)
            Me.SimultaneousDownloadsInput.TabIndex = 1
            Me.SimultaneousDownloadsInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.SimultaneousDownloadsInput.Value = New Decimal(New Integer() {1, 0, 0, 0})
            '
            'TabControl
            '
            Me.TabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TabControl.Controls.Add(Me.MainTabPage)
            Me.TabControl.Controls.Add(Me.OutputTabPage)
            Me.TabControl.Controls.Add(Me.NamingTabPage)
            Me.TabControl.Controls.Add(Me.CrunchyrollTabPage)
            Me.TabControl.Controls.Add(Me.FunimationTabPage)
            Me.TabControl.Controls.Add(Me.AboutTabPage)
            Me.TabControl.FontSize = MetroFramework.MetroTabControlSize.Tall
            Me.TabControl.FontWeight = MetroFramework.MetroTabControlWeight.Regular
            Me.TabControl.Location = New System.Drawing.Point(23, 63)
            Me.TabControl.Name = "TabControl"
            Me.TabControl.SelectedIndex = 0
            Me.TabControl.Size = New System.Drawing.Size(528, 589)
            Me.TabControl.TabIndex = 0
            Me.TabControl.UseSelectable = True
            '
            'MainTabPage
            '
            Me.MainTabPage.AutoScroll = True
            Me.MainTabPage.Controls.Add(Me.SubfolderGroupBox)
            Me.MainTabPage.Controls.Add(Me.ErrorHandlingGroupBox)
            Me.MainTabPage.Controls.Add(Me.ServerGroupBox)
            Me.MainTabPage.Controls.Add(Me.BrowserSettingsGroupBox)
            Me.MainTabPage.Controls.Add(Me.DownloadCountGroupBox)
            Me.MainTabPage.HorizontalScrollbar = True
            Me.MainTabPage.HorizontalScrollbarBarColor = True
            Me.MainTabPage.HorizontalScrollbarHighlightOnWheel = False
            Me.MainTabPage.HorizontalScrollbarSize = 10
            Me.MainTabPage.Location = New System.Drawing.Point(4, 44)
            Me.MainTabPage.Name = "MainTabPage"
            Me.MainTabPage.Padding = New System.Windows.Forms.Padding(5)
            Me.MainTabPage.Size = New System.Drawing.Size(520, 541)
            Me.MainTabPage.TabIndex = 10
            Me.MainTabPage.Text = "Main"
            Me.MainTabPage.VerticalScrollbar = True
            Me.MainTabPage.VerticalScrollbarBarColor = True
            Me.MainTabPage.VerticalScrollbarHighlightOnWheel = False
            Me.MainTabPage.VerticalScrollbarSize = 10
            '
            'SubfolderGroupBox
            '
            Me.SubfolderGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SubfolderGroupBox.BackColor = System.Drawing.Color.Transparent
            Me.SubfolderGroupBox.Controls.Add(Me.HideSubfoldersComboBox)
            Me.SubfolderGroupBox.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.SubfolderGroupBox.ForeColor = System.Drawing.Color.Black
            Me.SubfolderGroupBox.Location = New System.Drawing.Point(8, 390)
            Me.SubfolderGroupBox.Name = "SubfolderGroupBox"
            Me.SubfolderGroupBox.Size = New System.Drawing.Size(507, 67)
            Me.SubfolderGroupBox.TabIndex = 82
            Me.SubfolderGroupBox.TabStop = False
            Me.SubfolderGroupBox.Text = "Subfolder"
            '
            'HideSubfoldersComboBox
            '
            Me.HideSubfoldersComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.HideSubfoldersComboBox.DropDownHeight = 275
            Me.HideSubfoldersComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.HideSubfoldersComboBox.FormattingEnabled = True
            Me.HideSubfoldersComboBox.IntegralHeight = False
            Me.HideSubfoldersComboBox.ItemHeight = 23
            Me.HideSubfoldersComboBox.Location = New System.Drawing.Point(90, 25)
            Me.HideSubfoldersComboBox.Name = "HideSubfoldersComboBox"
            Me.HideSubfoldersComboBox.Size = New System.Drawing.Size(326, 29)
            Me.HideSubfoldersComboBox.TabIndex = 21
            Me.HideSubfoldersComboBox.UseSelectable = True
            '
            'ErrorHandlingGroupBox
            '
            Me.ErrorHandlingGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ErrorHandlingGroupBox.BackColor = System.Drawing.Color.Transparent
            Me.ErrorHandlingGroupBox.Controls.Add(Me.MetroLabel1)
            Me.ErrorHandlingGroupBox.Controls.Add(Me.CheckBox2)
            Me.ErrorHandlingGroupBox.Controls.Add(Me.Label2)
            Me.ErrorHandlingGroupBox.Controls.Add(Me.ErrorLimitInput)
            Me.ErrorHandlingGroupBox.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.ErrorHandlingGroupBox.ForeColor = System.Drawing.Color.Black
            Me.ErrorHandlingGroupBox.Location = New System.Drawing.Point(8, 278)
            Me.ErrorHandlingGroupBox.Name = "ErrorHandlingGroupBox"
            Me.ErrorHandlingGroupBox.Size = New System.Drawing.Size(507, 112)
            Me.ErrorHandlingGroupBox.TabIndex = 81
            Me.ErrorHandlingGroupBox.TabStop = False
            Me.ErrorHandlingGroupBox.Text = "Error Handling"
            '
            'MetroLabel1
            '
            Me.MetroLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.MetroLabel1.AutoSize = True
            Me.MetroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.MetroLabel1.Location = New System.Drawing.Point(199, 44)
            Me.MetroLabel1.Name = "MetroLabel1"
            Me.MetroLabel1.Size = New System.Drawing.Size(113, 19)
            Me.MetroLabel1.TabIndex = 45
            Me.MetroLabel1.Text = "(0 = deactivated)"
            '
            'CheckBox2
            '
            Me.CheckBox2.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.CheckBox2.AutoSize = True
            Me.CheckBox2.Location = New System.Drawing.Point(77, 74)
            Me.CheckBox2.Name = "CheckBox2"
            Me.CheckBox2.Size = New System.Drawing.Size(125, 15)
            Me.CheckBox2.TabIndex = 44
            Me.CheckBox2.Text = "ignore future errors"
            Me.CheckBox2.UseSelectable = True
            Me.CheckBox2.Visible = False
            '
            'Label2
            '
            Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.Label2.AutoSize = True
            Me.Label2.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.Label2.Location = New System.Drawing.Point(70, 18)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(386, 19)
            Me.Label2.TabIndex = 2
            Me.Label2.Text = "The amout of error(s) until the Download process get paused "
            '
            'ErrorLimitInput
            '
            Me.ErrorLimitInput.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.ErrorLimitInput.Location = New System.Drawing.Point(229, 70)
            Me.ErrorLimitInput.Name = "ErrorLimitInput"
            Me.ErrorLimitInput.Size = New System.Drawing.Size(200, 22)
            Me.ErrorLimitInput.TabIndex = 6
            Me.ErrorLimitInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            '
            'ServerGroupBox
            '
            Me.ServerGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ServerGroupBox.BackColor = System.Drawing.Color.Transparent
            Me.ServerGroupBox.Controls.Add(Me.CustomServerPortInput)
            Me.ServerGroupBox.Controls.Add(Me.ServerPortLabel)
            Me.ServerGroupBox.Controls.Add(Me.IgnoreTlsCheckBox)
            Me.ServerGroupBox.Controls.Add(Me.ServerPortInput)
            Me.ServerGroupBox.Controls.Add(Me.DarkModeCheckBox)
            Me.ServerGroupBox.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.ServerGroupBox.ForeColor = System.Drawing.Color.Black
            Me.ServerGroupBox.Location = New System.Drawing.Point(8, 164)
            Me.ServerGroupBox.Name = "ServerGroupBox"
            Me.ServerGroupBox.Size = New System.Drawing.Size(507, 114)
            Me.ServerGroupBox.TabIndex = 71
            Me.ServerGroupBox.TabStop = False
            Me.ServerGroupBox.Text = "Other"
            '
            'CustomServerPortInput
            '
            Me.CustomServerPortInput.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.CustomServerPortInput.Enabled = False
            Me.CustomServerPortInput.Location = New System.Drawing.Point(360, 25)
            Me.CustomServerPortInput.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
            Me.CustomServerPortInput.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.CustomServerPortInput.Name = "CustomServerPortInput"
            Me.CustomServerPortInput.Size = New System.Drawing.Size(120, 22)
            Me.CustomServerPortInput.TabIndex = 48
            Me.CustomServerPortInput.Value = New Decimal(New Integer() {80, 0, 0, 0})
            '
            'ServerPortLabel
            '
            Me.ServerPortLabel.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.ServerPortLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.ServerPortLabel.Location = New System.Drawing.Point(11, 21)
            Me.ServerPortLabel.Name = "ServerPortLabel"
            Me.ServerPortLabel.Size = New System.Drawing.Size(145, 29)
            Me.ServerPortLabel.TabIndex = 47
            Me.ServerPortLabel.Text = "Add-on server port"
            Me.ServerPortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'IgnoreTlsCheckBox
            '
            Me.IgnoreTlsCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.IgnoreTlsCheckBox.AutoSize = True
            Me.IgnoreTlsCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
            Me.IgnoreTlsCheckBox.Location = New System.Drawing.Point(233, 75)
            Me.IgnoreTlsCheckBox.Name = "IgnoreTlsCheckBox"
            Me.IgnoreTlsCheckBox.Size = New System.Drawing.Size(223, 19)
            Me.IgnoreTlsCheckBox.TabIndex = 46
            Me.IgnoreTlsCheckBox.Text = "add ""--insecure"" to curl requests"
            Me.IgnoreTlsCheckBox.UseSelectable = True
            '
            'ServerPortInput
            '
            Me.ServerPortInput.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.ServerPortInput.DropDownHeight = 250
            Me.ServerPortInput.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ServerPortInput.FormattingEnabled = True
            Me.ServerPortInput.IntegralHeight = False
            Me.ServerPortInput.ItemHeight = 23
            Me.ServerPortInput.Location = New System.Drawing.Point(162, 21)
            Me.ServerPortInput.Name = "ServerPortInput"
            Me.ServerPortInput.Size = New System.Drawing.Size(192, 29)
            Me.ServerPortInput.TabIndex = 45
            Me.ServerPortInput.UseSelectable = True
            Me.ServerPortInput.UseStyleColors = True
            '
            'DarkModeCheckBox
            '
            Me.DarkModeCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.DarkModeCheckBox.AutoSize = True
            Me.DarkModeCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
            Me.DarkModeCheckBox.Location = New System.Drawing.Point(61, 75)
            Me.DarkModeCheckBox.Name = "DarkModeCheckBox"
            Me.DarkModeCheckBox.Size = New System.Drawing.Size(135, 19)
            Me.DarkModeCheckBox.TabIndex = 5
            Me.DarkModeCheckBox.Text = "enable dark mode"
            Me.DarkModeCheckBox.UseSelectable = True
            '
            'OutputTabPage
            '
            Me.OutputTabPage.AutoScroll = True
            Me.OutputTabPage.Controls.Add(Me.QualityGroupBox)
            Me.OutputTabPage.Controls.Add(Me.FfmpegCommandGroupBox)
            Me.OutputTabPage.Controls.Add(Me.GroupBox4)
            Me.OutputTabPage.Controls.Add(Me.GroupBox16)
            Me.OutputTabPage.HorizontalScrollbar = True
            Me.OutputTabPage.HorizontalScrollbarBarColor = True
            Me.OutputTabPage.HorizontalScrollbarHighlightOnWheel = False
            Me.OutputTabPage.HorizontalScrollbarSize = 10
            Me.OutputTabPage.Location = New System.Drawing.Point(4, 44)
            Me.OutputTabPage.Name = "OutputTabPage"
            Me.OutputTabPage.Padding = New System.Windows.Forms.Padding(5)
            Me.OutputTabPage.Size = New System.Drawing.Size(520, 541)
            Me.OutputTabPage.TabIndex = 11
            Me.OutputTabPage.Text = "Output"
            Me.OutputTabPage.VerticalScrollbar = True
            Me.OutputTabPage.VerticalScrollbarBarColor = True
            Me.OutputTabPage.VerticalScrollbarHighlightOnWheel = False
            Me.OutputTabPage.VerticalScrollbarSize = 10
            '
            'QualityGroupBox
            '
            Me.QualityGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.QualityGroupBox.BackColor = System.Drawing.Color.Transparent
            Me.QualityGroupBox.Controls.Add(Me.LowerResolutionRadioButton)
            Me.QualityGroupBox.Controls.Add(Me.HigherResolutionRadioButton)
            Me.QualityGroupBox.Controls.Add(Me.BitratePanel)
            Me.QualityGroupBox.Controls.Add(Me.ResolutionComboBox)
            Me.QualityGroupBox.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.QualityGroupBox.ForeColor = System.Drawing.Color.Black
            Me.QualityGroupBox.Location = New System.Drawing.Point(8, 141)
            Me.QualityGroupBox.Name = "QualityGroupBox"
            Me.QualityGroupBox.Size = New System.Drawing.Size(507, 99)
            Me.QualityGroupBox.TabIndex = 34
            Me.QualityGroupBox.TabStop = False
            Me.QualityGroupBox.Text = "Quality"
            '
            'LowerResolutionRadioButton
            '
            Me.LowerResolutionRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.LowerResolutionRadioButton.AutoSize = True
            Me.LowerResolutionRadioButton.Location = New System.Drawing.Point(136, 68)
            Me.LowerResolutionRadioButton.Name = "LowerResolutionRadioButton"
            Me.LowerResolutionRadioButton.Size = New System.Drawing.Size(68, 15)
            Me.LowerResolutionRadioButton.TabIndex = 20
            Me.LowerResolutionRadioButton.Text = "Or lower"
            Me.LowerResolutionRadioButton.UseSelectable = True
            '
            'HigherResolutionRadioButton
            '
            Me.HigherResolutionRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.HigherResolutionRadioButton.AutoSize = True
            Me.HigherResolutionRadioButton.Location = New System.Drawing.Point(39, 68)
            Me.HigherResolutionRadioButton.Name = "HigherResolutionRadioButton"
            Me.HigherResolutionRadioButton.Size = New System.Drawing.Size(73, 15)
            Me.HigherResolutionRadioButton.TabIndex = 19
            Me.HigherResolutionRadioButton.Text = "Or higher"
            Me.HigherResolutionRadioButton.UseSelectable = True
            '
            'BitratePanel
            '
            Me.BitratePanel.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.BitratePanel.BackColor = System.Drawing.Color.Transparent
            Me.BitratePanel.Controls.Add(Me.MetroLabel7)
            Me.BitratePanel.Controls.Add(Me.HigherBitrateRadioButton)
            Me.BitratePanel.Controls.Add(Me.LowerBitrateRadioButton)
            Me.BitratePanel.Location = New System.Drawing.Point(272, 21)
            Me.BitratePanel.Name = "BitratePanel"
            Me.BitratePanel.Size = New System.Drawing.Size(228, 72)
            Me.BitratePanel.TabIndex = 53
            '
            'MetroLabel7
            '
            Me.MetroLabel7.AutoSize = True
            Me.MetroLabel7.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.MetroLabel7.Location = New System.Drawing.Point(3, 4)
            Me.MetroLabel7.Name = "MetroLabel7"
            Me.MetroLabel7.Size = New System.Drawing.Size(104, 19)
            Me.MetroLabel7.TabIndex = 23
            Me.MetroLabel7.Text = "When available:"
            '
            'HigherBitrateRadioButton
            '
            Me.HigherBitrateRadioButton.AutoSize = True
            Me.HigherBitrateRadioButton.Location = New System.Drawing.Point(3, 26)
            Me.HigherBitrateRadioButton.Name = "HigherBitrateRadioButton"
            Me.HigherBitrateRadioButton.Size = New System.Drawing.Size(128, 15)
            Me.HigherBitrateRadioButton.TabIndex = 21
            Me.HigherBitrateRadioButton.Text = "Prefer higher bitrate"
            Me.HigherBitrateRadioButton.UseSelectable = True
            '
            'LowerBitrateRadioButton
            '
            Me.LowerBitrateRadioButton.AutoSize = True
            Me.LowerBitrateRadioButton.Location = New System.Drawing.Point(3, 47)
            Me.LowerBitrateRadioButton.Name = "LowerBitrateRadioButton"
            Me.LowerBitrateRadioButton.Size = New System.Drawing.Size(123, 15)
            Me.LowerBitrateRadioButton.TabIndex = 22
            Me.LowerBitrateRadioButton.Text = "Prefer lower bitrate"
            Me.LowerBitrateRadioButton.UseSelectable = True
            '
            'ResolutionComboBox
            '
            Me.ResolutionComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.ResolutionComboBox.FormattingEnabled = True
            Me.ResolutionComboBox.ItemHeight = 23
            Me.ResolutionComboBox.Location = New System.Drawing.Point(39, 25)
            Me.ResolutionComboBox.Name = "ResolutionComboBox"
            Me.ResolutionComboBox.Size = New System.Drawing.Size(165, 29)
            Me.ResolutionComboBox.TabIndex = 17
            Me.ResolutionComboBox.UseSelectable = True
            '
            'FfmpegCommandGroupBox
            '
            Me.FfmpegCommandGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
            Me.FfmpegCommandGroupBox.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.FfmpegCommandGroupBox.ForeColor = System.Drawing.Color.Black
            Me.FfmpegCommandGroupBox.Location = New System.Drawing.Point(8, 310)
            Me.FfmpegCommandGroupBox.Name = "FfmpegCommandGroupBox"
            Me.FfmpegCommandGroupBox.Size = New System.Drawing.Size(507, 98)
            Me.FfmpegCommandGroupBox.TabIndex = 51
            Me.FfmpegCommandGroupBox.TabStop = False
            Me.FfmpegCommandGroupBox.Text = "Re-encoding"
            '
            'FfmpegCopyCheckBox
            '
            Me.FfmpegCopyCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.FfmpegCopyCheckBox.AutoSize = True
            Me.FfmpegCopyCheckBox.Location = New System.Drawing.Point(39, 20)
            Me.FfmpegCopyCheckBox.Name = "FfmpegCopyCheckBox"
            Me.FfmpegCopyCheckBox.Size = New System.Drawing.Size(155, 15)
            Me.FfmpegCopyCheckBox.TabIndex = 52
            Me.FfmpegCopyCheckBox.Text = "Copy (Do not re-encode)"
            Me.FfmpegCopyCheckBox.UseSelectable = True
            '
            'TargetBitrateCheckBox
            '
            Me.TargetBitrateCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.TargetBitrateCheckBox.AutoSize = True
            Me.TargetBitrateCheckBox.Location = New System.Drawing.Point(345, 43)
            Me.TargetBitrateCheckBox.Name = "TargetBitrateCheckBox"
            Me.TargetBitrateCheckBox.Size = New System.Drawing.Size(92, 15)
            Me.TargetBitrateCheckBox.TabIndex = 51
            Me.TargetBitrateCheckBox.Text = "Target bitrate"
            Me.TargetBitrateCheckBox.UseSelectable = True
            '
            'VideoCodecComboBox
            '
            Me.VideoCodecComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.VideoCodecComboBox.FormattingEnabled = True
            Me.VideoCodecComboBox.ItemHeight = 23
            Me.VideoCodecComboBox.Location = New System.Drawing.Point(39, 61)
            Me.VideoCodecComboBox.Name = "VideoCodecComboBox"
            Me.VideoCodecComboBox.Size = New System.Drawing.Size(65, 29)
            Me.VideoCodecComboBox.TabIndex = 50
            Me.VideoCodecComboBox.UseSelectable = True
            '
            'MetroLabel6
            '
            Me.MetroLabel6.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.MetroLabel6.AutoSize = True
            Me.MetroLabel6.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.MetroLabel6.Location = New System.Drawing.Point(39, 38)
            Me.MetroLabel6.Name = "MetroLabel6"
            Me.MetroLabel6.Size = New System.Drawing.Size(47, 19)
            Me.MetroLabel6.TabIndex = 49
            Me.MetroLabel6.Text = "Codec"
            '
            'VideoEncoderComboBox
            '
            Me.VideoEncoderComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.VideoEncoderComboBox.FormattingEnabled = True
            Me.VideoEncoderComboBox.ItemHeight = 23
            Me.VideoEncoderComboBox.Location = New System.Drawing.Point(110, 61)
            Me.VideoEncoderComboBox.Name = "VideoEncoderComboBox"
            Me.VideoEncoderComboBox.Size = New System.Drawing.Size(100, 29)
            Me.VideoEncoderComboBox.TabIndex = 48
            Me.VideoEncoderComboBox.UseSelectable = True
            '
            'MetroLabel5
            '
            Me.MetroLabel5.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.MetroLabel5.AutoSize = True
            Me.MetroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.MetroLabel5.Location = New System.Drawing.Point(108, 39)
            Me.MetroLabel5.Name = "MetroLabel5"
            Me.MetroLabel5.Size = New System.Drawing.Size(58, 19)
            Me.MetroLabel5.TabIndex = 47
            Me.MetroLabel5.Text = "Encoder"
            '
            'FfmpegPresetComboBox
            '
            Me.FfmpegPresetComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.FfmpegPresetComboBox.FormattingEnabled = True
            Me.FfmpegPresetComboBox.ItemHeight = 23
            Me.FfmpegPresetComboBox.Location = New System.Drawing.Point(216, 61)
            Me.FfmpegPresetComboBox.Name = "FfmpegPresetComboBox"
            Me.FfmpegPresetComboBox.Size = New System.Drawing.Size(123, 29)
            Me.FfmpegPresetComboBox.TabIndex = 46
            Me.FfmpegPresetComboBox.UseSelectable = True
            '
            'MetroLabel4
            '
            Me.MetroLabel4.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.MetroLabel4.AutoSize = True
            Me.MetroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.MetroLabel4.Location = New System.Drawing.Point(216, 38)
            Me.MetroLabel4.Name = "MetroLabel4"
            Me.MetroLabel4.Size = New System.Drawing.Size(88, 19)
            Me.MetroLabel4.TabIndex = 45
            Me.MetroLabel4.Text = "Speed preset"
            '
            'BitrateNumericInput
            '
            Me.BitrateNumericInput.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.BitrateNumericInput.Increment = New Decimal(New Integer() {100, 0, 0, 0})
            Me.BitrateNumericInput.Location = New System.Drawing.Point(345, 62)
            Me.BitrateNumericInput.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
            Me.BitrateNumericInput.Minimum = New Decimal(New Integer() {1000, 0, 0, 0})
            Me.BitrateNumericInput.Name = "BitrateNumericInput"
            Me.BitrateNumericInput.Size = New System.Drawing.Size(67, 22)
            Me.BitrateNumericInput.TabIndex = 43
            Me.BitrateNumericInput.Value = New Decimal(New Integer() {1000, 0, 0, 0})
            '
            'MetroLabel2
            '
            Me.MetroLabel2.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.MetroLabel2.AutoSize = True
            Me.MetroLabel2.Location = New System.Drawing.Point(418, 62)
            Me.MetroLabel2.Name = "MetroLabel2"
            Me.MetroLabel2.Size = New System.Drawing.Size(49, 19)
            Me.MetroLabel2.TabIndex = 44
            Me.MetroLabel2.Text = "(KBit/s)"
            '
            'GroupBox4
            '
            Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox4.Controls.Add(Me.SubtitleFormatComboBox)
            Me.GroupBox4.Controls.Add(Me.VideoFormatComboBox)
            Me.GroupBox4.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.GroupBox4.ForeColor = System.Drawing.Color.Black
            Me.GroupBox4.Location = New System.Drawing.Point(8, 240)
            Me.GroupBox4.Name = "GroupBox4"
            Me.GroupBox4.Size = New System.Drawing.Size(507, 70)
            Me.GroupBox4.TabIndex = 41
            Me.GroupBox4.TabStop = False
            Me.GroupBox4.Text = "Format"
            '
            'SubtitleFormatComboBox
            '
            Me.SubtitleFormatComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.SubtitleFormatComboBox.DropDownHeight = 250
            Me.SubtitleFormatComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.SubtitleFormatComboBox.FormattingEnabled = True
            Me.SubtitleFormatComboBox.IntegralHeight = False
            Me.SubtitleFormatComboBox.ItemHeight = 23
            Me.SubtitleFormatComboBox.Items.AddRange(New Object() {"[merge disabled]"})
            Me.SubtitleFormatComboBox.Location = New System.Drawing.Point(272, 25)
            Me.SubtitleFormatComboBox.Name = "SubtitleFormatComboBox"
            Me.SubtitleFormatComboBox.Size = New System.Drawing.Size(175, 29)
            Me.SubtitleFormatComboBox.TabIndex = 19
            Me.SubtitleFormatComboBox.UseSelectable = True
            '
            'VideoFormatComboBox
            '
            Me.VideoFormatComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.VideoFormatComboBox.DropDownHeight = 250
            Me.VideoFormatComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.VideoFormatComboBox.FormattingEnabled = True
            Me.VideoFormatComboBox.IntegralHeight = False
            Me.VideoFormatComboBox.ItemHeight = 23
            Me.VideoFormatComboBox.Location = New System.Drawing.Point(57, 25)
            Me.VideoFormatComboBox.Name = "VideoFormatComboBox"
            Me.VideoFormatComboBox.Size = New System.Drawing.Size(175, 29)
            Me.VideoFormatComboBox.TabIndex = 17
            Me.VideoFormatComboBox.UseSelectable = True
            '
            'GroupBox16
            '
            Me.GroupBox16.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox16.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox16.Controls.Add(Me.MetroLabel3)
            Me.GroupBox16.Controls.Add(Me.TemporaryFolderTextBox)
            Me.GroupBox16.Controls.Add(Me.DownloadModeDropdown)
            Me.GroupBox16.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.GroupBox16.ForeColor = System.Drawing.Color.Black
            Me.GroupBox16.Location = New System.Drawing.Point(8, 8)
            Me.GroupBox16.Name = "GroupBox16"
            Me.GroupBox16.Size = New System.Drawing.Size(507, 133)
            Me.GroupBox16.TabIndex = 32
            Me.GroupBox16.TabStop = False
            Me.GroupBox16.Text = "Download Mode"
            '
            'MetroLabel3
            '
            Me.MetroLabel3.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.MetroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.MetroLabel3.Location = New System.Drawing.Point(41, 65)
            Me.MetroLabel3.Name = "MetroLabel3"
            Me.MetroLabel3.Size = New System.Drawing.Size(423, 22)
            Me.MetroLabel3.TabIndex = 19
            Me.MetroLabel3.Text = "Temporary Folder"
            Me.MetroLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'TemporaryFolderTextBox
            '
            Me.TemporaryFolderTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            '
            '
            '
            Me.TemporaryFolderTextBox.CustomButton.Image = Nothing
            Me.TemporaryFolderTextBox.CustomButton.Location = New System.Drawing.Point(470, 1)
            Me.TemporaryFolderTextBox.CustomButton.Name = ""
            Me.TemporaryFolderTextBox.CustomButton.Size = New System.Drawing.Size(23, 23)
            Me.TemporaryFolderTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
            Me.TemporaryFolderTextBox.CustomButton.TabIndex = 1
            Me.TemporaryFolderTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
            Me.TemporaryFolderTextBox.CustomButton.UseSelectable = True
            Me.TemporaryFolderTextBox.CustomButton.Visible = False
            Me.TemporaryFolderTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium
            Me.TemporaryFolderTextBox.Lines = New String(-1) {}
            Me.TemporaryFolderTextBox.Location = New System.Drawing.Point(6, 90)
            Me.TemporaryFolderTextBox.MaxLength = 32767
            Me.TemporaryFolderTextBox.Name = "TemporaryFolderTextBox"
            Me.TemporaryFolderTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
            Me.TemporaryFolderTextBox.ReadOnly = True
            Me.TemporaryFolderTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
            Me.TemporaryFolderTextBox.SelectedText = ""
            Me.TemporaryFolderTextBox.SelectionLength = 0
            Me.TemporaryFolderTextBox.SelectionStart = 0
            Me.TemporaryFolderTextBox.ShortcutsEnabled = True
            Me.TemporaryFolderTextBox.Size = New System.Drawing.Size(494, 25)
            Me.TemporaryFolderTextBox.TabIndex = 20
            Me.TemporaryFolderTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.TemporaryFolderTextBox.UseSelectable = True
            Me.TemporaryFolderTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
            Me.TemporaryFolderTextBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
            '
            'DownloadModeDropdown
            '
            Me.DownloadModeDropdown.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.DownloadModeDropdown.DropDownHeight = 250
            Me.DownloadModeDropdown.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DownloadModeDropdown.FormattingEnabled = True
            Me.DownloadModeDropdown.IntegralHeight = False
            Me.DownloadModeDropdown.ItemHeight = 23
            Me.DownloadModeDropdown.Items.AddRange(New Object() {"Default - ffmpeg", "Hybrid Mode", "Hybrid Mode - keep cache"})
            Me.DownloadModeDropdown.Location = New System.Drawing.Point(140, 21)
            Me.DownloadModeDropdown.Name = "DownloadModeDropdown"
            Me.DownloadModeDropdown.Size = New System.Drawing.Size(225, 29)
            Me.DownloadModeDropdown.TabIndex = 18
            Me.DownloadModeDropdown.UseSelectable = True
            '
            'NamingTabPage
            '
            Me.NamingTabPage.AutoScroll = True
            Me.NamingTabPage.Controls.Add(Me.GroupBox3)
            Me.NamingTabPage.Controls.Add(Me.FilenameExtrasGroupBox)
            Me.NamingTabPage.Controls.Add(Me.GroupBox12)
            Me.NamingTabPage.HorizontalScrollbar = True
            Me.NamingTabPage.HorizontalScrollbarBarColor = True
            Me.NamingTabPage.HorizontalScrollbarHighlightOnWheel = False
            Me.NamingTabPage.HorizontalScrollbarSize = 10
            Me.NamingTabPage.Location = New System.Drawing.Point(4, 44)
            Me.NamingTabPage.Name = "NamingTabPage"
            Me.NamingTabPage.Padding = New System.Windows.Forms.Padding(5)
            Me.NamingTabPage.Size = New System.Drawing.Size(520, 541)
            Me.NamingTabPage.TabIndex = 8
            Me.NamingTabPage.Text = "Naming"
            Me.NamingTabPage.VerticalScrollbar = True
            Me.NamingTabPage.VerticalScrollbarBarColor = True
            Me.NamingTabPage.VerticalScrollbarHighlightOnWheel = False
            Me.NamingTabPage.VerticalScrollbarSize = 10
            '
            'GroupBox3
            '
            Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox3.Controls.Add(Me.IncludeLanguageNameCheckBox)
            Me.GroupBox3.Controls.Add(Me.SubLanguageNamingComboBox)
            Me.GroupBox3.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.GroupBox3.ForeColor = System.Drawing.Color.Black
            Me.GroupBox3.Location = New System.Drawing.Point(5, 339)
            Me.GroupBox3.Name = "GroupBox3"
            Me.GroupBox3.Size = New System.Drawing.Size(510, 129)
            Me.GroupBox3.TabIndex = 52
            Me.GroupBox3.TabStop = False
            Me.GroupBox3.Text = "Subtitle File naming"
            '
            'IncludeLanguageNameCheckBox
            '
            Me.IncludeLanguageNameCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.IncludeLanguageNameCheckBox.AutoSize = True
            Me.IncludeLanguageNameCheckBox.Location = New System.Drawing.Point(85, 38)
            Me.IncludeLanguageNameCheckBox.Name = "IncludeLanguageNameCheckBox"
            Me.IncludeLanguageNameCheckBox.Size = New System.Drawing.Size(341, 15)
            Me.IncludeLanguageNameCheckBox.TabIndex = 32
            Me.IncludeLanguageNameCheckBox.Text = "Include language name if there is only one subtitle language"
            Me.IncludeLanguageNameCheckBox.UseSelectable = True
            '
            'SubLanguageNamingComboBox
            '
            Me.SubLanguageNamingComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.SubLanguageNamingComboBox.DropDownHeight = 250
            Me.SubLanguageNamingComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.SubLanguageNamingComboBox.FormattingEnabled = True
            Me.SubLanguageNamingComboBox.IntegralHeight = False
            Me.SubLanguageNamingComboBox.ItemHeight = 23
            Me.SubLanguageNamingComboBox.Items.AddRange(New Object() {"Crunchyroll language names", "ISO639-2 language codes", "Crunchyroll + ISO639-2 language codes"})
            Me.SubLanguageNamingComboBox.Location = New System.Drawing.Point(97, 80)
            Me.SubLanguageNamingComboBox.Name = "SubLanguageNamingComboBox"
            Me.SubLanguageNamingComboBox.Size = New System.Drawing.Size(326, 29)
            Me.SubLanguageNamingComboBox.TabIndex = 31
            Me.SubLanguageNamingComboBox.UseSelectable = True
            '
            'FilenameExtrasGroupBox
            '
            Me.FilenameExtrasGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FilenameExtrasGroupBox.BackColor = System.Drawing.Color.Transparent
            Me.FilenameExtrasGroupBox.Controls.Add(Me.MetroLabel11)
            Me.FilenameExtrasGroupBox.Controls.Add(Me.MetroLabel10)
            Me.FilenameExtrasGroupBox.Controls.Add(Me.LeadingZerosComboBox)
            Me.FilenameExtrasGroupBox.Controls.Add(Me.SeasonNumberBehaviorComboBox)
            Me.FilenameExtrasGroupBox.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.FilenameExtrasGroupBox.ForeColor = System.Drawing.Color.Black
            Me.FilenameExtrasGroupBox.Location = New System.Drawing.Point(5, 246)
            Me.FilenameExtrasGroupBox.Name = "FilenameExtrasGroupBox"
            Me.FilenameExtrasGroupBox.Size = New System.Drawing.Size(510, 87)
            Me.FilenameExtrasGroupBox.TabIndex = 22
            Me.FilenameExtrasGroupBox.TabStop = False
            Me.FilenameExtrasGroupBox.Text = "Filename Extras"
            '
            'LeadingZerosComboBox
            '
            Me.LeadingZerosComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.LeadingZerosComboBox.DropDownHeight = 250
            Me.LeadingZerosComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LeadingZerosComboBox.FormattingEnabled = True
            Me.LeadingZerosComboBox.IntegralHeight = False
            Me.LeadingZerosComboBox.ItemHeight = 23
            Me.LeadingZerosComboBox.Items.AddRange(New Object() {"1", "01", "001", "0001"})
            Me.LeadingZerosComboBox.Location = New System.Drawing.Point(279, 40)
            Me.LeadingZerosComboBox.Name = "LeadingZerosComboBox"
            Me.LeadingZerosComboBox.Size = New System.Drawing.Size(225, 29)
            Me.LeadingZerosComboBox.TabIndex = 20
            Me.LeadingZerosComboBox.UseSelectable = True
            '
            'SeasonNumberBehaviorComboBox
            '
            Me.SeasonNumberBehaviorComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.SeasonNumberBehaviorComboBox.DropDownHeight = 250
            Me.SeasonNumberBehaviorComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.SeasonNumberBehaviorComboBox.FormattingEnabled = True
            Me.SeasonNumberBehaviorComboBox.IntegralHeight = False
            Me.SeasonNumberBehaviorComboBox.ItemHeight = 23
            Me.SeasonNumberBehaviorComboBox.Items.AddRange(New Object() {"[Default] use season numbers", "ignore Season 1", "ignore all season numbers"})
            Me.SeasonNumberBehaviorComboBox.Location = New System.Drawing.Point(6, 40)
            Me.SeasonNumberBehaviorComboBox.Name = "SeasonNumberBehaviorComboBox"
            Me.SeasonNumberBehaviorComboBox.Size = New System.Drawing.Size(225, 29)
            Me.SeasonNumberBehaviorComboBox.TabIndex = 40
            Me.SeasonNumberBehaviorComboBox.UseSelectable = True
            '
            'GroupBox12
            '
            Me.GroupBox12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox12.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox12.Controls.Add(Me.MetroLabel9)
            Me.GroupBox12.Controls.Add(Me.MetroLabel8)
            Me.GroupBox12.Controls.Add(Me.FilenamePreviewTextBox)
            Me.GroupBox12.Controls.Add(Me.KodiNamingTemplateButton)
            Me.GroupBox12.Controls.Add(Me.AudioLanguageTemplateButton)
            Me.GroupBox12.Controls.Add(Me.EpisodeTitleTemplateButton)
            Me.GroupBox12.Controls.Add(Me.EpisodeNumberTemplateButton)
            Me.GroupBox12.Controls.Add(Me.SeasonNumberTemplateButton)
            Me.GroupBox12.Controls.Add(Me.SeriesNameTemplateButton)
            Me.GroupBox12.Controls.Add(Me.FilenameTemplateInput)
            Me.GroupBox12.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.GroupBox12.ForeColor = System.Drawing.Color.Black
            Me.GroupBox12.Location = New System.Drawing.Point(5, 8)
            Me.GroupBox12.Name = "GroupBox12"
            Me.GroupBox12.Size = New System.Drawing.Size(510, 232)
            Me.GroupBox12.TabIndex = 21
            Me.GroupBox12.TabStop = False
            Me.GroupBox12.Text = "Filename"
            '
            'MetroLabel8
            '
            Me.MetroLabel8.AutoSize = True
            Me.MetroLabel8.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.MetroLabel8.Location = New System.Drawing.Point(6, 151)
            Me.MetroLabel8.Name = "MetroLabel8"
            Me.MetroLabel8.Size = New System.Drawing.Size(56, 19)
            Me.MetroLabel8.TabIndex = 40
            Me.MetroLabel8.Text = "Preview"
            Me.ToolTip1.SetToolTip(Me.MetroLabel8, "See how a file could be named with the current naming settings")
            '
            'FilenamePreviewTextBox
            '
            Me.FilenamePreviewTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            '
            '
            '
            Me.FilenamePreviewTextBox.CustomButton.Image = Nothing
            Me.FilenamePreviewTextBox.CustomButton.Location = New System.Drawing.Point(470, 1)
            Me.FilenamePreviewTextBox.CustomButton.Name = ""
            Me.FilenamePreviewTextBox.CustomButton.Size = New System.Drawing.Size(27, 27)
            Me.FilenamePreviewTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
            Me.FilenamePreviewTextBox.CustomButton.TabIndex = 1
            Me.FilenamePreviewTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
            Me.FilenamePreviewTextBox.CustomButton.UseSelectable = True
            Me.FilenamePreviewTextBox.CustomButton.Visible = False
            Me.FilenamePreviewTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium
            Me.FilenamePreviewTextBox.Lines = New String(-1) {}
            Me.FilenamePreviewTextBox.Location = New System.Drawing.Point(6, 173)
            Me.FilenamePreviewTextBox.MaxLength = 32767
            Me.FilenamePreviewTextBox.Multiline = True
            Me.FilenamePreviewTextBox.Name = "FilenamePreviewTextBox"
            Me.FilenamePreviewTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
            Me.FilenamePreviewTextBox.ReadOnly = True
            Me.FilenamePreviewTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
            Me.FilenamePreviewTextBox.SelectedText = ""
            Me.FilenamePreviewTextBox.SelectionLength = 0
            Me.FilenamePreviewTextBox.SelectionStart = 0
            Me.FilenamePreviewTextBox.ShortcutsEnabled = True
            Me.FilenamePreviewTextBox.Size = New System.Drawing.Size(498, 53)
            Me.FilenamePreviewTextBox.TabIndex = 39
            Me.FilenamePreviewTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.FilenamePreviewTextBox.UseSelectable = True
            Me.FilenamePreviewTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
            Me.FilenamePreviewTextBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
            '
            'CrunchyrollTabPage
            '
            Me.CrunchyrollTabPage.AutoScroll = True
            Me.CrunchyrollTabPage.Controls.Add(Me.GroupBox20)
            Me.CrunchyrollTabPage.Controls.Add(Me.GroupBox19)
            Me.CrunchyrollTabPage.Controls.Add(Me.CrunchyrollHardsubGroupBox)
            Me.CrunchyrollTabPage.Controls.Add(Me.CrunchyrollSoftSubsGroupBox)
            Me.CrunchyrollTabPage.HorizontalScrollbar = True
            Me.CrunchyrollTabPage.HorizontalScrollbarBarColor = True
            Me.CrunchyrollTabPage.HorizontalScrollbarHighlightOnWheel = False
            Me.CrunchyrollTabPage.HorizontalScrollbarSize = 10
            Me.CrunchyrollTabPage.Location = New System.Drawing.Point(4, 44)
            Me.CrunchyrollTabPage.Name = "CrunchyrollTabPage"
            Me.CrunchyrollTabPage.Padding = New System.Windows.Forms.Padding(5)
            Me.CrunchyrollTabPage.Size = New System.Drawing.Size(520, 541)
            Me.CrunchyrollTabPage.TabIndex = 7
            Me.CrunchyrollTabPage.Text = "Crunchyroll"
            Me.CrunchyrollTabPage.VerticalScrollbar = True
            Me.CrunchyrollTabPage.VerticalScrollbarBarColor = True
            Me.CrunchyrollTabPage.VerticalScrollbarHighlightOnWheel = False
            Me.CrunchyrollTabPage.VerticalScrollbarSize = 10
            '
            'GroupBox20
            '
            Me.GroupBox20.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox20.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox20.Controls.Add(Me.CrunchyrollChaptersCheckBox)
            Me.GroupBox20.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.GroupBox20.ForeColor = System.Drawing.Color.Black
            Me.GroupBox20.Location = New System.Drawing.Point(5, 426)
            Me.GroupBox20.Name = "GroupBox20"
            Me.GroupBox20.Size = New System.Drawing.Size(510, 65)
            Me.GroupBox20.TabIndex = 34
            Me.GroupBox20.TabStop = False
            Me.GroupBox20.Text = "Chapters"
            '
            'CrunchyrollChaptersCheckBox
            '
            Me.CrunchyrollChaptersCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.CrunchyrollChaptersCheckBox.AutoSize = True
            Me.CrunchyrollChaptersCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
            Me.CrunchyrollChaptersCheckBox.Location = New System.Drawing.Point(183, 23)
            Me.CrunchyrollChaptersCheckBox.Name = "CrunchyrollChaptersCheckBox"
            Me.CrunchyrollChaptersCheckBox.Size = New System.Drawing.Size(145, 19)
            Me.CrunchyrollChaptersCheckBox.TabIndex = 5
            Me.CrunchyrollChaptersCheckBox.Text = "enable CR Chapters"
            Me.CrunchyrollChaptersCheckBox.UseSelectable = True
            '
            'GroupBox19
            '
            Me.GroupBox19.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox19.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox19.Controls.Add(Me.CrunchyrollAudioLanguageComboBox)
            Me.GroupBox19.Controls.Add(Me.CrunchyrollAcceptHardsubsCheckBox)
            Me.GroupBox19.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.GroupBox19.ForeColor = System.Drawing.Color.Black
            Me.GroupBox19.Location = New System.Drawing.Point(5, 8)
            Me.GroupBox19.Name = "GroupBox19"
            Me.GroupBox19.Size = New System.Drawing.Size(510, 100)
            Me.GroupBox19.TabIndex = 33
            Me.GroupBox19.TabStop = False
            Me.GroupBox19.Text = "Dubbed"
            '
            'CrunchyrollAudioLanguageComboBox
            '
            Me.CrunchyrollAudioLanguageComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.CrunchyrollAudioLanguageComboBox.DropDownHeight = 275
            Me.CrunchyrollAudioLanguageComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CrunchyrollAudioLanguageComboBox.FormattingEnabled = True
            Me.CrunchyrollAudioLanguageComboBox.IntegralHeight = False
            Me.CrunchyrollAudioLanguageComboBox.ItemHeight = 23
            Me.CrunchyrollAudioLanguageComboBox.Location = New System.Drawing.Point(95, 55)
            Me.CrunchyrollAudioLanguageComboBox.Name = "CrunchyrollAudioLanguageComboBox"
            Me.CrunchyrollAudioLanguageComboBox.Size = New System.Drawing.Size(320, 29)
            Me.CrunchyrollAudioLanguageComboBox.TabIndex = 21
            Me.CrunchyrollAudioLanguageComboBox.UseSelectable = True
            '
            'CrunchyrollAcceptHardsubsCheckBox
            '
            Me.CrunchyrollAcceptHardsubsCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.CrunchyrollAcceptHardsubsCheckBox.BackColor = System.Drawing.Color.Transparent
            Me.CrunchyrollAcceptHardsubsCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
            Me.CrunchyrollAcceptHardsubsCheckBox.Location = New System.Drawing.Point(95, 21)
            Me.CrunchyrollAcceptHardsubsCheckBox.Name = "CrunchyrollAcceptHardsubsCheckBox"
            Me.CrunchyrollAcceptHardsubsCheckBox.Size = New System.Drawing.Size(320, 28)
            Me.CrunchyrollAcceptHardsubsCheckBox.TabIndex = 5
            Me.CrunchyrollAcceptHardsubsCheckBox.Text = "Accept hardsubs for dubbed shows"
            Me.CrunchyrollAcceptHardsubsCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.CrunchyrollAcceptHardsubsCheckBox.UseCustomBackColor = True
            Me.CrunchyrollAcceptHardsubsCheckBox.UseSelectable = True
            '
            'FunimationTabPage
            '
            Me.FunimationTabPage.AutoScroll = True
            Me.FunimationTabPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
            Me.FunimationTabPage.Controls.Add(Me.GroupBox10)
            Me.FunimationTabPage.Controls.Add(Me.GroupBox7)
            Me.FunimationTabPage.Controls.Add(Me.GroupBox9)
            Me.FunimationTabPage.HorizontalScrollbar = True
            Me.FunimationTabPage.HorizontalScrollbarBarColor = True
            Me.FunimationTabPage.HorizontalScrollbarHighlightOnWheel = False
            Me.FunimationTabPage.HorizontalScrollbarSize = 10
            Me.FunimationTabPage.Location = New System.Drawing.Point(4, 44)
            Me.FunimationTabPage.Name = "FunimationTabPage"
            Me.FunimationTabPage.Padding = New System.Windows.Forms.Padding(5)
            Me.FunimationTabPage.Size = New System.Drawing.Size(520, 541)
            Me.FunimationTabPage.TabIndex = 4
            Me.FunimationTabPage.Text = " Funimation"
            Me.FunimationTabPage.VerticalScrollbar = True
            Me.FunimationTabPage.VerticalScrollbarBarColor = True
            Me.FunimationTabPage.VerticalScrollbarHighlightOnWheel = False
            Me.FunimationTabPage.VerticalScrollbarSize = 10
            Me.FunimationTabPage.Visible = False
            '
            'GroupBox10
            '
            Me.GroupBox10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox10.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox10.Controls.Add(Me.FunimationDubComboBox)
            Me.GroupBox10.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.GroupBox10.ForeColor = System.Drawing.Color.Black
            Me.GroupBox10.Location = New System.Drawing.Point(2, 8)
            Me.GroupBox10.Name = "GroupBox10"
            Me.GroupBox10.Size = New System.Drawing.Size(516, 69)
            Me.GroupBox10.TabIndex = 80
            Me.GroupBox10.TabStop = False
            Me.GroupBox10.Text = "Funimation Dub"
            '
            'FunimationDubComboBox
            '
            Me.FunimationDubComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.FunimationDubComboBox.DropDownHeight = 250
            Me.FunimationDubComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FunimationDubComboBox.FormattingEnabled = True
            Me.FunimationDubComboBox.IntegralHeight = False
            Me.FunimationDubComboBox.ItemHeight = 23
            Me.FunimationDubComboBox.Location = New System.Drawing.Point(95, 30)
            Me.FunimationDubComboBox.Name = "FunimationDubComboBox"
            Me.FunimationDubComboBox.Size = New System.Drawing.Size(326, 29)
            Me.FunimationDubComboBox.TabIndex = 40
            Me.FunimationDubComboBox.UseSelectable = True
            '
            'GroupBox7
            '
            Me.GroupBox7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox7.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox7.Controls.Add(Me.FunimationHardSubComboBox)
            Me.GroupBox7.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.GroupBox7.ForeColor = System.Drawing.Color.Black
            Me.GroupBox7.Location = New System.Drawing.Point(2, 358)
            Me.GroupBox7.Name = "GroupBox7"
            Me.GroupBox7.Size = New System.Drawing.Size(516, 69)
            Me.GroupBox7.TabIndex = 40
            Me.GroupBox7.TabStop = False
            Me.GroupBox7.Text = "Hard Subtitle (post-processed)"
            '
            'FunimationHardSubComboBox
            '
            Me.FunimationHardSubComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.FunimationHardSubComboBox.DropDownHeight = 250
            Me.FunimationHardSubComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FunimationHardSubComboBox.FormattingEnabled = True
            Me.FunimationHardSubComboBox.IntegralHeight = False
            Me.FunimationHardSubComboBox.ItemHeight = 23
            Me.FunimationHardSubComboBox.Items.AddRange(New Object() {"Disabled", "English", "Español (LA)", "Português (Brasil)"})
            Me.FunimationHardSubComboBox.Location = New System.Drawing.Point(95, 30)
            Me.FunimationHardSubComboBox.Name = "FunimationHardSubComboBox"
            Me.FunimationHardSubComboBox.Size = New System.Drawing.Size(326, 29)
            Me.FunimationHardSubComboBox.TabIndex = 32
            Me.FunimationHardSubComboBox.UseSelectable = True
            '
            'GroupBox9
            '
            Me.GroupBox9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox9.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox9.Controls.Add(Me.GroupBox13)
            Me.GroupBox9.Controls.Add(Me.GroupBox11)
            Me.GroupBox9.Controls.Add(Me.GroupBox8)
            Me.GroupBox9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GroupBox9.Location = New System.Drawing.Point(2, 77)
            Me.GroupBox9.Name = "GroupBox9"
            Me.GroupBox9.Size = New System.Drawing.Size(516, 275)
            Me.GroupBox9.TabIndex = 50
            Me.GroupBox9.TabStop = False
            Me.GroupBox9.Text = "Soft-Subtitle"
            '
            'GroupBox13
            '
            Me.GroupBox13.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox13.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox13.Controls.Add(Me.FunimationDefaultSubComboBox)
            Me.GroupBox13.Font = New System.Drawing.Font("Arial", 9.75!)
            Me.GroupBox13.ForeColor = System.Drawing.Color.Black
            Me.GroupBox13.Location = New System.Drawing.Point(6, 180)
            Me.GroupBox13.Name = "GroupBox13"
            Me.GroupBox13.Size = New System.Drawing.Size(504, 82)
            Me.GroupBox13.TabIndex = 70
            Me.GroupBox13.TabStop = False
            Me.GroupBox13.Text = "Default Subtitle"
            '
            'FunimationDefaultSubComboBox
            '
            Me.FunimationDefaultSubComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.FunimationDefaultSubComboBox.DropDownHeight = 250
            Me.FunimationDefaultSubComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FunimationDefaultSubComboBox.FormattingEnabled = True
            Me.FunimationDefaultSubComboBox.IntegralHeight = False
            Me.FunimationDefaultSubComboBox.ItemHeight = 23
            Me.FunimationDefaultSubComboBox.Items.AddRange(New Object() {"[Disabled]"})
            Me.FunimationDefaultSubComboBox.Location = New System.Drawing.Point(89, 30)
            Me.FunimationDefaultSubComboBox.Name = "FunimationDefaultSubComboBox"
            Me.FunimationDefaultSubComboBox.Size = New System.Drawing.Size(326, 29)
            Me.FunimationDefaultSubComboBox.TabIndex = 39
            Me.FunimationDefaultSubComboBox.UseSelectable = True
            '
            'GroupBox11
            '
            Me.GroupBox11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox11.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox11.Controls.Add(Me.FunimationSubSrtCheckBox)
            Me.GroupBox11.Controls.Add(Me.FunimationSubVttCheckBox)
            Me.GroupBox11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GroupBox11.Location = New System.Drawing.Point(6, 100)
            Me.GroupBox11.Name = "GroupBox11"
            Me.GroupBox11.Size = New System.Drawing.Size(504, 75)
            Me.GroupBox11.TabIndex = 60
            Me.GroupBox11.TabStop = False
            Me.GroupBox11.Text = "Format"
            '
            'FunimationSubSrtCheckBox
            '
            Me.FunimationSubSrtCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.FunimationSubSrtCheckBox.AutoSize = True
            Me.FunimationSubSrtCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
            Me.FunimationSubSrtCheckBox.ForeColor = System.Drawing.Color.Black
            Me.FunimationSubSrtCheckBox.Location = New System.Drawing.Point(170, 28)
            Me.FunimationSubSrtCheckBox.Name = "FunimationSubSrtCheckBox"
            Me.FunimationSubSrtCheckBox.Size = New System.Drawing.Size(41, 19)
            Me.FunimationSubSrtCheckBox.TabIndex = 36
            Me.FunimationSubSrtCheckBox.Text = "srt"
            Me.FunimationSubSrtCheckBox.UseSelectable = True
            '
            'FunimationSubVttCheckBox
            '
            Me.FunimationSubVttCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.FunimationSubVttCheckBox.AutoSize = True
            Me.FunimationSubVttCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
            Me.FunimationSubVttCheckBox.ForeColor = System.Drawing.Color.Black
            Me.FunimationSubVttCheckBox.Location = New System.Drawing.Point(292, 28)
            Me.FunimationSubVttCheckBox.Name = "FunimationSubVttCheckBox"
            Me.FunimationSubVttCheckBox.Size = New System.Drawing.Size(42, 19)
            Me.FunimationSubVttCheckBox.TabIndex = 37
            Me.FunimationSubVttCheckBox.Text = "vtt"
            Me.FunimationSubVttCheckBox.UseSelectable = True
            '
            'GroupBox8
            '
            Me.GroupBox8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox8.BackColor = System.Drawing.Color.Transparent
            Me.GroupBox8.Controls.Add(Me.FunimationEnglishCheckBox)
            Me.GroupBox8.Controls.Add(Me.FunimationSpanishCheckBox)
            Me.GroupBox8.Controls.Add(Me.FunimationPortugueseCheckBox)
            Me.GroupBox8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GroupBox8.Location = New System.Drawing.Point(6, 21)
            Me.GroupBox8.Name = "GroupBox8"
            Me.GroupBox8.Size = New System.Drawing.Size(504, 75)
            Me.GroupBox8.TabIndex = 61
            Me.GroupBox8.TabStop = False
            Me.GroupBox8.Text = "Language"
            '
            'FunimationEnglishCheckBox
            '
            Me.FunimationEnglishCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.FunimationEnglishCheckBox.AutoSize = True
            Me.FunimationEnglishCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
            Me.FunimationEnglishCheckBox.ForeColor = System.Drawing.Color.Black
            Me.FunimationEnglishCheckBox.Location = New System.Drawing.Point(69, 28)
            Me.FunimationEnglishCheckBox.Name = "FunimationEnglishCheckBox"
            Me.FunimationEnglishCheckBox.Size = New System.Drawing.Size(68, 19)
            Me.FunimationEnglishCheckBox.TabIndex = 33
            Me.FunimationEnglishCheckBox.Text = "English"
            Me.FunimationEnglishCheckBox.UseSelectable = True
            '
            'FunimationSpanishCheckBox
            '
            Me.FunimationSpanishCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.FunimationSpanishCheckBox.AutoSize = True
            Me.FunimationSpanishCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
            Me.FunimationSpanishCheckBox.ForeColor = System.Drawing.Color.Black
            Me.FunimationSpanishCheckBox.Location = New System.Drawing.Point(183, 28)
            Me.FunimationSpanishCheckBox.Name = "FunimationSpanishCheckBox"
            Me.FunimationSpanishCheckBox.Size = New System.Drawing.Size(100, 19)
            Me.FunimationSpanishCheckBox.TabIndex = 34
            Me.FunimationSpanishCheckBox.Text = "Español (LA)"
            Me.FunimationSpanishCheckBox.UseSelectable = True
            '
            'FunimationPortugueseCheckBox
            '
            Me.FunimationPortugueseCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.FunimationPortugueseCheckBox.AutoSize = True
            Me.FunimationPortugueseCheckBox.FontSize = MetroFramework.MetroCheckBoxSize.Medium
            Me.FunimationPortugueseCheckBox.ForeColor = System.Drawing.Color.Black
            Me.FunimationPortugueseCheckBox.Location = New System.Drawing.Point(305, 28)
            Me.FunimationPortugueseCheckBox.Name = "FunimationPortugueseCheckBox"
            Me.FunimationPortugueseCheckBox.Size = New System.Drawing.Size(131, 19)
            Me.FunimationPortugueseCheckBox.TabIndex = 35
            Me.FunimationPortugueseCheckBox.Text = "Português (Brasil)"
            Me.FunimationPortugueseCheckBox.UseSelectable = True
            '
            'AboutTabPage
            '
            Me.AboutTabPage.AutoScroll = True
            Me.AboutTabPage.Controls.Add(Me.Label8)
            Me.AboutTabPage.Controls.Add(Me.LastVersion)
            Me.AboutTabPage.Controls.Add(Me.MetroFrameworkLabel)
            Me.AboutTabPage.Controls.Add(Me.Label5)
            Me.AboutTabPage.Controls.Add(Me.CurrentVersionLabel)
            Me.AboutTabPage.Controls.Add(Me.WebviewLabel)
            Me.AboutTabPage.Controls.Add(Me.FfmpegLabel)
            Me.AboutTabPage.Controls.Add(Me.PictureBox7)
            Me.AboutTabPage.Controls.Add(Me.Label4)
            Me.AboutTabPage.HorizontalScrollbar = True
            Me.AboutTabPage.HorizontalScrollbarBarColor = True
            Me.AboutTabPage.HorizontalScrollbarHighlightOnWheel = False
            Me.AboutTabPage.HorizontalScrollbarSize = 10
            Me.AboutTabPage.Location = New System.Drawing.Point(4, 35)
            Me.AboutTabPage.Name = "AboutTabPage"
            Me.AboutTabPage.Padding = New System.Windows.Forms.Padding(5)
            Me.AboutTabPage.Size = New System.Drawing.Size(520, 550)
            Me.AboutTabPage.TabIndex = 9
            Me.AboutTabPage.Text = "About"
            Me.AboutTabPage.VerticalScrollbar = True
            Me.AboutTabPage.VerticalScrollbarBarColor = True
            Me.AboutTabPage.VerticalScrollbarHighlightOnWheel = False
            Me.AboutTabPage.VerticalScrollbarSize = 10
            '
            'Label8
            '
            Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.Label8.BackColor = System.Drawing.Color.Transparent
            Me.Label8.FontSize = MetroFramework.MetroLabelSize.Tall
            Me.Label8.ForeColor = System.Drawing.Color.Black
            Me.Label8.Location = New System.Drawing.Point(44, 450)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(100, 30)
            Me.Label8.TabIndex = 44
            Me.Label8.Text = "libraries: "
            Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'LastVersion
            '
            Me.LastVersion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LastVersion.BackColor = System.Drawing.Color.Transparent
            Me.LastVersion.FontSize = MetroFramework.MetroLabelSize.Tall
            Me.LastVersion.ForeColor = System.Drawing.Color.Black
            Me.LastVersion.Location = New System.Drawing.Point(19, 227)
            Me.LastVersion.Name = "LastVersion"
            Me.LastVersion.Size = New System.Drawing.Size(483, 45)
            Me.LastVersion.TabIndex = 48
            Me.LastVersion.Text = "Latest release: v3.7.2"
            Me.LastVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'MetroFrameworkLabel
            '
            Me.MetroFrameworkLabel.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.MetroFrameworkLabel.BackColor = System.Drawing.Color.Transparent
            Me.MetroFrameworkLabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.MetroFrameworkLabel.FontSize = MetroFramework.MetroLabelSize.Tall
            Me.MetroFrameworkLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(175, Byte), Integer))
            Me.MetroFrameworkLabel.Location = New System.Drawing.Point(147, 450)
            Me.MetroFrameworkLabel.Name = "MetroFrameworkLabel"
            Me.MetroFrameworkLabel.Size = New System.Drawing.Size(144, 30)
            Me.MetroFrameworkLabel.TabIndex = 47
            Me.MetroFrameworkLabel.Text = "MetroFramework"
            Me.MetroFrameworkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Label5
            '
            Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label5.BackColor = System.Drawing.Color.Transparent
            Me.Label5.FontSize = MetroFramework.MetroLabelSize.Tall
            Me.Label5.ForeColor = System.Drawing.Color.Black
            Me.Label5.Location = New System.Drawing.Point(19, 317)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(483, 45)
            Me.Label5.TabIndex = 38
            Me.Label5.Text = "Created by hama3254"
            Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'CurrentVersionLabel
            '
            Me.CurrentVersionLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CurrentVersionLabel.BackColor = System.Drawing.Color.Transparent
            Me.CurrentVersionLabel.FontSize = MetroFramework.MetroLabelSize.Tall
            Me.CurrentVersionLabel.ForeColor = System.Drawing.Color.Black
            Me.CurrentVersionLabel.Location = New System.Drawing.Point(19, 272)
            Me.CurrentVersionLabel.Name = "CurrentVersionLabel"
            Me.CurrentVersionLabel.Size = New System.Drawing.Size(483, 45)
            Me.CurrentVersionLabel.TabIndex = 37
            Me.CurrentVersionLabel.Text = "You have:"
            Me.CurrentVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'WebviewLabel
            '
            Me.WebviewLabel.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.WebviewLabel.BackColor = System.Drawing.Color.Transparent
            Me.WebviewLabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.WebviewLabel.FontSize = MetroFramework.MetroLabelSize.Tall
            Me.WebviewLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(175, Byte), Integer))
            Me.WebviewLabel.Location = New System.Drawing.Point(294, 450)
            Me.WebviewLabel.Name = "WebviewLabel"
            Me.WebviewLabel.Size = New System.Drawing.Size(100, 30)
            Me.WebviewLabel.TabIndex = 46
            Me.WebviewLabel.Text = "WebView2"
            Me.WebviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'FfmpegLabel
            '
            Me.FfmpegLabel.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.FfmpegLabel.BackColor = System.Drawing.Color.Transparent
            Me.FfmpegLabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.FfmpegLabel.FontSize = MetroFramework.MetroLabelSize.Tall
            Me.FfmpegLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(175, Byte), Integer))
            Me.FfmpegLabel.Location = New System.Drawing.Point(397, 450)
            Me.FfmpegLabel.Name = "FfmpegLabel"
            Me.FfmpegLabel.Size = New System.Drawing.Size(80, 30)
            Me.FfmpegLabel.TabIndex = 45
            Me.FfmpegLabel.Text = "ffmpeg"
            Me.FfmpegLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'PictureBox7
            '
            Me.PictureBox7.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.PictureBox7.BackColor = System.Drawing.Color.Transparent
            Me.PictureBox7.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.about_icon
            Me.PictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
            Me.PictureBox7.Location = New System.Drawing.Point(14, 70)
            Me.PictureBox7.Name = "PictureBox7"
            Me.PictureBox7.Size = New System.Drawing.Size(493, 137)
            Me.PictureBox7.TabIndex = 43
            Me.PictureBox7.TabStop = False
            '
            'Label4
            '
            Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.Label4.BackColor = System.Drawing.Color.Transparent
            Me.Label4.FontSize = MetroFramework.MetroLabelSize.Tall
            Me.Label4.ForeColor = System.Drawing.Color.Black
            Me.Label4.Location = New System.Drawing.Point(60, 22)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(401, 45)
            Me.Label4.TabIndex = 40
            Me.Label4.Text = "Crunchyroll Downloader"
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'BackgroundWorker1
            '
            '
            'Btn_Save
            '
            Me.Btn_Save.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            Me.Btn_Save.BackColor = System.Drawing.Color.Transparent
            Me.Btn_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
            Me.Btn_Save.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Btn_Save.FlatAppearance.BorderSize = 0
            Me.Btn_Save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.Btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.Btn_Save.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.crdSettings_Button_SafeExit
            Me.Btn_Save.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.Btn_Save.Location = New System.Drawing.Point(110, 670)
            Me.Btn_Save.Name = "Btn_Save"
            Me.Btn_Save.Size = New System.Drawing.Size(355, 30)
            Me.Btn_Save.TabIndex = 9
            Me.Btn_Save.UseVisualStyleBackColor = False
            '
            'MetroStyleManager1
            '
            Me.MetroStyleManager1.Owner = Me
            Me.MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Orange
            '
            'MetroLabel10
            '
            Me.MetroLabel10.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.MetroLabel10.AutoSize = True
            Me.MetroLabel10.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.MetroLabel10.Location = New System.Drawing.Point(6, 18)
            Me.MetroLabel10.Name = "MetroLabel10"
            Me.MetroLabel10.Size = New System.Drawing.Size(110, 19)
            Me.MetroLabel10.TabIndex = 41
            Me.MetroLabel10.Text = "Season numbers"
            '
            'MetroLabel11
            '
            Me.MetroLabel11.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.MetroLabel11.AutoSize = True
            Me.MetroLabel11.FontWeight = MetroFramework.MetroLabelWeight.Regular
            Me.MetroLabel11.Location = New System.Drawing.Point(279, 18)
            Me.MetroLabel11.Name = "MetroLabel11"
            Me.MetroLabel11.Size = New System.Drawing.Size(93, 19)
            Me.MetroLabel11.TabIndex = 42
            Me.MetroLabel11.Text = "Zero-padding"
            '
            'SettingsDialog
            '
            Me.ApplyImageInvert = True
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle
            Me.ClientSize = New System.Drawing.Size(574, 723)
            Me.Controls.Add(Me.Btn_Save)
            Me.Controls.Add(Me.TabControl)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ForeColor = System.Drawing.Color.Black
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "SettingsDialog"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Style = MetroFramework.MetroColorStyle.Orange
            Me.Text = " Settings"
            Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
            Me.CrunchyrollSoftSubsGroupBox.ResumeLayout(False)
            Me.CrunchyrollSoftSubsGroupBox.PerformLayout()
            Me.CrunchyrollHardsubGroupBox.ResumeLayout(False)
            Me.BrowserSettingsGroupBox.ResumeLayout(False)
            Me.DownloadCountGroupBox.ResumeLayout(False)
            CType(Me.SimultaneousDownloadsInput, System.ComponentModel.ISupportInitialize).EndInit()
            Me.TabControl.ResumeLayout(False)
            Me.MainTabPage.ResumeLayout(False)
            Me.SubfolderGroupBox.ResumeLayout(False)
            Me.ErrorHandlingGroupBox.ResumeLayout(False)
            Me.ErrorHandlingGroupBox.PerformLayout()
            CType(Me.ErrorLimitInput, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ServerGroupBox.ResumeLayout(False)
            Me.ServerGroupBox.PerformLayout()
            CType(Me.CustomServerPortInput, System.ComponentModel.ISupportInitialize).EndInit()
            Me.OutputTabPage.ResumeLayout(False)
            Me.QualityGroupBox.ResumeLayout(False)
            Me.QualityGroupBox.PerformLayout()
            Me.BitratePanel.ResumeLayout(False)
            Me.BitratePanel.PerformLayout()
            Me.FfmpegCommandGroupBox.ResumeLayout(False)
            Me.FfmpegCommandGroupBox.PerformLayout()
            CType(Me.BitrateNumericInput, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox4.ResumeLayout(False)
            Me.GroupBox16.ResumeLayout(False)
            Me.NamingTabPage.ResumeLayout(False)
            Me.GroupBox3.ResumeLayout(False)
            Me.GroupBox3.PerformLayout()
            Me.FilenameExtrasGroupBox.ResumeLayout(False)
            Me.FilenameExtrasGroupBox.PerformLayout()
            Me.GroupBox12.ResumeLayout(False)
            Me.GroupBox12.PerformLayout()
            Me.CrunchyrollTabPage.ResumeLayout(False)
            Me.GroupBox20.ResumeLayout(False)
            Me.GroupBox20.PerformLayout()
            Me.GroupBox19.ResumeLayout(False)
            Me.FunimationTabPage.ResumeLayout(False)
            Me.GroupBox10.ResumeLayout(False)
            Me.GroupBox7.ResumeLayout(False)
            Me.GroupBox9.ResumeLayout(False)
            Me.GroupBox13.ResumeLayout(False)
            Me.GroupBox11.ResumeLayout(False)
            Me.GroupBox11.PerformLayout()
            Me.GroupBox8.ResumeLayout(False)
            Me.GroupBox8.PerformLayout()
            Me.AboutTabPage.ResumeLayout(False)
            CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ToolTip1 As ToolTip
        Friend WithEvents ToolTip2 As ToolTip
        Friend WithEvents BrowserSettingsGroupBox As GroupBox
        Friend WithEvents DownloadCountGroupBox As GroupBox
        Friend WithEvents SimultaneousDownloadsInput As NumericUpDown
        Friend WithEvents CrunchyrollSoftSubsGroupBox As GroupBox
        Friend WithEvents CrunchyrollHardsubGroupBox As GroupBox
        Private WithEvents TabControl As MetroFramework.Controls.MetroTabControl
        Private WithEvents FunimationTabPage As MetroFramework.Controls.MetroTabPage
        Friend WithEvents PictureBox7 As PictureBox
        Friend WithEvents GroupBox7 As GroupBox
        Friend WithEvents GroupBox9 As GroupBox
        Friend WithEvents GroupBox10 As GroupBox
        Friend WithEvents GroupBox11 As GroupBox
        Friend WithEvents Button1 As Button
        Friend WithEvents Label1 As MetroFramework.Controls.MetroLabel
        Public WithEvents Label4 As MetroFramework.Controls.MetroLabel
        Public WithEvents CurrentVersionLabel As MetroFramework.Controls.MetroLabel
        Public WithEvents Label5 As MetroFramework.Controls.MetroLabel
        Friend WithEvents FunimationPortugueseCheckBox As MetroFramework.Controls.MetroCheckBox
        Friend WithEvents FunimationSpanishCheckBox As MetroFramework.Controls.MetroCheckBox
        Friend WithEvents FunimationEnglishCheckBox As MetroFramework.Controls.MetroCheckBox
        Friend WithEvents FunimationSubSrtCheckBox As MetroFramework.Controls.MetroCheckBox
        Friend WithEvents FunimationSubVttCheckBox As MetroFramework.Controls.MetroCheckBox
        Public WithEvents Label8 As MetroFramework.Controls.MetroLabel
        Public WithEvents MetroFrameworkLabel As MetroFramework.Controls.MetroLabel
        Public WithEvents WebviewLabel As MetroFramework.Controls.MetroLabel
        Public WithEvents FfmpegLabel As MetroFramework.Controls.MetroLabel
        Friend WithEvents DefaultWebsiteTextBox As MetroFramework.Controls.MetroTextBox
        Friend WithEvents CrunchyrollHardsubComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents FunimationHardSubComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents FunimationDubComboBox As MetroFramework.Controls.MetroComboBox
        Public WithEvents LastVersion As MetroFramework.Controls.MetroLabel
        Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
        Friend WithEvents GroupBox13 As GroupBox
        Friend WithEvents FunimationDefaultSubComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents CrunchyrollTabPage As MetroFramework.Controls.MetroTabPage
        Friend WithEvents Btn_Save As Button
        Friend WithEvents GroupBox8 As GroupBox
        Friend WithEvents NamingTabPage As MetroFramework.Controls.MetroTabPage
        Friend WithEvents FilenameExtrasGroupBox As GroupBox
        Friend WithEvents GroupBox12 As GroupBox
        Friend WithEvents LeadingZerosComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents GroupBox3 As GroupBox
        Friend WithEvents SubLanguageNamingComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents SeasonNumberBehaviorComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents GroupBox19 As GroupBox
        Friend WithEvents CrunchyrollAcceptHardsubsCheckBox As MetroFramework.Controls.MetroCheckBox
        Friend WithEvents FilenameTemplateInput As MetroFramework.Controls.MetroTextBox
        Friend WithEvents CrunchyrollAudioLanguageComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents GroupBox20 As GroupBox
        Friend WithEvents CrunchyrollChaptersCheckBox As MetroFramework.Controls.MetroCheckBox
        Public WithEvents CR_SoftSubDefault As MetroFramework.Controls.MetroComboBox
        Friend WithEvents IncludeLanguageNameCheckBox As MetroFramework.Controls.MetroCheckBox
        Friend WithEvents CrunchyrollSoftSubsCheckedListBox As CheckedListBox
        Friend WithEvents Label3 As Label
        Friend WithEvents Label6 As Label
        Friend WithEvents MetroStyleManager1 As MetroFramework.Components.MetroStyleManager
        Friend WithEvents StyleExtender As MetroFramework.Components.MetroStyleExtender
        Friend WithEvents AboutTabPage As MetroFramework.Controls.MetroTabPage
        Friend WithEvents MainTabPage As MetroFramework.Controls.MetroTabPage
        Friend WithEvents SubfolderGroupBox As GroupBox
        Friend WithEvents HideSubfoldersComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents ErrorHandlingGroupBox As GroupBox
        Friend WithEvents MetroLabel1 As MetroFramework.Controls.MetroLabel
        Friend WithEvents CheckBox2 As MetroFramework.Controls.MetroCheckBox
        Friend WithEvents Label2 As MetroFramework.Controls.MetroLabel
        Friend WithEvents ErrorLimitInput As NumericUpDown
        Friend WithEvents ServerGroupBox As GroupBox
        Friend WithEvents CustomServerPortInput As NumericUpDown
        Friend WithEvents ServerPortLabel As MetroFramework.Controls.MetroLabel
        Friend WithEvents IgnoreTlsCheckBox As MetroFramework.Controls.MetroCheckBox
        Friend WithEvents ServerPortInput As MetroFramework.Controls.MetroComboBox
        Friend WithEvents DarkModeCheckBox As MetroFramework.Controls.MetroCheckBox
        Friend WithEvents OutputTabPage As MetroFramework.Controls.MetroTabPage
        Friend WithEvents FfmpegCommandGroupBox As GroupBox
        Friend WithEvents FfmpegCopyCheckBox As MetroFramework.Controls.MetroCheckBox
        Friend WithEvents TargetBitrateCheckBox As MetroFramework.Controls.MetroCheckBox
        Friend WithEvents VideoCodecComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents MetroLabel6 As MetroFramework.Controls.MetroLabel
        Friend WithEvents VideoEncoderComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents MetroLabel5 As MetroFramework.Controls.MetroLabel
        Friend WithEvents FfmpegPresetComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents MetroLabel4 As MetroFramework.Controls.MetroLabel
        Friend WithEvents BitrateNumericInput As NumericUpDown
        Friend WithEvents MetroLabel2 As MetroFramework.Controls.MetroLabel
        Friend WithEvents GroupBox4 As GroupBox
        Friend WithEvents SubtitleFormatComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents VideoFormatComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents QualityGroupBox As GroupBox
        Friend WithEvents GroupBox16 As GroupBox
        Friend WithEvents MetroLabel3 As MetroFramework.Controls.MetroLabel
        Friend WithEvents TemporaryFolderTextBox As MetroFramework.Controls.MetroTextBox
        Friend WithEvents DownloadModeDropdown As MetroFramework.Controls.MetroComboBox
        Friend WithEvents ResolutionComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents LowerBitrateRadioButton As MetroFramework.Controls.MetroRadioButton
        Friend WithEvents HigherBitrateRadioButton As MetroFramework.Controls.MetroRadioButton
        Friend WithEvents LowerResolutionRadioButton As MetroFramework.Controls.MetroRadioButton
        Friend WithEvents HigherResolutionRadioButton As MetroFramework.Controls.MetroRadioButton
        Friend WithEvents BitratePanel As Panel
        Friend WithEvents MetroLabel7 As MetroFramework.Controls.MetroLabel
        Friend WithEvents KodiNamingTemplateButton As MetroFramework.Controls.MetroButton
        Friend WithEvents AudioLanguageTemplateButton As MetroFramework.Controls.MetroButton
        Friend WithEvents EpisodeTitleTemplateButton As MetroFramework.Controls.MetroButton
        Friend WithEvents EpisodeNumberTemplateButton As MetroFramework.Controls.MetroButton
        Friend WithEvents SeasonNumberTemplateButton As MetroFramework.Controls.MetroButton
        Friend WithEvents SeriesNameTemplateButton As MetroFramework.Controls.MetroButton
        Friend WithEvents FilenamePreviewTextBox As MetroFramework.Controls.MetroTextBox
        Friend WithEvents MetroLabel8 As MetroFramework.Controls.MetroLabel
        Friend WithEvents MetroLabel9 As MetroFramework.Controls.MetroLabel
        Friend WithEvents MetroLabel11 As MetroFramework.Controls.MetroLabel
        Friend WithEvents MetroLabel10 As MetroFramework.Controls.MetroLabel
    End Class
End Namespace