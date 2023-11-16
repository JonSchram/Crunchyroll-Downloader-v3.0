﻿Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.settings.general
Imports Crunchyroll_Downloader.utilities
Imports Crunchyroll_Downloader.utilities.ffmpeg

Namespace pipeline
    Public Class DownloadStage
        Inherits AbstractPipelineStage(Of Selection, List(Of MediaFileEntry))

        Private ReadOnly TemporaryFolder As String
        Private ReadOnly FfmpegAdapter As IFfmpegAdapter
        Private ReadOnly FileSystem As IFilesystem
        Private ReadOnly Client As IHttpClient

        Public Sub New(stage As PipelineStage, Progress As IProgress(Of PipelineProgress), temporaryFolder As String, ffmpeg As IFfmpegAdapter,
                       fileSystem As IFilesystem, client As IHttpClient)
            MyBase.New(stage, Progress)
            Me.TemporaryFolder = temporaryFolder
            FfmpegAdapter = ffmpeg
            Me.FileSystem = fileSystem
            Me.Client = client
        End Sub

        Protected Overrides Async Function Run(data As Selection) As Task(Of List(Of MediaFileEntry))
            Dim settings As ProgramSettings = ProgramSettings.GetInstance()
            Dim roundResolutionUp As Boolean = settings.ResolutionMismatchRounding = ResolutionRounding.ROUND_UP
            Dim useHighBitrate As Boolean = settings.PreferredBitrate = BitrateSetting.HIGH
            Dim downloadPrefs = New DownloadPreferences() With {
                    .TemporaryDirectory = TemporaryFolder,
                    .PreferredResolution = settings.DownloadResolution,
                    .AcceptHigherResolution = roundResolutionUp,
                    .PreferHighBitrate = useHighBitrate
                }

            ' TODO: Use correct downloader
            Dim downloader As New FfmpegDownloader(downloadPrefs, FfmpegAdapter, FileSystem, Client)

            ' TODO: Allow naming sub-stages.
            ' This reports which stages are being executed, but doesn't tell the user what is happening.
            ' It would be nice to say that this is downloading subtitles / the video.
            AddHandler downloader.ReportDownloadProgress, AddressOf ReportSubStageProgress
            AddHandler downloader.ReportDownloadComplete, AddressOf ReportSubStageFinished

            Return Await downloader.DownloadSelection(data)
        End Function


    End Class
End Namespace