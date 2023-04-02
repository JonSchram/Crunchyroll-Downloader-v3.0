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
            Dim cookies = Await GetFunimationCookies()

            For Each cookie In cookies
                If cookie.Name = "src_token" Then
                    Return cookie.ToSystemNetCookie()
                End If
            Next

            Return Nothing
        End Function

        Public Async Function IsPaidAccount() As Task(Of Boolean)
            Dim cookies = Await GetFunimationCookies()

            For Each cookie In cookies
                If cookie.Name = "userState" Then
                    Return cookie.Value <> "Free"
                End If
            Next

            ' Didn't find the subscription type so can't determine.
            Return False
        End Function

        Private Async Function GetFunimationCookies() As Task(Of List(Of CoreWebView2Cookie))
            Return Await CookieManager.GetCookiesAsync("https://www.funimation.com")
        End Function
    End Class

End Namespace