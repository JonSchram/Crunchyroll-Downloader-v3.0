Namespace hls.parsing
    Public Class PlaylistData
        Public ReadOnly Value As String
        Public ReadOnly Quoted As Boolean
        Public Sub New(value As String, quoted As Boolean)
            Me.Value = value
            Me.Quoted = quoted
        End Sub

        Public Overrides Function ToString() As String
            Return $"[PlaylistData: {Value}, quoted: {Quoted}]"
        End Function
    End Class
End Namespace