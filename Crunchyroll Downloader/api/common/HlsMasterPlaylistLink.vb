Namespace api.common
    Public Class HlsMasterPlaylistLink
        Inherits MediaLink

        Public Sub New(type As MediaType, audioLanguage As Language, uri As String)
            MyBase.New(type, audioLanguage, uri)
        End Sub
    End Class
End Namespace
