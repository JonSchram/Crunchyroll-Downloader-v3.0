Imports Crunchyroll_Downloader.hls
Imports Crunchyroll_Downloader.hls.tags

Namespace hls.tags
    Public Class DiscontinuitySequenceNumberTag
        Const TagName = "EXT-X-DISCONTINUITY-SEQUENCE"

        Public ReadOnly Property StartNumber As Integer

        Public Sub New(SourceTag As Tag)
            If SourceTag.getTagName() <> TagName Then
                Throw New HlsFormatException($"Provided tag {SourceTag.getTagName()} does not match required tag name {TagName}.")
            End If

            Dim values = SourceTag.GetValues()
            If values.Count = 0 Then
                Throw New HlsFormatException($"Start sequence number must be set for {TagName}")
            End If
            StartNumber = CInt(values(0))
        End Sub
    End Class
End Namespace