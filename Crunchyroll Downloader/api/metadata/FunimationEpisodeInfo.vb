Public Class FunimationEpisodeInfo
    Inherits EpisodeInfo

    Public Overrides Function ToString() As String
        ' Display extra information indicating where the episode is from
        Return MyBase.ToString() + " (Funimation)"
    End Function
End Class
