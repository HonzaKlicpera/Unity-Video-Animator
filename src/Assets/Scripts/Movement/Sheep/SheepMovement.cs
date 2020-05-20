using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachines;
using UnityEngine.AI;

public class SheepMovement : CharacterSimpleMovement
{
    private Transform playerPos;

    public StateMachine<SheepMovement> sheepBehaviour { get; private set; }

    protected override void Start()
    {
        base.Start();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        sheepBehaviour = new StateMachine<SheepMovement>(this);
        sheepBehaviour.ChangeState(new IdleState(this));
    }

    public float DistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, playerPos.position);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        sheepBehaviour.Update();
    }
}
