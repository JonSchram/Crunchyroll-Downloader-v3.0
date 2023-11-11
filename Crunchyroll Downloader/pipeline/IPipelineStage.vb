Imports Crunchyroll_Downloader.data

Namespace pipeline
    Public Interface IPipelineStage(Of InType, OutType)
        Function Process(data As InType) As Task(Of OutType)
    End Interface
End Namespace