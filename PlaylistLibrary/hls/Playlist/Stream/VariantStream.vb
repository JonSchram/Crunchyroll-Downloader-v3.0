Imports PlaylistLibrary.hls.common
Imports PlaylistLibrary.hls.playlist.rendition

Namespace hls.playlist.stream
    ''' <summary>
    ''' A variant stream built from a main rendition and alternative renditions, filtered for the group
    ''' IDs specified in the main rendition.
    ''' 
    ''' This represents everything that could be used to create a playback at a single quality setting.
    ''' </summary>
    Public Class VariantStream
        Public ReadOnly Property MainRendition As VariantStreamMetadata

        Public ReadOnly Property VideoRenditions As RenditionGroup(Of LinkedRendition)
        Public ReadOnly Property AudioRenditions As RenditionGroup(Of LinkedRendition)
        Public ReadOnly Property SubtitleRenditions As RenditionGroup(Of LinkedRendition)
        Public ReadOnly Property ClosedCaptionInfo As RenditionGroup(Of ClosedCaptionRendition)


        ''' <summary>
        ''' Calculated value indicating which types of media are present in a variant stream.
        ''' This doesn't necessarily indicate that there are alternate renditions of this type - they
        ''' may be embedded in the primary stream.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property PresentMedia As ISet(Of MediaType)


        Public Sub New(mainRendition As VariantStreamMetadata, videoRenditions As RenditionGroup(Of LinkedRendition),
                       audioRenditions As RenditionGroup(Of LinkedRendition), subtitleRenditions As RenditionGroup(Of LinkedRendition),
                       closedCaptionRenditions As RenditionGroup(Of ClosedCaptionRendition))
            Me.MainRendition = mainRendition
            Me.VideoRenditions = videoRenditions
            Me.AudioRenditions = audioRenditions
            Me.SubtitleRenditions = subtitleRenditions
            ClosedCaptionInfo = closedCaptionRenditions

            PresentMedia = CalculatePresentMedia()
        End Sub

        Private Function CalculatePresentMedia() As ISet(Of MediaType)
            Dim result = CodecClassifier.ClassifyCodecs(MainRendition.Codecs)

            If VideoRenditions.Renditions.Count > 0 Then
                result.Add(MediaType.VIDEO)
            End If

            If AudioRenditions.Renditions.Count > 0 Then
                result.Add(MediaType.AUDIO)
            End If

            If SubtitleRenditions.Renditions.Count > 0 Then
                result.Add(MediaType.SUBTITLES)
            End If

            If ClosedCaptionInfo.Renditions.Count > 0 Then
                result.Add(MediaType.CLOSED_CAPTIONS)
            End If

            Return result
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

        Public Overrides Function ToString() As String
            Return $"[VariantStream: Main rendition: {MainRendition}, " +
                $"video renditions: {VideoRenditions}, " +
                $"audio renditions: {AudioRenditions}, " +
                $"subtitle renditions: {SubtitleRenditions}, " +
                $"closed caption renditions: {ClosedCaptionInfo} ]"
        End Function
    End Class
End Namespace