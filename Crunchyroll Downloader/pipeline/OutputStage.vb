Imports Crunchyroll_Downloader.data
Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.postprocess
Imports Crunchyroll_Downloader.preferences
Imports Crunchyroll_Downloader.utilities

Namespace pipeline
    Public Class OutputStage
        Inherits AbstractPipelineStage(Of List(Of MediaFileEntry), List(Of MediaFileEntry))

        Private ReadOnly Download As DownloadTask
        Private ReadOnly Filesystem As IFilesystem

        Public Sub New(stage As PipelineStage, Progress As IProgress(Of PipelineProgress), task As DownloadTask, filesystem As IFilesystem)
            MyBase.New(stage, Progress)
            Download = task
            Me.Filesystem = filesystem
        End Sub

        Protected Overrides Function Run(data As List(Of MediaFileEntry)) As Task(Of List(Of MediaFileEntry))
            Dim outputPrefs As OutputPreferences = New OutputPreferenceFactory().GetPreferences(Download)
            Dim outputProducer As New FinalOutputProducer(outputPrefs, Filesystem)

            Return Task.FromResult(outputProducer.ProcessInputs(data, Download.DownloadEpisode))
        End Function
    End Class
End Namespace