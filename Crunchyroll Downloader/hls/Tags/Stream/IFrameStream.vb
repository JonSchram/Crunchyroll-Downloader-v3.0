Imports Microsoft.VisualBasic.Devices
''' <summary>
''' Represents an I-frame only video stream.
''' </summary>
Public Class IFrameStream
    Inherits AbstractStream

    Public Sub New(SourceTag As Tag)
        MyBase.New(SourceTag)

        If GetTagName() <> SourceTag.getTagName() Then
            Throw New ArgumentException($"Tag {SourceTag.getTagName()} is incorrect for an I-frame stream, expected {GetTagName()}")
        End If

        Dim uri = SourceTag.GetAttribute("URI")
        If uri Is Nothing Then
            Throw New HlsFormatException($"{GetTagName()} requires a URI.")
        End If
        Me._uri = uri

    End Sub

    Protected Overrides Function GetTagName() As String
        Return "EXT-X-I-FRAME-STREAM-INF"
    End Function



    Public Overrides Function ToString() As String
        Return $"{{
  Resolution: {StreamResolution.ToString()},
  Bandwidth: {Bandwidth},
  AverageBandwidth: {AverageBandwidth},
  Uri: {Uri}
  Video: {Video},
  HdcpLevel: {HdcpLevel},
  Codecs: {Codecs}
}}"
    End Function
End Class
