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

        Public Property Type As PlaylistTypeTag.PlaylistType

        Public Property IFramesOnly As Boolean = False

        ' Unused, seems to be for a server to give extra date range information to the player.
        Public Property DateRangeList As List(Of DateRangeTag)

        Public Property AllSegments As MediaSegmentsCollection

        Private MediaStarted As Boolean = False

        Public Sub New()

        End Sub

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
            ' TODO: Set end list somewhere
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
                AllSegments = New MediaSegmentsCollection(MediaSequenceNumber, DiscontinuitySequenceNumber)
            End If
            MediaStarted = True
        End Sub

        Private Function MediaSequencesStarted() As Boolean
            Return MediaStarted
        End Function

        Public Sub AddSegmentInfo(info As InfTag)
            StartMediaSequences()
            AllSegments.AddSegmentInfo(info)
        End Sub

        Public Sub AddSegmentByteRange(range As ByteRangeTag)
            StartMediaSequences()
            AllSegments.AddSegmentByteRange(range.Bytes)
        End Sub

        Public Sub AddKey(key As KeyTag)
            StartMediaSequences()
            AllSegments.AddEncryptionKey(key)
        End Sub

        Public Sub AddInitialization(map As MediaInitializationTag)
            StartMediaSequences()
            AllSegments.AddInitialization(map)
        End Sub

        Public Sub AddDateTime(dateTime As DateTimeTag)
            StartMediaSequences()
            AllSegments.AddDateTime(dateTime)
        End Sub

        Public Sub AddDateRange(dateRange As DateRangeTag)
            StartMediaSequences()
            DateRangeList.Add(dateRange)
        End Sub

        Public Sub AddDiscontinuity()
            StartMediaSequences()
            AllSegments.AddDiscontinuity()
        End Sub

        Public Sub AddSegmentUri(uri As String)
            StartMediaSequences()
            AllSegments.SetCurrentSegmentUri(uri)
        End Sub


        ' TODO: Make API to get iterable segments

    End Class

End Namespace