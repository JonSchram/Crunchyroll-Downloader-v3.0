Imports System.IO

Namespace utilities
    Module FileModule

        Public Function GetUniqueFilename(filesystemApi As IFilesystem, pathToFile As String) As String
            Dim basePath As String = Path.GetFullPath(pathToFile)
            Dim baseName As String = Path.GetFileNameWithoutExtension(pathToFile)
            Dim extension As String = Path.GetExtension(pathToFile)
            Return GetUniqueFilename(filesystemApi, basePath, baseName, extension)
        End Function

        Public Function GetUniqueFilename(filesystemApi As IFilesystem, outputPath As String, baseFileName As String, extension As String) As String
            Dim iterationNumber As Integer = 0
            Dim newFilePath As String = Path.Combine(outputPath, baseFileName + extension)
            While filesystemApi.FileExists(newFilePath)
                ' Intentionally increment iterationNumber first so that the file renaming starts at 1.
                iterationNumber += 1

                newFilePath = Path.Combine(outputPath, $"{baseFileName} ({iterationNumber}){extension}")
            End While

            Return newFilePath
        End Function
    End Module
End Namespace