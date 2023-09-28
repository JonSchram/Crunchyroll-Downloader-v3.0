Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.playlist
Imports Crunchyroll_Downloader.hls.playlist.AbstractPlaylist
Imports Crunchyroll_Downloader.hls.segment

Namespace hls.parsing
    Public Class HlsMediaPlaylistBuilder
        Inherits AbstractPlaylistBuilder

        Private TargetDuration As Integer

        ' The sequence number of the first media segment
        Private MediaSequenceNumber As Integer = 0

        Private DiscontinuitySequenceNumber As Integer = 0

        Private PlaylistEnds As Boolean

        Private Type As PlaylistType = PlaylistType.NONE_SPECIFIED

        Private IFramesOnly As Boolean = False

        ' Unused, seems to be for a server to give extra date range information to the player.
        Private ReadOnly DateRangeList As New List(Of DateRange)

        Private SegmentsBuilder As HlsSegmentsBuilder

        Private MediaStarted As Boolean = False

        ' Playlist tags
        Public Sub SetTargetDuration(duration As Integer)
            TargetDuration = duration
        End Sub

        Public Sub SetStartSequenceNumber(start As Integer)
            If MediaSequencesStarted() Then
                Throw New HlsFormatException("Media sequence cannot appear after the first media segment.")
            End If
            MediaSequenceNumber = start
        End Sub

        Public Sub SetDiscontinuitySequenceNumber(discontinuitySequenceNumber As Integer)
            If MediaSequencesStarted() Then
                Throw New HlsFormatException("Discontinuity sequence cannot appear after the first media segment.")
            End If
            Me.DiscontinuitySequenceNumber = discontinuitySequenceNumber
        End Sub

        Public Sub SetEndlist()
            PlaylistEnds = True
        End Sub

        Public Sub SetIFramesOnly()
            IFramesOnly = True
        End Sub

        Public Sub SetPlaylistType(type As PlaylistType)
            Me.Type = type
        End Sub

        ' Media sequence tags

        Private Sub StartMediaSequences()
            If Not MediaStarted Then
                SegmentsBuilder = New HlsSegmentsBuilder(MediaSequenceNumber, DiscontinuitySequenceNumber)
            End If
            MediaStarted = True
        End Sub

        Private Function MediaSequencesStarted() As Boolean
            Return MediaStarted
        End Function

        Public Sub AddSegmentInfo(duration As Double, title As String)
            StartMediaSequences()
            SegmentsBuilder.AddSegmentInfo(duration, title)
        End Sub

        Public Sub AddSegmentByteRange(range As ByteRange)
            StartMediaSequences()
            SegmentsBuilder.AddSegmentByteRange(range)
        End Sub

        Public Sub AddKey(key As EncryptionKey)
            StartMediaSequences()
            SegmentsBuilder.AddEncryptionKey(key)
        End Sub

        Public Sub AddInitialization(map As MediaInitialization)
            StartMediaSequences()
            SegmentsBuilder.AddInitialization(map)
        End Sub

        Public Sub AddDateTime(dateTime As SegmentDateTime)
            StartMediaSequences()
            SegmentsBuilder.AddDateTime(dateTime)
        End Sub

        Public Sub AddDateRange(dateRange As DateRange)
            StartMediaSequences()
            DateRangeList.Add(dateRange)
        End Sub

        Public Sub AddDiscontinuity()
            StartMediaSequences()
            SegmentsBuilder.AddDiscontinuity()
        End Sub

        Public Sub AddSegmentUri(uri As String)
            StartMediaSequences()
            SegmentsBuilder.SetCurrentSegmentUri(uri)
        End Sub

        Public Function Build() As MediaPlaylist
            Return New MediaPlaylist(Version, IndependentSegments, StartPlayTime, TargetDuration,
                                      PlaylistEnds, Type, IFramesOnly, DateRangeList, SegmentsBuilder.Build())
        End Function
    End Class
End Namespace