Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports Crunchyroll_Downloader.hls.playlist
Imports Crunchyroll_Downloader.hls.tags
Imports Crunchyroll_Downloader.hls.tags.encryption
Imports Crunchyroll_Downloader.hls.tags.stream

Namespace hls
    Public Class PlaylistParser
        ' See https://www.rfc-editor.org/rfc/rfc8216#section-4.3.3

        ' This SHOULD accept any playlist and return the correct kind.
        ' According to the RFC, there are master playlists and media playlists, with tags for each.
        ' A master playlist tag must not appear in a media playlist and vice versa
        ' This must fail to parse any playlist containing:
        ' - a master playlist tag
        ' AND either of the following:
        ' - a media playlist tag
        ' - a media segment tag
        '
        ' To accomplish this, it might have to hold all the data in intermediate objects until it reaches the end, then package it up.
        ' But the return type of the method couldn't be determined. Need some super type or interface
        ' Alternatively, could have a parse method for each type of playlist and ensure that it is only used for the correct kind.

        Public Sub New()

        End Sub

        Public Function ParsePlaylist(playlist As String) As Object

            Return Nothing
        End Function

        Public Function parseEpisodeStreams(playlist As String) As MasterPlaylist
            Dim episodePlaylist As MasterPlaylist = New MasterPlaylist()

            Dim tagParser = New TagParser()

            Dim Lines() = splitIntoLines(playlist)

            If Lines(0) <> "#EXTM3U" Then
                Throw New FormatException("Input not an Extended M3U playlist!")
            End If

            For lineNumber = 1 To Lines.Length - 1
                Dim Line = Lines(lineNumber)
                Dim parsedTag = tagParser.ParseTagString(Line)

                If parsedTag IsNot Nothing Then
                    ' Comments also start with "#" but the tag name won't match any of these cases
                    ' TODO: Lots of magic strings here. Might want to get these from the class for each parsed type.
                    Select Case parsedTag.getTagName()
                        Case "EXT-X-MEDIA"
                            episodePlaylist.PlaylistMedia.Add(New Media(parsedTag))
                        Case "EXT-X-STREAM-INF"
                            lineNumber += 1
                            If lineNumber < Lines.Length Then
                                Dim Stream = New VariantStream(parsedTag, Lines(lineNumber))
                                episodePlaylist.StreamVariants.Add(Stream)
                            Else
                                Throw New HlsFormatException("EXT-X-STREAM-INF requires a URI on the following line")
                            End If
                        Case "EXT-X-I-FRAME-STREAM-INF"
                            Dim IFrameStream As New IFrameStream(parsedTag)
                            episodePlaylist.IframeStreams.Add(IFrameStream)
                        Case "EXT-X-INDEPENDENT-SEGMENTS"
                            episodePlaylist.IsIndependentSegments = True
                        Case "EXT-X-SESSION-KEY"
                            episodePlaylist.Key = New SessionKey(parsedTag)
                    End Select
                End If
            Next

            Return episodePlaylist
        End Function

        Private Function splitIntoLines(playlist As String) As String()
            Return playlist.Split(New String() {vbCr, vbLf, vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
        End Function

    End Class
End Namespace
