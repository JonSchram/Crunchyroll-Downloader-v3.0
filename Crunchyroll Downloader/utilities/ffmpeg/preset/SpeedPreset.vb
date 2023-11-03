Imports System.ComponentModel

Namespace utilities.ffmpeg.preset
    ''' <summary>
    ''' A preset for video codecs like h.264 and h.265 that are represented by a speed string such as slow/medium/fast.
    ''' </summary>
    Public Class SpeedPreset
        Implements IPreset

        Public ReadOnly Property EncoderSpeed As Speed
        Public Sub New(s As Speed)
            EncoderSpeed = s
        End Sub

        Public Function GetPresetString() As String Implements IPreset.GetPresetString
            Select Case EncoderSpeed
                Case Speed.VERYSLOW
                    Return "veryslow"
                Case Speed.SLOWER
                    Return "slower"
                Case Speed.SLOW
                    Return "slow"
                Case Speed.MEDIUM
                    Return "medium"
                Case Speed.FAST
                    Return "fast"
                Case Speed.FASTER
                    Return "faster"
                Case Speed.VERYFAST
                    Return "veryfast"
                Case Speed.SUPERFAST
                    Return "superfast"
                Case Speed.ULTRAFAST
                    Return "ultrafast"
                Case Else
                    ' All cases have been checked, so this should never happen.
                    Throw New InvalidEnumArgumentException("Invalid enum used for SpeedPreset")
            End Select
        End Function
    End Class
End Namespace