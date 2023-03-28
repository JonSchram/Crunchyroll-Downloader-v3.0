Imports System.Collections.Specialized
Imports Crunchyroll_Downloader.settings.ProgramSettings

Namespace settings
    Public Class CrunchyrollSettings
        Public Shared Instance As CrunchyrollSettings = Nothing

        Public Sub New()

        End Sub

        Friend Sub UpgradeSettings()
            UpgradeHardsubs()
            UpgradeSoftSubs()
            UpgradeDub()
            UpgradeAcceptHardsub()
            UpgradeDefaultSub()
        End Sub

        Private Sub UpgradeHardsubs()
            Try
                Dim oldHardsub As String = CStr(My.Settings.GetPreviousVersion("Subtitle"))
                Select Case oldHardsub
                    Case "[ null ]"
                        HardSubLanguage = CrunchyrollLanguage.NONE
                    Case "Deutsch"
                        HardSubLanguage = CrunchyrollLanguage.GERMAN_GERMANY
                    Case "English"
                        HardSubLanguage = CrunchyrollLanguage.ENGLISH_US
                    Case "Português (Brasil)"
                        HardSubLanguage = CrunchyrollLanguage.PORTUGUESE_BRAZIL
                    Case "Español (LA)"
                        HardSubLanguage = CrunchyrollLanguage.SPANISH_LATIN_AMERICA
                    Case "Français (France)"
                        HardSubLanguage = CrunchyrollLanguage.FRENCH_FRANCE
                    Case "العربية (Arabic)"
                        HardSubLanguage = CrunchyrollLanguage.ARABIC
                    Case "Русский (Russian)"
                        HardSubLanguage = CrunchyrollLanguage.RUSSIAN
                    Case "Italiano (Italian)"
                        HardSubLanguage = CrunchyrollLanguage.ITALIAN
                    Case "Español (España)"
                        HardSubLanguage = CrunchyrollLanguage.SPANISH_SPAIN
                    Case "Japanese"
                        HardSubLanguage = CrunchyrollLanguage.JAPANESE
                End Select
            Catch ex As Exception
                HardSubLanguage = CrunchyrollLanguage.NONE
            End Try
        End Sub

        Private Sub UpgradeSoftSubs()
            Dim newSoftSubs = New List(Of CrunchyrollLanguage)
            Try
                Dim oldSoftSubs = CStr(My.Settings.GetPreviousVersion("AddedSubs"))
                Dim oldSubsArray = oldSoftSubs.Split(New Char() {","c})
                For Each item In oldSubsArray
                    Dim language = LocaleToLanguage(item)
                    If language <> CrunchyrollLanguage.NONE Then
                        newSoftSubs.Add(language)
                    End If
                Next
                SoftSubLanguages = newSoftSubs
            Catch ex As Exception
                SoftSubLanguages = newSoftSubs
            End Try
        End Sub

        Private Sub UpgradeDub()
            Try
                Dim oldDub As String = CStr(My.Settings.GetPreviousVersion("CR_Dub"))
                Dim language = LocaleToLanguage(oldDub)
                AudioLanguage = language
            Catch ex As Exception
                AudioLanguage = CrunchyrollLanguage.NONE
            End Try
        End Sub

        Private Sub UpgradeAcceptHardsub()
            Try
                Dim oldHardsub As Boolean = CBool(My.Settings.GetPreviousVersion("DubMode"))
                AcceptHardsubs = oldHardsub
            Catch ex As Exception
                AcceptHardsubs = False
            End Try
        End Sub

        Private Sub UpgradeDefaultSub()
            Try
                Dim oldDefaultSub = CStr(My.Settings.GetPreviousVersion("DefaultSubCR"))
                Dim newDefault = LocaleToLanguage(oldDefaultSub)
                DefaultSoftSubLanguage = newDefault
            Catch ex As Exception
                DefaultSoftSubLanguage = CrunchyrollLanguage.NONE
            End Try
        End Sub

        Public Shared Function GetInstance() As CrunchyrollSettings
            If Instance Is Nothing Then
                Instance = New CrunchyrollSettings()
            End If
            Return Instance
        End Function

        Private Function LocaleToLanguage(locale As String) As CrunchyrollLanguage
            Select Case locale
                Case "None"
                    Return CrunchyrollLanguage.NONE
                Case "de-DE"
                    Return CrunchyrollLanguage.GERMAN_GERMANY
                Case "en-US"
                    Return CrunchyrollLanguage.ENGLISH_US
                Case "pt-BR"
                    Return CrunchyrollLanguage.PORTUGUESE_BRAZIL
                Case "es-419"
                    Return CrunchyrollLanguage.SPANISH_LATIN_AMERICA
                Case "fr-FR"
                    Return CrunchyrollLanguage.FRENCH_FRANCE
                Case "ar-SA"
                    Return CrunchyrollLanguage.ARABIC
                Case "ru-RU"
                    Return CrunchyrollLanguage.RUSSIAN
                Case "it-IT"
                    Return CrunchyrollLanguage.ITALIAN
                Case "es-ES"
                    Return CrunchyrollLanguage.SPANISH_SPAIN
                Case "ja-JP"
                    Return CrunchyrollLanguage.JAPANESE
            End Select
            Return CrunchyrollLanguage.NONE
        End Function

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
                Return CType(My.Settings.CrunchyrollDefaultSoftSub, CrunchyrollLanguage)
            End Get
            Set(value As CrunchyrollLanguage)
                My.Settings.CrunchyrollDefaultSoftSub = value
            End Set
        End Property

        Public Property EnableChapters As Boolean
            Get
                Return My.Settings.CR_Chapters
            End Get
            Set(value As Boolean)
                My.Settings.CR_Chapters = value
            End Set
        End Property

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
