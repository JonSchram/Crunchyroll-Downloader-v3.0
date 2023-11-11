Namespace pipeline

    Public Class PipelineProgress
        Public Property TotalPercent As Integer?
        Public Property StagePercent As Integer?
        Public Property Stage As PipelineStage?

        Public Property StageCompleted As Boolean?
        Public Property StageStarted As Boolean?

        Public Property Completed As Boolean?

        Public Sub New()
            Completed = False
        End Sub

        Public Sub New(totalPercent As Integer, stagePercent As Integer)
            Me.TotalPercent = totalPercent
            Me.StagePercent = stagePercent
            Completed = False
        End Sub

        Public Shared Function CreateStageStart(stage As PipelineStage, totalProgress As Integer) As PipelineProgress
            Return New PipelineProgress() With {
                    .Stage = stage,
                    .StageStarted = True,
                    .TotalPercent = totalProgress
                }
        End Function

        Public Shared Function CreateStageComplete(Stage As PipelineStage, totalProgress As Integer) As PipelineProgress
            Return New PipelineProgress() With {
                    .Stage = Stage,
                    .StageCompleted = True,
                    .StagePercent = 100,
                    .TotalPercent = totalProgress
                }
        End Function

        Public Shared Function CreateCompleted() As PipelineProgress
            Return New PipelineProgress() With {
                    .Completed = True,
                    .TotalPercent = 100,
                    .StagePercent = 100
                }
        End Function

        Public Overrides Function ToString() As String
            Return $"[PipelineProgress: total percent: {TotalPercent}, current percent: {StagePercent}, current stage: {Stage}, " +
                $"stage complete: {StageCompleted}, stage started: {StageStarted}, completed: {Completed}"
        End Function
    End Class
End Namespace