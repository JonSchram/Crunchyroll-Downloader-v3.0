Namespace api.common
    Public Class ByteRange
        Public ReadOnly Property Length As Integer
        ' Use a Long integer because the offset may get large with long streams
        Public ReadOnly Property Offset As Long?

        Public Sub New(length As Integer, offset As Long?)
            Me.Length = length
            Me.Offset = offset
        End Sub
    End Class
End Namespace