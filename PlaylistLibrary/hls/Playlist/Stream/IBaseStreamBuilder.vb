Imports PlaylistLibrary.hls.common
Imports PlaylistLibrary.hls.playlist.rendition

Namespace hls.playlist.stream
    Public Interface IBaseStreamBuilder(Of Out T As BaseStreamMetadata)
        Function Build() As T
        Sub SetUri(uri As String)
        Sub SetBandwidth(bandwidth As Integer)

        Sub SetAverageBandwidth(averageBandwidth As Integer)

        Sub SetMediaRenditions(mediaRenditions As List(Of AlternativeRendition))

        Sub SetHdcpLevel(hdcpLevel As Hdcp)

        Sub SetCodecs(codecs As String())

        Sub SetVideoResolution(resolution As Resolution)
        Sub SetVideoGroup(video As String)
    End Interface
End Namespace