Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.playlist.stream

Namespace hls.parsing.tags.rendition

    Public Class MediaTag
        Const TagName As String = "EXT-X-MEDIA"

        ' Parameter comments adapted from IETF RFC 8216: https://www.rfc-editor.org/rfc/rfc8216.html

        ' ----------- Required:
        Public ReadOnly Property Type As MediaType
        Public ReadOnly Property GroupId As String
        Public ReadOnly Property Name As String

        ' ----------- Optional:
        Public ReadOnly Property Language As String
        Public ReadOnly Property AssociatedLanguage As String
        Public ReadOnly Property Characteristics As String

        ' ----------- Optional but with some requirements:

        ' Optional, but must not exist if the type is closed captions.
        Public ReadOnly Property Uri As String

        ' Absence means no
        Public ReadOnly Property IsDefault As Boolean = False

        ' Absence means no
        ' If IsDefault is true, this must be true if present
        Public ReadOnly Property Autoselect As Boolean = False

        ' Absence means no
        ' Must not exist unless the type is subtitles
        Public ReadOnly Property Forced As Boolean = False

        ' ----------- Other:

        ' Required for closed captions. If present, must follow a specific format
        ' Must not appear for any other types.
        Public ReadOnly Property InstreamId As String

        ' Should exist for all audio types. Required if a master playlist has multiple renditions encoded with the same codec
        ' but a different number of channels. Otherwise optional.
        Public ReadOnly Property Channels As String

        Public Sub New(SourceTag As TagAttributes)
            If TagName <> SourceTag.getTagName() Then
                Throw New ArgumentException($"Tag {SourceTag.getTagName()} is incorrect for media, expected {TagName}")
            End If

            ' Required attributes
            Type = getTypeEnum(SourceTag.GetAttribute("TYPE"))

            GroupId = SourceTag.GetAttribute("GROUP-ID")
            If GroupId Is Nothing Then
                Throw New HlsFormatException($"{TagName} requires a group ID.")
            End If

            Name = SourceTag.GetAttribute("NAME")
            If Name Is Nothing Then
                Throw New HlsFormatException($"{TagName} requires a name.")
            End If

            ' Optional attributes
            Language = SourceTag.GetAttribute("LANGUAGE")
            AssociatedLanguage = SourceTag.GetAttribute("ASSOC-LANGUAGE")
            Characteristics = SourceTag.GetAttribute("CHARACTERISTICS")

            ' More complicated rules
            Dim UriString = SourceTag.GetAttribute("URI")
            If UriString IsNot Nothing Then
                If Type = MediaType.CLOSED_CAPTIONS Then
                    Throw New HlsFormatException($"{TagName} cannot set URI for subtitles.")
                End If
                Uri = UriString
            End If

            ' Booleans
            Dim DefaultString = SourceTag.GetAttribute("DEFAULT")
            If DefaultString IsNot Nothing Then
                IsDefault = HlsHelpers.ParseYesNoValue(DefaultString, "DEFAULT")
            End If

            Dim AutoselectString = SourceTag.GetAttribute("AUTOSELECT")
            If AutoselectString IsNot Nothing Then
                Autoselect = HlsHelpers.ParseYesNoValue(AutoselectString, "AUTOSELECT")
            End If

            Dim ForcedString = SourceTag.GetAttribute("FORCED")
            If ForcedString IsNot Nothing Then
                If Type <> MediaType.SUBTITLES Then
                    Throw New HlsFormatException($"{TagName} cannot specify FORCED for non-subtitle media.")
                End If
                Forced = HlsHelpers.ParseYesNoValue(ForcedString, "FORCED")
            End If

            ' Strings
            InstreamId = SourceTag.GetAttribute("INSTREAM-ID")
            If InstreamId Is Nothing And Type = MediaType.CLOSED_CAPTIONS Then
                Throw New HlsFormatException($"{TagName} must have INSTREAM-ID attribute for closed captions type.")
            ElseIf InstreamId IsNot Nothing And Type <> MediaType.CLOSED_CAPTIONS Then
                Throw New HlsFormatException($"{TagName} must only have INSTREAM-ID attribute for closed captions type.")
            End If

            ' This should ideally validate that this exists if a master playlist has two renditions with the same codec,
            ' but that can't be done at this stage
            Channels = SourceTag.GetAttribute("CHANNELS")
        End Sub

        Public Function GetRendition() As AlternativeRendition
            If Type = MediaType.CLOSED_CAPTIONS Then
                Return New ClosedCaptionRendition(GroupId, Name, Language, AssociatedLanguage, Characteristics, IsDefault, Autoselect, Forced, Channels, InstreamId)
            Else
                Return New LinkedRendition(Type, GroupId, Name, Language, AssociatedLanguage, Characteristics, IsDefault, Autoselect, Forced, Channels, Uri)
            End If
        End Function


        Private Shared Function getTypeEnum(typeString As String) As MediaType
            Select Case typeString
                Case "AUDIO"
                    Return MediaType.AUDIO
                Case "VIDEO"
                    Return MediaType.VIDEO
                Case "SUBTITLES"
                    Return MediaType.SUBTITLES
                Case "CLOSED-CAPTIONS"
                    Return MediaType.CLOSED_CAPTIONS
                Case Else
                    Throw New HlsFormatException($"{TagName} type incorrect: {typeString}")
            End Select
        End Function

        Public Overrides Function ToString() As String
            Return $"{{
  Type: {Type},
  GroupId: {GroupId},
  Name: {Name},
  Uri: {Uri}
  Language: {Language},
  AssociatedLanguage: {AssociatedLanguage},
  Channels: {Channels},
  Characteristics: {Characteristics},
  IsDefault: {IsDefault},
  Autoselect: {Autoselect},
  Forced: {Forced},
  InstreamId: {InstreamId}
}}"
        End Function
    End Class
End Namespace
