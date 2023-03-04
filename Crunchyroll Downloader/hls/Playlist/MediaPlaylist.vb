Imports Crunchyroll_Downloader.hls.segment
Imports Crunchyroll_Downloader.hls.tags.encryption

Namespace hls.playlist
    Public Class MediaPlaylist
        Inherits AbstractPlaylist

        ' Required
        Public Property TargetDuration As Integer

        Public Property MediaSequenceNumber As Integer = 0

        Public Property DiscontinuitySequence As Integer = 0

        Public Property PlaylistEnds As Boolean

        Public Property Type As PlaylistType

        Public Property IFramesOnly As Boolean = False



        ' Initialization applies to all media segments following it, until the next one
        Public Property Initialization As SegmentRangeData(Of MediaInitialization)

        Public Property Segments As MediaSegmentList

        ' Key applies to all segments following that key
        Private Property keys As SegmentRangeData(Of Key)

        ' Value corresponds to a sequence number in the media segments.
        ' A discontinuity happens at this segment (i.e. before it, appeared between it and the previous segment in the playlist)
        Private Property Discontinuities As List(Of Integer)


        Public Function GetSegmentCount() As Integer
            Return Segments.GetCount()
        End Function

        Public Function GetSegmentAtIndex(Index As Integer) As MediaSegment
            Return Segments.getSegment(Index)
        End Function

        Public Function GetKeyForIndex(Index As Integer) As Key
            ' TODO: This is likely really inefficient.
            ' Each time it does this, it's a binary search. Each one is O(log(n)) but when a playlist has thousands of lines, it could add up.
            ' Consider making a method that gets all segments with same key and initialization info
            Return keys.GetDataForSegmentIndex(Index)
        End Function

        Public Function GetInitializationForIndex(Index As Integer) As MediaInitialization
            Return Initialization.GetDataForSegmentIndex(Index)
        End Function
    End Class

    Public Enum PlaylistType
        EVENT_PLAYLIST
        VOD
    End Enum

End Namespace