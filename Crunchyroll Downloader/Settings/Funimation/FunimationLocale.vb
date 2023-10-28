Imports Crunchyroll_Downloader.api
Imports Crunchyroll_Downloader.api.common

Namespace settings.funimation
    Module FunimationLocale
        Public Function ConvertToLocale(funLanguage As FunimationLanguage) As Locale
            Select Case funLanguage
                Case FunimationLanguage.CHINESE_MANDARIN
                    Return New Locale(Language.MANDARIN)
                Case FunimationLanguage.ENGLISH
                    Return New Locale(Language.ENGLISH)
                Case FunimationLanguage.JAPANESE
                    Return New Locale(Language.JAPANESE)
                Case FunimationLanguage.PORTUGUESE
                    Return New Locale(Language.PORTUGUESE)
                Case FunimationLanguage.SPANISH
                    Return New Locale(Language.SPANISH)
                Case FunimationLanguage.NONE
                    Return New Locale(Language.NONE)
                Case Else
                    Return New Locale(Language.NONE)
            End Select
        End Function
    End Module
End Namespace