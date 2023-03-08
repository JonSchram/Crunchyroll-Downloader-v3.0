Namespace hls.tags
    Public Class PlaylistTypeTag
        Const TagName = "EXT-X-PLAYLIST-TYPE"

        Public ReadOnly Property Type As PlaylistType

        Public Sub New(SourceTag As Tag)
            If TagName <> SourceTag.getTagName() Then
                Throw New ArgumentException($"Tag {SourceTag.getTagName()} is incorrect for byte range, expected {TagName}")
            End If

            Dim values = SourceTag.GetValues()
            If values.Count = 0 Then
                Throw New HlsFormatException($"{TagName} must specify either EVENT or VOD")
            End If
            Type = CType([Enum].Parse(GetType(PlaylistType), values(0)), PlaylistType)
        End Sub

        Public Overrides Function ToString() As String
            Return $"{{
Type: {Type}
}}"
        End Function

        Public Enum PlaylistType
            [EVENT]
            VOD
        End Enum

    End Class
End Namespace
