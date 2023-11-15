Imports Crunchyroll_Downloader.settings.general
Imports SiteAPI.api
Imports SiteAPI.api.metadata

Namespace utilities
    ''' <summary>
    ''' Converts a name template into a filename using episode information.
    ''' File name template contains field names in braces, with an optional prefix. Episode information is subsituted for field template markers,
    ''' unless the field name is invalid. In this case, the field is ignored and treated just like normal text.
    ''' </summary>
    Public Class FilenameInterpolator
        Public Shared ReadOnly SERIES_NAME_FIELD As String = "SeriesName"
        Public Shared ReadOnly SEASON_NUMBER_FIELD As String = "SeasonNumber"
        Public Shared ReadOnly EPISODE_NUMBER_FIELD As String = "EpisodeNumber"
        Public Shared ReadOnly EPISODE_TITLE_FIELD As String = "EpisodeName"
        Public Shared ReadOnly AUDIO_LANGUAGE_FIELD As String = "AudioLanguage"

        Private Shared ReadOnly AllFields As New HashSet(Of String) From {
            SERIES_NAME_FIELD,
            SEASON_NUMBER_FIELD,
            EPISODE_NUMBER_FIELD,
            EPISODE_TITLE_FIELD,
            AUDIO_LANGUAGE_FIELD
        }


        Private ReadOnly Template As String
        Private ReadOnly SeasonPadding As Integer
        Private ReadOnly EpisodePadding As Integer
        Private ReadOnly UseIsoLanguageName As Boolean
        Private ReadOnly SeasonNumbering As SeasonNumberBehavior

        Public Sub New(template As String)
            Me.New(template, 1, 1, False, SeasonNumberBehavior.USE_SEASON_NUMBERS)
        End Sub

        Public Sub New(template As String,
                SeasonPadding As Integer,
                EpisodePadding As Integer,
                UseIsoLanguageName As Boolean,
                useSeasons As SeasonNumberBehavior)
            Me.Template = template
            Me.SeasonPadding = Math.Max(1, SeasonPadding)
            Me.EpisodePadding = Math.Max(1, EpisodePadding)
            Me.UseIsoLanguageName = UseIsoLanguageName
            SeasonNumbering = useSeasons
        End Sub

        Public Shared Function CreateKodiNamingInstance() As FilenameInterpolator
            Return New FilenameInterpolator(CreateKodiNamingTemplate(), 2, 2, True, SeasonNumberBehavior.USE_SEASON_NUMBERS)
        End Function

        Public Shared Function CreateKodiNamingTemplate() As String
            Dim seriesNameField As String = GetFieldTemplate(TemplateItem.SERIES_NAME)
            Dim seasonNumberField As String = GetFieldTemplate(TemplateItem.SEASON_NUMBER, "S")
            Dim episodeNumberField As String = GetFieldTemplate(TemplateItem.EPISODE_NUMBER, "E")
            Dim episodeNameField As String = GetFieldTemplate(TemplateItem.EPISODE_TITLE)
            Return $"{seriesNameField} {seasonNumberField}{episodeNumberField} {episodeNameField}"
        End Function

        Public Function CreateName(ep As Episode, locale As Locale) As String
            Dim result As String = Template

            Dim searchStart As Integer = 0
            Dim replacedAllFields As Boolean = False
            Dim fieldStart = result.IndexOf("{"c, searchStart)
            While fieldStart >= 0 And Not replacedAllFields
                Dim fieldEnd = result.IndexOf("}"c, fieldStart)

                If fieldEnd > fieldStart Then
                    Dim fieldContents As String = result.Substring(fieldStart + 1, fieldEnd - fieldStart - 1)
                    Dim interpolatedField As String = InterpolateField(fieldContents, ep, locale)
                    Dim resultPrefix As String = result.Substring(0, fieldStart)
                    result = resultPrefix + interpolatedField + result.Substring(fieldEnd + 1)
                    ' Ensure we never try to interpolate a name from the result of an interpolation.
                    searchStart = resultPrefix.Length + interpolatedField.Length
                    fieldStart = result.IndexOf("{"c, searchStart)
                Else
                    ' No more templates can be found
                    replacedAllFields = True
                End If
            End While

            Return result
        End Function

        Private Function InterpolateField(field As String, ep As Episode, l As Locale) As String
            If field.Contains(":"c) Then
                ' Contains prefix.
                Dim fieldParts As String() = field.Split(":"c)
                If fieldParts.Length = 2 Then
                    Return InterpolateWithPrefix(fieldParts(1), fieldParts(0), ep, l)
                Else
                    ' Too many or too few, cannot interpolate
                    Return $"{{{field}}}"
                End If
            Else
                Return InterpolateWithPrefix(field, "", ep, l)
            End If
        End Function

        Private Function InterpolateWithPrefix(field As String, prefix As String, ep As Episode, l As Locale) As String
            If IsValidField(field) Then
                If field = SEASON_NUMBER_FIELD Then
                    Return FormatSeasonField(ep.SeasonNumber, prefix)
                Else
                    Return $"{prefix}{GetFieldValue(field, ep, l)}"
                End If
            Else
                Return $"{{{field}}}"
            End If
        End Function

        Private Function GetFieldValue(field As String, ep As Episode, l As Locale) As String
            Select Case field
                Case SERIES_NAME_FIELD
                    Return ep.ShowName
                Case EPISODE_NUMBER_FIELD
                    Return PadNumber(EpisodePadding, ep.EpisodeNumber)
                Case EPISODE_TITLE_FIELD
                    Return ep.EpisodeName
                Case AUDIO_LANGUAGE_FIELD
                    If UseIsoLanguageName Then
                        Return l.GetAbbreviatedString()
                    Else
                        Return l.ToString()
                    End If
                Case Else
                    ' Season number is already handled.
                    Return ""
            End Select
        End Function

        Private Function FormatSeasonField(seasonNumber As Integer, prefix As String) As String
            Select Case SeasonNumbering
                Case SeasonNumberBehavior.USE_SEASON_NUMBERS
                    Return prefix & PadNumber(SeasonPadding, seasonNumber)
                Case SeasonNumberBehavior.IGNORE_SEASON_1
                    If seasonNumber = 1 Then
                        Return ""
                    Else
                        Return prefix & PadNumber(SeasonPadding, seasonNumber)
                    End If
                Case SeasonNumberBehavior.IGNORE_ALL_SEASON_NUMBERS
                    Return ""
                Case Else
                    ' If this happens, the season numbering enum has changed and this code hasn't been updated.
                    ' Default to normal formatting
                    Return prefix & PadNumber(SeasonPadding, seasonNumber)
            End Select
        End Function

        Private Function IsValidField(field As String) As Boolean
            Return AllFields.Contains(field)
        End Function


        Public Shared Function GetFieldTemplate(field As TemplateItem) As String
            Return $"{{{GetNameFragment(field)}}}"
        End Function

        Public Shared Function GetFieldTemplate(field As TemplateItem, prefix As String) As String
            Return $"{{{prefix}:{GetNameFragment(field)}}}"
        End Function


        Private Shared Function GetNameFragment(item As TemplateItem) As String
            Select Case item
                Case TemplateItem.SERIES_NAME
                    Return SERIES_NAME_FIELD
                Case TemplateItem.SEASON_NUMBER
                    Return SEASON_NUMBER_FIELD
                Case TemplateItem.EPISODE_NUMBER
                    Return EPISODE_NUMBER_FIELD
                Case TemplateItem.EPISODE_TITLE
                    Return EPISODE_TITLE_FIELD
                Case TemplateItem.AUDIO_LANGUAGE
                    Return AUDIO_LANGUAGE_FIELD
                Case Else
                    Return ""
            End Select
        End Function

    End Class
End Namespace