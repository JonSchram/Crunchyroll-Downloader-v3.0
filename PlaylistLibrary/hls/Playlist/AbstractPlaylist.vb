Imports PlaylistLibrary.hls.common

Namespace hls.playlist
    Public MustInherit Class AbstractPlaylist

        Public ReadOnly Property Version As Integer
        Public ReadOnly Property IndependentSegments As Boolean
        Public ReadOnly Property StartPlayTime As PlaylistStartTime

        Public Sub New(other As AbstractPlaylist)
            Version = other.Version
            IndependentSegments = other.IndependentSegments
            If other.StartPlayTime IsNot Nothing Then
                StartPlayTime = New PlaylistStartTime(other.StartPlayTime)
            End If
        End Sub

        Protected Sub New(version As Integer, independentSegments As Boolean, startPlayTime As PlaylistStartTime)
            Me.Version = version
            Me.IndependentSegments = independentSegments
            Me.StartPlayTime = startPlayTime
        End Sub

        Public MustInherit Class AbstractPlaylistBuilder
            Protected Property Version As Integer
            Protected Property IndependentSegments As Boolean
            Protected Property StartPlayTime As PlaylistStartTime

            Public Sub SetVersion(version As Integer)
                Me.Version = version
            End Sub

            Public Sub SetIndependentSegments()
                IndependentSegments = True
            End Sub

            Public Sub SetStart(start As PlaylistStartTime)
                StartPlayTime = start
            End Sub
        End Class

    End Class

End Namespace