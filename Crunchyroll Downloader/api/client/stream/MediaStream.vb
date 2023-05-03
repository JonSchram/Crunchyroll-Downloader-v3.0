Namespace api.client.stream
    Public MustInherit Class MediaStream
        Public ReadOnly Property Type As MediaType

        Public Sub New(type As MediaType)
            Me.Type = type
        End Sub
    End Class
End Namespace