Imports PlaylistLibrary.hls.segment
Imports PlaylistLibrary.hls.segment.encryption

Namespace hls.parsing
    Public Class KeyParser

        Public Shared Function ParseEncryptionKey(attributes As ParsedTag, tagName As String) As EncryptionKey
            ' Requirements adapted from IETF RFC 8216: https://www.rfc-editor.org/rfc/rfc8216.html

            Dim methodAttribute = attributes.GetAttribute("METHOD")
            If methodAttribute Is Nothing Then
                Throw New HlsFormatException($"Parse failure - {tagName} requires encryption method to be specified.")
            End If
            Dim Method As EncryptionMethod = HlsHelpers.convertEncryptionToEnum(methodAttribute.Value)

            ' Required unless the encryption method is NONE
            Dim Uri As String = attributes.GetAttribute("URI")?.Value
            If Uri Is Nothing And Method <> EncryptionMethod.NONE Then
                Throw New HlsFormatException($"{tagName} uri must be specified if the encryption method is not NONE")
            End If

            ' IV is a 128-bit number. Requires more bits than a long int. Only needed in some circumstances.
            Dim IvAttribute As PlaylistData = attributes.GetAttribute("IV")
            Dim InitializationVector As Decimal? = Nothing
            If IvAttribute?.Value?.StartsWith("0x", StringComparison.OrdinalIgnoreCase) Then
                ' HexNumber does not allow a leading '0x'
                InitializationVector = Decimal.Parse(IvAttribute.Value.Remove(0, 2), Globalization.NumberStyles.HexNumber)
            End If

            ' Optional. Unknown what other key formats may exist but it seems it could be any string.
            Dim KeyFormat As String = If(attributes.GetAttribute("KEYFORMAT")?.Value, "identity")

            ' Optional, requires compatibility version 5 or greater.
            Dim KeyFormatVersions = If(attributes.GetAttribute("KEYFORMATVERSIONS")?.Value, "1")

            Return New EncryptionKey(Method, Uri, InitializationVector, KeyFormat, KeyFormatVersions)
        End Function
    End Class
End Namespace