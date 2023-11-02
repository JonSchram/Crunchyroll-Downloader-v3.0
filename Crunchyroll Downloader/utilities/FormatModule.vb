Namespace utilities
    Module FormatModule
        Public Function PadNumber(totalWidth As Integer, number As Integer) As String
            Return number.ToString().PadLeft(totalWidth, "0"c)
        End Function

        ''' <summary>
        ''' Pads a double-precision number with leading zeros until the integer portion has the specified total width.
        ''' 
        ''' This is to maintain consistency with the integer overload of this method, so that episode 6.5 is printed as '06.5'
        ''' to match episode 6 printed as '06'.
        ''' </summary>
        ''' <param name="integerWidth"></param>
        ''' <param name="number"></param>
        ''' <returns></returns>
        Public Function PadNumber(integerWidth As Integer, number As Double) As String
            Dim integerPart As Integer = CInt(Int(number))
            Dim fractionPart As Double = number - integerPart

            Return PadNumber(integerWidth, integerPart) + String.Format("{0:.##}", fractionPart)
        End Function
    End Module
End Namespace