Namespace hls.tags.encryption
    Public Class KeyTag
        Const TagName = "EXT-X-KEY"

        ' Parameter comments adapted from IETF RFC 8216: https://www.rfc-editor.org/rfc/rfc8216.html

        ' Required
        Public ReadOnly Property Method As EncryptionMethod

        ' Required unless the encryption method is NONE
        Public ReadOnly Property Uri As String

        ' Technically a 128-bit number but there is no Int128 type
        Public ReadOnly Property InitializationVector As String

        ' Optional
        Public ReadOnly Property KeyFormat As String = "identity"
        Public ReadOnly Property KeyFormatVersions As String = "1"


        Public Sub New(ByRef attributes As Tag)
            Dim methodString = attributes.GetAttribute("METHOD")
            If methodString IsNot Nothing Then
                Method = convertEncryptionToEnum(methodString)
            End If

            Uri = attributes.GetAttribute("URI")
            If Uri Is Nothing And Method <> EncryptionMethod.NONE Then
                Throw New HlsFormatException($"{TagName} uri must be specified if the encryption method is not NONE")
            End If

            InitializationVector = attributes.GetAttribute("IV")

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

        Public Overrides Function ToString() As String
            Return $"{{
  Method: {Method},
  Uri: {Uri},
  InitializationVector: {InitializationVector},
  KeyFormat: {KeyFormat},
  KeyFormatVersions: {KeyFormatVersions}
}}"
        End Function

        Private Function convertEncryptionToEnum(Method As String) As EncryptionMethod
            Select Case Method
                Case "AES-128"
                    Return EncryptionMethod.AES_128
                Case "SAMPLE-AES"
                    Return EncryptionMethod.SAMPLE_AES
                Case "NONE"
                    Return EncryptionMethod.NONE
                Case Else
                    Throw New HlsFormatException($"{TagName} METHOD must be NONE, AES-128, or SAMPLE-AES but got {Method}")
            End Select
        End Function
    End Class
End Namespace