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

        Public Function RewriteSegment(CurrentSegment As MediaSegment) As MediaSegment Implements ISegmentRewriter.RewriteSegment
            Dim result = New MediaSegment(CurrentSegment)

            Dim sequenceNumber = result.SequenceNumber
            If SegmentMap.ContainsKey(sequenceNumber) Then
                Dim fileUri = SegmentMap.Item(sequenceNumber)
                result.Uri = fileUri
                result.Bytes = Nothing
            End If

            Return result
        End Function
    End Class
End Namespace