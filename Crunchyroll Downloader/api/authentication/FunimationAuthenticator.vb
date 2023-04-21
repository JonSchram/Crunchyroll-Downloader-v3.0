Imports System.Net
Imports System.Net.Http
Imports Microsoft.Web.WebView2.Core

Namespace api.authentication
    Public Class FunimationAuthenticator
        Implements ICookieBasedAuth

        Private CookieManager As CoreWebView2CookieManager
        Private token As String

        Public Sub New(CookieManager As CoreWebView2CookieManager)
            Me.CookieManager = CookieManager
        End Sub

        Public Sub New(token As String)
            Me.token = token
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

        Public Async Function Authenticate(url As String) As Task(Of String)
            'TODO: The client should be created once and used for many connections, or else it may exhaust sockets.
            Dim handler = New HttpClientHandler With {
                .PreAuthenticate = True
            }
            Dim client = New HttpClient(handler)


            Try
                Dim request As New HttpRequestMessage(HttpMethod.Get, url)
                request.Headers.Authorization = New Headers.AuthenticationHeaderValue("Token", token)

                Dim response As HttpResponseMessage = Await client.SendAsync(request)
                response.EnsureSuccessStatusCode()

                Return Await response.Content.ReadAsStringAsync()

            Catch e As HttpRequestException
                Console.WriteLine("HTTP request exception: ", e.Message)
            End Try

            Return Nothing
        End Function

        Private Function GenerateGuid() As String
            Return Guid.NewGuid().ToString()
        End Function
    End Class

End Namespace