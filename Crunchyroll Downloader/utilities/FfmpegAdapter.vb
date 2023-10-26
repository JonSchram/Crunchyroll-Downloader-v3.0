Imports System.Security.Cryptography.X509Certificates
Imports System.Text

Namespace utilities
    Public Class FfmpegAdapter
        Implements IFfmpegAdapter

        Private ReadOnly ExecutablePath As String
        Private ReadOnly Cookies As New Dictionary(Of String, String)
        Private UserAgent As String

        Public Sub New(executableLocation As String)
            ExecutablePath = executableLocation
        End Sub

        Public Sub SetUserAgent(userAgent As String) Implements IFfmpegAdapter.SetUserAgent
            Me.UserAgent = userAgent
        End Sub

        ''' <summary>
        ''' Adds a cookie to the ffmpeg adapter. This will be added to all ffmpeg commands created by this FFmpeg adapter instance.
        ''' </summary>
        ''' <param name="name"></param>
        ''' <param name="value"></param>
        Public Sub AddCookie(name As String, value As String) Implements IFfmpegAdapter.AddCookie
            Cookies.Add(name, value)
        End Sub

        Public Async Function Run(arguments As FfmpegArguments) As Task(Of Integer) Implements IFfmpegAdapter.Run
            Dim commandBuilder As New FfmpegCommandBuilder()
            Dim commandArguments = commandBuilder.BuildCommandLineArguments(arguments, Cookies, UserAgent)

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


            AddHandler ffmpegProcess.ErrorDataReceived, AddressOf HandleFfmpegError
            AddHandler ffmpegProcess.OutputDataReceived, AddressOf HandleFfmpegOutput

            Dim exitHandlerInstance As New ExitHandler()
            AddHandler ffmpegProcess.Exited, AddressOf exitHandlerInstance.HandleFfmpegExit


            ffmpegProcess.Start()
            ffmpegProcess.BeginOutputReadLine()
            ffmpegProcess.BeginErrorReadLine()

            Return Await exitHandlerInstance.GetExitTask()
        End Function

        Private Sub HandleFfmpegOutput(sendingProcess As Object, args As DataReceivedEventArgs)
            ' TODO: Handle ffmpeg output
            Debug.WriteLine($"[ffmpeg output]: {args.Data}")
        End Sub
        Private Sub HandleFfmpegError(sendingProcess As Object, args As DataReceivedEventArgs)
            ' TODO: Handle ffmpeg error out
            Debug.WriteLine($"[ffmpeg error output]: {args.Data}")
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
                tcs.SetResult(ExitCode)
            End Sub
        End Class
    End Class
End Namespace