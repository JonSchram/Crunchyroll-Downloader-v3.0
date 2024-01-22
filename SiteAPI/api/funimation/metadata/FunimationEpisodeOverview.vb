Imports Newtonsoft.Json.Linq
Imports SiteAPI.api.metadata

Namespace api.funimation.metadata
    Public Class FunimationEpisodeOverview
        Inherits EpisodeOverview


        ''' <summary>
        ''' A slug to add to the URL to get the web viewer for this episode
        ''' </summary>
        ''' <returns></returns>
        Public Property EpisodeUrlSlug As String

        ''' <summary>
        ''' Versions of an episode, i.e. simulcast or uncensored version.
        ''' </summary>
        ''' <returns></returns>
        Public Property Versions As List(Of String)

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