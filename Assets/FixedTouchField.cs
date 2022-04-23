using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float adjusted = 0.1f;
    public Vector2 TouchDist { get; private set; }
    public Vector2 LastTouchDist { get; private set; }
    protected Vector2 PointerOld;
    protected int PointerId;
    protected bool Pressed;

    public UnityEvent pointerDown = new UnityEvent();
    public UnityEvent pointerUp = new UnityEvent();
    public UnityEvent pointerHold = new UnityEvent();
    public bool IsPressed => Pressed;
    private void Start()
    {
        LastTouchDist = Vector2.zero;
    }
    void Update()
    {
        if (Pressed)
        {
            pointerHold.Invoke();
            if (PointerId == 0 && PointerId < Input.touches.Length)
            {
                TouchDist = Input.touches[PointerId].position * adjusted - PointerOld;
                PointerOld = Input.touches[PointerId].position * adjusted;
                LastTouchDist = TouchDist;
            }
            else
            {
                TouchDist = (new Vector2(Input.mousePosition.x, Input.mousePosition.y)) * adjusted - PointerOld;
                PointerOld = Input.mousePosition * adjusted;
                LastTouchDist = TouchDist;
            }
        }
        else
        {
            TouchDist = new Vector2();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //TimeHolder = 0f;
        //MaxDist = 0f;
        pointerDown.Invoke();
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position * adjusted;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        pointerUp.Invoke();
        Pressed = false;
    }

}