Imports Crunchyroll_Downloader.hls.tags
Imports Crunchyroll_Downloader.hls.tags.encryption
Imports Crunchyroll_Downloader.hls.tags.segment

Namespace hls.segment
    Public Class MediaSegment

        Public Property Duration As Double
        Public Property Title As String
        Public Property Bytes As ByteRange

        Public Property Uri As String

        Public Property EncryptionKey As KeyTag

        Public Property Initialization As MediaInitializationTag

        Public Property SegmentDateTime As DateTimeTag

        ' Whether this is the first media segment after a discontinuity
        Public HasDiscontinuity As Boolean = False


        ' This isn't explicitly listed in a playlist file, but is calculated as they are added to a parsed object
        Public Property SequenceNumber As Integer

        Public Property DiscontinuitySequenceNumber As Integer

        Public Overrides Function ToString() As String
            Return $"{{
Segment, Duration: {Duration},
URI: {Uri},
Byte Range: {Bytes}
}}"
        End Function

        Public Sub New()
        End Sub

        Public Sub New(other As MediaSegment)
            Duration = other.Duration
            Title = other.Title
            Uri = other.Uri
            HasDiscontinuity = other.HasDiscontinuity
            SequenceNumber = other.SequenceNumber
            DiscontinuitySequenceNumber = other.DiscontinuitySequenceNumber

            If other.Bytes IsNot Nothing Then
                Bytes = New ByteRange(other.Bytes)
            End If

            If other.EncryptionKey IsNot Nothing Then
                EncryptionKey = New KeyTag(other.EncryptionKey)
            End If

            If other.Initialization IsNot Nothing Then
                Initialization = New MediaInitializationTag(other.Initialization)
            End If

            If other.SegmentDateTime IsNot Nothing Then
                SegmentDateTime = New DateTimeTag(other.SegmentDateTime)
            End If
        End Sub
    End Class

End Namespace
