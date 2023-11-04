Imports System.Configuration

Namespace settings
    <Serializable>
    <SettingsSerializeAs(SettingsSerializeAs.Xml)>
    Public Class Format
        Private Property SubtitleFormat As SubtitleMerge
        Private Property VideoFormat As ContainerFormat

        Public Sub New(FileFormat As ContainerFormat, MergeSetting As SubtitleMerge)
            Dim allowedMergeSettings = GetValidSubtitleFormats(FileFormat)
            If Not allowedMergeSettings.Contains(MergeSetting) Then
                Throw New ArgumentException($"Merge setting {MergeSetting} not valid with {FileFormat}")
            End If

            VideoFormat = FileFormat
            SubtitleFormat = MergeSetting
        End Sub

        Public Shared Function GetValidSubtitleFormats(Format As ContainerFormat) As List(Of SubtitleMerge)
            Select Case Format
                Case ContainerFormat.MP4
                    Return New List(Of SubtitleMerge)({SubtitleMerge.DISABLED, SubtitleMerge.MOV_TEXT})
                Case ContainerFormat.MKV
                    Return New List(Of SubtitleMerge)({SubtitleMerge.DISABLED, SubtitleMerge.COPY, SubtitleMerge.SRT})
                Case ContainerFormat.AAC_AUDIO_ONLY
                    Return New List(Of SubtitleMerge)({SubtitleMerge.DISABLED})
                Case Else
                    Return New List(Of SubtitleMerge)()
            End Select
        End Function

        Public Function GetVideoFormat() As ContainerFormat
            Return VideoFormat
        End Function

        Public Function GetSubtitleFormat() As SubtitleMerge
            Return SubtitleFormat
        End Function

        Public Function GetFileExtension() As String
            Select Case VideoFormat
                Case ContainerFormat.MP4
                    Return "mp4"
                Case ContainerFormat.MKV
                    Return "mkv"
                Case Else
                    Return "aac"
            End Select
        End Function
    End Class
End Namespace