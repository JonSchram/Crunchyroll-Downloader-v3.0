Imports Newtonsoft.Json.Linq
Imports SiteAPI.api.metadata

Namespace api.crunchyroll.metadata
    Public Class CrunchyrollEpisode
        Inherits Episode

        Public Property StreamLink As String


        Public Shared Function CreateFromJson(Json As String) As CrunchyrollEpisode
            Dim episodeInfo As JObject = JObject.Parse(Json)
            Dim episodeData = episodeInfo.Item("data").Item(0)

            Dim id = episodeData.Item("id").Value(Of String)
            Dim slugTitle = episodeData.Item("slug_title").Value(Of String)
            Dim episodeTitle = episodeData.Item("title").Value(Of String)
            Dim streams = episodeData.Item("streams_link")?.Value(Of String)
            Dim videoType = episodeData.Item("type").Value(Of String)
            Dim imageArray = episodeData.Item("images")
            Dim imageUrl = GetImageUrl(imageArray)


            Dim metadataObject = episodeData.Item("episode_metadata")
            Dim showTitle = metadataObject.Item("series_title").Value(Of String)
            Dim seasonNumber = metadataObject.Item("season_number").Value(Of Integer)
            Dim episodeNumber = metadataObject.Item("episode_number").Value(Of Double)
            Dim premiumOnly = metadataObject.Item("is_premium_only").Value(Of Boolean)


            Dim episode As New CrunchyrollEpisode With {
                .VideoId = id,
                .UrlSlug = slugTitle,
                .EpisodeName = episodeTitle,
                .EpisodeNumber = episodeNumber,
                .ShowName = showTitle,
                .SeasonNumber = seasonNumber,
                .StreamLink = streams,
                .IsFree = Not premiumOnly,
                .Type = videoType,
                .ImageUrl = imageUrl
            }

            Return episode
        End Function

        Private Shared Function GetImageUrl(imagesObject As JToken) As String
            Dim thumbnailArray = imagesObject.Item("thumbnail").Item(0)
            If thumbnailArray.HasValues Then
                ' The array seems to be sorted with smallest first. The smallest image would be just fine,
                ' but for now it's not too important to worry about.
                Dim entry = thumbnailArray.Children.ElementAt(0)
                Return entry.Item("source")
            End If
            Return ""
        End Function


    End Class
End Namespace