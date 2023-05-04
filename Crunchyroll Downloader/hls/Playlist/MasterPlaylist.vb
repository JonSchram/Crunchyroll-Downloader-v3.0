
Imports System.Collections.Immutable
Imports Crunchyroll_Downloader.hls.tags
Imports Crunchyroll_Downloader.hls.tags.encryption
Imports Crunchyroll_Downloader.hls.tags.stream

Namespace hls.playlist
    ''' <summary>
    ''' A playlist that contains multiple multiple independent variants of a stream.
    ''' </summary>
    Public Class MasterPlaylist
        Inherits AbstractPlaylist

        Public ReadOnly Property Key As SessionKeyTag

        Public ReadOnly Property PlaylistMedia As ImmutableList(Of MediaTag)

        Public ReadOnly Property StreamVariants As ImmutableList(Of VariantStream)

        Public ReadOnly Property IframeStreams As ImmutableList(Of IFrameStream)

        Public Sub New(version As Integer, independentSegments As Boolean, startPlayTime As StartTag,
                       key As SessionKeyTag, playlistMedia As List(Of MediaTag),
                       streamVariants As List(Of VariantStream), iframeStreams As List(Of IFrameStream))
            MyBase.New(version, independentSegments, startPlayTime)
            Me.Key = key
            Me.PlaylistMedia = ImmutableList.CreateRange(playlistMedia)
            Me.StreamVariants = ImmutableList.CreateRange(streamVariants)
            Me.IframeStreams = ImmutableList.CreateRange(iframeStreams)
        End Sub

        ' Not planning to support session data, don't see any use case for this at all

        Public Overrides Function ToString() As String
            Return $"{{
  isIndependentSegments: {IndependentSegments},
  StartPlayTime: {StartPlayTime},
  Key: {Key},
  PlaylistMedia: {FormatFieldList(PlaylistMedia)},
  StreamVariants: {FormatFieldList(StreamVariants)},
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

        Public Class Builder
            Inherits AbstractPlaylist.AbstractBuilder
            Private Key As SessionKeyTag

            Private PlaylistMedia As List(Of MediaTag) = New List(Of MediaTag)

            Private StreamVariants As List(Of VariantStream) = New List(Of VariantStream)

            Private IframeStreams As List(Of IFrameStream) = New List(Of IFrameStream)

            Public Sub SetKey(key As SessionKeyTag)
                Me.Key = key
            End Sub

            Public Sub AddPlaylistMedia(media As MediaTag)
                PlaylistMedia.Add(media)
            End Sub

            Public Sub AddStreamVariant(streamVariant As VariantStream)
                StreamVariants.Add(streamVariant)
            End Sub

            Public Sub AddIframeStream(stream As IFrameStream)
                IframeStreams.Add(stream)
            End Sub

            Public Function Build() As MasterPlaylist
                Return New MasterPlaylist(Version, IndependentSegments, StartPlayTime, Key, PlaylistMedia,
                                          StreamVariants, IframeStreams)
            End Function
        End Class
    End Class
End Namespace