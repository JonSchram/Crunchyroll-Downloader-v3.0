Namespace api.common
    ''' <summary>
    ''' Represents media that can be downloaded as a single file, no processing needed.
    ''' </summary>
    Public Class FileMedia
        Inherits Media

        Public Sub New(type As MediaType, lang As Language, url As String)
            MyBase.New(type, lang, url)
        End Sub
    End Class
End Namespace