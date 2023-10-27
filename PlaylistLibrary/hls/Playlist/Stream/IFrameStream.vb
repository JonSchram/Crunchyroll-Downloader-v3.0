Imports System.Collections.Immutable
Imports PlaylistLibrary.hls.common
Imports PlaylistLibrary.hls.playlist.rendition

Namespace hls.playlist.stream
    ''' <summary>
    ''' A representation of an I-frame stream and all associated alternative video renditions.
    ''' </summary>
    Public Class IFrameStream

        Public ReadOnly Property IFrameInfo As IFrameStreamMetadata

        Public ReadOnly Property VideoRenditions As ImmutableList(Of LinkedRendition)


        Public Sub New(iframeRendition As IFrameStreamMetadata, videoRenditions As List(Of LinkedRendition))
            Me.IFrameInfo = iframeRendition
            Me.VideoRenditions = ImmutableList.CreateRange(videoRenditions)
        End Sub

        Public Function CreateFromRenditionPool(iFrameInfo As IFrameStreamMetadata,
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