Namespace hls.common
    Public Class PlaylistStartTime
        Public ReadOnly Property TimeOffset As Double

        Public ReadOnly Property Precise As Boolean = False

        Public Sub New(offset As Double, precise As Boolean)
            TimeOffset = offset
            Me.Precise = precise
        End Sub

        Public Sub New(start As PlaylistStartTime)
            TimeOffset = start.TimeOffset
            Precise = start.Precise
        End Sub
    End Class
End Namespace