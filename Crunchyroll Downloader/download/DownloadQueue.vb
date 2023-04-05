Imports System.Collections.Concurrent
Imports System.ComponentModel

Namespace download
    ''' <summary>
    ''' 
    ''' Adds anime episode to download. This should be individual videos. Output settings are not saved here,
    ''' because they may change
    ''' 
    ''' Might want to consider creating queues for each site or allowing you to get the next one for a site.
    ''' </summary>
    Public Class DownloadQueue
        Implements IBindingList

        Private EpisodeList As ConcurrentQueue(Of DownloadTask) = New ConcurrentQueue(Of DownloadTask)
        Private SyncObject As Object = New Object()

        Private Shared Instance As DownloadQueue = Nothing
        Public Event ListChanged As ListChangedEventHandler Implements IBindingList.ListChanged

        Public ReadOnly Property AllowNew As Boolean = False Implements IBindingList.AllowNew

        Public ReadOnly Property AllowEdit As Boolean = False Implements IBindingList.AllowEdit

        Public ReadOnly Property AllowRemove As Boolean = False Implements IBindingList.AllowRemove

        Public ReadOnly Property SupportsChangeNotification As Boolean = True Implements IBindingList.SupportsChangeNotification

        Public ReadOnly Property SupportsSearching As Boolean = False Implements IBindingList.SupportsSearching

        Public ReadOnly Property SupportsSorting As Boolean = False Implements IBindingList.SupportsSorting

        Public ReadOnly Property IsSorted As Boolean = False Implements IBindingList.IsSorted

        Public ReadOnly Property SortProperty As PropertyDescriptor Implements IBindingList.SortProperty
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public ReadOnly Property SortDirection As ListSortDirection Implements IBindingList.SortDirection
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Default Public Property Item(index As Integer) As Object Implements IList.Item
            Get
                Return EpisodeList.ElementAt(index)
            End Get
            Set(value As Object)
                Throw New NotImplementedException()
            End Set
        End Property

        Public ReadOnly Property IsReadOnly As Boolean = True Implements IList.IsReadOnly

        Public ReadOnly Property IsFixedSize As Boolean = False Implements IList.IsFixedSize

        Public ReadOnly Property Count As Integer Implements ICollection.Count
            Get
                Return EpisodeList.Count
            End Get
        End Property

        Public ReadOnly Property SyncRoot As Object Implements ICollection.SyncRoot
            Get
                Return SyncObject
            End Get
        End Property

        Public ReadOnly Property IsSynchronized As Boolean = True Implements ICollection.IsSynchronized

        Private Sub New()
        End Sub

        Public Shared Function GetInstance() As DownloadQueue
            If Instance Is Nothing Then
                Instance = New DownloadQueue()
            End If
            Return Instance
        End Function

        Public Sub Enqueue(episode As Episode, path As String)
            EpisodeList.Enqueue(New DownloadTask(episode, path))
            RaiseEvent ListChanged(Me, New ListChangedEventArgs(ListChangedType.ItemAdded, EpisodeList.Count - 1))
        End Sub

        Public Sub EnqueueRange(episodeList As List(Of Episode), startNum As Integer, endNum As Integer, path As String)
            Dim minEpisode = Math.Min(startNum, endNum)
            Dim maxEpisode = Math.Max(startNum, endNum)
            Dim episodeCount = maxEpisode - minEpisode + 1

            For episodeNumber As Integer = minEpisode To maxEpisode
                Me.Enqueue(episodeList.Item(episodeNumber), path)
            Next
        End Sub

        Public Function Dequeue() As DownloadTask
            ' TODO: This will dequeue episodes without the web site information.
            ' How do we get the episode info and the playlist? Save the API in the queue too?
            ' Allow the API to decide how to pick up from an episode?
            ' Maybe make subclasses of Episode so that when passing to the API, it chooses the right subclass
            Dim episode As DownloadTask = Nothing
            If EpisodeList.TryDequeue(episode) Then
                RaiseEvent ListChanged(Me, New ListChangedEventArgs(ListChangedType.ItemDeleted, 0))
                Return episode
            End If
            Return Nothing
        End Function

        Public Function Size() As Integer
            Return Me.Count()
        End Function

        Public Function AddNew() As Object Implements IBindingList.AddNew
            Throw New NotImplementedException()
        End Function

        Public Sub AddIndex([property] As PropertyDescriptor) Implements IBindingList.AddIndex
            Throw New NotImplementedException()
        End Sub

        Public Sub ApplySort([property] As PropertyDescriptor, direction As ListSortDirection) Implements IBindingList.ApplySort
            Throw New NotImplementedException()
        End Sub

        Public Function Find([property] As PropertyDescriptor, key As Object) As Integer Implements IBindingList.Find
            Throw New NotImplementedException()
        End Function

        Public Sub RemoveIndex([property] As PropertyDescriptor) Implements IBindingList.RemoveIndex
            Throw New NotImplementedException()
        End Sub

        Public Sub RemoveSort() Implements IBindingList.RemoveSort
            Throw New NotImplementedException()
        End Sub

        Public Function Add(value As Object) As Integer Implements IList.Add
            Throw New NotImplementedException()
        End Function

        Public Function Contains(value As Object) As Boolean Implements IList.Contains
            If TypeOf value IsNot DownloadTask Then
                Return False
            End If
            Return EpisodeList.Contains(CType(value, DownloadTask))
        End Function

        Public Sub Clear() Implements IList.Clear
            Throw New NotImplementedException()
        End Sub

        Public Function IndexOf(value As Object) As Integer Implements IList.IndexOf
            Throw New NotImplementedException()
        End Function

        Public Sub Insert(index As Integer, value As Object) Implements IList.Insert
            Throw New NotImplementedException()
        End Sub

        Public Sub Remove(value As Object) Implements IList.Remove
            Throw New NotImplementedException()
        End Sub

        Public Sub RemoveAt(index As Integer) Implements IList.RemoveAt
            Throw New NotImplementedException()
        End Sub

        Public Sub CopyTo(array As Array, index As Integer) Implements ICollection.CopyTo
            Throw New NotImplementedException()
        End Sub

        Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return EpisodeList.GetEnumerator()
        End Function
    End Class
End Namespace