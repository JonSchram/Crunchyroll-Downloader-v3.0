Imports Newtonsoft.Json.Linq
Imports SiteAPI.api.metadata

Namespace api.crunchyroll.metadata
    Public Class CrunchyrollEpisodeOverview
        Inherits EpisodeOverview

        Public Property SeriesId As String

        Public Shared Function CreateFromJToken(Episode As JToken) As CrunchyrollEpisodeOverview
            Dim id = Episode.Item("id")
            Dim number = Episode.Item("episode")
            Dim isPremium = Episode.Item("is_premium_only")
            Dim seriesId = Episode.Item("series_id")

            Dim episodeObject As New CrunchyrollEpisodeOverview With {
                .EpisodeId = id.Value(Of String),
                .EpisodeNumber = number.Value(Of String),
                .ApiUrlSlug = id.Value(Of String),
                .IsFree = Not isPremium.Value(Of Boolean),
                .SeriesId = seriesId.Value(Of String)
            }
            Return episodeObject
        End Function

    End Class
End Namespace
