Imports System.Collections.Immutable
Imports PlaylistLibrary.hls.common
Imports PlaylistLibrary.hls.playlist.rendition

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


        Public Function GetAutoselectRenditions() As IEnumerable(Of T)
            Return Renditions.Where(Function(rendition As T)
                                        Return rendition.Autoselect
                                    End Function)
        End Function

        ''' <summary>
        ''' Gets a single rendition from the group.
        ''' The rendition returned is the highest non-null rendition with the following priority:
        ''' <list type="number">
        ''' <item>The default in the group</item>
        ''' <item>The first rendition with autoselect set</item>
        ''' <item>If neither of the above, any arbitrary rendition.</item>
        ''' </list>
        ''' </summary>
        ''' <returns></returns>
        Public Function GetSingleRendition() As T
            Dim defaultRendition = GetDefaultRendition()
            If defaultRendition IsNot Nothing Then
                Return defaultRendition
            End If

            Dim autoselectRenditions As IEnumerable(Of T) = GetAutoselectRenditions()
            If autoselectRenditions.Count() > 0 Then
                Return autoselectRenditions.First()
            End If

            If Renditions.Count > 0 Then
                Return Renditions.First()
            End If

            Return Nothing
        End Function

        Public Overrides Function ToString() As String
            Return $"[RenditionGroup: type: {Type}, renditions: {Renditions}]"
            Return MyBase.ToString()
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