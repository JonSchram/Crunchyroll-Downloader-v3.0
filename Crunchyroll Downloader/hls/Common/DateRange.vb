Imports Crunchyroll_Downloader.hls.parsing

Namespace hls.common
    Public Class DateRange
        Public Sub New(id As String, classAttributes As String, startDate As Date, endDate As Date?,
                       duration As Decimal?, plannedDuration As Decimal?, endOnNext As Boolean,
                       scte35Cmd As String, scte35Out As String, scte35In As String,
                       customAttributes As List(Of KeyValuePair(Of String, PlaylistData)))
            Me.Id = id
            Me.ClassAttributes = classAttributes
            Me.StartDate = startDate
            Me.EndDate = endDate
            Me.Duration = duration
            Me.PlannedDuration = plannedDuration
            Me.EndOnNext = endOnNext
            Me.Scte35Cmd = scte35Cmd
            Me.Scte35Out = scte35Out
            Me.Scte35In = scte35In
            Me.CustomAttributes = customAttributes
        End Sub

        Public ReadOnly Property Id As String

        Public ReadOnly Property ClassAttributes As String

        Public ReadOnly Property StartDate As Date

        Public ReadOnly Property EndDate As Date?

        Public ReadOnly Property Duration As Decimal?

        Public ReadOnly Property PlannedDuration As Decimal?

        Public ReadOnly Property EndOnNext As Boolean

        Public ReadOnly Property Scte35Cmd As String

        Public ReadOnly Property Scte35Out As String

        Public ReadOnly Property Scte35In As String

        Public ReadOnly Property CustomAttributes As List(Of KeyValuePair(Of String, PlaylistData))

    End Class
End Namespace