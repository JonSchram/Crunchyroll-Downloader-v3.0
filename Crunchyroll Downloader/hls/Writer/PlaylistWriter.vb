Imports System.IO
Imports Crunchyroll_Downloader.hls.playlist

Namespace hls.writer
    Public Class PlaylistWriter

        Public Sub WriteToStream(playlist As MediaPlaylist, output As Stream)
            Dim printStream = New StreamWriter(output)

        End Sub

        Private Sub WriteHeader(printStream As StreamWriter)
            printStream.WriteLine("#EXTM3U")



        End Sub
    End Class
End Namespace