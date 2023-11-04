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

        ''' <summary>
        ''' Raised throughout the download process.
        ''' </summary>
        ''' <param name="sourceIndex"></param>
        ''' <param name="progress"></param>
        Public Event ReportDownloadProgress(sourceIndex As Integer, progress As Integer)
        Public Event ReportDownloadComplete(sourceIndex As Integer)

        Public Sub New(preferences As DownloadPreferences, client As IHttpClient)
            Me.Preferences = preferences
            Me.Client = client
        End Sub

        Protected Async Function DownloadSingleFile(media As FileMedia) As Task(Of MediaFileEntry)
            Dim itemIndex As Integer = 0
            OnMediaProgress(itemIndex, 0)

            Dim response = Await Client.SendAsync(New HttpRequestMessage(HttpMethod.Get, media.OriginalLocation))
            response.EnsureSuccessStatusCode()

            Dim dataStream As Stream = Await response.Content.ReadAsStreamAsync()

            Dim downloadUri As New Uri(media.OriginalLocation)
            Dim filename As String = downloadUri.Segments(downloadUri.Segments.Count - 1)
            Dim baseFilename As String = Path.GetFileNameWithoutExtension(filename)
            Dim extension As String = Path.GetExtension(filename)
            Dim uniqueTemporaryPath = GetUniqueFilename(FilesystemApi, Preferences.TemporaryDirectory, baseFilename, extension)

            Await FilesystemApi.CopyToAsync(dataStream, FileMode.CreateNew, uniqueTemporaryPath)
            'Dim dest As Stream = New FileStream(uniqueTemporaryPath, FileMode.CreateNew)
            'Await dataStream.CopyToAsync(dest)
            'dest.Close()

            Dim record = New MediaFileEntry(uniqueTemporaryPath, media.Type)

            OnMediaProgress(itemIndex, 100)
            OnMediaComplete(itemIndex)
            Return record
        End Function

        Public MustOverride Async Function DownloadSelection(playbacks As Selection) As Task(Of MediaFileEntry()) Implements IPlaybackDownloader.DownloadSelection

        Protected Sub OnMediaProgress(mediaIndex As Integer, progress As Integer)
            RaiseEvent ReportDownloadProgress(mediaIndex, progress)
        End Sub

        Protected Sub OnMediaComplete(mediaIndex As Integer)
            RaiseEvent ReportDownloadComplete(mediaIndex)
        End Sub

    End Class
End Namespace