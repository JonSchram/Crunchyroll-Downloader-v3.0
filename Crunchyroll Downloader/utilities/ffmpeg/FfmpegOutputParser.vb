Imports System.Globalization
Imports System.Text.RegularExpressions

Namespace utilities.ffmpeg
    Public Class FfmpegOutputParser
        Private duration As TimeSpan? = Nothing

        Public Event Progress(report As FfmpegProgressReport, totalDuration As TimeSpan?)

        Public Sub HandleFfmpegStdOut(sendingProcess As Object, args As DataReceivedEventArgs)
            Debug.WriteLine(args.Data)
            ParseFfmpegOutput(args.Data)
        End Sub

        Public Sub ParseFfmpegOutput(line As String)
            If line Is Nothing Then
                Return
            End If

            If Not duration.HasValue Then
                DetectDuration(line)
            End If

            If IsProgressLine(line) Then
                Dim frameNumber As Integer? = DetectFrame(line)
                Dim fps As Double? = DetectFps(line)
                Dim q As Double? = DetectQ(line)
                Dim size As Double? = DetectSize(line)
                Dim currentTime As TimeSpan? = DetectCurrentTime(line)
                Dim bitsPerSecond As Double? = DetectBitrate(line)
                Dim speed As Double? = DetectSpeed(line)

                If frameNumber.HasValue Or fps.HasValue Or q.HasValue Or size.HasValue Or currentTime.HasValue Or
                        bitsPerSecond.HasValue Or speed.HasValue Then
                    RaiseEvent Progress(New FfmpegProgressReport(frameNumber, fps, q, size, currentTime, bitsPerSecond, speed), duration)
                End If
            End If

        End Sub


        Public Function GetDuration() As TimeSpan?
            Return duration
        End Function

        Private Function IsProgressLine(line As String) As Boolean
            Return line.StartsWith("frame=")
        End Function

        Private Function DetectFrame(line As String) As Integer?
            Dim lineRegex As New Regex("frame=\s*(\d+)")
            Dim match = lineRegex.Match(line)
            If match.Success Then
                Return CInt(match.Groups(1).Value)
            End If
            Return Nothing
        End Function

        Private Function DetectFps(line As String) As Double?
            Dim regex As New Regex("fps=\s*(\d+(?:[,.]\d+)?)")
            Dim match = regex.Match(line)
            If match.Success Then
                Return CDbl(match.Groups(1).Value)
            End If
            Return Nothing
        End Function

        Private Function DetectQ(line As String) As Double?
            Dim regex As New Regex("q=\s*(-?\d+(?:\.\d+)?)")
            Dim match = regex.Match(line)
            If match.Success Then
                Return CDbl(match.Groups(1).Value)
            End If
            Return Nothing
        End Function

        Private Function DetectSize(line As String) As Double?
            ' Not sure whether this can output in megabytes or gigabytes (or even plain bits), but support it just in case.
            Dim regex As New Regex("size=\s*(\d+)([kmg]?)B")
            Dim match = regex.Match(line)
            If match.Success Then
                Dim amount As Integer = CInt(match.Groups(1).Value)
                Dim unit As String = match.Groups(2).Value
                Return ConvertToBaseUnit(amount, unit, False)
            End If

            Return Nothing
        End Function
        Private Function DetectBitrate(line As String) As Double?
            Dim regex As New Regex("bitrate=\s*(\d+(?:[.,]\d+)?)([kmg])?bits/s")
            Dim match = regex.Match(line)
            If match.Success Then
                Dim amount As Double = CDbl(match.Groups(1).Value)
                Dim unit As String = match.Groups(2).Value
                Return ConvertToBaseUnit(amount, unit, True)
            End If

            Return Nothing
        End Function

        Private Function ConvertToBaseUnit(amount As Double, unitLetter As String, base10 As Boolean) As Double
            Dim unitFactor As Long = If(base10, 1000, 1024)
            Select Case unitLetter.ToLower()
                Case "k"
                    Return amount * unitFactor
                Case "m"
                    Return amount * Math.Pow(unitFactor, 2)
                Case "g"
                    Return amount * Math.Pow(unitFactor, 3)
                Case Else
                    ' Assume there was no unit abbreviation.
                    Return amount
            End Select
        End Function

        Private Function DetectSpeed(line As String) As Double?
            Dim regex As New Regex("speed=\s*(\d+(?:[,.]\d+)?)x")
            Dim match = regex.Match(line)
            If match.Success Then
                Return CDbl(match.Groups(1).Value)
            End If

            Return Nothing
        End Function

        Private Function DetectCurrentTime(line As String) As TimeSpan?
            Dim timeRegex As New Regex($"time=\s*{GetTimespanRegex()}\s")
            Dim timeMatch = timeRegex.Match(line)
            If timeMatch.Success Then
                Return TimeSpan.Parse(timeMatch.Groups(1).Value)
            End If
            Return Nothing
        End Function


        Private Sub DetectDuration(line As String)
            ' Needs to detect correctly even in locales that use a comma for the decimal separation.
            ' Can check for both because I'm not sure whether the output from ffmpeg is guaranteed to match the system locale.
            Dim durationRegex As New Regex($"Duration:\s{GetTimespanRegex()},\s")
            Dim durationMatch = durationRegex.Match(line)
            If durationMatch.Success Then
                Dim durationString As String = durationMatch.Groups(1).Value
                duration = TimeSpan.Parse(durationString, CultureInfo.CurrentCulture())
            End If
        End Sub

        Private Function GetTimespanRegex() As String
            Return "((?:\d+:?)+\d+[\.,]\d+)"
        End Function

    End Class
End Namespace