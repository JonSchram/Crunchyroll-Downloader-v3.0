Namespace hls.parsing.tags
    Public Class VersionTag
        Const TagName = "EXT-X-VERSION"

        Public ReadOnly Property Number As Integer
        Public Sub New(SourceTag As TagAttributes)
            If TagName <> SourceTag.getTagName() Then
                Throw New ArgumentException($"Tag {SourceTag.getTagName()} is incorrect for a date range, expected {TagName}")
            End If

            Dim values = SourceTag.GetValues()
            If values.Count() = 0 Then
                Throw New HlsFormatException("Version tag must specify a version number")
            End If
            Number = CInt(values.Item(0))
        End Sub

        Public Overrides Function ToString() As String
            Return $"Version {Number}"
        End Function
    End Class
End Namespace