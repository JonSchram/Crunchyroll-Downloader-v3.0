Imports System.Net

Namespace api.authentication
    Public Interface ICookieBasedAuth
        Function GetLoginCookie() As Task(Of Cookie)
    End Interface
End Namespace