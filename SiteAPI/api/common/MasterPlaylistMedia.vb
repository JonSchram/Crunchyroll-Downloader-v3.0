

Imports PlaylistLibrary.hls.playlist

Namespace api.common
    ''' <summary>
    ''' Represents media that is stored in a master playlist. It contains additional URLs to process and download.
    ''' </summary>
    Public Class MasterPlaylistMedia
        Inherits Media

        Public ReadOnly Property Playlist As MasterPlaylist

        Public Sub New(type As MediaType, locale As Locale, location As String, playlist As MasterPlaylist)
            MyBase.New(type, locale, location)
            Me.Playlist = playlist
        End Sub

        Public Overrides Function ToString() As String
            Return $"[MasterPlaylistMedia  URI: {OriginalLocation}, Type: {Type}, Locale: {MediaLocale}," +
                $"Playlist: HLS version {Playlist.Version}, {Playlist.VariantStreams.Count} variant streams]"
        End Function
    End Class
End Namespace