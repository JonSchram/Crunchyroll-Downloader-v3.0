Imports System.Drawing.Imaging
Imports System.IO
Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.utilities
Imports SiteAPI.api
Imports SiteAPI.api.common
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

        Public Function ProcessInputs(files As List(Of MediaFileEntry), ep As Episode) As List(Of MediaFileEntry)
            Dim results As New List(Of MediaFileEntry)

            Dim nameGenerator = New FilenameInterpolator(Preferences.NameTemplate, Preferences.SeasonDigitPadding, Preferences.EpisodeDigitPadding,
                                        Preferences.UseIso639Codes)

            Dim numberOfSubtitleFiles = GetNumberOfSubtitleFiles(files)
            Dim appendLanguageToSubtitles = (numberOfSubtitleFiles = 1 And Preferences.AppendLanguageToSingleSubtitles) Or numberOfSubtitleFiles > 1

            Dim outputPath As String = CreateSavePath(Preferences.OutputPath, ep)
            ' All files should use the same locale for consistency.
            Dim audioFile As MediaFileEntry = GetAudioEntry(files)
            Dim audioLocale As Locale = If(audioFile IsNot Nothing, GetDubLocale(audioFile.StreamLocales), Nothing)
            Dim baseFilename = nameGenerator.CreateName(ep, audioLocale)

            For fileNumber = 0 To files.Count - 1
                Dim file As MediaFileEntry = files(fileNumber)
                Dim extension As String = Path.GetExtension(file.Location)
                Dim currentFilename As String = baseFilename

                Dim subtitleLocale As Locale = GetSubtitleLocale(file.StreamLocales)
                If file.OnlyContainsMedia(MediaType.Subtitles) AndAlso appendLanguageToSubtitles AndAlso subtitleLocale IsNot Nothing Then
                    If Preferences.UseIso639Codes Then
                        currentFilename += $".{subtitleLocale.GetAbbreviatedString()}"
                    Else
                        currentFilename += $".{subtitleLocale}"
                    End If
                End If

                Dim savePath = GetUniqueFilename(FilesystemApi, outputPath, currentFilename, extension)

                FilesystemApi.MoveFile(file.Location, savePath)
                results.Add(New MediaFileEntry(savePath, file.ContainedMedia, file.StreamLocales))
            Next

            Return results
        End Function

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

        ''' <summary>
        ''' Gets the locale of the dub. This is used when renaming a file, and should be the audio locale, but it uses the video locale as a fallback.
        ''' </summary>
        ''' <returns></returns>
        Private Function GetDubLocale(locales As IDictionary(Of MediaType, Locale)) As Locale
            Dim audioLocale As Locale = Nothing
            If locales.TryGetValue(MediaType.Audio, audioLocale) Then
                Return audioLocale
            End If

            Dim videoLocale As Locale = Nothing
            If locales.TryGetValue(MediaType.Audio, videoLocale) Then
                Return videoLocale
            End If

            Return Nothing
        End Function

        Private Function GetSubtitleLocale(locales As IDictionary(Of MediaType, Locale)) As Locale
            Dim l As Locale = Nothing
            If locales.TryGetValue(MediaType.Subtitles, l) Then
                Return l
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Gets the audio file from the MediaFileEntry list.
        ''' </summary>
        ''' <param name="files"></param>
        ''' <returns></returns>
        Private Function GetAudioEntry(files As List(Of MediaFileEntry)) As MediaFileEntry
            For Each file In files
                If file.ContainedMedia.HasFlag(MediaType.Audio) Then
                    Return file
                End If
            Next
            Return Nothing
        End Function

        Private Function GetNumberOfSubtitleFiles(files As List(Of MediaFileEntry)) As Integer
            Dim count As Integer = 0
            For Each file In files
                If file.OnlyContainsMedia(MediaType.Subtitles) Then
                    count += 1
                End If
            Next
            Return count
        End Function

    End Class
End Namespace