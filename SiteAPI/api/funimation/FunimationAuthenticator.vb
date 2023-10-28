Imports System.Net
Imports System.Net.Http

Namespace api.funimation
    Public Class FunimationAuthenticator
        Implements ICookieBasedAuth

        Private ReadOnly CookieProvider As ICookieProvider

        Private ReadOnly Client As HttpClient
        Private token As String


        Public Sub New(cookieProvider As ICookieProvider)
            If cookieProvider Is Nothing Then
                Throw New ArgumentException("Browser must not be Nothing.")
            End If

            Me.CookieProvider = cookieProvider

            Dim handler = New HttpClientHandler With {
                .PreAuthenticate = True
            }
            Client = New HttpClient(handler)
        End Sub

        Public Sub New(token As String)
            Me.token = token
        End Sub

        Public Async Function RefreshCookies() As Task
            Dim tokenCookie = Await GetSessionTokenCookie()
            If tokenCookie IsNot Nothing AndAlso tokenCookie.Value IsNot Nothing Then
                token = tokenCookie.Value
            End If
        End Function

        Public Async Function GetLoginCookie() As Task(Of Cookie) Implements ICookieBasedAuth.GetLoginCookie
            Return Await GetSessionTokenCookie()
        End Function

        Private Async Function GetSessionTokenCookie() As Task(Of Cookie)
            Dim cookies = Await GetFunimationCookies()

            If cookies IsNot Nothing Then
                For Each cookie In cookies
                    If cookie.Name = "src_token" Then
                        Return cookie
                    End If
                Next
            End If

            Return Nothing
        End Function

        Public Async Function GetLoginType() As Task(Of UserState)
            Dim cookies = Await GetFunimationCookies()

            For Each cookie In cookies
                If cookie.Name = "userState" Then
                    Select Case cookie.Value
                        Case "Free"
                            Return UserState.FREE
                        Case "Anonymous"
                            Return UserState.ANONYMOUS
                        Case "Paid"
                            ' TODO: Verify that this is the correct state
                            Return UserState.PAID
                        Case Else
                            Return UserState.ANONYMOUS
                    End Select
                End If
            Next

            ' Didn't find the subscription type so the user hasn't visited Funimation in the browser and needs to be anonymous.
            Return UserState.ANONYMOUS
        End Function

        Private Async Function GetFunimationCookies() As Task(Of List(Of Cookie))
            If CookieProvider IsNot Nothing Then
                Return Await CookieProvider.GetCookies("https://www.funimation.com")
            Else
                Return Nothing
            End If
        End Function

        Public Async Function SendAuthenticatedRequest(url As String) As Task(Of String)
            Try
                Dim request As New HttpRequestMessage(HttpMethod.Get, url)
                If token IsNot Nothing Then
                    request.Headers.Authorization = New Headers.AuthenticationHeaderValue("Token", token)
                End If

                Dim response As HttpResponseMessage = Await Client.SendAsync(request)
                response.EnsureSuccessStatusCode()

                Return Await response.Content.ReadAsStringAsync()

            Catch e As HttpRequestException
                ' If the episode requires a subscription, throws an HTTP 403 with:
                ' errorName: InsufficientSubscriptionError
                ' statusCode: 600
                ' The best design here might involve not catching the error so it can be
                ' handled elsewhere. Not sure how often this method will be used and
                ' what other errors might exist.
                Console.WriteLine("HTTP request exception: ", e.Message)
            End Try

            Return Nothing
        End Function
    End Class

    Public Enum UserState
        ANONYMOUS
        FREE
        PAID
    End Enum

End Namespace