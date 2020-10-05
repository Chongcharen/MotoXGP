using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class DetectBase : MonoBehaviour
{
    public bool isSelectBase = false;
    public float speed = 5;
    public float dragingSpeed = 2;
    public GameObject baseRotate;
    Rigidbody baseRigidbody;
    float mouseCursorSpeed;
    Vector3 downPoint,upPoint;

    float startDrag,stopDrag;
    float distanceDrag;
    float h;
    int directionBase = 0;


    //ปัด จับเวลา ถ้าหากเวลาสั้น แล้ว ระยะยาว ให้เป็น swipe เปลี่ยนตัวละคร
    //จับเวลาถ้าหากเวลาสั้นทั้งๆที่ฐานยังปัดให้เดาว่าเปลี่ยนตัวละคร
    [SerializeField]Collider baseCollider;
    public bool GetIsSelectBase() {
        return isSelectBase;
    }
    enum MoveState
    {
        idle,
        rotateL,
        rotateR
    }
    public void ClearIsSelectBase()
    {
        isSelectBase = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        baseRigidbody = baseRotate.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectBaseEditor();
        mouseCursorSpeed = Input.GetAxis("Mouse X") / Time.deltaTime;
       // Debug.Log("mouseCursorSpeed "+mouseCursorSpeed);
    }
    void FixedUpdate(){
        if(isSelectBase){
            baseRigidbody.angularVelocity = Vector3.zero;
            h = Mathf.Abs(mouseCursorSpeed*dragingSpeed) * -Input.GetAxis("Mouse X")*Time.fixedDeltaTime;
            float v = mouseCursorSpeed * Input.GetAxis("Mouse Y")*Time.fixedDeltaTime*dragingSpeed;
            Debug.Log("H "+h);
            baseRotate.transform.Rotate(0, h, 0);
        }else{
           // baseRigidbody.AddTorque(Vector3.down*-h*dragingSpeed,ForceMode.Acceleration);
        }
    }


    void SelectBaseEditor(){
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (baseCollider.Raycast(ray, out hit, 100.0f))
            {
                isSelectBase = true;
                Debug.Log("isSelectBase "+isSelectBase);
                downPoint = Input.mousePosition;
                startDrag = Time.time;
            }
        }else if(Input.GetMouseButtonUp(0)){
            isSelectBase = false;
            upPoint = Input.mousePosition;
            stopDrag = Time.time;
            distanceDrag = downPoint.x - upPoint.x;
            var timeDrag = stopDrag - startDrag;
            if(timeDrag < 0.25f && Mathf.Abs(distanceDrag) > 200){
                directionBase += distanceDrag > 0 ? 180 : -180;
                Debug.Log("directionBase "+directionBase);
                baseRotate.transform.DORotate(new Vector3(0,directionBase,0),1);
                directionBase = directionBase%360;
            }
        }

        // if(isSelectBase){
        //     float h = mouseCursorSpeed * Input.GetAxis("Mouse X");
        //     float v = mouseCursorSpeed * Input.GetAxis("Mouse Y");
        //     baseRotate.transform.Rotate(0, h, 0);
        // }
        
    }
}
