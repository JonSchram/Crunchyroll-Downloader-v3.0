Imports System.Collections.Immutable
Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.playlist.rendition

Namespace hls.playlist.stream
    ''' <summary>
    ''' Abstract type for any main rendition (I-frame or variant stream) in a master playlist.
    ''' </summary>
    Public MustInherit Class BaseStreamMetadata
        Protected ReadOnly Property Uri As String

        Public ReadOnly Property Bandwidth As Integer

        Public ReadOnly Property AverageBandwidth As Integer

        Public ReadOnly Property VideoResolution As Resolution

        Public ReadOnly Property VideoGroup As String

        Public ReadOnly Property HdcpLevel As Hdcp = Hdcp.NONE

        Public ReadOnly Property Codecs As IImmutableList(Of String)


        Public Sub New(uri As String, bandwidth As Integer, averageBandwidth As Integer,
                   videoResolution As Resolution, videoGroup As String, hdcpLevel As Hdcp,
                   codecs As IEnumerable(Of String))
            Me.Uri = uri
            Me.Bandwidth = bandwidth
            Me.AverageBandwidth = averageBandwidth
            Me.VideoResolution = videoResolution
            Me.VideoGroup = videoGroup
            Me.HdcpLevel = hdcpLevel
            Me.Codecs = ImmutableList.CreateRange(codecs)
        End Sub

        Protected Function BuildRenditionDict(renditions As List(Of AlternativeRendition)) As IDictionary(Of String, AlternativeRendition)
            Dim result = New Dictionary(Of String, AlternativeRendition)

            For Each rend In renditions
                result.Add(rend.GroupId, rend)
            Next

            Return result
        End Function

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

        Public MustInherit Class Builder(Of T As BaseStreamMetadata)
            Implements IBaseStreamBuilder(Of T)

            Protected Uri As String
            Protected Bandwidth As Integer
            Protected AverageBandwidth As Integer
            Protected VideoResolution As Resolution
            Protected VideoGroupId As String
            Protected MediaRenditions As List(Of AlternativeRendition)
            Protected HdcpLevel As Hdcp = Hdcp.NONE
            Protected Codecs As String()

            Public Sub SetUri(uri As String) Implements IBaseStreamBuilder(Of T).SetUri
                Me.Uri = uri
            End Sub

            Public Sub SetBandwidth(bandwidth As Integer) Implements IBaseStreamBuilder(Of T).SetBandwidth
                Me.Bandwidth = bandwidth
            End Sub

            Public Sub SetAverageBandwidth(averageBandwidth As Integer) Implements IBaseStreamBuilder(Of T).SetAverageBandwidth
                Me.AverageBandwidth = averageBandwidth
            End Sub

            Public Sub SetMediaRenditions(mediaRenditions As List(Of AlternativeRendition)) Implements IBaseStreamBuilder(Of T).SetMediaRenditions
                Me.MediaRenditions = mediaRenditions
            End Sub

            Public Sub SetHdcpLevel(hdcpLevel As Hdcp) Implements IBaseStreamBuilder(Of T).SetHdcpLevel
                Me.HdcpLevel = hdcpLevel
            End Sub

            Public Sub SetCodecs(codecs As String()) Implements IBaseStreamBuilder(Of T).SetCodecs
                Me.Codecs = codecs
            End Sub

            Public Sub SetVideoResolution(resolution As Resolution) Implements IBaseStreamBuilder(Of T).SetVideoResolution
                VideoResolution = resolution
            End Sub

            Public Sub SetVideoGroup(video As String) Implements IBaseStreamBuilder(Of T).SetVideoGroup
                VideoGroupId = video
            End Sub

            Public MustOverride Function Build() As T Implements IBaseStreamBuilder(Of T).Build
        End Class
    End Class
End Namespace