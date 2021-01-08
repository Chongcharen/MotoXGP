using System.Linq.Expressions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
public class PitchZoom : MonoBehaviour
{
    public Vector2 rangeOffsetX = new Vector2(-0.2f,0.2f);
    public Vector2 rangeOffsetY = new Vector2(-0.2f,0.2f);
    public Vector2 rangeOffsetZ = new Vector2(-0.2f,0.2f);
    
    Vector3 temp;
    [Range(-0.2f,0.2f)]public float pitchOffsetX = 0;
    [Range(-0.2f,0.2f)]public float pitchOffsetY = 0;
    [Range(-0.2f,0.2f)]public float pitchOffsetZ = 0;
    [SerializeField]DetectBase detectBase;
    [SerializeField]CinemachineVirtualCamera virtualCamera;
    CinemachineTransposer CinemachineTransposer;
    CinemachineComposer CinemachineComposer;

    public float mouseXSpeed,mouseYSpeed;
    bool isDragging = false;
    Vector2 startPosition,endPosition;
    private Vector2 lastestMousePos;
    float startSwipe,swipeTime;
    float maxSwipeTime = 10;

    public float dragSpeed = 0.1f;
    public float zoomSpeed = 0.1f;

    Vector2 fakeTouchZero;
    float previousDistance,currentDistance;
    void Start()
    {
        detectBase = GetComponent<DetectBase>();
        CinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        CinemachineComposer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
        temp = CinemachineTransposer.m_FollowOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if(detectBase.GetIsSelectBase())return;
        #if UNITY_EDITOR    
            // if(Input.GetMouseButton(0)){
            //     UpdateZoomWithEditor();
            // }else{
            //     previousDistance = 0;
            // }
            Swipe();
        #else
            if(Input.touchCount >1){
                UpdateZoom();
            }else
            {
                previousDistance = 0;
                Swipe();
            }
        #endif
    }
    
    void Swipe(){
        if(Input.GetMouseButton(0)){
            if(!isDragging){
                startPosition = Input.mousePosition;
                startSwipe = Time.time;
            }else
            {
                TestDrag();
                UpdatePitch();
            }
            isDragging = true;
            
            
        }else if(Input.GetMouseButtonUp(0)){
            isDragging = false;
            mouseXSpeed = 0;
            mouseYSpeed = 0;
            endPosition = Input.mousePosition;
            swipeTime = Time.time - startSwipe;
            // if(swipeTime < maxSwipeTime){
            //     Debug.Log("swipeTime "+swipeTime);
            //     Debug.Log("This is swipe");
            // }
        }
    }
    void UpdateZoom(){
        var touchZero = Input.GetTouch(0).position;
        var touchOne = Input.GetTouch(1).position;
        currentDistance = (touchZero - touchOne).magnitude;
        var differentDistance = (currentDistance - previousDistance)*Time.fixedDeltaTime*zoomSpeed;
        if(previousDistance != 0)
            CinemachineTransposer.m_FollowOffset.z = Mathf.Clamp(CinemachineTransposer.m_FollowOffset.z-differentDistance,temp.z+rangeOffsetZ.x,temp.z+rangeOffsetZ.y);
        previousDistance = currentDistance;
    }
    void UpdateZoomWithEditor(){
        fakeTouchZero = new Vector2(Screen.width*0.5f,Screen.height*0.5f);
        var touchOne = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        currentDistance = (fakeTouchZero - touchOne).magnitude;
        var differentDistance = (currentDistance - previousDistance)*Time.fixedDeltaTime*zoomSpeed;
        if(previousDistance != 0)
            CinemachineTransposer.m_FollowOffset.z = Mathf.Clamp(CinemachineTransposer.m_FollowOffset.z+differentDistance,temp.z+rangeOffsetZ.x,temp.z+rangeOffsetZ.y);
        previousDistance = currentDistance;
    }
    void TestDrag(){
        var currentPosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        float deltaMagnitudeDiff = (startPosition - currentPosition).magnitude;
        
    }
    void UpdatePitch(){
        
        mouseXSpeed = Input.GetAxis("Mouse X") * Time.fixedDeltaTime*dragSpeed;
        mouseYSpeed = Input.GetAxis("Mouse Y") * Time.fixedDeltaTime*dragSpeed;
        CinemachineTransposer.m_FollowOffset.x = Mathf.Clamp(CinemachineTransposer.m_FollowOffset.x+mouseXSpeed,temp.x+rangeOffsetX.x,temp.x+rangeOffsetX.y);
        CinemachineTransposer.m_FollowOffset.y = Mathf.Clamp(CinemachineTransposer.m_FollowOffset.y+mouseYSpeed,temp.y+rangeOffsetY.x,temp.y+rangeOffsetY.y);
        //CinemachineTransposer.m_FollowOffset.z = Mathf.Clamp(pitchOffsetZ,temp.x-rangeOffsetZ.x,temp.x+rangeOffsetZ.y);
    }
}
