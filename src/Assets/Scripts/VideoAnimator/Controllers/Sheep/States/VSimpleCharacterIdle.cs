using UnityEngine.Video;
using VStateMachines;

public class VSimpleCharacterIdle : VDirectionalState<VAnimSimpleCharacterController>
{
    public VSimpleCharacterIdle(VAnimSimpleCharacterController owner): base(owner) { }

    public override void EnterState()
    {
        owner.idleClips.SetAnimationClip(owner.videoPlayer, owner.lookDirection);
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        if (owner.velocity > 0.1)
            owner.ChangeState(new VSimpleCharacterWalking(owner));
    }

    public override void FrameUpdated(VideoPlayer source, long frameIdx)
    {
    }

    public override void LoopReached(VideoPlayer source)
    {
    }

}
