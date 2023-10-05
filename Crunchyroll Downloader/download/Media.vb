Imports Crunchyroll_Downloader.api.client.stream

Namespace download
    ''' <summary>
    ''' Superclass for media contained in a playback.
    ''' <para>
    ''' This is agnostic towards the way to access the media, and only indicates what kind of media it is after downloading
    ''' (how to download is left up to any subclass).
    ''' </para>
    ''' </summary>
    Public MustInherit Class Media
        Public ReadOnly Property Type As MediaType
        Public ReadOnly Property LanguageCode As String

        Protected Sub New(type As MediaType, languageCode As String)
            Me.Type = type
            Me.LanguageCode = languageCode
        End Sub
    End Class
End Namespace