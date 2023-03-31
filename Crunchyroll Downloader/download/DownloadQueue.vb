Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Namespace download
    ''' <summary>
    ''' 
    ''' Adds anime episode to download. This should be individual videos. Output settings are not saved here,
    ''' because they may change
    ''' 
    ''' Might want to consider creating queues for each site or allowing you to get the next one for a site.
    ''' </summary>
    Public Class DownloadQueue
        Inherits BindingList(Of DownloadTask)

        Private Shared Instance As DownloadQueue = Nothing

        Private Sub New()

        End Sub

        Public Shared Function GetInstance() As DownloadQueue
            If Instance Is Nothing Then
                Instance = New DownloadQueue()
            End If
            Return Instance
        End Function

        Public Sub enqueue(episode As Episode, path As String)
            Me.Add(New DownloadTask(episode, path))
        End Sub

        Public Sub enqueueRange(episodeList As List(Of Episode), startNum As Integer, endNum As Integer, path As String)
            Dim minEpisode = Math.Min(startNum, endNum)
            Dim maxEpisode = Math.Max(startNum, endNum)
            Dim episodeCount = maxEpisode - minEpisode + 1

            For episodeNumber As Integer = minEpisode To maxEpisode
                Me.Add(New DownloadTask(episodeList.Item(episodeNumber), path))
                'NotifyQueue.Add(episodeList.Item(episodeNumber))
            Next
        End Sub

        Public Function Dequeue() As DownloadTask
            ' TODO: This will dequeue episodes without the web site information.
            ' How do we get the episode info and the playlist? Save the API in the queue too?
            ' Allow the API to decide how to pick up from an episode?
            ' Maybe make subclasses of Episode so that when passing to the API, it chooses the right subclass
            Dim episode = Me.Item(0)
            Me.RemoveAt(0)
            Return episode
        End Function

        Public Function Size() As Integer
            Return Me.Count()
        End Function
    End Class
End Namespace