Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.utilities
Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports PlaylistLibrary.hls.playlist.comparer
Imports SiteAPI.api.common

Namespace download

    ''' <summary>
    ''' A downloader that is backed by ffmpeg.
    ''' </summary>
    Public Class FfmpegDownloader
        Inherits AbstractPlaybackDownloader
        Implements IPlaybackDownloader

        Private FfmpegRunner As IFfmpegAdapter

        Public Sub New(preferences As DownloadPreferences, ffmpegRunner As IFfmpegAdapter, fileSystemApi As IFilesystem, client As IHttpClient)
            MyBase.New(preferences, client)
            Me.FilesystemApi = fileSystemApi
            Me.FfmpegRunner = ffmpegRunner
        End Sub

        Public Overrides Async Function DownloadSelection(playback As Selection) As Task(Of MediaFileEntry())
            ' TODO: Allow sending progress.
            ' Right now, this just awaits all the downloads but the download thread has no idea what is finished.

            Dim allTasks As New List(Of Task(Of MediaFileEntry))

            Dim media As IEnumerable(Of Media) = playback.Media

            For Each item In media
                allTasks.Add(DownloadMediaItem(item))
            Next
            Dim completedRecords As MediaFileEntry() = Await Task.WhenAll(allTasks)


            Return completedRecords
        End Function

        Private Function DownloadMediaItem(item As Media) As Task(Of MediaFileEntry)
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
        Private Async Function DownloadPlaylist(item As MasterPlaylistMedia) As Task(Of MediaFileEntry)
            Dim itemIndex As Integer = 0
            OnMediaProgress(itemIndex, 0)

            ' TODO: Use proper playlist comparer.
            Dim programNumber = item.Playlist.GetClosestMatchProgramNumber(New LowestResolutionComparer())

            ' TODO: Ensure that mp4 is the correct file format, because this assumes mp4 can always contain the media.
            Dim outputName = GetUniqueFilename(FilesystemApi, Preferences.TemporaryDirectory, "downloaded-media", ".mp4")
            Dim ffmpegArguments As New FfmpegArguments(outputName)
            ffmpegArguments.InputFiles.Add(item.OriginalLocation)
            ffmpegArguments.SelectedStreams.Add(New MapArgument() With {
                .Selector = New StreamSpecifier() With {
                    .ProgramNumber = programNumber
                }
            })
            ffmpegArguments.Codecs.Add(New CopyCodecArgument())
            ffmpegArguments.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36"
            AddHandler FfmpegRunner.ReportProgress, AddressOf HandleFfmpegProgress

            Dim statusCode As Integer = Await FfmpegRunner.Run(ffmpegArguments)
            RemoveHandler FfmpegRunner.ReportProgress, AddressOf HandleFfmpegProgress

            If statusCode <> 0 Then
                Throw New Exception($"Ffmpeg exited with error: {statusCode}")
            End If

            OnMediaProgress(itemIndex, 100)
            OnMediaComplete(itemIndex)

            Return New MediaFileEntry(outputName, MediaType.Audio Or MediaType.Video, item.MediaLocale)
        End Function

        Private Sub HandleFfmpegProgress(amount As Integer)
            ' TODO: Real media index
            OnMediaProgress(0, amount)
        End Sub
    End Class
End Namespace
