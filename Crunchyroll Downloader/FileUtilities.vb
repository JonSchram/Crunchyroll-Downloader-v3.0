
Module FileUtilities
    Public Function GetSubDirectories(parentDirectory As String) As List(Of String)
        Dim subDirectories As List(Of String) = New List(Of String)
        Try
            Dim directory As New System.IO.DirectoryInfo(parentDirectory)
            For Each file As System.IO.DirectoryInfo In directory.EnumerateDirectories("*.*", System.IO.SearchOption.TopDirectoryOnly)
                ' TODO: Use ProgramSettings.SubfolderDisplay to filter out directories last modified too long ago
                If Not file.Attributes.HasFlag(System.IO.FileAttributes.Hidden) Then
                    subDirectories.Add(file.Name)
                End If
            Next
        Catch ex As Exception
            My.Application.Log.WriteException(ex, TraceEventType.Error, "Exception retrieving subdirectories of " & parentDirectory)
        End Try

        Return subDirectories
    End Function
End Module
