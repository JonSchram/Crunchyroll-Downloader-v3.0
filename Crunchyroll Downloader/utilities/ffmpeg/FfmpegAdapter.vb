Imports System.Text

Namespace utilities.ffmpeg
    Public Class FfmpegAdapter
        Implements IFfmpegAdapter

        Private ReadOnly ExecutablePath As String

        Public Event ReportProgress(percent As Double) Implements IFfmpegAdapter.ReportProgress

        Public Sub New(executableLocation As String)
            ExecutablePath = executableLocation
        End Sub

        Public Async Function Run(arguments As FfmpegArguments) As Task(Of Integer) Implements IFfmpegAdapter.Run
            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim commandArguments = commandBuilder.BuildCommandLineArguments(arguments)

            Dim startInfo As New ProcessStartInfo() With {
                .FileName = ExecutablePath,
                .Arguments = commandArguments,
                .WindowStyle = ProcessWindowStyle.Hidden,
                .RedirectStandardError = True,
                .StandardErrorEncoding = Encoding.UTF8,
                .RedirectStandardOutput = True,
                .StandardOutputEncoding = Encoding.UTF8,
                .CreateNoWindow = True,
                .UseShellExecute = False
            }
            Dim ffmpegProcess = New Process() With {
                .StartInfo = startInfo,
                .EnableRaisingEvents = True
            }


            Dim outputParser As New FfmpegOutputParser()
            AddHandler outputParser.Progress, AddressOf HandleFfmpegProgress

            AddHandler ffmpegProcess.ErrorDataReceived, AddressOf outputParser.HandleFfmpegStdOut
            AddHandler ffmpegProcess.OutputDataReceived, AddressOf outputParser.HandleFfmpegStdOut

            Dim exitHandlerInstance As New ExitHandler()
            AddHandler ffmpegProcess.Exited, AddressOf exitHandlerInstance.HandleFfmpegExit

            ffmpegProcess.Start()
            ffmpegProcess.BeginOutputReadLine()
            ffmpegProcess.BeginErrorReadLine()

            Return Await exitHandlerInstance.GetExitTask()
        End Function

        Private Sub HandleFfmpegProgress(report As FfmpegProgressReport, totalDuration As TimeSpan?)
            Dim progress As Double = 0
            If totalDuration?.TotalMinutes > 0 Then
                progress = CDbl(report.CurrentTime?.TotalMinutes / totalDuration?.TotalMinutes * 100)
            End If

            RaiseEvent ReportProgress(progress)
        End Sub


        Private Class ExitHandler
            Private ReadOnly tcs As New TaskCompletionSource(Of Integer)

            Private IsComplete As Boolean = False
            Private ExitCode As Integer = 0

            Public Function GetExitTask() As Task(Of Integer)
                If IsComplete Then
                    Return Task.FromResult(ExitCode)
                End If
                Return tcs.Task
            End Function

            Public Sub HandleFfmpegExit(sendingProcess As Object, args As EventArgs)
                ' General strategy loosely based on:
                ' https://stackoverflow.com/questions/470256/process-waitforexit-asynchronously

                IsComplete = True
                If TypeOf sendingProcess Is Process Then
                    ' This should always be a Process object, but requires a type cast.
                    ExitCode = CType(sendingProcess, Process).ExitCode
                End If
                If ExitCode = 0 Then
                    tcs.SetResult(ExitCode)
                Else
                    tcs.SetException(New Exception($"Ffmpeg exited with error code {ExitCode}"))
                End If
            End Sub
        End Class
    End Class
End Namespace