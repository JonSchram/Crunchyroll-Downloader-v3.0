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

Public Class Einstellungen
    Inherits MetroForm

    ' Display objects for combo boxes backed by enums
    Private ReadOnly ServerPortTextList As New EnumTextList(Of ServerPortOptions)()
    Private ReadOnly SubfolderTextList As New EnumTextList(Of SubfolderDisplay)()
    Private ReadOnly DownloadModeTextList As New EnumTextList(Of DownloadModeOptions)()
    Private ReadOnly VideoFormatTextList As New EnumTextList(Of Format.MediaFormat)()
    Private ReadOnly SubtitleFormatTextList As New EnumTextList(Of Format.SubtitleMerge)()
    Private ReadOnly SpeedPresetTextList As New EnumTextList(Of FfmpegSettings.VideoEncoder.Speed)()
    Private ReadOnly CodecTextList As New EnumTextList(Of FfmpegSettings.VideoEncoder.Codec)()
    Private ReadOnly EncoderHardwareTextList As New EnumTextList(Of FfmpegSettings.VideoEncoder.EncoderImplementation)()

    Private ReadOnly SubToTextMap As New Dictionary(Of Format.SubtitleMerge, String)() From {
    {Format.SubtitleMerge.DISABLED, "[merge disabled]"},
    {Format.SubtitleMerge.MOV_TEXT, "mov_text"},
    {Format.SubtitleMerge.COPY, "copy"},
    {Format.SubtitleMerge.SRT, "srt"}
    }

    Dim Manager As MetroStyleManager = Main.Manager
    Dim LastVersionString As String = "v3.8-Beta"

    Public CR_SoftSubsTemp As New List(Of String)

    Public Sub New()
        InitializeComponent()

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
            .Add(FfmpegSettings.VideoEncoder.Speed.NO_PRESET, "(No preset)")
            .Add(FfmpegSettings.VideoEncoder.Speed.VERY_SLOW, "Very slow")
            .Add(FfmpegSettings.VideoEncoder.Speed.SLOWER, "Slower")
            .Add(FfmpegSettings.VideoEncoder.Speed.SLOW, "Slow")
            .Add(FfmpegSettings.VideoEncoder.Speed.MEDIUM, "Medium")
            .Add(FfmpegSettings.VideoEncoder.Speed.FAST, "Fast")
            .Add(FfmpegSettings.VideoEncoder.Speed.FASTER, "Faster")
            .Add(FfmpegSettings.VideoEncoder.Speed.VERY_FAST, "Very fast")
        End With

        With CodecTextList
            .Add(FfmpegSettings.VideoEncoder.Codec.H_264, "h.264")
            .Add(FfmpegSettings.VideoEncoder.Codec.H_265, "h.265")
            .Add(FfmpegSettings.VideoEncoder.Codec.AV1, "AV1")
        End With

        With EncoderHardwareTextList
            .Add(FfmpegSettings.VideoEncoder.EncoderImplementation.SOFTWARE, "Software")
            .Add(FfmpegSettings.VideoEncoder.EncoderImplementation.NVIDIA, "Nvidia")
            .Add(FfmpegSettings.VideoEncoder.EncoderImplementation.AMD, "AMD")
            .Add(FfmpegSettings.VideoEncoder.EncoderImplementation.INTEL, "Intel")
        End With
    End Sub

    Private Sub PopulateSubFormats(VideoFormat As Format.MediaFormat)
        Dim supportedSubtitleFormats = Format.GetValidSubtitleFormats(VideoFormat)

        SubtitleFormatTextList.Clear()
        For Each SubFormat In supportedSubtitleFormats
            SubtitleFormatTextList.Add(SubFormat, SubToTextMap.Item(SubFormat))
        Next
    End Sub

    Private Sub UpdateMergeFormatInput()
        UpdateMergeFormatInput(VideoFormatTextList.GetEnumForItem(CB_Format.SelectedItem))
    End Sub

    Private Sub UpdateMergeFormatInput(VideoFormat As Format.MediaFormat)
        PopulateSubFormats(VideoFormat)
        CB_Merge.SelectedIndex = 0
        CB_Merge.Enabled = VideoFormat <> Format.MediaFormat.AAC_AUDIO_ONLY
    End Sub

    Private Sub Einstellungen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler ProgramSettings.DarkModeChanged, AddressOf HandleDarkModeChanged

        Dim settings As ProgramSettings = ProgramSettings.GetInstance()

        LoadSettings()

        Label6.Text = "You have: v" + Application.ProductVersion.ToString '+ " WebView2_Test"

        BackgroundWorker1.RunWorkerAsync()


        'CR_Anime_Folge = CR_Name_Staffel0_Folge1(1)
        'If GitHubLastTag1(0)
        CR_SoftSubsTemp.AddRange(Main.SoftSubs)

        Manager.Owner = Me
        Me.StyleManager = Manager



        LeadingZeroDD.SelectedIndex = Main.LeadingZero

        Bitrate_Funi.SelectedIndex = Main.Funimation_Bitrate

        CB_Ignore.SelectedIndex = Main.IgnoreSeason

        If Main.IncludeLangName = True Then
            CB_SoftSubSettings.SelectedIndex = 1
        Else
            CB_SoftSubSettings.SelectedIndex = 0
        End If

        If Main.LangNameType = 1 Then
            LangNameType_DD.SelectedIndex = 1
        ElseIf Main.LangNameType = 2 Then
            LangNameType_DD.SelectedIndex = 2
        Else
            LangNameType_DD.SelectedIndex = 0
        End If

        If Main.KodiNaming = True Then
            CB_Kodi.Checked = True
        End If

        If Main.DubMode = True Then
            DubMode.Checked = True
        End If

        If Main.CR_Chapters = True Then
            ChB_Chapters.Checked = True
        End If



        TabControl1.SelectedIndex = 0

