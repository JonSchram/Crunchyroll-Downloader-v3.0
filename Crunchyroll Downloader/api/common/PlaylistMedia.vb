Imports Crunchyroll_Downloader.api.common

Namespace api.common
    ''' <summary>
    ''' Represents media that is stored in a playlist. It contains additional URLs to process and download.
    ''' </summary>
    Public Class PlaylistMedia
        Inherits Media

        Public Sub New(type As MediaType, lang As Language, location As String)
            MyBase.New(type, lang, location)
        End Sub
    End Class
End Namespace