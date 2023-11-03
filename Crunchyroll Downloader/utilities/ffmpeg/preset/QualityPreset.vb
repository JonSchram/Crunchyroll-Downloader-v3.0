Namespace utilities.ffmpeg.preset
    ''' <summary>
    ''' A preset for codecs like AV1 whose preset value is an integer.
    ''' </summary>
    Public Class QualityPreset
        Implements IPreset

        Public ReadOnly Property Quality As Integer

        Public Sub New(quality As Integer)
            If quality < GetMinQuality() Or quality > GetMaxQuality() Then
                Throw New ArgumentException($"Quality {quality} is outside the allowed range.")
            End If

            Me.Quality = quality
        End Sub

        Public Shared Function GetMinQuality() As Integer
            Return 0
        End Function

        Public Shared Function GetMaxQuality() As Integer
            Return 8
        End Function

        Public Shared Function Best() As QualityPreset
            Return New QualityPreset(0)
        End Function

        Public Shared Function Fastest() As QualityPreset
            Return New QualityPreset(8)
        End Function

        Public Function GetPresetString() As String Implements IPreset.GetPresetString
            Return Quality.ToString()
        End Function
    End Class
End Namespace