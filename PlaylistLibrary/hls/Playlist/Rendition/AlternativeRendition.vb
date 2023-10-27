Imports PlaylistLibrary.hls.common

Namespace hls.playlist.rendition
    ''' <summary>
    ''' Superclass for any alternative rendition of a specific media type in a master playlist.
    ''' </summary>
    Public MustInherit Class AlternativeRendition
        Public ReadOnly Property Type As MediaType
        Public ReadOnly Property GroupId As String
        Public ReadOnly Property Name As String
        Public ReadOnly Property Language As String
        Public ReadOnly Property AssociatedLanguage As String
        Public ReadOnly Property Characteristics As String
        Public ReadOnly Property IsDefault As Boolean = False
        Public ReadOnly Property Autoselect As Boolean = False
        Public ReadOnly Property Forced As Boolean = False
        Public ReadOnly Property Channels As String

        Public Sub New(type As MediaType, groupId As String, name As String,
                       language As String, associatedLanguage As String,
                       characteristics As String, isDefault As Boolean,
                       autoselect As Boolean, forced As Boolean, channels As String)
            Me.Type = type
            Me.GroupId = groupId
            Me.Name = name
            Me.Language = language
            Me.AssociatedLanguage = associatedLanguage
            Me.Characteristics = characteristics
            Me.IsDefault = isDefault
            Me.Autoselect = autoselect
            Me.Forced = forced
            Me.Channels = channels
        End Sub
    End Class
End Namespace
