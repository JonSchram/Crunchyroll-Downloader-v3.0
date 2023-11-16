Imports Crunchyroll_Downloader.preferences
Imports SiteAPI.api
Imports SiteAPI.api.common
Imports SiteAPI.api.metadata

Namespace pipeline
    Public Class RetrieveMediaStage
        Inherits AbstractPipelineStage(Of Episode, List(Of MediaLink))

        Private ReadOnly Client As IDownloadClient

        Public Sub New(Stage As PipelineStage, Progress As IProgress(Of PipelineProgress), client As IDownloadClient)
            MyBase.New(Stage, Progress)
            Me.Client = client
        End Sub

        Protected Overrides Async Function Run(data As Episode) As Task(Of List(Of MediaLink))
            Console.WriteLine($"Getting media for {data}")

            Dim preferences As MediaPreferences = Nothing
            Select Case Client.GetSite()
                Case Site.FUNIMATION
                    preferences = New FunimationPreferenceFactory().GetCurrentPreferences()
                Case Site.CRUNCHYROLL

            End Select

            Return Await Client.GetAvailableMedia(data, preferences)
        End Function
    End Class
End Namespace