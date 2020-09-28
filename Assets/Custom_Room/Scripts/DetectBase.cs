using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectBase : MonoBehaviour
{
    public bool isSelectBase = false;
    public float speed = 5;
    public float dragingSpeed = 2;
    public GameObject baseRotate;
    Rigidbody baseRigidbody;
    float mouseCursorSpeed;
    Vector3 downPoint,upPoint;
    float h;
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
            h = Mathf.Abs(mouseCursorSpeed*speed) * -Input.GetAxis("Mouse X")*Time.fixedDeltaTime;
            float v = mouseCursorSpeed * Input.GetAxis("Mouse Y")*Time.fixedDeltaTime;
            baseRotate.transform.Rotate(0, h, 0);
            
        }else{
            baseRigidbody.AddTorque(Vector3.down*-h*dragingSpeed,ForceMode.Acceleration);
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
                downPoint = Input.mousePosition;
            }
        }else if(Input.GetMouseButtonUp(0)){
             isSelectBase = false;
             upPoint = Input.mousePosition;
        }

        // if(isSelectBase){
        //     float h = mouseCursorSpeed * Input.GetAxis("Mouse X");
        //     float v = mouseCursorSpeed * Input.GetAxis("Mouse Y");
        //     baseRotate.transform.Rotate(0, h, 0);
        // }
        
    }
}
