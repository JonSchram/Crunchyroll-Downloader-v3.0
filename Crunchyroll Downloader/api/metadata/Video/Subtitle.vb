Imports Newtonsoft.Json.Linq

Public Class Subtitle
    Public Property Language As String
    Public Property Format As String

    Public Property Path As String

    Public Property Type As SubtitleType

    Public Shared Function CreateFromJToken(subToken As JToken) As Subtitle
        Dim path = subToken.Item("filePath")
        Dim language = subToken.Item("languageCode")
        Dim format = subToken.Item("fileExt")
        Dim type = subToken.Item("contentType")

        Return New Subtitle() With {
            .Path = path.Value(Of String),
            .Format = format.Value(Of String),
            .Language = language.Value(Of String),
            .Type = ConvertTypeStringToType(type.Value(Of String))
        }
    End Function

    Private Shared Function ConvertTypeStringToType(type As String) As SubtitleType
        Select Case type
            Case "full"
                Return SubtitleType.FULL
            Case "cc"
                Return SubtitleType.CLOSED_CAPTIONS
            Case Else
                Return SubtitleType.UNKNOWN
        End Select
    End Function

    Public Enum SubtitleType
        UNKNOWN
        FULL
        CLOSED_CAPTIONS
    End Enum
End Class
