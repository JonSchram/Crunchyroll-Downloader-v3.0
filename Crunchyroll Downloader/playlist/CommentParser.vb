Public Class CommentParser

    ' The comment in the M3U8 is similar to CSV butnot quite - it contains quoted values in the middle of fields, and follows a "KEY=VALUE" pattern
    ' Each of these is separated by a comma

    Public Function ParseCommentString(Input As String) As CommentAttributes
        Dim Result = New SettableCommentAttributes()

        If Input.Length > 0 Then
            Dim colonPosition = Input.IndexOf(":"c)
            Dim startPosition = colonPosition + 1
            If (Input.Length > startPosition) Then
                Dim commentSuffix = Input.Substring(colonPosition + 1)
                Dim quotedMode = False
                Dim key As String = ""
                Dim value As String = ""
                Dim currentString = ""
                For Each character In commentSuffix
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
                                Result.setAttribute(key, value)
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
                If (key <> "") Then
                    Result.setAttribute(key, currentString)
                End If
            End If
        End If
        Return Result
    End Function
    Private Class SettableCommentAttributes
        Inherits CommentAttributes

        Public Sub setAttribute(Key As String, Value As String)
            attributeDictionary.Add(Key, Value)
        End Sub
    End Class
End Class

Public MustInherit Class CommentAttributes
    Friend attributeDictionary As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Public Function GetAttribute(Key As String) As String
        If (attributeDictionary.ContainsKey(Key)) Then
            Return attributeDictionary(Key)
        End If
        Return Nothing
    End Function
End Class

