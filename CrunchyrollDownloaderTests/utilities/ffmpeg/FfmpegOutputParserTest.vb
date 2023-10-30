Imports System.Globalization
Imports System.Text
Imports System.Threading
Imports Crunchyroll_Downloader.utilities.ffmpeg
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace utilities.ffmpeg
    <TestClass>
    Public Class FfmpegOutputParserTest
        <TestMethod>
        Public Sub TestHandleFfmepgOutput_Duration()
            Dim outputLine = "  Duration: 00:22:28.12, start: 0.000000, bitrate: 9001 kb/s"

            Dim outputParser As New FfmpegOutputParser()
            outputParser.ParseFfmpegOutput(outputLine)

            Dim expectedDuration As New TimeSpan(0, 0, 22, 28, 120)
            Assert.AreEqual(expectedDuration, outputParser.GetDuration())
        End Sub

        <TestMethod>
        Public Sub TestHandleFfmepgOutput_Duration_CommaSeparator()
            ' Save current culture to restore it after test finishes
            Dim currentCulture = Thread.CurrentThread.CurrentCulture
            ' Ensure that the correct separator is used when parsing the time span.
            Thread.CurrentThread.CurrentCulture = New CultureInfo("es")

            Dim outputParser As New FfmpegOutputParser()
            Dim outputLine = "  Duration: 00:22:28,12, start: 0.000000, bitrate: 9001 kb/s"
            outputParser.ParseFfmpegOutput(outputLine)

            Dim expectedDuration As New TimeSpan(0, 0, 22, 28, 120)
            Assert.AreEqual(expectedDuration, outputParser.GetDuration(), 0.01)

            Thread.CurrentThread.CurrentCulture = currentCulture
        End Sub


        <TestMethod>
        Public Sub TestHandleDurationAndOutput()
            Dim parser As New FfmpegOutputParser()
            Dim recorder As New EventRecorder()
            AddHandler parser.Progress, AddressOf recorder.HandleFfmpegReport

            Dim durationLine = "  Duration: 00:25:01.001, start: 0.000000, bitrate: 500 kb/s"
            parser.ParseFfmpegOutput(durationLine)

            Dim line = "frame=1234"
            parser.ParseFfmpegOutput(line)

            Dim expectedDuration As New TimeSpan(0, 0, 25, 1, 1)
            Assert.AreEqual(expectedDuration, parser.GetDuration())
            Assert.AreEqual(expectedDuration, recorder.reportedDuration)

            Assert.AreEqual(1, recorder.CreatedReports.Count)
            Dim report = recorder.CreatedReports.Item(0)
            Assert.AreEqual(1234, report.FrameNumber)
        End Sub

        <TestMethod>
        Public Sub TestHandleFfmpegOutput_Progress()
            Dim parser = New FfmpegOutputParser()
            Dim recorder As New EventRecorder()
            AddHandler parser.Progress, AddressOf recorder.HandleFfmpegReport

            Dim line = "frame=60 fps=7.4 q=28.2 size=3kB time=00:00:01.45 bitrate=6.1kbits/s speed=0.213x"
            parser.ParseFfmpegOutput(line)

            Assert.AreEqual(1, recorder.CreatedReports.Count)

            Dim report = recorder.CreatedReports.Item(0)
            Assert.AreEqual(60, report.FrameNumber)
            Assert.AreEqual(7.4, report.FramesPerSecond.Value, 0.001)
            Assert.AreEqual(28.2, report.Q.Value, 0.001)
            Assert.AreEqual(3 * 1024, report.CurrentSizeBytes.Value, 0.001)
            Assert.AreEqual(New TimeSpan(0, 0, 0, 1, 450), report.CurrentTime)
            Assert.AreEqual(6.1 * 1000, report.BitsPerSecond.Value, 0.001)
            Assert.AreEqual(0.213, report.Speed.Value, 0.0001)
        End Sub

        ''' <summary>
        ''' Tests that an entire ffmpeg output line can be parsed when there are spaces before each number.
        ''' </summary>
        <TestMethod>
        Public Sub TestHandleFfmpegOutput_Progress_WithSpaces()
            Dim parser = New FfmpegOutputParser()
            Dim recorder As New EventRecorder()
            AddHandler parser.Progress, AddressOf recorder.HandleFfmpegReport

            Dim line = "frame=   60 fps=     7.4 q= 28.2 size=         3kB time= 00:00:01.45 bitrate=  6.1kbits/s speed=  0.213x"
            parser.ParseFfmpegOutput(line)

            Assert.AreEqual(1, recorder.CreatedReports.Count)

            Dim report = recorder.CreatedReports.Item(0)
            Assert.AreEqual(60, report.FrameNumber)
            Assert.AreEqual(7.4, report.FramesPerSecond.Value, 0.001)
            Assert.AreEqual(28.2, report.Q.Value, 0.001)
            Assert.AreEqual(3 * 1024, report.CurrentSizeBytes.Value, 0.001)
            Assert.AreEqual(New TimeSpan(0, 0, 0, 1, 450), report.CurrentTime)
            Assert.AreEqual(6.1 * 1000, report.BitsPerSecond.Value, 0.001)
            Assert.AreEqual(0.213, report.Speed.Value, 0.0001)
        End Sub

        <TestMethod>
        Public Sub TestHandleFfmpegOutput_Progress_NegativeQ()
            Dim parser = New FfmpegOutputParser()
            Dim recorder As New EventRecorder()
            AddHandler parser.Progress, AddressOf recorder.HandleFfmpegReport

            Dim line = "frame=70211 fps= 782 q= -1.0 Lsize= 1118302kB time=00:38:57.15 bitrate=82781.6kbits/s speed=350.6x"
            parser.ParseFfmpegOutput(line)

            Assert.AreEqual(1, recorder.CreatedReports.Count)

            Dim report = recorder.CreatedReports.Item(0)
            Assert.AreEqual(70211, report.FrameNumber)
            Assert.AreEqual(782, report.FramesPerSecond.Value, 0.001)
            Assert.AreEqual(-1.0, report.Q.Value, 0.001)
            Assert.AreEqual(1118302 * 1024, report.CurrentSizeBytes.Value, 1)
            Assert.AreEqual(New TimeSpan(0, 0, 38, 57, 150), report.CurrentTime)
            Assert.AreEqual(82781.6 * 1000, report.BitsPerSecond.Value, 0.001)
            Assert.AreEqual(350.6, report.Speed.Value, 0.0001)
        End Sub

        <TestMethod>
        Public Sub TestHandleFfmpegOutput_Progress_MultipleLines()
            Dim parser = New FfmpegOutputParser()
            Dim recorder As New EventRecorder()
            AddHandler parser.Progress, AddressOf recorder.HandleFfmpegReport

            Dim line1 = "frame=980 fps=3 q=10.0 Lsize= 50kB time=00:00:57.15 bitrate=555.5kbits/s speed=0.6x"
            parser.ParseFfmpegOutput(line1)
            Dim line2 = "frame=999 fps=3 q=9.0 Lsize= 51kB time=00:00:58.85 bitrate=54kbits/s speed=1.6x"
            parser.ParseFfmpegOutput(line2)

            Assert.AreEqual(2, recorder.CreatedReports.Count)

            Dim report1 = recorder.CreatedReports.Item(0)
            Assert.AreEqual(980, report1.FrameNumber)

            Dim report2 = recorder.CreatedReports.Item(1)
            Assert.AreEqual(999, report2.FrameNumber)
        End Sub

        <TestMethod>
        Public Sub TestHandleFfmpegOutput_Progress_PartialLine()
            Dim parser = New FfmpegOutputParser()
            Dim recorder As New EventRecorder()
            AddHandler parser.Progress, AddressOf recorder.HandleFfmpegReport

            Dim line = "frame=980 fps=3 q=10.0 Lsize= 50kB"
            parser.ParseFfmpegOutput(line)

            Assert.AreEqual(1, recorder.CreatedReports.Count)

            Dim report = recorder.CreatedReports.Item(0)
            Assert.AreEqual(980, report.FrameNumber)
            Assert.AreEqual(10, report.Q.Value, 0.001)
            Assert.AreEqual(50 * 1024, report.CurrentSizeBytes.Value, 0.1)
            Assert.IsFalse(report.CurrentTime.HasValue)
            Assert.IsFalse(report.BitsPerSecond.HasValue)
            Assert.IsFalse(report.Speed.HasValue)
        End Sub

        <TestMethod>
        Public Sub TestHandleFfmpegOutput_Progress_Megabytes()
            Dim parser = New FfmpegOutputParser()
            Dim recorder As New EventRecorder()
            AddHandler parser.Progress, AddressOf recorder.HandleFfmpegReport

            Dim line = "frame=1  size= 100mB"
            parser.ParseFfmpegOutput(line)

            Assert.AreEqual(1, recorder.CreatedReports.Count)

            Dim report = recorder.CreatedReports.Item(0)
            Assert.AreEqual(CDbl(100 * 1024 * 1024), report.CurrentSizeBytes)
        End Sub

        <TestMethod>
        Public Sub TestHandleFfmpegOutput_Progress_Gigabytes()
            Dim parser = New FfmpegOutputParser()
            Dim recorder As New EventRecorder()
            AddHandler parser.Progress, AddressOf recorder.HandleFfmpegReport

            Dim line = "frame=1 size= 2gB"
            parser.ParseFfmpegOutput(line)

            Assert.AreEqual(1, recorder.CreatedReports.Count)

            Dim report = recorder.CreatedReports.Item(0)
            Assert.AreEqual(2 * Math.Pow(1024, 3), report.CurrentSizeBytes.Value, 1)
        End Sub

        <TestMethod>
        Public Sub TestHandleFfmpegOutput_Progress_CommaSeparator()
            ' Save current culture to restore it after test finishes
            Dim currentCulture = Thread.CurrentThread.CurrentCulture
            ' Ensure that the correct separator is used when parsing.
            Thread.CurrentThread.CurrentCulture = New CultureInfo("es")


            Dim parser = New FfmpegOutputParser()
            Dim recorder As New EventRecorder()
            AddHandler parser.Progress, AddressOf recorder.HandleFfmpegReport

            Dim line = "frame=1 speed=30,4x"
            parser.ParseFfmpegOutput(line)

            Assert.AreEqual(1, recorder.CreatedReports.Count)

            Dim report = recorder.CreatedReports.Item(0)
            Assert.AreEqual(30.4, report.Speed.Value, 0.001)

            Thread.CurrentThread.CurrentCulture = currentCulture
        End Sub

        <TestMethod>
        Public Sub TestHandleFfmpegOutput_Progress_MegabitBitrate()
            Dim parser = New FfmpegOutputParser()
            Dim recorder As New EventRecorder()
            AddHandler parser.Progress, AddressOf recorder.HandleFfmpegReport

            Dim line = "frame=1  bitrate=0.5mbits/s"
            parser.ParseFfmpegOutput(line)

            Assert.AreEqual(1, recorder.CreatedReports.Count)

            Dim report = recorder.CreatedReports.Item(0)
            Assert.AreEqual(0.5 * 1000 * 1000, report.BitsPerSecond.Value, 1)
        End Sub

        <TestMethod>
        Public Sub TestHandleFfmpegOutput_Progress_WholeBitsBitrate()
            Dim parser = New FfmpegOutputParser()
            Dim recorder As New EventRecorder()
            AddHandler parser.Progress, AddressOf recorder.HandleFfmpegReport

            Dim line = "frame=1 bitrate=4kbits/s"
            parser.ParseFfmpegOutput(line)

            Assert.AreEqual(1, recorder.CreatedReports.Count)

            Dim report = recorder.CreatedReports.Item(0)
            Assert.AreEqual(4000, report.BitsPerSecond.Value, 0.001)
        End Sub

        <TestMethod>
        Public Sub TestHandleNonProgressLine()
            Dim parser = New FfmpegOutputParser()
            Dim recorder As New EventRecorder()
            AddHandler parser.Progress, AddressOf recorder.HandleFfmpegReport

            Dim line = "  Stream #0:0(und): Video: hevc, yuv420p(tv, bt709/unknown/unknown, progressive), 1920x1080 [SAR 1:1 DAR 16:9], q=2-31, 23.98 fps, 1k tbn (default)"
            parser.ParseFfmpegOutput(line)

            Assert.AreEqual(0, recorder.CreatedReports.Count)
        End Sub


        Private Class EventRecorder
            Public CreatedReports As New List(Of FfmpegProgressReport)
            Public reportedDuration As TimeSpan?
            Public Sub HandleFfmpegReport(report As FfmpegProgressReport, totalDuration As TimeSpan?)
                CreatedReports.Add(report)
                reportedDuration = totalDuration
            End Sub
        End Class

    End Class
End Namespace