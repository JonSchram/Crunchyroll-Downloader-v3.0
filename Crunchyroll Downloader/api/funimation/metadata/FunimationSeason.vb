Imports Crunchyroll_Downloader.api.metadata
Imports Newtonsoft.Json.Linq

Namespace api.funimation.metadata
    Public Class FunimationSeason
        Inherits Season(Of FunimationEpisodeOverview)
        Public Property Id As Integer

        Public Property EpisodeCount As Integer

        Public Property Type As String

        Public Shared Function CreateFromJson(Json As String) As FunimationSeason
            Dim ResultSeason = New FunimationSeason()

            Dim SeasonObject = JObject.Parse(Json)
            Dim ShowObject = SeasonObject.Item("show")
            Dim NamesObject = ShowObject.Item("name")
            ResultSeason.Name = ExtractLanguageString(NamesObject)

            ResultSeason.Number = SeasonObject.Item("number").Value(Of Integer)
            ResultSeason.ApiID = SeasonObject.Item("id").Value(Of String)
            ResultSeason.Id = SeasonObject.Item("venueId").Value(Of Integer)
            ResultSeason.EpisodeCount = SeasonObject.Item("episodeCount").Value(Of Integer)
            ResultSeason.Type = SeasonObject.Item("type").Value(Of String)

            Dim EpisodeList = SeasonObject.Item("episodes").Children()
            ResultSeason.Episodes = New List(Of FunimationEpisodeOverview)
            For Each Episode In EpisodeList
                ResultSeason.Episodes.Add(FunimationEpisodeOverview.CreateFromJToken(Episode))
            Next
            Return ResultSeason
        End Function

        Private Shared Function ExtractLanguageString(LanguageObject As JToken) As String
            ' Prefer English, then Spanish, then Portuguese
            Dim languageList = {"en", "es", "pt"}
            For Each language In languageList
                Dim translation = LanguageObject.Item(language).Value(Of String)
                If translation IsNot Nothing And translation.Length > 0 Then
                    Return translation
                End If
            Next
            Return "UNDEFINED"
        End Function
    End Class
End Namespace