using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using VStateMachines;

public class VStandingState : VState<VAnimStandUpController>
{
    bool sittingDown = false;

    public VStandingState(VAnimStandUpController owner): base(owner) { }

    public override void EnterState()
    {
        owner.sitDownClip.SetAnimationClip(owner.videoPlayer);
        owner.OnStandCommand += SitDownCommand;
    }

    public override void ExitState()
    {
        owner.OnStandCommand -= SitDownCommand;
    }

    public override void FrameUpdated(VideoPlayer source, long frameIdx)
    {
    }

    public override void LoopReached(VideoPlayer source)
    {
        owner.ChangeState(new VSittingState(owner));
    }

    public override void UpdateState()
    {
    }

    void SitDownCommand()
    {
        if (!sittingDown)
        {
            owner.videoPlayer.Play();
            sittingDown = true;
        }
    }

}
