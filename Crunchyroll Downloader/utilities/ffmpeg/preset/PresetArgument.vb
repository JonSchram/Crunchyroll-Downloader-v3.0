Namespace utilities.ffmpeg.preset

    ''' <summary>
    ''' An argument passed to ffmpeg specifying some form of video preset.
    ''' </summary>
    Public MustInherit Class PresetArgument

        Private ReadOnly PresetValue As IPreset

        Public Sub New(value As IPreset)
            PresetValue = value
        End Sub

        Public Function BuildCommandLineArgument() As String
            Return $"-preset {PresetValue.GetPresetString()}"
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            Dim argument = TryCast(obj, PresetArgument)
            Return argument IsNot Nothing AndAlso
                   EqualityComparer(Of IPreset).Default.Equals(PresetValue, argument.PresetValue)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Dim hashCode As Long = -1752849569
            hashCode = (hashCode * -1521134295 + EqualityComparer(Of IPreset).Default.GetHashCode(PresetValue)).GetHashCode()
            Return CType(hashCode, Integer)
        End Function
    End Class
End Namespace