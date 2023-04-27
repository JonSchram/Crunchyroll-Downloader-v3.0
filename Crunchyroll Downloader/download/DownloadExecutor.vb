Imports System.Collections.Concurrent
Imports System.ComponentModel
Imports System.Threading
Imports Crunchyroll_Downloader.settings.general

Namespace download
    Public Class DownloadExecutor
        Implements INotifyPropertyChanged

        Private Shared Instance As DownloadExecutor

        Private queue As DownloadQueue = DownloadQueue.GetInstance()
        Private settings As ProgramSettings = ProgramSettings.GetInstance()
        Private ExecutingTasks As List(Of DownloadTask) = New List(Of DownloadTask)()
        Private TaskListLock As Object = New Object()

        Private Sub New()
            AddHandler queue.ListChanged, AddressOf QueueModified
            AddHandler ProgramSettings.SimultaneousDownloadsChanged, AddressOf HandleSimultaneousDownloadChange
        End Sub

        Public Shared Function GetInstance() As DownloadExecutor
            If Instance Is Nothing Then
                Instance = New DownloadExecutor()
            End If
            Return Instance
        End Function

        Private _IsProcessingQueue As Boolean = False
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Property IsProcessingQueue As Boolean
            Get
                Return _IsProcessingQueue
            End Get
            Set
                _IsProcessingQueue = Value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("IsProcessingQueue"))
                ChangeProcessing(Value)
            End Set
        End Property

        Private Sub QueueModified(sender As Object, e As ListChangedEventArgs)
            If e.ListChangedType = ListChangedType.ItemAdded Then
                CheckAndStartTask()
            End If
        End Sub

        Private Sub ChangeProcessing(processing As Boolean)
            If processing Then
                CheckAndStartTask()
            End If
        End Sub

        Private Sub HandleSimultaneousDownloadChange(newValue As Integer)
            CheckAndStartTask()
        End Sub

        Private Sub CheckAndStartTask()
            If IsProcessingQueue Then
                Dim simultaneousDownloads = settings.SimultaneousDownloads
                SyncLock TaskListLock
                    While queue.Size() > 0 And ExecutingTasks.Count < simultaneousDownloads
                        StartNewTask(queue.Dequeue())
                    End While
                End SyncLock
            End If
        End Sub

        Private Sub StartNewTask(task As DownloadTask)
            Dim threadState = New DownloadThread(task, AddressOf TaskCompleted)
            Dim taskThread = New Thread(New ThreadStart(AddressOf threadState.Download))
            SyncLock TaskListLock
                ExecutingTasks.Add(task)
            End SyncLock
            taskThread.Start()
        End Sub

        Private Sub TaskCompleted(task As DownloadTask)
            SyncLock TaskListLock
                ExecutingTasks.Remove(task)
            End SyncLock
            CheckAndStartTask()
        End Sub

        Public Delegate Sub ThreadCallback(task As DownloadTask)

        Private Class DownloadThread
            Private ReadOnly task As DownloadTask
            Private ReadOnly callback As ThreadCallback

            Public Sub New(task As DownloadTask, callback As ThreadCallback)
                Me.task = task
                Me.callback = callback
            End Sub

            Public Async Sub Download()
                Console.WriteLine("Downloading " + task.ToString())
                Dim playbackTask = Await GetPlaybackFile()
                Console.WriteLine($"Found best matching playback: {playbackTask}")
                callback(task)
            End Sub

            Private Async Function GetPlaybackFile() As Tasks.Task(Of Playback)
                Dim client = task.GetMetadataClient()
                Dim episode = task.GetEpisode()
                Console.WriteLine($"Getting playback file for {episode}")
                Dim playback = Await client.GetEpisodePlayback(episode)

                Dim selector = New PlaybackSelector()
                Dim bestPlayback = selector.ChooseFunimationPlayback(playback)
                Return bestPlayback
            End Function

        End Class
    End Class

End Namespace