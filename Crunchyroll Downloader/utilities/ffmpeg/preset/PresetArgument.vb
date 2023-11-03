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

    End Class
End Namespace