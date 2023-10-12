Imports System.IO
Imports System.Net.Http
Imports Crunchyroll_Downloader.api.common

Namespace download
    Public MustInherit Class AbstractPlaybackDownloader
        Implements IPlaybackDownloader

        Protected Client As HttpClient

        Protected OutputDirectory As String
        Protected TemporaryDirectory As String

        ''' <summary>
        ''' Complete media tracks that can be added to a container.
        ''' This should not contain fragments of a media stream, but it should contain the result of concatenating
        ''' these streams into a video or audio track.
        ''' </summary>
        Protected DownloadedTracks As New List(Of FileRecord)

        Public Sub New(tempDir As String, finalDir As String)
            TemporaryDirectory = tempDir
            OutputDirectory = finalDir

            Client = New HttpClient()
        End Sub

        Protected Async Function DownloadSingleFile(media As CompleteMedia) As Task
            Dim response = Await Client.SendAsync(New HttpRequestMessage(HttpMethod.Get, media.Url))
            If response.StatusCode = Net.HttpStatusCode.OK Then
                Dim dataStream As Stream = Await response.Content.ReadAsStreamAsync()

                Dim downloadUri As New Uri(media.Url)
                Dim filename = downloadUri.Segments(downloadUri.Segments.Count - 1)
                Dim temporaryPath = Path.Combine(TemporaryDirectory, filename)

                Dim dest As Stream = New FileStream(temporaryPath, FileMode.CreateNew)
                Await dataStream.CopyToAsync(dest)
                dest.Close()

                DownloadedTracks.Append(New FileRecord(media.Type, temporaryPath))
            End If
        End Function

        Public MustOverride Async Sub DownloadPlaybacks(playbacks As List(Of Playback)) Implements IPlaybackDownloader.DownloadPlaybacks

        Protected Class FileRecord
            Public ReadOnly Type As MediaType
            Public ReadOnly Path As String
            Public Sub New(type As MediaType, path As String)
                Me.Type = type
                Me.Path = path
            End Sub
        End Class
    End Class
End Namespace