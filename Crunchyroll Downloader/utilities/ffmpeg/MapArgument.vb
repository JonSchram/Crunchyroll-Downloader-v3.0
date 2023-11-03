Namespace utilities.ffmpeg
    Public Class MapArgument
        ''' <summary>
        ''' Which input file this mapping belongs to.
        ''' Defaults to 0, for the first stream.
        ''' </summary>
        ''' <returns></returns>
        Public Property InputFileNumber As Integer = 0
        ''' <summary>
        ''' Whether to exclude instead of include this item when matching streams.
        ''' </summary>
        ''' <returns></returns>
        Public Property Exclude As Boolean = False

        Public Property Selector As StreamSpecifier

        Public Property IsOptional As Boolean = False

        Public Overrides Function Equals(obj As Object) As Boolean
            Dim argument = TryCast(obj, MapArgument)
            Return argument IsNot Nothing AndAlso
                   InputFileNumber = argument.InputFileNumber AndAlso
                   Exclude = argument.Exclude AndAlso
                   EqualityComparer(Of StreamSpecifier).Default.Equals(Selector, argument.Selector) AndAlso
                   IsOptional = argument.IsOptional
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return (InputFileNumber, Exclude, Selector, IsOptional).GetHashCode()
        End Function
    End Class

End Namespace