Imports Crunchyroll_Downloader.hls.common

Namespace hls.parsing.tags
    Public Class StartTag
        Const TagName = "EXT-X-START"

        ' Required
        Public ReadOnly Property TimeOffset As Double

        ' Optional
        Public ReadOnly Property Precise As Boolean = False

        Public Sub New(SourceTag As TagAttributes)
            If SourceTag.getTagName() <> TagName Then
                Throw New HlsFormatException($"Provided tag {SourceTag.getTagName()} does not match required tag name {TagName}.")
            End If

            Dim offsetString = SourceTag.GetAttribute("TIME-OFFSET")
            If offsetString Is Nothing Then
                Throw New HlsFormatException($"{TagName} requires TIME-OFFSET to be set")
            End If
            TimeOffset = CDbl(offsetString)

            Dim PreciseString = SourceTag.GetAttribute("PRECISE")
            If offsetString IsNot Nothing Then
                Precise = HlsHelpers.ParseYesNoValue(PreciseString, "PRECISE")
            End If
        End Sub

        Public Sub New(other As StartTag)
            TimeOffset = other.TimeOffset
            Precise = other.Precise
        End Sub

        Public Function GetPlaylistStartTime() As PlaylistStartTime
            Return New PlaylistStartTime(TimeOffset, Precise)
        End Function

        Public Overrides Function ToString() As String
            Return $"{{
TimeOffset: {TimeOffset},
Precise: {Precise}
}}"

        End Function
    End Class

End Namespace
