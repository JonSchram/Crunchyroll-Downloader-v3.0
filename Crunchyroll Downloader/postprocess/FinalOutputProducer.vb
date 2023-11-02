Imports System.IO
Imports System.Runtime.InteropServices
Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.utilities
Imports SiteAPI.api.metadata

Namespace postprocess
    ''' <summary>
    ''' Class that moves and renames complete file media items, completing the download process.
    ''' </summary>
    Public Class FinalOutputProducer

        Private ReadOnly Preferences As OutputPreferences
        Private ReadOnly FilesystemApi As IFilesystem

        Public Sub New(preferences As OutputPreferences, fileSystemApi As IFilesystem)
            Me.Preferences = preferences
            Me.FilesystemApi = fileSystemApi
        End Sub

        Public Sub ProcessInputs(files As List(Of MediaFileEntry), ep As Episode)
            Dim nameGenerator = New FilenameInterpolator(Preferences.NameTemplate, Preferences.SeasonDigitPadding, Preferences.EpisodeDigitPadding)

            ' TODO: Append language name according to subtitle naming preference.
            Dim baseFilename = nameGenerator.CreateName(ep, False)
            Dim outputPath As String = CreateSavePath(Preferences.OutputPath, ep)

            For fileNumber = 0 To files.Count - 1
                Dim file As MediaFileEntry = files(fileNumber)
                Dim extension As String = Path.GetExtension(file.Location)

                Dim savePath = CreateSaveLocation(outputPath, baseFilename, extension)

                FilesystemApi.MoveFile(file.Location, savePath)
            Next

        End Sub

        ''' <summary>
        ''' Creates a file path to save the episode at, appending show and/or season directories if indicated in preferences.
        ''' </summary>
        ''' <param name="outputPath"></param>
        ''' <param name="ep"></param>
        ''' <returns></returns>
        Private Function CreateSavePath(outputPath As String, ep As Episode) As String
            Dim finalPath As String = outputPath

            If Preferences.UseShowPath Then
                finalPath = Path.Combine(finalPath, ep.ShowName)
            End If
            If Preferences.UseSeasonPath Then
                Dim paddedSeasonNumber As String = PadNumber(Preferences.SeasonDigitPadding, ep.SeasonNumber)
                finalPath = Path.Combine(finalPath, $"Season {paddedSeasonNumber}")
            End If

            Return finalPath
        End Function

        Public Function CreateSaveLocation(outputPath As String, baseFileName As String, extension As String) As String
            Dim iterationNumber As Integer = 0
            Dim newFilePath As String = Path.Combine(outputPath, baseFileName + extension)
            While FilesystemApi.FileExists(newFilePath)
                ' Intentionally increment iterationNumber first so that the file renaming starts at 1.
                iterationNumber += 1

                newFilePath = Path.Combine(outputPath, $"{baseFileName} ({iterationNumber}){extension}")
            End While

            Return newFilePath
        End Function

    End Class
End Namespace