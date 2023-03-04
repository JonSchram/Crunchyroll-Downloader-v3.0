Class HlsHelpers

    Public Shared Function ParseYesNoValue(Value As String, AttributeName As String) As Boolean
        Select Case Value
            Case "YES"
                Return True
            Case "NO"
                Return False
            Case Else
                Throw New HlsFormatException($"{AttributeName} boolean value must be YES or NO, but was {Value}")
        End Select
    End Function
End Class
