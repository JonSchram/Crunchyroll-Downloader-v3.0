Namespace hls.parsing.tags
    Public Class DateRangeTag
        Const TagName = "EXT-X-DATERANGE"

        ' No plans to do anything with this tag, but including it to make sure the entire playlist is parsed

        Public ReadOnly Property Id As String

        Public ReadOnly Property ClassAttribute As String

        Public ReadOnly Property StartDate As String

        Public ReadOnly Property EndDate As String

        Public ReadOnly Property Duration As String

        Public ReadOnly Property PlannedDuration As String

        Public ReadOnly Property EndOnNext As String

        Public ReadOnly Property Scte35Cmd As String

        Public ReadOnly Property Scte35Out As String

        Public ReadOnly Property Scte35In As String

        Public ReadOnly UnparsedAttributes As List(Of KeyValuePair(Of String, String))

        Public Sub New(SourceTag As TagAttributes)
            If TagName <> SourceTag.getTagName() Then
                Throw New ArgumentException($"Tag {SourceTag.getTagName()} is incorrect for a date range, expected {TagName}")
            End If

            Id = SourceTag.GetAttribute("ID")
            ClassAttribute = SourceTag.GetAttribute("CLASS")
            StartDate = SourceTag.GetAttribute("START-DATE")
            EndDate = SourceTag.GetAttribute("END-DATE")
            Duration = SourceTag.GetAttribute("DURATION")
            PlannedDuration = SourceTag.GetAttribute("PLANNED-DURATION")
            EndOnNext = SourceTag.GetAttribute("END-ON-NEXT")
            Scte35Cmd = SourceTag.GetAttribute("SCTE35-CMD")
            Scte35In = SourceTag.GetAttribute("SCTE35-IN")
            Scte35Out = SourceTag.GetAttribute("SCTE35-OUT")

            Dim definedTags As String() = {"ID", "CLASS", "START-DATE", "END-DATE", "DURATION",
                "PLANNED-DURATION", "END-ON-NEXT", "SCTE35-CMD", "SCTE35-OUT", "SCTE35-IN"}

            ' There may be x-client-attributes or SCTE-35 data that would be nice to preserve if this
            ' tag were written to a file again.
            UnparsedAttributes = SourceTag.GetRemainingAttributes(definedTags).ToList()
        End Sub

        Public Sub New(other As DateRangeTag)
            Id = other.Id
            ClassAttribute = other.ClassAttribute
            StartDate = other.StartDate
            EndDate = other.EndDate
            Duration = other.Duration
            EndOnNext = other.EndOnNext
        End Sub

        Public Overrides Function ToString() As String
            Return $"{{
  Id: {Id},
  Class: {ClassAttribute},
  StartDate: {StartDate},
  EndDate: {EndDate},
  Duration: {Duration},
  EndOnNext: {EndOnNext}
}}"
        End Function
    End Class
End Namespace
