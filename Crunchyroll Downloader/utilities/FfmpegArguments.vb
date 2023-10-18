Namespace utilities
    Public Class FfmpegArguments

        Public ReadOnly Property InputFiles As New List(Of String)

        Public ReadOnly Property SelectedStreams As New List(Of MapArgument)

        Public ReadOnly Property Codecs As New List(Of CodecArgument)

        Public ReadOnly Property OutputPath As String

        Public Sub New(OutputLocation As String)
            OutputPath = OutputLocation
        End Sub

        Public Class CodecArgument
            Public Property AppliedStream As StreamSpecifier
            Public Property Name As CodecName
        End Class

        Public Class MapArgument
            ''' <summary>
            ''' Which input file this mapping belongs to.
            ''' Defaults to 0, for the first stream.
            ''' </summary>
            ''' <returns></returns>
            Public Property InputFileNumber As Integer = 0
            ''' <summary>
            ''' Whether to exclude instead of include this item when matching streams.
            ''' </summary>
            ''' <returns></returns>
            Public Property Exclude As Boolean = False

            Public Property Selector As StreamSpecifier

            Public Property IsOptional As Boolean = False
        End Class

        ' TODO: Make codecs more logical. Video and audio codecs are all combined together.
        Public Enum CodecName
            COPY
            VIDEO_LIBX264
            VIDEO_LIBX265
            VIDEO_LIBXAVS2
            VIDEO_H264_NVENC
            VIDEO_HEVC_NVENC
            VIDEO_H264_AMF
            VIDEO_HEVC_AMF
            AUDIO_AAC
            AUDIO_AC3
            AUDIO_FLAC
            AUDIO_OPUS
            SUBTITLE_ASS
            SUBTITLE_SRT
            SUBTITLE_SSA
            SUBTITLE_MOV_TEXT
        End Enum

        Public Class StreamSpecifier
            Public Property Type As StreamType?
            ''' <summary>
            ''' Which stream number to copy.
            ''' </summary>
            ''' <returns></returns>
            Public Property StreamIndex As Integer?

            ''' <summary>
            ''' Which program number to copy streams from.
            ''' If this is specified, it takes effect before the stream index or stream type.
            ''' </summary>
            ''' <returns></returns>

            Public Property ProgramNumber As Integer?
        End Class

        Public Enum StreamType
            AUDIO
            VIDEO_ONLY
            VIDEO_AND_ATTACHMENTS
            SUBTITLE
            DATA
            ATTACHMENT
        End Enum

    End Class
End Namespace