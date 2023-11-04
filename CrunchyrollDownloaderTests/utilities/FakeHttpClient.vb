Imports System.Net
Imports System.Net.Http
Imports Crunchyroll_Downloader.utilities

Namespace utilities
    Public Class FakeHttpClient
        Implements IHttpClient

        Public ReadOnly Property SentRequests As New List(Of HttpRequestMessage)

        Public ReadOnly Property ResponseCodeMap As New Dictionary(Of String, HttpStatusCode)
        Public ReadOnly Property ResponseContent As New Dictionary(Of String, String)

        Public Sub SetStatusCode(url As String, responseCode As HttpStatusCode)
            ResponseCodeMap.Add(url, responseCode)
        End Sub

        Public Sub SetContent(url As String, content As String)
            ResponseContent.Add(url, content)
        End Sub

        Public Function SendAsync(httpRequestMessage As HttpRequestMessage) As Task(Of HttpResponseMessage) Implements IHttpClient.SendAsync
            SentRequests.Add(httpRequestMessage)
            Dim uri As String = httpRequestMessage.RequestUri.ToString()

            Dim responseCode As HttpStatusCode
            If Not ResponseCodeMap.TryGetValue(uri, responseCode) Then
                responseCode = HttpStatusCode.OK
            End If
            Dim response = New HttpResponseMessage(responseCode)

            Dim httpResponse As String = ""
            If ResponseContent.TryGetValue(uri, httpResponse) Then
                Dim content = New StringContent(httpResponse)
                response.Content = content
            End If

            Return Task.FromResult(response)
        End Function
    End Class
End Namespace
