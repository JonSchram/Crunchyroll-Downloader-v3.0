﻿Namespace hls.tags
    Public Class DateRangeTag
        Const TagName = "EXT-X-DATERANGE"

        ' No plans to do anything with this tag, but including it to make sure the entire playlist is parsed

        Public ReadOnly Property Id As String

        Public ReadOnly Property ClassAttribute As String

        Public ReadOnly Property StartDate As String

        Public ReadOnly Property EndDate As String

        Public ReadOnly Property Duration As String

        Public ReadOnly Property EndOnNext As String

        Public Sub New(SourceTag As Tag)
            If TagName <> SourceTag.getTagName() Then
                Throw New ArgumentException($"Tag {SourceTag.getTagName()} is incorrect for a date range, expected {TagName}")
            End If

            Id = SourceTag.GetAttribute("ID")
            ClassAttribute = SourceTag.GetAttribute("CLASS")
            StartDate = SourceTag.GetAttribute("START-DATE")
            EndDate = SourceTag.GetAttribute("END-DATE")
            Duration = SourceTag.GetAttribute("DURATION")
            EndOnNext = SourceTag.GetAttribute("END-ON-NEXT")

            ' This doesn't bother parsing the x- prefix for custom attributes or the SCTE-35 data.

        End Sub
    End Class
End Namespace
