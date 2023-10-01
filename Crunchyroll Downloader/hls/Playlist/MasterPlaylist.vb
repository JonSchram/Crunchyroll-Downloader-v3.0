
Imports System.Collections.Immutable
Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.playlist.rendition
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

        ''' <summary>
        ''' All session data in the master playlist.
        ''' <para>
        ''' The value is a list because the data-id may be duplicated if the language is different.
        ''' </para>
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property SessionData As ImmutableDictionary(Of String, List(Of SessionData))

        ' TODO: If any closed caption streams contain the enumerated string NONE, set a flag in playlist
        ' indicating there are no closed caption streams in any variant.

        Public Sub New(version As Integer, independentSegments As Boolean, startPlayTime As PlaylistStartTime,
            SessionKeys As List(Of EncryptionKey), playlistMedia As List(Of AlternativeRendition),
            streamVariants As List(Of VariantStreamMetadata), iframeStreams As List(Of IFrameStreamMetadata),
            sessionData As Dictionary(Of String, List(Of SessionData)))
            MyBase.New(version, independentSegments, startPlayTime)
            Me.SessionKeys = SessionKeys
            Me.AlternateRenditions = ImmutableList.CreateRange(playlistMedia)
            Me.VariantStreams = ImmutableList.CreateRange(streamVariants)
            Me.IframeStreams = ImmutableList.CreateRange(iframeStreams)
            Me.SessionData = ImmutableDictionary.CreateRange(sessionData)
        End Sub

        Public Function GetClosestMatch(comparer As IComparer(Of VariantStreamMetadata)) As VariantStream
            If VariantStreams.Count = 0 Then
                Return Nothing
            End If

            Dim bestVariant As VariantStreamMetadata = VariantStreams.Item(0)

            For i As Integer = 1 To VariantStreams.Count - 1
                If comparer.Compare(bestVariant, VariantStreams.Item(i)) < 0 Then
                    ' Current best is less (worse match) than the new item.
                    bestVariant = VariantStreams.Item(i)
                End If
            Next

            Return BuildVariantStream(bestVariant)
        End Function

        Private Function BuildVariantStream(metadata As VariantStreamMetadata) As VariantStream
            Dim videoGroup As RenditionGroup(Of LinkedRendition) = FindRenditionGroup(Of LinkedRendition)(metadata.VideoGroup, MediaType.VIDEO)
            Dim audioGroup As RenditionGroup(Of LinkedRendition) = FindRenditionGroup(Of LinkedRendition)(metadata.AudioGroup, MediaType.AUDIO)
            Dim subtitleGroup As RenditionGroup(Of LinkedRendition) = FindRenditionGroup(Of LinkedRendition)(metadata.SubtitleGroup, MediaType.SUBTITLES)
            Dim closedCaptionsGroup As RenditionGroup(Of ClosedCaptionRendition) = FindRenditionGroup(Of ClosedCaptionRendition)(metadata.ClosedCaptionsGroup, MediaType.CLOSED_CAPTIONS)

            Return New VariantStream(metadata, videoGroup, audioGroup, subtitleGroup, closedCaptionsGroup)
        End Function

        Private Function FindRenditionGroup(Of T As AlternativeRendition)(groupId As String, type As MediaType) As RenditionGroup(Of T)
            Dim builder = New RenditionGroup(Of T).Builder()
            For Each entry In AlternateRenditions
                If entry.Type = type AndAlso entry.GroupId = groupId AndAlso TypeOf entry Is T Then
                    builder.AddRendition(CType(entry, T))
                End If
            Next

            Return builder.Build()
        End Function

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

        Public Class Builder
            Inherits AbstractPlaylistBuilder
            Private ReadOnly Keys As New List(Of EncryptionKey)

            Private ReadOnly PlaylistMedia As New List(Of AlternativeRendition)

            Private ReadOnly StreamVariants As New List(Of VariantStreamMetadata)

            Private ReadOnly IframeStreams As New List(Of IFrameStreamMetadata)

            Private ReadOnly SessionData As New Dictionary(Of String, List(Of SessionData))

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

            Public Sub AddSessionData(data As SessionData)
                If data Is Nothing Then
                    Return
                End If

                Dim dataList As List(Of SessionData) = Nothing
                If Not SessionData.TryGetValue(data.GetDataId(), dataList) Then
                    dataList = New List(Of SessionData)
                End If
                dataList.Add(data)
            End Sub

            Public Function Build() As MasterPlaylist
                Return New MasterPlaylist(Version, IndependentSegments, StartPlayTime, Keys, PlaylistMedia,
                    StreamVariants, IframeStreams, SessionData)
            End Function
        End Class
    End Class
End Namespace