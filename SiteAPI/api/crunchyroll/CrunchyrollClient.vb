Imports System.Text
Imports Newtonsoft.Json.Linq
Imports SiteAPI.api
Imports SiteAPI.api.common
Imports SiteAPI.api.metadata

Namespace legacy
    Public Class CrunchyrollClient
        Implements IDownloadClient
        Public Sub FillCREpisodes(ByVal EpisodeJson As String)
            EpisodeJson = CleanJSON(EpisodeJson)
            'Main.CR_MassEpisodes.Clear()

            Dim EpisodeJObject As JObject = JObject.Parse(EpisodeJson)
            Dim EpisodeData As List(Of JToken) = EpisodeJObject.Children().ToList

            For Each item As JProperty In EpisodeData
                item.CreateReader()
                Select Case item.Name
                    Case "data" 'each record is inside the entries array
                        For Each Entry As JObject In item.Values
                            Dim episode_number As String = Entry.GetValue("episode_number").ToString
                            Dim episode_id As String = Entry.GetValue("id").ToString
                            Dim slug_title As String = Entry.GetValue("slug_title").ToString

                            'comboBox3.Items.Add("Episode " + episode_number)
                            'comboBox4.Items.Add("Episode " + episode_number)
                            'Main.CR_MassEpisodes.Add(New CR_Seasons(episode_id, slug_title, Main.CR_MassSeasons.Item(ComboBox1.SelectedIndex).Auth))
                        Next
                End Select
            Next

            ' TODO
            'If comboBox3.Items.Count > 0 Then
            '    comboBox3.SelectedIndex = 0
            '    comboBox4.SelectedIndex = comboBox4.Items.Count - 1
            'End If

            'comboBox3.Enabled = True
            'comboBox4.Enabled = True

        End Sub
        Private Function CleanJSON(ByVal JSON As String) As String
            JSON = JSON.Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
            While CBool(InStr(JSON, "\"))
                Dim index As Integer = InStr(JSON, "\")
                Dim myName As New StringBuilder(JSON)
                myName.Remove(index - 1, 2)
                JSON = myName.ToString
            End While
            Return JSON

        End Function

        Public Function Initialize() As Task Implements IDownloadClient.Initialize
            Throw New NotImplementedException()
        End Function

        Public Function ListSeasons(Url As String) As Task(Of IEnumerable(Of SeasonOverview)) Implements IDownloadClient.ListSeasons
            Throw New NotImplementedException()
        End Function

        Public Function ListEpisodes(Season As SeasonOverview) As Task(Of IEnumerable(Of EpisodeOverview)) Implements IDownloadClient.ListEpisodes
            Throw New NotImplementedException()
        End Function

        Public Function GetEpisodeInfo(Overview As EpisodeOverview) As Task(Of Episode) Implements IDownloadClient.GetEpisodeInfo
            Throw New NotImplementedException()
        End Function

        Public Function GetEpisodeInfo(Url As String) As Task(Of Episode) Implements IDownloadClient.GetEpisodeInfo
            Throw New NotImplementedException()
        End Function

        Public Function IsSeriesUrl(Url As String) As Boolean Implements IDownloadClient.IsSeriesUrl
            Throw New NotImplementedException()
        End Function

        Public Function IsVideoUrl(Url As String) As Boolean Implements IDownloadClient.IsVideoUrl
            Throw New NotImplementedException()
        End Function


        Public Function GetSiteName() As String Implements IDownloadClient.GetSiteName
            Return "Crunchyroll"
        End Function


        Public Function GetSite() As Site Implements IDownloadClient.GetSite
            Return Site.CRUNCHYROLL
        End Function


        Public Function ResolveMediaLink(link As MediaLink) As Task(Of Media) Implements IDownloadClient.ResolveMediaLink
            Throw New NotImplementedException()
        End Function

        Public Function GetAvailableMedia(ep As Episode, preferences As MediaPreferences) As Task(Of List(Of MediaLink)) Implements IDownloadClient.GetAvailableMedia
            Throw New NotImplementedException()
        End Function

    End Class
End Namespace