Namespace hls.segment
    Public Class ByteRange

        Public Property Length As Integer

        ' Use a long int becasue offsets on long media streams can easily exceed a 32-bit int
        Public Property Offset As Long

        Public Sub New(RangeString As String)
            If RangeString Is Nothing Then
                Throw New HlsFormatException("ByteRange formatting incorrect, cannot be Nothing")
            End If

            If RangeString.Contains("@"c) Then
                ' Length & offset pair
                Dim Parts() As String = Split(RangeString, "@")
                If Parts.Length <> 2 Then
                    Throw New HlsFormatException($"Byte range format incorrect: {RangeString}")
                End If
                Length = CInt(Parts(0))
                Offset = CLng(Parts(1))
            Else
                Length = CInt(RangeString)
            End If
        End Sub

        Public Sub New(length As Integer, offset As Long)
            Me.Length = length
            Me.Offset = offset
        End Sub

        Public Sub New(other As ByteRange)
            Me.New(other.Length, other.Offset)
        End Sub

        Public Overrides Function ToString() As String
            Return $"{{
Length: {Length},
Offset: {Offset}
}}"
        End Function
    End Class

End Namespace