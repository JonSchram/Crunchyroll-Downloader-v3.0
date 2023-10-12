Imports System.Net
Imports Microsoft.Web.WebView2.Core

Namespace api.crunchyroll
    Public Class CrunchyrollAuthenticator
        Implements ICookieBasedAuth

        Private CookieManager As CoreWebView2CookieManager
        Public Sub New(CookieManager As CoreWebView2CookieManager)
            Me.CookieManager = CookieManager
        End Sub

        Public Async Function GetLoginCookie() As Task(Of Cookie) Implements ICookieBasedAuth.GetLoginCookie
            Dim cookies = Await GetCrunchyrollCookies()

            For Each cookie In cookies
                If cookie.Name = "etp_rt" Then
                    Return cookie.ToSystemNetCookie()
                End If
            Next

            Return Nothing
        End Function
        Private Async Function GetCrunchyrollCookies() As Task(Of List(Of CoreWebView2Cookie))
            Return Await CookieManager.GetCookiesAsync("https://www.crunchyroll.com")
        End Function
    End Class
End Namespace