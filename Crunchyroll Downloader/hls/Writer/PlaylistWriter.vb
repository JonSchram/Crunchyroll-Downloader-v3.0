Imports System.IO
Imports Crunchyroll_Downloader.hls.playlist
Imports Crunchyroll_Downloader.hls.segment
Imports Crunchyroll_Downloader.hls.tags
Imports Crunchyroll_Downloader.hls.tags.encryption
Imports Crunchyroll_Downloader.hls.tags.segment

Namespace hls.writer
    ''' <summary>
    ''' Utility class that allows writing a class to a stream. This can be used to write the playlist back to a file or
    ''' back to a string in memory to display in a console.
    ''' </summary>
    Public Class PlaylistWriter

        Public Sub WriteToStream(playlist As MediaPlaylist, output As Stream)
            Dim printStream = New StreamWriter(output)
            WriteCommonHeader(playlist, printStream)
            WriteMediaPlaylistHeader(playlist, printStream)
            WriteSegments(playlist, printStream)
            WriteDateRanges(playlist.DateRangeList, printStream)
            WriteFooter(playlist, printStream)
            printStream.Flush()
        End Sub

        Private Sub WriteCommonHeader(playlist As AbstractPlaylist, printStream As StreamWriter)
            printStream.WriteLine("#EXTM3U")
            printStream.WriteLine($"#EXT-X-VERSION:{playlist.Version}")

            Dim start = playlist.StartPlayTime
            If start IsNot Nothing Then
                printStream.WriteLine($"#EXT-X-START:TIME-OFFSET={start.TimeOffset},PRECISE={HlsHelpers.ToYesNoValue(start.Precise)}")
            End If

            If playlist.IndependentSegments Then
                printStream.WriteLine("#EXT-X-INDEPENDENT-SEGMENTS")
            End If
        End Sub

        Private Sub WriteMediaPlaylistHeader(playlist As MediaPlaylist, printStream As StreamWriter)
            If playlist.Type <> PlaylistType.NONE_SPECIFIED Then
                printStream.WriteLine($"#EXT-X-PLAYLIST-TYPE:{HlsHelpers.ConvertToString(playlist.Type)}")
            End If

            If playlist.IFramesOnly Then
                printStream.WriteLine("#EXT-X-I-FRAMES-ONLY")
            End If

            printStream.WriteLine($"#EXT-X-TARGETDURATION:{playlist.TargetDuration}")
            printStream.WriteLine($"#EXT-X-MEDIA-SEQUENCE:{playlist.MediaSequenceNumber}")
            printStream.WriteLine($"#EXT-X-DISCONTINUITY-SEQUENCE:{playlist.DiscontinuitySequenceNumber}")
        End Sub

        Private Sub WriteSegments(playlist As MediaPlaylist, output As StreamWriter)
            Dim previousKey As KeyTag = Nothing
            Dim previousInitialization As MediaInitializationTag = Nothing
            For Each segment As MediaSegment In playlist.Segments
                Dim keyHasChanged = False
                Dim initChanged = False

                If PersistentPropertyChanged(previousKey, segment.EncryptionKey) Then
                    previousKey = segment.EncryptionKey
                    keyHasChanged = True
                End If
                If PersistentPropertyChanged(previousInitialization, segment.Initialization) Then
                    previousInitialization = segment.Initialization
                    initChanged = True
                End If

                WriteSegment(segment, output, keyHasChanged, initChanged)
            Next
        End Sub

        ''' <summary>
        ''' Returns true if the current object is non-null and is unequal to the previous object.
        ''' A segment property can never become null after it has been set on one segment, so there
        ''' is never a case where the current object is null and the previous one is non-null.
        ''' </summary>
        ''' <param name="previous"></param>
        ''' <param name="current"></param>
        ''' <returns></returns>
        Private Function PersistentPropertyChanged(previous As Object, current As Object) As Boolean
            Return current IsNot Nothing AndAlso Not current.Equals(previous)
        End Function

        Private Sub WriteSegment(segment As MediaSegment, output As StreamWriter, writeKey As Boolean, writeInitialization As Boolean)
            ' Write key / initialization first so the EXTINF / BYTERANGE tags are consistently close together.
            If writeKey Then
                Dim key = segment.EncryptionKey
                output.Write($"#EXT-X-KEY:METHOD={HlsHelpers.ConvertToString(key.Method)}")
                If key.Uri IsNot Nothing Then
                    output.Write($",URI=""{key.Uri}""")
                End If
                If key.InitializationVector IsNot Nothing Then
                    output.Write($",IV={key.InitializationVector}")
                End If
                If key.KeyFormat <> "identity" Then
                    output.Write($",KEYFORMAT={key.KeyFormat}")
                End If
                If key.KeyFormatVersions <> "1" Then
                    output.Write($",KEYFORMATVERSIONS={key.KeyFormatVersions}")
                End If
                output.WriteLine()
            End If

            If writeInitialization Then
                ' If writeInitialization is true, the initialization must be non-null so no need to check.
                Dim init = segment.Initialization
                output.Write($"#EXT-X-MAP:URI={init.Uri}")
                If init.Bytes IsNot Nothing Then
                    output.Write(",")
                    WriteByteRange(init.Bytes, output)
                End If
                output.WriteLine()
            End If

            output.Write($"#EXTINF:{segment.Duration}")
            If segment.Title IsNot Nothing Then
                output.Write($",{segment.Title}")
            End If
            output.WriteLine()

            If segment.Bytes IsNot Nothing Then
                output.Write("#EXT-X-BYTERANGE:")
                WriteByteRange(segment.Bytes, output)
                output.WriteLine()
            End If

            If segment.HasDiscontinuity Then
                output.WriteLine("#EXT-X-DISCONTINUITY")
            End If

            If segment.SegmentDateTime IsNot Nothing Then
                output.WriteLine($"#EXT-X-PROGRAM-DATE-TIME:{segment.SegmentDateTime}")
            End If

            ' Must write URI last because it indicates the next segment begins.
            output.WriteLine(segment.Uri)
        End Sub

        Private Sub WriteDateRanges(dateRanges As List(Of DateRangeTag), output As StreamWriter)
            For Each dateRange As DateRangeTag In dateRanges
                output.Write($"#EXT-X-DATERANGE:ID=""{dateRange.Id}""")

                If dateRange.ClassAttribute IsNot Nothing Then
                    output.Write($",CLASS=""{dateRange.ClassAttribute}""")
                End If

                output.Write($",START-DATE=""{dateRange.StartDate}""")

                If dateRange.EndDate IsNot Nothing Then
                    output.Write($",END-DATE=""{dateRange.EndDate}""")
                End If

                If dateRange.Duration IsNot Nothing Then
                    output.Write($",DURATION={dateRange.Duration}")
                End If

                If dateRange.PlannedDuration IsNot Nothing Then
                    output.Write($",PLANNED-DURATION={dateRange.PlannedDuration}")
                End If

                If dateRange.EndOnNext IsNot Nothing Then
                    output.Write($",END-ON-NEXT={dateRange.EndOnNext}")
                End If

                If dateRange.Scte35Cmd IsNot Nothing Then
                    output.Write($",SCTE35-CMD={dateRange.Scte35Cmd}")
                End If

                If dateRange.Scte35In IsNot Nothing Then
                    output.Write($",SCTE35-IN={dateRange.Scte35In}")
                End If

                If dateRange.Scte35Out IsNot Nothing Then
                    output.Write($",SCTE35-OUT={dateRange.Scte35Out}")
                End If

                Dim customAttributes = dateRange.UnparsedAttributes
                For Each valuePair As KeyValuePair(Of String, String) In customAttributes
                    ' The custom attribute can be a quoted string, a hex value, or a decimal.
                    ' The parser doesn't remember whether it was quoted in the input so assume it is quoted.
                    output.Write($",{valuePair.Key}=""{valuePair.Value}""")
                Next

                output.WriteLine()
            Next
        End Sub

        ''' <summary>
        ''' Writes a ByteRange formatted for an HLS playlist.
        ''' Does not include the BYTERANGE tag so that it can be written to an attribute list as well.
        ''' </summary>
        ''' <param name="bytes"></param>
        ''' <param name="output"></param>
        Private Sub WriteByteRange(bytes As ByteRange, output As StreamWriter)
            output.Write($"{bytes.Length}")
            If bytes.Offset.HasValue Then
                output.Write($"@{bytes.Offset}")
            End If
        End Sub

        Private Sub WriteFooter(playlist As MediaPlaylist, output As StreamWriter)
            If playlist.PlaylistEnds Then
                output.WriteLine("#EXT-X-ENDLIST")
            End If
        End Sub
    End Class
End Namespace