Imports Newtonsoft.Json.Linq

Namespace api.crunchyroll.metadata.cms
    Public Class StreamEntry
        Public ReadOnly HardsubLocale As String
        Public ReadOnly Url As String

        Public Sub New(locale As String, url As String)
            Me.HardsubLocale = locale
            Me.Url = url
        End Sub


        Public Shared Function CreateFromJToken(token As JToken) As StreamEntry
            Dim locale As String = token.Item("hardsub_locale").Value(Of String)
            Dim url As String = token.Item("url").Value(Of String)

            Return New StreamEntry(locale, url)
        End Function
    End Class
End Namespace