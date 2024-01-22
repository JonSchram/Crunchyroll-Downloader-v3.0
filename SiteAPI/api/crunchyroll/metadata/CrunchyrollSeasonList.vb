Imports Newtonsoft.Json.Linq
Imports SiteAPI.api.metadata

Namespace api.crunchyroll.metadata
    ''' <summary>
    ''' A list of all seasons available in a series. 
    ''' </summary>
    Public Class CrunchyrollSeasonList

        Public ReadOnly Property Seasons As List(Of CrunchyrollSeasonOverview)

        Public Sub New()
            Seasons = New List(Of CrunchyrollSeasonOverview)()
        End Sub

        Public Shared Function CreateFromSeriesJson(Json As String) As CrunchyrollSeasonList
            Dim series As New CrunchyrollSeasonList()

            Dim seriesInfo = JObject.Parse(Json)
            Dim seriesData = seriesInfo.Item("data")

            For Each season In seriesData
                series.Seasons.Add(CrunchyrollSeasonOverview.CreateFromJToken(season))
            Next

            Return series
        End Function

    End Class
End Namespace