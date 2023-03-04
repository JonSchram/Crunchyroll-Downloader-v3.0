Public Class ByteRange

    Public Property Length As Integer

    ' Use a long int becasue offsets on long media streams can easily exceed a 32-bit int
    Public Property Offset As Long

End Class
