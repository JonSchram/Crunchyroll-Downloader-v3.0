Imports Newtonsoft.Json.Linq

Namespace api.crunchyroll.metadata.cms
    Public Class StreamFormat
        Public Property FormatName As String
        Public Property HardsubVersions As Dictionary(Of String, StreamEntry)

        Public Sub New()
            Me.New("")
        End Sub

        Public Sub New(name As String)
            HardsubVersions = New Dictionary(Of String, StreamEntry)()
            FormatName = name
        End Sub

        Public Shared Function createFromJToken(token As JProperty) As StreamFormat
            Dim name As String = token.Name
            Dim children = token.Value

            Dim result As New StreamFormat(name)
            For Each child In children
                If TypeOf child Is JProperty Then
                    Dim childProperty = CType(child, JProperty)
                    Dim hardsubLocale As String = childProperty.Name
                    Dim s As StreamEntry = StreamEntry.CreateFromJToken(childProperty.Value)
                    result.HardsubVersions.Add(hardsubLocale, s)
                End If
            Next

            Return result
        End Function
    End Class
End Namespace