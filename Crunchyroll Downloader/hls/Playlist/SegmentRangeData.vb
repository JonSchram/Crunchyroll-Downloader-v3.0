Imports Crunchyroll_Downloader.hls.parsing

Namespace hls.playlist
    Public Class SegmentRangeData(Of T)
        ' A list of data items that exist for the media playlist.
        Private ReadOnly DataList As New List(Of T)

        ' A list of segment indices that have a key set. Can be searched later to find the key.
        Private ReadOnly SegmentIndices As New List(Of Integer)

        Public Sub SetSegmentKey(NextData As T, Index As Integer)
            DataList.Add(NextData)
            SegmentIndices.Add(Index)
        End Sub

        Public Function GetDataForSegmentIndex(SegmentIndex As Integer) As T
            Dim TType As Type = GetType(T)
            If SegmentIndex >= SegmentIndices.Count Then
                Throw New PlaylistParseException($"Requested media segment index that doesn't have data (Setting {TType})")
            End If
            Dim keyIndex = SegmentIndices.BinarySearch(SegmentIndex)
            If keyIndex < 0 Then
                ' Key not found, compute complement to get the next lowest index.
                keyIndex = -1 * keyIndex - 1
            End If

            Return DataList.Item(keyIndex)
        End Function
    End Class
End Namespace
