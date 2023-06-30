Imports System.Collections.Immutable
Imports Crunchyroll_Downloader.hls.common

Namespace hls.playlist.stream
    ''' <summary>
    ''' A representation of an I-frame stream and all associated alternative video renditions.
    ''' </summary>
    Public Class IFrameStream

        Public ReadOnly Property IFrameInfo As IframeRendition

        Public ReadOnly Property VideoRenditions As ImmutableList(Of LinkedRendition)


        Public Sub New(iframeRendition As IframeRendition, videoRenditions As List(Of LinkedRendition))
            Me.IFrameInfo = iframeRendition
            Me.VideoRenditions = ImmutableList.CreateRange(videoRenditions)
        End Sub

        Public Function CreateFromRenditionPool(iFrameInfo As IframeRendition,
                                            pool As List(Of AlternativeRendition)) As IFrameStream

            Return New IFrameStream(iFrameInfo, FindVideoRenditions(iFrameInfo.VideoGroup, pool))
        End Function

        Private Function FindVideoRenditions(groupId As String, pool As List(Of AlternativeRendition)) As List(Of LinkedRendition)
            Dim matchingRenditions As New List(Of LinkedRendition)

            For Each item In pool
                If item.Type = MediaType.VIDEO AndAlso TypeOf item Is LinkedRendition AndAlso item.GroupId = groupId Then
                    matchingRenditions.Add(CType(item, LinkedRendition))
                End If
            Next

            Return matchingRenditions
        End Function

    End Class
End Namespace