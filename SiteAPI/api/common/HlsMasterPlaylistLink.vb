Namespace api.common
    Public Class HlsMasterPlaylistLink
        Inherits MediaLink

        Public Sub New(type As MediaType, audioLocale As Locale, uri As String)
            MyBase.New(type, audioLocale, uri)
        End Sub

        Public Overrides Function ToString() As String
            Return $"[HlsMasterPlaylistLink URI: {Location}, Type: {Type}, Language: {MediaLocale}]"
        End Function
    End Class
End Namespace
