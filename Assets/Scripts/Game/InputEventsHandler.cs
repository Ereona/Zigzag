using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputEventsHandler : MonoBehaviour
{
    public VoidEventChannelSO TapEvent;

    private bool overUI = false;

    void Update()
    {
        if (IsPointerOverUI())
        {
            return;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                TapEvent.RaiseEvent();
            }
        }
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            TapEvent.RaiseEvent();
        }
#endif
    }


    public bool IsPointerOverUI()
    {
        if (EventSystem.current == null)
            return false;

        // check mouse
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        //check touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                overUI = EventSystem.current.IsPointerOverGameObject(touch.fingerId);
                return overUI;
            }
            else
            {
                return overUI;
            }

        }

        return false;
    }
}
