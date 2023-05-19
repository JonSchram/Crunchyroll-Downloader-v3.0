Imports Crunchyroll_Downloader.hls.parsing.tags.encryption

Namespace hls.parsing.tags
    Class HlsHelpers

        Public Shared Function ParseYesNoValue(Value As String, AttributeName As String) As Boolean
            Select Case Value
                Case "YES"
                    Return True
                Case "NO"
                    Return False
                Case Else
                    Throw New HlsFormatException($"{AttributeName} boolean value must be YES or NO, but was {Value}")
            End Select
        End Function

        Public Shared Function ToYesNoValue(Value As Boolean) As String
            Return If(Value, "YES", "NO")
        End Function

        Public Shared Function ConvertToString(type As PlaylistType) As String
            Select Case type
                Case PlaylistType.EVENT
                    Return "EVENT"
                Case PlaylistType.VOD
                    Return "VOD"
                Case Else
                    ' Not valid in a playlist file
                    Return ""
            End Select
        End Function

        Public Shared Function ConvertToString(method As EncryptionMethod) As String
            Select Case method
                Case EncryptionMethod.NONE
                    Return "NONE"
                Case EncryptionMethod.AES_128
                    Return "AES-128"
                Case EncryptionMethod.SAMPLE_AES
                    Return "SAMPLE-AES"
                Case Else
                    ' No other valid value exists.
                    Return "NONE"
            End Select
        End Function
    End Class

End Namespace