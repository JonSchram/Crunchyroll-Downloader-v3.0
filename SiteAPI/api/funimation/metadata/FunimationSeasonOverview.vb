Imports SiteAPI.api.metadata

Namespace api.funimation.metadata
    Public Class FunimationSeasonOverview
        Inherits SeasonOverview

        ''' <summary>
        ''' The ID of the episode. This or the ApiId in the superclass can be used to access season details.
        ''' </summary>
        ''' <returns></returns>
        Public Property Id As Integer

        Public Property Type As String

        Public Property IsFree As Boolean
    End Class
End Namespace