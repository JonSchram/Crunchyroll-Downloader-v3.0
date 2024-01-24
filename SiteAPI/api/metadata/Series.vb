Namespace api.metadata
    Public MustInherit Class Series(Of T As SeasonOverview)
        Public Property Seasons As List(Of T)

        '''  <summary>
        ''' Human-readable name for series
        ''' </summary>
        ''' <returns></returns>
        Public Property Name As String

        Public Function GetSeasons() As IEnumerable(Of T)
            Return Seasons.AsEnumerable()
        End Function
    End Class
End Namespace