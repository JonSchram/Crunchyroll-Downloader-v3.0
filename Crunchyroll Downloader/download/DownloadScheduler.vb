Imports System.ComponentModel
Imports Crunchyroll_Downloader.settings.general

Namespace download
    Public Class DownloadScheduler
        Implements INotifyPropertyChanged

        Private Shared Instance As DownloadScheduler

        Private ReadOnly queue As DownloadQueue = DownloadQueue.GetInstance()
        Private ReadOnly settings As ProgramSettings = ProgramSettings.GetInstance()
        Private ReadOnly ExecutingTasks As New List(Of DownloadTask)()
        Private ReadOnly TaskListLock As New Object()

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
            SyncLock TaskListLock
                ExecutingTasks.Add(task)
            End SyncLock
            RaiseEvent ScheduleTask(task)
        End Sub

        Private Sub TaskCompleted(task As DownloadTask)
            SyncLock TaskListLock
                ExecutingTasks.Remove(task)
            End SyncLock
            CheckAndStartTask()
        End Sub

        Friend Sub OnTaskCompleted(task As DownloadTask)
            TaskCompleted(task)
        End Sub
    End Class

End Namespace