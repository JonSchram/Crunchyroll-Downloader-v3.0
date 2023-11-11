Imports System.IO
Imports System.Threading
Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.pipeline
Imports Crunchyroll_Downloader.settings.general
Imports Crunchyroll_Downloader.utilities
Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports SiteAPI.api.common

Namespace download
    Public Class DownloadThread
        Private ReadOnly DlTask As DownloadTask
        Private ReadOnly WorkerThread As Thread
        Private ReadOnly Progress As IProgress(Of PipelineProgress)

        Public Sub New(task As DownloadTask, progress As IProgress(Of PipelineProgress))
            DlTask = task
            Me.Progress = progress

            WorkerThread = New Thread(AddressOf Download)
        End Sub


        Public Sub Start()
            WorkerThread.Start()
        End Sub

        Public Sub Cancel()
            ' TODO
        End Sub

        Public Sub Pause()
            ' TODO
        End Sub

        Private Async Sub Download()
            Dim settings As ProgramSettings = ProgramSettings.GetInstance()
            Dim filesystem = New RealFilesystem()
            Dim client As New RealHttpClient()
            Dim temporaryFolder = settings.TemporaryFolder
            ' TODO: Allow configuring ffmpeg exe location.
            Dim ffmpegAdapter As New FfmpegAdapter(Path.Combine(Application.StartupPath, "ffmpeg.exe"))

            Console.WriteLine("Downloading " + DlTask.ToString())

            Dim mediaStage As New RetrieveMediaStage(PipelineStage.FIND_MEDIA, Progress, DlTask.Client)
            Dim media As List(Of MediaLink) = Await mediaStage.Process(DlTask.DownloadEpisode)

            Dim selectionStage As New CalculateSelectionStage(PipelineStage.CHOOSE_MEDIA, Progress, DlTask.Client)
            Dim downloadSelection As Selection = Await selectionStage.Process(media)

            Dim downloadStage As New DownloadStage(PipelineStage.DOWNLOAD_MEDIA, Progress, temporaryFolder, ffmpegAdapter, filesystem, client)
            Dim downloadedEntries As List(Of MediaFileEntry) = Await downloadStage.Process(downloadSelection)

            Dim postprocessStage As New PostprocessStage(
                PipelineStage.POSTPROCESSING, Progress, temporaryFolder, settings.OutputFormat, settings.Ffmpeg, ffmpegAdapter, filesystem)
            Dim processedEntries As List(Of MediaFileEntry) = Await postprocessStage.Process(downloadedEntries)

            Dim output As New OutputStage(PipelineStage.FINAL_OUTPUT, Progress, DlTask, filesystem)
            Dim completedFiles As List(Of MediaFileEntry) = Await output.Process(processedEntries)

            Debug.WriteLine($"Download completed. {completedFiles.Count} files moved to {settings.TemporaryFolder}")

            Progress.Report(PipelineProgress.CreateCompleted())
        End Sub


    End Class
End Namespace