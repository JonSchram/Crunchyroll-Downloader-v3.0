Public MustInherit Class Tag
    Protected TagName As String
    Protected Value As String
    Protected HasAttributes As Boolean
    Protected AttributeDictionary As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Public Sub New(name As String)
        TagName = name
    End Sub

    Public Function getTagName() As String
        Return TagName
    End Function

    ''' <summary>
    ''' Returns the value of this tag if there are no named attributes
    ''' </summary>
    ''' <returns></returns>
    Public Function GetValue() As String
        Return Value
    End Function

    Public Function GetHasAttributes() As Boolean
        Return HasAttributes
    End Function

    Public Function GetAttribute(Key As String) As String
        If (AttributeDictionary.ContainsKey(Key)) Then
            Return AttributeDictionary(Key)
        End If
        Return Nothing
    End Function
End Class

