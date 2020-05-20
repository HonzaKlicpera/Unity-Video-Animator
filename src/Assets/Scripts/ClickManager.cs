using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClickManager : MonoBehaviour
{
    private const int SCROLL_TOLERANCE_PX = 5;
    private const int SCROLL_BUFFER_SAMPLE_SIZE = 5;

    private static ClickManager instance;

    public static ClickManager Instance()
    {
        if (instance == null)
            instance = new GameObject("[Click Manager]").AddComponent<ClickManager>();
        return instance;
    }

    public delegate void ClickDown();
    public static event ClickDown OnClickDown;

    public delegate void ClickAndRelease();
    public static event ClickAndRelease OnClickAndRelease;

    public delegate void ClickAndDragBegin();
    public static event ClickAndDragBegin OnClickAndDragBegin;

    public delegate void ClickAndDragEnd(Vector3 endVelocityAvg);
    public static event ClickAndDragEnd OnClickAndDragEnd;

    public delegate void DragVelocity(Vector3 velocity);
    public static event DragVelocity OnDragGetVelocity;

    private bool dragging = false;

    private Vector3 initialMousePosition;

    private Vector3 previousMousePosition;

    private Queue<Vector3> dragDistanceBuffer;


    // Start is called before the first frame update
    void Start()
    {
        dragDistanceBuffer = new Queue<Vector3>();
    }

    void UpdateDragBuffer(Vector3 scrollVelocity)
    {
        if (dragDistanceBuffer.Count >= SCROLL_BUFFER_SAMPLE_SIZE)
            dragDistanceBuffer.Dequeue();
        dragDistanceBuffer.Enqueue(scrollVelocity);
    }

    Vector3 GetDragBufferAverage()
    {
        return dragDistanceBuffer.Aggregate(Vector3.zero, (acc, v) => acc + v) / dragDistanceBuffer.Count();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClickDown?.Invoke();
            dragDistanceBuffer.Clear();
            initialMousePosition = Input.mousePosition;
            previousMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {

            if (Vector3.Distance(initialMousePosition, Input.mousePosition) >= SCROLL_TOLERANCE_PX && !dragging)
            {
                OnClickAndDragBegin?.Invoke();
                dragging = true;
            }
            else if (dragging)
            {
                var dragVelocity = previousMousePosition - Input.mousePosition;
                var currentDragVelocity = new Vector3(dragVelocity.x, 0, dragVelocity.y);
                OnDragGetVelocity?.Invoke(currentDragVelocity);
                UpdateDragBuffer(currentDragVelocity);
                previousMousePosition = Input.mousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (dragging)
            {
                OnClickAndDragEnd?.Invoke(GetDragBufferAverage());
                dragging = false;
            }
            else
                OnClickAndRelease?.Invoke();
        }
    }
}
