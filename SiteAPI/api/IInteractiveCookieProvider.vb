Imports System.Net

Namespace api

    Public Interface IInteractiveCookieProvider
        Inherits ICookieProvider

        ''' <summary>
        ''' Requests that the cookie provider visit the given URI in order to retrieve cookies.
        ''' </summary>
        ''' <param name="uri"></param>
        ''' <returns></returns>
        Function RequestCookies(uri As String) As Task(Of List(Of Cookie))

    End Interface

End Namespace