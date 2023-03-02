''' <summary>
''' A video playback option sent by the server.
''' </summary>
Public Class Playback
    Public Property VideoId As String

    ' The URL that must be used to download the playlist for this video
    Public Property PlaylistPath As String

    Public Property Subtitles As List(Of Subtitle)

End Class
