Namespace utilities
    Public Class FfmpegArguments
        Public Property PlaylistLocation As String

        ''' <summary>
        ''' Which program to select from a master playlist.
        ''' </summary>
        ''' <returns></returns>
        Public Property ProgramNumber As Integer?

        Public ReadOnly Property OutputPath As String

        Public Sub New(OutputLocation As String)
            OutputPath = OutputLocation
        End Sub
    End Class
End Namespace