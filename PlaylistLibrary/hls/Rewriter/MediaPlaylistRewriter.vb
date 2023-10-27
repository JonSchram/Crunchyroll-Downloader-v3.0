Imports PlaylistLibrary.hls.common
Imports PlaylistLibrary.hls.playlist
Imports PlaylistLibrary.hls.segment

Namespace hls.rewriter
    Public Class MediaPlaylistRewriter

        Private SegmentRewriter As ISegmentRewriter

        Public Sub New(segmentRewriter As ISegmentRewriter)
            Me.SegmentRewriter = segmentRewriter
        End Sub

        Public Function RewritePlaylist(playlist As MediaPlaylist) As MediaPlaylist
            Dim TargetDuration = playlist.TargetDuration
            Dim PlaylistEnds = playlist.PlaylistEnds
            Dim Type = playlist.Type
            Dim IFramesOnly = playlist.IFramesOnly

            Dim DateRangeList = New List(Of DateRange)
            DateRangeList.AddRange(playlist.DateRangeList)

            Dim SegmentList = New List(Of MediaSegment)()
            If playlist.Segments IsNot Nothing Then
                For Each OtherSegment In playlist.Segments
                    SegmentList.Add(SegmentRewriter.RewriteSegment(OtherSegment))
                Next
            End If

            Return New MediaPlaylist(playlist.Version, playlist.IndependentSegments, playlist.StartPlayTime,
                                     playlist.TargetDuration, playlist.PlaylistEnds, playlist.Type, playlist.IFramesOnly,
                                     DateRangeList, SegmentList)
        End Function

    End Class
End Namespace