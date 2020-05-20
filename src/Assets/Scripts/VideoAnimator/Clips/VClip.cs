using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class VClip
{

    public VideoClip clip;
    public string url;

    public void SetAnimationClip(VideoPlayer videoPlayer)
    {
#if UNITY_WEBGL
        videoPlayer.url = url;
#else
        videoPlayer.clip = clip;
#endif
    }
}
