Namespace api.common
    Public Class FileMedia
        Inherits MediaLink
        Public ReadOnly Property Uri As String

        Public Sub New(type As MediaType, uri As String)
            MyBase.New(type)
            Me.Uri = uri
        End Sub
    End Class
End Namespace