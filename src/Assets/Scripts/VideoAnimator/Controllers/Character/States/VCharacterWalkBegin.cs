using UnityEngine;
using UnityEngine.Video;
using VStateMachines;

public class VCharacterWalkBegin : VDirectionalState<VAnimCharacterController>
{

    public VCharacterWalkBegin(VAnimCharacterController owner) : base(owner) { }

    public override void EnterState()
    {
        lookDirection = owner.lookDirection;
        owner.walkingStartClips.SetAnimationClip(owner.videoPlayer, lookDirection);
        owner.characterMovement.isMoving = true;
        CalculateDistanceMultiplier();
    }

    void CalculateDistanceMultiplier()
    {
        var beginDist = owner.walkingStartClips.GetVelocitySum(lookDirection);
        var loopDist = owner.walkingLoopClips.GetVelocitySum(lookDirection);
        var endDist = owner.walkingEndClips.GetVelocitySum(lookDirection);

        var remainingDistance = owner.characterMovement.GetRemainingDistance();

        var numOfLoops = Mathf.RoundToInt((remainingDistance - beginDist - endDist) / loopDist);
        owner.loopsLeft = numOfLoops;
        if (numOfLoops <= 0)
            exitFlag = true;
        var animationDistance = beginDist + (numOfLoops * loopDist) + endDist;

        var multiplier = remainingDistance / animationDistance;
        owner.characterMovement.SetMovementMultiplier(multiplier);
    }

    public override void FrameUpdated(VideoPlayer source, long frameIdx)
    {
        
        var distance = owner.walkingStartClips.GetVelocityForFrame(lookDirection, frameIdx);
        owner.characterMovement.AddTravelDistance(distance);
    }

    public override void LoopReached(VideoPlayer source)
    {
        if (exitFlag)
            owner.ChangeState(new VCharacterWalkEnd(owner));
        else
            owner.ChangeState(new VCharacterWalkLoop(owner));
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
    }
}
