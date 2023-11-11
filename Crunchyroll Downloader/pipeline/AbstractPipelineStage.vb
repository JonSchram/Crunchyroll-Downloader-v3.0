Namespace pipeline
    ''' <summary>
    ''' A pipeline stage that reports progress before and after completing.
    ''' </summary>
    ''' <typeparam name="InType"></typeparam>
    ''' <typeparam name="OutType"></typeparam>
    Public MustInherit Class AbstractPipelineStage(Of InType, OutType)
        Implements IPipelineStage(Of InType, OutType)

        Protected ReadOnly Stage As PipelineStage

        Protected ReadOnly Progress As IProgress(Of PipelineProgress)

        ' A little bit hacky, but the COMPLETED stage is always last, so we can use it to know what percent of stages have finished.
        Private Shared ReadOnly NumberOfStages As Integer = PipelineStage.COMPLETED

        Public Sub New(stage As PipelineStage, Progress As IProgress(Of PipelineProgress))
            Me.Stage = stage
            Me.Progress = Progress
        End Sub


        Protected MustOverride Async Function Run(data As InType) As Task(Of OutType)

        Public Async Function Process(data As InType) As Task(Of OutType) Implements IPipelineStage(Of InType, OutType).Process
            Progress.Report(PipelineProgress.CreateStageStart(Stage, CalculateStartingProgress()))
            Dim result As OutType = Await Run(data)
            Progress.Report(PipelineProgress.CreateStageComplete(Stage, CalculateTotalProgress()))
            Return result
        End Function

        ' TODO: Allow reporting sub-stage, which is just divides the existing stage into equally-sized pieces.
        ' A sub-stage would be a milestone (such as finished downloading one file).
        ' Should be more accurate than combining the milestones into the same stage, because if one sub-stage takes a long time
        ' and another is nearly instant, the estimate would be way off.

        Private Function CalculateStartingProgress() As Integer
            Return CInt(100 * (Stage - 1) / NumberOfStages)
        End Function

        Private Function CalculateTotalProgress() As Integer
            Return CInt(100 * Stage / NumberOfStages)
        End Function

        Private Function CalculateSubStageProgress(Stage As Integer, subStage As Integer, totalSubStages As Integer) As Integer
            Return CInt(100 * ((Stage - 1) / NumberOfStages + subStage / totalSubStages))
        End Function

        Protected Sub ReportSubStageProgress(subStageNumber As Integer, totalSubStages As Integer, progress As Integer)
            Dim totalProgress As Integer = CalculateSubStageProgress(Stage, subStageNumber, totalSubStages)
            Me.Progress.Report(New PipelineProgress(totalProgress, progress))
            Debug.WriteLine($"Download thread progress reported: Sub-stage {subStageNumber} of {totalSubStages}, {progress}%")
        End Sub
        Protected Sub ReportSubStageFinished(subStageNumber As Integer, totalSubStages As Integer)
            ' TODO: Maybe make a dedicated sub-stage representation.
            ' This is only used so that the progress estimator will reset the current estimated time.
            Dim totalProgress As Integer = CalculateSubStageProgress(Stage, subStageNumber, totalSubStages)
            Progress.Report(PipelineProgress.CreateStageComplete(Stage, totalProgress))
            Debug.WriteLine($"Download thread download complete: {subStageNumber}")
        End Sub
    End Class
End Namespace