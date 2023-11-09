Imports Crunchyroll_Downloader.settings.general
Imports SiteAPI.api
Imports SiteAPI.api.metadata

Namespace download

    ''' <summary>
    ''' Class intended to represent a single download task.
    ''' It is for a single video and contains all information needed to download it and name the file correctly.
    ''' </summary>
    Public Class DownloadTask
        Public ReadOnly Property DownloadEpisode As Episode
        Public ReadOnly Property OutputPath As String
        Public ReadOnly Property OverriddenSubfolder As String
        Public ReadOnly Property SubfolderCreation As SubfolderBehavior
        Public ReadOnly Property Client As IDownloadClient

        Public Sub New(ep As Episode, path As String, client As IDownloadClient, overriddenSubfolder As String, subfolderCreation As SubfolderBehavior)
            DownloadEpisode = ep
            OutputPath = path
            Me.Client = client
            Me.OverriddenSubfolder = overriddenSubfolder
            Me.SubfolderCreation = subfolderCreation
        End Sub

        Public Overrides Function ToString() As String
            Return DownloadEpisode.ToString()
        End Function
    End Class
End Namespace