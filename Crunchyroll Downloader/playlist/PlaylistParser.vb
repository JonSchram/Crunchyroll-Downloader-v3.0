Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports System.Text

Public Class PlaylistParser
    ' See https://www.rfc-editor.org/rfc/rfc8216#section-4.3.4.2

    Public Sub New()

    End Sub

    Public Function parseEpisodeStreams(playlist As String) As Playlist
        Dim episodePlaylist As Playlist = New Playlist()

        Dim Lines() = splitIntoLines(playlist)

        If Lines(0) <> "#EXTM3U" Then
            Throw New FormatException("Input not an Extended M3U playlist!")
        End If

        Dim currentStream As StreamVariant

        For lineNumber = 1 To Lines.Length - 1
            Dim Line = Lines(lineNumber)

            If Line.Contains("#EXT-X-INDEPENDENT-SEGMENTS") Then
                episodePlaylist.IsIndependentSegments = True

            ElseIf Line.Contains("#EXT-X-STREAM-INF") Then
                currentStream = parseStreamMetadata(Line)

            ElseIf Line.Contains("#EXT-X-SESSION-KEY") Then
                episodePlaylist.Key = parseSessionKey(Line)

            ElseIf Line.Contains("#EXT-X-I-FRAME-STREAM-INF") Then
                episodePlaylist.IframeStreams.Add(parseStreamMetadata(Line))

            ElseIf Line.StartsWith("https") Then
                ' Any URL is assumed to be a string
                If currentStream Is Nothing Then
                    currentStream = New StreamVariant()
                End If
                currentStream.Uri = Line
                episodePlaylist.StreamVariants.Add(currentStream)
                currentStream = Nothing
            End If
        Next



        Return episodePlaylist
    End Function

    Private Function splitIntoLines(playlist As String) As String()
        Return playlist.Split(New String() {vbCr, vbLf, vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
    End Function

    Private Function parseStreamMetadata(line As String) As StreamVariant
        Dim VariantMetadata = New StreamVariant()
        Dim StreamTag As String = "#EXT-X-STREAM-INF:"

        ' Should always exist. Throw if it doesn't?
        Dim tagStartIndex = line.IndexOf(StreamTag)
        Dim ParsedLine = New CommentParser().ParseCommentString(line)

        Dim resolution = ParsedLine.GetAttribute("RESOLUTION")
        If resolution IsNot Nothing Then
            Dim resolutions = Split(resolution, "x")
            VariantMetadata.HorizontalResolution = CInt(resolutions(0))
            VariantMetadata.VerticalResolution = CInt(resolutions(1))
        End If

        Dim frameRate = ParsedLine.GetAttribute("FRAME_RATE")
        If frameRate IsNot Nothing Then
            VariantMetadata.FrameRate = CDbl(frameRate)
        End If

        Dim bandwidth = ParsedLine.GetAttribute("BANDWIDTH")
        If bandwidth IsNot Nothing Then
            VariantMetadata.Bandwidth = CInt(bandwidth)
        End If

        Dim averageBandwidth = ParsedLine.GetAttribute("AVERAGE-BANDWIDTH")
        If averageBandwidth IsNot Nothing Then
            VariantMetadata.AverageBandwidth = CInt(averageBandwidth)
        End If

        Dim audio = ParsedLine.GetAttribute("AUDIO")
        If audio IsNot Nothing Then
            VariantMetadata.AudioId = audio
        End If

        Dim uri = ParsedLine.GetAttribute("URI")
        If uri IsNot Nothing Then
            VariantMetadata.Uri = uri
        End If

        Return VariantMetadata
    End Function

    Private Function parseSessionKey(line As String) As SessionKey
        Dim keyResult = New SessionKey()

        Dim parser = New CommentParser()
        Dim attributes = parser.ParseCommentString(line)

        Dim method = attributes.GetAttribute("METHOD")
        If method IsNot Nothing Then
            keyResult.Method = convertEncryptionToEnum(method)
        End If

        Dim uri = attributes.GetAttribute("URI")
        If uri IsNot Nothing Then
            keyResult.Uri = uri
        End If

        Return keyResult
    End Function

    Private Function convertEncryptionToEnum(Method As String) As EncryptionMethod
        Select Case Method
            Case "AES-128"
                Return EncryptionMethod.AES_128
            Case Else
                Return EncryptionMethod.OTHER
        End Select
    End Function

End Class

