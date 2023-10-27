Imports PlaylistLibrary.hls.common

Namespace hls.segment
    Public Class MediaInitialization
        Public ReadOnly Property Uri As String
        Public ReadOnly Property Bytes As ByteRange

        Public Sub New(uri As String, bytes As ByteRange)
            Me.Uri = uri
            Me.Bytes = bytes
        End Sub

    End Class
End Namespace