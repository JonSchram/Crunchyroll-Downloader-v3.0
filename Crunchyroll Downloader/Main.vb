Option Strict On
Imports System.Net
Imports System.Text
Imports System.IO
Imports Microsoft.Win32
Imports System.Threading
Imports System.Net.WebUtility
Imports System.Net.Sockets
Imports MetroFramework.Forms
Imports MetroFramework
Imports MetroFramework.Components
Imports System.Globalization
Imports System.ComponentModel
Imports Newtonsoft.Json.Linq
Imports System.Runtime.InteropServices
Imports System.Security.Policy
Imports System.Windows
Imports Microsoft.Web.WebView2.Core
Imports System.Net.Http
Imports Crunchyroll_Downloader.CRD_Classes
Imports System.Drawing.Text
Imports Crunchyroll_Downloader.settings

Public Class Main
    Inherits MetroForm

    Dim t As Thread
    Dim HTML As String = Nothing
    Public CR_Cookies As String = "Cookie: "

    Public CheckCRLogin As Boolean = True


    Public CR_SeasonJson As UrlJson = New UrlJson("", "")
    Public CR_ObjectsJson As UrlJson = New UrlJson("", "")
    Public CR_VideoJson As UrlJson = New UrlJson("", "")
    Public CR_AuthToken As String = ""
    Public CR_v1Token As String = ""


    'Public GetBetaSeasonsRetry As Boolean = False
    'Public GetBetaSeasonSingle As Boolean = False

    Public CR_MassSeasons As New List(Of CR_Seasons)
    Public CR_MassEpisodes As New List(Of CR_Seasons)

    'Public CrBetaMass As String = Nothing
    'Public CrBetaMassEpisodes As String = Nothing
    'Public CrBetaMassParameters As String = Nothing
    'Public CrBetaMassBaseURL As String = Nothing

    Public CrBetaBasic As String = Nothing
    Public locale As String = Nothing
    Public Url_locale As String = Nothing
    Dim ProcessCounting As Integer = 30
    'Public CrBetaObjects As String = Nothing
    'Public CrBetaStreams As String = Nothing
    'Public CrBetaStreamsUrl As String = Nothing
    Public LoadingUrl As String = ""
    Public LoadedUrls As New List(Of CoreWebView2WebResourceRequest)
    Public Manager As New MetroStyleManager
    Public invalids As Char() = System.IO.Path.GetInvalidFileNameChars()
    Dim ServerThread As Thread
    Public KodiNaming As Boolean = False
    Public CookieList As New List(Of CoreWebView2Cookie)
    'Public liList As New List(Of String)
    Public HTMLString As String = My.Resources.Startuphtml
    Public ListBoxList As New List(Of String)
    'Public ItemList As New List(Of CRD_List_Item)
    Public RunningDownloads As Integer = 0
    Public ResoAvalibe As String = Nothing
    Public ResoSearchRunning As Boolean = False
    Public UsedMap As String = Nothing
    Public Debug1 As Boolean = False
    Public Debug2 As Boolean = False
    Public LogBrowserData As Boolean = False
    Public Thumbnail As String = Nothing
    'Public IgnoreS1 As Boolean = False
    Public IgnoreSeason As Integer = 0
    'Public SubsOnly As Boolean = False
    Public DownloadScope As Integer = 0
    Public MergeSubsFormat As String = "mov_text"
    'Public LoginDialog As Boolean = False
    'Public NonCR_Timeout As Integer = 5
    'Public NonCR_URL As String = Nothing
    Public DlSoftSubsRDY As Boolean = True
    Public DialogTaskString As String
    'Public ErrorBrowserString As String
    'Public ErrorBrowserUrl As String
    'Public ErrorBrowserBackString As String
    Public UserCloseDialog As Boolean = False
    Dim Aktuell As String
    Dim Gesamt As String
    Public LabelUpdate As String = "Status: idle"
    Public LabelEpisode As String = "..."
    ' I think this indicates the user is just browsing in the browser
    Public b As Boolean
    Public LoginOnly As String = "False"
    Public Pfad As String = My.Computer.FileSystem.CurrentDirectory
    Public ProfileFolder As String = Path.Combine(Application.StartupPath, "CRD-Profile") 'Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, "CRD-Profile")
    Public Season_Prefix As String = "[default season prefix]"
    Public Season_PrefixDefault As String = "[default season prefix]"
    Public Episode_Prefix As String = "[default episode prefix]"
    Public Episode_PrefixDefault As String = "[default episode prefix]"

    Public ResoSave As String = "6666x6666"
    Public ResoFunBackup As String = "6666x6666"

    Public LangValueEnum As New List(Of NameValuePair)
    Public DubSprache As NameValuePair = New NameValuePair("Japanese", "ja-JP", Nothing)
    Public SubSprache As NameValuePair = New NameValuePair("[ null ]", "", Nothing)

    Public SoftSubs As New List(Of String)
    Public IncludeLangName As Boolean = False
    Public LangNameType As Integer = 0
    Public HybridThread As Integer = CInt(Environment.ProcessorCount / 2 - 1)
    Public TempSoftSubs As New List(Of String)
    Public AbourtList As New List(Of String)
    Public watingList As New List(Of String)
    Public GeckoLogFile As String = Nothing
    Dim SoftSubsString As String
    Dim CR_Unlock_Error As String
    Dim SubSprache2 As String
    'Dim URL_DL As String
    'Dim Pfad_DL As String
    Public Grapp_RDY As Boolean = True
    Public Grapp_non_cr_RDY As Boolean = True
    Public Grapp_Abord As Boolean = False
    Public LeadingZero As Integer = 1
    Public ResoNotFoundString As String
    Public ResoBackString As String
    Public WebbrowserHeadText As String = Nothing
    Public WebbrowserSoftSubURL As String = Nothing
    Public WebbrowserURL As String = Nothing
    Public SystemWebBrowserCookie As String = Nothing
    Public WebbrowserText As String = Nothing
    Public WebbrowserTitle As String = Nothing
    Public WebbrowserCookie As String = Nothing
    Public UserBowser As Boolean = False
    Public HardSubFunimation As String = "Disabled"
    Public Funimation_Bitrate As Integer = 0
    Public DubFunimation As String = "Disabled"
    Public Funimation_srt As Boolean = False
    Public Funimation_vtt As Boolean = False
    Public SubFunimationString As String = "en"
    Public SubFunimation As New List(Of String)
    Public DefaultSubFunimation As String = "Disabled"
    Public DefaultSubCR As String = "Disabled"
    Public DubMode As Boolean = True
    Public CR_Chapters As Boolean = False
#Region "Sprachen Vairablen"
    Public URL_Invaild As String = "something is wrong here..."
    Dim DL_Path_String As String = "Please choose download directory."
    Public No_Stream As String = "Please make sure that the URL is correct or check if the Anime is available in your country."
    Dim TaskNotCompleed As String = "Please wait until the current task is completed."
    Public Premium_Stream As String = "For Premium episodes you need a premium membership and be logged in the Downloader."
    Public LoginReminder As String = "Please make sure that you logged in."
    Dim Error_Mass_DL As String = "We run into a problem here." + vbNewLine + "You can try to download every episode individually."
    Dim User_Fault_NoName As String = "no name, fallback solution : "
    Public Sub_language_NotFound As String = "Could not find the sub language" + vbNewLine + "please make sure the language is available: "
    Public Resolution_NotFound As String = "Could not find any resolution."
    Dim Error_unknown As String = "We run into a unknown problem here." + vbNewLine + "Do you like to send an Bug report?"
    Public ErrorNoPermisson As String = "Access is denied."
    'UI Variablen
    Public GB_Resolution_Text As String = "Resolution"
    Public GB_SubLanguage_Text As String = "Hardsub language"
    Public GB_Sub_Path_Text As String = "Sub directory"
    Public RBAnime_Text As String = "series name"
    Public RBStaffel_Text As String = "series name + season"
    Public NewStart_String As String = "to adopt all the settings, a restart is necessary."
    Public DL_Count_simultaneousText As String = "Simultaneous Downloads"
    Public GB_Sub_FormatText As String = "extended Sub Settings"
    Public LabelResoNotFoundText As String = "resolution not found" + vbNewLine + "Select another one below"
    Public LabelLangNotFoundText As String = "subtitle language not found" + vbNewLine + "Select another one below"
    Public ButtonResoNotFoundText As String = "Submit"
    'Public CB_SuB_Nothing As String = "[ null ]"
    Dim StatusToolTip As ToolTip = New ToolTip()
    Dim StatusToolTipText As String


#End Region

#Region "UI"
    Private Sub Main_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged
        Me.Invalidate()




    End Sub

    Public CloseImg As Bitmap = My.Resources.main_del
    Public MinImg As Bitmap = My.Resources.main_mini
    Public BackColorValue As Color = Color.FromArgb(243, 243, 243)
    Public ForeColorValue As Color = SystemColors.WindowText

    Private Sub HandleDarkModeChanged(isDarkMode As Boolean)
        If isDarkMode Then
            DarkMode()
        Else
            LightMode()
        End If
    End Sub
    Public Sub DarkMode()
        Manager.Theme = MetroThemeStyle.Dark
        Panel1.BackColor = Color.FromArgb(50, 50, 50)
        CloseImg = My.Resources.main_close_dark
        MinImg = My.Resources.main_mini_dark
        Btn_min.Image = MinImg
        Btn_Close.Image = CloseImg
        BackColorValue = Color.FromArgb(50, 50, 50)
        ForeColorValue = Color.FromArgb(243, 243, 243)
    End Sub

    Public Sub LightMode()
        Manager.Theme = MetroThemeStyle.Light
        BackColorValue = Color.FromArgb(243, 243, 243)
        ForeColorValue = SystemColors.WindowText
        Panel1.BackColor = SystemColors.Control
        CloseImg = My.Resources.main_close
        MinImg = My.Resources.main_mini
        Btn_min.Image = MinImg
        Btn_Close.Image = CloseImg
    End Sub

    Dim ListViewHeightOffset As Integer = 7
    Private Sub Btn_add_MouseEnter(sender As Object, e As EventArgs) Handles Btn_add.MouseEnter, Btn_add.GotFocus
        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_add.Image = My.Resources.main_add_invert_dark
        Else
            Btn_add.Image = My.Resources.main_add_invert
        End If
    End Sub

    Private Sub Btn_add_MouseLeave(sender As Object, e As EventArgs) Handles Btn_add.MouseLeave, Btn_add.LostFocus

        Btn_add.Image = My.Resources.main_add
    End Sub

    Private Sub Btn_Browser_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Browser.MouseEnter, Btn_Browser.GotFocus

        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_Browser.Image = My.Resources.main_browser_invert_dark
        Else
            Btn_Browser.Image = My.Resources.main_browser_invert
        End If
    End Sub

    Private Sub Btn_Browser_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Browser.MouseLeave, Btn_Browser.LostFocus
        Btn_Browser.Image = My.Resources.main_browser
    End Sub

    Private Sub Btn_Settings_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Settings.MouseEnter, Btn_Settings.GotFocus
        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_Settings.Image = My.Resources.main_setting_invert_dark
        Else
            Btn_Settings.Image = My.Resources.main_setting_invert
        End If
    End Sub

    Private Sub Btn_Settings_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Settings.MouseLeave, Btn_Settings.LostFocus
        Btn_Settings.Image = My.Resources.main_settings
    End Sub

    Private Sub Btn_Queue_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Queue.MouseEnter, Btn_Queue.GotFocus
        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_Queue.Image = My.Resources.main_queue_invert_dark
        Else
            Btn_Queue.Image = My.Resources.main_queue_invert
        End If
    End Sub

    Private Sub Btn_Queue_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Queue.MouseLeave, Btn_Queue.LostFocus
        Btn_Queue.Image = My.Resources.main_queue
    End Sub



    Private Sub Btn_min_MouseEnter(sender As Object, e As EventArgs) Handles Btn_min.MouseEnter, Btn_min.GotFocus
        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_min.Image = My.Resources.main_mini_dark_hover
        Else
            Btn_min.Image = My.Resources.main_mini_red
        End If
    End Sub

    Private Sub Btn_min_MouseLeave(sender As Object, e As EventArgs) Handles Btn_min.MouseLeave, Btn_min.LostFocus
        Btn_min.Image = MinImg
    End Sub

    Private Sub Btn_Close_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Close.MouseEnter, Btn_Close.GotFocus
        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_Close.Image = My.Resources.main_close_dark_hover
        Else
            Btn_Close.Image = My.Resources.main_close_hover
        End If
    End Sub

    Private Sub Btn_Close_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Close.MouseLeave, Btn_Close.LostFocus
        Btn_Close.Image = CloseImg
    End Sub

    Private Sub ConsoleBar_Click(sender As Object, e As EventArgs) Handles ConsoleBar.Click
        If TheTextBox.Visible = True Then
            'TheTextBox.Lines = DebugList.ToArray
            TheTextBox.Visible = False
            ListViewHeightOffset = 7
            ConsoleBar.Location = New Point(0, Me.Height - ListViewHeightOffset)
            TheTextBox.Location = New Point(1, Me.Height - ListViewHeightOffset + 7)
            TheTextBox.Width = Me.Width - 2
        Else
            ListViewHeightOffset = 103
            TheTextBox.Visible = True
            ConsoleBar.Location = New Point(0, Me.Height - ListViewHeightOffset)
            TheTextBox.Location = New Point(1, Me.Height - ListViewHeightOffset + 7)
            TheTextBox.Width = Me.Width - 2
        End If
        Me.Height = Me.Height + 1
    End Sub

    Private Sub ConsoleBar_MouseEnter(sender As Object, e As EventArgs) Handles ConsoleBar.MouseEnter
        ConsoleBar.BackgroundImage = My.Resources.balken_console
    End Sub

    Private Sub ConsoleBar_MouseLeave(sender As Object, e As EventArgs) Handles ConsoleBar.MouseLeave
        ConsoleBar.BackgroundImage = My.Resources.balken
    End Sub

    Private Sub Main_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Panel1.Width = Me.Width - 2
        Panel1.Height = Me.Height - 71 - ListViewHeightOffset
        PictureBox5.Width = Me.Width - 40
        ConsoleBar.Location = New Point(1, Me.Height - ListViewHeightOffset)
        ConsoleBar.Width = Me.Width - 40
        TheTextBox.Location = New Point(1, Me.Height - ListViewHeightOffset + 7)
        TheTextBox.Width = Me.Width - 2
        Btn_Close.Location = New Point(Me.Width - 36, 1)
        Btn_min.Location = New Point(Me.Width - 67, 1)
        Btn_Settings.Location = New Point(Me.Width - 165, 17)
        Btn_Queue.Location = New Point(Me.Width - 265, 17)
        Try
            Panel1.AutoScrollPosition = New Point(0, 0)

            Dim W As Integer = Panel1.Width
            If Panel1.Controls.Count * 142 > Panel1.Height Then
                W = Panel1.Width - SystemInformation.VerticalScrollBarWidth
            End If

            Dim Item As New List(Of CRD_List_Item)
            Item.AddRange(Panel1.Controls.OfType(Of CRD_List_Item))
            Item.Reverse()

            For s As Integer = 0 To Item.Count - 1
                Item(s).SetBounds(0, 142 * s, W - 2, 142)
                If Debug2 = True Then
                    Debug.WriteLine("Bounds: " + Item(s).GetTextBound.ToString)

                    Debug.WriteLine("Ist: " + Item(s).Location.Y.ToString)
                    Debug.WriteLine("Soll: " + (142 * s).ToString)
                End If
            Next
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub

