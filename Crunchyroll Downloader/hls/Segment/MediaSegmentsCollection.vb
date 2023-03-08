Imports Crunchyroll_Downloader.hls.tags
Imports Crunchyroll_Downloader.hls.tags.encryption
Imports Crunchyroll_Downloader.hls.tags.segment

Namespace hls.segment
    ''' <summary>
    ''' Remembers the current settings during playlist parsing and is able to build a list of correct media segments.
    ''' as segments are added.
    ''' </summary>
    Public Class MediaSegmentsCollection
        Implements IEnumerable(Of MediaSegment)

        Private ReadOnly Segments As New List(Of MediaSegment)

        ' Applies to all segments until the next tag
        Private CurrentEncryptionKey As KeyTag
        Private CurrentInitialization As MediaInitializationTag

        ' Apply to the next segment only
        Private NextSegmentInfo As InfTag
        Private NextByteRange As ByteRange
        ' Whether the next media segment group should have a discontinuity
        Private NextSegmentDiscontinuity As Boolean = False
        Private NextDateTime As DateTimeTag

        ' Dynamically calculated values
        Private NextDiscontinuityNumber As Integer
        Private NextSequenceNumber As Integer


        Public Sub New(startSequenceNumber As Integer, StartDiscontinuityNumber As Integer)
            NextSequenceNumber = startSequenceNumber
            NextDiscontinuityNumber = StartDiscontinuityNumber
        End Sub

        Public Sub SetCurrentSegmentUri(uri As String)
            If NextSegmentInfo Is Nothing Then
                Throw New HlsFormatException($"Media segment must have segment info. Failed adding URI {uri}.")
            End If

            Dim currentMediaSegment = New MediaSegment With {
                .Duration = NextSegmentInfo.Duration,
                .Title = NextSegmentInfo.Title,
                .Bytes = NextByteRange,
                .Uri = uri,
                .EncryptionKey = CurrentEncryptionKey,
                .Initialization = CurrentInitialization,
                .SegmentDateTime = NextDateTime,
                .HasDiscontinuity = NextSegmentDiscontinuity,
                .SequenceNumber = NextSequenceNumber,
                .DiscontinuitySequenceNumber = NextDiscontinuityNumber
            }

            Segments.Add(currentMediaSegment)

            ' Setting the URI finishes the media segment
            ResetForNextSegment()
        End Sub

        Private Sub ResetForNextSegment()
            NextSequenceNumber += 1

            NextSegmentInfo = Nothing
            NextByteRange = Nothing
            NextSegmentDiscontinuity = False
            NextDateTime = Nothing
        End Sub

        Public Sub AddSegmentInfo(info As InfTag)
            NextSegmentInfo = info
        End Sub

        Public Sub AddSegmentByteRange(bytes As ByteRange)
            NextByteRange = bytes
        End Sub

        Public Sub AddDiscontinuity()
            If Not NextSegmentDiscontinuity Then
                ' Prevent incrementing the number just in case the tag appears twice in a row (for some reason)
                NextSegmentDiscontinuity = True
                NextDiscontinuityNumber += 1
            End If
        End Sub

        Public Sub AddEncryptionKey(key As KeyTag)
            CurrentEncryptionKey = key
        End Sub

        Public Sub AddInitialization(init As MediaInitializationTag)
            CurrentInitialization = init
        End Sub

        Public Sub AddDateTime(dateTime As DateTimeTag)
            NextDateTime = dateTime
        End Sub

        Public Function GetEnumerator() As IEnumerator(Of MediaSegment) Implements IEnumerable(Of MediaSegment).GetEnumerator
            Return Segments.GetEnumerator()
        End Function


        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return Segments.GetEnumerator
        End Function

        Public Overrides Function ToString() As String
            Return $"{{
Segments: {FormatFieldList(Segments)}
}}"
        End Function
        Private Function FormatFieldList(StreamList As IEnumerable(Of Object)) As String
            Dim output As String = "["

            For Each streamItem In StreamList
                output += streamItem.ToString() + "," + vbCrLf
            Next

            output += "]"
            Return output
        End Function
    End Class
End Namespace
