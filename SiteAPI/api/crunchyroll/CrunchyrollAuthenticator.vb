Imports System.Net

Namespace api.crunchyroll
    Public Class CrunchyrollAuthenticator
        Implements ICookieBasedAuth

        Private CookieProvider As ICookieProvider

        Public Sub New(CookieProvider As ICookieProvider)
            Me.CookieProvider = CookieProvider
        End Sub

        Public Async Function GetLoginCookie() As Task(Of Cookie) Implements ICookieBasedAuth.GetLoginCookie
            Dim cookies = Await GetCrunchyrollCookies()

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
    End Class
End Namespace