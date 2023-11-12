Option Strict On
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports Crunchyroll_Downloader.debugging
Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.settings
Imports Crunchyroll_Downloader.settings.general
Imports MetroFramework
Imports MetroFramework.Components
Imports MetroFramework.Forms

Namespace ui
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



        Public CR_MassSeasons As New List(Of CR_Seasons)
        Public CR_MassEpisodes As New List(Of CR_Seasons)


        Public CrBetaBasic As String = Nothing
        Public locale As String = Nothing
        Public Url_locale As String = Nothing
        Dim ProcessCounting As Integer = 30
        Public LoadingUrl As String = ""
        Public Manager As New MetroStyleManager
        Public invalids As Char() = System.IO.Path.GetInvalidFileNameChars()
        Dim ServerThread As Thread
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
        Public DownloadScope As Integer = 0
        Public DlSoftSubsRDY As Boolean = True
        Public DialogTaskString As String
        Public UserCloseDialog As Boolean = False
        Public LabelUpdate As String = "Status: idle"
        Public LabelEpisode As String = "..."
        ' I think this indicates the user is just browsing in the browser
        Public b As Boolean
        Public LoginOnly As String = "False"
        Public ProfileFolder As String = Path.Combine(Application.StartupPath, "CRD-Profile") 'Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, "CRD-Profile")

        Public ResoSave As String = "6666x6666"
        Public ResoFunBackup As String = "6666x6666"

        Public LangValueEnum As New List(Of NameValuePair)

        Public HybridThread As Integer = CInt(Environment.ProcessorCount / 2 - 1)
        Public AbourtList As New List(Of String)
        Public watingList As New List(Of String)
        Public GeckoLogFile As String = Nothing
        Dim CR_Unlock_Error As String
        Dim SubSprache2 As String
        Public Grapp_RDY As Boolean = True
        Public Grapp_non_cr_RDY As Boolean = True
        Public Grapp_Abord As Boolean = False
        Public ResoNotFoundString As String
        Public ResoBackString As String
        Public SystemWebBrowserCookie As String = Nothing
        Public WebbrowserText As String = Nothing
        Public WebbrowserCookie As String = Nothing
        Public SubFunimationString As String = "en"
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

        Private Sub HandleDarkModeChanged(isDarkMode As Boolean)
            If isDarkMode Then
                DarkMode()
            Else
                LightMode()
            End If
        End Sub
        Public Sub DarkMode()
            Manager.Theme = MetroThemeStyle.Dark
            CloseImg = My.Resources.main_close_dark
            MinImg = My.Resources.main_mini_dark
            Btn_min.Image = MinImg
            Btn_Close.Image = CloseImg
        End Sub

        Public Sub LightMode()
            Manager.Theme = MetroThemeStyle.Light
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

        Private Sub Main_Resize(sender As Object, e As EventArgs) Handles Me.Resize
            ' TODO: Set correct anchor properties so the positioning logic can disappear
            PictureBox5.Width = Me.Width - 40
            Btn_Settings.Location = New Point(Me.Width - 165, 17)
            Btn_Queue.Location = New Point(Me.Width - 265, 17)
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


        Function AddLeadingZeros(ByVal txt As String) As String

            txt = txt.Replace(",", ".")
            Dim Post As String = Nothing
            If CBool(InStr(txt, ".")) = True Then
                Dim txt_split As String() = txt.Split(New String() {"."}, System.StringSplitOptions.RemoveEmptyEntries)
                txt = txt_split(0)
                Post = "." + txt_split(1)
            End If

            Dim paddedLength = ProgramSettings.GetInstance().ZeroPaddingLength
            txt = txt.PadLeft(paddedLength, "0"c)
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

            AddHandler ProgramSettings.DarkModeChanged, AddressOf HandleDarkModeChanged
            Dim settings = ProgramSettings.GetInstance()

            Dim presenter = New MainPresenter(Me)
            presenter.initialize()

            Me.ContextMenuStrip = ContextMenuStrip1
            b = True
            Thread.CurrentThread.Name = "Main"
            Debug.WriteLine("Thread Name: " + Thread.CurrentThread.Name)

            StyleManager = MainStyleManager
            Manager = StyleManager

            MetroStyleExtender1.SetApplyMetroTheme(TaskFlowPanel, True)

            If settings.DarkMode Then
                DarkMode()
            Else
                LightMode()
            End If

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

            HybridThread = My.Settings.HybridThread

            RetryWithCachedFiles()
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
            Dim keepCache = settings.DownloadMode = DownloadModeOptions.HYBRID_MODE_KEEP_CACHE
            Dim mergeSubs = settings.OutputFormat.GetSubtitleFormat <> SubtitleMerge.DISABLED
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

            Item.Parent = TaskFlowPanel
            TaskFlowPanel.Controls.Add(Item)

            Item.Visible = True
            ' TODO: Support dash MPD files
            Dim TempHybridMode As Boolean = Not ProgramSettings.GetInstance().DownloadMode = DownloadModeOptions.FFMPEG
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

            Dim subLanguageNaming = ProgramSettings.GetInstance().SubLanguageNaming
            If subLanguageNaming = LanguageNameMethod.ISO639_2_CODES Then
                Return CCtoMP4CC(HardSub)
            ElseIf subLanguageNaming = LanguageNameMethod.CRUNCHYROLL_AND_ISO639_2_CODES Then
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
            cmd = cmd + "--no-alpn -fsSLm 15 -A """ + My.Resources.ffmpeg_user_agend.Replace("User-Agent: ", "") + """ """ + Url + """"
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
            cmd = cmd + "--no-alpn -fsSLm 15 -A """ + My.Resources.ffmpeg_user_agend.Replace("User-Agent: ", "") + """" + Cookies + Auth + Post + " " + """" + Url + """"
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
            cmd = cmd + "--no-alpn -fsSLm 15 -A """ + My.Resources.ffmpeg_user_agend.Replace("User-Agent: ", "") + """" + Cookies + Auth + " " + """" + Url + """"
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
                    Item.AddRange(TaskFlowPanel.Controls.OfType(Of CRD_List_Item))
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
            If settings.DownloadMode <> DownloadModeOptions.HYBRID_MODE_KEEP_CACHE Then
                Try
                    Dim di As New System.IO.DirectoryInfo(settings.OutputPath)
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
                Dim di As New System.IO.DirectoryInfo(ProgramSettings.GetInstance().OutputPath)
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

            If Not File.Exists("cookies.txt") Then
                Dim browserDialog = Browser.GetInstance()
                If Not browserDialog.Visible Then
                    browserDialog.Show()
                End If
            End If

            Dim settings = ProgramSettings.GetInstance()
            Dim addVideoWindow As New AddVideo(settings.OutputPath, settings.LastSubfolderBehavior)
            addVideoWindow.Show()
        End Sub

        Private Sub Btn_Settings_Click(sender As Object, ByVal e As EventArgs) Handles Btn_Settings.Click
            Dim dialog = SettingsDialog.GetInstance()
            If Not dialog.Visible Then
                dialog.Show(Me)
            End If
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

        Private Sub Btn_Settings_DoubleClick(sender As Object, e As EventArgs) Handles Btn_Settings.DoubleClick
            Dim dialog = SettingsDialog.GetInstance()
            If dialog.Visible Then
                dialog.Close()
            End If
            If Debug1 = True Then
                If Debug2 = True Then
                    Try
                        My.Computer.Clipboard.SetText(WebbrowserText)
                        MsgBox("webbrowser text copyed to the clipboard")
                    Catch ex As Exception
                    End Try
                Else
                    Debug2 = True
                    MsgBox("Debug activated")
                End If
            Else
                Debug1 = True
                'MsgBox("Debug activated")
            End If
        End Sub

        Private Sub Btn_Browser_Click(sender As Object, e As EventArgs) Handles Btn_Browser.Click
            Debug.WriteLine(Date.Now.ToString + "." + Date.Now.Millisecond.ToString)

            Dim browserDialog = Browser.GetInstance()
            If Not browserDialog.Visible Then
                browserDialog.Show()
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
                Item.AddRange(TaskFlowPanel.Controls.OfType(Of CRD_List_Item))
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

                RunningDownloads = TaskFlowPanel.Controls.Count
            End Try
            'Debug.WriteLine("Running: " + RunningDownloads.ToString)

            'FontLabel2.Text = RunningDownloads.ToString
            'Debug.WriteLine("downloads.tick: " + RunningDownloads.ToString)
        End Sub

        Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick

            Dim Item As New List(Of CRD_List_Item)
            Item.AddRange(TaskFlowPanel.Controls.OfType(Of CRD_List_Item))
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

        Public Sub Navigate(ByVal Url As String)
            Dim browserDialog = Browser.GetInstance()
            If Not browserDialog.Visible Then
                browserDialog.Show()
            End If
            browserDialog.Navigate(Url)
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
            ' TODO: Can just set the image in the designer? No need to put it here?
            Btn_add.Image = My.Resources.main_add
            ' TODO: Not sure why the download task panel was selected upon showing the form.
            'TaskFlowPanel.Select()
        End Sub
        Private Async Sub Funimation_Token_Click(sender As Object, e As EventArgs) Handles Funimation_Token.Click
            Dim Token As String = Nothing
            Try
                Dim DeviceRegion As String = Nothing

                Dim browserDialog = Browser.GetInstance()

                Dim list As List(Of Cookie) = Await browserDialog.GetCookies("https://www.funimation.com/")
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

        Private Async Sub CRCookieToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CRCookieToolStripMenuItem.Click
            Dim browserDialog = Browser.GetInstance()
            Dim cookieList = Await browserDialog.GetCookies("https://www.crunchyroll.com")
            MsgBox(cookieList.Count.ToString())
        End Sub

        Private Sub ClearAllSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UrlJsonsToolStripMenuItem.Click

            MsgBox("Season" + vbNewLine + CR_SeasonJson.Content.Count.ToString)
            MsgBox("Object" + vbNewLine + CR_ObjectsJson.Content.Count.ToString)
            MsgBox("Streams" + vbNewLine + CR_VideoJson.Content.Count.ToString)

        End Sub

        Private Sub DummyItemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DummyItemToolStripMenuItem.Click
            Dim TN As String = "https://invalid.com/"
            Dim cmd As String = "-i " + """" + "https://invalid.com/" + """" + " -c copy "
            ListItemAdd("TestDL", "CR", "TestDL", "9987p", "DE", "None", TN, cmd, "E:\Test\RWBY\Testdl.mkv")


        End Sub

