Imports Crunchyroll_Downloader.settings
Imports Crunchyroll_Downloader.settings.ffmpeg

Namespace preferences
    Public Class ReencodePreferences
        Public Property OutputFormat As Format.ContainerFormat

        Public Property TemporaryOutputPath As String

        Public Property SubtitleBehavior As Format.SubtitleMerge

        Public Property PostprocessSettings As FfmpegOptions
        ' TODO: Allow outputting only an audio stream, if the audio has been combined with video and couldn't be separated until now.
    End Class
End Namespace