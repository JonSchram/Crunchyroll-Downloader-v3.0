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

        Public Sub New(tempDir As String, finalDir As String)
            TemporaryDirectory = tempDir
            OutputDirectory = finalDir

            Client = New HttpClient()
        End Sub

        Protected Async Function DownloadSingleFile(media As FileMedia) As Task(Of DownloadEntry)
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
            Return record
        End Function

        Public MustOverride Async Function DownloadPlaybacks(playbacks As List(Of Selection)) As Task(Of DownloadEntry()) Implements IPlaybackDownloader.DownloadPlaybacks

    End Class
End Namespace