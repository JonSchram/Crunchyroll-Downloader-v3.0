Imports System.Text.RegularExpressions
Imports PlaylistLibrary.hls.common

Namespace hls.playlist
    Public Class CodecClassifier
        Public Shared Function ClassifyCodec(codec As String) As MediaType
            Dim audioRegex = New Regex("^mp4a")
            Dim videoRegex = New Regex("^(avc|svc|mvc|mp4v)")

            If audioRegex.IsMatch(codec) Then
                Return MediaType.AUDIO
            ElseIf videoRegex.IsMatch(codec) Then
                Return MediaType.VIDEO
            Else
                Return MediaType.VIDEO
            End If
        End Function

        Public Shared Function ClassifyCodecs(codecs As IEnumerable(Of String)) As ISet(Of MediaType)
            Dim result As New HashSet(Of MediaType)
            For Each codec In codecs
                result.Add(ClassifyCodec(codec))
            Next
            Return result
        End Function
    End Class
End Namespace