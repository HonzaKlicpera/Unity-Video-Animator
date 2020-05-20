using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachines;

public class FollowingState : State<SheepMovement>
{
    public FollowingState(SheepMovement owner) : base(owner) { }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        /*
        owner.MoveSheepTowardsPlayer();
        float distanceFromPlayer = owner.DistanceFromPlayer();
        if (distanceFromPlayer < owner.radiusToPlayer || distanceFromPlayer > owner.viewDistance)
        {
            owner.stateMachine.ChangeState(new IdleState(owner));
        }
        */
    }
}
