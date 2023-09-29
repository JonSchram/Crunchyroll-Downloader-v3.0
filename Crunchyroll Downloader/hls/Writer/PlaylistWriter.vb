Imports System.Collections.Immutable
Imports System.IO
Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.parsing
Imports Crunchyroll_Downloader.hls.parsing.tags
Imports Crunchyroll_Downloader.hls.playlist
Imports Crunchyroll_Downloader.hls.segment

Namespace hls.writer
    ''' <summary>
    ''' Utility class that allows writing a class to a stream. This can be used to write the playlist back to a file or
    ''' back to a string in memory to display in a console.
    ''' </summary>
    Public Class PlaylistWriter

        Public Sub WriteToStream(playlist As MediaPlaylist, output As Stream)
            ' TODO: Make async
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

            Dim firstSegment = playlist.Segments.Item(0)

            printStream.WriteLine($"#EXT-X-TARGETDURATION:{playlist.TargetDuration}")
            printStream.WriteLine($"#EXT-X-MEDIA-SEQUENCE:{firstSegment.SequenceNumber}")
            printStream.WriteLine($"#EXT-X-DISCONTINUITY-SEQUENCE:{firstSegment.DiscontinuitySequenceNumber}")
        End Sub

        Private Sub WriteSegments(playlist As MediaPlaylist, output As StreamWriter)
            Dim previousKeys As ImmutableList(Of EncryptionKey) = Nothing
            Dim previousInitialization As MediaInitialization = Nothing
            For Each segment As MediaSegment In playlist.Segments
                Dim initChanged = False

                Dim changedKeys = FindChangedKeys(previousKeys, segment.Keys)
                If changedKeys.Count > 0 Then
                    previousKeys = segment.Keys
                End If
                If PersistentPropertyChanged(previousInitialization, segment.Initialization) Then
                    previousInitialization = segment.Initialization
                    initChanged = True
                End If

                WriteSegment(segment, output, changedKeys, initChanged)
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

        Private Function FindChangedKeys(previousKeys As IList(Of EncryptionKey), currentKeys As IList(Of EncryptionKey)) As ISet(Of EncryptionKey)
            Dim changedKeys As New HashSet(Of EncryptionKey)
            For Each key As EncryptionKey In currentKeys
                If Not previousKeys.Contains(key) Then
                    changedKeys.Add(key)
                End If
            Next
            Return changedKeys
        End Function

        Private Sub WriteSegment(segment As MediaSegment, output As StreamWriter, changedKeys As ISet(Of EncryptionKey),
                                 writeInitialization As Boolean)
            ' Write key / initialization first so the EXTINF / BYTERANGE tags are consistently close together.
            If changedKeys.Count > 0 Then
                For Each key As EncryptionKey In changedKeys
                    output.Write($"#EXT-X-KEY:METHOD={HlsHelpers.ConvertToString(key.Method)}")
                    If key.Uri IsNot Nothing Then
                        output.Write($",URI=""{key.Uri}""")
                    End If
                    If key.InitializationVector IsNot Nothing Then
                        output.Write($",IV={key.InitializationVector}")
                    End If
                    If key.Format <> "identity" Then
                        output.Write($",KEYFORMAT={key.Format}")
                    End If
                    If key.FormatVersions <> "1" Then
                        output.Write($",KEYFORMATVERSIONS={key.FormatVersions}")
                    End If
                    output.WriteLine()
                Next
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

        Private Sub WriteDateRanges(dateRanges As List(Of DateRange), output As StreamWriter)
            For Each dateRange In dateRanges
                output.Write($"#EXT-X-DATERANGE:ID=""{dateRange.Id}""")

                If dateRange.ClassAttributes IsNot Nothing Then
                    output.Write($",CLASS=""{dateRange.ClassAttributes}""")
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

                If dateRange.EndOnNext Then
                    output.Write(",END-ON-NEXT=YES")
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

                Dim customAttributes = dateRange.CustomAttributes
                For Each valuePair As KeyValuePair(Of String, PlaylistData) In customAttributes
                    Dim data As PlaylistData = valuePair.Value
                    ' The custom attribute can be a quoted string, a hex value, or a decimal.
                    If data.Quoted Then
                        output.Write($",{valuePair.Key}=""{data.Value}""")
                    Else
                        ' The value isn't parsed into a number yet, so just output with no quotes.
                        output.Write($",{valuePair.Key}={data.Value}")
                    End If
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