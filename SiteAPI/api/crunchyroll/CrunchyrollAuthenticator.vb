Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports Newtonsoft.Json.Linq

Namespace api.crunchyroll
    Public Class CrunchyrollAuthenticator
        Implements ICookieBasedAuth

        ''' <summary>
        ''' URL that a bearer token can be obtained from.
        ''' </summary>
        Private Const BEARER_TOKEN_REQUEST_URL As String = "https://www.crunchyroll.com/auth/v1/token"
        ''' <summary>
        ''' "cr_web:" in base 64 encoding. Required when obtaining a bearer token as an anonymous user.
        ''' </summary>
        Private Const ANONYMOUS_BASIC_AUTH_TOKEN As String = "Y3Jfd2ViOg=="
        ''' <summary>
        ''' Required when obtaining a bearer token as a logged in user.
        ''' </summary>
        Private Const LOGGED_IN_BASIC_AUTH_TOKEN As String = "bm9haWhkZXZtXzZpeWcwYThsMHE6"


        Private ReadOnly CookieProvider As IInteractiveCookieProvider
        Private ReadOnly Client As HttpClient
        Private ReadOnly UserAgent As String
        Private ReadOnly CookieContainer As CookieContainer

        Private DeviceGuid As String
        Private LoginToken As String
        Private BearerToken As String
        Private BearerExpiration As Date

        Public Sub New(CookieProvider As IInteractiveCookieProvider, userAgent As String)
            If CookieProvider Is Nothing Then
                Throw New ArgumentException("Cookie provider must not be Nothing.")
            End If

            Me.CookieProvider = CookieProvider
            Me.UserAgent = userAgent
            DeviceGuid = Guid.NewGuid().ToString()

            CookieContainer = New CookieContainer()
            Dim handler As New HttpClientHandler With {
                .PreAuthenticate = True,
                .CookieContainer = CookieContainer
            }

            Client = New HttpClient(handler)
        End Sub

        Public Sub New(loginToken As String, userAgent As String)
            Me.LoginToken = loginToken
            Me.UserAgent = userAgent

            DeviceGuid = Guid.NewGuid().ToString()

            CookieContainer = New CookieContainer()
            Dim handler As New HttpClientHandler With {
                .PreAuthenticate = True,
                .CookieContainer = CookieContainer
            }

            Client = New HttpClient(handler)
        End Sub

        Public Async Function Initialize() As Task
            Dim cookies As List(Of Cookie) = Await CookieProvider.RequestCookies("https://crunchyroll.com")
            For Each c In cookies
                CookieContainer.Add(c)
            Next
            Dim loginCookie = GetLoginCookie(cookies)
            If loginCookie IsNot Nothing Then
                LoginToken = loginCookie.Value
            End If
        End Function

        Public Async Function Login(token As String) As Task
            ' If we always need cookies to do anything, maybe a "log in" method isn't that useful.
            ' This also doesn't fit the pattern of "get credentials from cookies and assume they are good."
            LoginToken = token
            CookieContainer.Add(New Uri("https://www.crunchyroll.com"), New Cookie("etp_rt", token))
            ' TODO: Save login token, get new bearer token.
            ' Need to refresh bearer token at least every 5 minutes, the response tells how long it lasts.
        End Function

        Public Async Function RefreshAuthorization() As Task

            Dim token As String
            Dim requestContent As String
            If LoginToken Is Nothing OrElse "".Equals(LoginToken) Then
                ' Anonymous user.
                requestContent = $"grant_type=client_id"
                token = ANONYMOUS_BASIC_AUTH_TOKEN
            Else
                ' Logged in.
                requestContent = $"device_id={DeviceGuid}&device_type=Chrome%20on%20Windows&grant_type=etp_rt_cookie"
                token = LOGGED_IN_BASIC_AUTH_TOKEN
            End If

            Dim request As New HttpRequestMessage(HttpMethod.Post, BEARER_TOKEN_REQUEST_URL)
            request.Headers.Authorization = New AuthenticationHeaderValue("Basic", token)
            'request.Headers.Add("ETP-Anonymous-ID", DeviceGuid)
            request.Headers.UserAgent.TryParseAdd(UserAgent)
            request.Content = New StringContent(requestContent, Encoding.UTF8, "application/x-www-form-urlencoded")

            Dim response As HttpResponseMessage = Await Client.SendAsync(request)

            Debug.WriteLine($"Refreshing authorization, Crunchyroll returned {response.StatusCode()}")
            Dim responseContent As String = Await response.Content.ReadAsStringAsync()
            response.EnsureSuccessStatusCode()

            Dim jsonResponse As String = Await response.Content.ReadAsStringAsync()
            Dim json As JObject = JObject.Parse(jsonResponse)

            BearerToken = json.Item("access_token")
            Dim lifetime As Integer = json.Item("expires_in").Value(Of Integer)
            BearerExpiration = Date.Now.AddSeconds(lifetime)
        End Function

        Public Async Function RefreshCookies() As Task
            Dim loginCookie As Cookie = Await GetLoginCookie()
            If loginCookie?.Value IsNot Nothing Then
                LoginToken = loginCookie.Value
            End If
        End Function

        Public Async Function GetLoginCookie() As Task(Of Cookie) Implements ICookieBasedAuth.GetLoginCookie
            Dim cookies = Await GetCrunchyrollCookies()

            Return GetLoginCookie(cookies)
        End Function

        Private Function GetLoginCookie(cookies As List(Of Cookie)) As Cookie
            For Each cookie In cookies
                If cookie.Name = "etp_rt" Then
                    Return cookie
                End If
            Next
            Return Nothing
        End Function

        Private Async Function GetCrunchyrollCookies() As Task(Of List(Of Cookie))
            Return Await CookieProvider.GetCookies("https://www.crunchyroll.com")
        End Function

        Public Async Function SendAuthenticatedRequest(url As String) As Task(Of String)
            Try
                Dim bearerAgeRemaining As TimeSpan = BearerExpiration - Date.Now
                If BearerToken Is Nothing Or bearerAgeRemaining.TotalSeconds < 30 Then
                    Await RefreshAuthorization()
                End If

                Dim request As New HttpRequestMessage(HttpMethod.Get, url)
                request.Headers.UserAgent.TryParseAdd(UserAgent)
                If BearerToken IsNot Nothing Then
                    request.Headers.Authorization = New AuthenticationHeaderValue("Bearer", BearerToken)
                End If

                Dim response As HttpResponseMessage = Await Client.SendAsync(request)
                response.EnsureSuccessStatusCode()

                Return Await response.Content.ReadAsStringAsync()

            Catch e As HttpRequestException
                Debug.WriteLine("HTTP request exception: ", e.Message)
                Return $"HTTP request exception: {e.Message}"
            End Try

            Return Nothing
        End Function
    End Class
End Namespace