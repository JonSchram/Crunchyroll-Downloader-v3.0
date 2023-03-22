Namespace settings
    Public Class FilenameFormatter
        Private FilenameTemplate As String

        Public Sub New()
            Me.New("")
        End Sub

        Public Sub New(template As String)
            FilenameTemplate = template
        End Sub

        ' TODO: Make filename template a little more intuitive to use.
        Public Sub AppendTemplateItem(item As TemplateItem)
            FilenameTemplate += GetNameFragment(item)
        End Sub

        Public Sub RemoveTemplateItem(item As TemplateItem)
            FilenameTemplate.Replace(GetNameFragment(item), "")
        End Sub

        Private Function GetNameFragment(item As TemplateItem) As String
            Select Case item
                Case TemplateItem.SERIES_NAME
                    Return "AnimeTitle;"
                Case TemplateItem.SEASON_NUMBER
                    Return "Season;"
                Case TemplateItem.EPISODE_NUMBER
                    Return "EpisodeNR;"
                Case TemplateItem.EPISODE_TITLE
                    Return "EpisodeName;"
                Case TemplateItem.AUDIO_LANGUAGE
                    Return "AnimeDub;"
                Case Else
                    Return ""
            End Select
        End Function

        Public Enum TemplateItem
            SERIES_NAME
            SEASON_NUMBER
            EPISODE_NUMBER
            EPISODE_TITLE
            AUDIO_LANGUAGE
        End Enum
    End Class
End Namespace