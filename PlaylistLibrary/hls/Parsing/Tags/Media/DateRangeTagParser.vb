Imports PlaylistLibrary.hls.common
Imports PlaylistLibrary.hls.playlist

Namespace hls.parsing.tags.media
    Public Class DateRangeTagParser
        Inherits TagParser(Of MediaPlaylistBuilder)

        ' No plans to do anything with this tag, but including it to make sure the entire playlist is parsed

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As ParsedTag, playlist As MediaPlaylistBuilder)

            Dim Id As String = attributes.GetAttribute("ID")?.Value
            Dim ClassAttribute As String = attributes.GetAttribute("CLASS")?.Value

            Dim startDateString As String = attributes.GetAttribute("START-DATE")?.Value
            If startDateString Is Nothing Then
                Throw New HlsFormatException($"Parse failure - {GetTagName()} requires a start date.")
            End If
            Dim StartDate As Date = Date.ParseExact(startDateString, "s", Globalization.CultureInfo.InvariantCulture)

            Dim EndDateString As String = attributes.GetAttribute("END-DATE")?.Value
            Dim endDate As Date = Nothing
            If EndDateString IsNot Nothing Then
                endDate = Date.ParseExact(EndDateString, "s", Globalization.CultureInfo.InvariantCulture)
            End If

            Dim DurationString As String = attributes.GetAttribute("DURATION")?.Value
            Dim duration As Decimal? = Nothing
            If DurationString IsNot Nothing Then
                duration = Decimal.Parse(DurationString)
            End If

            Dim PlannedDurationString As String = attributes.GetAttribute("PLANNED-DURATION")?.Value
            Dim plannedDuration As Decimal? = Nothing
            If PlannedDurationString IsNot Nothing Then
                plannedDuration = Decimal.Parse(PlannedDurationString)
            End If

            Dim EndOnNextString As String = attributes.GetAttribute("END-ON-NEXT")?.Value
            Dim endOnNext As Boolean = False
            If EndOnNextString IsNot Nothing Then
                endOnNext = HlsHelpers.ParseYesValue(EndOnNextString, GetTagName(), "END-ON-NEXT")
            End If

            Dim Scte35Cmd As String = attributes.GetAttribute("SCTE35-CMD")?.Value
            Dim Scte35In As String = attributes.GetAttribute("SCTE35-IN")?.Value
            Dim Scte35Out As String = attributes.GetAttribute("SCTE35-OUT")?.Value

            Dim definedTags As String() = {"ID", "CLASS", "START-DATE", "END-DATE", "DURATION",
                "PLANNED-DURATION", "END-ON-NEXT", "SCTE35-CMD", "SCTE35-OUT", "SCTE35-IN"}

            ' There may be x-client-attributes or SCTE-35 data that would be nice to preserve if this
            ' tag were written to a file again.
            Dim UnparsedAttributes = attributes.GetRemainingAttributes(definedTags).ToList()

            playlist.AddDateRange(New DateRange(Id, ClassAttribute, StartDate, endDate, duration, plannedDuration,
                                                endOnNext, Scte35Cmd, Scte35Out, Scte35In, UnparsedAttributes))
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-DATERANGE"
        End Function
    End Class
End Namespace
