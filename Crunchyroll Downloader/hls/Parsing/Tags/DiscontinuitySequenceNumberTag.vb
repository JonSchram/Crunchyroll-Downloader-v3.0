Imports Crunchyroll_Downloader.hls

Namespace hls.parsing.tags
    Public Class DiscontinuitySequenceNumberTag
        Const TagName = "EXT-X-DISCONTINUITY-SEQUENCE"

        Public ReadOnly Property StartNumber As Integer

        Public Sub New(SourceTag As TagAttributes)
            If SourceTag.getTagName() <> TagName Then
                Throw New HlsFormatException($"Provided tag {SourceTag.getTagName()} does not match required tag name {TagName}.")
            End If

            Dim values = SourceTag.GetValues()
            If values.Count = 0 Then
                Throw New HlsFormatException($"Start sequence number must be set for {TagName}")
            End If
            StartNumber = CInt(values(0))
        End Sub

        Public Overrides Function ToString() As String
            Return $"{{
StartNumber: {StartNumber}
}}"
        End Function
    End Class
End Namespace