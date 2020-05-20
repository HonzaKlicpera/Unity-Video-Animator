using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using VStateMachines;

public class VSittingDownState : VState<VAnimStandUpController>
{
    public VSittingDownState(VAnimStandUpController owner) : base(owner) { }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void FrameUpdated(VideoPlayer source, long frameIdx)
    {
        throw new System.NotImplementedException();
    }

    public override void LoopReached(VideoPlayer source)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
