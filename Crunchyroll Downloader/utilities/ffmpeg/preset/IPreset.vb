Namespace utilities.ffmpeg.preset

    ''' <summary>
    ''' An interface that makes it possible to specify video encoder presets with different string representations.
    ''' </summary>
    Public Interface IPreset
        Function GetPresetString() As String
    End Interface
End Namespace