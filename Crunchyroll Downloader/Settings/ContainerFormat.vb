Namespace settings
    Public Enum ContainerFormat As Integer
        MP4 = 0
        MKV = 1
        ' TODO: This is recently moved to the individual download page
        ' Need to deprecate and modify references to audio-only mode to use the new setting (and since this is a WIP, delete AAC only).
        AAC_AUDIO_ONLY = 2
    End Enum
End Namespace