Imports Crunchyroll_Downloader.hls.common

Namespace hls.playlist.stream
    ''' <summary>
    ''' Alternative rendition of a closed caption stream.
    ''' </summary>
    Public Class ClosedCaptionRendition
        Inherits AlternativeRendition

        Public ReadOnly Property InstreamId As String

        Public Sub New(groupId As String, name As String, language As String,
                       associatedLanguage As String, characteristics As String, isDefault As Boolean,
                       autoselect As Boolean, forced As Boolean, channels As String, instreamId As String)
            MyBase.New(MediaType.CLOSED_CAPTIONS, groupId, name, language, associatedLanguage,
                       characteristics, isDefault, autoselect, forced, channels)
            Me.InstreamId = instreamId
        End Sub
    End Class
End Namespace
