Imports Newtonsoft.Json.Linq
Imports SiteAPI.api.metadata

Public Class CrunchyrollSeasonOverview
    Inherits SeasonOverview

    Public Property SeriesId As String


    Public Shared Function CreateFromJToken(Season As JToken) As CrunchyrollSeasonOverview

        Dim name = Season.Item("title")
        Dim id = Season.Item("id")
        Dim seasonNumber = Season.Item("season_number")
        Dim seriesId = Season.Item("series_id")

        Dim seasonObject = New CrunchyrollSeasonOverview With {
            .Name = name.Value(Of String),
            .ApiID = id.Value(Of String),
            .Number = seasonNumber.Value(Of Integer),
            .SeriesId = seriesId.Value(Of String)
        }

        Return seasonObject
    End Function


End Class
