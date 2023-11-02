Imports SiteAPI.api.metadata

Namespace utilities
    Public Class FilenameInterpolator
        Private ReadOnly Template As String
        Private ReadOnly SeasonPadding As Integer
        Private ReadOnly EpisodePadding As Integer

        Public Sub New(template As String)
            Me.New(template, 1, 1)
        End Sub

        Public Sub New(template As String, SeasonPadding As Integer, EpisodePadding As Integer)
            Me.Template = template
            Me.SeasonPadding = Math.Max(1, SeasonPadding)
            Me.EpisodePadding = Math.Max(1, EpisodePadding)
        End Sub

        Public Shared Function CreateKodiNamingInstance() As FilenameInterpolator
            Return New FilenameInterpolator(CreateKodiNamingTemplate(), 2, 2)
        End Function

        Public Shared Function CreateKodiNamingTemplate() As String
            Return "AnimeTitle; SSeason;EEpisodeNR; EpisodeName;"
        End Function

        Public Function CreateName(ep As Episode, appendLanguage As Boolean) As String
            Dim result = Template
            ' TODO: Name template parameters are not shared with template generation in any way and are prone to error.
            result = result.Replace("AnimeTitle;", ep.ShowName)
            result = result.Replace("Season;", PadNumber(SeasonPadding, ep.SeasonNumber))
            result = result.Replace("EpisodeNR;", PadNumber(EpisodePadding, ep.EpisodeNumber))
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