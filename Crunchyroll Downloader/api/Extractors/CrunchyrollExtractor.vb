Imports Newtonsoft.Json.Linq
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class CrunchyrollExtractor
    Public Sub FillCREpisodes(ByVal EpisodeJson As String)

        EpisodeJson = CleanJSON(EpisodeJson)
        Main.CR_MassEpisodes.Clear()

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
End Class
