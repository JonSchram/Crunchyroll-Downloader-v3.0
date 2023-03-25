Namespace settings
    Public Class FunimationSettings
        Private Shared Instance As FunimationSettings

        Private Sub New()

        End Sub

        Public Sub UpgradeSettings()
            UpgradeDubs()
        End Sub

        Private Sub UpgradeDubs()
            Dim dub = My.Settings.FunimationDub
            Select Case dub
                Case "english"
                    DubLanguage = FunimationLanguage.ENGLISH
                Case "japanese"
                    DubLanguage = FunimationLanguage.JAPANESE
                Case "portuguese(Brazil)"
                    DubLanguage = FunimationLanguage.PORTUGUESE
                Case "spanish(Mexico)"
                    DubLanguage = FunimationLanguage.SPANISH
                Case "Disabled"
                    DubLanguage = FunimationLanguage.NONE
            End Select
        End Sub

        Public Shared Function GetInstance() As FunimationSettings
            If Instance Is Nothing Then
                Instance = New FunimationSettings()
            End If
            Return Instance
        End Function

        Public Property DubLanguage As FunimationLanguage
            Get
                Return CType(My.Settings.FunimationDubLanguage, FunimationLanguage)
            End Get
            Set(value As FunimationLanguage)
                My.Settings.FunimationDubLanguage = value
            End Set
        End Property

        Public Property SoftSubtitleLanguages As List(Of FunimationLanguage)

        Public Property SubtitleFormats As List(Of SubFormat)

        Public Property DefaltSubtitle As FunimationLanguage

        Public Property HardSubtitleLanguage As FunimationLanguage

        Public Property PreferredBitrate As BitrateSetting

        Public Enum FunimationLanguage As Integer
            NONE = 0
            ENGLISH = 1
            JAPANESE = 2
            PORTUGUESE = 3
            SPANISH = 4
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
