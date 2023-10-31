Imports SiteAPI.api.common

Namespace data
    Public Class MediaFileEntry
        Public ReadOnly Property Location As String
        Public ReadOnly Property ContainedMedia As MediaType

        Public Sub New(location As String, media As MediaType)
            Me.Location = location
            ContainedMedia = media
        End Sub
    End Class
End Namespace