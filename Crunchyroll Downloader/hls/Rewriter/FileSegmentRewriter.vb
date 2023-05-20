Imports Crunchyroll_Downloader.hls.segment

Namespace hls.rewriter
    ''' <summary>
    ''' Segment rewriter that replaces URLs in an HLS playlist with URls pointing to files on the local hard drive.
    ''' </summary>
    Public Class FileSegmentRewriter
        Implements ISegmentRewriter

        Private SegmentMap As Dictionary(Of Integer, String)

        Public Sub New(SegmentMap As Dictionary(Of Integer, String))
            Me.SegmentMap = SegmentMap
        End Sub

        Public Function RewriteSegment(Segment As MediaSegment) As MediaSegment Implements ISegmentRewriter.RewriteSegment

            Dim sequenceNumber = Segment.SequenceNumber
            Dim newUri = Segment.Uri
            Dim newBytes = Segment.Bytes
            If SegmentMap.ContainsKey(sequenceNumber) Then
                Dim fileUri = SegmentMap.Item(sequenceNumber)
                newUri = fileUri
                newBytes = Nothing
            End If

            Return New MediaSegment(Segment.Duration, Segment.Title, newBytes, newUri, Segment.Keys,
                                    Segment.Initialization, Segment.SegmentDateTime, Segment.HasDiscontinuity,
                                    Segment.SequenceNumber, Segment.DiscontinuitySequenceNumber)
        End Function
    End Class
End Namespace