Namespace api.authentication
    Public Class FunimationAuthenticator
        Implements ICookieBasedAuth

        Public Function GetLoginCookies() As String Implements ICookieBasedAuth.GetLoginCookies
            Throw New NotImplementedException()
        End Function
    End Class

End Namespace