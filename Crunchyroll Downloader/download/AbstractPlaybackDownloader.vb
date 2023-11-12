Imports System.IO
Imports System.Net.Http
Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.utilities
Imports SiteAPI.api.common

Namespace download
    Public MustInherit Class AbstractPlaybackDownloader
        Implements IPlaybackDownloader

        Protected Client As IHttpClient

        Protected Preferences As DownloadPreferences
        Protected FilesystemApi As IFilesystem

        Public Event ReportDownloadProgress(currentFile As Integer, totalFiles As Integer, progress As Double)
        Public Event ReportDownloadComplete(fileNumber As Integer, totalFiles As Integer)

        Public Sub New(preferences As DownloadPreferences, client As IHttpClient)
            Me.Preferences = preferences
            Me.Client = client
        End Sub

        Protected Async Function DownloadSingleFile(media As FileMedia, currentIndex As Integer, totalFiles As Integer) As Task(Of MediaFileEntry)
            OnMediaProgress(currentIndex, totalFiles, 0)

            Dim response = Await Client.SendAsync(New HttpRequestMessage(HttpMethod.Get, media.OriginalLocation))
            response.EnsureSuccessStatusCode()

            Dim dataStream As Stream = Await response.Content.ReadAsStreamAsync()

            Dim downloadUri As New Uri(media.OriginalLocation)
            Dim filename As String = downloadUri.Segments(downloadUri.Segments.Count - 1)
            Dim baseFilename As String = Path.GetFileNameWithoutExtension(filename)
            Dim extension As String = Path.GetExtension(filename)
            Dim uniqueTemporaryPath = GetUniqueFilename(FilesystemApi, Preferences.TemporaryDirectory, baseFilename, extension)

            Await FilesystemApi.CopyToAsync(dataStream, FileMode.CreateNew, uniqueTemporaryPath)

            Dim record = New MediaFileEntry(uniqueTemporaryPath, media.Type, media.MediaLocale)

            OnMediaProgress(currentIndex, totalFiles, 100)
            OnMediaComplete(currentIndex, totalFiles)
            Return record
        End Function

        Public MustOverride Async Function DownloadSelection(playbacks As Selection) As Task(Of List(Of MediaFileEntry)) Implements IPlaybackDownloader.DownloadSelection

        Protected Sub OnMediaProgress(mediaIndex As Integer, totalFiles As Integer, progress As Double)
            RaiseEvent ReportDownloadProgress(mediaIndex, totalFiles, progress)
        End Sub

        Protected Sub OnMediaComplete(mediaIndex As Integer, totalFiles As Integer)
            RaiseEvent ReportDownloadComplete(mediaIndex, totalFiles)
        End Sub


        Protected Class ProgressReporter
            'Public Sub New(callback As Action, totalFiles)

            'End Sub


        End Class
    End Class
End Namespace