Imports System.IO
Imports System.Net.Http
Imports Crunchyroll_Downloader.api.client.stream
Imports Crunchyroll_Downloader.api.common

Namespace processing
    Public Class HybridDownloader
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
                    'ElseIf TypeOf stream Is SegmentedMedia Then
                    '    HandleSegmentStream(CType(stream, SegmentedMedia))
                End If
            Next

        End Sub

        'Private Async Sub HandleSegmentStream(stream As SegmentedMedia)
        '    Dim folderName As String = $"stream-{StreamNumber}"
        '    StreamNumber += 1
        '    Dim streamFolder = StreamTempFolder.CreateSubdirectory(folderName)

        '    For i As Integer = 0 To stream.GetSegmentCount()
        '        Dim segment = stream.GetSegment(i)
        '        Dim responseTask = client.GetStreamAsync(segment.Uri)

        '        Dim parsedUri = New Uri(folderName)
        '        Dim fileName = parsedUri.LocalPath
        '        Dim filePath = IO.Path.Combine(streamFolder.FullName, fileName)
        '        Dim fileStream = New FileStream(filePath, FileMode.CreateNew)

        '        Dim response = Await responseTask
        '        Await response.CopyToAsync(fileStream)
        '        fileStream.Close()
        '    Next
        'End Sub

        Private Async Sub HandleFile(file As FileMedia)
            ' TODO: Handle file system and HTTP errors
            Dim parsedUri = New Uri(file.Uri)
            Dim fileName = parsedUri.LocalPath
            Dim filePath = IO.Path.Combine(StreamTempFolder.FullName, fileName)
            Dim fileStream = New FileStream(filePath, FileMode.CreateNew)
            Dim response = Await client.GetStreamAsync(file.Uri)

            Await response.CopyToAsync(fileStream)
        End Sub

        Private Function MakeTempDirectory() As DirectoryInfo
            Dim tempFolder = Preferences.TempFolder
            Return GetNewTempDirectory(tempFolder)
        End Function
    End Class
End Namespace