Namespace settings.general
    ''' <summary>
    ''' Enum specifying how to create folders for a downloaded episode.
    ''' </summary>
    Public Enum SubfolderBehavior As Integer
        ''' <summary>
        ''' Don't create any subfolders.
        ''' </summary>
        NO_SUBFOLDER = 0
        ''' <summary>
        ''' Create subfolders for series (show name) only.
        ''' </summary>
        USE_SERIES = 1
        ''' <summary>
        ''' Create subfolders for series (show name) and season number.
        ''' </summary>
        USE_SERIES_AND_SEASON = 2
        ''' <summary>
        ''' Use a manually specified folder instead of generating one automatically.
        ''' </summary>
        OVERRIDE_FOLDER = 3
    End Enum
End Namespace