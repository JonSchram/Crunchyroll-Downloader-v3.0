Imports System.Configuration

Namespace settings
    <Serializable>
    <SettingsSerializeAs(SettingsSerializeAs.Xml)>
    Public Class Format
        Private Property SubtitleFormat As SubtitleMerge
        Private Property VideoFormat As MediaFormat

        Public Sub New(FileFormat As MediaFormat, MergeSetting As SubtitleMerge)
            Dim allowedMergeSettings = GetValidSubtitleFormats(FileFormat)
            If Not allowedMergeSettings.Contains(MergeSetting) Then
                Throw New ArgumentException($"Merge setting {MergeSetting} not valid with {FileFormat}")
            End If

            Me.VideoFormat = FileFormat
            Me.SubtitleFormat = MergeSetting
        End Sub

        Public Shared Function GetValidSubtitleFormats(Format As MediaFormat) As List(Of SubtitleMerge)
            Select Case Format
                Case MediaFormat.MP4
                    Return New List(Of SubtitleMerge)({SubtitleMerge.DISABLED, SubtitleMerge.MOV_TEXT})
                Case MediaFormat.MKV
                    Return New List(Of SubtitleMerge)({SubtitleMerge.DISABLED, SubtitleMerge.COPY, SubtitleMerge.SRT})
                Case MediaFormat.AAC_AUDIO_ONLY
                    Return New List(Of SubtitleMerge)({SubtitleMerge.DISABLED})
                Case Else
                    Return New List(Of SubtitleMerge)()
            End Select
        End Function

        Public Function GetVideoFormat() As MediaFormat
            Return VideoFormat
        End Function

        Public Function GetSubtitleFormat() As SubtitleMerge
            Return SubtitleFormat
        End Function

        Public Function GetFileExtension() As String
            Select Case VideoFormat
                Case MediaFormat.MP4
                    Return "mp4"
                Case MediaFormat.MKV
                    Return "mkv"
                Case Else
                    Return "aac"
            End Select
        End Function

        Public Enum MediaFormat As Integer
            MP4 = 0
            MKV = 1
            ' TODO: This is recently moved to the individual download page
            ' Need to deprecate and modify references to audio-only mode to use the new setting (and since this is a WIP, delete AAC only).
            AAC_AUDIO_ONLY = 2
        End Enum

        Public Enum SubtitleMerge As Integer
            DISABLED = 0
            MOV_TEXT = 1
            COPY = 2
            SRT = 3
        End Enum
    End Class
End Namespace