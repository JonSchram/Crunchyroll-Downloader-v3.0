Imports Crunchyroll_Downloader.hls.common

Namespace hls.playlist.stream
    Public Class RenditionGroup
        Public ReadOnly Property GroupId As String

        Public ReadOnly Property Type As MediaType

        Public Sub New(groupId As String, type As MediaType)
            Me.GroupId = groupId
            Me.Type = type
        End Sub
    End Class
End Namespace