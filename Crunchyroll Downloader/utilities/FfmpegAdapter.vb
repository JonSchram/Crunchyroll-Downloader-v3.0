﻿Imports System.Text

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

        Public Sub Run(arguments As FfmpegArguments) Implements IFfmpegAdapter.Run
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
            AddHandler ffmpegProcess.Exited, AddressOf HandleFfmpegExit

            ' TODO: Make async
            ffmpegProcess.Start()
            ffmpegProcess.BeginOutputReadLine()
            ffmpegProcess.BeginErrorReadLine()
        End Sub

        Private Sub HandleFfmpegOutput(sendingProcess As Object, args As DataReceivedEventArgs)
            ' TODO: Handle ffmpeg output
            Debug.WriteLine("Ffmpeg process output:")
            Debug.WriteLine(args.Data)
        End Sub
        Private Sub HandleFfmpegError(sendingProcess As Object, args As DataReceivedEventArgs)
            ' TODO: Handle ffmpeg error out
            Debug.WriteLine("Ffmpeg process error output:")
            Debug.WriteLine(args.Data)
        End Sub

        Private Sub HandleFfmpegExit(sendingProcess As Object, args As EventArgs)
            ' TODO: Handle process exit?
        End Sub
    End Class
End Namespace