Imports System.Collections.Specialized
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

        ' TODO:
        ' Upgrade My.settings.Subtitle to CrunchyrollHardSubLanguage
        ' Upgrade My.settings.AddedSubs to SelectedCrunchyrollSoftSubs
        '   - Is a list of locales
        ' Upgrade My.settings.CR_Dub to CrunchyrollDubLanguage
        ' Upgrade My.settings.DubMode to CrunchyrollAcceptHardsubs

        Public Property AcceptHardsubs As Boolean
            Get
                Return My.Settings.CrunchyrollAcceptHardsubs
            End Get
            Set(value As Boolean)
                My.Settings.CrunchyrollAcceptHardsubs = value
            End Set
        End Property
        Public Property AudioLanguage As CrunchyrollLanguage
            Get
                Return CType(My.Settings.CrunchyrollDubLanguage, CrunchyrollLanguage)
            End Get
            Set(value As CrunchyrollLanguage)
                My.Settings.CrunchyrollDubLanguage = value
            End Set
        End Property
        Public Property HardSubLanguage As CrunchyrollLanguage
            Get
                Return CType(My.Settings.CrunchyrollHardSubLanguage, CrunchyrollLanguage)
            End Get
            Set(value As CrunchyrollLanguage)
                My.Settings.CrunchyrollHardSubLanguage = value
            End Set
        End Property

        ' TODO: Might want this to accept / return a Set because that is how it is used in code.
        Public Property SoftSubLanguages As List(Of CrunchyrollLanguage)
            Get
                Dim subCollection = My.Settings.SelectedCrunchyrollSoftSubs
                Dim subList As New List(Of CrunchyrollLanguage)

                If subCollection IsNot Nothing Then
                    For Each item In subCollection
                        Dim parsedItem As CrunchyrollLanguage
                        If System.Enum.TryParse(item, parsedItem) Then
                            subList.Add(parsedItem)
                        End If
                    Next
                End If

                Return subList
            End Get
            Set(value As List(Of CrunchyrollLanguage))
                Dim subCollection = New StringCollection()
                For Each item As CrunchyrollLanguage In value
                    subCollection.Add(item.ToString())
                Next

                My.Settings.SelectedCrunchyrollSoftSubs = subCollection
            End Set
        End Property

        Public Property DefaultSoftSubLanguage As CrunchyrollLanguage
            Get
                Return CType(My.Settings.DefaultCrunchyrollSoftSub, CrunchyrollLanguage)
            End Get
            Set(value As CrunchyrollLanguage)
                My.Settings.DefaultCrunchyrollSoftSub = value
            End Set
        End Property

        Public Property EnableChapters As Boolean

        ' TODO: allow getting locale name from enum value
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

        Public Enum CrunchyrollLanguage As Integer
            NONE = 0
            JAPANESE = 1
            SPANISH_SPAIN = 2
            ITALIAN = 3
            RUSSIAN = 4
            ARABIC = 5
            FRENCH_FRANCE = 6
            SPANISH_LATIN_AMERICA = 7
            PORTUGUESE_BRAZIL = 8
            ENGLISH_US = 9
            GERMAN_GERMANY = 10
        End Enum
    End Class
End Namespace
