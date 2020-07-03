using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAI : MonoBehaviour {

    public Motorcycle_Controller mcc;

    public Transform center;
    public Transform _up;
    public Transform _down;
    public Transform _front;
    public Transform _back;
    public Transform _frontUp;
    public Transform _frontDown;
    public Transform _backUp;
    public Transform _backDown;

    public float rangeDetect = 3f;
    public float rotWanted;
    public float speedRot = 5f;

    public LayerMask layerMask;

    private bool canBoost = true;

    private void Start()
    {
        mcc = GetComponentInParent<Motorcycle_Controller>();
        mcc.accelerate = true;
    }

    private void Update()
    {
        if (!mcc)
            return;

        //if (DetectRay(-transform.up))
        //    mcc.accelerate = true;

        var rotZ = mcc.transform.rotation.eulerAngles.z;
        if (rotZ < 12 && rotZ > 0 || rotZ < 360 && rotZ > 290)
        {
            mcc.left = true;
        }
        else
        {
            mcc.left = false;
        }
        RotateWant();


        //if (rotZ > 40 && rotZ < 150)
        //{
        //    mcc.right = true;
        //}
        //else
        //    mcc.right = false;

        
        if (mcc.rearWheel.velocity.x < 12f && canBoost)
            StartCoroutine(Force());
        //if (!mcc.onGround && rotZ > 40 && rotZ < 150)
        //    RotateWant();
    }
    private IEnumerator Force()
    {
        canBoost = false;
        //mcc.Force(mcc.forcebike);
        mcc.MakeLowForce();
        yield return new WaitForSeconds(1f);
        canBoost = true;
    }

    private bool DetectRay(Vector3 direct)
    {
        bool detected = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direct, out hit, rangeDetect, layerMask))
        {
            detected = true;
        }
        return detected;
    }


    private void RotateWant()
    {
        Debug.Log("Lerp");
        float angle = Mathf.LerpAngle(mcc.transform.rotation.eulerAngles.z, rotWanted, speedRot*Time.time);
        mcc.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fall")
        {
            mcc.Jump();
        }
    }
}
