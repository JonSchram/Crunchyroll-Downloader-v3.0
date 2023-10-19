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

            Public Overrides Function Equals(obj As Object) As Boolean
                Dim argument = TryCast(obj, MapArgument)
                Return argument IsNot Nothing AndAlso
                       InputFileNumber = argument.InputFileNumber AndAlso
                       Exclude = argument.Exclude AndAlso
                       EqualityComparer(Of StreamSpecifier).Default.Equals(Selector, argument.Selector) AndAlso
                       IsOptional = argument.IsOptional
            End Function

            Public Overrides Function GetHashCode() As Integer
                Return (InputFileNumber, Exclude, Selector, IsOptional).GetHashCode()
            End Function
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

            Public Overrides Function Equals(obj As Object) As Boolean
                Dim specifier = TryCast(obj, StreamSpecifier)
                Return specifier IsNot Nothing AndAlso
                       Type.Equals(specifier.Type) AndAlso
                       StreamIndex.Equals(specifier.StreamIndex) AndAlso
                       ProgramNumber.Equals(specifier.ProgramNumber)
            End Function

            Public Overrides Function GetHashCode() As Integer
                Return (Type, StreamIndex, ProgramNumber).GetHashCode()
            End Function
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