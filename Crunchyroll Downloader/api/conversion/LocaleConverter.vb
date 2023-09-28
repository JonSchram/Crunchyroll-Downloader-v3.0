Imports Crunchyroll_Downloader.processing
Imports Crunchyroll_Downloader.settings.funimation

Namespace api
    Public Class LocaleConverter

        Public Shared Function ConvertFunimationLanguageToLocale(locale As FunimationLanguage) As String
            Select Case locale
                Case FunimationLanguage.JAPANESE
                    Return "japanese"
                Case FunimationLanguage.ENGLISH
                    Return "english"
                Case FunimationLanguage.PORTUGUESE
                    Return "portuguese(Brazil)"
                Case FunimationLanguage.SPANISH
                    Return "spanish(Mexico)"
                Case Else
                    Return "N/A"
            End Select
        End Function

        Public Shared Function ConvertFunimationLanguageCodeToLanguage(language As String) As FunimationLanguage
            Select Case language
                Case "en"
                    Return FunimationLanguage.ENGLISH
                Case "es"
                    Return FunimationLanguage.SPANISH
                Case "pt"
                    Return FunimationLanguage.PORTUGUESE
                Case "ja"
                    Return FunimationLanguage.JAPANESE
                Case "zh-mn"
                    Return FunimationLanguage.CHINESE_MANDARIN
                Case "N/A"
                    Return FunimationLanguage.NONE
                Case Else
                    Return FunimationLanguage.NONE
            End Select
        End Function

        Public Shared Function ConvertFunimationLangaugeToLanguageCode(language As FunimationLanguage) As String
            Select Case language
                Case FunimationLanguage.ENGLISH
                    Return "en"
                Case FunimationLanguage.SPANISH
                    Return "es"
                Case FunimationLanguage.JAPANESE
                    Return "ja"
                Case FunimationLanguage.PORTUGUESE
                    Return "pt"
                Case FunimationLanguage.CHINESE_MANDARIN
                    Return "zh-mn"
                Case FunimationLanguage.NONE
                    Return "N/A"
                Case Else
                    Return "N/A"
            End Select
        End Function

        Public Shared Function ConvertLanguageToFunimationLanguage(lang As Language) As FunimationLanguage
            Select Case lang
                Case Language.ENGLISH
                    Return FunimationLanguage.ENGLISH
                Case Language.CHINESE
                    Return FunimationLanguage.CHINESE_MANDARIN
                Case Language.JAPANESE
                    Return FunimationLanguage.JAPANESE
                Case Language.PORTUGUESE
                    Return FunimationLanguage.PORTUGUESE
                Case Language.SPANISH_LATIN_AMERICA, Language.SPANISH_SPAIN
                    Return FunimationLanguage.SPANISH
                Case Else
                    Return FunimationLanguage.NONE
            End Select
        End Function

        Public Shared Function ListContainsFunimationLanguage(testLanguage As FunimationLanguage,
                                                              testSet As List(Of Language)) As Boolean
            Select Case testLanguage
                Case FunimationLanguage.CHINESE_MANDARIN
                    Return testSet.Contains(Language.CHINESE)
                Case FunimationLanguage.ENGLISH
                    Return testSet.Contains(Language.ENGLISH)
                Case FunimationLanguage.JAPANESE
                    Return testSet.Contains(Language.JAPANESE)
                Case FunimationLanguage.PORTUGUESE
                    Return testSet.Contains(Language.PORTUGUESE)
                Case FunimationLanguage.SPANISH
                    Return testSet.Contains(Language.SPANISH_LATIN_AMERICA) Or testSet.Contains(Language.SPANISH_SPAIN)
                Case Else
                    ' If no langauge is requested, then technically anything is allowed?
                    ' There should always be a language though.
                    Return True
            End Select
        End Function

        Public Shared Function ConvertFunimationLanguageToLanguage(lang As FunimationLanguage) As Language
            Select Case lang
                Case FunimationLanguage.ENGLISH
                    Return Language.ENGLISH
                Case FunimationLanguage.JAPANESE
                    Return Language.JAPANESE
                Case FunimationLanguage.CHINESE_MANDARIN
                    Return Language.CHINESE
                Case FunimationLanguage.PORTUGUESE
                    Return Language.PORTUGUESE
                Case FunimationLanguage.SPANISH
                    Return Language.SPANISH_LATIN_AMERICA
                Case Else
                    Return Language.NONE
            End Select
        End Function
    End Class
End Namespace
