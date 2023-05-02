Imports Crunchyroll_Downloader.hls.tags

Namespace hls.playlist
    Public Class AbstractPlaylist
        Public Property Version As Integer
        Public Property IndependentSegments As Boolean
        Public Property StartPlayTime As StartTag

        Public Sub New()
        End Sub

        Public Sub New(other As AbstractPlaylist)
            IndependentSegments = other.IndependentSegments
            If other.StartPlayTime IsNot Nothing Then
                StartPlayTime = New StartTag(other.StartPlayTime)
            End If
        End Sub

        Public Sub SetVersion(version As VersionTag)
            Me.Version = version.Number
        End Sub

        Public Sub SetStart(start As StartTag)
            StartPlayTime = start
        End Sub

        Public Sub SetIndependentSegments()
            IndependentSegments = True
        End Sub

    End Class

End Namespace