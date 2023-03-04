Namespace hls
    Public Class PlaylistParseException
        Inherits Exception

        Public Sub New()
        End Sub

        Public Sub New(Message As String)
            MyBase.New(Message)
        End Sub

        Public Sub New(Message As String, inner As Exception)
            MyBase.New(Message, inner)
        End Sub
    End Class
End Namespace