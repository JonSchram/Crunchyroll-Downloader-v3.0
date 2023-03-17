Imports Crunchyroll_Downloader.settings.ProgramSettings

Namespace settings

    ''' <summary>
    ''' Class that makes it easier to associate an enum value with text to display in a UI element such as a combo box.
    ''' Exposes methods to get a list of objects to display in the component and a method to return the enum value from the item
    ''' (such as when retrieving the selected item in a combo box).
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    Public Class EnumTextList(Of T)
        Private EntryMap As Dictionary(Of T, EnumDisplayEntry) = New Dictionary(Of T, EnumDisplayEntry)
        Private textList As List(Of String) = New List(Of String)


        Public Function Add(enumValue As T, displayText As String) As EnumTextList(Of T)
            EntryMap.Add(enumValue, New EnumDisplayEntry(enumValue, displayText))
            textList.Add(displayText)
            Return Me
        End Function

        Public Function GetEnums() As List(Of T)
            Return EntryMap.Keys.ToList
        End Function

        Public Function GetTextList() As List(Of String)
            Return textList
        End Function

        Public Function GetDisplayItems() As List(Of EnumDisplayEntry)
            Return EntryMap.Values.ToList
        End Function
        Public Function GetEnumForItem(item As Object) As T
            If item Is Nothing Or TypeOf item IsNot EnumDisplayEntry Then
                Return Nothing
            End If
            Dim displayItem = CType(item, EnumDisplayEntry)
            Return displayItem.GetEnumValue()
        End Function

        Public Function Item(value As T) As EnumDisplayEntry
            Return EntryMap.Item(value)
        End Function

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