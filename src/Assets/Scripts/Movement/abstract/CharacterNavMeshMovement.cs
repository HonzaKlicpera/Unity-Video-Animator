using UnityEngine;
using UnityEngine.AI;

public abstract class CharacterNavMeshMovement : CharacterMovement
{

    protected NavMeshPath currentPath;
    protected NavMeshAgent agent;
    protected int currentCornerId;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<VAnimCharacterController>();
        currentPath = new NavMeshPath();
    }

    public bool IsCurrentPathPointFinal()
    {
        return currentCornerId == currentPath.corners.Length - 1;
    }

    public override void PathPointReached()
    {
        if (!IsCurrentPathPointFinal())
        {
            currentCornerId++;
            BeginWalkingAnimation();
        }
    }

    protected NavMeshPath CalculateNavmeshPath(Vector3 target)
    {
        var path = new NavMeshPath();
        agent.enabled = true;
        agent.CalculatePath(target, path);
        agent.enabled = false;

        return path;
    }

    protected void SetPathAndBeginAnimation(NavMeshPath path)
    {
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            currentPath = path;
            currentCornerId = 1;
            BeginWalkingAnimation();
        }
    }

    protected void BeginWalkingAnimation()
    {
        if(currentPath.status == NavMeshPathStatus.PathComplete)
        {
            currentTargetPoint = currentPath.corners[currentCornerId];
            interpolatedDistance = 0.0f;
            UpdateLookDirection();
            SetMovementMultiplier(1.0f);
            animationController.ChangeState(new VCharacterWalkBegin(animationController));
        }
    }
}

