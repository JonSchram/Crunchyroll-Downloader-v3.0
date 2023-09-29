Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.segment.encryption

Namespace hls.parsing
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


        Public Shared Function ParseYesValue(value As String, tagName As String, attributeName As String) As Boolean
            If "YES".Equals(value, StringComparison.OrdinalIgnoreCase) Then
                Return True
            End If
            Throw New HlsFormatException($"In {tagName}, {attributeName} must be YES if present.")
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

        Public Shared Function convertEncryptionToEnum(Method As String) As EncryptionMethod
            Select Case Method
                Case "AES-128"
                    Return EncryptionMethod.AES_128
                Case "SAMPLE-AES"
                    Return EncryptionMethod.SAMPLE_AES
                Case "NONE"
                    Return EncryptionMethod.NONE
                Case Else
                    Throw New HlsFormatException($"Encryption METHOD must be NONE, AES-128, or SAMPLE-AES but got {Method}")
            End Select
        End Function

        Public Shared Function ParseHdcp(HdcpString As String) As Hdcp
            Select Case HdcpString
                Case "TYPE-0"
                    Return Hdcp.TYPE_0
                Case "NONE"
                    Return Hdcp.NONE
                Case Else
                    Throw New HlsFormatException($"HDCP level format error, expected NONE or TYPE-0")
            End Select
        End Function

        Public Shared Function ParseResolution(resolutionString As String) As Resolution
            Dim dimensions() = Split(resolutionString, "x")
            If dimensions.Length = 0 Then
                Throw New HlsFormatException($"Resolution format error, expected format <horizontal>x<vertical>")
            End If
            Return New Resolution(CInt(dimensions(0)), CInt(dimensions(1)))
        End Function
    End Class

End Namespace