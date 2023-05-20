﻿Imports Crunchyroll_Downloader.hls.common

Namespace hls.parsing.tags
    Public Class DateTimeTag
        Const TagName = "EXT-X-PROGRAM-DATE-TIME"
        ' Not used but parsing this to keep the playlist well-formatted
        Public Property ProgramDateTime As String

        Public Sub New(SourceTag As TagAttributes)
            If SourceTag.getTagName() <> TagName Then
                Throw New HlsFormatException($"Provided tag {SourceTag.getTagName()} does not match required tag name {TagName}.")
            End If

            If SourceTag.GetValues().Count > 0 Then
                ProgramDateTime = SourceTag.GetValues(0)
            End If
        End Sub

        Public Sub New(other As DateTimeTag)
            ProgramDateTime = other.ProgramDateTime
        End Sub

        Public Function GetDateTime() As SegmentDateTime
            Return New SegmentDateTime(ProgramDateTime)
        End Function

        Public Overrides Function ToString() As String
            Return $"{{
ProgramDateTime: {ProgramDateTime}
}}"
        End Function
    End Class
End Namespace
