Imports Crunchyroll_Downloader.settings
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
    End Class
End Namespace
