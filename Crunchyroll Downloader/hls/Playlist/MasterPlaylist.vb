
Imports System.Collections.Immutable
Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.playlist.stream
Imports Crunchyroll_Downloader.hls.segment

Namespace hls.playlist
    ''' <summary>
    ''' A playlist that contains multiple multiple independent variants of a stream.
    ''' </summary>
    Public Class MasterPlaylist
        Inherits AbstractPlaylist

        ''' <summary>
        ''' All session keys used in the playlist.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property SessionKeys As IList(Of EncryptionKey)

        ''' <summary>
        ''' All alternate renditions present in the master playlist.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property AlternateRenditions As IReadOnlyList(Of AlternativeRendition)

        ''' <summary>
        ''' All streams in the master playlist representing variant streams.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property VariantStreams As ImmutableList(Of VariantStreamMetadata)

        ''' <summary>
        ''' All video streams in the playlist containing I-frames only.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property IframeStreams As ImmutableList(Of IFrameStreamMetadata)

        ' TODO: If any closed caption streams contain the enumerated string NONE, set a flag in playlist
        ' indicating there are no closed caption streams in any variant.

        Public Sub New(version As Integer, independentSegments As Boolean, startPlayTime As PlaylistStartTime,
                       SessionKeys As List(Of EncryptionKey), playlistMedia As List(Of AlternativeRendition),
                       streamVariants As List(Of VariantStreamMetadata), iframeStreams As List(Of IFrameStreamMetadata))
            MyBase.New(version, independentSegments, startPlayTime)
            Me.SessionKeys = SessionKeys
            Me.AlternateRenditions = ImmutableList.CreateRange(playlistMedia)
            Me.VariantStreams = ImmutableList.CreateRange(streamVariants)
            Me.IframeStreams = ImmutableList.CreateRange(iframeStreams)
        End Sub

        Public Function GetStream() As VariantStream
            ' TODO: Add parameters to filter renditions by language / type / resolution.
            Return Nothing
        End Function

        Private Function CreateRenditionGroups(media As List(Of AlternativeRendition)) As Dictionary(Of RenditionGroup, List(Of AlternativeRendition))
            Dim result As New Dictionary(Of RenditionGroup, List(Of AlternativeRendition))

            For Each entry In media
                Dim groupKey = New RenditionGroup(entry.GroupId, entry.Type)
                Dim groupMedia As List(Of AlternativeRendition)
                If result.ContainsKey(groupKey) Then
                    groupMedia = result.Item(groupKey)
                Else
                    groupMedia = New List(Of AlternativeRendition)
                    result.Add(groupKey, groupMedia)
                End If
                groupMedia.Add(entry)
            Next

            Return result
        End Function

        ' Not planning to support session data, don't see any use case for this at all

        Public Overrides Function ToString() As String
            Return $"{{
  isIndependentSegments: {IndependentSegments},
  StartPlayTime: {StartPlayTime},
  Key: {FormatFieldList(SessionKeys)},
  MainStreams: {FormatFieldList(VariantStreams)},
  IframeStreams: {FormatFieldList(IframeStreams)}
}}"
        End Function

        Private Function FormatFieldList(StreamList As IEnumerable(Of Object)) As String
            Dim output As String = "["

            For Each streamItem In StreamList
                output += streamItem.ToString() + ","
            Next

            output += "]"
            Return output
        End Function

        Private Function FormatRenditionGroups(Dict As IReadOnlyDictionary(Of RenditionGroup, List(Of AlternativeRendition))) As String
            Dim output As String = "{"

            For Each item In Dict
                output += item.Key.ToString()
                output += ":"
                output += "["
                For Each listItem In item.Value
                    output += listItem.ToString() + ","
                Next
                output += "]"
            Next

            output += "}"
            Return output
        End Function

        Public Class Builder
            Inherits AbstractBuilder
            Private Keys As New List(Of EncryptionKey)

            Private PlaylistMedia As New List(Of AlternativeRendition)

            Private StreamVariants As New List(Of VariantStreamMetadata)

            Private IframeStreams As New List(Of IFrameStreamMetadata)

            Public Sub AddKey(key As EncryptionKey)
                Keys.Add(key)
            End Sub

            Public Sub AddMedia(media As AlternativeRendition)
                PlaylistMedia.Add(media)
            End Sub

            Public Sub AddStreamVariant(streamVariant As VariantStreamMetadata)
                StreamVariants.Add(streamVariant)
            End Sub

            Public Sub AddIframeStream(stream As IFrameStreamMetadata)
                IframeStreams.Add(stream)
            End Sub

            Public Function Build() As MasterPlaylist
                Return New MasterPlaylist(Version, IndependentSegments, StartPlayTime, Keys, PlaylistMedia,
                                          StreamVariants, IframeStreams)
            End Function
        End Class
    End Class
End Namespace