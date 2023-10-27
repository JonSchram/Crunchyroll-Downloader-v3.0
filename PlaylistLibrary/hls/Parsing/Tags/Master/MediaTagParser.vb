Imports System.IO
Imports PlaylistLibrary.hls.common
Imports PlaylistLibrary.hls.playlist
Imports PlaylistLibrary.hls.playlist.rendition

Namespace hls.parsing.tags.master

    Public Class MediaTagParser
        Inherits TagParser(Of MasterPlaylist.Builder)

        Private Function GetTypeEnum(typeString As String) As MediaType
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
                    Throw New HlsFormatException($"{GetTagName()} type incorrect: {typeString}")
            End Select
        End Function

        Public Overrides Sub ParseInner(reader As TextReader, attributes As ParsedTag, playlist As MasterPlaylist.Builder)
            'Attribute comments and error messages adapted from IETF RFC 8216: https://www.rfc-editor.org/rfc/rfc8216.html

            ' Required attributes
            Dim Type As MediaType = GetTypeEnum(attributes.GetAttribute("TYPE")?.Value)

            Dim GroupId As String = attributes.GetAttribute("GROUP-ID")?.Value
            If GroupId Is Nothing Then
                Throw New HlsFormatException($"{GetTagName()} requires a group ID.")
            End If

            Dim Name As String = attributes.GetAttribute("NAME")?.Value
            If Name Is Nothing Then
                Throw New HlsFormatException($"{GetTagName()} requires a name.")
            End If

            ' Optional attributes
            Dim Language As String = attributes.GetAttribute("LANGUAGE")?.Value
            Dim AssociatedLanguage As String = attributes.GetAttribute("ASSOC-LANGUAGE")?.Value
            Dim Characteristics As String = attributes.GetAttribute("CHARACTERISTICS")?.Value

            ' More complicated rules
            Dim uri = attributes.GetAttribute("URI")?.Value
            If uri IsNot Nothing And Type = MediaType.CLOSED_CAPTIONS Then
                Throw New HlsFormatException($"{GetTagName()} cannot set URI for closed captions.")
            End If

            ' Boolean values.
            Dim DefaultString = attributes.GetAttribute("DEFAULT")?.Value
            Dim IsDefault As Boolean = False
            If DefaultString IsNot Nothing Then
                IsDefault = HlsHelpers.ParseYesNoValue(DefaultString, "DEFAULT")
            End If

            Dim AutoselectString = attributes.GetAttribute("AUTOSELECT")?.Value
            Dim Autoselect As Boolean = False
            If AutoselectString IsNot Nothing Then
                Autoselect = HlsHelpers.ParseYesNoValue(AutoselectString, "AUTOSELECT")

                If IsDefault And Not Autoselect Then
                    Throw New HlsFormatException("If DEFAULT is yes, then AUTOSELECT must be YES if it is present.")
                End If
            End If

            Dim ForcedString = attributes.GetAttribute("FORCED")?.Value
            Dim Forced As Boolean = False
            If ForcedString IsNot Nothing Then
                If Type <> MediaType.SUBTITLES Then
                    Throw New HlsFormatException($"{GetTagName()} must only contain FORCED for subtitle media.")
                End If
                Forced = HlsHelpers.ParseYesNoValue(ForcedString, "FORCED")
            End If

            ' Strings
            Dim InstreamId As String = attributes.GetAttribute("INSTREAM-ID")?.Value
            If InstreamId Is Nothing And Type = MediaType.CLOSED_CAPTIONS Then
                Throw New HlsFormatException($"{GetTagName()} must have INSTREAM-ID attribute for closed captions type.")
            ElseIf InstreamId IsNot Nothing And Type <> MediaType.CLOSED_CAPTIONS Then
                Throw New HlsFormatException($"{GetTagName()} must only have INSTREAM-ID attribute for closed captions type.")
            End If

            ' This should ideally validate that this exists if a master playlist has two renditions with the same codec,
            ' but that can't be done at this stage
            Dim Channels As String = attributes.GetAttribute("CHANNELS")?.Value

            Dim media As AlternativeRendition
            If Type = MediaType.CLOSED_CAPTIONS Then
                media = New ClosedCaptionRendition(GroupId, Name, Language, AssociatedLanguage, Characteristics, IsDefault, Autoselect, Forced, Channels, InstreamId)
            Else
                media = New LinkedRendition(Type, GroupId, Name, Language, AssociatedLanguage, Characteristics, IsDefault, Autoselect, Forced, Channels, uri)
            End If

            playlist.AddMedia(media)

        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-MEDIA"
        End Function
    End Class
End Namespace
