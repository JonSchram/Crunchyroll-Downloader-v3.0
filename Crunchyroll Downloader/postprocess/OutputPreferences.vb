Imports Crunchyroll_Downloader.settings
Imports Crunchyroll_Downloader.settings.ffmpeg

Namespace postprocess
    Public Class OutputPreferences
        ''' <summary>
        ''' The path to save the completed file, minus the file name.
        ''' </summary>
        ''' <returns></returns>
        Public Property OutputPath As String
        Public Property SubtitleBehavior As Format.SubtitleMerge
        Public Property OutputFormat As Format.MediaFormat

        Public Property PostprocessSettings As FfmpegOptions

        Public Property NameTemplate As String
        Public Property UseKodiNaming As Boolean

        ' TODO: Allow outputting only an audio stream, if the audio has been combined with video and couldn't be separated until now.
    End Class
End Namespace
