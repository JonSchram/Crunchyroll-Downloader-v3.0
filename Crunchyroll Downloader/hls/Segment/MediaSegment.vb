Public Class MediaSegment

    Public Property Duration As Double
    Public Property Title As String
    Public Property Bytes As ByteRange

    Public Property Uri As String

    ' This isn't explicitly listed in a playlist file, but is calculated as they are added to a parsed object
    Public Property SegmentNumber As Integer
End Class
