Imports Crunchyroll_Downloader.hls.segment

Namespace hls.tags.segment
    Public Class MediaInitializationTag
        Const TagName = "EXT-X-MAP"

        Public Property Uri As String

        ' Optional.
        Public Property Bytes As ByteRange


        Public Sub New(SourceTag As Tag)
            If TagName <> SourceTag.getTagName() Then
                Throw New ArgumentException($"Tag {SourceTag.getTagName()} is incorrect for media, expected {TagName}")
            End If

            Uri = SourceTag.GetAttribute("URI")
            If Uri Is Nothing Then
                Throw New HlsFormatException($"{TagName} requires URI to be set.")
            End If

            Dim ByteRangeString = SourceTag.GetAttribute("BYTERANGE")
            Bytes = New ByteRange(ByteRangeString)
        End Sub

        Public Sub New(other As MediaInitializationTag)
            Uri = other.Uri
            If other.Bytes IsNot Nothing Then
                Bytes = New ByteRange(other.Bytes)
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return $"{{
Uri: {Uri},
ByteRange: {Bytes}
}}"
        End Function
    End Class
End Namespace
