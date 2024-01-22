Imports Newtonsoft.Json.Linq
Imports SiteAPI.api.metadata

Namespace api.funimation.metadata
    Public Class FunimationSeries
        Inherits Series(Of FunimationSeasonOverview)

        Public Sub New()
            Seasons = New List(Of FunimationSeasonOverview)
        End Sub

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
End Namespace