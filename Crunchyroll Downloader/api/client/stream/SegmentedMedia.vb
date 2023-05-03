Imports Crunchyroll_Downloader.hls.playlist

Namespace api.client.stream
    Public MustInherit Class SegmentedMedia
        Inherits MediaStream

        Private ReadOnly SegmentList As List(Of MediaSegment)

        Public Sub New(type As MediaType)
            MyBase.New(type)
        End Sub

        Public Function GetSegment(segmentNumber As Integer) As MediaSegment
            Return SegmentList.Item(segmentNumber)
        End Function

        Public Function GetSegmentCount() As Integer
            Return SegmentList.Count
        End Function

        MustOverride Sub WritePlaylistTo(s As IO.Stream)

    End Class
End Namespace