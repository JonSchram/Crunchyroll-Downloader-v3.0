Imports Newtonsoft.Json.Linq

Namespace api.crunchyroll.metadata.cms
    Public Class CmsEntry
        Public Property Bucket As String
        Public Property Policy As String
        Public Property Signature As String
        Public Property KeyPairId As String
        Public Property Expiration As Date

        Public Shared Function CreateFromJToken(token As JToken) As CmsEntry
            Dim bucket As String = token.Item("bucket").Value(Of String)
            Dim policy As String = token.Item("policy").Value(Of String)
            Dim signature As String = token.Item("signature").Value(Of String)
            Dim keyPair As String = token.Item("key_pair_id").Value(Of String)
            Dim expirationDate As Date = token.Item("expires").Value(Of Date)

            Dim response As New CmsEntry With {
                .Bucket = bucket,
                .Policy = policy,
                .Signature = signature,
                .KeyPairId = keyPair,
                .Expiration = expirationDate
            }
            Return response
        End Function
    End Class
End Namespace