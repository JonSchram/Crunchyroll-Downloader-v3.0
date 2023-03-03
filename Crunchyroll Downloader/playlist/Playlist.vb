Public Class Playlist

    Public Property IsIndependentSegments As Boolean = False

    Public Property Key As SessionKey

    Public Property Audio As PlaylistAudio

    Public Property StreamVariants As List(Of StreamVariant) = New List(Of StreamVariant)

    Public Property IframeStreams As List(Of StreamVariant) = New List(Of StreamVariant)

    Public Overrides Function ToString() As String
        Dim StreamVariantsString = formatStreamList(StreamVariants)
        Dim IframeString = formatStreamList(IframeStreams)
        Return $"{{
  isIndependentSegments: {IsIndependentSegments},
  Key: {Key},
  Audio: {Audio},
  StreamVariants: {StreamVariantsString},
  IframeStreams: {IframeString}
}}"
    End Function

    Private Function formatStreamList(StreamList As List(Of StreamVariant)) As String
        Dim output As String = "["

        For Each Stream In StreamList
            output += Stream.ToString + ","
        Next

        output += "]"
        Return output
    End Function
End Class

Public Class StreamVariant
    ' TODO: Seems that the current version does care about the bandwidth / average bandwidth.
    ' Seems to choose the highest bandwidth stream
    ' The iframe ones may be bigger files for no reason so I should check the quality & size of each one
    Public Property VerticalResolution As Integer
    Public Property HorizontalResolution As Integer
    Public Property FrameRate As Double
    Public Property Bandwidth As Integer
    Public Property AverageBandwidth As Integer

    Public Property AudioId As String
    Public Property Uri As String

    Public Overrides Function ToString() As String
        Return $"{{
  VerticalResolution: {VerticalResolution},
  HorizontalResolution: {HorizontalResolution},
  FrameRate: {FrameRate},
  Bandwidth: {Bandwidth},
  AverageBandwidth: {AverageBandwidth},
  AudioId: {AudioId},
  Uri: {Uri}
}}"
    End Function
End Class

Public Class PlaylistAudio
    Public Property Id As String
    Public Property Language As String

    Public Property Name As String

    Public Property Uri As String

    Public Overrides Function ToString() As String
        Return $"{{
  Id: {Id},
  Language: {Language},
  Name: {Name},
  Uri: {Uri}
}}"
    End Function
End Class

Public Class SessionKey
    Public Property Method As EncryptionMethod
    Public Property Uri As String

    Public Overrides Function ToString() As String
        Return $"{{
  Method: {Method},
  Uri: {Uri}
}}"
    End Function
End Class

Public Enum EncryptionMethod
    NONE
    AES_128
    SAMPLE_AES
End Enum
