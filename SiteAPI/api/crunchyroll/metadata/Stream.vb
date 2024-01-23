Imports Newtonsoft.Json.Linq

Namespace api.crunchyroll.metadata
    Public Class Stream
        Public ReadOnly HardsubLocale As String
        Public ReadOnly Url As String

        Public Sub New(locale As String, url As String)
            Me.HardsubLocale = locale
            Me.Url = url
        End Sub


        Public Shared Function CreateFromJToken(token As JToken) As Stream
            Dim locale As String = token.Item("hardsub_locale").Value(Of String)
            Dim url As String = token.Item("url").Value(Of String)

            Return New Stream(locale, url)
        End Function
    End Class
End Namespace