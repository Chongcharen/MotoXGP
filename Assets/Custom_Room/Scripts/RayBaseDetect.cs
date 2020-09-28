using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayBaseDetect : MonoBehaviour
{
    public Collider coll;
    private bool isSelectBase = false;
    public Transform modelBase;

    private Vector2 touchStartPos;
    private Vector2 touchCurrentPos;

    private float maxR = 0;
    private float maxL = 0;

    private float accY = 0;

    enum MoveState
    {
        idle,
        rotateL,
        rotateR
    }
    
    MoveState State = MoveState.idle;

    public bool GetIsSelectBase() {
        return isSelectBase;
    }

    public void ClearIsSelectBase()
    {
        isSelectBase = false;
    }

    void Update()
    {
        // Move this object to the position clicked by the mouse.
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (coll.Raycast(ray, out hit, 100.0f))
            {
                isSelectBase = true;
            }
        }
    }

    void OnGUI()
    {
        if (State != MoveState.idle)
        {
            if (State == MoveState.rotateR)
            {
                accY += (170*Time.deltaTime);
                if (accY > maxR)
                {
                    accY = maxR;
                    State = MoveState.idle;
                    isSelectBase = false;
                }
                modelBase.eulerAngles = new Vector3(0, accY, 0);
            }

            if (State == MoveState.rotateL)
            {
                accY -= (170 * Time.deltaTime);
                if (accY < maxL)
                {
                    accY = maxL;
                    State = MoveState.idle;
                    isSelectBase = false;
                }
                modelBase.eulerAngles = new Vector3(0, accY, 0);
            }
            return;
        }

#if UNITY_ANDROID
		if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        if (coll.Raycast(ray, out hit, 100.0f))
                        {
                            isSelectBase = true;
                        }

				        touchStartPos = Input.GetTouch(0).position;
                        touchCurrentPos = touchStartPos;

                    }
                break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    if(isSelectBase){
                        touchCurrentPos = Input.GetTouch(0).position;

                        float difX = (touchCurrentPos.x - touchStartPos.x);
                        const float ratio = 50.0f;
                        if (Mathf.Abs(difX) > ratio)
                        {
                            //rotate L
                            if(touchCurrentPos.x > touchStartPos.x)
                            {
                                State = MoveState.rotateL;
                                maxL = accY - 180;
                            }

                            //rotate R
                            if (touchCurrentPos.x < touchStartPos.x)
                            {
                                State = MoveState.rotateR;

                                maxR = accY + 180;
                            }
                        }
                    }
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    isSelectBase = false;
                    break;
            }
        }
#elif UNITY_STANDALONE_WIN
        Event e = Event.current;
        if (e.type == EventType.MouseUp)
        {
            isSelectBase = false;
        }
        else
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (coll.Raycast(ray, out hit, 100.0f))
            {
                isSelectBase = true;
            }

            touchStartPos = e.mousePosition;
            touchCurrentPos = touchStartPos;
        }
        else
        if (isSelectBase && e.type == EventType.MouseDrag)
        {
            touchCurrentPos = e.mousePosition;
            float difX = (touchCurrentPos.x - touchStartPos.x);
            const float ratio = 50.0f;
            if (Mathf.Abs(difX) > ratio)
            {
                //rotate L
                if(touchCurrentPos.x > touchStartPos.x)
                {
                    State = MoveState.rotateL;
                    maxL = accY - 180;
                }

                //rotate R
                if (touchCurrentPos.x < touchStartPos.x)
                {
                    State = MoveState.rotateR;
                    maxR = accY + 180;
                }
            }
        }
#endif

    }
}
