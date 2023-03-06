Imports Crunchyroll_Downloader.hls.segment

Namespace hls.tags.segment
    Public Class ByteRangeTag
        Const TagName = "EXT-X-BYTERANGE"

        Public ReadOnly Property Bytes As ByteRange


        Public Sub New(SourceTag As Tag)
            If TagName <> SourceTag.getTagName() Then
                Throw New ArgumentException($"Tag {SourceTag.getTagName()} is incorrect for byte range, expected {TagName}")
            End If

            Dim ByteRangeString = SourceTag.GetAttribute("BYTERANGE")
            Bytes = New ByteRange(ByteRangeString)
        End Sub
    End Class
End Namespace

