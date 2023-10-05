Imports Crunchyroll_Downloader.api.client.stream

Namespace download
    ''' <summary>
    ''' Represents media that is stored in a playlist. It contains additional URLs to process and download.
    ''' </summary>
    Public Class PlaylistMedia
        Inherits Media

        Public Sub New(type As MediaType, languageCode As String)
            MyBase.New(type, languageCode)
        End Sub
    End Class
End Namespace