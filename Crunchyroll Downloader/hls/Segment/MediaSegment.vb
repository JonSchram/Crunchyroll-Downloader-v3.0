Imports System.Collections.Immutable
Imports Crunchyroll_Downloader.hls.common

Namespace hls.segment
    Public Class MediaSegment

        Public ReadOnly Property Duration As Double
        Public ReadOnly Property Title As String
        Public ReadOnly Property Bytes As ByteRange

        Public ReadOnly Property Uri As String

        Public ReadOnly Property Keys As ImmutableList(Of EncryptionKey)

        Public ReadOnly Property Initialization As MediaInitialization

        Public ReadOnly Property SegmentDateTime As SegmentDateTime

        ' Whether this is the first media segment after a discontinuity
        Public ReadOnly HasDiscontinuity As Boolean = False


        ' This isn't explicitly listed in a playlist file, but is calculated as they are added to a parsed object
        Public ReadOnly Property SequenceNumber As Integer

        Public ReadOnly Property DiscontinuitySequenceNumber As Integer

        Public Overrides Function ToString() As String
            Return $"{{
Segment, Duration: {Duration},
URI: {Uri},
Byte Range: {Bytes}
}}"
        End Function

        Public Sub New(duration As Double, title As String, bytes As ByteRange, uri As String, keys As IList(Of EncryptionKey),
                       initialization As MediaInitialization, segmentDateTime As SegmentDateTime, hasDiscontinuity As Boolean,
                       sequenceNumber As Integer, discontinuitySequenceNumber As Integer)
            Me.Duration = duration
            Me.Title = title
            Me.Bytes = bytes
            Me.Uri = uri
            Me.Keys = ImmutableList.CreateRange(keys)
            Me.Initialization = initialization
            Me.SegmentDateTime = segmentDateTime
            Me.HasDiscontinuity = hasDiscontinuity
            Me.SequenceNumber = sequenceNumber
            Me.DiscontinuitySequenceNumber = discontinuitySequenceNumber
        End Sub
    End Class

End Namespace
