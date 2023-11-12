Namespace utilities.ffmpeg
    Public Interface IFfmpegAdapter

        Event ReportProgress(percent As Double)


        ''' <summary>
        ''' Runs ffmpeg with the given arguments and returns a status code when complete.
        ''' </summary>
        ''' <param name="arguments"></param>
        ''' <returns></returns>
        Function Run(arguments As FfmpegArguments) As Task(Of Integer)
    End Interface
End Namespace
