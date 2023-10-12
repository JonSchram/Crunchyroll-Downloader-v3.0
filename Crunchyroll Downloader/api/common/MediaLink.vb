Namespace api.common
    Public MustInherit Class MediaLink
        Public ReadOnly Property Type As MediaType

        Public ReadOnly Property MediaLanguage As Language

        Public ReadOnly Property Location As String

        Public Sub New(type As MediaType, lang As Language, uri As String)
            Me.Type = type
            MediaLanguage = lang
            Location = uri
        End Sub
    End Class
End Namespace