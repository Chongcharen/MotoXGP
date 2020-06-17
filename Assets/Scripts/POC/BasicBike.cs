using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class BasicBike : MonoBehaviour
{
    Rigidbody myRigidbody;
    [SerializeField]WheelCollider[] frontWheelColliders;
    [SerializeField]WheelCollider[] backWheelColliders;
    //[SerializeField]WheelCollider[] driveWheels;
    [SerializeField] AnimationCurve motorTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));
    GameController gameController;

    [SerializeField]Transform targetMass,signalMass;

    bool isControll = true;
    float accel;
    float speed;
    public float speedLimit = 60;
    bool isBrake;
    Vector3 startPosition;
    void Start(){
        startPosition = transform.position;
        myRigidbody = GetComponent<Rigidbody>();
        gameController = GetComponent<GameController>();
        myRigidbody.centerOfMass = targetMass.localPosition;
        GameHUD.OnRestartPosition.Subscribe(_=>{
            RestartPosition();
        }).AddTo(this);
    }
    void FixedUpdate(){
        speed = transform.InverseTransformDirection(myRigidbody.velocity).z * 3.6f;
        if(isControll){
            accel = gameController.accelerator;
            isBrake = gameController.brake;
        }
         if(speed > speedLimit){
                speed = speedLimit;
            }
        if(Mathf.Abs(speed) < 4 || Mathf.Sign(speed) == Mathf.Sign(accel)){
            foreach (var bWheel in backWheelColliders)
            {
                bWheel.motorTorque = accel * motorTorque.Evaluate(speed)*2;
            }
        }
        if(isBrake){
            foreach (var fWheel in frontWheelColliders)
            {
                fWheel.brakeTorque = 100000;
            }
            foreach (var bWheel in backWheelColliders)
            {
                bWheel.motorTorque = 0.000001f;
                bWheel.brakeTorque = 2000;
            }
        }else{
             foreach (var fWheel in frontWheelColliders)
            {
                fWheel.brakeTorque = 0;
            }
            foreach (var bWheel in backWheelColliders)
            {
                bWheel.brakeTorque = 0;
            }
        }
        signalMass.transform.localPosition = myRigidbody.centerOfMass;
    }
    void RestartPosition(){
        if(!isControll)return;
        transform.position = startPosition;
        transform.rotation = Quaternion.Euler(0,90,0);
    }
}
