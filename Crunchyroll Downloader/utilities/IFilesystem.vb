Imports System.IO

Namespace utilities
    ''' <summary>
    ''' An interface for performing file system operations, to make it possible to test file system interactions.
    ''' </summary>
    Public Interface IFilesystem
        Sub CreateDirectory(dir As String)
        Sub RenameFile(oldName As String, newName As String)
        Sub RenameDirectory(oldName As String, newName As String)

        Sub MoveFile(sourceFilePath As String, newFilePath As String)

        Function DirectoryExists(path As String) As Boolean

        Function FileExists(path As String) As Boolean

        Function CopyToAsync(source As Stream, fileMode As FileMode, destination As String) As Task
    End Interface
End Namespace