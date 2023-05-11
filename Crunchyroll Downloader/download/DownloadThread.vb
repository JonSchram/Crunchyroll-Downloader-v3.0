Imports System.Threading
Imports Crunchyroll_Downloader.api.client.stream
Imports Crunchyroll_Downloader.api.metadata.video

Namespace download
    Public Class DownloadThread
        Private ReadOnly CreationSynchronizationContext As SynchronizationContext

        Private ReadOnly DlTask As DownloadTask
        Private ReadOnly WorkerThread As Thread

        ''' <summary>
        ''' Event fired by the download thread. Reports progress towards the goal.
        ''' A progress value less than 1 indicates the stage is in progress.
        ''' </summary>
        ''' <param name="DownloadStage">Which stage is currently being executed</param>
        ''' <param name="StageProgress">What percent of the current stage has been completed. Is a value between 0 and 1.</param>
        ''' <param name="TotalProgress">What percent of the total download process has completed. Is a value between 0 and 1.</param>
        Public Event ReportProgress(DownloadStage As Stage, StageProgress As Double, TotalProgress As Double)
        ''' <summary>
        ''' Event fired by the download thread when the entire process has finished.
        ''' </summary>
        Public Event DownloadComplete(task As DownloadTask)

        Private Shared ReadOnly NumberOfStages As Integer = [Enum].GetNames(GetType(Stage)).Length

        Public Sub New(task As DownloadTask)
            DlTask = task
            WorkerThread = New Thread(AddressOf Download)

            ' Save the current thread so events can be triggered on the thread that created this.
            CreationSynchronizationContext = SynchronizationContext.Current
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

        Private Sub RaiseReportProgressEvent(InProgressStage As Stage, percent As Double)
            CreationSynchronizationContext.Post(
                Sub()
                    RaiseEvent ReportProgress(InProgressStage, percent, InProgressStage / NumberOfStages)
                End Sub, Nothing)
        End Sub

        Private Sub RaiseCompletionEvent()
            ' TODO: Must raise event on the correct thread. The calling code creates new tasks on the thread this event is rasied from,
            ' meaning that when the download completes, the context will disappear.
            CreationSynchronizationContext.Post(
                Sub()
                    RaiseEvent DownloadComplete(DlTask)
                End Sub, Nothing)
        End Sub

        Private Async Sub Download()
            Console.WriteLine("Downloading " + DlTask.ToString())
            RaiseReportProgressEvent(Stage.FIND_VIDEO, 0)
            Dim playback = Await GetPlaybackFile()

            Console.WriteLine($"Found best matching playback: {playback}")

            RaiseCompletionEvent()
        End Sub

        Private Async Function GetPlaybackFile() As Tasks.Task(Of Playback)
            Dim client = DlTask.GetMetadataClient()
            Dim episode = DlTask.GetEpisode()
            Console.WriteLine($"Getting playback file for {episode}")
            Dim playback = Await client.GetEpisodePlayback(episode)
            RaiseReportProgressEvent(Stage.FIND_VIDEO, 1)

            ' TODO: This pattern of reporting a complete stage and immediately reporting the next stage as incomplete
            ' makes the code harder to read. It probably isn't necessary if this is being reported in the UI.
            ' Unless the progress reporting is a log, a single status message would disappear instantly.
            ' Still a problem but a little less of one is that fast stages with no async component will also disappear very quickly.
            ' I think the solution here would be in the UI

            RaiseReportProgressEvent(Stage.FIND_VERSION, 0)
            'Dim selector = New PlaybackSelector()
            'Dim bestPlayback = selector.ChooseFunimationPlayback(playback)
            'RaiseReportProgressEvent(Stage.FIND_VERSION, 1)

            'Return bestPlayback
            Return Nothing
        End Function

        Public Enum Stage As Integer
            ' Downloading the file containing download subtitles and the link to the video playlist.
            FIND_VIDEO = 0

            ' Choosing the video version that matches the user's preferences
            FIND_VERSION = 1

            ' Downloading the master playlist, lists all resolutions
            DOWNLOAD_MASTER_PLAYLIST = 2

            ' Finding which file in the master playlist contains the correct resolution
            FIND_RESOLUTION = 3

            ' Downloading the playlist for the chosen resolution.
            DOWNLOAD_VIDEO_PLAYLIST = 4

            ' Downloading the video segments
            DOWNLOAD_VIDEO_SEGMENTS = 5

            ' Downloading the playlist for the audio.
            DOWNLOAD_AUDIO_PLAYLIST = 6

            ' Downloading audio segments from the audio playlist.
            DOWNLOAD_AUDIO_SEGMENTS = 7

            ' Combining downloaded file into the final format.
            MERGE_OUTPUT = 8
        End Enum
    End Class
End Namespace