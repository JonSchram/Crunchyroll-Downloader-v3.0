Imports System.Net
Imports System.Net.Http
Imports Microsoft.Web.WebView2.Core

Namespace api.authentication
    Public Class FunimationAuthenticator
        Implements ICookieBasedAuth

        Private CookieManager As CoreWebView2CookieManager

        Public Sub New(CookieManager As CoreWebView2CookieManager)
            Me.CookieManager = CookieManager
        End Sub

        Public Async Function GetLoginCookie() As Task(Of Cookie) Implements ICookieBasedAuth.GetLoginCookie
            Dim cookies = Await CookieManager.GetCookiesAsync("https://www.funimation.com")

            For Each cookie In cookies
                If cookie.Name = "src_token" Then
                    Return cookie.ToSystemNetCookie()
                End If
            Next

            Return Nothing
        End Function
    End Class

End Namespace