Imports Newtonsoft.Json.Linq
Imports SiteAPI.api.metadata

Namespace api.crunchyroll.metadata
    ''' <summary>
    ''' A list of all seasons available in a series. 
    ''' </summary>
    Public Class CrunchyrollSeries
        Inherits Series(Of CrunchyrollSeasonOverview)

        Public Sub New()
            Seasons = New List(Of CrunchyrollSeasonOverview)()
        End Sub

        Public Shared Function CreateFromSeriesJson(Json As String, seriesName As String) As CrunchyrollSeries
            Dim series As New CrunchyrollSeries()
            series.Name = seriesName

            Dim seriesInfo = JObject.Parse(Json)
            Dim seriesData = seriesInfo.Item("data")

            For Each season In seriesData
                Dim name = season.Item("title")
                Dim id = season.Item("id")
                Dim seasonNumber = season.Item("season_number")
                Dim seriesId = season.Item("series_id")

                Dim seasonObject = New CrunchyrollSeasonOverview With {
                    .Name = name.Value(Of String),
                    .ApiID = id.Value(Of String),
                    .Number = seasonNumber.Value(Of Integer),
                    .SeriesId = seriesId.Value(Of String)
                }

                series.Seasons.Add(seasonObject)
            Next

            Return series
        End Function

    End Class
End Namespace