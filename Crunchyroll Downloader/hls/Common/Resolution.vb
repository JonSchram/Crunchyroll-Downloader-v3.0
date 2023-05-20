Namespace hls.common
    ''' <summary>
    ''' Horizontal and vertical resolution ofa  video stream.
    ''' </summary>
    Public Class Resolution
        Public Sub New(horizontalResolution As Integer, VerticalResolution As Integer)
            Horizontal = horizontalResolution
            Vertical = VerticalResolution
        End Sub

        Public ReadOnly Property Horizontal As Double
        Public ReadOnly Property Vertical As Double

        Public Overrides Function ToString() As String
            Return $"{Horizontal}x{Vertical}"
        End Function
    End Class
End Namespace