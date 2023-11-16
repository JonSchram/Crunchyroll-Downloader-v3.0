Imports System.IO
Imports Microsoft.VisualBasic.FileIO

Namespace utilities
    ''' <summary>
    ''' A thin wrapper around file system calls.
    ''' </summary>
    Public Class RealFilesystem
        Implements IFilesystem

        Public Sub CreateDirectory(dir As String) Implements IFilesystem.CreateDirectory
            My.Computer.FileSystem.CreateDirectory(dir)
        End Sub

        Public Sub DeleteDirectory(dir As String) Implements IFilesystem.DeleteDirectory
            My.Computer.FileSystem.DeleteDirectory(dir, DeleteDirectoryOption.DeleteAllContents)
        End Sub

        Public Sub RenameFile(oldName As String, newName As String) Implements IFilesystem.RenameFile
            My.Computer.FileSystem.RenameFile(oldName, newName)
        End Sub

        Private Sub RenameDirectory(oldName As String, newName As String) Implements IFilesystem.RenameDirectory
            My.Computer.FileSystem.RenameDirectory(oldName, newName)
        End Sub

        Public Sub MoveFile(sourceFilePath As String, newFilePath As String) Implements IFilesystem.MoveFile
            My.Computer.FileSystem.MoveFile(sourceFilePath, newFilePath)
        End Sub

        Public Function DirectoryExists(path As String) As Boolean Implements IFilesystem.DirectoryExists
            Return My.Computer.FileSystem.DirectoryExists(path)
        End Function

        Public Function FileExists(path As String) As Boolean Implements IFilesystem.FileExists
            Return My.Computer.FileSystem.FileExists(path)
        End Function

        Public Async Function CopyToAsync(source As Stream, fileMode As FileMode, destination As String) As Task Implements IFilesystem.CopyToAsync
            Dim dest As New FileStream(destination, fileMode)
            Await source.CopyToAsync(dest)
            dest.Close()
        End Function
    End Class
End Namespace