Imports System.ComponentModel

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
        Dim SubLists As New List(Of SubTextList)


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
            Return GetItemForEnum(value)
        End Function

        ''' <summary>
        ''' Returns 
        ''' </summary>
        ''' <returns></returns>
        Public Function CreateSubList() As SubTextList
            Return CreateSubList(OrderType.NO_ORDER)
        End Function

        Public Function CreateSubList(Order As OrderType) As SubTextList
            Return CreateSubList(Order, False)
        End Function

        Public Function CreateSubList(Order As OrderType, reverse As Boolean) As SubTextList
            Dim newSubList As New SubTextList(Me, Order, reverse)
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
        Public Class SubTextList
            Private ReadOnly Ordering As OrderType
            Private ReadOnly reverseOrder As Boolean

            Private ReadOnly parentTextList As EnumTextList(Of T)
            Private ReadOnly BoundList As New BindingList(Of EnumDisplayEntry)()

            Public Sub New(parent As EnumTextList(Of T))
                Me.New(parent, OrderType.NO_ORDER)
            End Sub
            Public Sub New(parent As EnumTextList(Of T), order As OrderType)
                Me.New(parent, order, False)
            End Sub

            Public Sub New(parent As EnumTextList(Of T), order As OrderType, reverse As Boolean)
                parentTextList = parent
                Ordering = order
                reverseOrder = reverse
            End Sub

            Public Sub AddFromParent(value As T)
                Select Case Ordering
                    Case OrderType.NO_ORDER
                        Add(value)
                    Case OrderType.ALPHABETICAL
                        InsertAlphabetical(value)
                    Case OrderType.PARENT_ORDER
                        InsertParentOrder(value)
                End Select
            End Sub

            Private Sub Add(value As T)
                Dim newItem = parentTextList.GetItemForEnum(value)
                If reverseOrder Then
                    BoundList.Insert(0, newItem)
                Else
                    BoundList.Add(newItem)
                End If
            End Sub

            Private Sub InsertAlphabetical(value As T)
                Dim newItem = parentTextList.GetItemForEnum(value)

                If reverseOrder Then
                    Dim insertIndex = BoundList.Count - 1
                    While insertIndex >= 0
                        If newItem.EnumText < BoundList.Item(insertIndex).EnumText Then
                            BoundList.Insert(insertIndex, newItem)
                            Return
                        End If
                        insertIndex -= 1
                    End While
                    BoundList.Insert(0, newItem)
                Else
                    Dim insertIndex = 0
                    While insertIndex < BoundList.Count
                        If newItem.EnumText >= BoundList.Item(insertIndex).EnumText Then
                            BoundList.Insert(insertIndex, newItem)
                            Return
                        End If
                        insertIndex += 1
                    End While
                    BoundList.Add(newItem)
                End If
            End Sub

            Private Sub InsertParentOrder(value As T)
                Dim newItem = parentTextList.GetItemForEnum(value)

                If reverseOrder Then
                    Dim insertIndex = BoundList.Count - 1
                    While insertIndex >= 0
                        Dim parentIndex = parentTextList.DisplayItemsBinding.IndexOf(newItem)
                        If insertIndex < parentIndex Then
                            BoundList.Insert(insertIndex, newItem)
                            Return
                        End If
                        insertIndex -= 1
                    End While
                    BoundList.Insert(0, newItem)
                Else
                    Dim insertIndex = 0
                    While insertIndex < BoundList.Count
                        Dim parentIndex = parentTextList.DisplayItemsBinding.IndexOf(newItem)
                        If insertIndex >= parentIndex Then
                            BoundList.Insert(insertIndex, newItem)
                            Return
                        End If
                        insertIndex += 1
                    End While
                    BoundList.Add(newItem)
                End If
            End Sub

            Public Sub RemoveEnum(value As T)
                Dim removed = True
                While removed
                    removed = BoundList.Remove(parentTextList.GetItemForEnum(value))
                End While
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
            Public ReadOnly Property EnumText As String

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

    Public Enum OrderType
        ''' <summary>
        '''   No sort order. New items are added to the end of the sub list (or the beginning in reverse order).
        ''' </summary>
        NO_ORDER
        ''' <summary>
        ''' New items are added to the proper location in the sub list according to the alphabetical order of display text (or reverse).
        ''' </summary>
        ALPHABETICAL
        ''' <summary>
        ''' New items are added to the location in the sub list to match the order they exist in the parent list (or in reverse of this order).
        ''' </summary>
        PARENT_ORDER
    End Enum

End Namespace