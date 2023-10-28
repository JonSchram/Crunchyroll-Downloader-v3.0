Module UrlUtilities
    Function IsFunimationUrl(url As String) As Boolean
        Return SafeContains(url, "funimation.com")
    End Function

    Function IsFunimationVideoUrl(url As String) As Boolean
        Return SafeContains(url, "funimation.com/v/")
    End Function

    Function IsCrunchyrollUrl(url As String) As Boolean
        Return SafeContains(url, "crunchyroll.com")
    End Function

    Function IsTestUrl(url As String) As Boolean
        Return SafeContains(url, "Test=true")
    End Function

    Function SafeContains(testString As String, containedString As String) As Boolean
        If testString Is Nothing Then
            Return False
        End If
        Return testString.Contains(containedString)
    End Function
End Module