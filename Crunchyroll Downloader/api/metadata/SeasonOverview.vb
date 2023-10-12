Namespace api.metadata
    Public MustInherit Class SeasonOverview

        Public Property Name As String

        Public Property Number As Integer

        ' The ID added to the URL that needs to be used to get season info
        Public Property ApiID As String

        Public Overrides Function ToString() As String
            Return Name
        End Function

    End Class
End Namespace