Namespace settings.general
    Public Enum Resolution As Integer
        WORST = 0
        RESOLUTION_360P = 360
        RESOLUTION_480P = 480
        RESOLUTION_720P = 720
        RESOLUTION_1080P = 1080
        ' There is no 4K option, but if there ever is, this can be easily extended without breaking anything.
        BEST = 9999
    End Enum
End Namespace