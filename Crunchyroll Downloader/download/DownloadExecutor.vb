Imports System.ComponentModel

Namespace download
    Public Class DownloadExecutor
        Implements INotifyPropertyChanged

        Private Shared Instance As DownloadExecutor
        Private Sub New()
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
            End Set
        End Property

    End Class
End Namespace