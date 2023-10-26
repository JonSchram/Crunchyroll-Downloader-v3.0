Imports System.IO
Imports Crunchyroll_Downloader.api.common
Imports Crunchyroll_Downloader.hls.playlist.comparer
Imports Crunchyroll_Downloader.utilities

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

        Public Overrides Async Function DownloadPlaybacks(playbacks As List(Of Selection)) As Task(Of Integer)

            For Each playback In playbacks
                Dim media As IEnumerable(Of Media) = playback.Media

                For Each item In media
                    Await DownloadMediaItem(item)
                Next
            Next

            Return 0
        End Function

        Private Async Function DownloadMediaItem(item As Media) As Task
            If TypeOf item Is FileMedia Then
                Await DownloadSingleFile(CType(item, FileMedia))
            ElseIf TypeOf item Is MasterPlaylistMedia Then
                Await DownloadPlaylist(CType(item, MasterPlaylistMedia))
            End If
        End Function

        ''' <summary>
        ''' Downloads a program from a master playlist and returns the location it was saved to.
        ''' </summary>
        ''' <param name="item"></param>
        ''' <returns></returns>
        Private Async Function DownloadPlaylist(item As MasterPlaylistMedia) As Task(Of String)
            ' TODO: Use proper playlist comparer.
            Dim programNumber = item.MasterPlaylist.GetClosestMatchProgramNumber(New HighestResolutionComparer())

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

            Await ffmpegAdapter.Run(ffmpegArguments)

            Return outputName
        End Function
    End Class
End Namespace
