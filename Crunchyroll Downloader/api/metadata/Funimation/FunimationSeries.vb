Imports Newtonsoft.Json.Linq

Public Class FunimationSeries
    ' TODO: Refactor this class into a generic Series and make the Seasons list be of the generic SeasonOverview type.
    Public Property Seasons As List(Of FunimationSeasonOverview) = New List(Of FunimationSeasonOverview)


    ' Human-readable name for series
    Public Property Name As String

    Public Function GetSeasons() As IEnumerable(Of FunimationSeasonOverview)
        Return Seasons.AsEnumerable()
    End Function

    Public Shared Function CreateFromJson(Json As String) As FunimationSeries
        Dim Series As New FunimationSeries()

        Dim SeriesInfo = JObject.Parse(Json)

        Dim SeriesName As String = SeriesInfo.Item("name").Value(Of String)
        Series.Name = SeriesName

        Dim SeasonsList = SeriesInfo.Item("seasons").Children()
        For Each Season In SeasonsList
            Dim Name = Season.Item("name")
            Dim Id = Season.Item("id")
            Dim Type = Season.Item("type")
            Dim Number = Season.Item("number")
            Dim EntitledSeason = Season.Item("entitledSeason")
            Dim ContentId = Season.Item("contentId")
            Dim SeasonObject = New FunimationSeasonOverview With {
                    .Name = Name.Value(Of String),
                    .Id = Id.Value(Of Integer),
                    .ApiID = ContentId.Value(Of String),
                    .Type = Type.Value(Of String),
                    .Number = Number.Value(Of Integer),
                    .IsFree = EntitledSeason.Value(Of Boolean)
                }
            Series.Seasons.Add(SeasonObject)
        Next
        Return Series
    End Function
End Class
