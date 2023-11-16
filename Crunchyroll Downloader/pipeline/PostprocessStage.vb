Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.postprocess
Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.settings
Imports Crunchyroll_Downloader.settings.ffmpeg
Imports Crunchyroll_Downloader.utilities
Imports Crunchyroll_Downloader.utilities.ffmpeg

Namespace pipeline
    Public Class PostprocessStage
        Inherits AbstractPipelineStage(Of List(Of MediaFileEntry), List(Of MediaFileEntry))

        Private ReadOnly TemporaryFolder As String
        Private ReadOnly OutputFormat As Format
        Private ReadOnly FfmpegSetting As FfmpegOptions
        Private ReadOnly FfmpegAdapter As IFfmpegAdapter
        Private ReadOnly FileSystem As IFilesystem

        Public Sub New(stage As PipelineStage, Progress As IProgress(Of PipelineProgress), temporaryFolder As String, outputFormat As Format,
                       ffmpegSetting As FfmpegOptions, ffmpegAdapter As IFfmpegAdapter, filesystem As IFilesystem)
            MyBase.New(stage, Progress)
            Me.TemporaryFolder = temporaryFolder
            Me.OutputFormat = outputFormat
            Me.FfmpegSetting = ffmpegSetting
            Me.FfmpegAdapter = ffmpegAdapter
            Me.FileSystem = filesystem
        End Sub

        Protected Overrides Async Function Run(data As List(Of MediaFileEntry)) As Task(Of List(Of MediaFileEntry))
            Dim reencodePreferences As VideoReencodePreferences =
                ReencodePreferenceFactory.GetVideoReencodePreferences(TemporaryFolder, OutputFormat, FfmpegSetting)
            Dim fileFormat As ContainerFormat = OutputFormat.GetVideoFormat()

            ' TODO: Set stream metadata such as stream language, default track (ffmpeg calls it 'disposition').

            Dim postprocessor As VideoPostprocessor = Nothing
            If fileFormat = ContainerFormat.MP4 Then
                postprocessor = New Mp4Postprocessor(reencodePreferences, FfmpegAdapter, FileSystem)
            ElseIf fileFormat = ContainerFormat.MKV Then
                postprocessor = New MkvPostprocessor(reencodePreferences, FfmpegAdapter, FileSystem)
            End If

            If postprocessor IsNot Nothing Then
                AddHandler postprocessor.ReportFfmpegProgress, AddressOf HandleEncodeProgress
                AddHandler postprocessor.ReportFfmpegComplete, AddressOf HandleEncodeFinished
                Return Await postprocessor.ProcessInputs(data.ToList())
            End If

            Return Nothing
        End Function

        Private Sub HandleEncodeProgress(progress As Double)
            ReportSubStageProgress(0, 1, progress)
        End Sub

        Private Sub HandleEncodeFinished()
            ReportSubStageFinished(0, 1)
        End Sub
    End Class
End Namespace