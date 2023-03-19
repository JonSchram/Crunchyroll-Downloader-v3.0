Imports Crunchyroll_Downloader.settings

Namespace settings

    Public Class ProgramSettings
        Public Shared Event DarkModeChanged(darkModeEnabled As Boolean)
        Private Shared Instance As New ProgramSettings()

        Private Sub New()
        End Sub

        Public Shared Function GetInstance() As ProgramSettings
            If Instance Is Nothing Then
                Instance = New ProgramSettings()
            End If
            Return Instance
        End Function

        ' ----- Main settings

        Public Property SimultaneousDownloads As Integer
            Get
                Return My.Settings.SL_DL
            End Get
            Set(value As Integer)
                My.Settings.SL_DL = value
            End Set
        End Property

        Public Property DefaultWebsite As String
            Get
                Return My.Settings.Startseite
            End Get
            Set(value As String)
                My.Settings.Startseite = value
            End Set
        End Property

        Public Property DarkMode As Boolean
            Get
                Return My.Settings.DarkModeValue
            End Get
            Set(value As Boolean)
                Dim previousDarkMode = My.Settings.DarkModeValue
                ' Make sure to set new dark mode first so no event handler can get confused over value from event vs. settings value.
                My.Settings.DarkModeValue = value
                If value <> previousDarkMode Then
                    RaiseEvent DarkModeChanged(value)
                End If
            End Set
        End Property

        Public Property ServerPort As Integer
            Get
                Return My.Settings.ServerPort
            End Get
            Set(value As Integer)
                My.Settings.ServerPort = value
            End Set
        End Property

        Public Property InsecureCurl As Boolean
            Get
                Return My.Settings.Curl_insecure
            End Get
            Set(value As Boolean)
                My.Settings.Curl_insecure = value
            End Set
        End Property

        ' The number of errors encountered before the download is paused
        Public Property ErrorLimit As Integer
            Get
                Return My.Settings.ErrorTolerance
            End Get
            Set(value As Integer)
                My.Settings.ErrorTolerance = value
            End Set
        End Property

        Public Property SubfolderDisplayBehavior As SubfolderDisplay
            Get
                Try
                    Return CType(My.Settings.HideSF, SubfolderDisplay)
                Catch
                    Return SubfolderDisplay.SHOW_ALL
                End Try
            End Get
            Set(value As SubfolderDisplay)
                My.Settings.HideSF = value
            End Set
        End Property

        ' ----- Output settings
        Public Property DownloadMode As DownloadModeOptions
            Get
                If My.Settings.HybridMode Then
                    If My.Settings.Keep_Cache Then
                        Return DownloadModeOptions.HYBRID_MODE_KEEP_CACHE
                    Else
                        Return DownloadModeOptions.HYBRID_MODE
                    End If
                Else
                    Return DownloadModeOptions.FFMPEG
                End If
            End Get
            Set(value As DownloadModeOptions)
                Select Case value
                    Case DownloadModeOptions.FFMPEG
                        My.Settings.HybridMode = False
                        My.Settings.Keep_Cache = False
                    Case DownloadModeOptions.HYBRID_MODE
                        My.Settings.HybridMode = True
                        My.Settings.Keep_Cache = False
                    Case DownloadModeOptions.HYBRID_MODE_KEEP_CACHE
                        My.Settings.HybridMode = True
                        My.Settings.Keep_Cache = True
                End Select
            End Set
        End Property

        Public Property TemporaryFolder As String
            Get
                Return My.Settings.TempFolder
            End Get
            Set(value As String)
                My.Settings.TempFolder = value
            End Set
        End Property

        Public Property UseDownloadQueue As Boolean
            Get
                Return My.Settings.QueueMode
            End Get
            Set(value As Boolean)
                My.Settings.QueueMode = value
            End Set
        End Property

        Public Property DownloadResolution As Resolution
            Get
                Dim resolution As Integer = My.Settings.Reso
                Select Case resolution
                    Case 360
                        Return ProgramSettings.Resolution.RESOLUTION_360P
                    Case 480
                        Return ProgramSettings.Resolution.RESOLUTION_480P
                    Case 720
                        Return ProgramSettings.Resolution.RESOLUTION_720P
                    Case 1080
                        Return ProgramSettings.Resolution.RESOLUTION_1080P
                    Case Else
                        ' 0 is value from enum, but the value "42" is also used as a flag for "auto"
                        Return ProgramSettings.Resolution.AUTO
                End Select
            End Get
            Set(value As Resolution)
                My.Settings.Reso = value
            End Set
        End Property

        Public Property OutputFormat As Format
            Get
                Dim videoFormat = CType(My.Settings.OutputMediaFormat, Format.MediaFormat)
                Dim subtitleFormat = CType(My.Settings.OutputSubtitleMerge, Format.SubtitleMerge)
                Return New Format(videoFormat, subtitleFormat)
            End Get
            Set(value As Format)
                My.Settings.OutputMediaFormat = value.GetVideoFormat()
                My.Settings.OutputSubtitleMerge = value.GetSubtitleFormat()
            End Set
        End Property

        Public Property Command As FfmpegCommand

        ' ------ Naming settings
        Public Property FilenameFormat As String

        Public Property SeasonNumberNaming As SeasonNumberBehavior

        Public Property SeasonPrefix As String
        Public Property EpisodePrefix As String

        ' Length of zero-padded episode / season number in file name
        Public Property FilenamePrefixDigits As Integer

        Public Property IncludeSubtitleLanguageInFirstSubtitleFile As Boolean

        Public Property SubNaming As LanguageNameMethod


        ' ----- Site settings
        Public ReadOnly Property Crunchyroll As CrunchyrollSettings = CrunchyrollSettings.GetInstance()

        Public ReadOnly Property Funimation As FunimationSettings = FunimationSettings.GetInstance()

        Public Enum SubfolderDisplay As Integer
            SHOW_ALL = 0
            HIDE_ALL = 1
            HIDE_OLDER_THAN_1_WEEK = 2
            HIDE_OLDER_THAN_1_MONTH = 3
            HIDE_OLDER_THAN_3_MONTHS = 4
            HIDE_OLDER_THAN_6_MONTHS = 5
        End Enum

        Public Enum DownloadModeOptions
            FFMPEG
            HYBRID_MODE
            HYBRID_MODE_KEEP_CACHE
        End Enum

        Public Enum Resolution As Integer
            AUTO = 0
            RESOLUTION_360P = 360
            RESOLUTION_480P = 480
            RESOLUTION_720P = 720
            RESOLUTION_1080P = 1080
        End Enum

        Public Enum SeasonNumberBehavior
            USE_SEASON_NUMBERS
            IGNORE_SEASON_1
            IGNORE_ALL_SEASON_NUMBERS
        End Enum

        Public Enum LanguageNameMethod
            CRUNCHYROLL
            ISO639_2_CODES
            CRUNCHYROLL_AND_ISO639_2_CODES
        End Enum
    End Class

End Namespace