Imports Crunchyroll_Downloader.api.common

Namespace download
    ''' <summary>
    ''' Represents media that can be downloaded as a single file, no processing needed.
    ''' </summary>
    Public Class CompleteMedia
        Inherits Media
        Public ReadOnly Property Url As String

        Public Sub New(type As MediaType, lang As String, url As String)
            MyBase.New(type, lang)
            Me.Url = url
        End Sub
    End Class
End Namespace