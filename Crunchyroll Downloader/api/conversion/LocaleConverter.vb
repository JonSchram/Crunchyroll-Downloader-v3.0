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
    End Class
End Namespace
