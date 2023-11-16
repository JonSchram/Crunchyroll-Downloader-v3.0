Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.settings.general
Imports Crunchyroll_Downloader.utilities
Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports PlaylistLibrary.hls.playlist.comparer
Imports PlaylistLibrary.hls.playlist.stream
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

        Public Overrides Async Function DownloadSelection(playback As Selection) As Task(Of List(Of MediaFileEntry))
            ' TODO: Allow sending progress.
            ' Right now, this just awaits all the downloads but the download thread has no idea what is finished.

            Dim allTasks As New List(Of Task(Of MediaFileEntry))

            Dim media As IReadOnlyList(Of Media) = playback.Media
            Dim totalFiles As Integer = media.Count

            Dim completedRecords As New List(Of MediaFileEntry)
            For i As Integer = 0 To totalFiles - 1
                Dim item As Media = media.Item(i)

                OnMediaProgress(i, totalFiles, 0)
                completedRecords.Add(Await DownloadMediaItem(item, i, totalFiles))
                OnMediaProgress(i, totalFiles, 100)
                OnMediaComplete(i, totalFiles)
            Next
            For Each item In media
            Next

            Return completedRecords
        End Function

        Private Function DownloadMediaItem(item As Media, currentIndex As Integer, totalFiles As Integer) As Task(Of MediaFileEntry)
            If TypeOf item Is FileMedia Then
                Return DownloadSingleFile(CType(item, FileMedia), currentIndex, totalFiles)
            ElseIf TypeOf item Is MasterPlaylistMedia Then
                Return DownloadPlaylist(CType(item, MasterPlaylistMedia), currentIndex, totalFiles)
            End If

            ' Should never happen.
            Return Nothing
        End Function

        ''' <summary>
        ''' Downloads a program from a master playlist and returns the location it was saved to.
        ''' </summary>
        ''' <param name="item"></param>
        ''' <returns></returns>
        Private Async Function DownloadPlaylist(item As MasterPlaylistMedia, currentIndex As Integer, totalFiles As Integer) As Task(Of MediaFileEntry)
            OnMediaProgress(currentIndex, totalFiles, 0)

            Dim resolutionComparer As IComparer(Of VariantStreamMetadata)
            If Preferences.PreferredResolution = Resolution.BEST Then
                resolutionComparer = New HighestResolutionComparer()
            ElseIf Preferences.PreferredResolution = Resolution.WORST Then
                resolutionComparer = New LowestResolutionComparer()
            Else
                resolutionComparer = New ClosestResolutionComparer(
                    Preferences.PreferredResolution, Preferences.AcceptHigherResolution, Preferences.PreferHighBitrate)
            End If

            Dim programNumber = item.Playlist.GetClosestMatchProgramNumber(resolutionComparer)

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

            Dim handler = Sub(amount As Double)
                              OnMediaProgress(currentIndex, totalFiles, amount)
                          End Sub
            AddHandler FfmpegRunner.ReportProgress, handler

            Dim statusCode As Integer = Await FfmpegRunner.Run(ffmpegArguments)

            RemoveHandler FfmpegRunner.ReportProgress, handler


            If statusCode <> 0 Then
                Throw New Exception($"Ffmpeg exited with error: {statusCode}")
            End If

            OnMediaProgress(currentIndex, totalFiles, 100)
            OnMediaComplete(currentIndex, totalFiles)

            Return New MediaFileEntry(outputName, MediaType.Audio Or MediaType.Video, item.MediaLocale)
        End Function
    End Class
End Namespace
