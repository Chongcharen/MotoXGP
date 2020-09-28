using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PinchZoom : MonoBehaviour
{
    private Vector3 moveToPos;

	public float maxZoomIn = -2;
	public float maxZoomOut = -10;

	public float perspectiveZoomSpeed = 0.1f;        // The rate of change of the field of view in perspective mode.
													 //public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
													 //public float pinchRatio = 0.5f;

#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
	private Vector2 lastestMousePos;
#endif

#if UNITY_ANDROID
	void Update ()
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
void OnGUI()
#endif
	{
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		Event e = Event.current;
#endif
		//Do not process when touch on any UI object.
		if (EventSystem.current.IsPointerOverGameObject())
            return;

#if UNITY_ANDROID
		// If there are two touches on the device...
		if (Input.touchCount == 2 && EventSystem.current.currentSelectedGameObject == null)
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
//		if (EventSystem.current.currentSelectedGameObject == null )
#endif
		{
#if UNITY_ANDROID
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            float moveValue = Mathf.Abs(deltaMagnitudeDiff);


            Vector2 touchZerovDir = (touchZero.deltaPosition).normalized;
            Vector2 touchOneDir = (touchOne.deltaPosition).normalized;

            float dotResult = Vector2.Dot(touchZerovDir, touchOneDir);

            if (dotResult < 0.8)
            {
				if (deltaMagnitudeDiff < 0)//zoom in
				{
					
                    float nextMoveValue = (transform.localPosition.z - moveValue * perspectiveZoomSpeed * Time.deltaTime);
					if (nextMoveValue > maxZoomIn)
						transform.Translate(0.0f, 0.0f, moveValue * perspectiveZoomSpeed * Time.deltaTime);
					else
					{
                        transform.localPosition = new Vector3(0.0f, 0.0f, maxZoomIn);
                    }
                }
				else
				if(deltaMagnitudeDiff > 0)//zoom out
				{
                    float nextMoveValue = (transform.localPosition.z + moveValue * perspectiveZoomSpeed * Time.deltaTime);
					if (nextMoveValue < maxZoomOut)
                    {
                        transform.Translate(0.0f, 0.0f, -moveValue * perspectiveZoomSpeed * Time.deltaTime);
                    }
                    else
					{
                        transform.localPosition = new Vector3(0.0f, 0.0f, maxZoomOut);
                    }
                }				
            }
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX

            if (e.type == EventType.MouseDown && e.button == 1)
			{
                lastestMousePos = e.mousePosition;
			}
			else
			if (e.type == EventType.MouseDrag && e.button == 1)
			{
                float deltaMagnitudeDiff = (lastestMousePos - e.mousePosition).magnitude;
				if (lastestMousePos.x > e.mousePosition.x || lastestMousePos.y < e.mousePosition.y)//Zoom out
				{
                    float nextMoveValue = (transform.localPosition.z + deltaMagnitudeDiff * perspectiveZoomSpeed * Time.deltaTime);
					if (nextMoveValue < maxZoomOut)
                    {
                        transform.Translate(0.0f, 0.0f, -deltaMagnitudeDiff * perspectiveZoomSpeed * Time.deltaTime);
                    }
                    else
					{
                        transform.localPosition = new Vector3(0.0f, 0.0f, maxZoomOut);
                    }
                }
				else
				if (lastestMousePos.x < e.mousePosition.x || lastestMousePos.y > e.mousePosition.y)//Zoom in
				{
                    float nextMoveValue = (transform.localPosition.z - deltaMagnitudeDiff * perspectiveZoomSpeed * Time.deltaTime);
					if (nextMoveValue > maxZoomIn)
						transform.Translate(0.0f, 0.0f, deltaMagnitudeDiff * perspectiveZoomSpeed * Time.deltaTime);
					else
					{
                        transform.localPosition = new Vector3(0.0f, 0.0f, maxZoomIn);
                    }
                }
				lastestMousePos = e.mousePosition;
			}
#endif
        }
    }
}