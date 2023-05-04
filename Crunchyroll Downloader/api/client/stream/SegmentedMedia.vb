Imports Crunchyroll_Downloader.hls.playlist

Namespace api.client.stream
    Public MustInherit Class SegmentedMedia
        Inherits MediaStream

        Public Sub New(type As MediaType)
            MyBase.New(type)
        End Sub

        Public MustOverride Function GetSegment(segmentNumber As Integer) As StreamSegment

        Public MustOverride Function GetSegmentCount() As Integer

        MustOverride Sub WritePlaylistTo(s As IO.Stream)

    End Class
End Namespace