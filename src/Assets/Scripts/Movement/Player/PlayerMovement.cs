using UnityEngine;

public class PlayerMovement : CharacterNavMeshMovement
{
    //Layer mask defining which layers can be clicked upon for the player to move
    public LayerMask clickMask;
    //Layer mask defining which layers block the click raycast, so that the player does not move, even if valid surface is behind 
    //the layer.
    public LayerMask blockClickMask;

    Camera mainCamera;

    LineRenderer lr;

    protected override void Awake()
    {
        base.Awake();
        ClickManager.Instance();
        ClickManager.OnClickAndRelease += PlayerMoveInputUpdate;

        if (!mainCamera)
            mainCamera = FindObjectOfType<Camera>();

        lr = GetComponent<LineRenderer>();
    }

    void OnDrawGizmosSelected(Vector3[] path)
    {

        if (path != null && path.Length > 1)
        {
            lr.positionCount = path.Length;
            for (int i = 0; i < path.Length; i++)
            {
                lr.SetPosition(i, path[i]);
            }
        }

    }

    void Update ()
    {
        if (drawDebugLine)
            OnDrawGizmosSelected(currentPath.corners);

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void PlayerMoveInputUpdate()
    {
        var positionOnScreen = Input.mousePosition;
        SetPositionToTravel(positionOnScreen);
    }

    /// <summary>
    /// Checks whether the position on screen is valid (not blocked by blockclickmask and has a valid navmesh path).
    /// If valid, sets the new destination and begins the animation.
    /// </summary>
    /// <param name="positionOnScreen"></param>
    void SetPositionToTravel(Vector2 positionOnScreen)
    {
        Ray ray = mainCamera.ScreenPointToRay(positionOnScreen);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickMask | blockClickMask, QueryTriggerInteraction.Ignore))
        {
            //Check if click is blocked by blockClickMask
            if (!IsInLayerMask(hit.collider.gameObject.layer, blockClickMask))
            {
                var path = CalculateNavmeshPath(hit.point);
                SetPathAndBeginAnimation(path);
            }
        }
    }

    /// <summary>
    /// Used to determine whether a layer is present inside of a layermask, using bit operations.
    /// </summary>
    /// <param name="layer">Layer to be tested</param>
    /// <param name="layermask">Layermask to be tested</param>
    /// <returns>True if layer is present inside the layermask</returns>
    bool IsInLayerMask(int layer, LayerMask layermask)
    {
        return layermask == (layermask | (1 << layer));
    }
}
