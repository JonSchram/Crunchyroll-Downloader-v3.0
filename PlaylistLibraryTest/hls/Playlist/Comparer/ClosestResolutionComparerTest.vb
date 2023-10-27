Imports PlaylistLibrary.hls.common
Imports PlaylistLibrary.hls.playlist.comparer
Imports PlaylistLibrary.hls.playlist.stream

Namespace hls.playlist.comparer
    <TestClass>
    Public Class ClosestResolutionComparerTest
        ''' <summary>
        ''' Tests that if two variant streams have equal resolution but unequal bandwidth, the comparer can choose the one with higher bandwidth.
        ''' </summary>
        <TestMethod>
        Sub TestCompare_EqualResolutions_ChoosesHigherBandwidth()
            Dim stream1 As New VariantStreamMetadata("url1", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream2 As New VariantStreamMetadata("url2", 5000, 5000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(1920, True, True)

            Assert.AreEqual(-1, comparer.Compare(stream1, stream2))
        End Sub

        ''' <summary>
        ''' Tests that if two variant streams have equal resolution but unequal bandwidth, the comparer can choose the one with higher bandwidth.
        ''' The stream with higher bandwidth comes first.
        ''' </summary>
        <TestMethod>
        Sub TestCompare_EqualResolutions_ChoosesHigherBandwidth_ReverseOrder()
            Dim stream1 As New VariantStreamMetadata("url1", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream2 As New VariantStreamMetadata("url2", 5000, 5000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(1920, True, True)

            Assert.AreEqual(1, comparer.Compare(stream2, stream1))
        End Sub

        ''' <summary>
        ''' Tests that if two variant streams have equal resolution but unequal bandwidth, the comparer can choose the one with lower bandwidth.
        ''' The stream with higher bandwidth comes first.
        ''' </summary>
        <TestMethod>
        Sub TestCompare_EqualResolutions_ChoosesLowerBandwidth()
            Dim stream1 As New VariantStreamMetadata("url1", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream2 As New VariantStreamMetadata("url2", 5000, 5000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(1920, True, False)

            Assert.AreEqual(1, comparer.Compare(stream1, stream2))
        End Sub

        ''' <summary>
        ''' Tests that if two variant streams have equal resolution but unequal bandwidth, the comparer can choose the one with lower bandwidth.
        ''' The stream with higher bandwidth comes first.
        ''' </summary>
        <TestMethod>
        Sub TestCompare_EqualResolutions_ChoosesLowerBandwidth_ReverseOrder()
            Dim stream1 As New VariantStreamMetadata("url1", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream2 As New VariantStreamMetadata("url2", 5000, 5000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(1920, True, False)

            Assert.AreEqual(-1, comparer.Compare(stream2, stream1))
        End Sub

        <TestMethod>
        Sub TestCompare_UnequalResolution_Get1080pStream()
            Dim stream_720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream_1080p As New VariantStreamMetadata("1080p-url", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(1080, True, False)

            Assert.AreEqual(1, comparer.Compare(stream_1080p, stream_720p))
        End Sub

        <TestMethod>
        Sub TestCompare_UnequalResolution_Get1080pStream_ReverseOrder()
            Dim stream_720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream_1080p As New VariantStreamMetadata("1080p-url", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(1080, True, False)

            Assert.AreEqual(-1, comparer.Compare(stream_720p, stream_1080p))
        End Sub

        <TestMethod>
        Sub TestCompare_UnequalResolution_Get720pStream()
            Dim stream_720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream_1080p As New VariantStreamMetadata("1080p-url", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(720, True, False)

            Assert.AreEqual(-1, comparer.Compare(stream_1080p, stream_720p))
        End Sub

        <TestMethod>
        Sub TestCompare_UnequalResolution_Get720pStream_ReverseOrder()
            Dim stream_720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream_1080p As New VariantStreamMetadata("1080p-url", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(720, True, False)

            Assert.AreEqual(1, comparer.Compare(stream_720p, stream_1080p))
        End Sub

        <TestMethod>
        Sub TestCompare_EqualResolution_GetHigherBandwidthStreamHigherThan720p()
            Dim lowBandwidthStream As New VariantStreamMetadata("1080p-url1", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim highBandwidthStream As New VariantStreamMetadata("1080p-url2", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(720, True, True)

            Assert.AreEqual(1, comparer.Compare(highBandwidthStream, lowBandwidthStream))
        End Sub

        <TestMethod>
        Sub TestCompare_EqualResolution_GetHigherBandwidthStreamHigherThan720p_ReverseOrder()
            Dim lowBandwidthStream As New VariantStreamMetadata("1080p-url1", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim highBandwidthStream As New VariantStreamMetadata("1080p-url2", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(720, True, True)

            Assert.AreEqual(-1, comparer.Compare(lowBandwidthStream, highBandwidthStream))
        End Sub

        <TestMethod>
        Sub TestCompare_EqualResolution_GetLowerBandwidthStreamHigherThan720p()
            Dim lowBandwidthStream As New VariantStreamMetadata("1080p-url1", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim highBandwidthStream As New VariantStreamMetadata("1080p-url2", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(720, True, False)

            Assert.AreEqual(-1, comparer.Compare(highBandwidthStream, lowBandwidthStream))
        End Sub

        <TestMethod>
        Sub TestCompare_EqualResolution_GetLowerBandwidthStreamHigherThan720p_ReverseOrder()
            Dim lowBandwidthStream As New VariantStreamMetadata("1080p-url1", 1000, 1000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim highBandwidthStream As New VariantStreamMetadata("1080p-url2", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(720, True, False)

            Assert.AreEqual(1, comparer.Compare(lowBandwidthStream, highBandwidthStream))
        End Sub

        <TestMethod>
        Sub TestCompare_RoundUp_BothAboveTarget()
            Dim stream720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("1080p-url", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(700, True, False)

            Assert.AreEqual(1, comparer.Compare(stream720p, stream1080p))
        End Sub

        <TestMethod>
        Sub TestCompare_RoundUp_BothAboveTarget_ReverseOrder()
            Dim stream720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("1080p-url", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(700, True, False)

            Assert.AreEqual(-1, comparer.Compare(stream1080p, stream720p))
        End Sub

        <TestMethod>
        Sub TestCompare_RoundDown_BothBelowTarget()
            Dim stream720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("1080p-url", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(1500, False, False)

            Assert.AreEqual(1, comparer.Compare(stream1080p, stream720p))
        End Sub

        <TestMethod>
        Sub TestCompare_RoundDown_BothBelowTarget_ReverseOrder()
            Dim stream720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("1080p-url", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(1500, False, False)

            Assert.AreEqual(-1, comparer.Compare(stream720p, stream1080p))
        End Sub

        <TestMethod>
        Sub TestCompare_RoundUp_BothBelowTarget()
            Dim stream720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("1080p-url", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(1500, True, False)

            Assert.AreEqual(1, comparer.Compare(stream1080p, stream720p))
        End Sub

        <TestMethod>
        Sub TestCompare_RoundUp_BothBelowTarget_ReverseOrder()
            Dim stream720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("1080p-url", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(1500, True, False)

            Assert.AreEqual(-1, comparer.Compare(stream720p, stream1080p))
        End Sub

        <TestMethod>
        Sub TestCompare_RoundDown_BothAboveTarget()
            Dim stream720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("1080p-url", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(500, False, False)

            Assert.AreEqual(-1, comparer.Compare(stream1080p, stream720p))
        End Sub

        <TestMethod>
        Sub TestCompare_RoundDown_BothAboveTarget_ReverseOrder()
            Dim stream720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("1080p-url", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(500, False, False)

            Assert.AreEqual(1, comparer.Compare(stream720p, stream1080p))
        End Sub

        <TestMethod>
        Sub TestCompare_RoundUp_OneAboveOneBelow()
            Dim stream720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("1080p-url", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(900, True, False)

            Assert.AreEqual(1, comparer.Compare(stream1080p, stream720p))
        End Sub

        <TestMethod>
        Sub TestCompare_RoundUp_OneAboveOneBelow_ReverseOrder()
            Dim stream720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("1080p-url", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(900, True, False)

            Assert.AreEqual(-1, comparer.Compare(stream720p, stream1080p))
        End Sub

        <TestMethod>
        Sub TestCompare_RoundDown_OneAboveOneBelow()
            Dim stream720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("1080p-url", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(900, False, False)

            Assert.AreEqual(-1, comparer.Compare(stream1080p, stream720p))
        End Sub

        <TestMethod>
        Sub TestCompare_RoundDown_OneAboveOneBelow_ReverseOrder()
            Dim stream720p As New VariantStreamMetadata("720p-url", 1000, 1000, New Resolution(1280, 720), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim stream1080p As New VariantStreamMetadata("1080p-url", 9000, 9000, New Resolution(1920, 1080), "group", Hdcp.NONE,
                                                     Array.Empty(Of String)(), Nothing, Nothing, Nothing, False, 24)

            Dim comparer As New ClosestResolutionComparer(900, False, False)

            Assert.AreEqual(1, comparer.Compare(stream720p, stream1080p))
        End Sub

    End Class
End Namespace

