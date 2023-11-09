Imports Crunchyroll_Downloader.settings.general

Namespace preferences
    Public Class OutputPreferenceFactory

        Public Function GetPreferences() As OutputPreferences
            Dim settings As ProgramSettings = ProgramSettings.GetInstance()

            Dim outputDir = settings.OutputPath
            Dim template = settings.FilenameFormat

            Dim numberPaddingLength As Integer = settings.ZeroPaddingLength


            Return New OutputPreferences() With {
                .OutputPath = outputDir
            }
        End Function
    End Class
End Namespace