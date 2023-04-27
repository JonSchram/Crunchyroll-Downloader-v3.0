Imports System.Runtime.Remoting
Imports Crunchyroll_Downloader.api.client

Namespace download

    ''' <summary>
    ''' Class intended to represent a single download task.
    ''' It is for a single video and contains all information needed to download it and name the file correctly.
    ''' </summary>
    Public Class DownloadTask
        ' TODO: Choose which version of an episode to download
        ' Episodes may have multiple versions and there isn't a way to choose which one.
        Private ReadOnly DownloadEpisode As Episode
        Private ReadOnly OutputPath As String
        Private ReadOnly MetadataClient As IMetadataDownloader

        Public Sub New(ep As Episode, path As String, client As IMetadataDownloader)
            DownloadEpisode = ep
            OutputPath = path
            MetadataClient = client
        End Sub

        Public Function GetEpisode() As Episode
            Return DownloadEpisode
        End Function
        Public Function GetSaveLocation() As String
            Return OutputPath
        End Function

        Public Function GetMetadataClient() As IMetadataDownloader
            Return MetadataClient
        End Function

        Public Overrides Function ToString() As String
            Return DownloadEpisode.ToString()
        End Function
    End Class
End Namespace