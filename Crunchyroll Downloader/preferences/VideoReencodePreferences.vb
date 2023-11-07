Imports Crunchyroll_Downloader.settings
Imports Crunchyroll_Downloader.utilities.ffmpeg.codec
Imports Crunchyroll_Downloader.utilities.ffmpeg.preset

Namespace preferences
    ''' <summary>
    ''' Preferences for reencoding or remuxing streams to a video file.
    ''' </summary>
    Public Class VideoReencodePreferences
        Public Property TemporaryOutputPath As String

        Public Property VideoCodec As VideoCodec

        Public Property VideoPreset As IPreset

        Public Property TargetVideoBitrate As Integer?

        Public Property AudioCodec As AudioCodec

        Public Property MergeSoftSubtitles As Boolean

        Public Property SoftSubCodec As SubtitleCodec?

        ' TODO: Allow hard-subbing.

        Public Shared Function GetCodecForSubtitleMerge(mergeBehavior As SubtitleMerge) As SubtitleCodec?
            Select Case mergeBehavior
                Case SubtitleMerge.COPY
                    Return SubtitleCodec.COPY
                Case SubtitleMerge.MOV_TEXT
                    Return SubtitleCodec.MOV_TEXT
                Case SubtitleMerge.SRT
                    Return SubtitleCodec.SRT
                Case Else
                    ' There is no format for DISABLED
                    Return Nothing
            End Select
        End Function
    End Class
End Namespace