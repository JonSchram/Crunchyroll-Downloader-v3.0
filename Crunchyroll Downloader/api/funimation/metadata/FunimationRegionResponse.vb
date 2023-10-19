Imports Newtonsoft.Json.Linq

Namespace api.funimation.metadata
    Public Class FunimationRegionResponse
        Public ReadOnly Property Region As String
        Public ReadOnly Property IsSupportedRegion As Boolean

        Public Sub New(Region As String, supported As Boolean)
            Me.Region = Region
            IsSupportedRegion = supported
        End Sub

        Public Shared Function CreateFromJson(json As String) As FunimationRegionResponse
            Dim regionInfo As JObject = JObject.Parse(json)

            Dim regionString = regionInfo.Item("region").Value(Of String)
            Dim supportedRegion = regionInfo.Item("isSupportedRegion").Value(Of Boolean)

            Return New FunimationRegionResponse(regionString, supportedRegion)
        End Function
    End Class
End Namespace