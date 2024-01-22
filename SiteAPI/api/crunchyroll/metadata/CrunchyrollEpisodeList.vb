Imports Newtonsoft.Json.Linq
Imports SiteAPI.api.metadata

Namespace api.crunchyroll.metadata
    Public Class CrunchyrollEpisodeList

        Public ReadOnly Property Episodes As List(Of CrunchyrollEpisodeOverview)
        Public Sub New()
            Episodes = New List(Of CrunchyrollEpisodeOverview)()
        End Sub


        Public Shared Function CreateFromJson(Json As String) As CrunchyrollEpisodeList
            Dim jsonObject = JObject.Parse(Json)
            Dim episodeData = jsonObject.Item("data")

            Dim result = New CrunchyrollEpisodeList()
            For Each episode In episodeData
                result.Episodes.Add(CrunchyrollEpisodeOverview.CreateFromJToken(episode))
            Next

            Return result
        End Function
    End Class
End Namespace