#Region "sof subs"
        CR_SoftSubDefault.SelectedIndex = 0

        For i As Integer = 1 To Main.LangValueEnum.Count - 2 ' index 0 = 'null' | last index = jp

            If Main.SoftSubs.Contains(Main.LangValueEnum(i).CR_Value) Then
                CR_SoftSubDefault.Items.Add(Main.LangValueEnum(i).Name)

            End If

        Next
        CR_SoftSubs.SelectedIndex = 0

#End Region

        For i As Integer = 0 To Main.SubFunimation.Count - 1
            If Main.SubFunimation(i) = "en" Then
                CB_fun_eng.Checked = True
            ElseIf Main.SubFunimation(i) = "es" Then
                CB_fun_es.Checked = True
            ElseIf Main.SubFunimation(i) = "pt" Then
                CB_fun_ptbr.Checked = True
            End If

        Next

        Me.Location = New Point(CInt(Main.Location.X + Main.Width / 2 - Me.Width / 2), CInt(Main.Location.Y + Main.Height / 2 - Me.Height / 2))
        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception

        End Try

        If Main.Funimation_srt = True Then
            CB_srt.Checked = True
        Else
            CB_srt.Checked = False
        End If

        If Main.Funimation_vtt = True Then
            CB_vtt.Checked = True
        Else
            CB_vtt.Checked = False
        End If


        If Main.HardSubFunimation = "en" Then
            CB_Fun_HardSubs.SelectedItem = "English"

        ElseIf Main.HardSubFunimation = "pt" Then
            CB_Fun_HardSubs.SelectedItem = "Português (Brasil)"

        ElseIf Main.HardSubFunimation = "es" Then
            CB_Fun_HardSubs.SelectedItem = "Español (LA)"

        Else
            CB_Fun_HardSubs.SelectedItem = "Disabled"
            'FunimationHardsub.Checked = True
        End If

        If Main.DubFunimation = "english" Then
            Fun_Dub_Over.SelectedItem = "english"

        ElseIf Main.DubFunimation = "japanese" Then
            Fun_Dub_Over.SelectedItem = "japanese"

        ElseIf Main.DubFunimation = "portuguese(Brazil)" Then
            Fun_Dub_Over.SelectedItem = "portuguese(Brazil)"

        ElseIf Main.DubFunimation = "spanish(Mexico)" Then
            Fun_Dub_Over.SelectedItem = "spanish(Mexico)"

        Else
            Fun_Dub_Over.SelectedItem = "Disabled"
        End If


        Try
            GB_SubLanguage.Text = Main.GB_SubLanguage_Text
            DL_Count_simultaneous.Text = Main.DL_Count_simultaneousText
        Catch ex As Exception

        End Try

        CB_CR_Harsubs.Items.Clear()

        For i As Integer = 0 To Main.LangValueEnum.Count - 1
            CB_CR_Harsubs.Items.Add(Main.LangValueEnum(i).Name)
            If Main.LangValueEnum(i).CR_Value = Main.SubSprache.CR_Value Then
                'MsgBox(CB_CR_Harsubs.Items.Count.ToString)
                'MsgBox(i.ToString)
                CB_CR_Harsubs.SelectedIndex = i
                'Exit For
            End If

        Next


        CB_CR_Audio.Items.Clear()

        For i As Integer = 1 To Main.LangValueEnum.Count - 1

            CB_CR_Audio.Items.Add(Main.LangValueEnum(i).Name)
            If Main.LangValueEnum(i).CR_Value = Main.DubSprache.CR_Value Then
                CB_CR_Audio.SelectedIndex = i - 1

            End If

        Next



        DD_Season_Prefix.Text = Main.Season_Prefix

        DD_Episode_Prefix.Text = Main.Episode_Prefix


        If Main.DefaultSubFunimation = "en" Then
            FunSubDef.SelectedItem = "English"
        ElseIf Main.DefaultSubFunimation = "pt" Then
            FunSubDef.SelectedItem = "Português (Brasil)"
        ElseIf Main.DefaultSubFunimation = "es" Then
            FunSubDef.SelectedItem = "Español (LA)"
        Else
            FunSubDef.SelectedItem = "[Disabled]"
            'FunimationHardsub.Checked = True
        End If



        If Main.DefaultSubCR = "de-DE" Then
            CR_SoftSubDefault.SelectedItem = "Deutsch"
        ElseIf Main.DefaultSubCR = "en-US" Then
            CR_SoftSubDefault.SelectedItem = "English"
        ElseIf Main.DefaultSubCR = "pt-BR" Then
            CR_SoftSubDefault.SelectedItem = "Português (Brasil)"
        ElseIf Main.DefaultSubCR = "es-419" Then
            CR_SoftSubDefault.SelectedItem = "Español (LA)"
        ElseIf Main.DefaultSubCR = "fr-FR" Then
            CR_SoftSubDefault.SelectedItem = "Français (France)"
        ElseIf Main.DefaultSubCR = "ar-SA" Then
            CR_SoftSubDefault.SelectedItem = "العربية (Arabic)"
        ElseIf Main.DefaultSubCR = "ru-RU" Then
            CR_SoftSubDefault.SelectedItem = "Русский (Russian)"
        ElseIf Main.DefaultSubCR = "it-IT" Then
            CR_SoftSubDefault.SelectedItem = "Italiano (Italian)"
        ElseIf Main.DefaultSubCR = "es-ES" Then
            CR_SoftSubDefault.SelectedItem = "Español (España)"
        Else
            CR_SoftSubDefault.SelectedItem = "[Disabled]"
        End If


        Dim NameParts As String() = Main.NameBuilder.Split(New String() {";"}, System.StringSplitOptions.RemoveEmptyEntries)



        For i As Integer = 0 To NameParts.Count - 1

            If NameParts(i) = "AnimeTitle" Then
                CB_Anime.Checked = True
            ElseIf NameParts(i) = "Season" Then
                CB_Season.Checked = True
            ElseIf NameParts(i) = "EpisodeNR" Then
                CB_EpisodeNR.Checked = True
            ElseIf NameParts(i) = "EpisodeName" Then
                CB_EpisodeName.Checked = True
            ElseIf NameParts(i) = "AnimeDub" Then
                CB_AnimeDub.Checked = True
            End If

        Next





    End Sub

    Private Sub LoadSettings()
        Dim settings = ProgramSettings.GetInstance()

        ' Main settings
        SimultaneousDownloadsInput.Value = settings.SimultaneousDownloads
        DefaultWebsiteTextBox.Text = settings.DefaultWebsite

        If settings.DarkMode Then
            DarkMode.Checked = True
            GroupBoxColor(Color.FromArgb(150, 150, 150))
            ' TODO: Set image box for dark mode directly instead of from Main
            pictureBox1.Image = Main.CloseImg
        Else
            DarkMode.Checked = False
            GroupBoxColor(Color.FromArgb(0, 0, 0))
        End If

        InitializeAddOnPortInput()
        Chb_Ign_tls.Checked = settings.InsecureCurl
        ErrorLimitInput.Value = settings.ErrorLimit
        InitializeSubfolderDisplayInput()

        ' Output settings
        InitializeDownloadModeInput()
        TemporaryFolderTextBox.Text = settings.TemporaryFolder
        UseQueueCheckbox.Checked = settings.UseDownloadQueue
        InitializeResolutionInput()

        InitializeOutputFormat()
        InitializeFfmepgInputs()

    End Sub

    Private Sub InitializeFfmepgInputs()
        Dim settings = ProgramSettings.GetInstance()

        Dim ffmpegCommand = settings.Ffmpeg
        Dim savedEncoder = ffmpegCommand.GetSavedEncoder()

        VideoCodecComboBox.DataSource = CodecTextList.GetDisplayItems()
        VideoEncoderComboBox.DataSource = EncoderHardwareTextList.GetDisplayItems()
        FfmpegPresetComboBox.DataSource = SpeedPresetTextList.GetDisplayItems()

        FfmpegCopyCheckBox.Checked = ffmpegCommand.VideoCopy
        VideoCodecComboBox.SelectedItem = CodecTextList.Item(savedEncoder.VideoCodec)
        VideoEncoderComboBox.SelectedItem = EncoderHardwareTextList.Item(savedEncoder.Hardware)
        FfmpegPresetComboBox.SelectedItem = SpeedPresetTextList.Item(savedEncoder.Preset)
        TargetBitrateCheckBox.Checked = savedEncoder.UseTargetBitrate
        BitrateNumericInput.Value = savedEncoder.TargetBitrate

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

    Private Function CreateFfmpegSettingFromInputs() As FfmpegSettings
        Dim builder = New FfmpegSettings.Builder()
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

    Private Sub UpdateFfmpegDisplay()
        FfmpegCommandPreviewTextBox.Text = CreateFfmpegSettingFromInputs().GetFfmpegArguments()
    End Sub

    Public Sub InitializeOutputFormat()
        Dim settings = ProgramSettings.GetInstance()
        ' TODO: Maybe put in a try-catch block in case the object parses incorrectly?
        Dim currentFormat = settings.OutputFormat

        CB_Format.Items.Clear()
        CB_Format.DataSource = VideoFormatTextList.GetDisplayItems()
        CB_Format.SelectedItem = VideoFormatTextList.Item(currentFormat.GetVideoFormat())

        ' Must set data source first because updating the merge format input sets selected index.
        CB_Merge.DataSource = SubtitleFormatTextList.GetDisplayItems()
        UpdateMergeFormatInput(currentFormat.GetVideoFormat())
        CB_Merge.SelectedItem = SubtitleFormatTextList.Item(currentFormat.GetSubtitleFormat())
    End Sub

    Private Sub InitializeResolutionInput()
        Dim settings = ProgramSettings.GetInstance()
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

    Private Sub InitializeAddOnPortInput()
        ServerPortInput.Items.Clear()
        ServerPortInput.DataSource = ServerPortTextList.GetDisplayItems()

        Dim addOnPort = ProgramSettings.GetInstance().ServerPort
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

    Private Sub SaveAddOnPortSetting()
        Dim settings = ProgramSettings.GetInstance()

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

    Private Sub ServerPortInput_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ServerPortInput.SelectedIndexChanged
        Dim selectedEnum = ServerPortTextList.GetEnumForItem(ServerPortInput.SelectedItem)
        CustomServerPortInput.Enabled = selectedEnum = ServerPortOptions.CUSTOM
    End Sub

    Private Sub InitializeSubfolderDisplayInput()
        Dim settings = ProgramSettings.GetInstance()
        CB_HideSF.Items.Clear()
        CB_HideSF.DataSource = SubfolderTextList.GetDisplayItems()

        Dim currentSetting = settings.SubfolderDisplayBehavior
        CB_HideSF.SelectedItem = SubfolderTextList.Item(currentSetting)
    End Sub

    Private Sub SaveSubfolderDisplaySetting()
        Dim settings = ProgramSettings.GetInstance()
        settings.SubfolderDisplayBehavior = SubfolderTextList.GetEnumForItem(CB_HideSF.SelectedItem)
    End Sub

    Private Sub InitializeDownloadModeInput()
        Dim settings = ProgramSettings.GetInstance()
        DownloadModeDropdown.Items.Clear()
        DownloadModeDropdown.DataSource = DownloadModeTextList.GetDisplayItems()

        Dim currentSetting = settings.DownloadMode
        DownloadModeDropdown.SelectedItem = DownloadModeTextList.Item(currentSetting)
    End Sub

    Private Sub SaveDownloadModeSetting()
        Dim settings = ProgramSettings.GetInstance()
        settings.DownloadMode = DownloadModeTextList.GetEnumForItem(DownloadModeDropdown.SelectedItem)
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
        Dim currentFormat = New Format(videoFormat, subFormat)

        Dim settings = ProgramSettings.GetInstance()
        settings.OutputFormat = currentFormat
    End Sub

    Private Sub SaveFfmpegSettings()
        Dim ffmpeg = CreateFfmpegSettingFromInputs()
        Dim settings = ProgramSettings.GetInstance()
        settings.Ffmpeg = ffmpeg
    End Sub

    Private Sub SaveCurrentSettings()
        Dim settings As ProgramSettings = ProgramSettings.GetInstance()

        ' Main settings
        settings.SimultaneousDownloads = CInt(SimultaneousDownloadsInput.Value)
        If DefaultWebsiteTextBox.Text = Nothing Then
            settings.DefaultWebsite = "https://www.crunchyroll.com/"
        Else
            settings.DefaultWebsite = DefaultWebsiteTextBox.Text
        End If

        SaveAddOnPortSetting()
        SaveSubfolderDisplaySetting()

        settings.InsecureCurl = Chb_Ign_tls.Checked

        settings.ErrorLimit = CInt(ErrorLimitInput.Value)

        ' Output settings
        SaveDownloadModeSetting()
        settings.TemporaryFolder = TemporaryFolderTextBox.Text
        settings.UseDownloadQueue = UseQueueCheckbox.Checked
        SaveResolutionSetting()

        SaveOutputFormat()
        SaveFfmpegSettings()
    End Sub

    Private Sub Btn_Save_Click(sender As Object, e As EventArgs) Handles Btn_Save.Click
        SaveCurrentSettings()

        Main.LeadingZero = LeadingZeroDD.SelectedIndex
        My.Settings.LeadingZero = LeadingZeroDD.SelectedIndex

        Main.Funimation_Bitrate = Bitrate_Funi.SelectedIndex
        My.Settings.Funimation_Bitrate = Bitrate_Funi.SelectedIndex

        Main.IgnoreSeason = CB_Ignore.SelectedIndex
        My.Settings.IgnoreSeason = CB_Ignore.SelectedIndex


        If DubMode.Checked = True Then
            Main.DubMode = True
            My.Settings.DubMode = True
        Else
            Main.DubMode = False
            My.Settings.DubMode = False

        End If

        If ChB_Chapters.Checked = True Then
            Main.CR_Chapters = True
            My.Settings.CR_Chapters = True
        Else
            Main.CR_Chapters = False
            My.Settings.CR_Chapters = False
        End If


        If CB_Kodi.Checked = True Then
            Main.KodiNaming = True
            My.Settings.KodiSupport = True
        Else
            Main.KodiNaming = False
            My.Settings.KodiSupport = False
        End If


        If DD_Season_Prefix.Text IsNot "[default season prefix]" Then
            Main.Season_Prefix = DD_Season_Prefix.Text
            My.Settings.Prefix_S = Main.Season_Prefix
        End If

        If DD_Episode_Prefix.Text IsNot "[default episode prefix]" Then
            Main.Episode_Prefix = DD_Episode_Prefix.Text
            My.Settings.Prefix_E = Main.Episode_Prefix
        End If

        For i As Integer = 0 To Main.LangValueEnum.Count - 1

            If CB_CR_Harsubs.SelectedItem.ToString = Main.LangValueEnum(i).Name Then
                Main.SubSprache = Main.LangValueEnum(i)
                My.Settings.Subtitle = Main.SubSprache.CR_Value
                'MsgBox(Main.LangValueEnum(i).Name)
                'MsgBox(Main.LangValueEnum(i).CR_Value)
                Exit For
            End If

        Next

        For i As Integer = 0 To Main.LangValueEnum.Count - 1

            If CB_CR_Audio.SelectedItem.ToString = Main.LangValueEnum(i).Name Then
                Main.DubSprache = Main.LangValueEnum(i)
                My.Settings.CR_Dub = Main.DubSprache.CR_Value

                Exit For
            End If

        Next



        If CR_SoftSubDefault.SelectedItem.ToString = "English" Then
            Main.DefaultSubCR = "en-US"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Deutsch" Then
            Main.DefaultSubCR = "de-DE"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Português (Brasil)" Then
            Main.DefaultSubCR = "pt-BR"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Español (LA)" Then
            Main.DefaultSubCR = "es-419"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Français (France)" Then
            Main.DefaultSubCR = "fr-FR"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "العربية (Arabic)" Then
            Main.DefaultSubCR = "ar-SA"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Русский (Russian)" Then
            Main.DefaultSubCR = "ru-RU"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Italiano (Italian)" Then
            Main.DefaultSubCR = "it-IT"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Español (España)" Then
            Main.DefaultSubCR = "es-ES"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "[Disabled]" Then
            Main.DefaultSubCR = "None"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        End If


        Main.NameBuilder = TB_NameString.Text

        My.Settings.NameTemplate = Main.NameBuilder



