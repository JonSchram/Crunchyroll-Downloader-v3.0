Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Runtime.CompilerServices
''' <summary>
''' 
''' Adds anime episode to download. This should be individual videos. Output settings are not saved here,
''' because they may change
''' 
''' Might want to consider creating queues for each site or allowing you to get the next one for a site.
''' </summary>
Public Class DownloadQueue
    Inherits ObservableCollection(Of Episode)

    Private Shared Instance As DownloadQueue = Nothing

    'Private NotifyQueue As ObservableCollection(Of Episode)

    Private queueBinding As BindingSource

    Private Sub New()
        'NotifyQueue = New ObservableCollection(Of Episode)
        'queueBinding = New BindingSource With {
        '    .DataSource = NotifyQueue
        '}

    End Sub

    Public Function getList() As List(Of Episode)
        Return Me.ToList()
        'Return NotifyQueue.ToList()
    End Function

    Public Shared Function getInstance() As DownloadQueue
        If Instance Is Nothing Then
            Instance = New DownloadQueue()
        End If

        Return Instance
    End Function

    Public Sub enqueue(episode As Episode)
        Me.Add(episode)
        'NotifyQueue.Add(episode)
    End Sub

    Public Sub enqueueRange(episodeList As List(Of Episode), startNum As Integer, endNum As Integer)
        Dim minEpisode = Math.Min(startNum, endNum)
        Dim maxEpisode = Math.Max(startNum, endNum)
        Dim episodeCount = maxEpisode - minEpisode + 1

        For episodeNumber As Integer = minEpisode To maxEpisode
            Me.Add(episodeList.Item(episodeNumber))
            'NotifyQueue.Add(episodeList.Item(episodeNumber))
        Next
    End Sub

    Public Function Dequeue() As Episode
        ' TODO: This will dequeue episodes without the web site information.
        ' How do we get the episode info and the playlist? Save the API in the queue too?
        ' Allow the API to decide how to pick up from an episode?
        ' Maybe make subclasses of Episode so that when passing to the API, it chooses the right subclass
        Dim episode = Me.Item(0)
        Me.RemoveAt(0)
        'Dim episode = NotifyQueue.Item(0)
        'NotifyQueue.RemoveAt(0)
        Return episode
    End Function

    Public Function size() As Integer
        Return Me.Count()
        'Return NotifyQueue.Count()
    End Function

    Public Function getBindingSource() As BindingSource
        Return queueBinding
    End Function
End Class
