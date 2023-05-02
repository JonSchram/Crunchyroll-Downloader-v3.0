Namespace hls.tags
    Public MustInherit Class Tag
        Protected TagName As String
        Protected Values As List(Of String) = New List(Of String)
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
        Public Function GetValues() As List(Of String)
            Return Values
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

        Public Function GetRemainingAttributes(excludeAttributes As String()) As IEnumerable(Of KeyValuePair(Of String, String))
            Return AttributeDictionary.Where(
                Function(value As KeyValuePair(Of String, String))
                    Return Not excludeAttributes.Contains(value.Key)
                End Function
            )
        End Function
    End Class
End Namespace