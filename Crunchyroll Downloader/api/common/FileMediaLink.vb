﻿Namespace api.common
    ''' <summary>
    ''' Media that is contained in a single file at the location given by the URI.
    ''' The unique thing about this media type is that resolving it is essentially a no-op:
    ''' there is nothing to do until it is time to download.
    ''' </summary>
    Public Class FileMediaLink
        Inherits MediaLink

        Public Sub New(type As MediaType, lang As Locale, uri As String)
            MyBase.New(type, lang, uri)
        End Sub

        Public Overrides Function ToString() As String
            Return $"[FileMediaLink URI: {Location}, Type: {Type}, Locale: {MediaLocale}]"
        End Function
    End Class
End Namespace