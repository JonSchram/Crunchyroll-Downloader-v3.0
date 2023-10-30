Namespace utilities.ffmpeg
    Public Class FfmpegProgressReport
        Public ReadOnly Property FrameNumber As Integer?
        Public ReadOnly Property FramesPerSecond As Double?
        Public ReadOnly Property Q As Double?
        Public ReadOnly Property CurrentSizeBytes As Double?
        Public ReadOnly Property CurrentTime As TimeSpan?
        Public ReadOnly Property BitsPerSecond As Double?
        Public ReadOnly Property Speed As Double?

        Public Sub New(frameNumber As Integer?, framesPerSecond As Double?, q As Double?, CurrentSizeBytes As Double?, currentTime As TimeSpan?,
                       BitsPerSecond As Double?, speed As Double?)
            Me.FrameNumber = frameNumber
            Me.FramesPerSecond = framesPerSecond
            Me.Q = q
            Me.CurrentSizeBytes = CurrentSizeBytes
            Me.CurrentTime = currentTime
            Me.BitsPerSecond = BitsPerSecond
            Me.Speed = speed
        End Sub

    End Class
End Namespace