Imports Crunchyroll_Downloader.settings.funimation

Namespace api.conversion
    Public Class FormatConverter
        Public Shared Function ConvertFormatStringToSubtitleFormat(format As String) As SubFormat
            If format = "vtt" Then
                Return SubFormat.VTT
            ElseIf format = "srt" Then
                Return SubFormat.SRT
            Else
                Return Nothing
            End If
        End Function
    End Class
End Namespace