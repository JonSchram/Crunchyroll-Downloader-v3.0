Imports System.Runtime.CompilerServices
Imports Crunchyroll_Downloader.hls.segment
Imports Crunchyroll_Downloader.hls.tags
Imports Crunchyroll_Downloader.hls.tags.encryption
Imports Crunchyroll_Downloader.hls.tags.segment

Namespace hls.playlist
    Public Class MediaPlaylist
        Inherits AbstractPlaylist

        ' TODO: Maybe make a builder for a media playlist. Would make it easier to enforce that required tags are set.
        ' But this already assumes that the input is well-formed so it isn't a big issue

        ' Required
        Public Property TargetDuration As Integer

        ' The sequence number of the first media segment
        Public Property MediaSequenceNumber As Integer = 0

        Public Property DiscontinuitySequenceNumber As Integer = 0

        Public Property PlaylistEnds As Boolean

        Public Property Type As PlaylistType

        Public Property IFramesOnly As Boolean = False

        Public Property DateRangeList As List(Of DateRangeTag)

        ' Initialization applies to all media segments following it, until the next one
        Public Property Initialization As SegmentRangeData(Of MediaInitializationTag)

        Public Property Segments As GroupedMediaSegments

        ' Key applies to all segments following that key
        Private Property Keys As SegmentRangeData(Of KeyTag)

        ' Value corresponds to a sequence number in the media segments.
        ' A discontinuity happens at this segment (i.e. before it, appeared between it and the previous segment in the playlist)
        Private Property Discontinuities As List(Of Integer)

        Private CurrentSegmentInfo As InfTag
        Private CurrentByteRange As ByteRange
        Private nextSegmentNumber As Integer = 0

        Public Sub New()

        End Sub

        Public Sub setTargetDuration(target As TargetDurationTag)
            TargetDuration = target.Duration
        End Sub

        Public Sub SetStartSequenceNumber(start As Integer)
            If MediaSequencesStarted() Then
                Throw New HlsFormatException("Media sequence cannot appear after the first media segment.")
            End If
            MediaSequenceNumber = start
            nextSegmentNumber = start
        End Sub

        Public Sub SetDiscontinuitySequenceNumber(number As Integer)
            If MediaSequencesStarted() Then
                Throw New HlsFormatException("Discontinuity sequence cannot appear after the first media segment.")
            End If
            DiscontinuitySequenceNumber = number
        End Sub

        Private Function MediaSequencesStarted() As Boolean
            Return Segments.GetSegmentCount > 0
        End Function

        Public Sub SetSegmentInfo(info As InfTag)
            CurrentSegmentInfo = info
        End Sub

        Public Sub SetSegmentByterange(range As ByteRange)
            CurrentByteRange = range
        End Sub

        Public Sub SetCurrentSegmentUrl(uri As String)
            Dim currentMediaSegment = New MediaSegment With {
                .Duration = CurrentSegmentInfo.Duration,
                .Title = CurrentSegmentInfo.Title,
                .Bytes = CurrentByteRange,
                .Uri = uri,
                .SegmentNumber = nextSegmentNumber
            }

            Segments.AddSegment(currentMediaSegment)

            nextSegmentNumber += 1
            CurrentSegmentInfo = Nothing
            CurrentByteRange = Nothing
        End Sub

        Public Function GetSegmentCount() As Integer
            Return Segments.GetSegmentCount()
        End Function

        Public Function GetSegmentAtIndex(Index As Integer) As MediaSegment
            Return Segments.GetSegment(Index)
        End Function

        Public Function GetKeyForIndex(Index As Integer) As KeyTag
            ' TODO: This is likely really inefficient.
            ' Each time it does this, it's a binary search. Each one is O(log(n)) but when a playlist has thousands of lines, it could add up.
            ' Consider making a method that gets all segments with same key and initialization info
            Return Keys.GetDataForSegmentIndex(Index)
        End Function

        Public Function GetInitializationForIndex(Index As Integer) As MediaInitializationTag
            Return Initialization.GetDataForSegmentIndex(Index)
        End Function
    End Class

    Public Enum PlaylistType
        EVENT_PLAYLIST
        VOD
    End Enum

End Namespace