Imports Crunchyroll_Downloader.api.common

Namespace api.funimation
    Module FunimationStringParser


        Public Function ParseLanguageCode(code As String) As Locale
            Select Case code
                Case "en"
                    Return New Locale(Language.ENGLISH)
                Case "es"
                    Return New Locale(Language.SPANISH)
                Case "pt"
                    Return New Locale(Language.PORTUGUESE)
                Case "ja"
                    Return New Locale(Language.JAPANESE)
                Case "zh-mn"
                    Return New Locale(Language.MANDARIN)
                Case "N/A"
                    Return New Locale(Language.NONE)
                Case Else
                    Return New Locale(Language.NONE)
            End Select
        End Function

        Public Function ParseSubtitleFormat(formatName As String) As SubtitleFormat
            Select Case formatName
                Case "srt"
                    Return SubtitleFormat.SRT
                Case "vtt"
                    Return SubtitleFormat.VTT
                Case ""
                    Return SubtitleFormat.NONE
                Case Else
                    ' Couldn't be parsed and isn't an emptpy string, so optimistically take any subtitle format.
                    Return SubtitleFormat.ANY
            End Select
        End Function
    End Module
End Namespace