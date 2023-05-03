Namespace api.client.stream
    Public Class FileMedia
        Inherits MediaStream
        Public ReadOnly Property Uri As String

        Public Sub New(type As MediaType, uri As String)
            MyBase.New(type)
            Me.Uri = uri
        End Sub
    End Class
End Namespace