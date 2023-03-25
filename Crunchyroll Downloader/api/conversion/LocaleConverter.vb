Imports Crunchyroll_Downloader.settings

Namespace api
    Public Class LocaleConverter

        Public Shared Function ConvertFunimationLanguageToLocale(locale As FunimationSettings.FunimationLanguage) As String
            Select Case locale
                Case FunimationSettings.FunimationLanguage.JAPANESE
                    Return "japanese"
                Case FunimationSettings.FunimationLanguage.ENGLISH
                    Return "english"
                Case FunimationSettings.FunimationLanguage.PORTUGUESE
                    Return "portuguese(Brazil)"
                Case FunimationSettings.FunimationLanguage.SPANISH
                    Return "spanish(Mexico)"
                Case Else
                    Return "N/A"
            End Select
        End Function
    End Class
End Namespace
