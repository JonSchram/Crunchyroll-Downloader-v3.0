Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.segment

Namespace hls.parsing
    Public Class HlsSegmentsBuilder
        ' Applies to all segments until the next tag
        Private CurrentInitialization As MediaInitialization
        ' All keys apply until the next key with the same key format.
        Private CurrentEncryptionKeys As Dictionary(Of String, EncryptionKey)

        ' Apply to the next segment only
        Private NextSegmentDuration As Double?
        Private NextSegmentTitle As String
        Private NextByteRange As ByteRange
        ' Whether the next media segment group should have a discontinuity
        Private NextSegmentDiscontinuity As Boolean = False
        Private NextDateTime As SegmentDateTime

        ' Dynamically calculated values
        Private NextDiscontinuityNumber As Integer
        Private NextSequenceNumber As Integer

        Private ReadOnly BuiltSegments As New List(Of MediaSegment)

        Public Sub New(startSequenceNumber As Integer, StartDiscontinuityNumber As Integer)
            NextSequenceNumber = startSequenceNumber
            NextDiscontinuityNumber = StartDiscontinuityNumber
            CurrentEncryptionKeys = New Dictionary(Of String, EncryptionKey)()
        End Sub

        Public Function Build() As List(Of MediaSegment)
            Return BuiltSegments
        End Function

        Public Sub SetCurrentSegmentUri(uri As String)
            If Not NextSegmentDuration.HasValue Then
                Throw New HlsFormatException($"Media segment must have segment info. Failed adding URI {uri}.")
            End If

            Dim currentKeyList = CurrentEncryptionKeys.Values.ToList()

            Dim currentMediaSegment = New MediaSegment(NextSegmentDuration.Value, NextSegmentTitle, NextByteRange,
                                                    uri, currentKeyList, CurrentInitialization, NextDateTime,
                                                    NextSegmentDiscontinuity, NextSequenceNumber, NextDiscontinuityNumber)

            BuiltSegments.Add(currentMediaSegment)

            ' Setting the URI finishes the media segment
            ResetForNextSegment()
        End Sub

        Private Sub ResetForNextSegment()
            NextSequenceNumber += 1

            NextSegmentTitle = Nothing
            NextSegmentDuration = Nothing
            NextByteRange = Nothing
            NextSegmentDiscontinuity = False
            NextDateTime = Nothing
        End Sub

        Public Sub AddSegmentInfo(duration As Double, title As String)
            NextSegmentDuration = duration
            NextSegmentTitle = title
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

        Public Sub AddEncryptionKey(key As EncryptionKey)
            ' Deliberately overwrite an item if it already exists.
            CurrentEncryptionKeys.Add(key.Format, key)
        End Sub

        Public Sub AddInitialization(init As MediaInitialization)
            CurrentInitialization = init
        End Sub

        Public Sub AddDateTime(dateTime As SegmentDateTime)
            NextDateTime = dateTime
        End Sub
    End Class
End Namespace