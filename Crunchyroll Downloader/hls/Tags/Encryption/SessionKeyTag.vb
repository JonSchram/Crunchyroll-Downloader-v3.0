Namespace hls.tags.encryption
    Public Class SessionKeyTag
        Inherits KeyTag
        Const TagName = "EXT-X-SESSION-KEY"

        Public Sub New(ByRef attributes As Tag)
            MyBase.New(attributes)

            If Method = EncryptionMethod.NONE Then
                Throw New HlsFormatException($"{TagName} requires the encryption method is not NONE")
            End If
        End Sub
    End Class
End Namespace