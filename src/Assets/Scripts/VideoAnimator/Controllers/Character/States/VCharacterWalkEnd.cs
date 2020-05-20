using UnityEngine.Video;
using VStateMachines;

public class VCharacterWalkEnd : VDirectionalState<VAnimCharacterController>
{

    public VCharacterWalkEnd(VAnimCharacterController owner) : base(owner) { }

    public override void EnterState()
    {
        owner.videoPlayer.isLooping = false;
        lookDirection = owner.lookDirection;
        owner.walkingEndClips.SetAnimationClip(owner.videoPlayer, lookDirection);
    }


    public override void FrameUpdated(VideoPlayer source, long frameIdx)
    {
        var distance = owner.walkingEndClips.GetVelocityForFrame(lookDirection, frameIdx);
        owner.characterMovement.AddTravelDistance(distance);
    }

    public override void LoopReached(VideoPlayer source)
    {
        owner.ChangeState(new VCharacterIdle(owner));
        owner.characterMovement.PathPointReached();
    }

    public override void ExitState()
    {
        owner.characterMovement.isMoving = false;
    }

    public override void UpdateState()
    {
    }
}