Namespace utilities

    Public Class ProgressEstimator
        Private ReadOnly sw As Stopwatch

        Public Sub New()
            sw = New Stopwatch()
        End Sub

        Public Sub Start()
            sw.Start()
        End Sub

        Public Sub Pause()
            sw.Stop()
        End Sub

        Public Sub Reset()
            sw.Reset()
        End Sub

        Public Function GetElapsedTime() As TimeSpan
            Return sw.Elapsed
        End Function

        Public Function GetRemainingTime(currentPercent As Double) As TimeSpan
            Dim elapsedSeconds As Double = GetElapsedTime().TotalSeconds
            If elapsedSeconds <> 0 And currentPercent <> 0 Then
                Dim percentPerSecond As Double = currentPercent / elapsedSeconds
                Dim totalSeconds As Double = 100 / percentPerSecond
                Return TimeSpan.FromSeconds(totalSeconds - elapsedSeconds)
            Else
                Return Nothing
            End If
        End Function
    End Class
End Namespace