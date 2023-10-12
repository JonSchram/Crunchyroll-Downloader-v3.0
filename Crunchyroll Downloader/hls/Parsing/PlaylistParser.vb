Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports Crunchyroll_Downloader.hls.parsing.tags.encryption
Imports Crunchyroll_Downloader.hls.parsing.tags.master
Imports Crunchyroll_Downloader.hls.parsing.tags.master.stream
Imports Crunchyroll_Downloader.hls.parsing.tags.media
Imports Crunchyroll_Downloader.hls.parsing.tags.universal
Imports Crunchyroll_Downloader.hls.playlist

Namespace hls.parsing
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

        Private ReadOnly Parsers As New Dictionary(Of String, ITagParser)

        Public Sub New()
            RegisterUniversalParsers()
            RegisterMediaPlaylistParsers()
            RegisterMasterParsers()
        End Sub


        Private Sub RegisterUniversalParsers()
            RegisterParser(New VersionTagParser())
            RegisterParser(New IndependentSegmentsTagParser())
            RegisterParser(New StartTagParser())
        End Sub

        Private Sub RegisterMediaPlaylistParsers()
            RegisterParser(New InfTagParser())
            RegisterParser(New ByteRangeTagParser())
            RegisterParser(New DiscontinuityTagParser())
            RegisterParser(New KeyTagParser())
            RegisterParser(New MediaInitializationTagParser())
            RegisterParser(New DateTimeTagParser())
            RegisterParser(New DateRangeTagParser())

            RegisterParser(New TargetDurationTagParser())
            RegisterParser(New MediaSequenceNumberTagParser())
            RegisterParser(New DiscontinuitySequenceNumberTagParser())
            RegisterParser(New EndlistTagParser())
            RegisterParser(New PlaylistTypeTagParser())
            RegisterParser(New IframesOnlyTagParser())
        End Sub

        Private Sub RegisterMasterParsers()
            RegisterParser(New MediaTagParser())
            RegisterParser(New StreamInfTagParser())
            RegisterParser(New IframeStreamInfTagParser())
            RegisterParser(New SessionDataTagParser())
            RegisterParser(New SessionKeyTagParser())
        End Sub

        Private Sub RegisterParser(parser As ITagParser)
            Parsers.Add(parser.GetTagName(), parser)
        End Sub

        Public Function ParseMediaPlaylist(playlist As Stream) As MediaPlaylist
            Dim reader As New StreamReader(playlist)

            Return ParseMediaPlaylist(reader)
        End Function

        Public Function ParseMediaPlaylist(playlist As String) As MediaPlaylist
            Dim playlistReader As New StringReader(playlist)
            Return ParseMediaPlaylist(playlistReader)
        End Function

        Public Function ParseMediaPlaylist(playlistReader As TextReader) As MediaPlaylist
            If Not ValidatePlaylistFile(playlistReader) Then
                Throw New HlsFormatException("Input not an Extended M3U playlist!")
            End If
            Dim ResultBuilder As New MediaPlaylistBuilder()

            While playlistReader.Peek() <> -1
                Dim line As String = playlistReader.ReadLine()

                If line.StartsWith("#EXT") Then
                    Dim tag As ParsedTag = TagParserHelper.ParseTagString(line)

                    Dim tagName = tag.getTagName()
                    ' Tags can only begin with #EXT
                    If tagName.StartsWith("EXT") Then
                        Dim parser As ITagParser = Nothing
                        If Parsers.TryGetValue(tagName, parser) Then
                            parser.ParseInto(playlistReader, tag, ResultBuilder)
                        Else
                            Console.WriteLine($"Cannot parse tag: {tagName}")
                        End If
                    Else
                        ' A line starting with "#" but without the EXT following it can only be a comment.
                    End If
                ElseIf line.StartsWith("#") Then
                    ' This is a non-standard tag or a comment. Safe to ignore.

                ElseIf line.Length = 0 Then
                    ' Skip blank lines

                Else
                    ' Anything other than a # is a URI
                    ResultBuilder.AddSegmentUri(line)
                End If
            End While

            Return ResultBuilder.Build()
        End Function

        Public Function ParseMasterPlaylist(playlist As String) As MasterPlaylist
            Dim playlistReader As New StringReader(playlist)
            Return ParseMasterPlaylist(playlistReader)
        End Function

        Public Function ParseMasterPlaylist(playlist As Stream) As MasterPlaylist
            Dim reader As New StreamReader(playlist)
            Return ParseMasterPlaylist(reader)
        End Function

        Public Function ParseMasterPlaylist(playlistReader As TextReader) As MasterPlaylist
            If Not ValidatePlaylistFile(playlistReader) Then
                Throw New HlsFormatException("Input not an Extended M3U playlist!")
            End If

            Dim playlistBuilder = New MasterPlaylist.Builder()
            While playlistReader.Peek() <> -1
                Dim line As String = playlistReader.ReadLine()

                If line.StartsWith("#EXT") Then
                    Dim tag As ParsedTag = TagParserHelper.ParseTagString(line)
                    Dim tagName = tag.getTagName()
                    ' Tags can only begin with #EXT
                    If tagName.StartsWith("EXT") Then
                        Dim parser As ITagParser = Nothing
                        If Parsers.TryGetValue(tagName, parser) Then
                            parser.ParseInto(playlistReader, tag, playlistBuilder)
                        Else
                            ' The tag can't be parsed. There is a possibility that this was a comment
                            ' that happened to start with EXT, so don't throw an error.
                            Console.WriteLine($"Unparsed HLS tag: {tagName}")
                        End If
                    Else
                        ' A line starting with "#" but without the EXT following it can only be a comment.
                    End If

                ElseIf line.StartsWith("#") Then
                    ' This is a non-standard tag or a comment. Safe to ignore.

                ElseIf line.Length = 0 Then
                    ' Skip blank lines

                Else
                    ' Anything other than a # is a URI
                    ' As this should be handled by the StreamInfTagParser, this is an error
                    Throw New HlsFormatException("Error in playlist - unexpected URL.")
                End If
            End While

            Return playlistBuilder.Build()
        End Function

        Private Function ReadTagName(reader As TextReader) As String
            Dim sb = New StringBuilder()

            While reader.Peek() <> -1
                Dim currentCharacter = Convert.ToChar(reader.Read())

                If currentCharacter = vbCr Or currentCharacter = vbLf Then
                    Return sb.ToString()
                ElseIf currentCharacter <> ":"c Then
                    sb.Append(currentCharacter)
                Else
                    Return sb.ToString()
                End If
            End While

            Return sb.ToString()
        End Function

        Private Function ValidatePlaylistFile(reader As TextReader) As Boolean
            Dim firstLine As String = reader.ReadLine()
            Return "#EXTM3U".Equals(firstLine)
        End Function

    End Class
End Namespace
