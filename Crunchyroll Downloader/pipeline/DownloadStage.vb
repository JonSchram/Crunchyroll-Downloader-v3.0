Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.utilities
Imports Crunchyroll_Downloader.utilities.ffmpeg

Namespace pipeline
    Public Class DownloadStage
        Inherits AbstractPipelineStage(Of Selection, List(Of MediaFileEntry))

        Private ReadOnly TemporaryFolder As String
        Private FfmpegAdapter As IFfmpegAdapter
        Private FileSystem As IFilesystem
        Private Client As IHttpClient

        Public Sub New(stage As PipelineStage, Progress As IProgress(Of PipelineProgress), temporaryFolder As String, ffmpeg As IFfmpegAdapter,
                       fileSystem As IFilesystem, client As IHttpClient)
            MyBase.New(stage, Progress)
            Me.TemporaryFolder = temporaryFolder
            FfmpegAdapter = ffmpeg
            Me.FileSystem = fileSystem
            Me.Client = client
        End Sub

        Protected Overrides Async Function Run(data As Selection) As Task(Of List(Of MediaFileEntry))
            ' TODO: Use correct downloader
            Dim downloadPrefs = New DownloadPreferences() With {
                    .TemporaryDirectory = TemporaryFolder
                }
            Dim downloader As New FfmpegDownloader(downloadPrefs, FfmpegAdapter, FileSystem, Client)
            Dim downloadProgressReporter As New StageProgressHandler(Stage, Progress)

            ' TODO: Allow naming sub-stages.
            ' This reports which stages are being executed, but doesn't tell the user what is happening.
            ' It would be nice to say that this is downloading subtitles / the video.
            AddHandler downloader.ReportDownloadProgress, AddressOf downloadProgressReporter.HandleProgressReported
            AddHandler downloader.ReportDownloadComplete, AddressOf downloadProgressReporter.HandleSubStageFinished

            Return Await downloader.DownloadSelection(data)
        End Function


    End Class
End Namespace