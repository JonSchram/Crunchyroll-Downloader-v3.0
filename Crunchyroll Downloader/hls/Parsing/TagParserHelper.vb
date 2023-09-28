﻿Imports System.Text

Namespace hls.parsing
    Public Class TagParserHelper
        Public Shared Function ParseTagString(line As String) As TagAttributes
            If line Is Nothing Or line.Length = 0 Then
                Return Nothing
            End If

            If line(0) <> "#" Then
                Return Nothing
            End If

            Dim tagEndIndex = line.IndexOf(":")
            If tagEndIndex < 0 Then
                Return New SettableTag(line.Substring(1))
            End If

            Dim TagName = line.Substring(1, tagEndIndex - 1)
            Dim Result = New SettableTag(TagName)

            Dim quotedMode = False
            Dim key As String = ""
            Dim currentStringBuilder = New StringBuilder()
            Dim startPosition = tagEndIndex + 1
            For Index As Integer = startPosition To line.Length - 1
                Dim character As Char = line(Index)
                If quotedMode Then
                    If character = """" Then
                        quotedMode = False
                    Else
                        currentStringBuilder.Append(character)
                    End If
                Else
                    Select Case character
                        Case """"c
                            quotedMode = True
                        Case ","c
                            If key = "" Then
                                Result.AddValue(currentStringBuilder.ToString())
                            Else
                                Result.SetAttribute(key, currentStringBuilder.ToString())
                            End If
                            key = ""
                            currentStringBuilder.Clear()
                        Case "="c
                            key = currentStringBuilder.ToString()
                            currentStringBuilder.Clear()
                        Case Else
                            currentStringBuilder.Append(character)
                    End Select
                End If
            Next
            ' Either set the current key to the remainder of the string or if there is no key,
            ' set the value of the tag
            If currentStringBuilder.Length > 0 Then
                If (key <> "") Then
                    Result.SetAttribute(key, currentStringBuilder.ToString())
                Else
                    Result.AddValue(currentStringBuilder.ToString())
                End If
            End If
            Return Result
        End Function



        Private Class SettableTag
            Inherits TagAttributes

            Public Sub New(name As String)
                MyBase.New(name)
            End Sub
            Public Sub New()
                MyBase.New("")
            End Sub

            Public Sub AddValue(Value As String)
                Values.Add(Value)
            End Sub

            Public Sub SetAttribute(Key As String, Value As String)
                HasAttributes = True
                AttributeDictionary.Add(Key, Value)
            End Sub
        End Class
    End Class
End Namespace