using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void SwipeAction(Vector2 distance);

public class InputController : Singleton<InputController>
{
    public bool IsSwipping { get; private set; }

    Vector2 startSwipePosition;// endSwipePosition;
    float currentSwipeTime;
    bool isDragging = false;

    public event SwipeAction OnSwipeCompleted;
    public event Action OnSwipeCancel, OnSwipeStarted;

    private const float swipeTime = 1f, minSwipeDistance = 50f;

    protected override void Awake()
    {
        base.Awake();

        IsSwipping = false;
    }

    public bool Touched()
    {
        if (Input.touchSupported)
        {
            return Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended;
        }
        else
        {
            return Input.GetMouseButtonUp(0);
        }

    }
    public bool TouchIsOverControlPanel()
    {
        return true;
        //Vector2 pos = GetTouchPosition();
        //EventSystem eventSystem = EventSystem.current;
        //PointerEventData eventData = new PointerEventData(eventSystem);
        //eventData.position = pos;
        //List<RaycastResult> raycastResults = new List<RaycastResult>();
        //eventSystem.RaycastAll(eventData, raycastResults);

        //if (raycastResults != null && raycastResults.Count > 0)
        //{
        //    return raycastResults[0].gameObject.name == "GameAreaPanel";
        //}
        //else
        //{
        //    return false;
        //}
    }

    private void Update()
    {
        if (isDragging)
        {
            if (TouchStopped())
            {
                isDragging = false;
                IsSwipping = false;

                Vector2 touchPos = GetTouchPosition();
                Vector2 distance = touchPos - startSwipePosition;
                if ((Mathf.Abs(distance.x) >= minSwipeDistance || Mathf.Abs(distance.y) >= minSwipeDistance) && currentSwipeTime <= swipeTime)
                {
                  //  endSwipePosition = touchPos;
                    OnSwipeCompleted?.Invoke(distance);

                  //  endSwipePosition = Vector3.zero;
                    startSwipePosition = Vector3.zero;
                }
                else
                {
                    OnSwipeCancel?.Invoke();
                }
            }
            else
            {
                Vector2 touchPos = GetTouchPosition();
                if (touchPos == startSwipePosition)
                {
                    currentSwipeTime = 0f;
                    startSwipePosition = touchPos;
                }
                else
                {
                    IsSwipping = true;
                    currentSwipeTime += Time.deltaTime;
                }
            }
        }
        else
        {
            if (TouchStarted())
            {
                startSwipePosition = GetTouchPosition();
                currentSwipeTime = 0f;
                isDragging = true;

                OnSwipeStarted?.Invoke();
            }
        }
    }

    //public bool SwipeTimeIsValid()
    //{
    //    return currentSwipeTime > 0f && currentSwipeTime <= swipeTime;
    //}

    bool TouchStarted()
    {
        if (Input.touchSupported)
        {
            return Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began;
        }
        else
        {
            return Input.GetMouseButtonDown(0);
        }
    }

    bool TouchStopped()
    {
        if (Input.touchSupported)
        {
            return Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended;
        }
        else
        {
            return Input.GetMouseButtonUp(0);
        }
    }

    //public bool SwipeCrossedRectInside(Vector2 center, Vector2 size)
    //{
    //    return PointCrossedRectInside(startSwipePosition, center, size) || PointCrossedRectInside(endSwipePosition, center, size);
    //}

    //bool PointCrossedRectInside(Vector2 point, Vector2 center, Vector2 size)
    //{
    //    float halfHeight = size.y / 2f;
    //    if ((center.y + halfHeight) < point.y || (center.y - halfHeight) > point.y)
    //    {
    //        return false;
    //    }

    //    float halfWidth = size.x / 2f;
    //    if ((center.x + halfWidth) < point.x || (center.x - halfWidth) > point.x)
    //    {
    //        return false;
    //    }

    //    return true;
    //}

    public static Vector2 GetTouchPosition()
    {
        if (Input.touchSupported)
        {
            return Input.GetTouch(0).position;
        }
        else
        {
            return Input.mousePosition;
        }
    }
}
