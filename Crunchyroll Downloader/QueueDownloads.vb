''' <summary>
''' 
''' Adds anime to download from a URL with specified output directory and naming.
''' This may be either a series or an individual video.
''' 
''' </summary>
Public Class QueueDownloads
    Private parentDirectory As String
    Private subFolder As String
    Public Sub New(parentDirectory As String, subFolder As String)
        Me.parentDirectory = parentDirectory
        Me.subFolder = subFolder
    End Sub

    Public Sub enqueue(url As String)

    End Sub
End Class
