Imports Crunchyroll_Downloader.hls.segment.encryption

Namespace hls.segment
    Public Class EncryptionKey
        Public ReadOnly Property Method As EncryptionMethod
        Public ReadOnly Property Uri As String

        ' A 128-bit number.
        Public ReadOnly Property InitializationVector As Decimal?

        ' May be any string
        Public ReadOnly Property Format As String = "identity"
        ' Optional, requires compatibility version 5 or greater.
        Public ReadOnly Property FormatVersions As String = "1"

        Public Sub New(method As EncryptionMethod, uri As String, iv As Decimal?,
                       format As String, formatVersions As String)
            Me.Method = method
            Me.Uri = uri
            InitializationVector = iv
            Me.Format = format
            Me.FormatVersions = formatVersions
        End Sub

    End Class
End Namespace