#Region "funimation"




        Main.DubFunimation = Fun_Dub_Over.SelectedItem.ToString


        My.Settings.FunimationDub = Main.DubFunimation


        'If CB_Fun_HardSubs.SelectedItem.ToString = "Disabled" Then
        '    Main.HardSubFunimation = "Disabled"
        '    rk.SetValue("FunimationHardsub", "Disabled", RegistryValueKind.String)

        'ElseIf CB_Fun_HardSubs.SelectedItem.ToString = "English" Then
        '    Main.HardSubFunimation = "en"
        '    rk.SetValue("FunimationHardsub", "en", RegistryValueKind.String)

        'ElseIf CB_Fun_HardSubs.SelectedItem.ToString = "Português (Brasil)" Then
        '    Main.HardSubFunimation = "pt"
        '    rk.SetValue("FunimationHardsub", "pt", RegistryValueKind.String)

        'ElseIf CB_Fun_HardSubs.SelectedItem.ToString = "Español (LA)" Then
        '    Main.HardSubFunimation = "es"
        '    rk.SetValue("FunimationHardsub", "es", RegistryValueKind.String)

        'End If

        If FunSubDef.SelectedItem.ToString = "[Disabled]" Then
            Main.DefaultSubFunimation = "Disabled"
            My.Settings.DefaultSubFunimation = Main.DefaultSubFunimation
        ElseIf FunSubDef.SelectedItem.ToString = "English" Then
            Main.DefaultSubFunimation = "en"
            My.Settings.DefaultSubFunimation = Main.DefaultSubFunimation
        ElseIf FunSubDef.SelectedItem.ToString = "Português (Brasil)" Then
            Main.DefaultSubFunimation = "pt"
            My.Settings.DefaultSubFunimation = Main.DefaultSubFunimation
        ElseIf FunSubDef.SelectedItem.ToString = "Español (LA)" Then
            Main.DefaultSubFunimation = "es"
            My.Settings.DefaultSubFunimation = Main.DefaultSubFunimation
        End If

        Main.SubFunimation.Clear()
        If CB_fun_eng.Checked = True Then
            Main.SubFunimation.Add("en")
        End If

        If CB_fun_es.Checked = True Then
            Main.SubFunimation.Add("es")

        End If

        If CB_fun_ptbr.Checked = True Then
            Main.SubFunimation.Add("pt")
        End If

        If Main.SubFunimation.Count > 0 And CB_vtt.Checked = False And CB_srt.Checked = False Then
            CB_vtt.Checked = True
        End If


        If CB_srt.Checked = True Then
            Main.Funimation_srt = True
            My.Settings.Funimation_srt = True
        Else
            Main.Funimation_srt = False
            My.Settings.Funimation_srt = False
        End If
        If CB_vtt.Checked = True Then
            Main.Funimation_vtt = True
            My.Settings.Funimation_vtt = True
        Else
            Main.Funimation_vtt = False
            My.Settings.Funimation_vtt = False
        End If


        Dim FunimationSaveString As String = Nothing
        For ii As Integer = 0 To Main.SubFunimation.Count - 1
            If FunimationSaveString = Nothing Then
                FunimationSaveString = Main.SubFunimation(ii)
            Else
                FunimationSaveString = FunimationSaveString + "," + Main.SubFunimation(ii)
            End If
        Next
        If FunimationSaveString = Nothing Then
            FunimationSaveString = "None"
        End If
        My.Settings.Fun_Sub = FunimationSaveString

