Imports PlaylistLibrary.hls.playlist.stream

Namespace hls.playlist.comparer

    Public Class HighestResolutionComparer
        Implements IComparer(Of VariantStreamMetadata)

        Public Function Compare(x As VariantStreamMetadata, y As VariantStreamMetadata) As Integer Implements IComparer(Of VariantStreamMetadata).Compare
            If x.VideoResolution.Vertical > y.VideoResolution.Vertical Then
                ' Both horizontal and vertical resolution should be greater, but assume that checking vertical resolution is enough.
                Return 1
            ElseIf x.VideoResolution.Vertical < y.VideoResolution.Vertical Then
                Return -1
            End If

            If x.Bandwidth > y.Bandwidth Then
                Return 1
            ElseIf x.Bandwidth < y.Bandwidth Then
                Return -1
            Else
                Return 0
            End If
        End Function
    End Class
End Namespace