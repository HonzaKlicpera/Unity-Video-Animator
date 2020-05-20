using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using VStateMachines;

public class VSittingState : VState<VAnimStandUpController>
{
    bool standingUp = false;

    public VSittingState(VAnimStandUpController owner) : base(owner) { }

    public override void EnterState()
    {
        owner.OnStandCommand += StandUpCommand;
        owner.standUpClip.SetAnimationClip(owner.videoPlayer);
    }

    public override void ExitState()
    {
        owner.OnStandCommand -= StandUpCommand;
    }

    public override void FrameUpdated(VideoPlayer source, long frameIdx)
    {
    }

    public override void LoopReached(VideoPlayer source)
    {
        owner.ChangeState(new VStandingState(owner));
    }

    public override void UpdateState()
    {
    }

    void StandUpCommand()
    {
        if (!standingUp)
        {
            owner.videoPlayer.Play();
            standingUp = true;
        }
    }
}
