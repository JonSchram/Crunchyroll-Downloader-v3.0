Imports System.Net

Namespace api
    Public Interface ICookieProvider
        Function GetCookies(uri As String) As Task(Of List(Of Cookie))

    End Interface
End Namespace