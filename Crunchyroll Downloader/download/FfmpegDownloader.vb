﻿Imports System.IO
Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.utilities
Imports PlaylistLibrary.hls.playlist.comparer
Imports SiteAPI.api.common

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

        Public Overrides Async Function DownloadSelection(playback As Selection) As Task(Of DownloadEntry())
            ' TODO: Allow sending progress.
            ' Right now, this just awaits all the downloads but the download thread has no idea what is finished.

            Dim allTasks As New List(Of Task(Of DownloadEntry))

            Dim media As IEnumerable(Of Media) = playback.Media

            For Each item In media
                allTasks.Add(DownloadMediaItem(item))
            Next
            Dim completedRecords As DownloadEntry() = Await Task.WhenAll(allTasks)


            Return completedRecords
        End Function

        Private Function DownloadMediaItem(item As Media) As Task(Of DownloadEntry)
            If TypeOf item Is FileMedia Then
                Return DownloadSingleFile(CType(item, FileMedia))
            ElseIf TypeOf item Is MasterPlaylistMedia Then
                Return DownloadPlaylist(CType(item, MasterPlaylistMedia))
            End If

            ' Should never happen.
            Return Nothing
        End Function

        ''' <summary>
        ''' Downloads a program from a master playlist and returns the location it was saved to.
        ''' </summary>
        ''' <param name="item"></param>
        ''' <returns></returns>
        Private Async Function DownloadPlaylist(item As MasterPlaylistMedia) As Task(Of DownloadEntry)
            Dim itemIndex As Integer = 0
            OnMediaProgress(itemIndex, 0)

            ' TODO: Use proper playlist comparer.
            Dim programNumber = item.Playlist.GetClosestMatchProgramNumber(New HighestResolutionComparer())

            ' TODO: Make a proper file name and file format.
            Dim outputName = Path.Combine(OutputDirectory, "playlist.mp4")
            Dim ffmpegArguments As New FfmpegArguments(outputName)
            ffmpegArguments.InputFiles.Add(item.OriginalLocation)
            ffmpegArguments.SelectedStreams.Add(New FfmpegArguments.MapArgument() With {
                .Selector = New FfmpegArguments.StreamSpecifier() With {
                    .ProgramNumber = programNumber
                }
            })
            ffmpegArguments.Codecs.Add(New FfmpegArguments.CodecArgument() With {
                .Name = FfmpegArguments.CodecName.COPY
            })
            ' TODO: Allow configuring ffmpeg exe location.
            Dim ffmpegAdapter As New FfmpegAdapter(Path.Combine(Application.StartupPath, "ffmpeg.exe"))
            ffmpegAdapter.SetUserAgent("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36")
            AddHandler ffmpegAdapter.ReportProgress, AddressOf HandleFfmpegProgress

            Dim statusCode As Integer = Await ffmpegAdapter.Run(ffmpegArguments)

            If statusCode <> 0 Then
                Throw New Exception($"Ffmpeg exited with error: {statusCode}")
            End If

            OnMediaProgress(itemIndex, 100)
            OnMediaComplete(itemIndex)

            Return New DownloadEntry(outputName, MediaType.Audio Or MediaType.Video)
        End Function

        Private Sub HandleFfmpegProgress(amount As Integer)
            ' TODO: Real media index
            OnMediaProgress(0, amount)
        End Sub
    End Class
End Namespace
