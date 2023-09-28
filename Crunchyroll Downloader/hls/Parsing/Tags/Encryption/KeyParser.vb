Imports Crunchyroll_Downloader.hls.segment
Imports Crunchyroll_Downloader.hls.segment.encryption

Namespace hls.parsing.tags.encryption
    Public Class KeyParser

        Public Shared Function ParseEncryptionKey(attributes As TagAttributes, tagName As String) As EncryptionKey
            ' Requirements adapted from IETF RFC 8216: https://www.rfc-editor.org/rfc/rfc8216.html

            Dim methodString = attributes.GetAttribute("METHOD")
            If methodString Is Nothing Then
                Throw New HlsFormatException($"Parse failure - {tagName} requires encryption method to be specified.")
            End If
            Dim Method As EncryptionMethod = HlsHelpers.convertEncryptionToEnum(methodString)

            ' Required unless the encryption method is NONE
            Dim Uri As String = attributes.GetAttribute("URI")
            If Uri Is Nothing And Method <> EncryptionMethod.NONE Then
                Throw New HlsFormatException($"{tagName} uri must be specified if the encryption method is not NONE")
            End If

            ' IV is a 128-bit number. Requires more bits than a long int. Only needed in some circumstances.
            Dim IvString = attributes.GetAttribute("IV")
            Dim InitializationVector As Decimal? = Nothing
            If IvString IsNot Nothing AndAlso IvString.StartsWith("0x", StringComparison.OrdinalIgnoreCase) Then
                ' HexNumber does not allow a leading '0x'
                InitializationVector = Decimal.Parse(IvString.Remove(0, 2), Globalization.NumberStyles.HexNumber)
            End If

            ' Optional. Unknown what other key formats may exist but it seems it could be any string.
            Dim KeyFormat As String = If(attributes.GetAttribute("KEYFORMAT"), "identity")

            ' Optional, requires compatibility version 5 or greater.
            Dim KeyFormatVersions = If(attributes.GetAttribute("KEYFORMATVERSIONS"), "1")

            Return New EncryptionKey(Method, Uri, InitializationVector, KeyFormat, KeyFormatVersions)
        End Function
    End Class
End Namespace