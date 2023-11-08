Imports SiteAPI.api
Imports SiteAPI.api.common

Namespace data
    Public Class MediaFileEntry
        Public ReadOnly Property Location As String
        Public ReadOnly Property ContainedMedia As MediaType
        Public ReadOnly Property StreamLocales As Dictionary(Of MediaType, Locale)

        Public Sub New(location As String, media As MediaType, locales As IDictionary(Of MediaType, Locale))
            Me.Location = location
            ContainedMedia = media
            If locales IsNot Nothing Then
                StreamLocales = New Dictionary(Of MediaType, Locale)(locales)
            Else
                StreamLocales = New Dictionary(Of MediaType, Locale)()
            End If
        End Sub

        Public Sub New(location As String, media As MediaType, mediaLocale As Locale)
            Me.Location = location
            ContainedMedia = media
            StreamLocales = BuildDictionary(media, mediaLocale)
        End Sub

        Private Function BuildDictionary(media As MediaType, l As Locale) As Dictionary(Of MediaType, Locale)
            Dim result As New Dictionary(Of MediaType, Locale)()

            If l Is Nothing Then
                Return result
            End If

            For Each mt As MediaType In {MediaType.Video, MediaType.Audio, MediaType.Subtitles}
                If media.HasFlag(mt) Then
                    result.Add(mt, l)
                End If
            Next

            Return result
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            Dim entry = TryCast(obj, MediaFileEntry)
            Return entry IsNot Nothing AndAlso
                   Location = entry.Location AndAlso
                   ContainedMedia = entry.ContainedMedia AndAlso
                   New DictionaryComparer().Equals(StreamLocales, entry.StreamLocales)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return (Location, ContainedMedia, StreamLocales).GetHashCode()
        End Function

        ' From Servy(https://stackoverflow.com/users/1159478/servy) on Stack Overflow. Permalink: https://stackoverflow.com/a/21758422
        Private Class DictionaryComparer
            Implements IEqualityComparer(Of Dictionary(Of MediaType, Locale))

            Public Overloads Function Equals(x As Dictionary(Of MediaType, Locale), y As Dictionary(Of MediaType, Locale)) As Boolean Implements IEqualityComparer(Of Dictionary(Of MediaType, Locale)).Equals
                If x Is Nothing And y Is Nothing Then
                    Return True
                End If

                If x Is Nothing Xor y Is Nothing Then
                    Return False
                End If

                If x.Count <> y.Count Then
                    Return False
                End If
                If x.Keys.Except(y.Keys).Any() Then
                    Return False
                End If
                If y.Keys.Except(x.Keys).Any() Then
                    Return False
                End If

                For Each item As KeyValuePair(Of MediaType, Locale) In x
                    If Not Equals(item.Value, y.Item(item.Key)) Then
                        Return False
                    End If
                Next
                Return True
            End Function

            Public Overloads Function GetHashCode(obj As Dictionary(Of MediaType, Locale)) As Integer Implements IEqualityComparer(Of Dictionary(Of MediaType, Locale)).GetHashCode
                Return obj.GetHashCode()
            End Function
        End Class
    End Class
End Namespace