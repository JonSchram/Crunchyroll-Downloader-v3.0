Imports System.Collections.Immutable
Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.playlist.rendition

Namespace hls.playlist.stream
    ''' <summary>
    ''' A variant stream built from a main rendition and alternative renditions, filtered for the group
    ''' IDs specified in the main rendition.
    ''' 
    ''' This represents everything that could be used to create a playback at a single quality setting.
    ''' </summary>
    Public Class VariantStream
        Public ReadOnly Property MainRendition As VariantStreamMetadata

        Public ReadOnly Property VideoRenditions As IImmutableList(Of LinkedRendition)
        Public ReadOnly Property AudioRenditions As IImmutableList(Of LinkedRendition)
        Public ReadOnly Property SubtitleRenditions As IImmutableList(Of LinkedRendition)
        Public ReadOnly Property ClosedCaptionInfo As IImmutableList(Of ClosedCaptionRendition)


        ''' <summary>
        ''' Calculated value indicating which types of media are present in a variant stream.
        ''' This doesn't necessarily indicate that there are alternate renditions of this type - they
        ''' may be embedded in the primary stream.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property PresentMedia As ISet(Of MediaType)


        Public Sub New(mainRendition As VariantStreamMetadata, videoRenditions As List(Of LinkedRendition),
                       audioRenditions As List(Of LinkedRendition), subtitleRenditions As List(Of LinkedRendition),
                       closedCaptionRenditions As List(Of ClosedCaptionRendition))
            Me.MainRendition = mainRendition
            Me.VideoRenditions = ImmutableList.CreateRange(videoRenditions)
            Me.AudioRenditions = ImmutableList.CreateRange(audioRenditions)
            Me.SubtitleRenditions = ImmutableList.CreateRange(subtitleRenditions)
            ClosedCaptionInfo = ImmutableList.CreateRange(closedCaptionRenditions)

            PresentMedia = CalculatePresentMedia()
        End Sub

        Private Function CalculatePresentMedia() As ISet(Of MediaType)
            Dim result = CodecClassifier.ClassifyCodecs(MainRendition.Codecs)

            If VideoRenditions.Count > 0 Then
                result.Add(MediaType.VIDEO)
            End If

            If AudioRenditions.Count > 0 Then
                result.Add(MediaType.AUDIO)
            End If

            If SubtitleRenditions.Count > 0 Then
                result.Add(MediaType.SUBTITLES)
            End If

            If ClosedCaptionInfo.Count > 0 Then
                result.Add(MediaType.CLOSED_CAPTIONS)
            End If

            Return result
        End Function

        Public Function CreateFromRenditionPool(mainStream As VariantStreamMetadata,
                                                renditionPool As List(Of AlternativeRendition)) As VariantStream
            Dim videos = FindLinkedRenditions(mainStream.VideoGroup, MediaType.VIDEO, renditionPool)
            Dim audio = FindLinkedRenditions(mainStream.AudioGroup, MediaType.AUDIO, renditionPool)
            Dim subtitles = FindLinkedRenditions(mainStream.SubtitleGroup, MediaType.SUBTITLES, renditionPool)
            Dim closedCaptions = FindClosedCaptionRenditions(mainStream.ClosedCaptionsGroup, renditionPool)

            Return New VariantStream(mainStream, videos, audio, subtitles, closedCaptions)
        End Function

        Private Function FindLinkedRenditions(groupId As String, type As MediaType, pool As List(Of AlternativeRendition)) As List(Of LinkedRendition)
            Dim matchingRenditions As New List(Of LinkedRendition)

            For Each item In pool
                If item.Type = type AndAlso TypeOf item Is LinkedRendition AndAlso item.GroupId = groupId Then
                    matchingRenditions.Add(CType(item, LinkedRendition))
                End If
            Next

            Return matchingRenditions
        End Function

        Private Function FindClosedCaptionRenditions(groupId As String, pool As List(Of AlternativeRendition)) As List(Of ClosedCaptionRendition)
            Dim matchingRenditions As New List(Of ClosedCaptionRendition)

            For Each item In pool
                If item.Type = MediaType.CLOSED_CAPTIONS AndAlso TypeOf item Is ClosedCaptionRendition AndAlso item.GroupId = groupId Then
                    matchingRenditions.Add(CType(item, ClosedCaptionRendition))
                End If
            Next

            Return matchingRenditions
        End Function

        ''' <summary>
        ''' Gets all renditions required to play this variant stream.
        ''' 
        ''' If there are multiple alternative renditions, gets the default. Prefers any alternative renditions over the
        ''' built-in version.
        ''' </summary>
        Public Sub GetFullStream()
            ' TODO:
            ' - Convert to function
            ' Create a data type for a stream.
        End Sub
    End Class
End Namespace