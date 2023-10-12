Imports Crunchyroll_Downloader.api.common

Namespace api
    Public Interface IStreamSelector

        ' TODO: This should get the stream with the given language and then get the subtitle and video streams for it.
        ' This is because episodes are usually grouped by audio language, then by sub language
        ' (there are many versions of each subtitle language depending on the audio language and you want the correct one)
        Function GetStreams(type As MediaType, langauges As List(Of Language)) As List(Of MediaLink)

        Function GetStreams(audioLanguage As Language, subtitleLanguages As List(Of Language), streamTypeFlags As MediaType) As List(Of MediaLink)

    End Interface
End Namespace