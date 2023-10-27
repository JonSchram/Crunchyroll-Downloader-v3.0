Imports System.IO
Imports System.Net.Http
Imports Crunchyroll_Downloader.api.common
Imports Crunchyroll_Downloader.data

Namespace download
    Public MustInherit Class AbstractPlaybackDownloader
        Implements IPlaybackDownloader

        Protected Client As HttpClient

        Protected OutputDirectory As String
        Protected TemporaryDirectory As String

        ''' <summary>
        ''' Raised throughout the download process.
        ''' </summary>
        ''' <param name="sourceIndex"></param>
        ''' <param name="progress"></param>
        Public Event ReportDownloadProgress(sourceIndex As Integer, progress As Integer)
        Public Event ReportDownloadComplete(sourceIndex As Integer)

        Public Sub New(tempDir As String, finalDir As String)
            TemporaryDirectory = tempDir
            OutputDirectory = finalDir

            Client = New HttpClient()
        End Sub

        Protected Async Function DownloadSingleFile(media As FileMedia) As Task(Of DownloadEntry)
            Dim itemIndex As Integer = 0
            OnMediaProgress(itemIndex, 0)

            Dim response = Await Client.SendAsync(New HttpRequestMessage(HttpMethod.Get, media.OriginalLocation))
            response.EnsureSuccessStatusCode()

            Dim dataStream As Stream = Await response.Content.ReadAsStreamAsync()

            Dim downloadUri As New Uri(media.OriginalLocation)
            Dim filename = downloadUri.Segments(downloadUri.Segments.Count - 1)
            Dim temporaryPath = Path.Combine(TemporaryDirectory, filename)

            Dim dest As Stream = New FileStream(temporaryPath, FileMode.CreateNew)
            Await dataStream.CopyToAsync(dest)
            dest.Close()

            Dim record = New DownloadEntry(temporaryPath, media.Type)

            OnMediaProgress(itemIndex, 100)
            OnMediaComplete(itemIndex)
            Return record
        End Function

        Public MustOverride Async Function DownloadSelection(playbacks As Selection) As Task(Of DownloadEntry()) Implements IPlaybackDownloader.DownloadSelection

        Protected Sub OnMediaProgress(mediaIndex As Integer, progress As Integer)
            RaiseEvent ReportDownloadProgress(mediaIndex, progress)
        End Sub

        Protected Sub OnMediaComplete(mediaIndex As Integer)
            RaiseEvent ReportDownloadComplete(mediaIndex)
        End Sub

    End Class
End Namespace