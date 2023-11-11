Namespace pipeline
    Public Enum PipelineStage
        ' Starting to process task.
        INITIALIZING = 0

        ' Finding all media available for the task, before choosing which to download.
        FIND_MEDIA = 1

        ' Media has been found, deciding which to download.
        CHOOSE_MEDIA = 2

        ' Downloading all media to a temporary file before processing further.
        DOWNLOAD_MEDIA = 3

        ' Performing additional processing on downloaded media, such as remuxing or reencoding.
        POSTPROCESSING = 4

        ' All processing is done, renaming and moving to final location.
        FINAL_OUTPUT = 5

        ' All processing finished.
        COMPLETED = 6
    End Enum
End Namespace