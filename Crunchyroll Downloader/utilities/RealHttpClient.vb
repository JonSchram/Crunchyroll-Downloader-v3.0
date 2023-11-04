Imports System.Net.Http

Namespace utilities
    Public Class RealHttpClient
        Implements IHttpClient
        Dim Client As HttpClient

        Public Sub New()
            Client = New HttpClient()
        End Sub

        Public Function SendAsync(httpRequestMessage As HttpRequestMessage) As Task(Of HttpResponseMessage) Implements IHttpClient.SendAsync
            Return Client.SendAsync(httpRequestMessage)
        End Function
    End Class
End Namespace