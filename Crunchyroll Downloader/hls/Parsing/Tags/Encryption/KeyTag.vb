Imports System.Text.RegularExpressions
Imports Crunchyroll_Downloader.hls.segment
Imports Crunchyroll_Downloader.hls.segment.encryption

Namespace hls.parsing.tags.encryption
    Public Class KeyTag
        Const TagName = "EXT-X-KEY"

        ' Parameter comments adapted from IETF RFC 8216: https://www.rfc-editor.org/rfc/rfc8216.html

        ' Required
        Public ReadOnly Property Method As EncryptionMethod

        ' Required unless the encryption method is NONE
        Public ReadOnly Property Uri As String

        ' A 128-bit number. Requires more bits than a long int. Only needed in some circumstances.
        Public ReadOnly Property InitializationVector As Decimal?

        ' Optional. Unknown what other key formats may exist but it seems it could be any string.
        Public ReadOnly Property KeyFormat As String = "identity"
        ' Optional, requires compatibility version 5 or greater.
        Public ReadOnly Property KeyFormatVersions As String = "1"


        Public Sub New(ByRef attributes As TagAttributes)
            Dim methodString = attributes.GetAttribute("METHOD")
            If methodString IsNot Nothing Then
                Method = HlsHelpers.convertEncryptionToEnum(methodString)
            End If

            Uri = attributes.GetAttribute("URI")
            If Uri Is Nothing And Method <> EncryptionMethod.NONE Then
                Throw New HlsFormatException($"{TagName} uri must be specified if the encryption method is not NONE")
            End If

            Dim IvString = attributes.GetAttribute("IV")
            If IvString IsNot Nothing And IvString.StartsWith("0x", StringComparison.OrdinalIgnoreCase) Then
                ' HexNumber does not allow a leading '0x'
                Decimal.Parse(IvString.Remove(0, 2), Globalization.NumberStyles.HexNumber)
            End If

            ' Don't want to overwrite the implied default value
            Dim formatString = attributes.GetAttribute("KEYFORMAT")
            If formatString IsNot Nothing Then
                KeyFormat = formatString
            End If

            Dim formatVersions = attributes.GetAttribute("KEYFORMATVERSIONS")
            If formatVersions IsNot Nothing Then
                KeyFormatVersions = formatVersions
            End If
        End Sub

        Public Sub New(other As KeyTag)
            Method = other.Method
            Uri = other.Uri
            InitializationVector = other.InitializationVector
            KeyFormat = other.KeyFormat
            KeyFormatVersions = other.KeyFormatVersions
        End Sub

        Public Function GetEncryptionKey() As EncryptionKey
            Return New EncryptionKey(Method, Uri, InitializationVector, KeyFormat, KeyFormatVersions)
        End Function

        Public Overrides Function ToString() As String
            Return $"{{
  Method: {Method},
  Uri: {Uri},
  InitializationVector: {InitializationVector},
  KeyFormat: {KeyFormat},
  KeyFormatVersions: {KeyFormatVersions}
}}"
        End Function
    End Class
End Namespace