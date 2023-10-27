Namespace hls.parsing
    Public MustInherit Class ParsedTag
        Protected TagName As String
        Protected Values As New List(Of String)
        Protected HasAttributes As Boolean
        Protected AttributeDictionary As New Dictionary(Of String, PlaylistData)

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

        Public Function GetAttribute(Key As String) As PlaylistData
            If AttributeDictionary.ContainsKey(Key) Then
                Return AttributeDictionary(Key)
            End If
            Return Nothing
        End Function

        Public Function GetRemainingAttributes(excludeAttributes As String()) As IEnumerable(Of KeyValuePair(Of String, PlaylistData))
            Return AttributeDictionary.Where(
                Function(value As KeyValuePair(Of String, PlaylistData))
                    Return Not excludeAttributes.Contains(value.Key)
                End Function
            )
        End Function

    End Class
End Namespace