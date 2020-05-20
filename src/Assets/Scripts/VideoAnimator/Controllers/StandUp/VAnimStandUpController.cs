using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VAnimStandUpController : VAnimController<VAnimStandUpController>
{
    public VClip standUpClip;
    public VClip sitDownClip;

    public delegate void Stand();
    public event Stand OnStandCommand;
    // Start is called before the first frame update
    void Start()
    {
        Initialize(sendFrameReadyEvents: false);
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = false;
        standUpClip.SetAnimationClip(videoPlayer);
        ChangeState(new VStandingState(this));
        OnStandCommand?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            OnStandCommand?.Invoke();
    }

}
