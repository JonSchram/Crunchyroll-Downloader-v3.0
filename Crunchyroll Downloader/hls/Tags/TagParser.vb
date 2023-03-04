Public Class TagParser

    Public Function ParseTagString(Input As String) As Tag
        If Input Is Nothing Or Input.Length = 0 Then
            Return Nothing
        End If

        Dim tagStartIndex = Input.IndexOf("#")
        Dim tagEndIndex = Input.IndexOf(":")
        If tagEndIndex < 0 Then
            Return New SettableTag(Input.Substring(tagStartIndex + 1))
        End If

        Dim TagName = Input.Substring(tagStartIndex + 1, tagEndIndex - tagStartIndex - 1)
        Dim Result = New SettableTag(TagName)

        Dim quotedMode = False
        Dim key As String = ""
        Dim value As String = ""
        Dim currentString = ""
        Dim startPosition = tagEndIndex + 1
        For Index As Integer = startPosition To Input.Length - 1
            Dim character As Char = Input(Index)
            If quotedMode Then
                If character = """" Then
                    quotedMode = False
                Else
                    currentString += character
                End If
            Else
                Select Case character
                    Case """"c
                        quotedMode = True
                    Case ","c
                        If key = "" Then
                            key = currentString
                        Else
                            value = currentString
                        End If
                        Result.SetAttribute(key, value)
                        key = ""
                        value = ""
                        currentString = ""
                    Case "="c
                        key = currentString
                        currentString = ""
                    Case Else
                        currentString += character
                End Select
            End If
        Next
        ' Either set the current key to the remainder of the string or if there is no key,
        ' set the value of the tag
        If currentString <> "" Then
            If (key <> "") Then
                Result.SetAttribute(key, currentString)
            Else
                Result.SetValue(currentString)
            End If
        End If
        Return Result
    End Function

    Private Class SettableTag
        Inherits Tag

        Public Sub New(name As String)
            MyBase.New(name)
        End Sub

        Public Sub SetValue(Value As String)
            Me.Value = Value
        End Sub

        Public Sub SetAttribute(Key As String, Value As String)
            HasAttributes = True
            AttributeDictionary.Add(Key, Value)
        End Sub
    End Class
End Class

