Imports Crunchyroll_Downloader.hls.playlist
Imports Crunchyroll_Downloader.hls.tags
Imports Crunchyroll_Downloader.hls.tags.encryption
Imports Crunchyroll_Downloader.hls.tags.segment
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

        Public Function ParseMediaPlaylist(playlist As String) As MediaPlaylist

            Dim tagParser = New TagParser()
            Dim Lines() = SplitIntoLines(playlist)

            If Not ValidatePlaylistFile(Lines) Then
                Return Nothing
            End If
            Dim ResultBuilder As New MediaPlaylist.Builder()

            For LineNumber = 1 To Lines.Length - 1
                Dim Line = Lines(LineNumber)
                If Line.StartsWith("#EXT") Then
                    ' Tags can only begin with #EXT
                    Dim tag = tagParser.ParseTagString(Line)

                    Select Case tag.getTagName()
                        Case "EXT-X-VERSION"
                            ResultBuilder.SetVersion(New VersionTag(tag))
                        Case "EXT-X-INDEPENDENT-SEGMENTS"
                            ResultBuilder.SetIndependentSegments()
                        Case "EXT-X-START"
                            ResultBuilder.SetStart(New StartTag(tag))
                        Case "EXT-X-TARGETDURATION"
                            ResultBuilder.SetTargetDuration(New TargetDurationTag(tag))
                        Case "EXT-X-MEDIA-SEQUENCE"
                            ResultBuilder.SetStartSequenceNumber(New MediaSequenceNumberTag(tag))
                        Case "EXT-X-DISCONTINUITY-SEQUENCE"
                            ResultBuilder.SetDiscontinuitySequenceNumber(New DiscontinuitySequenceNumberTag(tag))
                        Case "EXT-X-ENDLIST"
                            ResultBuilder.SetEndlist()
                        Case "EXT-X-PLAYLIST-TYPE"
                            ResultBuilder.SetPlaylistType(New PlaylistTypeTag(tag))
                        Case "EXT-X-I-FRAMES-ONLY"
                            ResultBuilder.SetIFramesOnly()
                        Case "EXTINF"
                            ResultBuilder.AddSegmentInfo(New InfTag(tag))
                        Case "EXT-X-BYTERANGE"
                            ResultBuilder.AddSegmentByteRange(New ByteRangeTag(tag))
                        Case "EXT-X-DISCONTINUITY"
                            ResultBuilder.AddDiscontinuity()
                        Case "EXT-X-KEY"
                            ResultBuilder.AddKey(New KeyTag(tag))
                        Case "EXT-X-MAP"
                            ResultBuilder.AddInitialization(New MediaInitializationTag(tag))
                        Case "EXT-X-PROGRAM-DATE-TIME"
                            ResultBuilder.AddDateTime(New DateTimeTag(tag))
                        Case "EXT-X-DATERANGE"
                            ResultBuilder.AddDateRange(New DateRangeTag(tag))
                    End Select
                ElseIf Line(0) <> "#" Then
                    ' Anything other than a # is a URI
                    ResultBuilder.AddSegmentUri(Line)
                End If
            Next

            Return ResultBuilder.Build()
        End Function

        Public Function parseMasterPlaylist(playlist As String) As MasterPlaylist

            Dim tagParser = New TagParser()

            Dim Lines() = SplitIntoLines(playlist)

            If Not ValidatePlaylistFile(Lines) Then
                Return Nothing
            End If

            Dim playlistBuilder = New MasterPlaylist.Builder()
            For lineNumber = 1 To Lines.Length - 1
                Dim Line = Lines(lineNumber)
                Dim parsedTag = tagParser.ParseTagString(Line)

                If parsedTag IsNot Nothing Then
                    ' Comments also start with "#" but the tag name won't match any of these cases
                    ' TODO: Lots of magic strings here. Might want to get these from the class for each parsed type.
                    Select Case parsedTag.getTagName()
                        Case "EXT-X-VERSION"
                            playlistBuilder.SetVersion(New VersionTag(parsedTag))
                        Case "EXT-X-START"
                            playlistBuilder.SetStart(New StartTag(parsedTag))
                        Case "EXT-X-INDEPENDENT-SEGMENTS"
                            playlistBuilder.SetIndependentSegments()
                        Case "EXT-X-MEDIA"
                            playlistBuilder.AddPlaylistMedia(New MediaTag(parsedTag))
                        Case "EXT-X-STREAM-INF"
                            lineNumber += 1
                            If lineNumber < Lines.Length Then
                                Dim Stream = New VariantStream(parsedTag, Lines(lineNumber))
                                playlistBuilder.AddStreamVariant(Stream)
                            Else
                                Throw New HlsFormatException("EXT-X-STREAM-INF requires a URI on the following line")
                            End If
                        Case "EXT-X-I-FRAME-STREAM-INF"
                            Dim IFrameStream As New IFrameStream(parsedTag)
                            playlistBuilder.AddIframeStream(IFrameStream)
                        Case "EXT-X-SESSION-KEY"
                            playlistBuilder.SetKey(New SessionKeyTag(parsedTag))
                    End Select
                End If
            Next

            Return playlistBuilder.build()
        End Function

        Private Function ValidatePlaylistFile(Lines() As String) As Boolean
            If Lines(0) <> "#EXTM3U" Then
                Throw New HlsFormatException("Input not an Extended M3U playlist!")
            End If
            Return True
        End Function

        Private Function SplitIntoLines(playlist As String) As String()
            Return playlist.Split(New String() {vbCr, vbLf, vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
        End Function

    End Class
End Namespace
