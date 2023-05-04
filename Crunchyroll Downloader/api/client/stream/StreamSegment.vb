Namespace api.client.stream
    Public Class StreamSegment
        Public ReadOnly Property Uri As String

        Public ReadOnly Property DownloadRange As ByteRange

        Public ReadOnly Property SegmentNumber As Integer

        Public Sub New(uri As String, DownloadRange As ByteRange, SegmentNumber As Integer)
            Me.Uri = uri
            Me.DownloadRange = DownloadRange
            Me.SegmentNumber = SegmentNumber
        End Sub
    End Class
End Namespace