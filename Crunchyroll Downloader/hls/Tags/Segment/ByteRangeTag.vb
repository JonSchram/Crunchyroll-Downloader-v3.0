Imports Crunchyroll_Downloader.hls.segment

Namespace hls.tags.segment
    Public Class ByteRangeTag
        Const TagName = "EXT-X-BYTERANGE"

        Public ReadOnly Property Bytes As ByteRange


        Public Sub New(SourceTag As Tag)
            If TagName <> SourceTag.getTagName() Then
                Throw New ArgumentException($"Tag {SourceTag.getTagName()} is incorrect for byte range, expected {TagName}")
            End If

            Dim values = SourceTag.GetValues()
            If values.Count = 0 Then
                Throw New HlsFormatException($"{TagName} requires a value indicating the length and/or offset")
            End If
            Dim ByteRangeString = values(0)
            Bytes = New ByteRange(ByteRangeString)
        End Sub
    End Class
End Namespace

