Imports Crunchyroll_Downloader.postprocess
Imports Crunchyroll_Downloader.settings
Imports Crunchyroll_Downloader.settings.ffmpeg
Imports Crunchyroll_Downloader.settings.ffmpeg.encoding
Imports Crunchyroll_Downloader.utilities.ffmpeg.codec
Imports Crunchyroll_Downloader.utilities.ffmpeg.preset
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace postprocess
    <TestClass>
    Public Class ReencodePreferenceFactoryTest
        <TestMethod>
        Public Sub TestNoMergeSubs()
            Dim path As String = "\temporary\path"
            Dim f As New Format(ContainerFormat.MKV, SubtitleMerge.DISABLED)
            Dim ffmpegBuilder As New FfmpegOptions.Builder()
            With ffmpegBuilder
                .SetPresetSpeed(SpeedSetting.NO_PRESET)
                .SetCopyMode(True)
            End With

            Dim prefs = ReencodePreferenceFactory.GetVideoReencodePreferences(path, f, ffmpegBuilder.Build())

            Assert.IsFalse(prefs.MergeSoftSubtitles)
            Assert.IsNull(prefs.SoftSubCodec)
            Assert.AreEqual(VideoCodec.COPY, prefs.VideoCodec)
            Assert.AreEqual(AudioCodec.COPY, prefs.AudioCodec)
            Assert.AreEqual("\temporary\path", prefs.TemporaryOutputPath)
        End Sub

        <TestMethod>
        Public Sub TestCopySubs()
            Dim path As String = "\temporary\path"
            Dim f As New Format(ContainerFormat.MKV, SubtitleMerge.COPY)
            Dim ffmpegBuilder As New FfmpegOptions.Builder()
            With ffmpegBuilder
                .SetPresetSpeed(SpeedSetting.NO_PRESET)
                .SetCopyMode(True)
            End With

            Dim prefs = ReencodePreferenceFactory.GetVideoReencodePreferences(path, f, ffmpegBuilder.Build())

            Assert.IsTrue(prefs.MergeSoftSubtitles)
            Assert.AreEqual(SubtitleCodec.COPY, prefs.SoftSubCodec)
            Assert.AreEqual(VideoCodec.COPY, prefs.VideoCodec)
            Assert.AreEqual(AudioCodec.COPY, prefs.AudioCodec)
            Assert.AreEqual("\temporary\path", prefs.TemporaryOutputPath)
        End Sub

        <TestMethod>
        Public Sub TestPreset()
            Dim path As String = "\temporary\path"
            Dim f As New Format(ContainerFormat.MP4, SubtitleMerge.DISABLED)
            Dim ffmpegBuilder As New FfmpegOptions.Builder()
            With ffmpegBuilder
                .SetPresetSpeed(SpeedSetting.VERY_FAST)
                .SetCopyMode(False)
                .SetUseTargetBitrate(False)
                .SetEncoderHardware(EncoderImplementation.SOFTWARE)
                .SetVideoCodec(Codec.H_264)
            End With

            Dim prefs = ReencodePreferenceFactory.GetVideoReencodePreferences(path, f, ffmpegBuilder.Build())

            Assert.IsFalse(prefs.MergeSoftSubtitles)
            Assert.IsNull(prefs.SoftSubCodec)
            Assert.AreEqual(VideoCodec.LIBX264, prefs.VideoCodec)
            Assert.AreEqual(AudioCodec.COPY, prefs.AudioCodec)
            Assert.AreEqual(New SpeedPreset(Speed.VERYFAST), prefs.VideoPreset)
            Assert.AreEqual("\temporary\path", prefs.TemporaryOutputPath)
        End Sub
    End Class
End Namespace