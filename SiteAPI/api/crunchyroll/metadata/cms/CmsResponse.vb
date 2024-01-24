Imports Newtonsoft.Json.Linq

Namespace api.crunchyroll.metadata.cms
    Public Class CmsResponse
        Public ReadOnly Property CmsOptions As Dictionary(Of String, CmsEntry)

        Public Sub New()
            CmsOptions = New Dictionary(Of String, CmsEntry)()
        End Sub

        Public Shared Function CreateFromJson(json As String) As CmsResponse
            Dim cmsObject = JToken.Parse(json)

            Dim response As New CmsResponse()
            For Each child In cmsObject.Children
                If TypeOf child Is JProperty Then
                    Dim childProperty = CType(child, JProperty)
                    If TypeOf childProperty.Value Is JObject Then
                        Dim name = childProperty.Name
                        Dim cms = CmsEntry.CreateFromJToken(childProperty.Value)

                        response.CmsOptions.Add(name, cms)
                    End If
                End If
            Next

            Return response
        End Function
    End Class
End Namespace