Imports System.Collections.Specialized

Namespace settings.funimation
    Public Class FunimationSettings
        Private Shared Instance As FunimationSettings

        Private Sub New()

        End Sub

        Friend Sub UpgradeSettings()
            UpgradeDubs()
            UpgradeSubs()
            UpgradeDefaultSub()
            UpgradeHardsubs()
        End Sub

        Private Sub UpgradeDubs()
            Try
                Dim dub As String = CStr(My.Settings.GetPreviousVersion("FunimationDub"))
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
            Catch ex As Exception
                DubLanguage = FunimationLanguage.NONE
            End Try
        End Sub

        Private Sub UpgradeSubs()
            Dim newSubs = New HashSet(Of FunimationLanguage)
            Try
                Dim subs As String = CStr(My.Settings.GetPreviousVersion("Fun_Sub"))
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
            Catch ex As Exception
                SoftSubtitleLanguages = newSubs
            End Try
        End Sub

        Private Sub UpgradeDefaultSub()
            Try
                Dim oldDefault As String = CStr(My.Settings.GetPreviousVersion("DefaultSubFunimation"))
                If oldDefault = "Disabled" Then
                    DefaultSubtitle = FunimationLanguage.NONE
                ElseIf oldDefault = "en" Then
                    DefaultSubtitle = FunimationLanguage.ENGLISH
                ElseIf oldDefault = "pt" Then
                    DefaultSubtitle = FunimationLanguage.PORTUGUESE
                ElseIf oldDefault = "es" Then
                    DefaultSubtitle = FunimationLanguage.SPANISH
                End If
            Catch ex As Exception
                DefaultSubtitle = FunimationLanguage.NONE
            End Try
        End Sub

        Private Sub UpgradeHardsubs()
            Try
                Dim oldSub As String = CStr(My.Settings.GetPreviousVersion("FunimationHardsub"))
                Select Case oldSub
                    Case "en"
                        HardSubtitleLanguage = FunimationLanguage.ENGLISH
                    Case "pt"
                        HardSubtitleLanguage = FunimationLanguage.PORTUGUESE
                    Case "es"
                        HardSubtitleLanguage = FunimationLanguage.SPANISH
                    Case "Disabled"
                        HardSubtitleLanguage = FunimationLanguage.NONE
                End Select
            Catch ex As Exception
                HardSubtitleLanguage = FunimationLanguage.NONE
            End Try
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

        Public Property SubtitleFormats As ISet(Of SubFormat)
            Get
                Dim subFormatList = My.Settings.FunimationSubFormats
                Dim result = New HashSet(Of SubFormat)

                If subFormatList IsNot Nothing Then
                    For Each item In subFormatList
                        Dim parsed As SubFormat
                        If [Enum].TryParse(item, parsed) Then
                            result.Add(parsed)
                        End If
                    Next
                End If

                Return result
            End Get
            Set(value As ISet(Of SubFormat))
                Dim stringList = New StringCollection()
                For Each item In value
                    stringList.Add(item.ToString())
                Next
                My.Settings.FunimationSubFormats = stringList
            End Set
        End Property

        Public Property DefaultSubtitle As FunimationLanguage
            Get
                Return CType(My.Settings.FunimationDefaultSoftSub, FunimationLanguage)
            End Get
            Set(value As FunimationLanguage)
                My.Settings.FunimationDefaultSoftSub = value
            End Set
        End Property

        Public Property HardSubtitleLanguage As FunimationLanguage
            Get
                Return CType(My.Settings.FunimationHardsubLanguage, FunimationLanguage)
            End Get
            Set(value As FunimationLanguage)
                My.Settings.FunimationHardsubLanguage = value
            End Set
        End Property

    End Class
End Namespace
