Imports Crunchyroll_Downloader.utilities

Namespace utilities
    Public Class FakeFileSystem
        Implements IFilesystem

        Private ReadOnly ExistingDirectories As New HashSet(Of String)
        Private ReadOnly ExistingFiles As New HashSet(Of String)

        Public Property CreatedDirectories As New List(Of String)
        Public Property RenamedFiles As New List(Of ModifiedFile)
        Public Property RenamedDirectories As New List(Of ModifiedFile)
        Public Property MovedFiles As New List(Of ModifiedFile)

        Public Sub CreateDirectory(dir As String) Implements IFilesystem.CreateDirectory
            CreatedDirectories.Add(dir)
        End Sub

        Public Sub RenameFile(oldName As String, newName As String) Implements IFilesystem.RenameFile
            RenamedFiles.Add(New ModifiedFile(oldName, newName))
        End Sub

        Public Sub RenameDirectory(oldName As String, newName As String) Implements IFilesystem.RenameDirectory
            RenamedDirectories.Add(New ModifiedFile(oldName, newName))
        End Sub

        Public Sub MoveFile(sourceFilePath As String, newFilePath As String) Implements IFilesystem.MoveFile
            MovedFiles.Add(New ModifiedFile(sourceFilePath, newFilePath))
        End Sub


        Public Sub ClearDirectories()
            ExistingDirectories.Clear()
        End Sub
        Public Sub AddDirectory(dir As String)
            ExistingDirectories.Add(dir)
        End Sub

        Public Function DirectoryExists(path As String) As Boolean Implements IFilesystem.DirectoryExists
            Return ExistingDirectories.Contains(path)
        End Function

        Public Sub ClearFiles()
            ExistingFiles.Clear()
        End Sub

        Public Sub AddFile(file As String)
            ExistingFiles.Add(file)
        End Sub

        Public Function FileExists(path As String) As Boolean Implements IFilesystem.FileExists
            Return ExistingFiles.Contains(path)
        End Function

        Public Class ModifiedFile
            Public ReadOnly Property OldPath As String
            Public ReadOnly Property NewPath As String

            Public Sub New(oldPath As String, newPath As String)
                Me.OldPath = oldPath
                Me.NewPath = newPath
            End Sub
        End Class
    End Class
End Namespace