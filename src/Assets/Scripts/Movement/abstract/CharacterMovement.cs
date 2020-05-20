using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class CharacterMovement : MonoBehaviour
{
    protected float movementMultiplier { get; set; }
    protected float interpolatedDistance { get; set; }
    protected float distanceBuffer { get; set; }
    protected float prevDistance { get; set; }

    Vector3 lookDirection;

    public bool isMoving { get; set; }

    public bool interpolate = true;
    public bool drawDebugLine = false;

    protected VAnimCharacterController animationController;

    public float minMovementMultiplier = 0.85f;
    public float maxMovementMultiplier = 1.15f;

    public float playbackRateShift = 0.15f;

    protected Vector3 currentTargetPoint;

    void FixedUpdate()
    {
        if (isMoving)
            UpdateCharacterMovement();
    }

    /// <summary>
    /// Because the animation framerate is generally not synced up with the fixed update framerate (and is also lower),
    /// the distance buffer is sometimes empty when fixed update occurs. That creates jittering, as the character is not
    /// animated smoothly. To prevent that from happening, this method tries to divide up the distance between animation updates
    /// into smaller roughly equal parts, so that some distance is applied every fixed update call.
    /// </summary>
    void Interpolate()
    {
        //get the approximate update ratio
        var updateRatio = Time.fixedDeltaTime 
            * animationController.videoPlayer.frameRate
            * animationController.videoPlayer.playbackSpeed; 

        var interpolateVal = prevDistance * updateRatio;
        //if the distance buffer is empty, then it is filled with the interpolated value
        if (distanceBuffer == 0.0f)
        {
            distanceBuffer += interpolateVal;
            interpolatedDistance += interpolateVal;
        }
        //if the distance buffer is not empty, then a part of it is cut away from it
        else if (interpolateVal < distanceBuffer)
        {
            var valueToTake = distanceBuffer - interpolateVal;
            if (valueToTake > interpolatedDistance)
                valueToTake = interpolatedDistance;
            distanceBuffer -= valueToTake;
            interpolatedDistance -= valueToTake;
        }
    }

    /// <summary>
    /// Applies distance from the distance buffer multiplied by the movement multiplier to
    /// the characters transform. Also interpolates the distance if turned on.
    /// </summary>
    void UpdateCharacterMovement()
    {
        if (interpolate)
            Interpolate();

        var velocityVector = lookDirection * distanceBuffer * movementMultiplier;
        transform.position += velocityVector;
        distanceBuffer = 0.0f;
    }

    public void UpdateLookDirection()
    {
        lookDirection = currentTargetPoint - transform.position;
        lookDirection.Normalize();
        animationController.SetLookDirection(lookDirection.x, lookDirection.z);
    }

    public void SetMovementMultiplier(float multiplier)
    {
        movementMultiplier = Mathf.Clamp(multiplier, minMovementMultiplier, maxMovementMultiplier);
        SetAnimationPlaybackRate(multiplier);
    }

    public void SetAnimationPlaybackRate(float multiplier)
    {
        multiplier += playbackRateShift;
        var minPlaybackSpeed = playbackRateShift + minMovementMultiplier;
        var maxPlaybackSpeed = playbackRateShift + maxMovementMultiplier;
        animationController.videoPlayer.playbackSpeed = Mathf.Clamp(multiplier, minPlaybackSpeed, maxPlaybackSpeed);
    }

    public float GetRemainingDistance()
    {
        return Vector3.Distance(transform.position, currentTargetPoint);
    }

    public void AddTravelDistance(float distance)
    {
        prevDistance = distance;
        if (distance == 0.0f)
            isMoving = false;
        else
            isMoving = true;
        distanceBuffer += distance;
    }

    public abstract void PathPointReached();
}
