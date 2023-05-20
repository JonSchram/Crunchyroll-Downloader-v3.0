Imports Crunchyroll_Downloader.hls.segment

Namespace hls.rewriter
    Public Interface ISegmentRewriter

        Function RewriteSegment(Segment As MediaSegment) As MediaSegment

    End Interface
End Namespace