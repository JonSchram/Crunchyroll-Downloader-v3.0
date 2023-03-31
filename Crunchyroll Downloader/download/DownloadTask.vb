Namespace download

    ''' <summary>
    ''' Class intended to represent a single download task.
    ''' It is for a single video and contains all information needed to download it and name the file correctly.
    ''' </summary>
    Public Class DownloadTask
        Private ReadOnly DownloadEpisode As Episode
        Private ReadOnly OutputPath As String

        Public Sub New(ep As Episode, path As String)
            DownloadEpisode = ep
            OutputPath = path
        End Sub

        Public Function GetEpisode() As Episode
            Return DownloadEpisode
        End Function
        Public Function GetSaveLocation() As String
            Return OutputPath
        End Function

        Public Overrides Function ToString() As String
            Return DownloadEpisode.ToString()
        End Function
    End Class
End Namespace