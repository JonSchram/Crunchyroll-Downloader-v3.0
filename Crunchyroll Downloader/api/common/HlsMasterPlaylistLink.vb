Namespace api.common
    Public Class HlsMasterPlaylistLink
        Inherits MediaLink

        Public Sub New(type As MediaType, audioLanguage As Language, uri As String)
            MyBase.New(type, audioLanguage, uri)
        End Sub

        Public Overrides Function ToString() As String
            Return $"[HlsMasterPlaylistLink URI: {Location}, Type: {Type}, Language: {MediaLanguage}]"
        End Function
    End Class
End Namespace
