Namespace hls.tags
    Public Class MediaSequenceNumberTag
        Const TagName = "EXT-X-MEDIA-SEQUENCE"

        Public ReadOnly Property StartSequenceNumber As Integer

        Public Sub New(SourceTag As Tag)
            If SourceTag.getTagName() <> TagName Then
                Throw New HlsFormatException($"Provided tag {SourceTag.getTagName()} does not match required tag name {TagName}.")
            End If

            Dim values = SourceTag.GetValues()
            If values.Count = 0 Then
                Throw New HlsFormatException($"Start sequence number must be set for {TagName}")
            End If
            StartSequenceNumber = CInt(values(0))
        End Sub

        Public Overrides Function ToString() As String
            Return $"{{
StartSequenceNumber: {StartSequenceNumber}
}}"
        End Function
    End Class
End Namespace