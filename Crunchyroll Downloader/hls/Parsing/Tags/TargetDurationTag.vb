Namespace hls.parsing.tags
    Public Class TargetDurationTag
        Const TagName = "EXT-X-TARGETDURATION"
        Public Property Duration As Integer

        Public Sub New(SourceTag As TagAttributes)
            If TagName <> SourceTag.getTagName() Then
                Throw New ArgumentException($"Tag {SourceTag.getTagName()} is incorrect for target duration, expected {TagName}")
            End If

            Dim values = SourceTag.GetValues()
            If values.Count = 0 Then
                Throw New HlsFormatException($"Target duration must be set for {TagName}")
            End If
            Duration = CInt(values(0))
        End Sub

        Public Overrides Function ToString() As String
            Return $"{{
Duration: {Duration}
}}"
        End Function
    End Class
End Namespace
