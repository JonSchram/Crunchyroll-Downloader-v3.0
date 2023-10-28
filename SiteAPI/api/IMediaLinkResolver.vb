Imports SiteAPI.api.common

Public Interface IMediaLinkResolver(Of TIn As MediaLink, TOut As Media)

    Function ResolveMedia(link As TIn) As Task(Of TOut)

End Interface
