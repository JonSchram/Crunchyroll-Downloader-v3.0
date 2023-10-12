Imports Crunchyroll_Downloader.api.metadata
Imports Newtonsoft.Json.Linq
Namespace api.funimation.metadata
    Public Class FunimationEpisodeOverview
        Inherits EpisodeOverview

        Public Shared Function CreateFromJToken(Episode As JToken) As FunimationEpisodeOverview
            Dim Id = Episode.Item("id")
            Dim Slug = Episode.Item("slug")
            Dim ApiSlug = Episode.Item("venueId")
            Dim Number = Episode.Item("episodeNumber")
            Dim IsSubRequired = Episode.Item("isSubRequired")
            Dim Versions = Episode.Item("versions").Values(Of String).ToList()
            Dim EpisodeObject = New FunimationEpisodeOverview With {
                .EpisodeId = Id.Value(Of String),
                .EpisodeUrlSlug = Slug.Value(Of String),
                .ApiUrlSlug = ApiSlug.Value(Of String),
                .IsFree = Not IsSubRequired.Value(Of Boolean),
                .EpisodeNumber = Number.Value(Of String),
                .Versions = Versions
            }
            Return EpisodeObject
        End Function
    End Class
End Namespace