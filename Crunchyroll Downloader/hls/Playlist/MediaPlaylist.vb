Imports System.Collections.Immutable
Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.segment

Namespace hls.playlist
    Public Class MediaPlaylist
        Inherits AbstractPlaylist
        ' Required
        Public ReadOnly Property TargetDuration As Integer

        Public ReadOnly Property PlaylistEnds As Boolean

        Public ReadOnly Property Type As PlaylistType = PlaylistType.NONE_SPECIFIED

        Public ReadOnly Property IFramesOnly As Boolean = False

        ' Unused, seems to be for a server to give extra date range information to the player.
        Public ReadOnly Property DateRangeList As New List(Of DateRange)

        Public ReadOnly Property Segments As ImmutableList(Of MediaSegment)

        Public Sub New(version As Integer, independentSegments As Boolean, startPlayTime As PlaylistStartTime,
                       targetDuration As Integer, playlistEnds As Boolean, type As PlaylistType, iFramesOnly As Boolean,
                       dateRangeList As List(Of DateRange), segments As List(Of MediaSegment))
            MyBase.New(version, independentSegments, startPlayTime)
            Me.TargetDuration = targetDuration
            Me.PlaylistEnds = playlistEnds
            Me.Type = type
            Me.IFramesOnly = iFramesOnly
            Me.DateRangeList = dateRangeList
            Me.Segments = ImmutableList.CreateRange(segments)
        End Sub



        ' TODO: Make API to get iterable segments


        Public Overrides Function ToString() As String
            Return $"{{
Version: {Version},
IndependentSegments: {IndependentSegments},
Start: {StartPlayTime},
TargetDuration: {TargetDuration},
PlaylistEnds: {PlaylistEnds},
Type: {Type},
IFramesOnly: {IFramesOnly},
DateRangeList: {FormatList(DateRangeList)},
Media segments: {FormatList(Segments)}
}}"
        End Function

        Private Function FormatList(PropertyList As IEnumerable(Of Object)) As String
            Dim output As String = "["

            For Each streamItem In PropertyList
                output += streamItem.ToString() + ","
            Next

            output += "]"
            Return output
        End Function

    End Class

End Namespace