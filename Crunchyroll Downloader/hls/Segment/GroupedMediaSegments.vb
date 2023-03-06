Imports System.Runtime.CompilerServices
Imports Crunchyroll_Downloader.hls.playlist
Imports Crunchyroll_Downloader.hls.tags.encryption
Imports Crunchyroll_Downloader.hls.tags.segment

Namespace hls.segment
    ' Field comments adapted from RFC #8216 at https://www.rfc-editor.org/rfc/rfc8216.html

    ''' <summary>
    ''' A group of media segments with the same settings (encryption key, having no discontinuities between them, etc.)
    ''' </summary>
    Public Class GroupedMediaSegments
        ' TODO: Enforce setting the key / initialization settings only once.
        ' If this is getting called after a segment has been added, it should be a new segment.
        ' Probably can't force everything to be set in the constructor because the next level up will need to buffer configuration

        Private Property Segments As List(Of MediaSegment) = New List(Of MediaSegment)

        ' Whether this sequence of media segments begins with a discontinuity
        Private HasDiscontinuity As Boolean = False

        ' Because there may only be a single discontinuity for a media segment list, this applies to all segments in this list.
        Private DiscontinuitySequenceNumber As Integer

        Private Property EncryptionKey As KeyTag

        Private Property Initialization As MediaInitializationTag


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
        ' On second thought, #2 sounds better. There's a lot of properties that apply to ranges and I'd rather not do a bunch of searches

        Public Sub SetHasDiscontinuity()
            HasDiscontinuity = True
        End Sub

        Public Function GetHasDiscontinuity() As Boolean
            Return HasDiscontinuity
        End Function

        Public Sub SetEncryptionKey(encryptionKey As KeyTag)
            Me.EncryptionKey = encryptionKey
        End Sub

        Public Function GetEncryptionKey() As KeyTag
            Return EncryptionKey
        End Function

        Public Sub SetInitialization(init As MediaInitializationTag)
            Initialization = init
        End Sub

        Public Function GetInitialization() As MediaInitializationTag
            Return Initialization
        End Function

        Public Sub AddSegment(Segment As MediaSegment)
            Segments.Add(Segment)
        End Sub
        Friend Function GetSegment(index As Integer) As MediaSegment
            Return Segments.Item(index)
        End Function

        Public Function GetSegmentCount() As Integer
            Return Segments.Count()
        End Function
    End Class


End Namespace
