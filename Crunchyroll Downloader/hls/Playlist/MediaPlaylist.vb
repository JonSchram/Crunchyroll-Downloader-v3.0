Imports Crunchyroll_Downloader.hls.rewriter
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
        Public ReadOnly Property TargetDuration As Integer

        ' The sequence number of the first media segment
        Public ReadOnly Property MediaSequenceNumber As Integer = 0

        Public ReadOnly Property DiscontinuitySequenceNumber As Integer = 0

        Public ReadOnly Property PlaylistEnds As Boolean

        Public ReadOnly Property Type As PlaylistType = PlaylistType.NONE_SPECIFIED

        Public ReadOnly Property IFramesOnly As Boolean = False

        ' Unused, seems to be for a server to give extra date range information to the player.
        Public ReadOnly Property DateRangeList As New List(Of DateRangeTag)

        Public ReadOnly Property Segments As MediaSegmentsCollection

        Public Sub New(other As MediaPlaylist)
            Me.New(other, Nothing)
        End Sub

        Public Sub New(other As MediaPlaylist, rewriter As ISegmentRewriter)
            MyBase.New(other)

            TargetDuration = other.TargetDuration
            MediaSequenceNumber = other.MediaSequenceNumber
            DiscontinuitySequenceNumber = other.DiscontinuitySequenceNumber
            PlaylistEnds = other.PlaylistEnds
            Type = other.Type
            IFramesOnly = other.IFramesOnly

            DateRangeList = New List(Of DateRangeTag)
            For Each DateRange In other.DateRangeList
                DateRangeList.Add(New DateRangeTag(DateRange))
            Next

            If other.Segments IsNot Nothing Then
                Dim SegmentList = New List(Of MediaSegment)()
                For Each OtherSegment In other.Segments
                    If rewriter IsNot Nothing Then
                        SegmentList.Add(rewriter.RewriteSegment(OtherSegment))
                    Else
                        SegmentList.Add(OtherSegment)
                    End If
                Next
                Segments = New MediaSegmentsCollection(SegmentList)
            End If
        End Sub

        Public Sub New(version As Integer, independentSegments As Boolean, startPlayTime As StartTag,
                       targetDuration As Integer, mediaSequenceNumber As Integer, discontinuitySequenceNumber As Integer,
                       playlistEnds As Boolean, type As PlaylistType, iFramesOnly As Boolean,
                       dateRangeList As List(Of DateRangeTag), segments As MediaSegmentsCollection)
            MyBase.New(version, independentSegments, startPlayTime)
            Me.TargetDuration = targetDuration
            Me.MediaSequenceNumber = mediaSequenceNumber
            Me.DiscontinuitySequenceNumber = discontinuitySequenceNumber
            Me.PlaylistEnds = playlistEnds
            Me.Type = type
            Me.IFramesOnly = iFramesOnly
            Me.DateRangeList = dateRangeList
            Me.Segments = segments
        End Sub



        ' TODO: Make API to get iterable segments


        Public Overrides Function ToString() As String
            Return $"{{
Version: {Version},
IndependentSegments: {IndependentSegments},
Start: {StartPlayTime},
TargetDuration: {TargetDuration},
MediaSequenceNumber: {MediaSequenceNumber},
DiscontinuitySequenceNumber: {DiscontinuitySequenceNumber},
PlaylistEnds: {PlaylistEnds},
Type: {Type},
IFramesOnly: {IFramesOnly},
DateRangeList: {DateRangeList},
Media segments: {Segments}
}}"
        End Function

        Public Class Builder
            Inherits AbstractBuilder

            Private TargetDuration As Integer

            ' The sequence number of the first media segment
            Private MediaSequenceNumber As Integer = 0

            Private DiscontinuitySequenceNumber As Integer = 0

            Private PlaylistEnds As Boolean

            Private Type As PlaylistType = PlaylistType.NONE_SPECIFIED

            Private IFramesOnly As Boolean = False

            ' Unused, seems to be for a server to give extra date range information to the player.
            Private ReadOnly DateRangeList As New List(Of DateRangeTag)

            Private SegmentsBuilder As MediaSegmentsCollection.Builder

            Private MediaStarted As Boolean = False

            ' Playlist tags
            Public Sub SetTargetDuration(target As TargetDurationTag)
                TargetDuration = target.Duration
            End Sub

            Public Sub SetStartSequenceNumber(start As MediaSequenceNumberTag)
                If MediaSequencesStarted() Then
                    Throw New HlsFormatException("Media sequence cannot appear after the first media segment.")
                End If
                MediaSequenceNumber = start.StartSequenceNumber
            End Sub

            Public Sub SetDiscontinuitySequenceNumber(start As DiscontinuitySequenceNumberTag)
                If MediaSequencesStarted() Then
                    Throw New HlsFormatException("Discontinuity sequence cannot appear after the first media segment.")
                End If
                DiscontinuitySequenceNumber = start.StartNumber
            End Sub

            Public Sub SetEndlist()
                PlaylistEnds = True
            End Sub

            Public Sub SetIFramesOnly()
                IFramesOnly = True
            End Sub

            Public Sub SetPlaylistType(typeTag As PlaylistTypeTag)
                Type = typeTag.Type
            End Sub

            ' Media sequence tags

            Private Sub StartMediaSequences()
                If Not MediaStarted Then
                    SegmentsBuilder = New MediaSegmentsCollection.Builder(MediaSequenceNumber, DiscontinuitySequenceNumber)
                End If
                MediaStarted = True
            End Sub

            Private Function MediaSequencesStarted() As Boolean
                Return MediaStarted
            End Function

            Public Sub AddSegmentInfo(info As InfTag)
                StartMediaSequences()
                SegmentsBuilder.AddSegmentInfo(info)
            End Sub

            Public Sub AddSegmentByteRange(range As ByteRangeTag)
                StartMediaSequences()
                SegmentsBuilder.AddSegmentByteRange(range.Bytes)
            End Sub

            Public Sub AddKey(key As KeyTag)
                StartMediaSequences()
                SegmentsBuilder.AddEncryptionKey(key)
            End Sub

            Public Sub AddInitialization(map As MediaInitializationTag)
                StartMediaSequences()
                SegmentsBuilder.AddInitialization(map)
            End Sub

            Public Sub AddDateTime(dateTime As DateTimeTag)
                StartMediaSequences()
                SegmentsBuilder.AddDateTime(dateTime)
            End Sub

            Public Sub AddDateRange(dateRange As DateRangeTag)
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
                                          MediaSequenceNumber, DiscontinuitySequenceNumber, PlaylistEnds, Type,
                                          IFramesOnly, DateRangeList, SegmentsBuilder.Build())
            End Function
        End Class
    End Class

End Namespace