﻿Imports System.ComponentModel
Imports System.Threading
Imports Crunchyroll_Downloader.settings.general

Namespace download
    Public Class DownloadExecutor
        Implements INotifyPropertyChanged

        Private Shared Instance As DownloadExecutor

        Private queue As DownloadQueue = DownloadQueue.GetInstance()
        Private settings As ProgramSettings = ProgramSettings.GetInstance()
        Private ExecutingTasks As List(Of DownloadTask) = New List(Of DownloadTask)()

        Private Sub New()
            AddHandler queue.ListChanged, AddressOf QueueModified
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
            If e.ListChangedType = ListChangedType.ItemAdded And IsProcessingQueue Then
                CheckAndStartTask()
            End If
        End Sub

        Private Sub ChangeProcessing(processing As Boolean)
            If processing Then
                CheckAndStartTask()
            End If
        End Sub

        Private Sub CheckAndStartTask()
            Dim simultaneousDownloads = settings.SimultaneousDownloads
            While queue.Size() > 0 And ExecutingTasks.Count < simultaneousDownloads
                StartNewTask(queue.Dequeue())
            End While
        End Sub

        Private Sub StartNewTask(task As DownloadTask)
            Dim threadState = New DownloadThread(task, AddressOf TaskCompleted)
            Dim taskThread = New Thread(New ThreadStart(AddressOf threadState.Download))
            ExecutingTasks.Add(task)

            taskThread.Start()
        End Sub

        Private Sub TaskCompleted(task As DownloadTask)
            ExecutingTasks.Remove(task)
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

            Public Sub Download()
                Console.WriteLine("Pretending to download " + task.ToString())
                Thread.Sleep(10_000)
                callback(task)
            End Sub

        End Class
    End Class

End Namespace