using UnityEngine.Video;
using VStateMachines;

public class VCharacterWalkLoop : VDirectionalState<VAnimCharacterController>
{
    bool validLoop;

    public VCharacterWalkLoop(VAnimCharacterController owner) : base(owner) { }

    public override void EnterState()
    {
        validLoop = false;
        owner.videoPlayer.isLooping = true;
        lookDirection = owner.lookDirection;
        owner.walkingLoopClips.SetAnimationClip(owner.videoPlayer, lookDirection);
        CheckRemainingDistance();
        
        if(exitFlag)
            owner.ChangeState(new VCharacterWalkEnd(owner));
           
    }

    public override void FrameUpdated(VideoPlayer source, long frameIdx)
    {
        if(frameIdx > 0)
            validLoop = true;
        var distance = owner.walkingLoopClips.GetVelocityForFrame(lookDirection, frameIdx);
        owner.characterMovement.AddTravelDistance(distance);
    }

    public override void LoopReached(VideoPlayer source)
    {
        if (validLoop)
        {
            owner.loopsLeft--;
            CheckRemainingDistance();
            if (exitFlag)
                owner.ChangeState(new VCharacterWalkEnd(owner));
        }
        validLoop = false;
    }

    public override void ExitState()
    {
    }


    void CheckRemainingDistance()
    {
        if (owner.loopsLeft == 0)
            exitFlag = true;
        var loopDist = owner.walkingLoopClips.GetVelocitySum(lookDirection) * owner.loopsLeft;
        var endDist = owner.walkingEndClips.GetVelocitySum(lookDirection);
        var animationDistance = loopDist + endDist;
        var remainingDistance = owner.characterMovement.GetRemainingDistance();

        var multiplier = remainingDistance / animationDistance;
        owner.characterMovement.SetMovementMultiplier(multiplier);
    }

    public override void UpdateState()
    {
    }
}