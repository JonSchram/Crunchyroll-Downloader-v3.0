Imports System.Collections.Immutable
Imports Crunchyroll_Downloader.hls.common
Imports Crunchyroll_Downloader.hls.playlist.rendition

Namespace hls.playlist.stream
    Public Class RenditionGroup(Of T As AlternativeRendition)
        Public ReadOnly Property Type As MediaType

        Public ReadOnly Property Renditions As IReadOnlyList(Of T)

        Public Sub New(type As MediaType, renditions As List(Of T))
            Me.Type = type
            Me.Renditions = ImmutableList.CreateRange(renditions)
        End Sub

        Public Function GetDefaultRendition() As T
            For Each rendition As T In Renditions
                If rendition.IsDefault Then
                    Return rendition
                End If
            Next
            Return Nothing
        End Function

        Public Overrides Function ToString() As String
            Return $"[RenditionGroup: type: {Type}, renditions: {Renditions}]"
            Return MyBase.ToString()
        End Function

        Public Function GetAutoselectRenditions() As IEnumerable(Of T)
            Return Renditions.Where(Function(rendition As T)
                                        Return rendition.Autoselect
                                    End Function)
        End Function

        Public Class Builder
            Private Type As MediaType
            Private ReadOnly Renditions As New List(Of T)

            Public Sub SetType(type As MediaType)
                Me.Type = type
            End Sub

            Public Sub AddRendition(rendition As T)
                Renditions.Add(rendition)
            End Sub

            Public Function Build() As RenditionGroup(Of T)
                Return New RenditionGroup(Of T)(Type, Renditions)
            End Function
        End Class
    End Class
End Namespace