Imports Crunchyroll_Downloader.settings.ProgramSettings

Namespace settings
    Public Class CrunchyrollSettings
        Public Shared Instance As CrunchyrollSettings = Nothing

        Public Sub New()

        End Sub

        Public Shared Function GetInstance() As CrunchyrollSettings
            If Instance Is Nothing Then
                Instance = New CrunchyrollSettings()
            End If
            Return Instance
        End Function

        Public Property AcceptHardsubs As Boolean
        Public Property AudioLanguage As CrunchyrollLanguage
        Public Property HardSubLanguage As CrunchyrollLanguage
        Public Property SoftSubLanguage As List(Of CrunchyrollLanguage)

        Public Property DefaultSoftSubLanguage As CrunchyrollLanguage

        Public Property EnableChapters As Boolean

        ' TODO: Add display names from here:
        'LangValueEnum.Add(New NameValuePair("[ null ]", "", Nothing))
        'LangValueEnum.Add(New NameValuePair("Deutsch", "de-DE", Nothing))
        'LangValueEnum.Add(New NameValuePair("English", "en-US", "en"))
        'LangValueEnum.Add(New NameValuePair("Português (Brasil)", "pt-BR", "pt"))
        'LangValueEnum.Add(New NameValuePair("Español (LA)", "es-419", "es"))
        'LangValueEnum.Add(New NameValuePair("Français (France)", "fr-FR", Nothing))
        'LangValueEnum.Add(New NameValuePair("العربية (Arabic)", "ar-SA", Nothing))
        'LangValueEnum.Add(New NameValuePair("Русский (Russian)", "ru-RU", Nothing))
        'LangValueEnum.Add(New NameValuePair("Italiano (Italian)", "it-IT", Nothing))
        'LangValueEnum.Add(New NameValuePair("Español (España)", "es-ES", Nothing))
        'LangValueEnum.Add(New NameValuePair("Japanese", "ja-JP", Nothing))

        Public Enum CrunchyrollLanguage
            NONE
            JAPANESE
            SPANISH_SPAIN
            ITALIAN
            RUSSIAN
            ARABIC
            FRENCH_FRANCE
            SPANISH_LATIN_AMERICA
            PORTUGUESE_BRAZIL
            ENGLISH_US
            GERMAN_GERMANY
        End Enum
    End Class
End Namespace
