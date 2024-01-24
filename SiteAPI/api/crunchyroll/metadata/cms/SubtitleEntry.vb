Imports Newtonsoft.Json.Linq

Namespace api.crunchyroll.metadata.cms
    Public Class SubtitleEntry
        Public Property Locale As String
        Public Property Url As String
        Public Property Format As String


        Public Shared Function CreateFromJToken(token As JToken) As SubtitleEntry
            Dim locale = token("locale")
            Dim url = token("url")
            Dim format = token("format")
            Return New SubtitleEntry With {
                .Locale = locale.Value(Of String),
                .Url = url.Value(Of String),
                .Format = format.Value(Of String)
            }
        End Function

    End Class
End Namespace