Imports System.Text

Namespace utilities
    Public Class FfmpegCommandBuilder
        Public Function BuildCommandLineArguments(arguments As FfmpegArguments, Cookies As Dictionary(Of String, String),
                                                  userAgent As String) As String
            Dim result = ""

            Dim headers As String = BuildHeaders(Cookies)
            If Not "".Equals(headers) Then
                result += $" -headers ""{headers}"""
            End If

            If userAgent IsNot Nothing Then
                result += $" -user_agent ""{userAgent}"""
            End If

            If arguments.PlaylistLocation IsNot Nothing Then
                result += $" -i ""{arguments.PlaylistLocation}"""
            End If

            ' TODO: Make program number selection less brittle. This works fine with one input file but doesn't scale,
            ' and doesn't fit with mapping other stream types.
            If arguments.ProgramNumber.HasValue Then
                result += $" -map 0:p:{arguments.ProgramNumber.Value}"
            End If

            result += $" ""{arguments.OutputPath}"""

            Return result
        End Function

        Private Function BuildHeaders(Cookies As Dictionary(Of String, String)) As String
            Dim resultBuilder As New StringBuilder()
            If Cookies.Count > 0 Then
                resultBuilder.Append("Cookie: ")
                ' From https://stackoverflow.com/a/13610649
                resultBuilder.Append(String.Join("; ", Cookies.Select(Function(kvp) String.Format("{0}={1}", kvp.Key, kvp.Value))))
                ' Cookie header must end in \r\n.
                resultBuilder.Append(vbCrLf)
            End If

            Return resultBuilder.ToString()
        End Function
    End Class
End Namespace