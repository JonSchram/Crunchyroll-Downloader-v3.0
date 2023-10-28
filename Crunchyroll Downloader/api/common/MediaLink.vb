Namespace api.common
    Public MustInherit Class MediaLink
        Public ReadOnly Property Type As MediaType

        Public ReadOnly Property MediaLocale As Locale

        Public ReadOnly Property Location As String

        Public Sub New(type As MediaType, locale As Locale, uri As String)
            Me.Type = type
            MediaLocale = locale
            Location = uri
        End Sub
    End Class
End Namespace