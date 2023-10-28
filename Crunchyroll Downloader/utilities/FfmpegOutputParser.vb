Imports System.Text.RegularExpressions

Namespace utilities
    Public Class FfmpegOutputParser
        Private duration As Double?

        Public Sub New()

        End Sub

        Public Sub HandleFfmpegOutput(line As String)
            If Not duration.HasValue Then
                DetectDuration(line)
            End If
        End Sub


        Public Function GetDuration() As Double
            Return duration.GetValueOrDefault()
        End Function

        Private Sub DetectCurrentTime(line As String)
            Dim timeRegex As New Regex($"time={GetTimespanRegex()}\s")
            Dim timeMatch = timeRegex.Match(line)
            If timeMatch.Success Then
                Dim currentTime As String = timeMatch.Groups(1).Value
                Dim currentTimeSpan = TimeSpan.Parse(currentTime)
            End If

        End Sub

        Private Sub DetectDuration(line As String)
            ' Needs to detect correctly even in locales that use a comma for the decimal separation.
            ' Can check for both because I'm not sure whether the output from ffmpeg is guaranteed to match the system locale.
            Dim durationRegex As New Regex($"Duration:\s{GetTimespanRegex()},\s")
            Dim durationMatch = durationRegex.Match(line)
            If durationMatch.Success Then
                Dim durationString As String = durationMatch.Groups(1).Value
                Dim durationTimeSpan = TimeSpan.Parse(durationString)
                duration = durationTimeSpan.TotalMinutes
            End If
        End Sub

        Private Function GetTimespanRegex() As String
            Return "((?:\d+:?)+\d+[\.,]\d+)"
        End Function

    End Class
End Namespace