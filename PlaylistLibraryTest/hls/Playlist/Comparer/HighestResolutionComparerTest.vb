Imports PlaylistLibrary.hls.common
Imports PlaylistLibrary.hls.playlist.comparer
Imports PlaylistLibrary.hls.playlist.stream

Namespace hls.playlist.comparer
    <TestClass>
    Public Class HighestResolutionComparerTest
        <TestMethod>
        Sub TestCompare_EqualResolutions()
            Dim lowBitrateStream As New VariantStreamMetadata("url1", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim highBitrateStream As New VariantStreamMetadata("url2", 5000, 5000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New HighestResolutionComparer()

            Assert.AreEqual(1, comparer.Compare(highBitrateStream, lowBitrateStream))
        End Sub

        <TestMethod>
        Sub TestCompare_EqualResolutions_ReverseOrder()
            Dim lowBitrateStream As New VariantStreamMetadata("url1", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim highBitrateStream As New VariantStreamMetadata("url2", 5000, 5000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New HighestResolutionComparer()

            Assert.AreEqual(-1, comparer.Compare(lowBitrateStream, highBitrateStream))
        End Sub


        <TestMethod>
        Sub TestCompare_DifferentResolution()
            Dim stream720p As New VariantStreamMetadata("url1", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("url2", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New HighestResolutionComparer()

            Assert.AreEqual(1, comparer.Compare(stream1080p, stream720p))
        End Sub

        <TestMethod>
        Sub TestCompare_DifferentResolution_ReverseOrder()
            Dim stream720p As New VariantStreamMetadata("url1", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("url2", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New HighestResolutionComparer()

            Assert.AreEqual(-1, comparer.Compare(stream720p, stream1080p))
        End Sub

        ''' <summary>
        ''' Tests that the comparer chooses the high resolution stream even if the low resolution stream has a higher bitrate.
        ''' </summary>
        <TestMethod>
        Sub TestCompare_DifferentResolutionAndBitrate()
            Dim stream720p As New VariantStreamMetadata("url1", 5000, 5000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("url2", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New HighestResolutionComparer()

            Assert.AreEqual(1, comparer.Compare(stream1080p, stream720p))
        End Sub

        ''' <summary>
        ''' Tests that the comparer chooses the high resolution stream even if the low resolution stream has a higher bitrate.
        ''' Streams are passed to the comparer in opposite order
        ''' </summary>
        <TestMethod>
        Sub TestCompare_DifferentResolutionAndBitrate_ReverseOrder()
            Dim stream720p As New VariantStreamMetadata("url1", 5000, 5000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("url2", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New HighestResolutionComparer()

            Assert.AreEqual(-1, comparer.Compare(stream720p, stream1080p))
        End Sub
    End Class
End Namespace