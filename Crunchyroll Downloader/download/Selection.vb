Imports System.Collections.Immutable

Namespace download
    ''' <summary>
    ''' Class representing a single video selected for download. This is everything that should be played together, such as the audio and subtitle language.
    ''' </summary>
    Public Class Selection
        Public ReadOnly Property Media As IReadOnlyList(Of Media)

        Public Sub New(sources As List(Of Media))
            Media = ImmutableList.CreateRange(sources)
        End Sub

    End Class
End Namespace