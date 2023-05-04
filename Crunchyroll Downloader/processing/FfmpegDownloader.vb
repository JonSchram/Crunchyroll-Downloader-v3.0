﻿Imports System.IO
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

        Private StreamTempFolder As DirectoryInfo

        Private StreamNumber As Integer = 0

        Public Sub New(streamSelector As IStreamSelector, preferences As DownloadPreferences)
            Me.StreamSelector = streamSelector
            Me.Preferences = preferences

            client = New HttpClient()
            client.DefaultRequestHeaders.UserAgent.ParseAdd(My.Resources.user_agent)

            StreamTempFolder = MakeTempDirectory()
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

            ' TODO: combine streams into a single file.

        End Sub

        Private Sub HandleSegmentStream(stream As SegmentedMedia)
            ' TODO: Get correct name of playlist from the stream.
            Dim fileName = "playlist.m3u8"
            Dim filePath = IO.Path.Combine(StreamTempFolder.FullName, fileName)
            Dim fileStream = New FileStream(filePath, FileMode.CreateNew)
            stream.WritePlaylistTo(fileStream)
            fileStream.Close()
        End Sub

        Private Async Sub HandleFile(file As FileMedia)
            ' TODO: Handle file system and HTTP errors
            Dim parsedUri = New Uri(file.Uri)
            Dim fileName = parsedUri.LocalPath
            Dim filePath = IO.Path.Combine(StreamTempFolder.FullName, fileName)
            Dim fileStream = New FileStream(filePath, FileMode.CreateNew)
            Dim response = Await client.GetStreamAsync(file.Uri)

            Await response.CopyToAsync(fileStream)
            fileStream.Close()
        End Sub

        Private Function MakeTempDirectory() As DirectoryInfo
            Dim tempFolder = Preferences.TempFolder
            Return GetNewTempDirectory(tempFolder)
        End Function

        Private Sub MakeTempFile()
            'File.Create(StreamTempFolder)



            'File.Create(Preferences.TempFolder)
        End Sub

    End Class
End Namespace