using UnityEngine.Video;
using VStateMachines;

public class VCharacterIdle : VDirectionalState<VAnimCharacterController>
{
    public VCharacterIdle(VAnimCharacterController owner) : base(owner) { }

    public override void EnterState()
    {
        owner.videoPlayer.playbackSpeed = 1;
        owner.videoPlayer.isLooping = true;
        owner.idleClips.SetAnimationClip(owner.videoPlayer, owner.lookDirection);
        owner.videoPlayer.Play();
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {

    }

    public override void FrameUpdated(VideoPlayer source, long frameIdx)
    {
        
    }

    public override void LoopReached(VideoPlayer source)
    {
        
    }
}
