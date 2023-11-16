Imports Crunchyroll_Downloader.settings.general

Namespace preferences
    Public Class DownloadPreferences
        Public Property TemporaryDirectory As String

        Public Property PreferredResolution As Resolution

        ''' <summary>
        ''' Whether to get a higher resolution video than preferred if the preferred resolution cannot be found.
        ''' </summary>
        ''' <returns></returns>
        Public Property AcceptHigherResolution As Boolean

        ''' <summary>
        ''' Whether to get the highest bitrate version of a video when there is an option.
        ''' </summary>
        ''' <returns></returns>
        Public Property PreferHighBitrate As Boolean

    End Class
End Namespace