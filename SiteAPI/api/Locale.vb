Imports SiteAPI.api.common

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

        Public Overrides Function ToString() As String
            ' TODO: Make this easy to internationalize. Interpolating strings like this isn't the way.
            If Region = Region.NOT_SPECIFIED Then
                Return GetLanguageString(Language)
            Else
                Return $"{GetLanguageString(Language)} ({GetRegionString(Region)})"
            End If
        End Function

        Public Shared Function GetLanguageString(lang As Language) As String
            ' TODO: Make this easy to internationalize.
            Select Case lang
                Case Language.ARABIC
                    Return "Arabic"
                Case Language.ENGLISH
                    Return "English"
                Case Language.FRENCH
                    Return "French"
                Case Language.GERMAN
                    Return "German"
                Case Language.ITALIAN
                    Return "Italian"
                Case Language.JAPANESE
                    Return "Japanese"
                Case Language.MANDARIN
                    Return "Mandarin"
                Case Language.PORTUGUESE
                    Return "Portuguese"
                Case Language.RUSSIAN
                    Return "Russian"
                Case Language.SPANISH
                    Return "Spanish"
                Case Else
                    Return "None"
            End Select
        End Function

        Public Shared Function GetRegionString(region As Region) As String
            Select Case region
                Case Region.BRAZIL
                    Return "Brazil"
                Case Region.FRANCE
                    Return "France"
                Case Region.GERMANY
                    Return "Germany"
                Case Region.ITALY
                    Return "Italy"
                Case Region.JAPAN
                    Return "Japan"
                Case Region.LATIN_AMERICA
                    Return "Latin America"
                Case Region.PORTUGAL
                    Return "Portugal"
                Case Region.RUSSIA
                    Return "Russia"
                Case Region.SAUDI_ARABIA
                    Return "Saudi Arabia"
                Case Region.SPAIN
                    Return "Spain"
                Case Region.UNITED_STATES
                    Return "United States"
                Case Else
                    Return "Not specified"
            End Select
        End Function

    End Class
End Namespace