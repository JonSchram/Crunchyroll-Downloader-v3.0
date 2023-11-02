Imports SiteAPI.api.common

Namespace data
    Public Class MediaFileEntry
        Public ReadOnly Property Location As String
        Public ReadOnly Property ContainedMedia As MediaType

        Public Sub New(location As String, media As MediaType)
            Me.Location = location
            ContainedMedia = media
        End Sub

        Public Overrides Function Equals(obj As Object) As Boolean
            Dim entry = TryCast(obj, MediaFileEntry)
            Return entry IsNot Nothing AndAlso
                   Location = entry.Location AndAlso
                   ContainedMedia = entry.ContainedMedia
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return (Location, ContainedMedia).GetHashCode()
        End Function
    End Class
End Namespace