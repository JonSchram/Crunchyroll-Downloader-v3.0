
Imports System.IO

Module FileUtilities
    Public Function GetSubDirectories(parentDirectory As String) As List(Of String)
        Dim subDirectories As New List(Of String)
        Try
            Dim directory As New DirectoryInfo(parentDirectory)
            For Each file As DirectoryInfo In directory.EnumerateDirectories("*.*", SearchOption.TopDirectoryOnly)
                ' TODO: Use ProgramSettings.SubfolderDisplay to filter out directories last modified too long ago
                If Not file.Attributes.HasFlag(FileAttributes.Hidden) Then
                    subDirectories.Add(file.Name)
                End If
            Next
        Catch ex As Exception
            My.Application.Log.WriteException(ex, TraceEventType.Error, "Exception retrieving subdirectories of " & parentDirectory)
        End Try

        Return subDirectories
    End Function

    Public Function GetNewTempDirectory(parentDirectory As String) As DirectoryInfo
        Dim folderName = Guid.NewGuid().ToString()

        Dim parent = New DirectoryInfo(parentDirectory)

        Dim attemptLimit = 5
        Dim attemptCount = 0
        Do While attemptCount < attemptLimit
            Try
                Dim tempFolder = parent.CreateSubdirectory(folderName)
                Return tempFolder
            Catch ex As IOException
                attemptCount += 1
                folderName = Guid.NewGuid().ToString()
            End Try
        Loop
        Throw New Exception("Could not create temp directory")
    End Function
End Module
