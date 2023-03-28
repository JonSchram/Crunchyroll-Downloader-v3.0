Imports System.Net.Http.Headers

Namespace settings
    Public Class FunimationSettings
        Private Shared Instance As FunimationSettings

        Private Sub New()

        End Sub

        Public Sub UpgradeSettings()
            UpgradeDubs()
            UpgradeSubs()
            UpgradeDefaultSub()
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

        Private Sub UpgradeSubs()
            Dim subs = My.Settings.Fun_Sub
            Dim newSubs = New HashSet(Of FunimationLanguage)
            If subs <> "None" Then
                Dim SoftSubsStringSplit() As String = subs.Split(New Char() {","c}, System.StringSplitOptions.RemoveEmptyEntries)
                For Each item In SoftSubsStringSplit
                    Select Case item
                        Case "en"
                            newSubs.Add(FunimationLanguage.ENGLISH)
                        Case "es"
                            newSubs.Add(FunimationLanguage.SPANISH)
                        Case "pt"
                            newSubs.Add(FunimationLanguage.PORTUGUESE)
                    End Select
                Next
            End If
            SoftSubtitleLanguages = newSubs
        End Sub

        Private Sub UpgradeDefaultSub()
            Dim oldDefault = My.Settings.DefaultSubFunimation
            If oldDefault = "Disabled" Then
                DefaultSubtitle = FunimationLanguage.NONE
            ElseIf oldDefault = "en" Then
                DefaultSubtitle = FunimationLanguage.ENGLISH
            ElseIf oldDefault = "pt" Then
                DefaultSubtitle = FunimationLanguage.PORTUGUESE
            ElseIf oldDefault = "es" Then
                DefaultSubtitle = FunimationLanguage.SPANISH
            End If
            My.Settings.DefaultSubFunimation = ""
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

        Public Property SoftSubtitleLanguages As ISet(Of FunimationLanguage)
            Get
                Dim csv = My.Settings.FunimationSoftSubList
                Dim result = New HashSet(Of FunimationLanguage)

                If csv = Nothing Then
                    Return result
                End If

                ' Property is a CSV list of integers corresponding to the language enum, so a simple string split is enough.
                Dim splitList = csv.Split(New Char() {","c})
                For Each item In splitList
                    Dim itemValue = CInt(item)
                    result.Add(CType(itemValue, FunimationLanguage))
                Next

                Return result
            End Get
            Set(value As ISet(Of FunimationLanguage))
                Dim resultString = ""
                Dim enumerator = value.GetEnumerator()
                Dim isFirst = True
                While enumerator.MoveNext()
                    If Not isFirst Then
                        resultString += ","
                    Else
                        isFirst = False
                    End If
                    resultString += CStr(enumerator.Current)
                End While
                My.Settings.FunimationSoftSubList = resultString
            End Set
        End Property

        Public Property SubtitleFormats As List(Of SubFormat)

        Public Property DefaultSubtitle As FunimationLanguage
            Get
                Return CType(My.Settings.FunimationDefaultSoftSub, FunimationLanguage)
            End Get
            Set(value As FunimationLanguage)
                My.Settings.FunimationDefaultSoftSub = value
            End Set
        End Property

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
