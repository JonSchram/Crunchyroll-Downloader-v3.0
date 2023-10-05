Namespace download

    ''' <summary>
    ''' A downloader that is backed by ffmpeg.
    ''' </summary>
    Public Class FfmpegDownloader
        Inherits AbstractPlaybackDownloader
        Implements IPlaybackDownloader

        Public Sub New(tempDir As String, finalDir As String)
            MyBase.New(tempDir, finalDir)
        End Sub

        Public Overrides Async Sub DownloadPlaybacks(playbacks As List(Of Playback))

            For Each playback In playbacks
                Dim media As IEnumerable(Of Media) = playback.Media

                For Each item In media
                    Await DownloadMediaItem(item)
                Next
            Next
        End Sub

        Private Async Function DownloadMediaItem(item As Media) As Task
            If TypeOf item Is CompleteMedia Then
                Await DownloadSingleFile(CType(item, CompleteMedia))
            End If
        End Function
    End Class
End Namespace
