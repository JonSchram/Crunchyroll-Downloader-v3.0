Namespace hls.tags.segment
    Public Class InfTag
        Const TagName = "EXTINF"
        Public ReadOnly Property Duration As Double
        Public ReadOnly Property Title As String

        Public Sub New(SourceTag As Tag)
            If TagName <> SourceTag.getTagName() Then
                Throw New ArgumentException($"Tag {SourceTag.getTagName()} is incorrect for media segment info, expected {TagName}")
            End If

            ' TODO: This looks really ugly, not sure how to improve it. Maybe add a getter to the Tag class?
            ' Like getValue(int)  & hasValue(int)
            Dim Values = SourceTag.GetValues()
            If Values.Count >= 1 Then
                Duration = CDbl(Values(0))
            End If

            If Values.Count >= 2 Then
                Title = Values(1)
            End If
        End Sub

    End Class
End Namespace
