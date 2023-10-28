Namespace api.common
    ''' <summary>
    ''' Superclass for media contained in a Selection.
    ''' <para>
    ''' This is agnostic towards the way to access the media, and only indicates what kind of media it is after downloading
    ''' (how to download is left up to any subclass).
    ''' </para>
    ''' </summary>
    Public MustInherit Class Media
        Public ReadOnly Property Type As MediaType
        Public ReadOnly Property MediaLocale As Locale
        Public ReadOnly Property OriginalLocation As String

        Protected Sub New(type As MediaType, locale As Locale, location As String)
            Me.Type = type
            MediaLocale = locale
            OriginalLocation = location
        End Sub
    End Class
End Namespace