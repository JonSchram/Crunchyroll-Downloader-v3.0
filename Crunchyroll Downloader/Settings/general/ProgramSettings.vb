Imports System.Text.RegularExpressions
Imports Crunchyroll_Downloader.settings.crunchyroll
Imports Crunchyroll_Downloader.settings.ffmpeg
Imports Crunchyroll_Downloader.settings.ffmpeg.encoding
Imports Crunchyroll_Downloader.settings.funimation

Namespace settings.general

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

        ' TODO: Remove upgrade options after the new settings have been used for a few releases
        Public Function NeedsUpgrade() As Boolean
            Return My.Settings.NeedsUpgrade
        End Function

        ''' <summary>
        ''' Reads previous saved settings and attempts to convert them to the new settings.
        ''' Sets old settings version to empty or default values so that the upgrade doesn't happen next time.
        ''' </summary>
        Public Sub UpgradeSettings()
            My.Settings.Upgrade()
            UpgradeVideoFormat()
            UpgradeMergeSubs()
            UpgradeFfmpegCommand()
            UpgradeNameTemplate()
            UpgradeSeasonPrefix()
            UpgradeEpisodePrefix()
            UpgradeResolution()
            UpgradePath()

            Crunchyroll.UpgradeSettings()
            Funimation.UpgradeSettings()

            My.Settings.NeedsUpgrade = False
        End Sub

        Private Sub UpgradeVideoFormat()
            Try
                Dim videoFormat As String = CStr(My.Settings.GetPreviousVersion("VideoFormat"))
                If videoFormat = "" Then
                    Exit Sub
                End If
                Dim NewSetting = Format.MediaFormat.MP4
                If videoFormat = ".mp4" Then
                    NewSetting = Format.MediaFormat.MP4
                ElseIf videoFormat = ".mkv" Then
                    NewSetting = Format.MediaFormat.MKV
                ElseIf videoFormat = ".aac" Then
                    NewSetting = Format.MediaFormat.AAC_AUDIO_ONLY
                End If
                My.Settings.OutputMediaFormat = NewSetting
            Catch ex As Exception
                My.Settings.OutputMediaFormat = Format.MediaFormat.MP4
            End Try
        End Sub

        Private Sub UpgradeMergeSubs()
            Try
                Dim mergeSubs = CStr(My.Settings.GetPreviousVersion("MergeSubs"))
                If mergeSubs = "" Then
                    Exit Sub
                End If
                Dim NewSetting = Format.SubtitleMerge.DISABLED
                If mergeSubs = "[merge disabled]" Then
                    NewSetting = Format.SubtitleMerge.DISABLED
                ElseIf mergeSubs = "mov_text" Then
                    NewSetting = Format.SubtitleMerge.MOV_TEXT
                ElseIf mergeSubs = "copy" Then
                    NewSetting = Format.SubtitleMerge.COPY
                ElseIf mergeSubs = "srt" Then
                    NewSetting = Format.SubtitleMerge.SRT
                End If
                My.Settings.OutputSubtitleMerge = NewSetting
            Catch ex As Exception
                My.Settings.OutputSubtitleMerge = Format.SubtitleMerge.DISABLED
            End Try
        End Sub

        Private Sub UpgradeFfmpegCommand()
            Try
                Dim command = CStr(My.Settings.GetPreviousVersion("ffmpeg_command"))
                If command Is Nothing Or command = "" Then
                    Exit Sub
                End If
                If command.Contains("-c copy") Then
                    My.Settings.ffmpeg_copy = True
                ElseIf command.Contains("-c:v") Then
                    My.Settings.ffmpeg_copy = False

                    If command.Contains("libx264") Then
                        My.Settings.ffmpeg_video_codec = Codec.H_264
                        My.Settings.ffmpeg_video_hardware = EncoderImplementation.SOFTWARE
                    ElseIf command.Contains("libx265") Then
                        My.Settings.ffmpeg_video_codec = Codec.H_265
                        My.Settings.ffmpeg_video_hardware = EncoderImplementation.SOFTWARE
                    ElseIf command.Contains("libstav1") Then
                        My.Settings.ffmpeg_video_codec = Codec.AV1
                        My.Settings.ffmpeg_video_hardware = EncoderImplementation.SOFTWARE

                        ' NVIDIA encoders
                    ElseIf command.Contains("h264_nvenc") Then
                        My.Settings.ffmpeg_video_codec = Codec.H_264
                        My.Settings.ffmpeg_video_hardware = EncoderImplementation.NVIDIA
                    ElseIf command.Contains("hevc_nvenc") Then
                        My.Settings.ffmpeg_video_codec = Codec.H_265
                        My.Settings.ffmpeg_video_hardware = EncoderImplementation.NVIDIA
                    ElseIf command.Contains("av1_nvenc") Then
                        My.Settings.ffmpeg_video_codec = Codec.AV1
                        My.Settings.ffmpeg_video_hardware = EncoderImplementation.NVIDIA

                        ' AMD encoders
                    ElseIf command.Contains("h264_amf") Then
                        My.Settings.ffmpeg_video_codec = Codec.H_264
                        My.Settings.ffmpeg_video_hardware = EncoderImplementation.AMD
                    ElseIf command.Contains("hevc_amf") Then
                        My.Settings.ffmpeg_video_codec = Codec.H_265
                        My.Settings.ffmpeg_video_hardware = EncoderImplementation.AMD

                        ' Intel encoders
                    ElseIf command.Contains("h264_qsv") Then
                        My.Settings.ffmpeg_video_codec = Codec.H_264
                        My.Settings.ffmpeg_video_hardware = EncoderImplementation.INTEL
                    ElseIf command.Contains("hevc_qsv") Then
                        My.Settings.ffmpeg_video_codec = Codec.H_265
                        My.Settings.ffmpeg_video_hardware = EncoderImplementation.INTEL
                    ElseIf command.Contains("av1_qsv") Then
                        My.Settings.ffmpeg_video_codec = Codec.AV1
                        My.Settings.ffmpeg_video_hardware = EncoderImplementation.INTEL
                    End If
                End If

                If command.Contains("-preset fast") Then
                    My.Settings.ffmpeg_video_preset = Speed.FAST
                ElseIf command.Contains("-preset slow") Then
                    My.Settings.ffmpeg_video_preset = Speed.SLOW
                End If

                If command.Contains("-b:v") Then
                    My.Settings.ffmpeg_use_target_bitrate = True

                    Dim match = Regex.Match(command, "-b:v (\d+)k")
                    If match.Success Then
                        Dim bitrate = CInt(match.Groups(1).Value)
                        My.Settings.ffmpeg_video_bitrate = bitrate
                    End If
                End If

            Catch ex As Exception
                Ffmpeg = New FfmpegOptions.Builder().SetCopyMode(True).Build()
            End Try
        End Sub

        Private Sub UpgradeNameTemplate()
            Try
                Dim oldNameMethod As Integer = CInt(My.Settings.GetPreviousVersion("CR_NameMethode"))
                Select Case oldNameMethod
                    Case 0
                        FilenameFormat = "AnimeTitle;Season;EpisodeNR;"
                    Case 1
                        FilenameFormat = "AnimeTitle;Season;EpisodeName;"
                    Case 2
                        FilenameFormat = "AnimeTitle;Season;EpisodeNR;EpisodeName;"
                    Case 3
                        FilenameFormat = "AnimeTitle;Season;EpisodeName;EpisodeNR;"
                End Select
            Catch ex As Exception
                FilenameFormat = ""
            End Try
        End Sub

        Private Sub UpgradeSeasonPrefix()
            If My.Settings.Prefix_S = "[default season prefix]" Then
                My.Settings.Prefix_S = "Season"
            End If
        End Sub

        Private Sub UpgradeEpisodePrefix()
            If My.Settings.Prefix_E = "[default episode prefix]" Then
                My.Settings.Prefix_E = "Episode"
            End If
        End Sub

        Private Sub UpgradeResolution()
            If My.Settings.Reso = 42 Then
                DownloadResolution = Resolution.AUTO
            End If
        End Sub
        Private Sub UpgradePath()
            Try
                Dim oldPath As String = CStr(My.Settings.GetPreviousVersion("Pfad"))
                OutputPath = oldPath
            Catch ex As Exception
                OutputPath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            End Try
        End Sub


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
                Dim resolutionSetting As Integer = My.Settings.Reso
                Select Case resolutionSetting
                    Case 360
                        Return Resolution.RESOLUTION_360P
                    Case 480
                        Return Resolution.RESOLUTION_480P
                    Case 720
                        Return Resolution.RESOLUTION_720P
                    Case 1080
                        Return Resolution.RESOLUTION_1080P
                    Case Else
                        Return Resolution.AUTO
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

        Public Property Ffmpeg As FfmpegOptions
            Get
                Dim codec = CType(My.Settings.ffmpeg_video_codec, Codec)
                Dim hardware = CType(My.Settings.ffmpeg_video_hardware, EncoderImplementation)
                Dim preset = CType(My.Settings.ffmpeg_video_preset, Speed)

                Dim settingBuilder = New FfmpegOptions.Builder()
                With settingBuilder
                    .SetIncludeUnusedVideoSettings(True)
                    .SetCopyMode(My.Settings.ffmpeg_copy)
                    .SetVideoCodec(codec)
                    .SetEncoderHardware(hardware)
                    .SetPresetSpeed(preset)
                    .SetUseTargetBitrate(My.Settings.ffmpeg_use_target_bitrate)
                    .SetVideoBitrate(My.Settings.ffmpeg_video_bitrate)
                End With

                Return settingBuilder.Build()
            End Get
            Set(value As FfmpegOptions)
                My.Settings.ffmpeg_copy = value.VideoCopy
                Dim encoder = value.GetSavedEncoder()
                If encoder IsNot Nothing Then
                    My.Settings.ffmpeg_video_codec = encoder.VideoCodec
                    My.Settings.ffmpeg_video_hardware = encoder.Hardware
                    My.Settings.ffmpeg_video_preset = encoder.Preset
                    My.Settings.ffmpeg_use_target_bitrate = encoder.UseTargetBitrate
                    My.Settings.ffmpeg_video_bitrate = encoder.TargetBitrate
                End If
            End Set
        End Property

        ' ------ Naming settings
        Public Property FilenameFormat As String
            Get
                Return My.Settings.NameTemplate
            End Get
            Set(value As String)
                My.Settings.NameTemplate = value
            End Set
        End Property

        Public Property KodiNaming As Boolean
            Get
                Return My.Settings.KodiSupport
            End Get
            Set(value As Boolean)
                My.Settings.KodiSupport = value
            End Set
        End Property

        Public Property SeasonNumberNaming As SeasonNumberBehavior
            Get
                Return CType(My.Settings.IgnoreSeason, SeasonNumberBehavior)
            End Get
            Set(value As SeasonNumberBehavior)
                My.Settings.IgnoreSeason = value
            End Set
        End Property

        Public Property SeasonPrefix As String
            Get
                Return My.Settings.Prefix_S
            End Get
            Set(value As String)
                My.Settings.Prefix_S = value
            End Set
        End Property
        Public Property EpisodePrefix As String
            Get
                Return My.Settings.Prefix_E
            End Get
            Set(value As String)
                My.Settings.Prefix_E = value
            End Set
        End Property

        ''' <summary>
        ''' Length of zero-padded episode / season number in file name
        ''' Length is after padding has been applied, so 1 -> no padding, 2 -> add a zero such that length = 2
        ''' </summary>
        ''' <returns></returns>
        Public Property ZeroPaddingLength As Integer
            Get
                Return My.Settings.LeadingZero
            End Get
            Set(value As Integer)
                My.Settings.LeadingZero = value
            End Set
        End Property

        Public Property IncludeSubtitleLanguageInFirstSubtitle As Boolean
            Get
                Return My.Settings.IncludeLangName
            End Get
            Set(value As Boolean)
                My.Settings.IncludeLangName = value
            End Set
        End Property

        Public Property SubLanguageNaming As LanguageNameMethod
            Get
                Return CType(My.Settings.LangNameType, LanguageNameMethod)
            End Get
            Set(value As LanguageNameMethod)
                My.Settings.LangNameType = value
            End Set
        End Property

        Public Property OutputPath As String
            Get
                Return My.Settings.VideoOutputPath
            End Get
            Set(value As String)
                My.Settings.VideoOutputPath = value
            End Set
        End Property


        ' ----- Site settings
        Public ReadOnly Property Crunchyroll As CrunchyrollSettings = CrunchyrollSettings.GetInstance()

        Public ReadOnly Property Funimation As FunimationSettings = FunimationSettings.GetInstance()

    End Class

End Namespace