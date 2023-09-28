Imports Crunchyroll_Downloader.hls.playlist

Namespace hls.parsing.tags

    ''' <summary>
    ''' Not used for anything, but included for completeness.
    ''' </summary>
    Public Class SessionDataTagParser
        Inherits TagParser(Of MasterPlaylist.Builder)

        Public Overrides Sub ParseInner(reader As IO.TextReader, attributes As TagAttributes, playlist As MasterPlaylist.Builder)
            Dim dataId As String = attributes.GetAttribute("DATA-ID")
            Dim value As String = attributes.GetAttribute("VALUE")
            Dim uri As String = attributes.GetAttribute("URI")
            Dim language As String = attributes.GetAttribute("LANGUAGE")

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