#End Region

#Region "enum"

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
            If QueueDialog.WindowState = System.Windows.Forms.FormWindowState.Minimized Then
                QueueDialog.WindowState = System.Windows.Forms.FormWindowState.Normal
            Else
                QueueDialog.Show()
            End If
        End Sub

#End Region

#Region "Process Urls"


        Public Sub LoadBrowser(ByVal Url As String)
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
            ' TODO: SaveMode doesn't seem to do much (only controlled whether the browser redirected)
            ' It can be removed.
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

        Private Sub TaskFlowPanel_Layout(sender As Object, e As LayoutEventArgs) Handles TaskFlowPanel.Layout
            Dim panelInternalWidth = TaskFlowPanel.ClientSize.Width
            Dim controls = TaskFlowPanel.Controls
            For Each item As Control In controls
                item.Width = panelInternalWidth - item.Margin.Left - item.Margin.Right
            Next
        End Sub

#End Region

        Public Function DisplayDownloadTask(item As DownloadTask) As DownloadingItemView
            If InvokeRequired Then
                Return CType(Invoke(Function() DisplayDownloadTask(item)), DownloadingItemView)
            Else
                Dim taskView = New DownloadingItemView()
                TaskFlowPanel.Controls.Add(taskView)
                Return taskView
            End If
        End Function
        Public Sub RemoveDownloadTask(view As DownloadingItemView)
            TaskFlowPanel.Controls.Remove(view)
        End Sub

    End Class

End Namespace