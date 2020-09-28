using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SwipeRotate : MonoBehaviour {

    public Animator anim;
    public bool m_bLockCtrl;
    private Vector3 moveToDir;
    private Vector3 moveToPos;

    private float xAngle = 0.0f; //angle for axes x for rotation
    private float yAngle = 0.0f;
    private float xAngTemp = 0.0f; //temp variable for angle
    private float yAngTemp = 0.0f;

    private Vector2 touchStartPos;
    private Vector2 touchCurrentPos;


    private float touchDuration = 0.0f;
    private float doubleTabDuration = 0.0f;
    private int tabCount = 0;

    private bool movingBack = false;
    private Vector3 initDir;
    public float reFocusSpeed = 20.0f;

    private float ratioWaitDeciseDoubleTab = 0.5f;


    public bool invertX = false;
    public bool invertY = false;
    public bool disableX = false;
    public bool disableY = false;

    public float rangeRX = 50;
    public float rangeRY = 50;
    private float minRX;
    private float maxRX;
    private float minRY;
    private float maxRY;

    RayBaseDetect rayCastDetect;

    // Use this for initialization
    void Start () {

        rayCastDetect = GameObject.Find("Node_Detect_Character_Base").GetComponent<RayBaseDetect>();

        m_bLockCtrl = false;

        initDir = transform.forward;

		xAngle = transform.eulerAngles.y;
        minRX = xAngle - (rangeRX / 2);
        maxRX = xAngle + (rangeRX / 2);
        yAngle = transform.eulerAngles.x;
        minRY = yAngle - (rangeRY / 2);
        maxRY = yAngle + (rangeRY / 2);


    }

    // Update is called once per frame
#if UNITY_ANDROID
	void Update ()
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
    void OnGUI()
#endif
	{
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        Event e = Event.current;
#endif
        //do nothing when base is triggered
        if (rayCastDetect.GetIsSelectBase())
        {
#if UNITY_ANDROID
              if (Input.touchCount > 0) {
				touchStartPos = Input.GetTouch(0).position;
              }
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
                touchStartPos = e.mousePosition;
#endif
                touchCurrentPos = touchStartPos;

                xAngTemp = xAngle;
                yAngTemp = yAngle;

            return;
        }

		//Do not process when touch on any UI object.
		if (EventSystem.current.IsPointerOverGameObject() )
            return;
#if UNITY_ANDROID
		if (Input.touchCount == 1 && EventSystem.current.currentSelectedGameObject == null)
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
//		if (EventSystem.current.currentSelectedGameObject == null)
#endif
		{
#if UNITY_ANDROID
			if (Input.GetTouch(0).phase == TouchPhase.Began )
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
			if (e.type == EventType.MouseDown && e.button == 0)
#endif
			{
#if UNITY_ANDROID
				touchStartPos = Input.GetTouch(0).position;
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
				touchStartPos = e.mousePosition;
#endif
				touchCurrentPos = touchStartPos;

				xAngTemp = xAngle;
				yAngTemp = yAngle;
			}
			else
#if UNITY_ANDROID
			if (Input.GetTouch(0).phase == TouchPhase.Moved)
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
			if (e.type == EventType.MouseDrag && e.button == 0)
#endif
			{
#if UNITY_ANDROID
				touchCurrentPos = Input.GetTouch(0).position;
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
				touchCurrentPos = e.mousePosition;
#endif
				//Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
				if (!disableX)
				{
					if (invertX)
						xAngle = xAngTemp - (touchCurrentPos.x - touchStartPos.x) * 180.0f / Screen.width;
					else
						xAngle = xAngTemp + (touchCurrentPos.x - touchStartPos.x) * 180.0f / Screen.width;
				}

                xAngle = Mathf.Clamp(xAngle, minRX, maxRX);

                if (!disableY)
				{
#if UNITY_ANDROID
					invertY = true;
#endif
                    //Debug.Log("yyyyyyy");
					if (invertY)
						yAngle = yAngTemp + (touchCurrentPos.y - touchStartPos.y) * 90.0f / Screen.height;
					else
						yAngle = yAngTemp - (touchCurrentPos.y - touchStartPos.y) * 90.0f / Screen.height;
				}

				yAngle = Mathf.Clamp(yAngle, minRY, maxRY);
				//Rotate camera
				transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
			}
		}

		
	}
   
}
