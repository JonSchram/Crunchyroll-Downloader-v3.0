Imports System.ComponentModel
Imports System.Runtime.Serialization
Imports Crunchyroll_Downloader.settings.ProgramSettings

Namespace settings

    ''' <summary>
    ''' Class that makes it easier to associate an enum value with text to display in a UI element such as a combo box.
    ''' Exposes methods to get a list of objects to display in the component and a method to return the enum value from the item
    ''' (such as when retrieving the selected item in a combo box).
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    Public Class EnumTextList(Of T)
        Private ReadOnly EntryMap As New Dictionary(Of T, EnumDisplayEntry)
        Private ReadOnly textList As New List(Of String)
        ' Maintain a binding list so that if the display items are bound to a control, they are automatically updated.
        Private ReadOnly DisplayItemsBinding As New BindingList(Of EnumDisplayEntry)()
        ' Sub-lists that have been created (so they can be cleared when the main list is)
        Dim SubLists As List(Of SubTextList)


        Public Function Add(enumValue As T, displayText As String) As EnumTextList(Of T)
            Dim displayEntry = New EnumDisplayEntry(enumValue, displayText)
            EntryMap.Add(enumValue, displayEntry)
            textList.Add(displayText)
            DisplayItemsBinding.Add(displayEntry)
            Return Me
        End Function

        Public Function GetEnums() As List(Of T)
            Return EntryMap.Keys.ToList
        End Function

        Public Function GetTextList() As List(Of String)
            Return textList
        End Function

        Public Function GetDisplayItems() As BindingList(Of EnumDisplayEntry)
            Return DisplayItemsBinding
        End Function
        Public Function GetEnumForItem(item As Object) As T
            If item Is Nothing Or TypeOf item IsNot EnumDisplayEntry Then
                Return Nothing
            End If
            Dim displayItem = CType(item, EnumDisplayEntry)
            Return displayItem.GetEnumValue()
        End Function
        Public Function GetItemForEnum(enumValue As T) As EnumDisplayEntry
            Return EntryMap.Item(enumValue)
        End Function

        Public Function Item(value As T) As EnumDisplayEntry
            Return EntryMap.Item(value)
        End Function

        ''' <summary>
        ''' Returns 
        ''' </summary>
        ''' <returns></returns>
        Public Function CreateSubList() As SubTextList
            Dim newSubList As New SubTextList(Me)
            SubLists.Add(newSubList)
            Return newSubList
        End Function

        Public Sub Clear()
            EntryMap.Clear()
            textList.Clear()
            DisplayItemsBinding.Clear()
            ' Make sure no sub list is using the old display entries.
            For Each subList In SubLists
                subList.Clear()
            Next
        End Sub

        ''' <summary>
        ''' A list populated from the same pool of display items but containing a different subset of these items.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        Public Class SubTextList
            Private ReadOnly parentTextList As EnumTextList(Of T)
            Private ReadOnly BoundList As New BindingList(Of EnumDisplayEntry)

            Public Sub New(parent As EnumTextList(Of T))
                parentTextList = parent
            End Sub

            Public Sub AddFromParent(value As T)
                BoundList.Add(parentTextList.GetItemForEnum(value))
            End Sub

            Public Sub RemoveEnum(value As T)
                For Each displayEntry In BoundList
                    If displayEntry.GetEnumValue().Equals(value) Then
                        BoundList.Remove(displayEntry)
                    End If
                Next
            End Sub

            Public Sub Clear()
                BoundList.Clear()
            End Sub

            Public Function GetDisplayItems() As BindingList(Of EnumDisplayEntry)
                Return BoundList
            End Function
        End Class

        Public Class EnumDisplayEntry
            Private ReadOnly Property EnumValue As T
            Private ReadOnly Property EnumText As String

            Public Sub New(value As T, text As String)
                EnumValue = value
                EnumText = text
            End Sub

            Friend Function GetEnumValue() As T
                Return EnumValue
            End Function

            Public Overrides Function ToString() As String
                Return EnumText
            End Function
        End Class
    End Class

End Namespace