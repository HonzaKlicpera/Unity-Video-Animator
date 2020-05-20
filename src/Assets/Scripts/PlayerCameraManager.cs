using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerCameraManager : MonoBehaviour
{
    public float maxZoomDistance = 40.0f;
    public float minZoomDistance = 5.0f;
    public float zoomSpeed = 10.0f;

    public float initialZoomDistance = 15.0f;

    new Rigidbody rigidbody;

    CinemachineVirtualCamera virtualCamera;
    Transform playerTransform;
    bool followingPlayer = false;

    bool enterBorder;

    Vector3 groundPos;

    // Start is called before the first frame update
    void Start()
    {
        ClickManager.Instance();
        ClickManager.OnClickAndDragBegin += DragBegin;
        ClickManager.OnClickAndDragEnd += DragEnd;
        ClickManager.OnDragGetVelocity += Dragging;
        ClickManager.OnClickDown += ClickDown;
        rigidbody = GetComponent<Rigidbody>();
        enterBorder = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        var mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        virtualCamera = mainCam.GetComponentInChildren<CinemachineVirtualCamera>();

        SetInitialZoomSliderPos();
    }

    void SetInitialZoomSliderPos()
    {
        var pos = transform.position;
        groundPos = pos;

        var newPos = new Vector3(pos.x, pos.y + initialZoomDistance, pos.z - initialZoomDistance);
        transform.position = newPos;
    }

    void ClickDown()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    void DragBegin()
    {
    }

    void Dragging(Vector3 dragVelocity)
    {
        if(!followingPlayer)
            transform.position += dragVelocity / 1000 * GetDistanceFromGround();
    }

    void DragEnd(Vector3 endDragVelocity)
    {
        if(!followingPlayer)
            rigidbody.AddForce(endDragVelocity * 500);
    }

    float GetDistanceFromGround()
    {
        return transform.position.y - groundPos.y;
    }

    void SwitchCameraFollow()
    {
        if (!playerTransform)
        {
            Debug.Log("Player not found in the scene!");
            return;
        }

        if (followingPlayer)
        {
            transform.parent = null;
            SetTransposerDamping(0.0f);
            followingPlayer = false;
        }

        else
        {
            SetTransposerDamping(1.0f);
            rigidbody.velocity = Vector3.zero;
            transform.parent = playerTransform;
            transform.localPosition = new Vector3(0.0f, GetDistanceFromGround() , -GetDistanceFromGround() + 2.5f);
            transform.localScale = Vector3.one;
            followingPlayer = true;
        }
    }

    void SetTransposerDamping(float damping)
    {
        var transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_XDamping = damping;
        transposer.m_YDamping = damping;
        transposer.m_ZDamping = damping;
    }

    // Update is called once per frame
    void Update()
    {
        var zoomInput = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        var zoomSliderDistance = GetDistanceFromGround();

        if (Input.GetKeyDown(KeyCode.F))
            SwitchCameraFollow();

        if ((zoomInput < 0.0f && zoomSliderDistance < maxZoomDistance) ||
            (zoomInput > 0.0f && zoomSliderDistance > minZoomDistance))
            rigidbody.AddForce(new Vector3(0, -zoomInput, zoomInput));
        else if ((zoomSliderDistance > maxZoomDistance || zoomSliderDistance < minZoomDistance) && !enterBorder)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            enterBorder = true;
        }

        if (zoomSliderDistance < maxZoomDistance && zoomSliderDistance > minZoomDistance)
            enterBorder = false;
    }
}
