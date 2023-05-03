Namespace api.client.stream
    ''' <summary>
    ''' How a media stream is formatted. A stream might exist as a single file that can be downloaded
    ''' all at once (most likely subtitles) or a file split into many segments.
    ''' </summary>
    Public Enum StreamFormat
        COMPLETE_FILE
        SEGMENTS
    End Enum
End Namespace
