Imports Crunchyroll_Downloader.hls.playlist

Namespace api.common
    ''' <summary>
    ''' Represents media that is stored in a master playlist. It contains additional URLs to process and download.
    ''' </summary>
    Public Class MasterPlaylistMedia
        Inherits Media

        Public ReadOnly Property MasterPlaylist As MasterPlaylist

        Public Sub New(type As MediaType, lang As Language, location As String, playlist As MasterPlaylist)
            MyBase.New(type, lang, location)
            MasterPlaylist = playlist
        End Sub

        Public Overrides Function ToString() As String
            Return $"[MasterPlaylistMedia  URI: {OriginalLocation}, Type: {Type}, Language: {MediaLanguage}," +
                $"Playlist: HLS version {MasterPlaylist.Version}, {MasterPlaylist.VariantStreams.Count} variant streams]"
        End Function
    End Class
End Namespace