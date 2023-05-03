Namespace api.client.stream
    ''' <summary>
    ''' Specifies what kind of content is contained in a stream. Acts as bitwise flags because a stream may
    ''' contain multiple types of content.
    ''' </summary>
    <Flags()>
    Public Enum MediaType As Integer
        Video = 1
        Audio = 2
        subtitles = 4
    End Enum
End Namespace