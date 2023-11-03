Namespace utilities.ffmpeg

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

End Namespace