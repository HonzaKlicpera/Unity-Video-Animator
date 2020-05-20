using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class CharacterSimpleMovement : MonoBehaviour
{
    public VAnimSimpleCharacterController animationController { get; private set; }
    protected NavMeshAgent agent;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        animationController = GetComponent<VAnimSimpleCharacterController>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void FixedUpdate()
    {
        animationController.velocity = agent.velocity.magnitude;
    }

    public bool IsDestinationReached()
    {
        return !agent.pathPending
                && agent.remainingDistance <= agent.stoppingDistance
                && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }

    public bool SetNewTarget(Vector3 target, float maxAreaDistance)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(target, out hit, maxAreaDistance, 1))
        {
            agent.SetDestination(hit.position);
            var lookDirection = (hit.position - transform.position).normalized;
            animationController.SetLookDirection(lookDirection.x, lookDirection.z);
            return true;
        }
        return false;
    }
}
