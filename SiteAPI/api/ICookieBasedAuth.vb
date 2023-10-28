Imports System.Net

Namespace api
    Public Interface ICookieBasedAuth
        Function GetLoginCookie() As Task(Of Cookie)
    End Interface
End Namespace