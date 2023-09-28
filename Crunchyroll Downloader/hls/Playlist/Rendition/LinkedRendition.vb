Imports Crunchyroll_Downloader.hls.common

Namespace hls.playlist.rendition
    ''' <summary>
    ''' An alternative rendition that supports referencing a playlist URL.
    ''' 
    ''' In practice, this is video, audio, or subtitle media types.
    ''' </summary>
    Public Class LinkedRendition
        Inherits AlternativeRendition

        Public Property Uri As String

        Public Sub New(type As MediaType, groupId As String, name As String, language As String,
                       associatedLanguage As String, characteristics As String, isDefault As Boolean,
                       autoselect As Boolean, forced As Boolean, channels As String, uri As String)
            MyBase.New(type, groupId, name, language, associatedLanguage, characteristics,
                       isDefault, autoselect, forced, channels)
            Me.Uri = uri
        End Sub
    End Class
End Namespace