#End Region
    Public Declare Function waveOutSetVolume Lib "winmm.dll" (ByVal uDeviceID As Integer, ByVal dwVolume As Integer) As Integer
    <FlagsAttribute()>
    Public Enum EXECUTION_STATE As UInteger
        ES_SYSTEM_REQUIRED = &H1
        ES_DISPLAY_REQUIRED = &H2
        ES_CONTINUOUS = &H80000000UI
    End Enum
    <DllImport("Kernel32.DLL", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function SetThreadExecutionState(ByVal state As EXECUTION_STATE) As EXECUTION_STATE
    End Function
    Public Sub SetSettingsTheme()
        Einstellungen.Theme = Manager.Theme
    End Sub





    Function AddLeadingZeros(ByVal txt As String) As String

        txt = txt.Replace(",", ".")
        Dim Post As String = Nothing
        If CBool(InStr(txt, ".")) = True Then
            Dim txt_split As String() = txt.Split(New String() {"."}, System.StringSplitOptions.RemoveEmptyEntries)
            txt = txt_split(0)
            Post = "." + txt_split(1)
        End If

        For i As Integer = 0 To LeadingZero + 1
            If txt.Count = LeadingZero + 1 Or txt.Count > LeadingZero + 1 Then
                Exit For
            Else
                txt = "0" + txt
            End If
        Next

        Dim Output As String = txt + Post

        Return Output
    End Function


    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
#If DEBUG Then
        ' TODO: Might want to programmatically add the debug button so it's not creating it and an event handler in release builds
        DebugButton.Visible = True
#Else
        DebugButton.Visible = false
#End If

        FillArray()

        AddHandler ProgramSettings.DarkModeChanged, AddressOf HandleDarkModeChanged
        Dim settings = ProgramSettings.GetInstance()

        If settings.NeedsUpgrade() Then
            Dim messageBoxResult = MessageBox.Show(
                "Some settings options have changed." + vbNewLine +
                "Do you want to migrate your old settings?" + vbNewLine +
                "Selecting no will discard your old settings.",
                "Upgrade settings",
                MessageBoxButtons.YesNo)
            If messageBoxResult = DialogResult.Yes Then
                settings.UpgradeSettings()
                MessageBox.Show("Done! You may want to review the program settinsg to confirm they are correct.")
            Else
                settings.DiscardOldSettings()
            End If
        End If


#Region "settings path"

        Dim mySettings As New DirectorySettings()
        mySettings.DirectoryName = Application.StartupPath
        mySettings.FileName = "User.config.dat"
        mySettings.Save() ' muss explizit gepeichert werden...

#End Region

        Me.ContextMenuStrip = ContextMenuStrip1
        Dim tbtl As TextBoxTraceListener = New TextBoxTraceListener(TheTextBox)
        Trace.Listeners.Add(tbtl)
        b = True
        Thread.CurrentThread.Name = "Main"
        Debug.WriteLine("Thread Name: " + Thread.CurrentThread.Name)



        Manager.Style = MetroColorStyle.Orange
        If settings.DarkMode Then
            DarkMode()
        Else
            LightMode()
        End If
        Me.StyleManager = Manager
        Manager.Owner = Me

        If ProgramSettings.GetInstance().ServerPort > 0 Then
            Timer3.Enabled = True
            ServerThread = New Thread(AddressOf ServerStart)
            ServerThread.Priority = ThreadPriority.Normal
            ServerThread.IsBackground = True
            ServerThread.Start()
        End If
        waveOutSetVolume(0, 0)

        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        StatusToolTip.Active = True

        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception
        End Try

        If My.Settings.Pfad = Nothing Then
            Pfad = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        Else
            Pfad = My.Settings.Pfad
        End If

        If settings.TemporaryFolder = Nothing Then
            settings.TemporaryFolder = Pfad
        End If

        Episode_Prefix = My.Settings.Prefix_E

        Season_Prefix = My.Settings.Prefix_S

        DefaultSubFunimation = My.Settings.DefaultSubFunimation

        DefaultSubCR = My.Settings.DefaultSubCR

        KodiNaming = My.Settings.KodiSupport

        DubMode = My.Settings.DubMode

        CR_Chapters = My.Settings.CR_Chapters


        LeadingZero = My.Settings.LeadingZero


        For i As Integer = 0 To LangValueEnum.Count - 1
            If LangValueEnum(i).CR_Value = My.Settings.Subtitle Then
                'MsgBox(My.Settings.Subtitle)
                SubSprache = LangValueEnum(i)
                Exit For
            End If
        Next

        For i As Integer = 0 To LangValueEnum.Count - 1
            If LangValueEnum(i).CR_Value = My.Settings.CR_Dub Then
                DubSprache = LangValueEnum(i)
                Exit For
            End If
        Next


        Funimation_Bitrate = My.Settings.Funimation_Bitrate

        SubFolder_Value = My.Settings.SubFolder_Value

        IncludeLangName = My.Settings.IncludeLangName


        LangNameType = My.Settings.LangNameType


        HybridThread = My.Settings.HybridThread

        IgnoreSeason = My.Settings.IgnoreSeason
        Funimation_srt = My.Settings.Funimation_srt
        Funimation_vtt = My.Settings.Funimation_vtt

        DubFunimation = My.Settings.FunimationDub


        HardSubFunimation = "Disabled"


        SoftSubsString = My.Settings.AddedSubs


        If SoftSubsString = "None" Then
        Else
            Dim SoftSubsStringSplit() As String = SoftSubsString.Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To SoftSubsStringSplit.Count - 1
                SoftSubs.Add(SoftSubsStringSplit(i))
            Next
        End If



        SubFunimationString = My.Settings.Fun_Sub

        If SubFunimationString = "None" Then
        Else
            Dim SoftSubsStringSplit() As String = SubFunimationString.Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To SoftSubsStringSplit.Count - 1
                SubFunimation.Add(SoftSubsStringSplit(i))
            Next
        End If



        MergeSubsFormat = My.Settings.MergeSubs


        RetryWithCachedFiles()

        ' TODO: Maybe notify the user that some settings may need to be re-applied because of code changes.


    End Sub



    Public Sub ListItemAdd(ByVal NameKomplett As String, ByVal NameP1 As String, ByVal NameP2 As String, ByVal Reso As String, ByVal HardSub As String, ByVal ThumbnialURL As String, ByVal URL_DL As String, ByVal Pfad_DL As String, Optional Service As String = "CR") ', ByVal AudioLang As String)

        'With ListView1.Items.Add("0")
        'For i As Integer = 0 To 10
        ItemConstructor(NameKomplett, NameP1, NameP2, Reso, HardSub, ThumbnialURL, URL_DL, Pfad_DL, Service)

        'Next
        'End With
    End Sub

    Public Sub ItemConstructor(ByVal NameKomplett As String, ByVal NameP1 As String, ByVal NameP2 As String, ByVal DisplayReso As String, ByVal HardSub As String, ByVal ThumbnialURL As String, ByVal URL_DL As String, ByVal Pfad_DL As String, ByVal Service As String)
        Dim Item As New CRD_List_Item
        Item.Visible = False

        ' TODO: Move item initialization into the constructor or a builder
        Dim settings = ProgramSettings.GetInstance()
        Dim keepCache = settings.DownloadMode = ProgramSettings.DownloadModeOptions.HYBRID_MODE_KEEP_CACHE
        Dim mergeSubs = settings.OutputFormat.GetSubtitleFormat <> Format.SubtitleMerge.DISABLED
#Region "Set Variables"
        Item.SetService(Service)
        Item.SetTolerance(settings.ErrorLimit)
        Item.SetTargetReso(settings.DownloadResolution)
        Item.SetLabelWebsite(NameP1)
        Item.SetLabelAnimeTitel(NameP2)
        Item.SetLabelResolution(DisplayReso)
        Item.SetLabelHardsub(HardSub)
        Item.SetThumbnailImage(ThumbnialURL)
        Item.SetLabelPercent("0%")
        Item.SetCache(keepCache)
        Item.SetMergeSubstoMP4(mergeSubs)
        Item.SetDebug2(Debug2)
#End Region




        Dim W As Integer = Panel1.Width
        If Panel1.Controls.Count * 142 > Panel1.Height Then
            W = Panel1.Width - SystemInformation.VerticalScrollBarWidth
        End If



        Item.SetBounds(0, 142 * Panel1.Controls.Count, W - 2, 142)


        Item.Parent = Panel1
        Panel1.Controls.Add(Item)

        Item.Visible = True
        ' TODO: Support dash MPD files
        Dim TempHybridMode As Boolean = Not ProgramSettings.GetInstance().DownloadMode = ProgramSettings.DownloadModeOptions.FFMPEG
        If CBool(InStr(URL_DL, ".mpd")) Then
            TempHybridMode = False
        End If

        If Pfad_DL.Length > 255 Then
            'MsgBox(Pfad_DL.Length.ToString)
            Pfad_DL = """" + "\\?\" + Pfad_DL.Replace("""", "") + """"
        End If


        'MsgBox(URL_DL + vbNewLine + Pfad_DL + vbNewLine + NameKomplett + vbNewLine + TempHybridMode.ToString)
        Item.StartDownload(URL_DL, Pfad_DL, NameKomplett, TempHybridMode, settings.TemporaryFolder)
    End Sub

#Region "Sub to display"


    Public Function GetSubFileLangName(ByVal HardSub As String) As String

        HardSub = HardSub.Replace("""", "")

        If LangNameType = 1 Then
            Return CCtoMP4CC(HardSub)
        ElseIf LangNameType = 2 Then
            Dim RS As String = HardSubValuesToDisplay(HardSub) + "." + CCtoMP4CC(HardSub)
            Return RS
        Else
            Return HardSubValuesToDisplay(HardSub)
        End If


    End Function
    Public Function HardSubValuesToDisplay(ByVal HardSub As String) As String

        For i As Integer = 0 To LangValueEnum.Count - 1
            If LangValueEnum(i).CR_Value = HardSub Or LangValueEnum(i).FM_Value = HardSub Then
                Return LangValueEnum(i).Name
                Exit Function
            End If
        Next

        Return "Error"

    End Function


    Public Function CCtoMP4CC(ByVal HardSub As String) As String
        Try
            If HardSub = "de-DE" Then
                Return "ger"
            ElseIf HardSub = "en-US" Or HardSub = "en" Then
                Return "eng"
            ElseIf HardSub = "pt-BR" Or HardSub = "pt" Then
                Return "por"
            ElseIf HardSub = "es" Or HardSub = "es-419" Then
                Return "spa"
            ElseIf HardSub = "fr-FR" Then
                Return "fre"
            ElseIf HardSub = "ar-SA" Then
                Return "ara"
            ElseIf HardSub = "ru-RU" Then
                Return "rus"
            ElseIf HardSub = "it-IT" Then
                Return "ita"
            ElseIf HardSub = "es-ES" Then
                Return "spa"
            ElseIf HardSub = "ja-JP" Then
                Return "jpn"
            Else
                Return "chi"
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region

#Region "curl"

    Public Function Curl(ByVal Url As String) As String
        Dim settings As ProgramSettings = ProgramSettings.GetInstance()
        Dim exepath As String = Path.Combine(Application.StartupPath, "lib", "curl.exe")

        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim sr As StreamReader
        Dim sr2 As StreamReader
        Dim cmd As String = ""
        If settings.InsecureCurl Then
            cmd = "--insecure "
        End If
        cmd = cmd + "--no-alpn -fsSLm 15 -A " + My.Resources.ffmpeg_user_agend.Replace("User-Agent: ", "") + " " + """" + Url + """"
        Dim Proc As New Process
        'MsgBox(cmd)
        Dim CurlOutput As String = Nothing
        Dim CurlError As String = Nothing
        ' all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Normal
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        startinfo.StandardOutputEncoding = Encoding.UTF8
        startinfo.StandardErrorEncoding = Encoding.UTF8
        Proc.StartInfo = startinfo
        Proc.Start() ' start the process
        sr = Proc.StandardOutput 'standard error is used by ffmpeg
        sr2 = Proc.StandardError
        'sw = proc.StandardInput

        Dim start, finish, pau As Double
        start = CSng(Microsoft.VisualBasic.DateAndTime.Timer)
        pau = 5
        finish = start + pau

        Do
            CurlOutput = CurlOutput + sr.ReadToEnd
            CurlError = CurlError + sr2.ReadToEnd
            'ffmpegOutput2 = sr.ReadLine
            'Debug.WriteLine(CurlOutput)

        Loop Until Proc.HasExited Or Microsoft.VisualBasic.DateAndTime.Timer < finish


        If CurlOutput = Nothing And CurlError = Nothing Then
            Debug.WriteLine("curl-E: " + "curl: ")
            Return CurlError
        ElseIf CurlOutput = Nothing And CurlError IsNot Nothing Then
            Debug.WriteLine("curl-E: " + CurlError)
            Return CurlError
        ElseIf CurlOutput IsNot Nothing And CurlError = Nothing Then
            Debug.WriteLine("curl-O: " + CurlOutput)
            Return CurlOutput
        ElseIf CurlOutput IsNot Nothing And CurlError IsNot Nothing Then
            Debug.WriteLine("curl-O: " + CurlOutput)
            Return CurlOutput
        Else
            Debug.WriteLine("curl-E: " + "curl: ")
            Return CurlError
        End If


    End Function

    Public Function CurlPost(ByVal Url As String, ByVal Cookies As String, ByVal Auth As String, ByVal Post As String) As String
        Dim settings As ProgramSettings = ProgramSettings.GetInstance()

        Dim exepath As String = Path.Combine(Application.StartupPath, "lib", "curl.exe")

        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim sr As StreamReader
        Dim sr2 As StreamReader


        Dim cmd As String = ""
        If settings.InsecureCurl Then
            cmd = "--insecure "
        End If
        cmd = cmd + "--no-alpn -fsSLm 15 -A " + My.Resources.ffmpeg_user_agend.Replace("User-Agent: ", "") + Cookies + Auth + Post + " " + """" + Url + """"
        Dim Proc As New Process
        'Debug.WriteLine("CurlPost: " + cmd)
        Dim CurlOutput As String = Nothing
        Dim CurlError As String = Nothing
        ' all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Normal
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        startinfo.StandardOutputEncoding = Encoding.UTF8
        startinfo.StandardErrorEncoding = Encoding.UTF8
        Proc.StartInfo = startinfo
        Proc.Start() ' start the process
        sr = Proc.StandardOutput 'standard error is used by ffmpeg
        sr2 = Proc.StandardError
        'sw = proc.StandardInput
        Dim start, finish, pau As Double
        start = CSng(Microsoft.VisualBasic.DateAndTime.Timer)
        pau = 5
        finish = start + pau

        Do
            CurlOutput = CurlOutput + sr.ReadToEnd
            CurlError = CurlError + sr2.ReadToEnd
            'ffmpegOutput2 = sr.ReadLine
            'Debug.WriteLine(CurlOutput)

        Loop Until Proc.HasExited Or Microsoft.VisualBasic.DateAndTime.Timer < finish


        If CurlOutput = Nothing And CurlError = Nothing Then
            Debug.WriteLine("CurlPost-E: " + "curl: ")
            Return CurlError
        ElseIf CurlOutput = Nothing And CurlError IsNot Nothing Then
            Debug.WriteLine("CurlPost-E: " + CurlError)
            Return CurlError
        ElseIf CurlOutput IsNot Nothing And CurlError = Nothing Then
            Debug.WriteLine("CurlPost-O: " + CurlOutput)
            Return CurlOutput
        ElseIf CurlOutput IsNot Nothing And CurlError IsNot Nothing Then
            Debug.WriteLine("CurlPost-O: " + CurlOutput)
            Return CurlOutput
        Else
            Debug.WriteLine("CurlPost-E: " + "curl: ")
            Return CurlError
        End If

    End Function


    Public Function CurlAuth(ByVal Url As String, ByVal Cookies As String, ByVal Auth As String) As String
        ' TODO: Replace curl with HttpWebRequest
        Dim settings As ProgramSettings = ProgramSettings.GetInstance()

        Dim exepath As String = Path.Combine(Application.StartupPath, "lib", "curl.exe")

        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim sr As StreamReader
        Dim sr2 As StreamReader



        Dim cmd As String = ""
        If settings.InsecureCurl Then
            cmd = "--insecure "
        End If
        cmd = cmd + "--no-alpn -fsSLm 15 -A " + My.Resources.ffmpeg_user_agend.Replace("User-Agent: ", "") + Cookies + Auth + " " + """" + Url + """"
        Dim Proc As New Process
        'MsgBox(cmd)
        Dim CurlOutput As String = Nothing
        Dim CurlError As String = Nothing
        ' all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Normal
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        startinfo.StandardOutputEncoding = Encoding.UTF8
        startinfo.StandardErrorEncoding = Encoding.UTF8
        Proc.StartInfo = startinfo
        Proc.Start() ' start the process
        sr = Proc.StandardOutput 'standard error is used by ffmpeg
        sr2 = Proc.StandardError
        'sw = proc.StandardInput

        Dim start, finish, pau As Double
        start = CSng(Microsoft.VisualBasic.DateAndTime.Timer)
        pau = 5
        finish = start + pau

        Do
            CurlOutput = CurlOutput + sr.ReadToEnd
            CurlError = CurlError + sr2.ReadToEnd
            'ffmpegOutput2 = sr.ReadLine
            'Debug.WriteLine(CurlOutput)

        Loop Until Proc.HasExited Or Microsoft.VisualBasic.DateAndTime.Timer < finish

        If CurlOutput = Nothing And CurlError = Nothing Then
            Debug.WriteLine("CurlAuth-E: " + "curl: ")
            Return CurlError
        ElseIf CurlOutput = Nothing And CurlError IsNot Nothing Then
            Debug.WriteLine("CurlAuth-E: " + CurlError)
            Return CurlError
        ElseIf CurlOutput IsNot Nothing And CurlError = Nothing Then
            Debug.WriteLine("CurlAuth-O: " + CurlOutput)
            Return CurlOutput
        ElseIf CurlOutput IsNot Nothing And CurlError IsNot Nothing Then
            Debug.WriteLine("CurlAuth-O: " + CurlOutput)
            Return CurlOutput
        Else
            Debug.WriteLine("CurlAuth-E: " + "curl: ")
            Return CurlError
        End If


    End Function




#End Region


    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        If RunningDownloads > 0 Then
            If MessageBox.Show("Are you sure you want close the program and end all active downloads?", "confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim Item As New List(Of CRD_List_Item)
                Item.AddRange(Panel1.Controls.OfType(Of CRD_List_Item))
                Item.Reverse()

                For i As Integer = 0 To Item.Count - 1
                    Item(i).KillRunningTask()
                Next

                RemoveTempFiles()
                Me.Close()
            End If
        Else
            Timer3.Enabled = False
            RemoveTempFiles()
            Me.Close()
        End If
    End Sub

    Private Sub RemoveTempFiles()
        Try
            Dim files() As String = System.IO.Directory.GetFiles(Application.StartupPath)
            For Each file As String In files
                If CBool(InStr(file, "CRD-Temp-File-")) Or CBool(InStr(file, "-mdata.txt")) Then
                    System.IO.File.Delete(file)
                End If
            Next
        Catch ex As Exception
        End Try
        Dim settings = ProgramSettings.GetInstance()
        If settings.DownloadMode <> ProgramSettings.DownloadModeOptions.HYBRID_MODE_KEEP_CACHE Then
            Try
                Dim di As New System.IO.DirectoryInfo(Pfad)
                For Each fi As System.IO.DirectoryInfo In di.EnumerateDirectories("*.*", System.IO.SearchOption.TopDirectoryOnly)
                    If fi.Attributes.HasFlag(System.IO.FileAttributes.Hidden) Then
                    Else
                        If CBool(InStr(fi.Name, "CRD-Temp-File-")) Then
                            System.IO.Directory.Delete(fi.FullName, True)
                        End If
                    End If
                Next
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub RetryWithCachedFiles()
        Try
            Dim di As New System.IO.DirectoryInfo(Pfad)
            For Each fi As System.IO.DirectoryInfo In di.EnumerateDirectories("*.*", System.IO.SearchOption.TopDirectoryOnly)
                If fi.Attributes.HasFlag(System.IO.FileAttributes.Hidden) Then
                Else
                    If CBool(InStr(fi.Name, "CRD-Temp-File-")) Then
                        If File.Exists(fi.FullName + "\Retry\retry.txt") Then
                            If MessageBox.Show("Cached data found, you can try to retry the download by pressing 'Yes'", "Retry?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                Dim L1Name As String = Nothing
                                Dim L2Name As String = Nothing
                                Dim ResoHTMLDisplay As String = Nothing
                                Dim Subsprache3 As String = Nothing
                                Dim thumbnail3 As String = "file:///" + fi.FullName + "/Retry/retry.jpg"
                                Dim Pfad2 As String = fi.FullName
                                Dim URL2 As String = Nothing
                                Dim Filename As String = Nothing
                                Dim reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader(fi.FullName + "\Retry\retry.txt")
                                Dim a As String
                                For i As Integer = 0 To 5
                                    a = reader.ReadLine
                                    If i = 0 Then
                                        URL2 = a
                                    ElseIf i = 1 Then
                                        L1Name = a
                                    ElseIf i = 2 Then
                                        L2Name = a
                                    ElseIf i = 3 Then
                                        ResoHTMLDisplay = a
                                    ElseIf i = 4 Then
                                        Subsprache3 = a
                                    ElseIf i = 5 Then
                                        Filename = Path.GetFileName(a.Replace("""", ""))
                                    End If
                                Next
                                reader.Close()
                                Me.Invoke(New Action(Function() As Object
                                                         ListItemAdd(Filename, L1Name, L2Name, ResoHTMLDisplay, Subsprache3, thumbnail3, URL2, Pfad2)
                                                         Return Nothing
                                                     End Function))
                                ' liList.Add(My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + L1Name + " <br> " + L2Name + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + L2Name + "-->")
                            Else
                                Grapp_non_cr_RDY = True
                                System.IO.Directory.Delete(fi.FullName, True)
                                Exit Sub
                            End If
                        Else
                            System.IO.Directory.Delete(fi.FullName, True)
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Btn_add_Click(sender As Object, e As EventArgs) Handles Btn_add.Click

        If File.Exists("cookies.txt") = False Then
            If Application.OpenForms().OfType(Of Browser).Any = True Then
            Else
                UserBowser = False
                Browser.Show()
            End If
        End If

        If AddVideo.WindowState = System.Windows.Forms.FormWindowState.Minimized Then
            AddVideo.WindowState = System.Windows.Forms.FormWindowState.Normal
        Else
            AddVideo.OutputPath = Pfad
            AddVideo.OutputSubFolder = My.Settings.SubFolder_Value
            AddVideo.Show()
        End If
    End Sub

    Private Sub Btn_Settings_Click(sender As Object, ByVal e As EventArgs) Handles Btn_Settings.Click
        Einstellungen.Show()
    End Sub

    Private Sub ToggleDebugModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToggleDebugModeToolStripMenuItem.Click
        If Debug2 = True Then
            Debug2 = False
            MsgBox("Debug Mode Disabled")
        Else
            Debug2 = True
            MsgBox("Debug Mode Enabled")
        End If
    End Sub

    Private Sub OpenSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Einstellungen.Show()
    End Sub

    Private Sub Btn_Settings_DoubleClick(sender As Object, e As EventArgs) Handles Btn_Settings.DoubleClick
        Einstellungen.Close()
        If Debug1 = True Then
            If Debug2 = True Then
                Einstellungen.Close()
                Try
                    My.Computer.Clipboard.SetText(WebbrowserText)
                    MsgBox("webbrowser text copyed to the clipboard")
                Catch ex As Exception
                End Try
            Else
                Debug2 = True
                Einstellungen.Close()
                MsgBox("Debug activated")
            End If
        Else
            Debug1 = True
            Einstellungen.Close()
            'MsgBox("Debug activated")
        End If
    End Sub

    Private Sub Btn_Browser_Click(sender As Object, e As EventArgs) Handles Btn_Browser.Click
        Debug.WriteLine(Date.Now.ToString + "." + Date.Now.Millisecond.ToString)
        UserBowser = True

        If Application.OpenForms().OfType(Of Browser).Any = True Then
            Browser.Location = Me.Location
        Else
            Browser.Location = Me.Location
            Browser.Show()
        End If


    End Sub

    Public Function RemoveExtraSpaces(input_text As String) As String
        Dim rsRegEx As System.Text.RegularExpressions.Regex
        rsRegEx = New System.Text.RegularExpressions.Regex("\s+")
        Return rsRegEx.Replace(input_text, " ").Trim()
    End Function



    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            Dim ItemFinshedCount As Integer = 0
            Dim Item As New List(Of CRD_List_Item)
            Item.AddRange(Panel1.Controls.OfType(Of CRD_List_Item))
            Item.Reverse()

            For i As Integer = 0 To Item.Count - 1
                'Debug.WriteLine(Item(i).GetIsStatusFinished().ToString)
                If Item(i).GetIsStatusFinished() = True Then
                    ItemFinshedCount = ItemFinshedCount + 1
                End If
            Next

            RunningDownloads = Item.Count - ItemFinshedCount

            If RunningDownloads > 0 Then
                SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED Or EXECUTION_STATE.ES_CONTINUOUS)
            Else
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS)
            End If
        Catch ex As Exception
            Debug.WriteLine("Failed? : " + ex.ToString)

            RunningDownloads = Panel1.Controls.Count
        End Try
        'Debug.WriteLine("Running: " + RunningDownloads.ToString)

        'FontLabel2.Text = RunningDownloads.ToString
        'Debug.WriteLine("downloads.tick: " + RunningDownloads.ToString)
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick

        Dim Item As New List(Of CRD_List_Item)
        Item.AddRange(Panel1.Controls.OfType(Of CRD_List_Item))
        Item.Reverse()

        Dim GeckoHTML As String = My.Resources.htmlTop + vbNewLine + My.Resources.htmlTitlel.Replace("Placeholder", Me.Text.Replace("open the add window to continue", ""))

        For i As Integer = 0 To Item.Count - 1
            Dim ItemString As String = My.Resources.htmlvorThumbnail + Item(i).GetThumbnailSource + My.Resources.htmlnachTumbnail + Item(i).Label_website.Text + " <br> " + Item(i).Label_Anime.Text + My.Resources.htmlvorAufloesung.Replace("0%", Item(i).Label_percent.Text).Replace("width:0%", Item(i).GetPercentValue.ToString + "%") + Item(i).Label_Reso.Text + My.Resources.htmlvorSoftSubs + vbNewLine + My.Resources.htmlvorHardSubs + Item(i).Label_Hardsub.Text + My.Resources.htmlnachHardSubs
            GeckoHTML = GeckoHTML + vbNewLine + ItemString
        Next



        Dim c As String = GeckoHTML + vbNewLine + My.Resources.htmlEnd
        Dim Balken As String = "balken.png"
        c = c.Replace("balken1.png", Balken)
        Dim CC As String = "cc.png"
        c = c.Replace("cc1.png", CC)
        HTML = c

    End Sub




#Region "process html"
    Public Sub ProcessHTML(ByVal document As String, ByVal Address As String, ByVal DocumentTitle As String)
        Dim localHTML As String = document
        Debug.WriteLine(Date.Now.ToString + "." + Date.Now.Millisecond.ToString)
        Debug.WriteLine(Address)

        If CBool(InStr(Address, "title-api.prd.funimationsvc.com")) Then
            ' TODO: Move this into the downloader or extractor
            'If FunimationJsonBrowser = "EpisodeJson" Then
            '    ' Anime_Add.FillFunimationEpisodes(localHTML.Replace("<body>", "").Replace("</body>", "").Replace("<pre>", "").Replace("</pre>", "").Replace("</html>", "").Replace("<html><head></head><pre style=" + """" + "word-wrap: break-word; white-space: pre-wrap;" + """" + ">", "")) '
            '    FunimationJsonBrowser = Nothing
            '    WebbrowserURL = "https://funimation.com/js"
            'ElseIf FunimationJsonBrowser = "v1Json" Then
            '    GetFunimationNewJS_VideoProxy(Nothing, localHTML.Replace("<body>", "").Replace("</body>", "").Replace("<pre>", "").Replace("</pre>", "").Replace("</html>", "").Replace("<html><head></head><pre style=" + """" + "word-wrap: break-word; white-space: pre-wrap;" + """" + ">", "")) '
            '    FunimationJsonBrowser = Nothing
            '    WebbrowserURL = "https://funimation.com/js"
            'End If
            Exit Sub
        ElseIf CBool(InStr(Address, "/data/v2/shows/")) Then
            ' TODO: Move into extractor or downloader
            'If FunimationJsonBrowser = "SeasonJson" Then
            '    Me.Invoke(New Action(Function() As Object
            '                             'My.Computer.Clipboard.SetText(localHTML)
            '                             FunimationSeasonAPIUrl = Address
            '                             GetFunimationJS_Seasons(Nothing, localHTML.Replace("<body>", "").Replace("</body>", "").Replace("<pre>", "").Replace("</pre>", "").Replace("</html>", "").Replace("<html><head></head><pre style=" + """" + "word-wrap: break-word; white-space: pre-wrap;" + """" + ">", "")) '
            '                             FunimationJsonBrowser = Nothing
            '                             WebbrowserURL = "https://funimation.com/js"
            '                             Return Nothing
            '                         End Function))
            'End If
            Exit Sub
        ElseIf CBool(InStr(Address, "wakanim.tv")) Then
            If CBool(InStr(document, "var tracks = [{" + """" + "file" + """" + ":" + """")) Then
                Dim WakanimSub() As String = document.Split(New String() {"var tracks = [{" + """" + "file" + """" + ":" + """"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim WakanimSub2() As String = WakanimSub(1).Split(New String() {""""}, System.StringSplitOptions.RemoveEmptyEntries)
                Try
                    Using client As New WebClient()
                        client.Encoding = System.Text.Encoding.UTF8
                        client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace("""", ""))
                        Dim SaveName As String = System.Text.RegularExpressions.Regex.Replace(DocumentTitle.Replace(" - Schaue legal auf Wakanim.TV", ""), "[^\w\\-]", " ").Replace(":", "")
                        SaveName = RemoveExtraSpaces(SaveName)
                        client.DownloadFile(WakanimSub2(0), Pfad + "\" + SaveName + ".vtt")
                    End Using
                Catch ex As Exception
                    'Debug.WriteLine("error- getting funimation SeasonJson data")
                    'FunimationJsonBrowser = "SeasonJson"
                    'Navigate(JsonUrl)
                    ''Navigate(JsonUrl)
                    Exit Sub
                End Try
            End If
        End If
        If b = True Then
            LoadedUrls.Clear()
            Grapp_RDY = True
            Debug.WriteLine("Just Browsing, exiting...")
            'Debug.WriteLine("Just Browsing, exiting... for real...")
            Exit Sub
        End If
        'MsgBox("loaded!")
        If CBool(InStr(Address, "crunchyroll.com")) Or CBool(InStr(Address, "funimation.com")) Then
            WebbrowserURL = Address

            ScanTimeout.Start()


            'ElseIf CBool(InStr(Address, "funimation.com")) Then

            '    Dim list As List(Of CoreWebView2Cookie) = Await Browser.WebView2.CoreWebView2.CookieManager.GetCookiesAsync("https://www.funimation.com")
            '    Dim Cookie As String = ""
            '    For i As Integer = 0 To list.Count - 1
            '        If CBool(InStr(list.Item(i).Domain, "funimation.com")) Then 'list.Item(i).Domain = "funimation.com" Then
            '            'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)
            '            Cookie = Cookie + list.Item(i).Name + "=" + list.Item(i).Value + ";"
            '        End If
            '        If CBool(InStr(list.Item(i).Domain, "funimation.com")) And CBool(InStr(list.Item(i).Name, "src_token")) Then 'list.Item(i).Domain = "funimation.com" Then
            '            'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)
            '            FunimationToken = "Token " + list.Item(i).Value
            '        End If
            '    Next
            '    If b = False Then
            '        WebbrowserCookie = Cookie
            '        WebbrowserURL = Address
            '        Text = "Crunchyroll Downloader"
            '        For i As Integer = 10 To 0 Step -1
            '            Anime_Add.StatusLabel.Text = "Status: checking traffic - " + i.ToString
            '            Pause(1)
            '        Next
            '        Dim Evaluator = New Thread(Sub() Me.ProcessUrls())
            '        Evaluator.Start()
            '        Exit Sub
            '    End If
            'Else
            '    WebbrowserURL = Address
            '    Text = "Crunchyroll Downloader"
            '    For i As Integer = 10 To 0 Step -1
            '        Anime_Add.StatusLabel.Text = "Status: checking traffic - " + i.ToString
            '        Pause(1)
            '    Next
            '    ProcessUrls()
            '    'Pause(10)
            '    'ProcessUrls()
        End If
        'End If
    End Sub




#End Region

    Private Sub Process(sender As Object, e As EventArgs) Handles ScanTimeout.Tick
        If b = True Then
            ' TODO
            'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
            ' Anime_Add.StatusLabel.Text = "Status: idle"
            'End If
            Me.Text = "Crunchyroll Downloader"
            Grapp_RDY = True
            LoadedUrls.Clear()
            Debug.WriteLine("canceled....")
            ProcessCounting = 30
            ScanTimeout.Enabled = False
            Exit Sub
        End If

        If LoadedUrls.Count = 0 And ProcessCounting > 0 Then
            ' TODO
            'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
            '    Anime_Add.StatusLabel.Text = "Status: Processing Url " + ProcessCounting.ToString
            'End If
            Me.Text = "Status: Processing Url " + ProcessCounting.ToString

            ProcessCounting = ProcessCounting - 1
            Exit Sub
        ElseIf LoadedUrls.Count = 1 And ProcessCounting > 0 Then

            If CBool(InStr(LoadedUrls.Item(0).Uri, "/objects/")) Then
                ' TODO
                'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                '    Anime_Add.StatusLabel.Text = "Status: Processing Url " + ProcessCounting.ToString
                'End If
                Me.Text = "Status: Processing Url " + ProcessCounting.ToString

                ProcessCounting = ProcessCounting - 1
                Exit Sub
            End If

        ElseIf LoadedUrls.Count = 0 And ProcessCounting = 0 Then
            ' TODO
            'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
            '    Anime_Add.StatusLabel.Text = "Status: nothing found"
            'End If
            Me.Text = "Status: nothing found"
            'ProcessUrls()
            b = True
            Debug.WriteLine("3508: nothing found")
            Grapp_RDY = True
            ProcessCounting = 30
            ScanTimeout.Enabled = False
            Exit Sub
        End If


        Debug.WriteLine("LoadedUrls: " + LoadedUrls.Count.ToString)
        ' TODO
        'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
        '    Anime_Add.StatusLabel.Text = "Status: Processing... "
        'End If
        Me.Text = "Status: Processing... "
        Debug.WriteLine("ProcessUrls")
        ProcessCounting = 30
        ScanTimeout.Enabled = False
        ProcessUrls()

        Exit Sub



    End Sub
    Public Sub ProcessUrls()
        Debug.WriteLine(LoadedUrls.Count.ToString)
        Debug.WriteLine(Date.Now.ToString + " Thread Name: " + Thread.CurrentThread.Name)
        Dim SavedObjectsUrl = ""
        For i As Integer = 0 To LoadedUrls.Count - 1

            Dim Request As CoreWebView2WebResourceRequest = LoadedUrls.Item(i)


            If CBool(InStr(Request.Uri, "crunchyroll.com/")) And CBool(InStr(Request.Uri, "streams?")) Then

                If b = False Then

                    ' TODO
                    'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                    '    Anime_Add.StatusLabel.Text = "Status: Crunchyroll episode found."
                    'End If
                    Me.Text = "Status: Crunchyroll episode found."
                    Debug.WriteLine("Crunchyroll episode found")
                    ' TODO
                    'GetBetaVideoProxy(Request.Uri, CR_AuthToken, WebbrowserURL)
                    b = True
                    LoadedUrls.Clear()
                    Me.Text = "Crunchyroll Downloader"
                    Exit Sub
                End If

            ElseIf CBool(InStr(Request.Uri, "crunchyroll.com/")) And CBool(InStr(Request.Uri, "seasons?preferred_audio_language=")) And CBool(InStr(WebbrowserURL, "series")) Then

                If b = False Then
                    ' TODO
                    'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                    '    Anime_Add.StatusLabel.Text = "Status: Crunchyroll season found."
                    'End If
                    Me.Text = "Status: Crunchyroll season found."
                    Debug.WriteLine("Crunchyroll season found")

                    Dim Auth As String = " -H " + """" + "Authorization: " + Request.Headers.GetHeader("Authorization") + """"
                    Debug.WriteLine(Auth)

                    CR_Cookies = "Cookie: " + Request.Headers.GetHeader("Cookie")

                    ' TODO
                    'GetBetaSeasons(WebbrowserURL, Request.Uri, Auth)

                    b = True
                    LoadedUrls.Clear()
                    Me.Text = "Crunchyroll Downloader"
                    Exit Sub
                End If
            ElseIf CBool(InStr(Request.Uri, "crunchyroll.com/")) And CBool(InStr(Request.Uri, "seasons?series_id=")) Then

                If b = False Then
                    ' TODO
                    'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                    '    Anime_Add.StatusLabel.Text = "Status: Error found invalid data."
                    'End If
                    b = True
                    LoadedUrls.Clear()
                    Me.Text = "Crunchyroll Downloader"
                    Exit Sub
                End If
            ElseIf CBool(InStr(Request.Uri, "crunchyroll.com/")) And CBool(InStr(Request.Uri, "/objects/")) Then
                If i = LoadedUrls.Count - 1 And SavedObjectsUrl IsNot "" Then

                    If b = False Then
                        Dim ObjectJson As String
                        Dim ObjectsUrl As String = Request.Uri
                        Dim StreamsUrl As String
                        ObjectJson = Curl(ObjectsUrl)

                        If CBool(InStr(ObjectJson, "curl:")) = True Then
                            ObjectJson = Curl(ObjectsUrl)
                        End If

                        If CBool(InStr(ObjectJson, "curl:")) = True Then
                            Continue For
                        ElseIf CBool(InStr(ObjectJson, "videos/")) = False Then

                            ' TODO
                            'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                            '    Anime_Add.StatusLabel.Text = "Status: Failed, check CR login"
                            'End If
                            Me.Text = "Status: Failed, check CR login"
                            Debug.WriteLine("Status: Failed, check CR login")

                            Continue For
                        End If




                        Dim StreamsUrlBuilder() As String = ObjectJson.Split(New String() {"videos/"}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim StreamsUrlBuilder2() As String = StreamsUrlBuilder(1).Split(New String() {"/streams"}, System.StringSplitOptions.RemoveEmptyEntries)

                        Dim StreamsUrlBuilder3() As String = ObjectsUrl.Split(New String() {"objects/"}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim StreamsUrlBuilder4() As String = StreamsUrlBuilder3(1).Split(New String() {"?"}, System.StringSplitOptions.RemoveEmptyEntries)

                        StreamsUrl = StreamsUrlBuilder3(0) + "videos/" + StreamsUrlBuilder2(0) + "/streams?" + StreamsUrlBuilder4(1)

                        ' TODO
                        'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                        '    Anime_Add.StatusLabel.Text = "Status: Crunchyroll episode found."
                        'End If
                        Me.Text = "Status: Crunchyroll episode found."
                        Debug.WriteLine("Crunchyroll episode found")
                        Dim Headers As New List(Of KeyValuePair(Of String, String))
                        Headers.AddRange(Request.Headers.ToList)
                        Dim AuthToken As String = ""
                        For ii As Integer = 0 To Headers.Count
                            If CBool(InStr(Headers.Item(i).Value, "Bearer")) Then
                                AuthToken = Headers.Item(i).Value
                            End If
                        Next
                        ' TODO
                        ' GetBetaVideoProxy(StreamsUrl, AuthToken, WebbrowserURL)
                        b = True
                        LoadedUrls.Clear()
                        Me.Text = "Crunchyroll Downloader"
                        Exit Sub
                    End If
                Else
                    SavedObjectsUrl = Request.Uri
                End If

            End If

            If CBool(InStr(Request.Uri, "/data/v2/shows/")) Then
                b = True
                ' TODO
                Me.Invoke(New Action(Function() As Object
                                         'GetFunimationJS_Seasons(Request.Uri)
                                         WebbrowserURL = "https://funimation.com/js"
                                         Return Nothing
                                     End Function))
                LoadedUrls.Clear()
                Me.Text = "Crunchyroll Downloader"
                Exit Sub
            End If
            If CBool(InStr(Request.Uri, "data/v1/episodes/")) Then
                b = True
                ' TODO
                Me.Invoke(New Action(Function() As Object
                                         'GetFunimationNewJS_VideoProxy(Request.Uri)
                                         WebbrowserURL = "https://funimation.com/js"
                                         Return Nothing
                                     End Function))
                LoadedUrls.Clear()
                Me.Text = "Crunchyroll Downloader"
                Exit Sub
            End If
            If CBool(InStr(Request.Uri, "https://title-api.prd.funimationsvc.com")) And CBool(InStr(Request.Uri, "?region=")) Then
                ' TODO: Move into downloader
                'If FunimationAPIRegion = Nothing Then
                '    Me.Invoke(New Action(Function() As Object
                '                             Dim parms As String() = Request.Uri.Split(New String() {"?region="}, System.StringSplitOptions.RemoveEmptyEntries)
                '                             FunimationAPIRegion = "?region=" + parms(1)
                '                             Return Nothing
                '                         End Function))
                'End If
                If b = False Then

                    If CBool(InStr(Request.Uri, "https://title-api.prd.funimationsvc.com/v1/show")) And CBool(InStr(Request.Uri, "/episodes/")) Then
                        b = True
                        ' TODO
                        ' GetFunimationNewJS_VideoProxy(Request.Uri)
                        Debug.WriteLine("processing :" + Request.Uri)
                        LoadedUrls.Clear()
                        Me.Text = "Crunchyroll Downloader"
                        Exit Sub
                    End If
                End If
            End If
        Next

        LoadedUrls.Clear()

        If b = True Then
            LoadedUrls.Clear()
            Debug.WriteLine("Just Browsing after all, exiting...")
            Grapp_RDY = True
            Me.Text = "Crunchyroll Downloader"
            Exit Sub
        End If

    End Sub

    Public Sub Navigate(ByVal Url As String)
        If Application.OpenForms().OfType(Of Browser).Any = True Then
            If InvokeRequired = True Then
                Me.Invoke(New Action(Function() As Object
                                         Browser.WebView2.CoreWebView2.Navigate(Url)
                                         Return Nothing
                                     End Function))
            Else
                Browser.WebView2.CoreWebView2.Navigate(Url)
            End If
        Else
            If InvokeRequired = True Then
                Me.Invoke(New Action(Function() As Object
                                         Browser.Show()
                                         Browser.WebView2.CoreWebView2.Navigate(Url)
                                         Return Nothing
                                     End Function))
            Else
                Browser.Show()
                Browser.WebView2.CoreWebView2.Navigate(Url)
            End If
        End If
    End Sub

#Region "server"
    ' TODO: Probably want a separate server class
    ' It would need a downloader too
    Dim ListOfThread As New List(Of Thread)
    Sub ServerStart()
        Dim server As TcpListener
        server = Nothing
        Try
            Dim Port As Integer = ProgramSettings.GetInstance().ServerPort
            server = New TcpListener(IPAddress.Loopback, Port)
            ' Start listening for client requests.
            server.Start()
            Debug.WriteLine("Web server started at: " & IPAddress.Loopback.ToString() & ":" & Port)
            While True
                Dim client As TcpClient = server.AcceptTcpClient()
                Dim clientThread As New Thread(Sub() Me.ManageConnections(client))
                clientThread.Start()
            End While
        Catch ex As SocketException
            Debug.WriteLine("SocketException: " + ex.ToString)
        Finally
            Debug.WriteLine(Date.Now.ToString + " " + "End server")
            server.Stop()
        End Try
        Debug.WriteLine(ControlChars.Cr + "Hit enter to continue....")
    End Sub

    Sub ManageConnections(ByVal client As TcpClient)
        Dim bytes(1048576) As Byte
        Dim stream As NetworkStream = client.GetStream()
        ' Debug.WriteLine(Date.Now + " " + "stream opend")
        Dim numberOfBytesRead As Integer = 0
        Dim myCompleteMessage As StringBuilder = New StringBuilder()
        Dim stopWatch As New Stopwatch()
        stopWatch.Start()
        Do While stopWatch.Elapsed.TotalSeconds < 4 And stream.DataAvailable
            'Debug.WriteLine(Date.Now + " " + numberOfBytesRead.ToString + " " + stopWatch.Elapsed.TotalSeconds.ToString)
            numberOfBytesRead = stream.Read(bytes, 0, bytes.Length)
            myCompleteMessage.AppendFormat("{0}", Encoding.UTF8.GetString(bytes, 0, numberOfBytesRead))
        Loop
        stopWatch.Stop()
        ProcessRequest(stream, myCompleteMessage.ToString())
        client.Close()
    End Sub

    Sub ProcessRequest(ByVal stream As NetworkStream, ByVal htmlReq As String)
        Debug.WriteLine(htmlReq)
        ' Dim recvBytes(1048576) As Byte
        Try
            Dim rootPath As String = Directory.GetCurrentDirectory() & "\WebInterface\"
            ' Set default page
            Dim defaultPage As String = "index.html"
            Dim PostPage As String = "post.html"
            Dim strArray() As String
            Dim strRequest As String
            strArray = htmlReq.Trim.Split(New String() {" "}, System.StringSplitOptions.RemoveEmptyEntries)
            'MsgBox(htmlReq)
            If strArray(0).Trim().ToUpper.Equals("POST") Then
                'Debug.WriteLine("receiving data from the add-on")
                'Debug.WriteLine(htmlReq)
                'UrlDecode
                Me.Invoke(New Action(Function() As Object
                                         Me.Text = "Status: receiving data from the add-on"
                                         Me.Invalidate()
                                         Return Nothing
                                     End Function))

#Region "mass-dl"


                If CBool(InStr(htmlReq, "HTMLMass=")) Then
                    Debug.WriteLine("multi episode mode")
                    Try
                        Dim html() As String = htmlReq.Split(New String() {"HTMLMass="}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim DecodedHTML As String = UrlDecode(html(1))
                        Dim URLSplit() As String = DecodedHTML.Split(New String() {"javascript:"}, System.StringSplitOptions.RemoveEmptyEntries)
                        'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                        '    For i As Integer = 0 To URLSplit.Count - 1
                        '        Dim ii As Integer = i
                        '        Me.Invoke(New Action(Function() As Object
                        '                                 If Anime_Add.ListBox1.Items.Contains(URLSplit(ii)) = False Then
                        '                                     Anime_Add.ListBox1.Items.Add(URLSplit(ii))
                        '                                 End If
                        '                                 'Anime_Add.ListBox1.Items.Add(URLSplit(ii))
                        '                                 Return Nothing
                        '                             End Function))
                        '    Next
                        'Else
                        For i As Integer = 0 To URLSplit.Count - 1
                            If ListBoxList.Contains(URLSplit(i)) = False Then
                                ListBoxList.Add(URLSplit(i))
                            End If
                        Next
                        Me.Invoke(New Action(Function() As Object
                                                 Me.Text = "Status: " + ListBoxList.Count.ToString + " downloads in queue" + vbNewLine + "open the add window to continue"
                                                 Me.Invalidate()
                                                 Return Nothing
                                             End Function))
                        'End If
                        strRequest = rootPath & "Post_Mass_Sucess.html" 'PostPage
                        SendHTMLResponse(stream, strRequest)
                    Catch abort As ThreadAbortException
                        Exit Sub
                    Catch ex As Exception
                        Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
                        'My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
                        'strRequest = rootPath & "error_Page.html" 'PostPage
                        'SendHTMLResponse(stream, strRequest)
                        SendHTMLResponse(stream, Nothing, New ServerResponse(ErrorPage, "html"))

                    End Try
#End Region
#Region "Funimation-mass"
                ElseIf CBool(InStr(htmlReq, "FunimationMass=")) Then
                    Debug.WriteLine("Funimation multi episode mode")
                    Try
                        Dim DecodedHTML As String = UrlDecode(htmlReq)
                        If CBool(InStr(DecodedHTML, "&FunimationCookie=")) Then
                            Dim CookieSplit() As String = DecodedHTML.Split(New String() {"&FunimationCookie="}, System.StringSplitOptions.RemoveEmptyEntries)
                            SystemWebBrowserCookie = CookieSplit(1)
                            Dim URLSplit() As String = CookieSplit(0).Split(New String() {"FunimationMass="}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim URLSplit2() As String = URLSplit(1).Split(New String() {"javascript:"}, System.StringSplitOptions.RemoveEmptyEntries)
                            'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                            '    For i As Integer = 0 To URLSplit2.Count - 1
                            '        Dim ii As Integer = i
                            '        Me.Invoke(New Action(Function() As Object
                            '                                 If Anime_Add.ListBox1.Items.Contains(URLSplit2(ii)) = False Then
                            '                                     Anime_Add.ListBox1.Items.Add(URLSplit2(ii))
                            '                                 End If
                            '                                 'Anime_Add.ListBox1.Items.Add(URLSplit(ii))
                            '                                 Return Nothing
                            '                             End Function))
                            '    Next
                            'Else
                            For i As Integer = 0 To URLSplit2.Count - 1
                                If ListBoxList.Contains(URLSplit2(i)) = False Then
                                    ListBoxList.Add(URLSplit2(i))
                                End If
                            Next
                            Me.Invoke(New Action(Function() As Object
                                                     Me.Text = "Status: " + ListBoxList.Count.ToString + " downloads in queue" + vbNewLine + "open the add window to continue"
                                                     Me.Invalidate()
                                                     Return Nothing
                                                 End Function))
                            'End If
                            strRequest = rootPath & "Post_Mass_Sucess.html" 'PostPage
                            SendHTMLResponse(stream, strRequest)
                        End If
                    Catch abort As ThreadAbortException
                        Exit Sub
                    Catch ex As Exception
                        Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
                        'My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
                        'strRequest = rootPath & "error_Page.html" 'PostPage
                        'SendHTMLResponse(stream, strRequest)
                        SendHTMLResponse(stream, Nothing, New ServerResponse(ErrorPage, "html"))

                    End Try
#End Region
#Region "funimation Einzeln"
                ElseIf CBool(InStr(htmlReq, "FunimationURL=")) Then
                    Debug.WriteLine("single episode mode - Funimation")
                    'MsgBox(htmlReq)
                    Me.Invoke(New Action(Function() As Object
                                             Me.Text = "Status: Download added from add-on"
                                             Me.Invalidate()
                                             Return Nothing
                                         End Function))
                    Try
                        Dim URLSplit() As String = htmlReq.Split(New String() {"FunimationURL="}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim URLSplit2() As String = URLSplit(1).Split(New String() {"&FunimationCookie="}, System.StringSplitOptions.RemoveEmptyEntries)
                        SystemWebBrowserCookie = URLSplit2(1)
                        WebbrowserURL = UrlDecode(URLSplit2(0))
                        If CBool(InStr(WebbrowserURL, "funimation.com")) Then
                            If DubFunimation = "Disabled" Then
                            Else
                                If CBool(InStr(WebbrowserURL, "?lang=")) Then
                                    Dim ClearUri As String() = WebbrowserURL.Split(New String() {"?lang="}, System.StringSplitOptions.RemoveEmptyEntries)
                                    If ClearUri.Count > 1 Then
                                        If CBool(InStr(ClearUri(1), "&")) Then
                                            Dim ClearUri2 As String() = ClearUri(1).Split(New String() {"&"}, System.StringSplitOptions.RemoveEmptyEntries)
                                            Dim Parms As String = Nothing
                                            For i As Integer = 1 To ClearUri2.Count - 1
                                                Parms = Parms + "&" + ClearUri2(i)
                                            Next
                                            WebbrowserURL = ClearUri(0) + "?lang=" + DubFunimation + Parms
                                        Else
                                            WebbrowserURL = ClearUri(0) + "?lang=" + DubFunimation
                                        End If
                                    Else
                                        WebbrowserURL = ClearUri(0) + "?lang=" + DubFunimation
                                    End If
                                ElseIf CBool(InStr(WebbrowserURL, "&lang=")) Then
                                    Dim ClearUri As String() = WebbrowserURL.Split(New String() {"&lang="}, System.StringSplitOptions.RemoveEmptyEntries)
                                    If ClearUri.Count > 1 Then
                                        If CBool(InStr(ClearUri(1), "&")) Then
                                            Dim ClearUri2 As String() = ClearUri(1).Split(New String() {"&"}, System.StringSplitOptions.RemoveEmptyEntries)
                                            Dim Parms As String = Nothing
                                            For i As Integer = 1 To ClearUri2.Count - 1
                                                Parms = Parms + "&" + ClearUri2(i)
                                            Next
                                            WebbrowserURL = ClearUri(0) + "&lang=" + DubFunimation + Parms
                                        Else
                                            WebbrowserURL = ClearUri(0) + "&lang=" + DubFunimation
                                        End If
                                    Else
                                        WebbrowserURL = ClearUri(0) + "&lang=" + DubFunimation
                                    End If
                                ElseIf CBool(InStr(WebbrowserURL, "?")) Then
                                    WebbrowserURL = WebbrowserURL + "&lang=" + DubFunimation
                                Else
                                    WebbrowserURL = WebbrowserURL + "?lang=" + DubFunimation
                                End If
                            End If
                        End If
                        ' TODO
                        'If Funimation_Grapp_RDY = True Then
                        '    If RunningDownloads >= MaxDL Then
                        '        If ListBoxList.Contains(WebbrowserURL) = False Then
                        '            ListBoxList.Add(WebbrowserURL)
                        '        End If
                        '        'ListBoxList.Add(WebbrowserURL)
                        '    Else
                        '        Me.Invoke(New Action(Function() As Object
                        '                                 Navigate(WebbrowserURL)
                        '                                 Return Nothing
                        '                             End Function))
                        '        b = False
                        '    End If
                        'Else
                        '    'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                        '    '    Me.Invoke(New Action(Function() As Object
                        '    '                             If Anime_Add.ListBox1.Items.Contains(WebbrowserURL) = False Then
                        '    '                                 Anime_Add.ListBox1.Items.Add(WebbrowserURL)
                        '    '                             End If
                        '    '                             Return Nothing
                        '    '                         End Function))
                        '    'Else
                        '    If ListBoxList.Contains(WebbrowserURL) = False Then
                        '        ListBoxList.Add(WebbrowserURL)
                        '    End If
                        '    Me.Invoke(New Action(Function() As Object
                        '                             Me.Text = "Status: " + ListBoxList.Count.ToString + " downloads in queue"
                        '                             Me.Invalidate()
                        '                             Return Nothing
                        '                         End Function))
                        '    'End If
                        'End If
                        strRequest = rootPath & "Post_Single_Sucess.html" 'PostPage
                        SendHTMLResponse(stream, strRequest)
                    Catch abort As ThreadAbortException
                        Exit Sub
                    Catch ex As Exception
                        Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
                        'My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
                        'strRequest = rootPath & "error_Page.html" 'PostPage
                        'SendHTMLResponse(stream, strRequest)
                        SendHTMLResponse(stream, Nothing, New ServerResponse(ErrorPage, "html"))

                    End Try
#End Region
                Else
                    strRequest = rootPath & "error_Page_default.html" 'PostPage
                    SendHTMLResponse(stream, strRequest)
                End If
            ElseIf strArray(0).Trim().ToUpper.Equals("GET") Then
                strRequest = strArray(1).Trim

                If CBool(InStr(strRequest, "403")) Then
                    strRequest = strRequest & defaultPage '"HTMLString" 'strRequest & defaultPage
                    SendHTMLResponse(stream, "index.html", Nothing, "HTTP/1.0 403 Forbidden")
                End If

                If strRequest.StartsWith("/") Then
                    strRequest = strRequest.Substring(1)
                End If
                If strRequest.EndsWith("/") Or strRequest.Equals("") Then
                    'Debug.WriteLine(Date.Now + " " + "it's index.html")
                    strRequest = strRequest & defaultPage '"HTMLString" 'strRequest & defaultPage
                End If

                strRequest = rootPath & strRequest
                SendHTMLResponse(stream, strRequest)
            Else ' Not HTTP GET method

                strRequest = rootPath & defaultPage
                SendHTMLResponse(stream, strRequest)
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            Dim ErrorPage As String = My.Resources.Post_error_Top + ex.ToString + My.Resources.Post_error_Bottom
            ' My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\WebInterface\error_Page.html", ErrorPage, False)
            SendHTMLResponse(stream, Nothing, New ServerResponse(ErrorPage, "html"))

        End Try
    End Sub

    ' Send HTTP Response
    Private Sub SendHTMLResponse(ByVal stream As NetworkStream, Optional ByVal httpRequest As String = Nothing, Optional ByVal Response As ServerResponse = Nothing, Optional ByVal httpCode As String = "HTTP/1.0 200 OK")
        Try
            Dim respByte() As Byte
            If httpRequest = Nothing Then
                Debug.WriteLine(httpRequest)
                respByte = System.Text.Encoding.UTF8.GetBytes(Response.Content) 'File.ReadAllBytes("") '
                ' Set HTML Header
                Dim htmlHeader As String =
                   httpCode & ControlChars.CrLf &
                    "Server: CRD 1.0" & ControlChars.CrLf &
                   "Content-Length: " & respByte.Length & ControlChars.CrLf &
                    "Content-Type: " & GetContentType(Response.Type) &
                    ControlChars.CrLf & ControlChars.CrLf
                ' The content Length of HTML Header
                Dim headerByte() As Byte = Encoding.UTF8.GetBytes(htmlHeader)
                stream.Write(headerByte, 0, headerByte.Length)
                stream.Write(respByte, 0, respByte.Length)

            ElseIf CBool(InStr(httpRequest, "index.html")) Then
                Debug.WriteLine(httpRequest)
                respByte = System.Text.Encoding.UTF8.GetBytes(HTML) 'File.ReadAllBytes("") '
                ' Set HTML Header
                Dim htmlHeader As String =
                    httpCode & ControlChars.CrLf &
                    "Server: CRD 1.0" & ControlChars.CrLf &
                   "Content-Length: " & respByte.Length & ControlChars.CrLf &
                    "Content-Type: " & GetContentType(httpRequest) &
                    ControlChars.CrLf & ControlChars.CrLf
                ' The content Length of HTML Header
                Dim headerByte() As Byte = Encoding.UTF8.GetBytes(htmlHeader)
                'Debug.WriteLine("HTML Header: " & ControlChars.CrLf & htmlHeader)
                ' Send HTML Header back to Web Browser
                'Dim response() As Byte = headerByte.Concat(respByte).ToArray()
                ' stream.Write(response, 0, response.Length)
                'Debug.WriteLine("sending headers")
                stream.Write(headerByte, 0, headerByte.Length)
                'Debug.WriteLine("headers send")
                'Debug.WriteLine("sending content")
                ' Send HTML Content back to Web Browser
                stream.Write(respByte, 0, respByte.Length)
                'clientSocket.Send(respByte, 0, respByte.Length, SocketFlags.None)
                ' Close HTTP Socket connection
                'Debug.WriteLine("content send")
            ElseIf File.Exists(httpRequest) Then
                Debug.WriteLine(httpRequest)
                respByte = File.ReadAllBytes(httpRequest)
                ' Set HTML Header
                Dim htmlHeader As String =
                    httpCode & ControlChars.CrLf &
                    "Server: CRD 1.0" & ControlChars.CrLf &
                   "Content-Length: " & respByte.Length & ControlChars.CrLf &
                    "Content-Type: " & GetContentType(httpRequest) & ControlChars.CrLf &
                    "Connection: close" &
                    ControlChars.CrLf & ControlChars.CrLf
                ' The content Length of HTML Header
                Dim headerByte() As Byte = Encoding.UTF8.GetBytes(htmlHeader)
                ' Send HTML Header back to Web Browser
                stream.Write(headerByte, 0, headerByte.Length)
                ' Send HTML Content back to Web Browser
                stream.Write(respByte, 0, respByte.Length)
            ElseIf httpRequest = "Handshake_Confirm" Then
                respByte = System.Text.Encoding.UTF8.GetBytes("CRD_Handshake_Confirm") 'File.ReadAllBytes("") '
                Dim htmlHeader As String =
                    "HTTP/1.0 200 OK" & ControlChars.CrLf &
                    "Server: CRD 1.0" & ControlChars.CrLf &
                    "Access-Control-Allow-Origin: *" & ControlChars.CrLf &
                    "Content-Length: " & respByte.Length & ControlChars.CrLf &
                    "Content-Type: text/plain" &
                    "Connection: close" &
                      ControlChars.CrLf & ControlChars.CrLf
                Dim headerByte() As Byte = Encoding.UTF8.GetBytes(htmlHeader)
                stream.Write(headerByte, 0, headerByte.Length)
                ' Send HTML Content back to Web Browser
                stream.Write(respByte, 0, respByte.Length)
                Debug.WriteLine("content send")
            Else
                respByte = Encoding.UTF8.GetBytes(My.Resources.Error_404) 'File.ReadAllBytes(httpRequest)
                Debug.WriteLine("404 Not Found : " + httpRequest)
                ' Set HTML Header
                Dim htmlHeader As String =
                "HTTP/1.0 404 Not Found" & ControlChars.CrLf &
                "Server: WebServer 1.0" & ControlChars.CrLf &
                "Connection: close" &
                 ControlChars.CrLf & ControlChars.CrLf
                ' The content Length of HTML Header
                Dim headerByte() As Byte = Encoding.UTF8.GetBytes(htmlHeader)
                ' Send HTML Header back to Web Browser
                stream.Write(headerByte, 0, headerByte.Length)
                'stream.Write(headerByte, 0, headerByte.Length, SocketFlags.None)
                ' Send HTML Content back to Web Browser
                stream.Write(respByte, 0, respByte.Length)
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
        End Try
    End Sub

    ' Get Content Type
    Private Function GetContentType(ByVal httpRequest As String) As String
        If (httpRequest.EndsWith("html")) Then
            Return "text/html"
        ElseIf (httpRequest.EndsWith("htm")) Then
            Return "text/html"
        ElseIf (httpRequest.EndsWith("txt")) Then
            Return "text/plain"
        ElseIf (httpRequest.EndsWith("gif")) Then
            Return "image/gif"
        ElseIf (httpRequest.EndsWith("jpg")) Then
            Return "image/jpeg"
        ElseIf (httpRequest.EndsWith("jpg")) Then
            Return "image/jpeg"
        ElseIf (httpRequest.EndsWith("ico")) Then
            Return "image/x-icon"
        ElseIf (httpRequest.EndsWith("png")) Then
            Return "image/png"
        ElseIf (httpRequest.EndsWith("jpeg")) Then
            Return "image/jpeg"
        ElseIf (httpRequest.EndsWith("pdf")) Then
            Return "application/pdf"
        ElseIf (httpRequest.EndsWith("pdf")) Then
            Return "application/pdf"
        ElseIf (httpRequest.EndsWith("doc")) Then
            Return "application/msword"
        ElseIf (httpRequest.EndsWith("xls")) Then
            Return "application/vnd.ms-excel"
        ElseIf (httpRequest.EndsWith("ppt")) Then
            Return "application/vnd.ms-powerpoint"
        ElseIf (httpRequest.EndsWith("js")) Then
            Return "application/javascript"
        ElseIf (httpRequest.EndsWith("ass")) Then
            Return "application/octet-stream"
        ElseIf (httpRequest.EndsWith("check")) Then
            Return "application/json"
        Else
            Return "text/plain"
        End If
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        ErrorDialog.Show()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        ErrorDialog.ShowDialog()
    End Sub

    Private Sub Btn_min_Click(sender As Object, e As EventArgs) Handles Btn_min.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick

        'If Application.OpenForms().OfType(Of Anime_Add).Any = False Then
        '    If ListBoxList.Count > 0 Then
        '        If CBool(InStr(Me.Text, "Crunchyroll Downloader")) Or CBool(InStr(Me.Text, " downloads in queue")) Then
        '            Me.Text = "Status: " + ListBoxList.Count.ToString + " downloads in queue" + vbNewLine + "open the add window to continue"
        '        End If
        '    End If
        'End If

    End Sub

    Private Sub Main_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Btn_add.Image = My.Resources.main_add
        Panel1.Select()



    End Sub
    Private Async Sub Funimation_Token_Click(sender As Object, e As EventArgs) Handles Funimation_Token.Click
        Dim Token As String = Nothing
        Try
            Dim DeviceRegion As String = Nothing

            'Browser.GetCookies()

            Dim list As List(Of CoreWebView2Cookie) = Await Browser.WebView2.CoreWebView2.CookieManager.GetCookiesAsync("https://www.funimation.com/")
            Dim Cookie As String = ""
            For i As Integer = 0 To list.Count - 1
                If CBool(InStr(list.Item(i).Domain, "funimation.com")) Then 'list.Item(i).Domain = "funimation.com" Then
                    'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)
                    Cookie = Cookie + list.Item(i).Name + "=" + list.Item(i).Value + ";"
                End If
                If CBool(InStr(list.Item(i).Domain, "funimation.com")) And CBool(InStr(list.Item(i).Name, "src_token")) Then 'list.Item(i).Domain = "funimation.com" Then
                    'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)
                    Token = "Token " + list.Item(i).Value
                End If
                If CBool(InStr(list.Item(i).Domain, "funimation.com")) And CBool(InStr(list.Item(i).Name, "region")) Then 'list.Item(i).Domain = "funimation.com" Then
                    'MsgBox(list.Item(i).Name + vbNewLine + list.Item(i).Value)
                    DeviceRegion = "?deviceType=web&" + list.Item(i).Name + "=" + list.Item(i).Value
                End If
            Next
        Catch ex As Exception

        End Try
        ' region=US;
        If Token = Nothing Then
            MsgBox("No Token has been found...", MsgBoxStyle.Exclamation)
        Else
            ' TODO
            ' FunimationToken = Token
            MsgBox("Token found!" + vbNewLine + Token, MsgBoxStyle.Information)
        End If
    End Sub


    Private Sub CheckCRBetaTokenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckCRBetaTokenToolStripMenuItem.Click
        If CrBetaBasic = Nothing Then
            If CBool(MessageBox.Show("No CR Beta Basic Token has been found..." + vbNewLine + "Press 'Yes' to manuel edit the Token", "Token", MessageBoxButtons.YesNo) = DialogResult.Yes) Then
                CrBetaBasic = InputBox("Please enter a valid Token", "Token")
            End If

        Else
            MsgBox("CR Beta Basic Token found!" + vbNewLine + CrBetaBasic, MsgBoxStyle.Information)
            ' CrBetaBasic = Nothing


        End If
    End Sub

    Private Sub AddonHTMLToolStripMenuItem_Click(sender As Object, e As EventArgs)
        My.Computer.Clipboard.SetText(HTML)
    End Sub

    Private Sub Timer3OffToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Timer3OffToolStripMenuItem.Click
        Timer3.Enabled = False
    End Sub

    Private Sub ThreadCount_Click(sender As Object, e As EventArgs) Handles ThreadCount.Click
        Trackbar.ShowDialog()
    End Sub

    Private Sub MsgBoxToolStripMenuItem_Click(sender As Object, e As EventArgs)
        MsgBox(LoadedUrls.Count.ToString)
        For i As Integer = 0 To LoadedUrls.Count - 1
            MsgBox(LoadedUrls(i))
        Next
    End Sub

    Private Sub CRCookieToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CRCookieToolStripMenuItem.Click

        'MsgBox(Curl(InputBox("test", "test")))
        'For i As Integer = 0 To CookieList.Count - 1


        'Next
        MsgBox(CookieList.Count.ToString)
        'MsgBox(CR_Cookies)
    End Sub

    Private Sub ClearAllSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UrlJsonsToolStripMenuItem.Click

        MsgBox("Season" + vbNewLine + CR_SeasonJson.Content.Count.ToString)
        MsgBox("Object" + vbNewLine + CR_ObjectsJson.Content.Count.ToString)
        MsgBox("Streams" + vbNewLine + CR_VideoJson.Content.Count.ToString)

    End Sub


    Private Sub ItemBoundsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try

            For s As Integer = 0 To Panel1.Controls.Count - 1
                MsgBox(Panel1.Controls.Item(s).Bounds.ToString)
            Next
        Catch ex As Exception
        End Try
    End Sub




    Private Sub PanelControlRemoved(sender As Object, e As ControlEventArgs) Handles Panel1.ControlAdded, Panel1.ControlRemoved

        ItemBounds()
    End Sub

    Private Sub PanelScroll(sender As Object, e As ScrollEventArgs) Handles Panel1.Scroll
        'MsgBox("Scroll")
        ItemBounds()
    End Sub

    Sub ItemBounds()
        Try
            Panel1.AutoScrollPosition = New Point(0, 0)
            Dim W As Integer = Panel1.Width
            If Panel1.Controls.Count * 142 > Panel1.Height Then
                W = Panel1.Width - SystemInformation.VerticalScrollBarWidth
            End If

            Dim Item As New List(Of CRD_List_Item)
            Item.AddRange(Panel1.Controls.OfType(Of CRD_List_Item))
            Item.Reverse()

            For s As Integer = 0 To Item.Count - 1
                Item(s).SetBounds(0, 142 * s, W - 2, 142)
                If Debug2 = True Then
                    Debug.WriteLine("Ist: " + Item(s).Location.Y.ToString)
                    Debug.WriteLine("Soll: " + (142 * s).ToString)
                End If
            Next


        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Sub DummyItemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DummyItemToolStripMenuItem.Click
        Dim TN As String = "https://invalid.com/"
        Dim cmd As String = "-i " + """" + "https://invalid.com/" + """" + " -c copy "
        ListItemAdd("TestDL", "CR", "TestDL", "9987p", "DE", "None", TN, cmd, "E:\Test\RWBY\Testdl.mkv")


    End Sub

#End Region

#Region "enum"

    Sub FillArray() '

        LangValueEnum.Add(New NameValuePair("[ null ]", "", Nothing))
        LangValueEnum.Add(New NameValuePair("Deutsch", "de-DE", Nothing))
        LangValueEnum.Add(New NameValuePair("English", "en-US", "en"))
        LangValueEnum.Add(New NameValuePair("Português (Brasil)", "pt-BR", "pt"))
        LangValueEnum.Add(New NameValuePair("Español (LA)", "es-419", "es"))
        LangValueEnum.Add(New NameValuePair("Français (France)", "fr-FR", Nothing))
        LangValueEnum.Add(New NameValuePair("العربية (Arabic)", "ar-SA", Nothing))
        LangValueEnum.Add(New NameValuePair("Русский (Russian)", "ru-RU", Nothing))
        LangValueEnum.Add(New NameValuePair("Italiano (Italian)", "it-IT", Nothing))
        LangValueEnum.Add(New NameValuePair("Español (España)", "es-ES", Nothing))
        LangValueEnum.Add(New NameValuePair("Japanese", "ja-JP", Nothing))

    End Sub



    Private Sub QueueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QueueToolStripMenuItem.Click
        'ffmpeg_options.ShowDialog()
        Dim newCmd As New ffmpeg_options
        newCmd.command = ProgramSettings.GetInstance().Ffmpeg.GetFfmpegArguments()
        'MsgBox(newCmd.ShowDialog.ToString)
        If newCmd.ShowDialog = DialogResult.OK Then
            My.Settings.ffmpeg_command_override = newCmd.command
        End If
    End Sub

    Private Sub Btn_Queue_Click(sender As Object, e As EventArgs) Handles Btn_Queue.Click
        If File.Exists("cookies.txt") = False Then
            If Application.OpenForms().OfType(Of Browser).Any = True Then
            Else
                UserBowser = False
                Browser.Show()
            End If
        End If

        If Queue.WindowState = System.Windows.Forms.FormWindowState.Minimized Then
            Queue.WindowState = System.Windows.Forms.FormWindowState.Normal
        Else
            Queue.Show()
        End If

    End Sub



#End Region

#Region "Process Urls"


    Public Sub LoadBrowser(ByVal Url As String)


        LoadingUrl = Url
        LoadedUrls.Clear()
        Dim NoBrowser As Boolean = False

        If My.Settings.SaveMode = True Then
            Browser.WebView2.CoreWebView2.Navigate(Url)
            Exit Sub
        End If

        'CR_v1Token = "Get"
        'Browser.WebView2.Source = New Uri(Url)
        'Exit Sub
        'MsgBox(Url)

        If CBool(InStr(Url, "crunchyroll.com")) = True And CBool(InStr(Url, "series")) = True Or CBool(InStr(Url, "crunchyroll.com")) = True And CBool(InStr(Url, "watch")) = True Then



#Region "Get Cookies"

            CR_Cookies = "Cookie: "
            'MsgBox("Cookies")
            If File.Exists("cookies.txt") = True Then
                CR_Cookies = GetCookiesFromFile("crunchyroll.com")
                NoBrowser = True
                CrBetaBasic = "Basic bm9haWhkZXZtXzZpeWcwYThsMHE6"
                'MsgBox(True.ToString)
            Else
                Browser.GetCookies(Url)

                Debug.WriteLine(CookieList.Count.ToString)
                If CookieList.Count = 0 Then
                    Browser.WebView2.CoreWebView2.Navigate(Url)
                    SetStatusLabel("Status: loading in browser...")
                    Me.Text = "Status: loading in browser..."
                    Exit Sub
                End If



                For i As Integer = 0 To CookieList.Count - 1

                    If CBool(InStr(CookieList.Item(i).Domain, ".crunchyroll.com")) And CBool(InStr(CookieList.Item(i).Name, "_evidon_suppress")) = False Then
                        CR_Cookies = CR_Cookies + CookieList.Item(i).Name + "=" + CookieList.Item(i).Value + ";"
                    End If

                Next

            End If

            'MsgBox(Main.CR_Cookies)

            Dim DeviceRegion As String = Nothing

            If CBool(InStr(Url, "/series")) Then
                Dim locale1() As String = Url.Split(New String() {"crunchyroll.com/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim locale2() As String = locale1(1).Split(New String() {"/series"}, System.StringSplitOptions.RemoveEmptyEntries)
                ' TODO: This is in Crunchyroll Downloader
                'locale = Convert_locale(locale2(0))
                If locale = "en-US" Then
                    Url_locale = ""
                Else
                    Url_locale = locale2(0)
                End If
            ElseIf CBool(InStr(Url, "/watch")) Then
                Dim locale1() As String = Url.Split(New String() {"crunchyroll.com/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim locale2() As String = locale1(1).Split(New String() {"/watch"}, System.StringSplitOptions.RemoveEmptyEntries)
                'MsgBox(locale2(0))

                ' TODO: This is in Crunchyroll Downloader
                ' locale = Convert_locale(locale2(0))
                'End If
                If locale = "en-US" Then
                    Url_locale = ""
                Else
                    Url_locale = locale2(0)
                End If
            End If

            Debug.WriteLine("###" + CR_Cookies + "###")

            Dim Loc_CR_Cookies = " -H " + """" + CR_Cookies + """"


            'CR_v1Token = "Get"
            'Browser.WebView2.Source = New Uri(Url)
            'Exit Sub

#End Region
            Dim Auth As String = " -H " + """" + "Authorization: " + CrBetaBasic + """"
            Dim Post As String = " -d " + """" + "grant_type=etp_rt_cookie" + """" + " -X POST"

            Dim CRBetaBearer As String = "Bearer "

            Dim v1Token As String = CurlPost("https://www.crunchyroll.com/auth/v1/token", Loc_CR_Cookies, Auth, Post)


            If CBool(InStr(v1Token, "curl:")) = True And CBool(InStr(v1Token, "400")) = True Then

                v1Token = CurlPost("https://www.crunchyroll.com/auth/v1/token", Loc_CR_Cookies, Auth, Post.Replace("etp_rt_cookie", "client_id"))

            End If

            'MsgBox(v1Token)

            If CBool(InStr(v1Token, "curl:")) = True And CBool(InStr(v1Token, "400")) = True Then
                SetStatusLabel("Status: Failed - bad request, check CR login")
                Me.Text = "Status: Failed - bad request, check CR login"
                Debug.WriteLine("Status: Failed - bad request, check CR login")

                b = True
                Exit Sub

            ElseIf CBool(InStr(v1Token, "curl:")) = True Then
                v1Token = CurlPost("https://www.crunchyroll.com/auth/v1/token", Loc_CR_Cookies, Auth, Post)
            End If

            'MsgBox(v1Token)
            If CBool(InStr(v1Token, "curl: (60)")) = True Then
                SetStatusLabel("Status: Critical error. #4478")
                MsgBox("Please try the '--insecure' option found on the 'Main' page of the settings.")
                Exit Sub
                'ElseIf CBool(InStr(v1Token, "curl:")) Then

            ElseIf CBool(InStr(v1Token, "curl:")) = True Then
                Browser.WebView2.CoreWebView2.Navigate(Url)
                Exit Sub

            End If

            Dim Token() As String = v1Token.Split(New String() {"""" + "access_token" + """" + ":" + """"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Token2() As String = Token(1).Split(New String() {"""" + "," + """"}, System.StringSplitOptions.RemoveEmptyEntries)
            CRBetaBearer = CRBetaBearer + Token2(0)

            Dim Auth2 As String = " -H " + """" + "Authorization: " + CRBetaBearer + """"

            ProcessLoading(Url, Auth2, Loc_CR_Cookies)

        Else
            'to do
        End If
    End Sub

    Public Sub ProcessLoading(ByVal url As String, Auth2 As String, ByVal Loc_CR_Cookies As String)

        If CBool(InStr(url, "crunchyroll.com")) = True And CBool(InStr(url, "series/")) = True Then

            Dim SeriesUrlBuilder() As String = url.Split(New String() {"series/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim SeriesUrlBuilder2() As String = SeriesUrlBuilder(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)


            Dim SeriesUrl As String = "https://www.crunchyroll.com/content/v2/cms/series/" + SeriesUrlBuilder2(0) + "/seasons?preferred_audio_language=" + DubSprache.CR_Value + "&locale=" + locale '+ "&Signature=" + signature2(0) + "&Policy=" + policy2(0) + "&Key-Pair-Id=" + key_pair_id2(0)
            Debug.WriteLine(SeriesUrl)
            ' TODO
            'GetBetaSeasons(url, SeriesUrl, Auth2)


        ElseIf CBool(InStr(url, "crunchyroll.com")) = True And CBool(InStr(url, "watch/")) = True And CBool(CrBetaBasic = Nothing) = False Then

            Dim ObjectsUrl As String = Nothing


            'MsgBox(Url)
            Dim ObjectsURLBuilder3() As String = url.Split(New String() {"watch/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ObjectsURLBuilder4() As String = ObjectsURLBuilder3(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)



            ObjectsUrl = "https://www.crunchyroll.com/content/v2/cms/objects/" + ObjectsURLBuilder4(0) + "?preferred_audio_language=" + DubSprache.CR_Value + "&locale=" + locale
            'End Using
            'MsgBox(ObjectsUrl)

            Debug.WriteLine("ObjectsUrl: " + ObjectsUrl)


            Dim StreamsUrl As String = Nothing
            Dim ObjectJson As String
            ObjectJson = CurlAuth(ObjectsUrl, Loc_CR_Cookies, Auth2)


            If CBool(InStr(ObjectJson, "curl:")) = True Then
                Browser.WebView2.CoreWebView2.Navigate(url)

                Exit Sub
            ElseIf CBool(InStr(ObjectJson, "videos/")) = False Then

                SetStatusLabel("Status: Failed - no video, check CR login")
                Me.Text = "Status: Failed - no video, check CR login"
                Debug.WriteLine("Status: Failed - no video, check CR login")

                Exit Sub
            End If

            Try
                Dim StreamsUrlBuilder() As String = ObjectJson.Split(New String() {"videos/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim StreamsUrlBuilder2() As String = StreamsUrlBuilder(1).Split(New String() {"/streams"}, System.StringSplitOptions.RemoveEmptyEntries)

                Dim StreamsUrlBuilder3() As String = ObjectsUrl.Split(New String() {"objects/"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim StreamsUrlBuilder4() As String = StreamsUrlBuilder3(1).Split(New String() {"?"}, System.StringSplitOptions.RemoveEmptyEntries)

                StreamsUrl = StreamsUrlBuilder3(0) + "videos/" + StreamsUrlBuilder2(0) + "/streams?" + StreamsUrlBuilder4(1)

                'MsgBox(StreamsUrl)

                ' Debug.WriteLine(StreamsUrl)
            Catch ex As Exception
                Browser.WebView2.CoreWebView2.Navigate(url)
                Exit Sub
            End Try

            ' TODO
            'GetBetaVideoProxy(StreamsUrl, Auth2, url)


        Else
            Browser.WebView2.CoreWebView2.Navigate(url)
        End If


    End Sub


    Public Function GetCookiesFromFile(ByVal Host As String) As String

        Dim Cookies As String = "Cookie: "
        Dim Cookie_txt As String = My.Computer.FileSystem.ReadAllText("cookies.txt")

        Dim LineChar As String = vbCrLf

        If CBool(InStr(Cookie_txt, vbCr)) Then
            LineChar = vbCr
            'Debug.WriteLine("vbCr")
        ElseIf CBool(InStr(Cookie_txt, vbLf)) Then
            LineChar = vbLf
            'Debug.WriteLine("vbLf")
        End If

        Dim Cookie_txt1() As String = Cookie_txt.Split(New String() {LineChar}, System.StringSplitOptions.RemoveEmptyEntries)

        Debug.WriteLine("got txt")

        For i As Integer = 0 To Cookie_txt1.Count - 1

            Dim Cookie_txt2() As String = Cookie_txt1(i).Split(New String() {vbTab}, System.StringSplitOptions.RemoveEmptyEntries)

            If CBool(InStr(Cookie_txt2(0), Host)) = True Then

                If CBool(InStr(Cookie_txt2(5), "_evidon_suppress")) = True Then
                    Continue For
                End If

                Cookies = Cookies + Cookie_txt2(5) + "=" + Cookie_txt2(6) + ";"



            End If

        Next


        'Debug.WriteLine(Cookies)

        Return Cookies
    End Function

    Sub SetStatusLabel(ByVal txt As String)
        ' TODO: Change this to be in the correct dialog
        'If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
        ' Anime_Add.StatusLabel.Text = txt
        'End If

    End Sub

    Private Sub SaveThumbnailAsImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveThumbnailAsImageToolStripMenuItem.Click
        If My.Settings.SaveThumbnail = False Then
            My.Settings.SaveThumbnail = True
            MsgBox("Thumbnails will be saved into the video folder")
            My.Settings.Save()

        Else
            My.Settings.SaveThumbnail = False
            MsgBox("Thumbnail saving disabled")
            My.Settings.Save()

        End If
    End Sub

    Private Sub SaveModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveModeToolStripMenuItem.Click
        If My.Settings.SaveMode = False Then
            My.Settings.SaveMode = True
            MsgBox("SaveMode enabled")
            My.Settings.Save()

        Else
            My.Settings.SaveMode = False
            MsgBox("SaveMode disabled")
            My.Settings.Save()

        End If
    End Sub

    Private Sub debugButton_Click(sender As Object, e As EventArgs) Handles DebugButton.Click
        Dim debugWindow = New DebugForm()
        debugWindow.Show()
    End Sub

#End Region

End Class


