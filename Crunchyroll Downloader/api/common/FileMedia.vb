Namespace api.common
    ''' <summary>
    ''' Represents media that can be downloaded as a single file, no processing needed.
    ''' </summary>
    Public Class FileMedia
        Inherits Media

        Public Sub New(type As MediaType, locale As Locale, url As String)
            MyBase.New(type, locale, url)
        End Sub

        Public Overrides Function ToString() As String
            Return $"[FileMedia: URI: {OriginalLocation}, Type: {Type}, Locale: {MediaLocale}"
        End Function
    End Class
End Namespace