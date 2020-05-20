using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using VStateMachines;

public class VSimpleCharacterWalking : VDirectionalState<VAnimSimpleCharacterController>
{
    public VSimpleCharacterWalking(VAnimSimpleCharacterController owner): base(owner) { }

    public override void EnterState()
    {
        owner.walkingClips.SetAnimationClip(owner.videoPlayer, owner.lookDirection);
    }

    public override void ExitState()
    {
    }

    public override void FrameUpdated(VideoPlayer source, long frameIdx)
    {

    }

    public override void LoopReached(VideoPlayer source)
    {
    }

    public override void UpdateState()
    {
        if (owner.velocity <= 0.1)
            owner.ChangeState(new VSimpleCharacterIdle(owner));
    }
}