#End Region





        Dim ffpmeg_cmd As String = Nothing

        If Main.ffmpeg_command = My.Settings.ffmpeg_command_override Then
            'override should not get overwritten 

        Else
            Main.ffmpeg_command = ffpmeg_cmd
            My.Settings.ffmpeg_command = Main.ffmpeg_command
        End If

        ' TODO: Replace with values from currently applying settings to ensure the correct value is used.
        Dim settings = ProgramSettings.GetInstance()
        Dim isAudioOnly = settings.OutputFormat.GetVideoFormat() = Format.MediaFormat.AAC_AUDIO_ONLY
        Dim ffmpegCommand = settings.Ffmpeg
        Dim encoder = ffmpegCommand.GetSavedEncoder()
        If Not isAudioOnly And encoder IsNot Nothing Then
            If encoder.Hardware = FfmpegSettings.VideoEncoder.EncoderImplementation.NVIDIA Then
                If SimultaneousDownloadsInput.Value > 2 Then
                    SimultaneousDownloadsInput.Value = 2
                End If

            ElseIf encoder.Hardware = FfmpegSettings.VideoEncoder.EncoderImplementation.SOFTWARE Then
                If SimultaneousDownloadsInput.Value > 1 Then
                    SimultaneousDownloadsInput.Value = 1
                End If
            End If
        End If

        Main.SoftSubs.Clear()
        Main.SoftSubs.AddRange(CR_SoftSubsTemp)


        Dim SaveString As String = Nothing
        For ii As Integer = 0 To Main.SoftSubs.Count - 1
            If SaveString = Nothing Then
                SaveString = Main.SoftSubs(ii)
            Else
                SaveString = SaveString + "," + Main.SoftSubs(ii)
            End If
        Next
        If SaveString = Nothing Then
            SaveString = "None"
        End If
        My.Settings.AddedSubs = SaveString




        If CB_SoftSubSettings.SelectedIndex = 0 Then
            Main.IncludeLangName = False
            My.Settings.IncludeLangName = Main.IncludeLangName
        Else
            Main.IncludeLangName = True
            My.Settings.IncludeLangName = Main.IncludeLangName
        End If

        If LangNameType_DD.SelectedIndex = 1 Then
            Main.LangNameType = 1
            My.Settings.LangNameType = Main.LangNameType
        ElseIf LangNameType_DD.SelectedIndex = 2 Then
            Main.LangNameType = 2
            My.Settings.LangNameType = Main.LangNameType
        Else
            Main.LangNameType = 0
            My.Settings.LangNameType = Main.LangNameType
        End If

        My.Settings.Save()

        Me.Close()
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


    Private Sub ComboBox1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles CB_CR_Harsubs.DrawItem, CB_Fun_HardSubs.DrawItem, Fun_Dub_Over.DrawItem
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

    Private Sub FfmpegCopyCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles FfmpegCopyCheckBox.CheckedChanged
        UpdateFfmpegInputStates()
        UpdateFfmpegDisplay()
    End Sub

    Private Sub VideoCodecComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles VideoCodecComboBox.SelectedIndexChanged
        Dim selectedItem = CodecTextList.GetEnumForItem(VideoCodecComboBox.SelectedItem)
        If selectedItem = FfmpegSettings.VideoEncoder.Codec.AV1 Then
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
                VideoCodecComboBox.SelectedItem = CodecTextList.Item(FfmpegSettings.VideoEncoder.Codec.H_264)
            End If
        End If

        UpdateFfmpegDisplay()
    End Sub

    Private Sub VideoEncoderComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles VideoEncoderComboBox.SelectedIndexChanged
        UpdateFfmpegDisplay()
    End Sub

    Private Sub FfmpegPresetComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FfmpegPresetComboBox.SelectedIndexChanged
        UpdateFfmpegDisplay()
    End Sub

    Private Sub TargetBitrateCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles TargetBitrateCheckBox.CheckedChanged
        UpdateFfmpegInputStates()
        UpdateFfmpegDisplay()
    End Sub

    Private Sub BitrateNumericInput_ValueChanged(sender As Object, e As EventArgs) Handles BitrateNumericInput.ValueChanged
        UpdateFfmpegDisplay()
    End Sub


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
        GroupBox14.ForeColor = color
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

    Private Sub DarkMode_CheckedChanged(sender As Object, e As EventArgs) Handles DarkMode.CheckedChanged
        ProgramSettings.GetInstance().DarkMode = DarkMode.Checked
    End Sub


    Private Sub Server_Click(sender As Object, e As EventArgs)
        'If Server.Checked = True Then
        '    MsgBox("This feature requires a restart of the downloader", MsgBoxStyle.Information)
        'End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Dim client0 As New WebClient
            client0.Encoding = Encoding.UTF8
            client0.Headers.Add(My.Resources.ffmpeg_user_agend.Replace("""", ""))

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




    Private Sub CB_Fun_HardSubs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Fun_HardSubs.SelectedIndexChanged
        If CB_Fun_HardSubs.SelectedIndex = 0 Then
        Else
            If Main.HardSubFunimation = "Disabled" Then
                If FfmpegCopyCheckBox.Checked Then
                    Dim messageBoxResult =
                        MessageBox.Show("Funimation hard subtitles are post-processed." + vbNewLine +
                        "This will take a lot of resources and it should not do more than one download at a time!" + vbNewLine +
                        "Continue with this setting?",
                        "Prepare for unforeseen consequences.",
                        MessageBoxButtons.YesNo)
                    If messageBoxResult = DialogResult.No Then
                        CB_Fun_HardSubs.SelectedIndex = 0
                    End If
                End If
            End If

        End If

        'MetroMessageBox.Show(Me, "Test", "Test Box", MessageBoxButtons.YesNo, MessageBoxIcon.None, 150)
        'MetroMessageBox.Show(Me, "Test", "Test Box", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, 150, MetroThemeStyle.Dark)

    End Sub

    Private Sub CB_fun_eng_CheckedChanged(sender As Object, e As EventArgs) Handles CB_fun_eng.CheckedChanged
        If CB_fun_eng.Checked = True Then
            FunSubDef.Items.Add("English")
        Else
            FunSubDef.Items.Remove("English")
            If FunSubDef.Text = "English" Then
                FunSubDef.SelectedItem = "[Disabled]"
            End If
        End If
    End Sub

    Private Sub CB_fun_es_CheckedChanged(sender As Object, e As EventArgs) Handles CB_fun_es.CheckedChanged
        If CB_fun_es.Checked = True Then
            FunSubDef.Items.Add("Español (LA)")
        Else
            FunSubDef.Items.Remove("Español (LA)")
            If FunSubDef.Text = "Español (LA)" Then
                FunSubDef.SelectedItem = "[Disabled]"
            End If
        End If
    End Sub

    Private Sub CB_fun_ptbr_CheckedChanged(sender As Object, e As EventArgs) Handles CB_fun_ptbr.CheckedChanged
        If CB_fun_ptbr.Checked = True Then
            FunSubDef.Items.Add("Português (Brasil)")
        Else
            FunSubDef.Items.Remove("Português (Brasil)")
            If FunSubDef.Text = "Português (Brasil)" Then
                FunSubDef.SelectedItem = "[Disabled]"
            End If
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

    Private Sub CB_Anime_CheckedChanged(sender As Object, e As EventArgs) Handles CB_Anime.CheckedChanged, CB_Season.CheckedChanged, CB_EpisodeNR.CheckedChanged, CB_EpisodeName.CheckedChanged, CB_AnimeDub.CheckedChanged, CB_Kodi.CheckedChanged
        If CB_Anime.Checked = True And CBool(InStr(TB_NameString.Text, "AnimeTitle;")) = False Then
            TB_NameString.AppendText("AnimeTitle;")
        ElseIf CB_Anime.Checked = False Then
            TB_NameString.Text = TB_NameString.Text.Replace("AnimeTitle;", "")
        End If

        If CB_Season.Checked = True And CBool(InStr(TB_NameString.Text, "Season;")) = False Then
            TB_NameString.AppendText("Season;")
        ElseIf CB_Season.Checked = False Then
            TB_NameString.Text = TB_NameString.Text.Replace("Season;", "")
        End If

        If CB_EpisodeNR.Checked = True And CBool(InStr(TB_NameString.Text, "EpisodeNR;")) = False Then
            TB_NameString.AppendText("EpisodeNR;")
        ElseIf CB_EpisodeNR.Checked = False Then
            TB_NameString.Text = TB_NameString.Text.Replace("EpisodeNR;", "")
        End If

        If CB_EpisodeName.Checked = True And CBool(InStr(TB_NameString.Text, "EpisodeName;")) = False Then
            TB_NameString.AppendText("EpisodeName;")
        ElseIf CB_EpisodeName.Checked = False Then
            TB_NameString.Text = TB_NameString.Text.Replace("EpisodeName;", "")
        End If

        If CB_AnimeDub.Checked = True And CBool(InStr(TB_NameString.Text, "AnimeDub;")) = False Then
            TB_NameString.AppendText("AnimeDub;")
        ElseIf CB_AnimeDub.Checked = False Then
            TB_NameString.Text = TB_NameString.Text.Replace("AnimeDub;", "")
        End If



    End Sub


    Private Sub DD_Season_Prefix_UserAction(sender As Object, e As EventArgs) Handles DD_Season_Prefix.Click, DD_Season_Prefix.GotFocus
        If DD_Season_Prefix.Text = Main.Season_PrefixDefault Then
            DD_Season_Prefix.Text = Nothing
        End If
    End Sub

    Private Sub DD_Season_Prefix_LostFocus(sender As Object, e As EventArgs) Handles DD_Season_Prefix.LostFocus
        If DD_Season_Prefix.Text = Nothing Then
            DD_Season_Prefix.Text = Main.Season_PrefixDefault
        End If
    End Sub


    Private Sub DD_Episode_Prefix_UserAction(sender As Object, e As EventArgs) Handles DD_Episode_Prefix.Click, DD_Episode_Prefix.GotFocus
        If DD_Episode_Prefix.Text = Main.Episode_PrefixDefault Then
            DD_Episode_Prefix.Text = Nothing
        End If
    End Sub

    Private Sub DD_Episode_Prefix_LostFocus(sender As Object, e As EventArgs) Handles DD_Episode_Prefix.LostFocus
        If DD_Episode_Prefix.Text = Nothing Then
            DD_Episode_Prefix.Text = Main.Episode_PrefixDefault
        End If
    End Sub

    Private Sub CR_SoftSubs_Change(sender As Object, e As EventArgs) Handles CR_SoftSubs.Click
        Dim Popup As New CheckBoxComboBox
        Popup.Text = "CR Sub selection"
        Popup.Show()
    End Sub



    'Private Sub CB_CR_Audio_Click(sender As Object, e As EventArgs) Handles CB_CR_Audio.Click
    '    Dim Popup As New CheckBoxComboBox
    '    Popup.Text = "CR Dub selection"
    '    Popup.Show()
    'End Sub





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