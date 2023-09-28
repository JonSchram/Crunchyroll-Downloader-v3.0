Namespace hls.playlist
    Public Class SessionData
        Private ReadOnly DataId As String
        Private ReadOnly Value As String
        Private ReadOnly Uri As String
        Private ReadOnly Language As String

        Public Sub New(dataId As String, value As String, uri As String, language As String)
            Me.DataId = dataId
            Me.Value = value
            Me.Uri = uri
            Me.Language = language
        End Sub

        Public Function GetDataId() As String
            Return DataId
        End Function

        Public Function GetValue() As String
            Return Value
        End Function

        Public Function GetUri() As String
            Return Uri
        End Function

        Public Function GetLanguage() As String
            Return Language
        End Function

    End Class
End Namespace