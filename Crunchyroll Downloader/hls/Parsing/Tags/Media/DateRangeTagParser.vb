Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.playlist

Namespace hls.parsing.tags.media
    Public Class DateRangeTagParser
        Inherits TagParser(Of MediaPlaylistBuilder)

        ' No plans to do anything with this tag, but including it to make sure the entire playlist is parsed

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As MediaPlaylistBuilder)

            Dim Id As String = attributes.GetAttribute("ID")
            Dim ClassAttribute As String = attributes.GetAttribute("CLASS")

            Dim startDateString As String = attributes.GetAttribute("START-DATE")
            If startDateString Is Nothing Then
                Throw New HlsFormatException($"Parse failure - {GetTagName()} requires a start date.")
            End If
            Dim StartDate As Date = Date.ParseExact(startDateString, "s", Globalization.CultureInfo.InvariantCulture)

            Dim EndDateString As String = attributes.GetAttribute("END-DATE")
            Dim endDate As Date = Nothing
            If EndDateString IsNot Nothing Then
                endDate = Date.ParseExact(EndDateString, "s", Globalization.CultureInfo.InvariantCulture)
            End If

            Dim DurationString As String = attributes.GetAttribute("DURATION")
            Dim duration As Decimal? = Nothing
            If DurationString IsNot Nothing Then
                duration = Decimal.Parse(DurationString)
            End If

            Dim PlannedDurationString As String = attributes.GetAttribute("PLANNED-DURATION")
            Dim plannedDuration As Decimal? = Nothing
            If PlannedDurationString IsNot Nothing Then
                plannedDuration = Decimal.Parse(PlannedDurationString)
            End If

            Dim EndOnNextString As String = attributes.GetAttribute("END-ON-NEXT")
            Dim endOnNext As Boolean = False
            If EndOnNextString IsNot Nothing Then
                If "YES".Equals(EndOnNextString, StringComparison.OrdinalIgnoreCase) Then
                    endOnNext = True
                Else
                    Throw New HlsFormatException($"Parse failure - in {GetTagName()}, if END-ON-NEXT is present, the value must be YES.")
                End If
            End If

            Dim Scte35Cmd As String = attributes.GetAttribute("SCTE35-CMD")
            Dim Scte35In As String = attributes.GetAttribute("SCTE35-IN")
            Dim Scte35Out As String = attributes.GetAttribute("SCTE35-OUT")

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
