Namespace utilities.ffmpeg.codec
    Public Enum VideoCodec
        COPY
        ' Software
        LIBX264
        LIBX265
        LIBXAVS2
        LIBSVTAV1
        ' NVIDIA
        H264_NVENC
        HEVC_NVENC
        AV1_NVENC
        ' AMD
        H264_AMF
        HEVC_AMF
        AV1_AMF
        ' Intel
        H264_QSV
        HEVC_QSV
        AV1_QSV
    End Enum
End Namespace