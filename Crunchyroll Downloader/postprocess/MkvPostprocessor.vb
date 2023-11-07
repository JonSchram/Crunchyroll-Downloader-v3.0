Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.utilities
Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports Crunchyroll_Downloader.utilities.ffmpeg.codec

Namespace postprocess
    ''' <summary>
    ''' Class that remuxes or reencodes any video file with audio and subtitles into an mkv file.
    ''' </summary>
    Public Class MkvPostprocessor
        Inherits VideoPostprocessor

        Public Sub New(prefs As VideoReencodePreferences, ffmpegRunner As IFfmpegAdapter, filesystemApi As IFilesystem)
            MyBase.New(prefs, ffmpegRunner, filesystemApi)
        End Sub

        Protected Overrides Function GetFileExtension() As String
            Return ".mkv"
        End Function

        Protected Overrides Function GetAllowedSubtitleCodecs() As List(Of SubtitleCodec)
            Return New List(Of SubtitleCodec) From {SubtitleCodec.COPY, SubtitleCodec.ASS, SubtitleCodec.SRT, SubtitleCodec.SSA}
        End Function
    End Class
End Namespace