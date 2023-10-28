Imports Crunchyroll_Downloader.api.common

Namespace api
    ''' <summary>
    ''' A combination of language and region to precisely identify language settings.
    ''' </summary>
    Public Class Locale
        Public ReadOnly Property Language As Language
        Public ReadOnly Property Region As Region

        Public Sub New(language As Language, region As Region)
            Me.Language = language
            Me.Region = region
        End Sub

        Public Sub New(language As Language)
            Me.Language = language
            Region = Region.NOT_SPECIFIED
        End Sub

        ''' <summary>
        ''' Tests whether the languages are the same, ignoring region.
        ''' </summary>
        ''' <param name="other"></param>
        ''' <returns></returns>
        Public Function ApproximateMatch(other As Locale) As Boolean
            Return Language = other.Language
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            Dim locale = TryCast(obj, Locale)
            Return locale IsNot Nothing AndAlso
                   Language = locale.Language AndAlso
                   Region = locale.Region
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return (Language, Region).GetHashCode()
        End Function
    End Class
End Namespace