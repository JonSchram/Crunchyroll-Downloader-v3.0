Imports Crunchyroll_Downloader.download
Imports SiteAPI.api
Imports SiteAPI.api.common

Namespace pipeline
    Public Class CalculateSelectionStage
        Inherits AbstractPipelineStage(Of List(Of MediaLink), Selection)

        Private ReadOnly Client As IDownloadClient

        Public Sub New(stage As PipelineStage, Progress As IProgress(Of PipelineProgress), client As IDownloadClient)
            MyBase.New(stage, Progress)
            Me.Client = client
        End Sub

        Protected Overrides Async Function Run(data As List(Of MediaLink)) As Task(Of Selection)
            Dim resolvedMedia As New List(Of Media)

            For Each item As MediaLink In data
                Debug.WriteLine($"Resolving media link: {item}")
                Dim resolvedItem As Media = Await Client.ResolveMediaLink(item)
                Debug.WriteLine($"Finished resolving media. Resolved as ")
                resolvedMedia.Add(resolvedItem)
            Next

            Return New Selection(resolvedMedia)
        End Function
    End Class
End Namespace