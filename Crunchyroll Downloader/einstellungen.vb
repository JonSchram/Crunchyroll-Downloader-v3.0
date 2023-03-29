Option Strict On

Imports Microsoft.Win32
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Threading
Imports MetroFramework.Forms
Imports MetroFramework
Imports MetroFramework.Components
Imports System.Text.RegularExpressions
Imports Crunchyroll_Downloader.settings
Imports Crunchyroll_Downloader.settings.ProgramSettings
Imports System.ComponentModel
Imports System.Reflection
Imports Microsoft.VisualBasic.Logging
Imports System.Collections.Specialized
Imports Crunchyroll_Downloader.settings.crunchyroll
Imports Crunchyroll_Downloader.settings.funimation
Imports Crunchyroll_Downloader.settings.ffmpeg.encoding
Imports Crunchyroll_Downloader.settings.ffmpeg

Public Class Einstellungen
    Inherits MetroForm

    Private Const SEASON_PREFIX_PLACEHOLDER = "[default season prefix]"
    Private Const DEFAULT_SEASON_PREFIX = "Season"

    Private Const EPISODE_PREFIX_PLACEHOLDER = "[default episode prefix]"
    Private Const DEFAULT_EPISODE_PREFIX = "Episode"

    ' Display objects for combo boxes backed by enums
    Private ReadOnly ServerPortTextList As New EnumTextList(Of ServerPortOptions)()
    Private ReadOnly SubfolderTextList As New EnumTextList(Of SubfolderDisplay)()
    Private ReadOnly DownloadModeTextList As New EnumTextList(Of DownloadModeOptions)()
    Private ReadOnly VideoFormatTextList As New EnumTextList(Of Format.MediaFormat)()
    Private ReadOnly SubtitleFormatTextList As New EnumTextList(Of Format.SubtitleMerge)()
    Private ReadOnly ValidSubtitleFormatList As EnumTextList(Of Format.SubtitleMerge).SubTextList = SubtitleFormatTextList.CreateSubList()
    Private ReadOnly SpeedPresetTextList As New EnumTextList(Of Speed)()
    Private ReadOnly CodecTextList As New EnumTextList(Of Codec)()
    Private ReadOnly EncoderHardwareTextList As New EnumTextList(Of EncoderImplementation)()
    Private ReadOnly SeasonNumberBehaviorTextlist As New EnumTextList(Of SeasonNumberBehavior)()
    Private ReadOnly SubtitleNamingTextList As New EnumTextList(Of LanguageNameMethod)()

    Private ReadOnly CrunchyrollLanguageTextList As New EnumTextList(Of CrunchyrollLanguage)
    Private ReadOnly CrunchyrollSoftSubLanguageSubList As EnumTextList(Of CrunchyrollLanguage).SubTextList = CrunchyrollLanguageTextList.CreateSubList(OrderType.PARENT_ORDER)
    Private ReadOnly CrunchyrollDefaultLanguageSubList As EnumTextList(Of CrunchyrollLanguage).SubTextList = CrunchyrollLanguageTextList.CreateSubList(OrderType.PARENT_ORDER)
    Private ReadOnly CrunchyrollHardSubLanguageSubList As EnumTextList(Of CrunchyrollLanguage).SubTextList = CrunchyrollLanguageTextList.CreateSubList(OrderType.PARENT_ORDER)
    Private ReadOnly CrunchyrollDubLanguageSubList As EnumTextList(Of CrunchyrollLanguage).SubTextList = CrunchyrollLanguageTextList.CreateSubList(OrderType.ALPHABETICAL)

    Private ReadOnly FunimationLanguageTextList As New EnumTextList(Of FunimationLanguage)()
    Private ReadOnly FunimationDefaultSubOptionsList As EnumTextList(Of FunimationLanguage).SubTextList = FunimationLanguageTextList.CreateSubList(OrderType.PARENT_ORDER)
    Private ReadOnly FunimationHardSubLanguagesList As EnumTextList(Of FunimationLanguage).SubTextList = FunimationLanguageTextList.CreateSubList(OrderType.PARENT_ORDER)
    Private ReadOnly FunimationBitrateTextList As New EnumTextList(Of BitrateSetting)()

    Dim Manager As MetroStyleManager = Main.Manager
    Dim LastVersionString As String = "v3.8-Beta"

    Private nameFormatter As FilenameFormatter
    Private uiInitializing As Boolean = False
    Private nameTemplateLoading As Boolean = False

    Private ReadOnly settings As ProgramSettings
    Private ReadOnly crSettings As CrunchyrollSettings
    Private ReadOnly funSettings As FunimationSettings

    Public Sub New()
        InitializeComponent()

        InitializeTextLists()
        InitializeUi()

        settings = ProgramSettings.GetInstance()
        crSettings = settings.Crunchyroll
        funSettings = settings.Funimation
    End Sub

    Private Sub InitializeTextLists()
        With ServerPortTextList
            .Add(ServerPortOptions.DISABLED, "add-on support disabled")
            .Add(ServerPortOptions.PORT_80, "80")
            .Add(ServerPortOptions.PORT_8080, "8080")
            .Add(ServerPortOptions.CUSTOM, "Custom port")
        End With

        With SubfolderTextList
            .Add(SubfolderDisplay.SHOW_ALL, "Show all subfolders")
            .Add(SubfolderDisplay.HIDE_ALL, "Hide all subfolders")
            .Add(SubfolderDisplay.HIDE_OLDER_THAN_1_WEEK, "Hide subfolders last accessed > 1 week ago")
            .Add(SubfolderDisplay.HIDE_OLDER_THAN_1_MONTH, "Hide subfolders last accessed > 1 month ago")
            .Add(SubfolderDisplay.HIDE_OLDER_THAN_3_MONTHS, "Hide subfolders last accessed > 3 months ago")
            .Add(SubfolderDisplay.HIDE_OLDER_THAN_6_MONTHS, "Hide subfolders last accessed > 6 months ago")
        End With

        With DownloadModeTextList
            .Add(DownloadModeOptions.FFMPEG, "Default - ffmpeg")
            .Add(DownloadModeOptions.HYBRID_MODE, "Hybrid Mode")
            .Add(DownloadModeOptions.HYBRID_MODE_KEEP_CACHE, "Hybrid Mode - keep cache")
        End With

        With VideoFormatTextList
            .Add(Format.MediaFormat.MP4, "MP4")
            .Add(Format.MediaFormat.MKV, "MKV")
            .Add(Format.MediaFormat.AAC_AUDIO_ONLY, "AAC (Audio only)")
        End With

        With SpeedPresetTextList
            .Add(Speed.NO_PRESET, "(No preset)")
            .Add(Speed.VERY_SLOW, "Very slow")
            .Add(Speed.SLOWER, "Slower")
            .Add(Speed.SLOW, "Slow")
            .Add(Speed.MEDIUM, "Medium")
            .Add(Speed.FAST, "Fast")
            .Add(Speed.FASTER, "Faster")
            .Add(Speed.VERY_FAST, "Very fast")
        End With

        With CodecTextList
            .Add(Codec.H_264, "h.264")
            .Add(Codec.H_265, "h.265")
            .Add(Codec.AV1, "AV1")
        End With

        With EncoderHardwareTextList
            .Add(EncoderImplementation.SOFTWARE, "Software")
            .Add(EncoderImplementation.NVIDIA, "Nvidia")
            .Add(EncoderImplementation.AMD, "AMD")
            .Add(EncoderImplementation.INTEL, "Intel")
        End With

        With SeasonNumberBehaviorTextlist
            .Add(SeasonNumberBehavior.USE_SEASON_NUMBERS, "Use season numbers (default)")
            .Add(SeasonNumberBehavior.IGNORE_SEASON_1, "Ignore season 1")
            .Add(SeasonNumberBehavior.IGNORE_ALL_SEASON_NUMBERS, "Ignore all season numbers")
        End With

        With SubtitleNamingTextList
            .Add(LanguageNameMethod.CRUNCHYROLL, "Crunchyroll language names")
            .Add(LanguageNameMethod.ISO639_2_CODES, "ISO639-2 language codes")
            .Add(LanguageNameMethod.CRUNCHYROLL_AND_ISO639_2_CODES, "Crunchyroll + ISO639-2 language codes")
        End With

        With CrunchyrollLanguageTextList
            .Add(CrunchyrollLanguage.NONE, "[None]")
            .Add(CrunchyrollLanguage.GERMAN_GERMANY, "Deutsch (Germany)")
            .Add(CrunchyrollLanguage.ENGLISH_US, "English (US)")
            .Add(CrunchyrollLanguage.PORTUGUESE_BRAZIL, "Português (Brazil)")
            .Add(CrunchyrollLanguage.SPANISH_LATIN_AMERICA, "Español (LA)")
            .Add(CrunchyrollLanguage.FRENCH_FRANCE, "Français (France)")
            .Add(CrunchyrollLanguage.ARABIC, "العربية (Arabic)")
            .Add(CrunchyrollLanguage.RUSSIAN, "Русский (Russian)")
            .Add(CrunchyrollLanguage.ITALIAN, "Italiano (Italian)")
            .Add(CrunchyrollLanguage.SPANISH_SPAIN, "Español (Spain)")
            .Add(CrunchyrollLanguage.JAPANESE, "日本語 (Japanese)")
        End With
        InitializeCrunchyrollLanguageList(CrunchyrollDubLanguageSubList)

        CrunchyrollHardSubLanguageSubList.AddFromParent(CrunchyrollLanguage.NONE)
        InitializeCrunchyrollLanguageList(CrunchyrollHardSubLanguageSubList)

        InitializeCrunchyrollLanguageList(CrunchyrollSoftSubLanguageSubList)

        CrunchyrollDefaultLanguageSubList.AddFromParent(CrunchyrollLanguage.NONE)

        With SubtitleFormatTextList
            .Add(Format.SubtitleMerge.DISABLED, "[merge disabled]")
            .Add(Format.SubtitleMerge.MOV_TEXT, "mov_text")
            .Add(Format.SubtitleMerge.COPY, "copy")
            .Add(Format.SubtitleMerge.SRT, "srt")
        End With

        With FunimationLanguageTextList
            .Add(FunimationLanguage.NONE, "[None]")
            .Add(FunimationLanguage.ENGLISH, "English")
            .Add(FunimationLanguage.JAPANESE, "Japanese")
            .Add(FunimationLanguage.PORTUGUESE, "Português (Brazil)")
            .Add(FunimationLanguage.SPANISH, "Spanish (Mexico)")
        End With
        FunimationDefaultSubOptionsList.AddFromParent(FunimationLanguage.NONE)

        With FunimationBitrateTextList
            .Add(BitrateSetting.HIGH, "Prefer high bitrate")
            .Add(BitrateSetting.LOW, "Prefer low bitrate")
        End With

        With FunimationHardSubLanguagesList
            .AddFromParent(FunimationLanguage.NONE)
            .AddFromParent(FunimationLanguage.ENGLISH)
            .AddFromParent(FunimationLanguage.SPANISH)
            .AddFromParent(FunimationLanguage.PORTUGUESE)
        End With
    End Sub

    Private Sub InitializeCrunchyrollLanguageList(subList As EnumTextList(Of CrunchyrollLanguage).SubTextList)
        With subList
            .AddFromParent(CrunchyrollLanguage.JAPANESE)
            .AddFromParent(CrunchyrollLanguage.SPANISH_SPAIN)
            .AddFromParent(CrunchyrollLanguage.ITALIAN)
            .AddFromParent(CrunchyrollLanguage.RUSSIAN)
            .AddFromParent(CrunchyrollLanguage.ARABIC)
            .AddFromParent(CrunchyrollLanguage.FRENCH_FRANCE)
            .AddFromParent(CrunchyrollLanguage.SPANISH_LATIN_AMERICA)
            .AddFromParent(CrunchyrollLanguage.PORTUGUESE_BRAZIL)
            .AddFromParent(CrunchyrollLanguage.ENGLISH_US)
            .AddFromParent(CrunchyrollLanguage.GERMAN_GERMANY)
        End With
    End Sub

    ''' <summary>
    ''' Populates combo boxes with elements and does everything except load settings.
    ''' </summary>
    Private Sub InitializeUi()
        uiInitializing = True
        InitializeMainTab()
        InitializeOutputTab()
        InitializeNamingTab()
        InitializeCrunchyrollTab()
        InitializeFunimationTab()

        uiInitializing = False
    End Sub

    Private Sub InitializeMainTab()
        ServerPortInput.DataSource = ServerPortTextList.GetDisplayItems()
        HideSubfoldersComboBox.DataSource = SubfolderTextList.GetDisplayItems()
    End Sub

    Private Sub InitializeOutputTab()
        DownloadModeDropdown.DataSource = DownloadModeTextList.GetDisplayItems()
        CB_Format.DataSource = VideoFormatTextList.GetDisplayItems()

        VideoCodecComboBox.DataSource = CodecTextList.GetDisplayItems()
        VideoEncoderComboBox.DataSource = EncoderHardwareTextList.GetDisplayItems()
        FfmpegPresetComboBox.DataSource = SpeedPresetTextList.GetDisplayItems()
    End Sub

    Private Sub InitializeNamingTab()
        SeasonNumberBehaviorComboBox.DataSource = SeasonNumberBehaviorTextlist.GetDisplayItems()
        SubLanguageNamingComboBox.DataSource = SubtitleNamingTextList.GetDisplayItems()
    End Sub

    Private Sub InitializeCrunchyrollTab()
        CrunchyrollAudioLanguageComboBox.DataSource = CrunchyrollDubLanguageSubList.GetDisplayItems()
        CrunchyrollHardsubComboBox.DataSource = CrunchyrollHardSubLanguageSubList.GetDisplayItems()

        ' Must set the data items manually before setting as a data source.
        ' Setting a data source refreshes the items and it tries to get the old item corresponding to a new item number.
        Dim displayItems = CrunchyrollSoftSubLanguageSubList.GetDisplayItems()
        CrunchyrollSoftSubsCheckedListBox.Items.AddRange(displayItems.ToArray)
        CrunchyrollSoftSubsCheckedListBox.DisplayMember = "EnumText"
        CrunchyrollSoftSubsCheckedListBox.DataSource = CrunchyrollSoftSubLanguageSubList.GetDisplayItems()

        CR_SoftSubDefault.DataSource = CrunchyrollDefaultLanguageSubList.GetDisplayItems()
    End Sub

    Private Sub InitializeFunimationTab()
        FunimationDubComboBox.DataSource = FunimationLanguageTextList.GetDisplayItems()
        FunimationDefaultSubComboBox.DataSource = FunimationDefaultSubOptionsList.GetDisplayItems()
        FunimationBitrateComboBox.DataSource = FunimationBitrateTextList.GetDisplayItems()
        FunimationHardSubComboBox.DataSource = FunimationHardSubLanguagesList.GetDisplayItems()
    End Sub

    Private Sub UpdateMergeFormatInput()
        UpdateMergeFormatInput(VideoFormatTextList.GetEnumForItem(CB_Format.SelectedItem))
    End Sub

    Private Sub UpdateMergeFormatInput(VideoFormat As Format.MediaFormat)
        If uiInitializing Then
            Return
        End If
        PopulateSubFormats(VideoFormat)
        CB_Merge.Items.Clear()
        CB_Merge.Items.AddRange(ValidSubtitleFormatList.GetDisplayItems().ToArray())
        CB_Merge.SelectedIndex = 0
        CB_Merge.Enabled = VideoFormat <> Format.MediaFormat.AAC_AUDIO_ONLY
    End Sub
    Private Sub PopulateSubFormats(VideoFormat As Format.MediaFormat)
        Dim supportedSubtitleFormats = Format.GetValidSubtitleFormats(VideoFormat)

        ValidSubtitleFormatList.Clear()
        For Each SubFormat In supportedSubtitleFormats
            ValidSubtitleFormatList.AddFromParent(SubFormat)
        Next
    End Sub

#Region "Loading"
    Private Sub Einstellungen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler ProgramSettings.DarkModeChanged, AddressOf HandleDarkModeChanged

        LoadSettings()

        CurrentVersionLabel.Text = "You have: v" + Application.ProductVersion.ToString '+ " WebView2_Test"

        BackgroundWorker1.RunWorkerAsync()


        Manager.Owner = Me
        Me.StyleManager = Manager

        TabControl1.SelectedIndex = 0

        Me.Location = New Point(CInt(Main.Location.X + Main.Width / 2 - Me.Width / 2), CInt(Main.Location.Y + Main.Height / 2 - Me.Height / 2))
        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception

        End Try

    End Sub

    Private Sub LoadSettings()
        nameFormatter = New FilenameFormatter(settings.FilenameFormat)

        LoadMainSettings()
        LoadOutputSettings()
        LoadNamingSettings()
        LoadCrunchyrollSettings()
        LoadFunimationSettings()

    End Sub

    Private Sub LoadMainSettings()
        SimultaneousDownloadsInput.Value = settings.SimultaneousDownloads
        DefaultWebsiteTextBox.Text = settings.DefaultWebsite

        If settings.DarkMode Then
            DarkModeCheckBox.Checked = True
            GroupBoxColor(Color.FromArgb(150, 150, 150))
            ' TODO: Set image box for dark mode directly instead of from Main
            pictureBox1.Image = Main.CloseImg
        Else
            DarkModeCheckBox.Checked = False
            GroupBoxColor(Color.FromArgb(0, 0, 0))
        End If

        LoadAddOnPort()
        IgnoreTlsCheckBox.Checked = settings.InsecureCurl
        ErrorLimitInput.Value = settings.ErrorLimit
        HideSubfoldersComboBox.SelectedItem = SubfolderTextList.Item(settings.SubfolderDisplayBehavior)
    End Sub
    Private Sub LoadAddOnPort()
        Dim addOnPort = settings.ServerPort
        Dim newOption As ServerPortOptions
        If addOnPort = 0 Then
            newOption = ServerPortOptions.DISABLED
        ElseIf addOnPort = 80 Then
            newOption = ServerPortOptions.PORT_80
            CustomServerPortInput.Value = addOnPort
        ElseIf addOnPort = 8080 Then
            newOption = ServerPortOptions.PORT_8080
            CustomServerPortInput.Value = addOnPort
        Else
            newOption = ServerPortOptions.CUSTOM
            CustomServerPortInput.Value = addOnPort
        End If

        ServerPortInput.SelectedItem = ServerPortTextList.Item(newOption)

        CustomServerPortInput.Enabled = (newOption = ServerPortOptions.CUSTOM)
    End Sub

    Private Sub LoadOutputSettings()
        DownloadModeDropdown.SelectedItem = DownloadModeTextList.Item(settings.DownloadMode)
        TemporaryFolderTextBox.Text = settings.TemporaryFolder
        UseQueueCheckbox.Checked = settings.UseDownloadQueue
        LoadResolution()

        LoadOutputFormat()
        LoadFfmpegSettings()
    End Sub

    Private Sub LoadResolution()
        Select Case settings.DownloadResolution
            Case Resolution.RESOLUTION_1080P
                A1080p.Checked = True
            Case Resolution.RESOLUTION_720P
                A720p.Checked = True
            Case Resolution.RESOLUTION_480P
                A480p.Checked = True
            Case Resolution.RESOLUTION_360P
                A360p.Checked = True
            Case Else
                AAuto.Checked = True
        End Select
    End Sub

    Public Sub LoadOutputFormat()
        Dim currentFormat As Format
        Try
            currentFormat = settings.OutputFormat
        Catch ex As Exception
            currentFormat = New Format(Format.MediaFormat.MP4, Format.SubtitleMerge.COPY)
        End Try

        CB_Format.SelectedItem = VideoFormatTextList.Item(currentFormat.GetVideoFormat())
        ' Even though setting the selected item will update the merge format combo box, we need to explicitly set it in case the value didn't change.
        UpdateMergeFormatInput(currentFormat.GetVideoFormat())
        CB_Merge.SelectedItem = SubtitleFormatTextList.Item(currentFormat.GetSubtitleFormat())
    End Sub

    Private Sub LoadFfmpegSettings()
        Dim ffmpegCommand = settings.Ffmpeg
        Dim savedEncoder = ffmpegCommand.GetSavedEncoder()

        FfmpegCopyCheckBox.Checked = ffmpegCommand.VideoCopy
        VideoCodecComboBox.SelectedItem = CodecTextList.Item(savedEncoder.VideoCodec)
        VideoEncoderComboBox.SelectedItem = EncoderHardwareTextList.Item(savedEncoder.Hardware)
        FfmpegPresetComboBox.SelectedItem = SpeedPresetTextList.Item(savedEncoder.Preset)
        TargetBitrateCheckBox.Checked = savedEncoder.UseTargetBitrate
        BitrateNumericInput.Value = savedEncoder.TargetBitrate

        UpdateFfmpegInputStates()
        UpdateFfmpegDisplay()
    End Sub

    Private Sub LoadNamingSettings()
        LoadNamingInputs()

        KodiNamingCheckBox.Checked = settings.KodiNaming

        SeasonNumberBehaviorComboBox.SelectedItem = SeasonNumberBehaviorTextlist.Item(settings.SeasonNumberNaming)
        LoadSeasonPrefix()
        LoadEpisodePrefix()

        LoadZeroPadding()
        IncludeLanguageNameCheckBox.Checked = settings.IncludeSubtitleLanguageInFirstSubtitle
        SubLanguageNamingComboBox.SelectedItem = SubtitleNamingTextList.Item(settings.SubLanguageNaming)
    End Sub
    Private Sub LoadNamingInputs()
        nameTemplateLoading = True
        Dim placeholders = nameFormatter.GetCurrentPlaceholders()

        SeriesNameCheckBox.Checked = placeholders.Contains(FilenameFormatter.TemplateItem.SERIES_NAME)
        SeasonNumberCheckBox.Checked = placeholders.Contains(FilenameFormatter.TemplateItem.SEASON_NUMBER)
        EpisodeNumberCheckBox.Checked = placeholders.Contains(FilenameFormatter.TemplateItem.EPISODE_NUMBER)
        EpisodeTitleCheckBox.Checked = placeholders.Contains(FilenameFormatter.TemplateItem.EPISODE_TITLE)
        AudioLanguageCheckBox.Checked = placeholders.Contains(FilenameFormatter.TemplateItem.AUDIO_LANGUAGE)

        FilenameTemplatePreview.Text = nameFormatter.GetTemplate()
        nameTemplateLoading = False
    End Sub
    Private Sub LoadSeasonPrefix()
        Dim seasonPrefix = settings.SeasonPrefix
        If seasonPrefix Is Nothing Or seasonPrefix = "" Or seasonPrefix = DEFAULT_SEASON_PREFIX Then
            SeasonPrefixTextBox.Text = SEASON_PREFIX_PLACEHOLDER
        Else
            SeasonPrefixTextBox.Text = seasonPrefix
        End If
    End Sub
    Private Sub LoadEpisodePrefix()
        Dim episodePrefix = settings.EpisodePrefix
        If episodePrefix Is Nothing Or episodePrefix = "" Or episodePrefix = DEFAULT_EPISODE_PREFIX Then
            EpisodePrefixTextBox.Text = EPISODE_PREFIX_PLACEHOLDER
        Else
            EpisodePrefixTextBox.Text = episodePrefix
        End If
    End Sub
    Private Sub LoadZeroPadding()
        Dim zeroPadding = settings.ZeroPaddingLength
        If zeroPadding >= 5 Then
            zeroPadding = 4
        ElseIf zeroPadding < 1 Then
            zeroPadding = 1
        End If
        LeadingZerosComboBox.SelectedIndex = zeroPadding - 1
    End Sub

    Private Sub LoadCrunchyrollSettings()
        CrunchyrollAcceptHardsubsCheckBox.Checked = crSettings.AcceptHardsubs
        CrunchyrollAudioLanguageComboBox.SelectedItem = CrunchyrollLanguageTextList.Item(crSettings.AudioLanguage)
        CrunchyrollHardsubComboBox.SelectedItem = CrunchyrollLanguageTextList.Item(crSettings.HardSubLanguage)
        CrunchyrollChaptersCheckBox.Checked = crSettings.EnableChapters
        LoadCrunchyrollSoftSubs()
    End Sub
    Private Sub LoadCrunchyrollSoftSubs()
        Dim selectedSoftSubs = crSettings.SoftSubLanguages
        For itemNumber As Integer = 0 To CrunchyrollSoftSubsCheckedListBox.Items.Count - 1
            Dim item = CrunchyrollSoftSubsCheckedListBox.Items.Item(itemNumber)
            Dim itemEnum = CrunchyrollLanguageTextList.GetEnumForItem(item)
            If selectedSoftSubs.Contains(itemEnum) And itemEnum <> CrunchyrollLanguage.NONE Then
                CrunchyrollSoftSubsCheckedListBox.SetItemChecked(itemNumber, True)
            End If
        Next

        ' Can set the default sub after the defaults have been populated from the checked list box.
        Dim defaultSub = crSettings.DefaultSoftSubLanguage
        CR_SoftSubDefault.SelectedItem = CrunchyrollLanguageTextList.Item(defaultSub)
    End Sub

    Private Sub LoadFunimationSettings()
        FunimationDubComboBox.SelectedItem = FunimationLanguageTextList.Item(funSettings.DubLanguage)
        FunimationDefaultSubComboBox.SelectedItem = FunimationLanguageTextList.Item(funSettings.DefaultSubtitle)
        FunimationBitrateComboBox.SelectedItem = FunimationBitrateTextList.Item(funSettings.PreferredBitrate)
        FunimationHardSubComboBox.SelectedItem = FunimationLanguageTextList.Item(funSettings.HardSubtitleLanguage)
        LoadFunimationSoftSubs()
        LoadFunimationSubFormats()
    End Sub
    Private Sub LoadFunimationSoftSubs()
        Dim softSubs = funSettings.SoftSubtitleLanguages

        FunimationEnglishCheckBox.Checked = softSubs.Contains(FunimationLanguage.ENGLISH)
        FunimationSpanishCheckBox.Checked = softSubs.Contains(FunimationLanguage.SPANISH)
        FunimationPortugueseCheckBox.Checked = softSubs.Contains(FunimationLanguage.PORTUGUESE)
    End Sub

    Private Sub LoadFunimationSubFormats()
        Dim formats = funSettings.SubtitleFormats

        FunimationSubSrtCheckBox.Checked = formats.Contains(SubFormat.SRT)
        FunimationSubVttCheckBox.Checked = formats.Contains(SubFormat.VTT)

        Dim languages = funSettings.SoftSubtitleLanguages
        If languages.Count > 0 And formats.Count = 0 Then
            FunimationSubVttCheckBox.Checked = True
        End If
    End Sub

#End Region

#Region "Saving"
    Private Sub Btn_Save_Click(sender As Object, e As EventArgs) Handles Btn_Save.Click
        SaveCurrentSettings()

        ' Adjust simultaneous downloads limit for incompatible settings.
        ' Must do this after saving other settings so that the download limit is set using the latest settings.
        Dim isAudioOnly = settings.OutputFormat.GetVideoFormat() = Format.MediaFormat.AAC_AUDIO_ONLY
        Dim ffmpegCommand = settings.Ffmpeg
        Dim encoder = ffmpegCommand.GetSavedEncoder()
        If Not isAudioOnly And encoder IsNot Nothing Then
            If encoder.Hardware = EncoderImplementation.NVIDIA And settings.SimultaneousDownloads > 2 Then
                settings.SimultaneousDownloads = 2

            ElseIf encoder.Hardware = EncoderImplementation.SOFTWARE And settings.SimultaneousDownloads > 1 Then
                settings.SimultaneousDownloads = 1
            End If
        End If

        My.Settings.Save()
        Close()
    End Sub


    Private Sub SaveCurrentSettings()
        SaveMainSettings()
        SaveOutputSettings()
        SaveNamingSettings()
        SaveCrunchyrollSettings()
        SaveFunimationSettings()
    End Sub

    Private Sub SaveMainSettings()
        SaveAddOnPortSetting()

        settings.SimultaneousDownloads = CInt(SimultaneousDownloadsInput.Value)
        If DefaultWebsiteTextBox.Text = Nothing Then
            settings.DefaultWebsite = "https://www.crunchyroll.com/"
        Else
            settings.DefaultWebsite = DefaultWebsiteTextBox.Text
        End If

        settings.SubfolderDisplayBehavior = SubfolderTextList.GetEnumForItem(HideSubfoldersComboBox.SelectedItem)
        settings.InsecureCurl = IgnoreTlsCheckBox.Checked
        settings.ErrorLimit = CInt(ErrorLimitInput.Value)
    End Sub

    Private Sub SaveAddOnPortSetting()
        Dim previousAddOnPort = settings.ServerPort
        Dim port As Integer
        Select Case ServerPortTextList.GetEnumForItem(ServerPortInput.SelectedItem)
            Case ServerPortOptions.DISABLED
                port = 0
            Case ServerPortOptions.PORT_80
                port = 80
            Case ServerPortOptions.PORT_8080
                port = 8080
            Case ServerPortOptions.CUSTOM
                port = CInt(CustomServerPortInput.Value)
        End Select

        settings.ServerPort = port

        If previousAddOnPort <> port Then
            MsgBox("Changing add-on support requires a restart of the downloader.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub SaveOutputSettings()
        SaveResolutionSetting()
        SaveOutputFormat()
        settings.DownloadMode = DownloadModeTextList.GetEnumForItem(DownloadModeDropdown.SelectedItem)
        settings.TemporaryFolder = TemporaryFolderTextBox.Text
        settings.UseDownloadQueue = UseQueueCheckbox.Checked
        settings.Ffmpeg = CreateFfmpegSettingFromInputs()
    End Sub
    Private Sub SaveResolutionSetting()
        Dim settings = ProgramSettings.GetInstance()
        If A1080p.Checked Then
            settings.DownloadResolution = Resolution.RESOLUTION_1080P
        ElseIf A720p.Checked Then
            settings.DownloadResolution = Resolution.RESOLUTION_720P
        ElseIf A480p.Checked Then
            settings.DownloadResolution = Resolution.RESOLUTION_480P
        ElseIf A360p.Checked Then
            settings.DownloadResolution = Resolution.RESOLUTION_360P
        ElseIf AAuto.Checked Then
            settings.DownloadResolution = Resolution.AUTO
        End If
    End Sub
    Private Sub SaveOutputFormat()
        Dim videoFormat = VideoFormatTextList.GetEnumForItem(CB_Format.SelectedItem)
        Dim subFormat = SubtitleFormatTextList.GetEnumForItem(CB_Merge.SelectedItem)
        settings.OutputFormat = New Format(videoFormat, subFormat)
    End Sub

    Private Sub SaveNamingSettings()
        SaveSeasonPrefix()
        SaveEpisodePrefix()
        settings.FilenameFormat = nameFormatter.GetTemplate()
        settings.KodiNaming = KodiNamingCheckBox.Checked
        settings.SeasonNumberNaming = SeasonNumberBehaviorTextlist.GetEnumForItem(SeasonNumberBehaviorComboBox.SelectedItem)
        settings.ZeroPaddingLength = LeadingZerosComboBox.SelectedIndex + 1
        settings.IncludeSubtitleLanguageInFirstSubtitle = IncludeLanguageNameCheckBox.Checked
        settings.SubLanguageNaming = SubtitleNamingTextList.GetEnumForItem(SubLanguageNamingComboBox.SelectedItem)
    End Sub
    Private Sub SaveSeasonPrefix()
        If SeasonPrefixTextBox.Text = SEASON_PREFIX_PLACEHOLDER Then
            settings.SeasonPrefix = DEFAULT_SEASON_PREFIX
        Else
            settings.SeasonPrefix = SeasonPrefixTextBox.Text
        End If
    End Sub
    Private Sub SaveEpisodePrefix()
        If EpisodePrefixTextBox.Text = EPISODE_PREFIX_PLACEHOLDER Then
            settings.EpisodePrefix = DEFAULT_EPISODE_PREFIX
        Else
            settings.EpisodePrefix = EpisodePrefixTextBox.Text
        End If
    End Sub

    Private Sub SaveCrunchyrollSettings()
        SaveCrunchyrollSoftSubs()
        crSettings.AcceptHardsubs = CrunchyrollAcceptHardsubsCheckBox.Checked
        crSettings.AudioLanguage = CrunchyrollLanguageTextList.GetEnumForItem(CrunchyrollAudioLanguageComboBox.SelectedItem)
        crSettings.HardSubLanguage = CrunchyrollLanguageTextList.GetEnumForItem(CrunchyrollHardsubComboBox.SelectedItem)
        crSettings.DefaultSoftSubLanguage = CrunchyrollLanguageTextList.GetEnumForItem(CR_SoftSubDefault.SelectedItem)
        crSettings.EnableChapters = CrunchyrollChaptersCheckBox.Checked
    End Sub
    Private Sub SaveCrunchyrollSoftSubs()
        Dim selectedItems = CrunchyrollSoftSubsCheckedListBox.CheckedItems

        Dim selectedEnumList = New List(Of CrunchyrollLanguage)
        For Each item In selectedItems
            Dim enumValue = CrunchyrollLanguageTextList.GetEnumForItem(item)
            selectedEnumList.Add(enumValue)
        Next

        crSettings.SoftSubLanguages = selectedEnumList
    End Sub

    Private Sub SaveFunimationSettings()
        SaveFunimationSoftSubs()
        SaveFunimationSubFormats()
        funSettings.DubLanguage = FunimationLanguageTextList.GetEnumForItem(FunimationDubComboBox.SelectedItem)
        funSettings.DefaultSubtitle = FunimationLanguageTextList.GetEnumForItem(FunimationDefaultSubComboBox.SelectedItem)
        funSettings.PreferredBitrate = FunimationBitrateTextList.GetEnumForItem(FunimationBitrateComboBox.SelectedItem)
        funSettings.HardSubtitleLanguage = FunimationLanguageTextList.GetEnumForItem(FunimationHardSubComboBox.SelectedItem)
    End Sub
    Private Sub SaveFunimationSoftSubs()
        Dim subList = New HashSet(Of FunimationLanguage)

        If FunimationEnglishCheckBox.Checked Then
            subList.Add(FunimationLanguage.ENGLISH)
        End If
        If FunimationSpanishCheckBox.Checked Then
            subList.Add(FunimationLanguage.SPANISH)
        End If
        If FunimationPortugueseCheckBox.Checked Then
            subList.Add(FunimationLanguage.PORTUGUESE)
        End If

        funSettings.SoftSubtitleLanguages = subList
    End Sub
    Private Sub SaveFunimationSubFormats()
        Dim subSet = New HashSet(Of SubFormat)

        If FunimationSubSrtCheckBox.Checked Then
            subSet.Add(SubFormat.SRT)
        End If
        If FunimationSubVttCheckBox.Checked Then
            subSet.Add(SubFormat.VTT)
        End If

        If funSettings.SoftSubtitleLanguages.Count > 0 And subSet.Count = 0 Then
            subSet.Add(SubFormat.VTT)
        End If

        funSettings.SubtitleFormats = subSet
    End Sub

#End Region

    Private Sub ServerPortInput_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ServerPortInput.SelectedIndexChanged
        Dim selectedEnum = ServerPortTextList.GetEnumForItem(ServerPortInput.SelectedItem)
        CustomServerPortInput.Enabled = selectedEnum = ServerPortOptions.CUSTOM
    End Sub


    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles pictureBox1.Click
        Me.Close()
    End Sub
#Region "UI"

    Private Sub Btn_Close_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox1.MouseEnter

        pictureBox1.Image = My.Resources.main_del
    End Sub

    Private Sub Btn_Close_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox1.MouseLeave

        pictureBox1.Image = Main.CloseImg
    End Sub

    Private Sub Btn_Save_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Save.MouseEnter, Btn_Save.GotFocus
        Btn_Save.Image = My.Resources.crdSettings_Button_SafeExit_hover
    End Sub

    Private Sub Btn_Save_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Save.MouseLeave, Btn_Save.LostFocus
        Btn_Save.Image = My.Resources.crdSettings_Button_SafeExit
    End Sub


    Private Sub ComboBox1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles CrunchyrollHardsubComboBox.DrawItem, FunimationHardSubComboBox.DrawItem, FunimationDubComboBox.DrawItem
        Dim CB As ComboBox = CType(sender, ComboBox)
        CB.BackColor = Color.White
        If e.Index >= 0 Then
            Using st As New StringFormat With {.Alignment = StringAlignment.Center}
                ' e.DrawBackground()
                ' e.DrawFocusRectangle()
                e.Graphics.FillRectangle(SystemBrushes.ControlLightLight, e.Bounds)
                e.Graphics.DrawString(CB.Items(e.Index).ToString, e.Font, Brushes.Black, e.Bounds, st)

            End Using
        End If
    End Sub


    Private Sub AAuto_Click(sender As Object, e As EventArgs) Handles AAuto.Click
        If CB_Merge.SelectedIndex > 0 Then
            If AAuto.Checked = True Then
                If MessageBox.Show("Resolution '[Auto]' and merge the subtitle with the video file will download all resolutions!" + vbNewLine + "Press 'Yes' to enable it anyway", "Prepare for unforeseen consequences.", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                Else
                    AAuto.Checked = False
                    A360p.Checked = True
                End If
            End If
        End If
    End Sub

    Private Sub MergeMP4_Click(sender As Object, e As EventArgs)
        If CB_Merge.SelectedIndex > 0 Then
            If AAuto.Checked = True Then
                If MessageBox.Show("Resolution '[Auto]' and merge the subtitle with the video file will download all resolutions!" + vbNewLine + "Press 'Yes' to enable it anyway", "Prepare for unforeseen consequences.", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                Else
                    CB_Merge.SelectedIndex = 0
                End If
            End If
        End If
    End Sub

    Private Sub VideoCodecComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles VideoCodecComboBox.SelectedIndexChanged
        Dim selectedItem = CodecTextList.GetEnumForItem(VideoCodecComboBox.SelectedItem)
        If selectedItem = Codec.AV1 Then
            ' TODO: Disable AMD hardware option
            Dim messageBoxResult =
                MessageBox.Show("The inculded ffmpeg version does not support any AV1 encoders." + vbNewLine +
                               "Click 'Help' to go to the ffmpeg download page.",
                               "AV1 support",
                               MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Information,
                               MessageBoxDefaultButton.Button1,
                               0,
                               "https://ffmpeg.org/download.html")
            If messageBoxResult = DialogResult.Cancel Then
                VideoCodecComboBox.SelectedItem = CodecTextList.Item(Codec.H_264)
            End If
        End If

        UpdateFfmpegDisplay()
    End Sub

    Private Sub Ffmpeg_parameters_changed(sender As Object, e As EventArgs) Handles VideoEncoderComboBox.SelectedIndexChanged, FfmpegPresetComboBox.SelectedIndexChanged, BitrateNumericInput.ValueChanged
        UpdateFfmpegDisplay()
    End Sub

    Private Sub Ffmpeg_copy_and_bitrate_CheckedChanged(sender As Object, e As EventArgs) Handles FfmpegCopyCheckBox.CheckedChanged, TargetBitrateCheckBox.CheckedChanged
        UpdateFfmpegInputStates()
        UpdateFfmpegDisplay()
    End Sub

    Private Sub UpdateFfmpegInputStates()
        If FfmpegCopyCheckBox.Checked Then
            VideoCodecComboBox.Enabled = False
            VideoEncoderComboBox.Enabled = False
            FfmpegPresetComboBox.Enabled = False
            TargetBitrateCheckBox.Enabled = False
            BitrateNumericInput.Enabled = False
        Else
            VideoCodecComboBox.Enabled = True
            VideoEncoderComboBox.Enabled = True
            FfmpegPresetComboBox.Enabled = True
            TargetBitrateCheckBox.Enabled = True
            BitrateNumericInput.Enabled = TargetBitrateCheckBox.Checked
        End If
    End Sub

    Private Sub UpdateFfmpegDisplay()
        FfmpegCommandPreviewTextBox.Text = CreateFfmpegSettingFromInputs().GetFfmpegArguments()
    End Sub

    Private Function CreateFfmpegSettingFromInputs() As FfmpegOptions
        Dim builder = New FfmpegOptions.Builder()
        builder.SetIncludeUnusedVideoSettings(True)

        builder.SetCopyMode(FfmpegCopyCheckBox.Checked)

        Dim codec = CodecTextList.GetEnumForItem(VideoCodecComboBox.SelectedItem)
        builder.SetVideoCodec(codec)

        Dim hardware = EncoderHardwareTextList.GetEnumForItem(VideoEncoderComboBox.SelectedItem)
        builder.SetEncoderHardware(hardware)

        Dim preset = SpeedPresetTextList.GetEnumForItem(FfmpegPresetComboBox.SelectedItem)
        builder.SetPresetSpeed(preset)

        builder.SetUseTargetBitrate(TargetBitrateCheckBox.Checked)
        builder.SetVideoBitrate(CInt(BitrateNumericInput.Value))

        Return builder.Build()
    End Function

    Private Sub Label7_Click(sender As Object, e As EventArgs)
        Process.Start("https://learn.microsoft.com/de-de/microsoft-edge/webview2/")
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)
        Process.Start("https://www.ffmpeg.org/about.html")
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs)
        Process.Start("https://github.com/hama3254/metroframework-modern-ui")
    End Sub



    Sub GroupBoxColor(ByVal color As Color)
        SimultaneousDownloadsInput.ForeColor = color
        ErrorLimitInput.ForeColor = color
        SoftSubs.ForeColor = color
        GB_SubLanguage.ForeColor = color
        DL_Count_simultaneous.ForeColor = color
        GB_Resolution.ForeColor = color
        GB_Filename_Pre.ForeColor = color
        GroupBox1.ForeColor = color
        FfmpegCommandGroupBox.ForeColor = color
        GroupBox3.ForeColor = color
        GroupBox4.ForeColor = color
        GroupBox5.ForeColor = color
        GroupBox6.ForeColor = color
        GroupBox7.ForeColor = color
        GroupBox8.ForeColor = color
        GroupBox9.ForeColor = color
        GroupBox10.ForeColor = color
        GroupBox11.ForeColor = color
        GroupBox12.ForeColor = color
        GroupBox13.ForeColor = color
        GroupBox15.ForeColor = color
        GroupBox16.ForeColor = color
        GroupBox17.ForeColor = color
        GroupBox18.ForeColor = color
        GroupBox19.ForeColor = color
        GroupBox20.ForeColor = color
    End Sub

    Private Sub HandleDarkModeChanged(NewValue As Boolean)
        If NewValue Then
            GroupBoxColor(Color.FromArgb(150, 150, 150))
            SimultaneousDownloadsInput.BackColor = Color.FromArgb(17, 17, 17)
            ErrorLimitInput.BackColor = Color.FromArgb(17, 17, 17)
        Else
            GroupBoxColor(Color.FromArgb(0, 0, 0))
            SimultaneousDownloadsInput.BackColor = Color.FromArgb(243, 243, 243)
            ErrorLimitInput.BackColor = Color.FromArgb(243, 243, 243)
        End If
        ' TODO: Get correct close image for theme
        pictureBox1.Image = Main.CloseImg
    End Sub

    Private Sub DarkMode_CheckedChanged(sender As Object, e As EventArgs) Handles DarkModeCheckBox.CheckedChanged
        ProgramSettings.GetInstance().DarkMode = DarkModeCheckBox.Checked
    End Sub


    Private Sub Server_Click(sender As Object, e As EventArgs)
        'If Server.Checked = True Then
        '    MsgBox("This feature requires a restart of the downloader", MsgBoxStyle.Information)
        'End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Dim client0 As New WebClient With {
                .Encoding = Encoding.UTF8
            }
            client0.Headers.Add(My.Resources.ffmpeg_user_agend)

            Dim str0 As String = client0.DownloadString("https://api.github.com/repos/hama3254/Crunchyroll-Downloader-v3.0/releases")

            Dim GitHubLastIsPre() As String = str0.Split(New String() {"""" + "prerelease" + """" + ": "}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim LastNonPreRelase As Integer = 0

            For i As Integer = 1 To GitHubLastIsPre.Count - 1
                Dim GitHubLastIsPre1() As String = GitHubLastIsPre(i).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)

                If GitHubLastIsPre1(0) = "false" Then
                    LastNonPreRelase = i
                    Exit For
                End If
            Next

            Dim GitHubLastTag() As String = str0.Split(New String() {"""" + "tag_name" + """" + ": " + """"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim GitHubLastTag1() As String = GitHubLastTag(LastNonPreRelase).Split(New String() {"""" + ","}, System.StringSplitOptions.RemoveEmptyEntries)

            LastVersionString = GitHubLastTag1(0)

            'Debug.WriteLine(GitHubLastTag1(0))

        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub




    Private Sub CB_Fun_HardSubs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FunimationHardSubComboBox.SelectedIndexChanged
        Dim selectedSetting = FunimationLanguageTextList.GetEnumForItem(FunimationHardSubComboBox.SelectedItem)

        If Not uiInitializing And selectedSetting <> FunimationLanguage.NONE Then
            If FfmpegCopyCheckBox.Checked Then
                Dim messageBoxResult =
                    MessageBox.Show("Funimation hard subtitles are post-processed and require re-encoding the video stream." + vbNewLine +
                    "This will take a lot of resources. You may not want to do more than one download at a time." + vbNewLine +
                    "Continue with this setting?",
                    "Heavy system usage warning",
                    MessageBoxButtons.YesNo)
                If messageBoxResult = DialogResult.No Then
                    FunimationHardSubComboBox.SelectedItem = FunimationLanguageTextList.Item(FunimationLanguage.NONE)
                End If
            End If
        End If
    End Sub

    Private Sub FunimationEnglishCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles FunimationEnglishCheckBox.CheckedChanged
        UpdateFunimationDefaultSubList(FunimationLanguage.ENGLISH, FunimationEnglishCheckBox.Checked)
    End Sub

    Private Sub FunimationSpanishCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles FunimationSpanishCheckBox.CheckedChanged
        UpdateFunimationDefaultSubList(FunimationLanguage.SPANISH, FunimationSpanishCheckBox.Checked)
    End Sub

    Private Sub FunimationPortugueseCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles FunimationPortugueseCheckBox.CheckedChanged
        UpdateFunimationDefaultSubList(FunimationLanguage.PORTUGUESE, FunimationPortugueseCheckBox.Checked)
    End Sub

    Private Sub UpdateFunimationDefaultSubList(language As FunimationLanguage, enabled As Boolean)
        If enabled Then
            FunimationDefaultSubOptionsList.AddFromParent(language)
        Else
            FunimationDefaultSubOptionsList.RemoveEnum(language)
        End If
    End Sub


    Private Sub MetroLink1_Click(sender As Object, e As EventArgs)
        Process.Start("https://github.com/hama3254/Crunchyroll-Downloader-v3.0/discussions/276")
    End Sub


    Private Sub TabPage7_Enter(sender As Object, e As EventArgs) Handles TabPage7.Enter
        LastVersion.Text = "last release: " + LastVersionString
    End Sub

    Private Sub CB_Format_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Format.SelectedIndexChanged
        UpdateMergeFormatInput()
    End Sub

    Private Sub CrunchyrollSoftSubsCheckedListBox_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CrunchyrollSoftSubsCheckedListBox.ItemCheck
        Dim currentSelectedItem = CR_SoftSubDefault.SelectedItem
        Dim currentDefaultSub = CrunchyrollLanguageTextList.GetEnumForItem(currentSelectedItem)

        Dim changedIndex = e.Index
        Dim changedItem = CrunchyrollSoftSubsCheckedListBox.Items.Item(changedIndex)
        Dim changedEnum = CrunchyrollLanguageTextList.GetEnumForItem(changedItem)
        If e.NewValue = CheckState.Checked Then
            CrunchyrollDefaultLanguageSubList.AddFromParent(changedEnum)
        ElseIf e.NewValue = CheckState.Unchecked Then
            CrunchyrollDefaultLanguageSubList.RemoveEnum(changedEnum)
        End If

        ' Must set the selected item because it may have changed when adding or removing an item
        ' Combo box seems to maintain the selected index, not the selected item
        If changedEnum <> currentDefaultSub Then
            CR_SoftSubDefault.SelectedItem = currentSelectedItem
        End If

    End Sub

    Private Sub RepopulateMergeComboBox()

    End Sub

    Private Sub MergeMP4_CheckedChanged(sender As Object, e As EventArgs)
        ' I don't think this is ever used - this isn't a valid handler. Probably an old design
        If CB_Format.Text = "AAC (Audio only)" Then
            If CB_Merge.SelectedIndex > 0 Then
                MsgBox("Merged subs are not avalible with audio only!", MsgBoxStyle.Information)
            End If
            CB_Merge.SelectedIndex = 0
        End If
    End Sub

    Private Sub DD_DLMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DownloadModeDropdown.SelectedIndexChanged
        Dim currentDownloadMode = DownloadModeTextList.GetEnumForItem(DownloadModeDropdown.SelectedItem)
        TemporaryFolderTextBox.Enabled = currentDownloadMode <> DownloadModeOptions.FFMPEG
    End Sub

    Private Sub TempTB_Click(sender As Object, e As EventArgs) Handles TemporaryFolderTextBox.Click
        Dim folderDialog As New FolderBrowserDialog With {
            .RootFolder = Environment.SpecialFolder.MyComputer
        }
        If TemporaryFolderTextBox.Text IsNot Nothing Then
            folderDialog.SelectedPath = TemporaryFolderTextBox.Text
        End If

        If folderDialog.ShowDialog() = DialogResult.OK Then
            TemporaryFolderTextBox.Text = folderDialog.SelectedPath
        End If
    End Sub
#Region "Build Name String"

    Private Sub SeriesNameCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles SeriesNameCheckBox.CheckedChanged
        UpdateFilenameTemplate(FilenameFormatter.TemplateItem.SERIES_NAME, SeriesNameCheckBox.Checked)
    End Sub

    Private Sub EpisodeNumberCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles EpisodeNumberCheckBox.CheckedChanged
        UpdateFilenameTemplate(FilenameFormatter.TemplateItem.EPISODE_NUMBER, EpisodeNumberCheckBox.Checked)
    End Sub

    Private Sub EpisodeTitleCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles EpisodeTitleCheckBox.CheckedChanged
        UpdateFilenameTemplate(FilenameFormatter.TemplateItem.EPISODE_TITLE, EpisodeTitleCheckBox.Checked)
    End Sub

    Private Sub SeasonNumberCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles SeasonNumberCheckBox.CheckedChanged
        UpdateFilenameTemplate(FilenameFormatter.TemplateItem.SEASON_NUMBER, SeasonNumberCheckBox.Checked)
    End Sub

    Private Sub AudioLanguageCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles AudioLanguageCheckBox.CheckedChanged
        UpdateFilenameTemplate(FilenameFormatter.TemplateItem.AUDIO_LANGUAGE, AudioLanguageCheckBox.Checked)
    End Sub

    Private Sub UpdateFilenameTemplate(item As FilenameFormatter.TemplateItem, add As Boolean)
        If nameTemplateLoading Then
            ' Checkbox initial values are being set, don't modify the template.
            Exit Sub
        End If
        If add Then
            nameFormatter.AppendTemplateItem(item)
        Else
            nameFormatter.RemoveTemplateItem(item)
        End If
        FilenameTemplatePreview.Text = nameFormatter.GetTemplate()
    End Sub

    Private Sub SeasonPrefixTextBox_UserAction(sender As Object, e As EventArgs) Handles SeasonPrefixTextBox.Click, SeasonPrefixTextBox.GotFocus
        If SeasonPrefixTextBox.Text = SEASON_PREFIX_PLACEHOLDER Then
            SeasonPrefixTextBox.Text = Nothing
        End If
    End Sub

    Private Sub SeasonPrefixTextBox_LostFocus(sender As Object, e As EventArgs) Handles SeasonPrefixTextBox.LostFocus
        If SeasonPrefixTextBox.Text = Nothing Then
            SeasonPrefixTextBox.Text = SEASON_PREFIX_PLACEHOLDER
        End If
    End Sub


    Private Sub EpisodePrefixTextBox_UserAction(sender As Object, e As EventArgs) Handles EpisodePrefixTextBox.Click, EpisodePrefixTextBox.GotFocus
        If EpisodePrefixTextBox.Text = EPISODE_PREFIX_PLACEHOLDER Then
            EpisodePrefixTextBox.Text = Nothing
        End If
    End Sub

    Private Sub EpisodePrefixTextBox_LostFocus(sender As Object, e As EventArgs) Handles EpisodePrefixTextBox.LostFocus
        If EpisodePrefixTextBox.Text = Nothing Then
            EpisodePrefixTextBox.Text = EPISODE_PREFIX_PLACEHOLDER
        End If
    End Sub



#End Region

#End Region

    ' TODO: This probably shouldn't use the HTTP port but the Chrome extension would probably have to be changed too.
    Public Enum ServerPortOptions
        DISABLED
        PORT_80
        PORT_8080
        CUSTOM
    End Enum
End Class