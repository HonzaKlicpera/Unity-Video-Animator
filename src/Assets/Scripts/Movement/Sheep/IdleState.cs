using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachines;

public class IdleState : State<SheepMovement>
{
    public IdleState(SheepMovement owner): base(owner) { }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        if (Random.Range(0, 150) == 0)
            owner.sheepBehaviour.ChangeState(new RoamingState(owner));
    }
}
