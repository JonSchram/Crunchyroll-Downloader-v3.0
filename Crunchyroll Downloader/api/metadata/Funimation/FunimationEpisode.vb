Imports Crunchyroll_Downloader.api
Imports Newtonsoft.Json.Linq

Public Class FunimationEpisode
    Inherits Episode

    Public Shared Function CreateFromJson(Json As String) As FunimationEpisode
        Dim episodeInfo As JObject = JObject.Parse(Json)

        Dim Id = episodeInfo.Item("id")
        Dim slug = episodeInfo.Item("slug")
        Dim apiId = episodeInfo.Item("venueId")
        Dim episodeNumber = episodeInfo.Item("episodeNumber")
        Dim subRequired = episodeInfo.Item("isSubRequired")
        Dim type = episodeInfo.Item("type")
        Dim EpisodeName = ExtractEpisodeName(episodeInfo.Item("name"))

        Dim SeasonInfo = episodeInfo.Item("season")
        Dim seasonNumber = SeasonInfo.Item("number")

        Dim ShowInfo = episodeInfo.Item("show")
        Dim showName = extractShowName(ShowInfo.Item("name"))


        Dim imagesList = episodeInfo.Item("images")
        Dim imageUrl = extractEpisodeImageUrl(imagesList.ToList)

        Dim Episode As New FunimationEpisode With {
            .VideoId = Id.Value(Of String),
            .ApiId = apiId.Value(Of Integer),
            .UrlSlug = slug.Value(Of String),
            .EpisodeName = EpisodeName,
            .EpisodeNumber = episodeNumber.Value(Of Double),
            .SeasonNumber = seasonNumber.Value(Of Integer),
            .ShowName = showName,
            .ImageUrl = imageUrl,
            .IsFree = Not subRequired.Value(Of Boolean),
            .Type = type.Value(Of String)
        }

        Return Episode
    End Function

    Private Shared Function extractShowName(nameObject As JToken) As String
        Return ExtractLanguage(nameObject, "Series title")
    End Function

    Private Shared Function ExtractEpisodeName(nameObject As JToken) As String
        Return ExtractLanguage(nameObject, "Episode name")
    End Function

    Private Shared Function ExtractLanguage(apiObject As JToken, defaultText As String) As String
        ' Prefer English, then Spanish, then Portuguese
        Dim languageList = {"en", "es", "pt"}
        For Each language In languageList
            Dim title = apiObject.Item(language).Value(Of String)
            If title IsNot Nothing And title.Length > 0 Then
                Return title
            End If
        Next
        Return defaultText
    End Function

    Private Shared Function extractEpisodeImageUrl(imageList As IEnumerable(Of JToken)) As String
        ' TODO Might want to more intelligently select the episode image than to take the first one that matches
        For Each image As JToken In imageList
            Dim key As String = image("key").Value(Of String)
            If key = "Key Art - Official Video Image" Or key = "Episode Thumbnail" Then
                Dim path As String = image("path").Value(Of String)
                Return path
            End If
        Next
        Return ""
    End Function

    Public Overrides Function ToString() As String
        ' Display extra information indicating where the episode is from
        Return MyBase.ToString() + " (Funimation)"
    End Function
End Class
