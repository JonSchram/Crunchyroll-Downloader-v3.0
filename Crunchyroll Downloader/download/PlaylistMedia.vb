Imports Crunchyroll_Downloader.api.common

Namespace download
    ''' <summary>
    ''' Represents media that is stored in a playlist. It contains additional URLs to process and download.
    ''' </summary>
    Public Class PlaylistMedia
        Inherits Media
        ' TODO: Needs access to the original URL of the playlist so that the ffmpeg downloader can use it.

        Public Sub New(type As MediaType, languageCode As String)
            MyBase.New(type, languageCode)
        End Sub
    End Class
End Namespace