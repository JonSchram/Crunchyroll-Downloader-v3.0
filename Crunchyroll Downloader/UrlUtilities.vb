Module UrlUtilities
    Public Function isFunimationUrl(url As String) As Boolean
        Return safeContains(url, "funimation.com")
    End Function

    Public Function isCrunchyrollUrl(url As String) As Boolean
        Return safeContains(url, "crunchyroll.com")
    End Function

    Public Function isTestUrl(url As String) As Boolean
        Return safeContains(url, "Test=true")
    End Function

    Private Function safeContains(testString As String, containedString As String) As Boolean
        If (testString Is Nothing) Then
            Return False
        End If
        Return testString.Contains(containedString)
    End Function
End Module