
Imports Crunchyroll_Downloader.hls.tags
Imports Crunchyroll_Downloader.hls.tags.encryption
Imports Crunchyroll_Downloader.hls.tags.stream

Namespace hls.playlist
    ''' <summary>
    ''' A playlist that contains multiple multiple independent variants of a stream.
    ''' </summary>
    Public Class MasterPlaylist

        Public Property IsIndependentSegments As Boolean = False

        Public Property Key As SessionKey

        Public Property PlaylistMedia As List(Of Media) = New List(Of Media)

        Public Property StreamVariants As List(Of VariantStream) = New List(Of VariantStream)

        Public Property IframeStreams As List(Of IFrameStream) = New List(Of IFrameStream)

        Public Overrides Function ToString() As String
            Return $"{{
  isIndependentSegments: {IsIndependentSegments},
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
    End Class

End Namespace