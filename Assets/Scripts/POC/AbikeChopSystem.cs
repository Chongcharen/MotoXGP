﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class AbikeChopSystem : MonoBehaviour
{
    public static Subject<int> OnBoostChanged = new Subject<int>();
    public static Subject<float> OnBoostTime = new Subject<float>();
    public static Subject<bool> OnGrouned = new Subject<bool>();
    public static Subject<Unit> OnReset = new Subject<Unit>();  

    Transform currentSpawn;    
    [SerializeField]TextMeshProUGUI playerName_txt;
    [SerializeField]GameObject testUp,testDown;
    [SerializeField]Canvas canvas;
    public Transform targetForCameraLook;
    [Header("Controller")]
    [SerializeField]GameController motorControl;
    [SerializeField]Animator animator;
    
    public float torque = 200;
    public float airDrag = 0.1f;
    public float massBodyIncrease = 0.05f;
    float accel;
    public float forceJump = 150;
    public float bikeRoatePower = 5;
    public bool brake;
    public bool jump;
    public bool isLeft = false;
    public bool isRight = false;
    public bool backWard = false;
    public bool grounded = true;
    public bool crash = false;
    bool[] isGround = new bool[2]{true,true};
    public bool isControll = false;
    [Header ("Speed Setting")]
    public float speed;
    public float speedLimit = 60;
    float axisX;
    public float direction = 0;
    [SerializeField] AnimationCurve motorTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));
     [SerializeField] Transform centerOfMass;
    [Header("Boost")]
    [SerializeField]Transform explosionTransform;
    [SerializeField]float explosionPower;
    [SerializeField]float explosionRadius;
    [SerializeField] float boostForce = 5000;
    [SerializeField] int boostLimit = 3;
    BoostSystem boostSystem;
    
    [SerializeField] float boostTimeLimit = 5;
    float currentBoostTime = 0;
    bool isBoosting = false;
    public float BoostForce { get { return boostForce; } set { boostForce = value; } }
    [SerializeField]int airBrake = 200;
    [Range(0.5f, 10f)]
    [SerializeField] float downforce = 1.0f;
    public float Downforce { get{ return downforce; } set{ downforce = Mathf.Clamp(value, 0, 5); } }     
    [Range(2, 16)]
    [SerializeField] float diffGearing = 4.0f;
    [Header("Wheels Setting")]
    [SerializeField]ConnectWheel connectWheel;
     WheelComponent[] wheels;

    [Header("Bike Setting")]
    public BikeWheels bikeWheels;
    [SerializeField]BikeSetting bikeSetting;
    [Header("Body Setting")]
    [SerializeField]Rigidbody myRigidbody;
    [SerializeField]Collider bodyCollider;
    [SerializeField]GameObject objectDetecter;

    //Observable

      
    
    //resetPosition and reStartPosition;
    public Vector3 startPosition;
    public Vector3 bikerStartPosition;
    //respawn by zone
    public Vector3 respawnPosition;
    bool isDeadzone = false;
    void Awake(){
        myRigidbody = GetComponent<Rigidbody>();
        wheels = new WheelComponent[2];
        wheels[0] = SetWheelComponent(connectWheel.wheelFront,connectWheel.AxleFront,false,0,connectWheel.AxleFront.localPosition.y);
        wheels[1] = SetWheelComponent(connectWheel.wheelBack,connectWheel.AxleBack,true,0,connectWheel.AxleBack.localPosition.y);
        startPosition = transform.position;
        respawnPosition = startPosition;
        bikerStartPosition = bikeSetting.bikerMan.transform.localPosition;
        GameHUD.OnRestartPosition.Subscribe(_=>{
            RestartPosition();
        }).AddTo(this);
        GameHUD.OnResetPosition.Subscribe(_=>{
            ResetPosition();
        }).AddTo(this);
        PlayerTrigger.OnRespawnPosition.Subscribe(pos =>{
            RestartPosition(pos);
        });
        
        canvas.worldCamera = Camera.main;
        if(!isControll){
            RemoveCrashDetecter();
        }else
        {
            // CrashDetecter.OnCrash.Subscribe(_=>{
            //     crash = true;
            //     OnCrash();
            // }).AddTo(this);
        }
        boostSystem = GetComponent<BoostSystem>();
    }

    public void SetController(bool _isActive){
        isControll = _isActive;
        objectDetecter.SetActive(_isActive);
        if(_isActive){
            CrashDetecter.OnCrash.Subscribe(crashPosition=>{
                crash = true;
                respawnPosition = new Vector3(crashPosition.x,respawnPosition.y+2,startPosition.z);
                OnCrash();
            }).AddTo(this);
            GameplayManager.OnGameEnd.Subscribe(_=>{
                isControll = false;
                ForceBrake();
                print(Depug.Log("GameEnd !!!!!!!",Color.green));
            });
        }
    }

    void OnCrash(){
        animator.enabled = false;
        objectDetecter.gameObject.SetActive(false);
        StartCoroutine(DelayRespawn());
    }
    IEnumerator DelayRespawn(){
        yield return new WaitForSeconds(2);
        RestartPosition();
    }
    void RestartPosition(){
        Debug.Log("restartPosition");
        if(!isControll)return;
        if(MapManager.Instance.isDeadzone)
            respawnPosition = new Vector3(MapManager.Instance.respawnPosition.x,MapManager.Instance.respawnPosition.y,startPosition.z);
        StopAllCoroutines();
        transform.position = respawnPosition;
        transform.rotation = Quaternion.Euler(0,90,0);
        animator.enabled = true;
        animator.gameObject.transform.localPosition = bikerStartPosition;
        GetComponent<CenterOfMass>().Reset();
        crash = false;
        OnReset.OnNext(default);
        objectDetecter.gameObject.SetActive(true);
        StopMotor();
    }
    void RestartPosition(Vector3 newPosition){
        
        if(!isControll)return;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(0,90,0);
         StopMotor();
    }
    void ResetPosition(){
        if(!isControll)return;
        transform.position = new Vector3(transform.position.x,transform.position.y+5,transform.position.z);
        transform.rotation = Quaternion.Euler(0,90,0);
        StopMotor();
    }
    void StopMotor(){
        
         foreach(WheelComponent component in wheels){
             component.collider.motorTorque = 0f;
             component.collider.brakeTorque = 0;
         }
        //  myRigidbody.isKinematic = true;
        //         myRigidbody.isKinematic = false;
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = Vector3.zero;
        
    }
    void Start()
    {
        //  if (myRigidbody != null && centerOfMass != null)
        //     {
        //         myRigidbody.centerOfMass = centerOfMass.localPosition;
        //     }
        if(playerName_txt != null)
            playerName_txt.text = GetComponent<PhotonView>().Controller.NickName;
    }

    // Update is called once per frame
    void ForceBrake(){
         foreach(WheelComponent component in wheels){
             if(component.collider == null)continue;
             component.collider.brakeTorque = 100000000;
             component.collider.motorTorque = 0f;
         }
    }
    void Brake(bool brake){
        if(brake){
            wheels[0].collider.brakeTorque = 100000000;
            wheels[1].collider.brakeTorque = 100000000;
            wheels[0].collider.motorTorque = 0;
        }else{
            wheels[0].collider.brakeTorque = 0;
        }
    }
    void ResetEngine(){
        foreach(WheelComponent component in wheels){
             component.collider.brakeTorque = 0;
             component.collider.motorTorque = 0;
         }
         wheels[0].collider.enabled = false;
         wheels[1].collider.enabled = false;
         
    }
    
    void FixedUpdate(){
        speed = transform.InverseTransformDirection(myRigidbody.velocity).z * 3.6f;
        if(!isControll)return;
        if(isControll && !crash){
            accel = motorControl.accelerator;
            brake = motorControl.brake;
            jump = motorControl.isJump;
            isLeft = motorControl.isLeft;
            isRight = motorControl.isRight;
            if(motorControl.isBoost&& boostLimit >0 && !isBoosting){
                isBoosting = true; 
                boostLimit -- ;    
                OnBoostChanged.OnNext(boostLimit);
                OnBoostTime.OnNext(boostTimeLimit);
                if(boostSystem != null)
                    boostSystem.StartBoostEffect(boostTimeLimit);
                myRigidbody.AddExplosionForce(explosionPower,explosionTransform.position,explosionRadius,1,ForceMode.Impulse);
                //myRigidbody.AddForce(transform.forward*100000);
            }
        }
        if(isControll&&crash){
            ForceBrake();
            return;
        }
        var indexWhell = 0;
        if(jump && grounded){
            myRigidbody.AddForce(transform.up* myRigidbody.mass*forceJump);
        }
        if(isBoosting){
            if(currentBoostTime < boostTimeLimit){
                myRigidbody.AddForce(transform.forward*BoostForce);
                currentBoostTime += Time.deltaTime*1;
            }else
            {
                myRigidbody.AddForce(-transform.forward*(BoostForce*100));
                wheels[1].collider.brakeTorque = 10000;
                isBoosting = false;
                currentBoostTime = 0;
                wheels[1].collider.brakeTorque = 0;
            }
        }
         
        foreach(WheelComponent component in wheels){
            WheelHit hit;
            if(speed > speedLimit && !isBoosting){
                speed = speedLimit;
            }
            if(component.drive && grounded&&!brake){
                if(Mathf.Abs(speed) < 4 || Mathf.Sign(speed) == Mathf.Sign(accel)){
                    component.collider.motorTorque = accel * motorTorque.Evaluate(speed) * diffGearing / 1;
                   // Debug.Log("Evaluate "+motorTorque.Evaluate(speed));
                    //Debug.Log("component.collider.motorTorque "+component.collider.motorTorque);
                  // component.sphereCollider.attachedRigidbody.AddForce(transform.forward * boostForce);
                    //myRigidbody.AddForce(transform.forward * boostForce);
                    //component.sphereCollider.GetComponent<Rigidbody>().AddTorque(Vector3.forward* accel * motorTorque.Evaluate(speed) * diffGearing / 1);
                }else{
                    component.collider.brakeTorque = Mathf.Abs(accel) * bikeSetting.brakePower;
                }
            }
            if(component.drive && accel == 0){
                 component.collider.brakeTorque = 100;
            }
            // if(!component.drive){
            //     if(brake)
            //         component.collider.brakeTorque = 100000000;
            //     else
            //         component.collider.brakeTorque = 0.00001f;
            // }
            // if(!isGround[indexWhell]){
            //     if(brake){
            //         component.collider.brakeTorque = bikeSetting.brakePower;
            //     }else
            //     {
            //         component.collider.brakeTorque = airBrake;
            //     }
                
            // }else
            // {
            //     component.collider.brakeTorque = 0;
            // }

            if(brake){
                if(!component.drive)
                    component.collider.brakeTorque = bikeSetting.brakePower;
                else
                    component.collider.brakeTorque = bikeSetting.brakePower;
            }else
            {
                if(!isGround[indexWhell])
                    component.collider.brakeTorque = airBrake;
                else
                    component.collider.brakeTorque = 0;
            }
            Quaternion quaternion;
            Vector3 position;
            component.collider.GetWorldPose(out position,out quaternion);
            component.rotation = Mathf.Repeat(component.rotation + Time.deltaTime * component.collider.rpm * 360.0f / 60.0f, 360.0f);
            component.wheel.localRotation = Quaternion.Euler(component.rotation,0,0);
            Vector3 lp = component.axle.localPosition;
            if(component.collider.GetGroundHit(out hit)){
                isGround[indexWhell] = true;
                lp.y -= Vector3.Dot(component.wheel.position - hit.point, transform.TransformDirection(0, 1, 0)) - (component.collider.radius);
                lp.y = Mathf.Clamp(lp.y,component.startPos.y - bikeWheels.setting.SuspensionDistance, component.startPos.y +  bikeWheels.setting.SuspensionDistance);
            }else{
                isGround[indexWhell] = false;
                
            }
            // if(component.drive ){
            //     if(!isGround[indexWhell])
            //         component.collider.brakeTorque = airBrake;
            //     else
            //         component.collider.brakeTorque = 0;
            // }
           

            component.axle.localPosition = lp;
            lp.y -= Vector3.Dot(component.wheel.position - hit.point, transform.TransformDirection(0, 1, 0)) - (component.collider.radius);
            lp.y = Mathf.Clamp(lp.y, component.startPos.y - bikeWheels.setting.SuspensionDistance, component.startPos.y + bikeWheels.setting.SuspensionDistance);
            indexWhell++;
            
        }

        //old
        // if((isGround[0] == false && isGround[1] == false)){
        //     if(grounded && isControll)
        //         OnGrouned.OnNext(false);
        //     grounded = false;
        //     OnGrouned.OnNext(grounded);
        // }
        // else if((isGround[0] == true && isGround[1] == true)){
        //     if(!grounded && isControll)
        //         OnGrouned.OnNext(true);
        //     grounded = true;
        // }
        // else if((isGround[0] == false && isGround[1] == true)){
        //     OnGrouned.OnNext(false);
        //     grounded = true;
        // }
        // else if((isGround[0] == true && isGround[1] == false)){
        //     OnGrouned.OnNext(true);
        //     grounded = true;
        // }

        //OnGrouned.OnNext(isGround[1]);
        
        
        // if(isGround[0] && isGround[1])
        //     OnGrouned.OnNext(true);
        // else if(!isGround[0] && !isGround[1])
        //     OnGrouned.OnNext(false);
       // grounded = isGround[0] && isGround[1];
        if(grounded != (isGround[0] && isGround[1])){
            grounded = !grounded;
            OnGrouned.OnNext(grounded);
        }
        // if(isGround[0] == false && isGround[1] == false){
        //     grounded = false;
        //     if(grounded && isControll)
        //         OnGrouned.OnNext(grounded);
        //     OnGrouned.OnNext(grounded);
        // }else if((isGround[0] == true && isGround[1] == true)){
        //     grounded = true;
        //     if(!grounded && isControll)
        //         OnGrouned.OnNext(grounded);
        // }else if((isGround[0] == true && isGround[1] == false)){

        // }

        // if(isGround[0] == false){
        //     OnGrouned.OnNext(false);
        // }
        
        // if(isGround[0] == false && isGround[1] == false){
        //     grounded = false;
        // }else if((isGround[0] == true && isGround[1] == true)){
        //     grounded = true;
        // }
        //grounded = isGround[0] && isGround[1];
        //OnGrouned.OnNext(isGround[1]);

       // OnGrouned.OnNext(isGround[1]);


        /*if(grounded && accel > 0){
            myRigidbody.AddForce(transform.forward * boostForce);
        }
        nolonger to use
        */
        if(isLeft){
            direction = -1;
            if(!grounded){
                 myRigidbody.AddTorque(Vector3.forward*bikeRoatePower,ForceMode.Acceleration);
            }
            //myRigidbody.AddForce(-transform.forward* myRigidbody.mass*forceJump);
            //myRigidbody.AddForceAtPosition(testUp.transform.position,-Vector3.right*10);
        }
        if(isRight){
            direction = 1;
            if(!grounded){
                myRigidbody.AddTorque(-Vector3.forward*bikeRoatePower,ForceMode.Acceleration);
            }
        }
        if(!isLeft && !isRight){
            axisX = Mathf.Lerp(axisX,0.66f,Time.deltaTime*10);
        }else
        {
            axisX = Mathf.Clamp(axisX +Time.deltaTime*direction,0,1);
        }
        if(animator != null){
           // animator.SetFloat("AxisX",axisX);
           animator.SetBool("isLeft",isLeft);
           animator.SetBool("isRight",isRight);
           animator.SetFloat("speed",speed);

        }
        
        // Downforce
        myRigidbody.AddForce(-transform.forward * speed * downforce);
    }
    
    public void RemoveCrashDetecter(){
        objectDetecter.gameObject.SetActive(false);
    }
    public void RemoveEngine(){
        myRigidbody.Sleep();
        GetComponent<CenterOfMass>().enabled = false;
    }
    #region Setting
    private WheelComponent SetWheelComponent(Transform wheel, Transform axle, bool drive, float maxSteer, float pos_y)
    {

        WheelComponent result = new WheelComponent();
        GameObject wheelCol = new GameObject(wheel.name + "WheelCollider");


        
        wheelCol.transform.parent = transform;
        wheelCol.transform.position = wheel.position;
        wheelCol.transform.eulerAngles = transform.eulerAngles;
        pos_y = wheelCol.transform.localPosition.y;


       
        wheelCol.AddComponent(typeof(WheelCollider));

        
        result.drive = drive;
        result.wheel = wheel;
        result.axle = axle;
        result.collider = wheelCol.GetComponent<WheelCollider>();
        result.collider.mass = bikeWheels.setting.Weight;
        result.collider.radius = bikeWheels.setting.Radius;
        result.collider.center = bikeWheels.setting.WheelCenter;
        result.collider.suspensionDistance = bikeWheels.setting.SuspensionDistance;
        result.collider.forceAppPointDistance = bikeWheels.setting.ForceAppointDistance;
        result.collider.wheelDampingRate = bikeWheels.setting.SuspensionDistance;
        result.pos_y = pos_y;
        result.maxSteer = maxSteer;
        result.startPos = axle.transform.localPosition;
        JointSpring spring = new JointSpring();
        spring.spring = bikeWheels.setting.SuspensionSpring.spring;
        spring.damper = bikeWheels.setting.SuspensionSpring.damper;
        spring.targetPosition = bikeWheels.setting.SuspensionSpring.targetposition;
        result.collider.suspensionSpring = spring;    
        WheelFrictionCurve frictionCurve = new WheelFrictionCurve();
        frictionCurve.extremumSlip = bikeWheels.setting.ForwardFriction.extremumSlip;
        frictionCurve.extremumValue = bikeWheels.setting.ForwardFriction.extremumValue;
        frictionCurve.asymptoteSlip = bikeWheels.setting.ForwardFriction.asymptoteSlip;
        frictionCurve.asymptoteValue = bikeWheels.setting.ForwardFriction.asymptoteValue;
        frictionCurve.stiffness = bikeWheels.setting.ForwardFriction.stiffness;
        result.collider.forwardFriction = frictionCurve;

        WheelFrictionCurve sidewayFriction = new WheelFrictionCurve();
        sidewayFriction.extremumSlip = bikeWheels.setting.SidewaysFriction.extremumSlip;
        sidewayFriction.extremumValue = bikeWheels.setting.SidewaysFriction.extremumValue;
        sidewayFriction.asymptoteSlip = bikeWheels.setting.SidewaysFriction.asymptoteSlip;
        sidewayFriction.asymptoteValue = bikeWheels.setting.SidewaysFriction.asymptoteValue;
        sidewayFriction.stiffness = bikeWheels.setting.SidewaysFriction.stiffness;
        result.collider.sidewaysFriction = frictionCurve;

        //result.sphereCollider = wheel.gameObject.AddComponent<SphereCollider>();
        if(!drive){
            result.collider.suspensionDistance = 0.23f;
        }

        return result;

    }
    #endregion
    Quaternion RigidbodyRotation;
    Quaternion RigidbodyRotationTensor;
    Quaternion transformRotation;
    void OnGUI(){
        GUIStyle style = new GUIStyle();
        style.fontSize = 50;
       // GUI.Label(new Rect(10, 10, 300, 150), "Speed  "+speed,style);
       // GUI.Label(new Rect(10, 70, 300, 150), "Torque  "+motorTorque.Evaluate(speed),style);
        //   GUI.Label(new Rect(10, 25, 300, 150), "RigidbodyRotation "+RigidbodyRotation,style);
        //   GUI.Label(new Rect(10, 50, 300, 150), "RigidbodyRotationTensor "+RigidbodyRotationTensor,style);
    }
}
