Imports Newtonsoft.Json.Linq

Namespace api.crunchyroll.metadata.cms
    Public Class CmsStreams

        Public Property Streams As Dictionary(Of String, StreamFormat)
        Public Property Subtitles As Dictionary(Of String, SubtitleEntry)

        Public Sub New()
            Streams = New Dictionary(Of String, StreamFormat)()
            Subtitles = New Dictionary(Of String, SubtitleEntry)()
        End Sub

        Public Shared Function CreateFromJson(Json As String) As CmsStreams
            Dim parsedJson = JObject.Parse(Json)

            Dim result As New CmsStreams() With {
                .Streams = ParseStreams(parsedJson("streams")),
                .Subtitles = ParseSubtitles(parsedJson("subtitles"))
            }

            ' TODO: There is another key 'versions' that contains the IDs of other episodes.
            ' This could be used to get other audio versions, and it might be good to parse it.

            Return result
        End Function

        Private Shared Function ParseStreams(token As JToken) As Dictionary(Of String, StreamFormat)
            Dim streamDict As New Dictionary(Of String, StreamFormat)()
            For Each item As JProperty In token.Children
                Dim formatName = item.Name
                Dim format = StreamFormat.createFromJToken(item)
                streamDict.Add(format.FormatName, format)
            Next

            Return streamDict
        End Function

        Private Shared Function ParseSubtitles(token As JToken) As Dictionary(Of String, SubtitleEntry)
            Dim subtitleDict As New Dictionary(Of String, SubtitleEntry)()

            For Each item In token.Children
                If TypeOf item Is JProperty Then
                    Dim itemProperty As JProperty = item
                    Dim name = itemProperty.Name
                    Dim subtitle = SubtitleEntry.CreateFromJToken(itemProperty.Value)
                    subtitleDict.Add(name, subtitle)
                End If
            Next

            Return subtitleDict
        End Function
    End Class
End Namespace