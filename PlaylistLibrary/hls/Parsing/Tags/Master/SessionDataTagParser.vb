Imports PlaylistLibrary.hls.playlist

Namespace hls.parsing.tags.master

    ''' <summary>
    ''' Not used for anything, but included for completeness.
    ''' </summary>
    Public Class SessionDataTagParser
        Inherits TagParser(Of MasterPlaylist.Builder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As ParsedTag, playlist As MasterPlaylist.Builder)
            Dim dataId As String = attributes.GetAttribute("DATA-ID")?.Value
            Dim value As String = attributes.GetAttribute("VALUE")?.Value
            Dim uri As String = attributes.GetAttribute("URI")?.Value
            Dim language As String = attributes.GetAttribute("LANGUAGE")?.Value

            If value Is Nothing Xor uri Is Nothing Then
                Throw New HlsFormatException($"{GetTagName()} must contain either a VALUE attribute or a URI attribute but not both.")
            End If

            playlist.AddSessionData(New SessionData(dataId, value, uri, language))
        End Sub

        Public Overrides Function GetTagName() As String
            Return "EXT-X-SESSION-DATA"
        End Function
    End Class
End Namespace