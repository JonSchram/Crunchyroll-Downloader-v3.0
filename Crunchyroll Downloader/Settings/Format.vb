Public Class Format
    Private Property MergeSetting As Merge
    Private Property FileFormat As MediaFormat

    Public Sub New(FileFormat As MediaFormat, MergeSetting As Merge)
        Dim allowedMergeSettings = GetValidMergeOptions(FileFormat)
        If Not allowedMergeSettings.Contains(MergeSetting) Then
            Throw New ArgumentException($"Merge setting {MergeSetting} not valid with {FileFormat}")
        End If

        Me.FileFormat = FileFormat
        Me.MergeSetting = MergeSetting
    End Sub

    Public Shared Function GetValidMergeOptions(Format As MediaFormat) As List(Of Merge)
        Select Case Format
            Case MediaFormat.MP4
                Return New List(Of Merge)({Merge.DISABLED, Merge.MOVE_TEXT})
            Case MediaFormat.MKV
                Return New List(Of Merge)({Merge.DISABLED, Merge.COPY, Merge.SRT})
            Case MediaFormat.AAC_AUDIO_ONLY
                Return New List(Of Merge)({Merge.DISABLED})
        End Select
    End Function

    Public Enum MediaFormat
        MP4
        MKV
        AAC_AUDIO_ONLY
    End Enum

    Public Enum Merge
        DISABLED
        MOVE_TEXT
        COPY
        SRT
    End Enum
End Class
