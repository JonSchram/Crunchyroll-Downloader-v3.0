Imports System.Text

Namespace utilities
    Public Class FfmpegCommandBuilder
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

            argumentParts.Add($"""{arguments.OutputPath}""")

            Return String.Join(" ", argumentParts)
        End Function

        Private Function BuildMappings(MapList As List(Of FfmpegArguments.MapArgument)) As String
            Dim mapArguments As New List(Of String)

            For Each map As FfmpegArguments.MapArgument In MapList
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

        Private Function BuildCodecs(Codecs As List(Of FfmpegArguments.CodecArgument)) As String
            Dim codecArguments As New List(Of String)

            For Each codec As FfmpegArguments.CodecArgument In Codecs
                Dim argumentBuilder As New StringBuilder()

                argumentBuilder.Append("-c")
                If codec.AppliedStream IsNot Nothing Then
                    argumentBuilder.AppendFormat(":{0}", BuildStreamSpecifier(codec.AppliedStream))
                End If
                argumentBuilder.AppendFormat(" {0}", GetCodecString(codec.Name))

                codecArguments.Add(argumentBuilder.ToString())
            Next

            Return String.Join(" ", codecArguments)
        End Function

        Private Function BuildStreamSpecifier(Selector As FfmpegArguments.StreamSpecifier) As String
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

        Private Function GetStreamTypeSelector(Type As FfmpegArguments.StreamType) As String
            Select Case Type
                Case FfmpegArguments.StreamType.AUDIO
                    Return "a"
                Case FfmpegArguments.StreamType.VIDEO_ONLY
                    Return "V"
                Case FfmpegArguments.StreamType.VIDEO_AND_ATTACHMENTS
                    Return "v"
                Case FfmpegArguments.StreamType.SUBTITLE
                    Return "s"
                Case FfmpegArguments.StreamType.DATA
                    Return "d"
                Case FfmpegArguments.StreamType.ATTACHMENT
                    Return "t"
                Case Else
                    Return ""
            End Select
        End Function

        Private Function GetCodecString(name As FfmpegArguments.CodecName) As String
            Select Case name
                Case FfmpegArguments.CodecName.COPY
                    Return "copy"
                Case FfmpegArguments.CodecName.VIDEO_LIBX264
                    Return "libx264"
                Case FfmpegArguments.CodecName.VIDEO_LIBX265
                    Return "libx265"
                Case FfmpegArguments.CodecName.VIDEO_LIBXAVS2
                    Return "libxavs2"
                Case FfmpegArguments.CodecName.VIDEO_H264_NVENC
                    Return "h264_nvenc"
                Case FfmpegArguments.CodecName.VIDEO_HEVC_NVENC
                    Return "hevc_nvenc "
                Case FfmpegArguments.CodecName.VIDEO_H264_AMF
                    Return "h264_amf"
                Case FfmpegArguments.CodecName.VIDEO_HEVC_AMF
                    Return "hevc_amf"
                Case FfmpegArguments.CodecName.AUDIO_AAC
                    Return "aac"
                Case FfmpegArguments.CodecName.AUDIO_AC3
                    Return "ac3"
                Case FfmpegArguments.CodecName.AUDIO_FLAC
                    Return "flac"
                Case FfmpegArguments.CodecName.AUDIO_OPUS
                    Return "libopus"
                Case FfmpegArguments.CodecName.SUBTITLE_ASS
                    Return "ass"
                Case FfmpegArguments.CodecName.SUBTITLE_SRT
                    Return "srt"
                Case FfmpegArguments.CodecName.SUBTITLE_SSA
                    Return "ssa"
                Case FfmpegArguments.CodecName.SUBTITLE_MOV_TEXT
                    Return "mov_text"
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