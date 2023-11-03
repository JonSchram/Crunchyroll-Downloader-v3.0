Imports Crunchyroll_Downloader.settings.ffmpeg.encoding

Namespace utilities.ffmpeg.preset
    Public Class SpeedPresetArgument
        Inherits PresetArgument

        Public Sub New(value As SpeedPreset)
            MyBase.New(value)
        End Sub

        Public Shared Function FromSetting(s As SpeedSetting) As SpeedPresetArgument
            Return New SpeedPresetArgument(New SpeedPreset(GetSpeed(s)))
        End Function

        Private Shared Function GetSpeed(s As SpeedSetting) As Speed
            Select Case s
                Case SpeedSetting.VERY_SLOW
                    Return Speed.SLOW
                Case SpeedSetting.SLOWER
                    Return Speed.SLOWER
                Case SpeedSetting.SLOW
                    Return Speed.SLOW
                Case SpeedSetting.MEDIUM
                    Return Speed.MEDIUM
                Case SpeedSetting.FAST
                    Return Speed.FAST
                Case SpeedSetting.FASTER
                    Return Speed.FASTER
                Case SpeedSetting.VERY_FAST
                    Return Speed.VERYFAST
                Case Else
                    Return Speed.MEDIUM
            End Select
        End Function

    End Class
End Namespace