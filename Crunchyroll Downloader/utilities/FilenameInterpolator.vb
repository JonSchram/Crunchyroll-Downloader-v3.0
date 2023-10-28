Imports SiteAPI.api.metadata

Namespace utilities
    Public Class FilenameInterpolator
        Private ReadOnly Template As String

        Public Sub New(template As String)
            Me.Template = template
        End Sub

        Public Shared Function CreateKodiNamingInstance() As FilenameInterpolator
            ' TODO: Name template parameters are not shared in any way and are prone to error.
            Return New FilenameInterpolator("AnimeTitle; SSeason;EEpisodeNR; EpisodeName;")
        End Function

        Public Function CreateName(ep As Episode, appendLanguage As Boolean) As String
            Dim result = Template
            result = result.Replace("AnimeTitle;", ep.ShowName)
            result = result.Replace("Season;", ep.SeasonNumber.ToString())
            result = result.Replace("EpisodeNR;", ep.EpisodeNumber.ToString())
            result = result.Replace("EpisodeName;", ep.EpisodeName)
            result = result.Replace("AnimeDub;", "(TODO - Language)")

            If appendLanguage Then
                ' TODO: Add language to episode for naming.
                result += ".(TODO - Language)"
            End If
            Return result
        End Function

    End Class
End Namespace