Namespace api.client.stream
    Public Class MediaSegment
        Public ReadOnly Property Uri As String

        Public ReadOnly Property DownloadRange As ByteRange

        Public Sub New(uri As String, range As ByteRange)
            Me.Uri = uri
            DownloadRange = range
        End Sub
    End Class
End Namespace