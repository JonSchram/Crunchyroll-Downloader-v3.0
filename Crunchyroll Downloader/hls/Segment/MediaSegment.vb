Imports Crunchyroll_Downloader.hls.tags
Imports Crunchyroll_Downloader.hls.tags.segment

Namespace hls.segment
    Public Class MediaSegment

        Public Property Duration As Double
        Public Property Title As String
        Public Property Bytes As ByteRange

        Public Property Uri As String

        Public Property SegmentDateTime As DateTimeTag


        ' This isn't explicitly listed in a playlist file, but is calculated as they are added to a parsed object
        Public Property SegmentNumber As Integer
    End Class

End Namespace
