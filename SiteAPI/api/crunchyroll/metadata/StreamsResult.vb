Imports Newtonsoft.Json.Linq

Namespace api.crunchyroll.metadata
    Public Class StreamsResult
        Public Property Streams As Dictionary(Of String, StreamFormat)

        Public Sub New()
            Streams = New Dictionary(Of String, StreamFormat)()
        End Sub

        Public Shared Function CreateFromJson(Json As String) As StreamsResult
            Dim parsedJson = JObject.Parse(Json)
            Dim data = parsedJson.Item("data")

            Dim result As New StreamsResult
            For Each item In data.Children
                ' Data is an array of dictionaries mapping from a string to a set of hardsub versions.
                For Each formatToken In item.Children
                    If TypeOf formatToken Is JProperty Then
                        Dim format = StreamFormat.createFromJToken(formatToken)
                        result.Streams.Add(format.FormatName, format)
                    End If
                Next
            Next

            Dim metadata = parsedJson.Item("meta")
            ' TODO: Seems that even if this isn't the desired audio language, meta contains the IDs of other episodes.

            Return result
        End Function
    End Class
End Namespace