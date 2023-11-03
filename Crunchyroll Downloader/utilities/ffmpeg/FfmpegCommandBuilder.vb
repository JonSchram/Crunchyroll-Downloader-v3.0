Imports System.Text
Imports Crunchyroll_Downloader.utilities.ffmpeg.codec

Namespace utilities.ffmpeg
    Public Class FfmpegCommandBuilder

        Public Function BuildCommandLineArguments(arguments As FfmpegArguments) As String
            Return BuildCommandLineArguments(arguments, Nothing, Nothing)
        End Function

        Public Function BuildCommandLineArguments(arguments As FfmpegArguments, Cookies As Dictionary(Of String, String),
                                                  userAgent As String) As String
            Dim argumentParts As New List(Of String)()

            Dim headers As String = BuildHeaders(Cookies)
            If Not "".Equals(headers) Then
                argumentParts.Add($"-headers ""{headers}""")
            End If

            If userAgent IsNot Nothing Then
                argumentParts.Add($"-user_agent ""{userAgent}""")
            End If

            For Each inputFile In arguments.InputFiles
                argumentParts.Add($"-i ""{inputFile}""")
            Next

            If arguments.SelectedStreams.Count > 0 Then
                argumentParts.Add(BuildMappings(arguments.SelectedStreams))
            End If

            If arguments.Codecs.Count > 0 Then
                argumentParts.Add(BuildCodecs(arguments.Codecs))
            End If

            If arguments.Preset IsNot Nothing Then
                argumentParts.Add(arguments.Preset.BuildCommandLineArgument())
            End If

            argumentParts.Add($"""{arguments.OutputPath}""")

            Return String.Join(" ", argumentParts)
        End Function

        Private Function BuildMappings(MapList As List(Of MapArgument)) As String
            Dim mapArguments As New List(Of String)

            For Each map As MapArgument In MapList
                Dim argumentBuilder As New StringBuilder()

                argumentBuilder.Append("-map ")
                If map.Exclude Then
                    argumentBuilder.Append("-")
                End If
                argumentBuilder.Append(map.InputFileNumber)
                Dim selectorString As String = BuildStreamSpecifier(map.Selector)
                If Not "".Equals(selectorString) Then
                    argumentBuilder.AppendFormat(":{0}", selectorString)
                End If
                If map.IsOptional Then
                    argumentBuilder.Append("?")
                End If
                mapArguments.Add(argumentBuilder.ToString())
            Next

            Return String.Join(" ", mapArguments)
        End Function

        Private Function BuildCodecs(Codecs As List(Of ICodecArgument)) As String
            Dim codecArguments As New List(Of String)

            For Each codec As ICodecArgument In Codecs
                Dim argumentBuilder As New StringBuilder()

                argumentBuilder.Append("-c")
                If codec.AppliedStream IsNot Nothing Then
                    Dim specifier As String = BuildStreamSpecifier(codec.AppliedStream)
                    If Not "".Equals(specifier) Then
                        argumentBuilder.AppendFormat(":{0}", specifier)
                    End If
                End If
                argumentBuilder.AppendFormat(" {0}", codec.GetCodecString())

                codecArguments.Add(argumentBuilder.ToString())
            Next

            Return String.Join(" ", codecArguments)
        End Function

        Private Function BuildStreamSpecifier(Selector As StreamSpecifier) As String
            If Selector Is Nothing Then
                Return ""
            End If

            Dim parts As New List(Of String)

            If Selector.ProgramNumber.HasValue Then
                parts.Add($"p:{Selector.ProgramNumber.Value}")
            End If
            If Selector.Type.HasValue Then
                parts.Add(GetStreamTypeSelector(Selector.Type.Value))
            End If
            If Selector.StreamIndex.HasValue Then
                parts.Add(Selector.StreamIndex.Value.ToString())
            End If

            Return String.Join(":", parts)
        End Function

        Private Function GetStreamTypeSelector(Type As StreamType) As String
            Select Case Type
                Case StreamType.AUDIO
                    Return "a"
                Case StreamType.VIDEO_ONLY
                    Return "V"
                Case StreamType.VIDEO_AND_ATTACHMENTS
                    Return "v"
                Case StreamType.SUBTITLE
                    Return "s"
                Case StreamType.DATA
                    Return "d"
                Case StreamType.ATTACHMENT
                    Return "t"
                Case Else
                    Return ""
            End Select
        End Function

        Private Function BuildHeaders(Cookies As Dictionary(Of String, String)) As String
            Dim resultBuilder As New StringBuilder()
            If Cookies?.Count > 0 Then
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