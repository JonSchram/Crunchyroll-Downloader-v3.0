Namespace utilities.ffmpeg
    Public Interface IFfmpegAdapter
        Sub AddCookie(name As String, value As String)

        ''' <summary>
        ''' Runs ffmpeg with the given arguments and returns a status code when complete.
        ''' </summary>
        ''' <param name="arguments"></param>
        ''' <returns></returns>
        Function Run(arguments As FfmpegArguments) As Task(Of Integer)
        Sub SetUserAgent(userAgent As String)
    End Interface
End Namespace
