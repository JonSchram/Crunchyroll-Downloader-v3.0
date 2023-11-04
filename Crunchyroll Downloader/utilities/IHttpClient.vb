Imports System.Net.Http

Namespace utilities
    Public Interface IHttpClient
        Function SendAsync(httpRequestMessage As HttpRequestMessage) As Task(Of HttpResponseMessage)
    End Interface
End Namespace