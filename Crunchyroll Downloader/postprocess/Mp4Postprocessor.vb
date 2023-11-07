Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.utilities
Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports Crunchyroll_Downloader.utilities.ffmpeg.codec

Namespace postprocess

    ''' <summary>
    ''' Class that remuxes or reencodes any video / audio files if desired.
    ''' </summary>
    Public Class Mp4Postprocessor
        Inherits VideoPostprocessor

        Public Sub New(prefs As VideoReencodePreferences, ffmpegRunner As IFfmpegAdapter, fileSystemApi As IFilesystem)
            MyBase.New(prefs, ffmpegRunner, fileSystemApi)
        End Sub

        Protected Overrides Function GetFileExtension() As String
            Return ".mp4"
        End Function

        Protected Overrides Function GetAllowedSubtitleCodecs() As List(Of SubtitleCodec)
            Return New List(Of SubtitleCodec) From {SubtitleCodec.COPY, SubtitleCodec.MOV_TEXT}
        End Function
    End Class
End Namespace