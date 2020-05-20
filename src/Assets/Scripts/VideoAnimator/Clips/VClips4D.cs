
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

class UndefinedLookDirectionException: System.Exception
{

}

[System.Serializable]
public class VClips4D
{
    public VClip clipLeft;
    public VClip clipRight;
    public VClip clipUp;
    public VClip clipDown;


    public void SetAnimationClip(VideoPlayer videoPlayer, LookDirection lookDirection)
    {
        switch (lookDirection)
        {
            case LookDirection.UP:
                clipUp.SetAnimationClip(videoPlayer);
                break;
            case LookDirection.RIGHT:
                clipRight.SetAnimationClip(videoPlayer);
                break;
            case LookDirection.DOWN:
                clipDown.SetAnimationClip(videoPlayer);
                break;
            case LookDirection.LEFT:
                clipLeft.SetAnimationClip(videoPlayer);
                break;
            default:
                throw new UndefinedLookDirectionException();
        }
    }

}
