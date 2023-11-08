Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.settings
Imports Crunchyroll_Downloader.settings.ffmpeg
Imports Crunchyroll_Downloader.settings.ffmpeg.encoding
Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports Crunchyroll_Downloader.utilities.ffmpeg.codec
Imports Crunchyroll_Downloader.utilities.ffmpeg.preset

Namespace postprocess
    Public Class ReencodePreferenceFactory
        Public Shared Function GetVideoReencodePreferences(temporaryPath As String, fileFormat As Format, ffmpegSetting As FfmpegOptions) As VideoReencodePreferences
            Dim subs As SubtitleMerge = fileFormat.GetSubtitleFormat()
            Dim mergeSubs As Boolean = subs <> SubtitleMerge.DISABLED
            Dim subCodec As SubtitleCodec? = VideoReencodePreferences.GetCodecForSubtitleMerge(subs)
            Dim encoder As VideoEncoder = ffmpegSetting.GetActiveEncoder()
            Dim videoCodec As VideoCodec = SettingsConverter.VideoCodecFromSettings(encoder)
            Dim speedPreset As IPreset = SettingsConverter.PresetFromSettings(encoder)

            Return New VideoReencodePreferences() With {
                        .TemporaryOutputPath = temporaryPath,
                        .MergeSoftSubtitles = mergeSubs,
                        .SoftSubCodec = subCodec,
                        .AudioCodec = AudioCodec.COPY,
                        .VideoCodec = videoCodec,
                        .VideoPreset = speedPreset
                    }
        End Function
    End Class
End Namespace