using System.Collections;
using System.Collections.Generic;
using StateMachines;
using UnityEngine;

public class RoamingState : State<SheepMovement>
{
    public float roamRange = 10.0f;

    public RoamingState(SheepMovement owner) : base(owner) { }

    public override void EnterState()
    {
        Vector3 sheepPos = owner.transform.position;
        float xInc = Random.Range(-roamRange, roamRange);
        float zInc = Random.Range(-xInc/5, xInc/5);
        var roamTarget = sheepPos + new Vector3(xInc, 0, zInc);
        if (!owner.SetNewTarget(roamTarget, roamRange))
            owner.sheepBehaviour.ChangeState(new IdleState(owner));
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        if (owner.IsDestinationReached())
            owner.sheepBehaviour.ChangeState(new IdleState(owner));
    }
}
