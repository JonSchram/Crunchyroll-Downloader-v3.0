Imports System.IO
Imports System.Net.Http
Imports Crunchyroll_Downloader.api.client.stream

Namespace processing
    ''' <summary>
    ''' A downloader that gets the playlist contents verbatim and relies on FFmpeg to download the segments.
    ''' </summary>
    Public Class FfmpegDownloader
        Private StreamSelector As IStreamSelector
        Private Preferences As DownloadPreferences

        Private ReadOnly DownloadedStreams As Dictionary(Of MediaType, Language)
        Private ReadOnly client As HttpClient

        Public Sub New(streamSelector As IStreamSelector, preferences As DownloadPreferences)
            Me.StreamSelector = streamSelector
            Me.Preferences = preferences

            client = New HttpClient()
            client.DefaultRequestHeaders.UserAgent.ParseAdd(My.Resources.user_agent)
        End Sub

        Public Sub Start()

            Dim streams = StreamSelector.GetStreams(Preferences.AudioLanguage, Preferences.SubtitleLanguages,
                                                    Preferences.DownloadTypes)

            For Each stream In streams
                If TypeOf stream Is FileMedia Then
                    HandleFile(CType(stream, FileMedia))
                ElseIf TypeOf stream Is SegmentedMedia Then
                    HandleSegmentStream(CType(stream, SegmentedMedia))
                End If
            Next

        End Sub

        Private Sub HandleSegmentStream(stream As SegmentedMedia)
            ' TODO: write playlist contents to file.
        End Sub

        Private Async Sub HandleFile(file As FileMedia)
            ' TODO: Handle file system and HTTP errors
            Dim fileStream = New FileStream(Preferences.TempFolder, FileMode.CreateNew)
            Dim response = Await client.GetStreamAsync(file.Uri)

            Await response.CopyToAsync(fileStream)
        End Sub

    End Class
End Namespace