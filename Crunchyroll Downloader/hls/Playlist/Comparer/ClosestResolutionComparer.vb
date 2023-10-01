Imports Crunchyroll_Downloader.hls.playlist.stream

Namespace hls.playlist.comparer
    Public Class ClosestResolutionComparer
        Implements IComparer(Of VariantStreamMetadata)

        Private ReadOnly targetResolution As Integer

        Private ReadOnly RoundUp As Boolean

        Private ReadOnly HighBandwidth As Boolean

        Public Sub New(targetHeight As Integer, roundUp As Boolean, preferHighBandwidth As Boolean)
            targetResolution = targetHeight
            Me.RoundUp = roundUp
            HighBandwidth = preferHighBandwidth
        End Sub

        Public Function Compare(x As VariantStreamMetadata, y As VariantStreamMetadata) As Integer Implements IComparer(Of VariantStreamMetadata).Compare
            ' Shortcut if exactly one of the streams matches the target resolution.
            If x.VideoResolution.Vertical = targetResolution And y.VideoResolution.Vertical <> targetResolution Then
                Return 1
            ElseIf y.VideoResolution.Vertical = targetResolution And x.VideoResolution.Vertical <> targetResolution Then
                Return -1

            ElseIf x.VideoResolution.Vertical = y.VideoResolution.Vertical Then
                Return CompareBandwidth(x, y)

            Else
                ' Neither stream matches the target resolution, so compare other properties.
                Return CompareInconclusiveResolution(x, y)
            End If
        End Function

        ''' <summary>
        ''' Compares streams when neither stream is equal to the target resolution and the two streams don't have the same resolution.
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="y"></param>
        ''' <returns></returns>
        Private Function CompareInconclusiveResolution(x As VariantStreamMetadata, y As VariantStreamMetadata) As Integer
            Dim xResolution As Double = x.VideoResolution.Vertical
            Dim yResolution As Double = y.VideoResolution.Vertical

            If RoundUp Then
                If xResolution > targetResolution And yResolution > targetResolution Then
                    ' If both are greater than the target resolution, choose the lower of the two.
                    Return Math.Sign(-xResolution + yResolution)
                ElseIf xResolution > targetResolution And yResolution < targetResolution Then
                    ' Only x is greater than target resolution, so x is best.
                    Return 1
                ElseIf yResolution > targetResolution And xResolution < targetResolution Then
                    ' Only y is greater than the target resolution, so y is best.
                    Return -1
                Else
                    ' Both x and y are less than the target, so the greater one is best.
                    Return Math.Sign(xResolution - yResolution)
                End If
            Else
                If xResolution < targetResolution And yResolution < targetResolution Then
                    ' Both are less than the target resolution and rounding down: choose the greater one.
                    Return Math.Sign(xResolution - yResolution)
                ElseIf yResolution > targetResolution And xResolution < targetResolution Then
                    ' Only x is less than target resolution, so x is best.
                    Return 1
                ElseIf xResolution > targetResolution And yResolution < targetResolution Then
                    ' Only y is less than target resolution, so y is best.
                    Return -1
                Else
                    ' Both are greater than the target, so the lower one is better.
                    Return Math.Sign(-xResolution + yResolution)
                End If
            End If
        End Function

        Private Function CompareBandwidth(x As VariantStreamMetadata, y As VariantStreamMetadata) As Integer
            If HighBandwidth Then
                Return Math.Sign(x.Bandwidth - y.Bandwidth)
            Else
                Return Math.Sign(y.Bandwidth - x.Bandwidth)
            End If
        End Function
    End Class
End Namespace