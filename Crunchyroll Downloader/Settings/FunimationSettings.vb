Namespace settings
    Public Class FunimationSettings
        Private Shared Instance As FunimationSettings

        Private Sub New()

        End Sub

        Public Shared Function GetInstance() As FunimationSettings
            If Instance Is Nothing Then
                Instance = New FunimationSettings()
            End If
            Return Instance
        End Function

        Public Property DubLanguage As FunimationLanguage

        Public Property SoftSubtitleLanguages As List(Of FunimationLanguage)

        Public Property SubtitleFormats As List(Of SubFormat)

        Public Property DefaltSubtitle As FunimationLanguage

        Public Property HardSubtitleLanguage As FunimationLanguage

        Public Property PreferredBitrate As BitrateSetting

        Public Enum FunimationLanguage
            NONE
            ENGLISH
            JAPANESE
            PORTUGUESE
            SPANISH
        End Enum

        Public Enum SubFormat
            SRT
            VTT
        End Enum

        Public Enum BitrateSetting
            HIGH
            LOW
        End Enum
    End Class
End Namespace
