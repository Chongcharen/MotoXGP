using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelUppdate : MonoBehaviour
{
    [SerializeField]WheelCollider wheelCollider;
    public float torque = 200;
    void Start(){
       
    }

    void Go(float accel){
        Debug.Log("Acceel "+accel);
        accel = Mathf.Clamp(accel,-1,1);
        float thrustTorque = accel * torque;
        wheelCollider.motorTorque = thrustTorque;

       // wheelCollider.steerAngle = 3;
    }
    void Update(){
        if(Input.GetKey(KeyCode.K)){
            Go(1);
        }
    }
}
