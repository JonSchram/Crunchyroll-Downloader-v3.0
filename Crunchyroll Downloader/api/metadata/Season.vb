Public MustInherit Class Season(Of T As EpisodeOverview)
    Public Property Name As String

    Public Property Number As Integer

    Public Property ApiID As String

    Public Property Episodes As List(Of T)

    Public Function GetEpisodes() As IEnumerable(Of T)
        Return Episodes.AsEnumerable()
    End Function
End Class
