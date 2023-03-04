Imports Crunchyroll_Downloader.hls.playlist
Imports Crunchyroll_Downloader.hls.tags.encryption

Namespace hls.segment

    Public Class MediaSegmentList

        Private Property Segments As List(Of MediaSegment)



        ' Need an api that:
        ' - Lets you add media segments, keys, and discontinuities one at a time
        ' - Increments sequence numbers of new segments
        ' - Associates each segment with the correct key
        ' - Allows you to retrieve the key for each segment

        ' Potential designs:
        ' 1: Have a list of keys in the main playlist, maintain a list of key # -> segment #. The key applies to all segments after this
        ' 2: Have a list of MediaSegments in the main playlist, each MediaSegment contains a list of MediaSegment, the same key applies to all in that segment
        ' 
        ' Pros of 1: Simpler to write the api to consume segments
        ' Cons of 1: A little more work creating the key -> segment range mapping
        ' Pros of 2: Single list holds both key and segments
        ' Cons of 2: Hides segments another layer down, harder to iterate over segments

        ' #1 sounds better

        Public Function GetCount() As Integer
            Return Segments.Count()
        End Function

        Friend Function GetSegment(index As Integer) As MediaSegment
            Return Segments.Item(index)
        End Function
    End Class


End Namespace
