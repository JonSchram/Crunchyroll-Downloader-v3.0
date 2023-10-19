Namespace utilities
    Public Interface IFfmpegAdapter
        Sub AddCookie(name As String, value As String)
        Sub Run(arguments As FfmpegArguments)
        Sub SetUserAgent(userAgent As String)
    End Interface
End Namespace
