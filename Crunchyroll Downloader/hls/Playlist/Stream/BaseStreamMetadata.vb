Imports System.Collections.Immutable
Imports Crunchyroll_Downloader.hls.common

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

            For Each rendition In renditions
                result.Add(rendition.GroupId, rendition)
            Next

            Return result
        End Function

        Public MustInherit Class Builder(Of T)

            Protected Uri As String
            Protected Bandwidth As Integer
            Protected AverageBandwidth As Integer
            Protected VideoResolution As Resolution
            Protected MediaRenditions As List(Of AlternativeRendition)
            Protected HdcpLevel As Hdcp = Hdcp.NONE
            Protected Codecs As String

            Public Sub SetUri(uri As String)
                Me.Uri = uri
            End Sub

            Public Sub SetBandwidth(bandwidth As Integer)
                Me.Bandwidth = bandwidth
            End Sub

            Public Sub SetAverageBandwidth(averageBandwidth As Integer)
                Me.AverageBandwidth = averageBandwidth
            End Sub

            Public Sub SetMediaRenditions(mediaRenditions As List(Of AlternativeRendition))
                Me.MediaRenditions = mediaRenditions
            End Sub

            Public Sub SetHdcpLevel(hdcpLevel As Hdcp)
                Me.HdcpLevel = hdcpLevel
            End Sub

            Public Sub SetCodecs(codecs As String)
                Me.Codecs = codecs
            End Sub

            Public MustOverride Function Build() As T
        End Class
    End Class
End Namespace