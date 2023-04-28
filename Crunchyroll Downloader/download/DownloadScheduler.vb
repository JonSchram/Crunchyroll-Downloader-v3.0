Imports System.ComponentModel
Imports System.Threading
Imports Crunchyroll_Downloader.settings.general

Namespace download
    Public Class DownloadScheduler
        Implements INotifyPropertyChanged

        Private Shared Instance As DownloadScheduler

        Private queue As DownloadQueue = DownloadQueue.GetInstance()
        Private settings As ProgramSettings = ProgramSettings.GetInstance()
        Private ExecutingTasks As List(Of DownloadTask) = New List(Of DownloadTask)()
        Private TaskListLock As Object = New Object()

        Public Event ScheduleTask(newTask As DownloadTask)

        Private Sub New()
            AddHandler queue.ListChanged, AddressOf QueueModified
            AddHandler ProgramSettings.SimultaneousDownloadsChanged, AddressOf HandleSimultaneousDownloadChange
        End Sub

        Public Shared Function GetInstance() As DownloadScheduler
            If Instance Is Nothing Then
                Instance = New DownloadScheduler()
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
            RaiseEvent ScheduleTask(task)
            SyncLock TaskListLock
                ExecutingTasks.Add(task)
            End SyncLock

            Dim download = New DownloadThread(task)
            AddHandler download.ReportProgress, AddressOf TaskProgress
            AddHandler download.DownloadComplete, AddressOf TaskCompleted
            download.Start()
        End Sub

        Private Sub TaskProgress(s As DownloadThread.Stage, stagePercent As Double, totalPercent As Double)
            Console.WriteLine($"Thread reported progress. {s}, stage percent: {stagePercent}, total percent: {totalPercent}")
        End Sub

        Private Sub TaskCompleted(task As DownloadTask)
            SyncLock TaskListLock
                ExecutingTasks.Remove(task)
            End SyncLock
            CheckAndStartTask()
        End Sub
    End Class

End Namespace