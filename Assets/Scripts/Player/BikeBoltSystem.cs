using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using TMPro;
using UniRx;
[RequireComponent(typeof(Rigidbody))]
public class BikeBoltSystem : EntityEventListener<IPlayerBikeState>
{
    public static Subject<bool> OnControllGained = new Subject<bool>();
    #region Bike Property
    [Header("Body Setting")]
    
    [SerializeField]Collider bodyCollider;
    [SerializeField]GameObject objectDetecter;
    Rigidbody Rigidbody;
    Transform currentSpawn;

    [Header("Bike Setting")]
    [SerializeField]BilkWheelSetting bikeWheelSetting;
    [SerializeField]BikeSetting bikeSetting;
    WheelComponent[] wheels;
    [Header ("Speed Setting")]
    [SerializeField] AnimationCurve motorTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));
    [SerializeField] AnimationCurve boostTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));
    float speed;
    public float currentSpeedLimit =0;
    public float speedLimit = 60;
    
    public float boostSpeedLimit = 120;
    
    [Header("Boost Setting")]
    [SerializeField] float boostForce = 200;
    [SerializeField] float brakeForce = 5000;
    [SerializeField] float boostTimeLimit = 5;
    [SerializeField] float boostDelay =2;
    BoostSystem boostSystem;
    bool isBoosting = false;
    bool isBoostDelay = false;
    int boostLimit = 1000;
    public int BoostLimit{
        get{
            return boostLimit;
        }
        set{
            boostLimit = value;
            //OnBoostChanged.OnNext(boostLimit);
        }
    }
    
    public float BoostForce { get { return boostForce; } set { boostForce = value; } }
    public float BreakForce { get { return boostForce; } set { boostForce = value; } }
    [Header("Lower Gear")]
    [SerializeField]int lower_gear_force = 200;
    [SerializeField]int airBrake = 200;
    [Range(0.5f, 10f)]
    [SerializeField] float downforce = 1.0f;
    public float Downforce { get{ return downforce; } set{ downforce = Mathf.Clamp(value, 0, 5); } }     
    [Range(2, 16)]
    [SerializeField] float diffGearing = 4.0f;

    [Header("ExplodeSetting")]
    public Vector3 positionExplode;
    public float explosionForce;
    public float explosionRadius;
    public float explosionUpward;
    public float explosionDuration = 1;
    public ForceMode explosionMode;
    public Vector2 bodyRotationRageX;
    public Vector2 bodyRotationRageY;
    public Vector2 bodyRotationRageZ;
    [Header("Controller")]
    [SerializeField]GameController motorControl;
    [SerializeField]Animator animator;
    [SerializeField]RagdollCollider ragdollCollider;
    [SerializeField]GameObject ragdollObject;
    public float airDrag = 0.1f;
    float accel;
    public float forceJump = 150;
    public float bikeRoatePower = 5;
    public bool brake = false;
    public bool jump = false;
    public bool isLeft = false;
    public bool isRight = false;
    public bool backWard = false;
    public bool grounded = true;
    public bool crash = false;
    bool[] isGround = new bool[2]{true,true};
    public bool isControll = false;
    public bool isReady = false;
    [Header("Canvas")]
    [SerializeField]Canvas canvas;
    [SerializeField]TextMeshProUGUI playerName_txt;
    #endregion
    void Awake(){
        Rigidbody = GetComponent<Rigidbody>();
        wheels = new WheelComponent[2];
        wheels[0] = SetWheelComponent(bikeWheelSetting.wheels.wheelFront,bikeWheelSetting.wheels.AxleFront,false,0,bikeWheelSetting.wheels.AxleFront.localPosition.y,bikeWheelSetting.wheelSettings[0]);
        wheels[1] = SetWheelComponent(bikeWheelSetting.wheels.wheelBack,bikeWheelSetting.wheels.AxleBack,true,0,bikeWheelSetting.wheels.AxleBack.localPosition.y,bikeWheelSetting.wheelSettings[1]);
        GameCallback.OnGameReady.Subscribe(raceCountdown =>{
            // if(BoltNetwork.IsClient){
            //     GetComponent<Rigidbody>().isKinematic = true;
            // }
        }).AddTo(this);
    }
    private WheelComponent SetWheelComponent(Transform wheel, Transform axle, bool drive, float maxSteer, float pos_y,WheelSetting wheelSetting)
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
        result.collider.mass = wheelSetting.Weight;
        result.collider.radius = wheelSetting.Radius;
        result.collider.center = wheelSetting.WheelCenter;
        result.collider.suspensionDistance = wheelSetting.SuspensionDistance;
        result.collider.forceAppPointDistance = wheelSetting.ForceAppointDistance;
        result.collider.wheelDampingRate = wheelSetting.DampingRate;
        result.pos_y = pos_y;
        result.maxSteer = maxSteer;
        result.startPos = axle.transform.localPosition;
        JointSpring spring = new JointSpring();
        spring.spring = wheelSetting.SuspensionSpring.spring;//bikeWheels.setting.SuspensionSpring.spring;
        spring.damper = wheelSetting.SuspensionSpring.damper;//bikeWheels.setting.SuspensionSpring.damper;
        spring.targetPosition = wheelSetting.SuspensionSpring.targetposition;//bikeWheels.setting.SuspensionSpring.targetposition;
        result.collider.suspensionSpring = spring;    
        WheelFrictionCurve frictionCurve = new WheelFrictionCurve();
        frictionCurve.extremumSlip = wheelSetting.ForwardFriction.extremumSlip;//bikeWheels.setting.ForwardFriction.extremumSlip;
        frictionCurve.extremumValue = wheelSetting.ForwardFriction.extremumValue;//bikeWheels.setting.ForwardFriction.extremumValue;
        frictionCurve.asymptoteSlip = wheelSetting.ForwardFriction.asymptoteSlip;//bikeWheels.setting.ForwardFriction.asymptoteSlip;
        frictionCurve.asymptoteValue = wheelSetting.ForwardFriction.asymptoteValue;//bikeWheels.setting.ForwardFriction.asymptoteValue;
        frictionCurve.stiffness = wheelSetting.ForwardFriction.stiffness;//bikeWheels.setting.ForwardFriction.stiffness;
        result.collider.forwardFriction = frictionCurve;

        WheelFrictionCurve sidewayFriction = new WheelFrictionCurve();
        sidewayFriction.extremumSlip = wheelSetting.SidewaysFriction.extremumSlip;//bikeWheels.setting.SidewaysFriction.extremumSlip;
        sidewayFriction.extremumValue = wheelSetting.SidewaysFriction.extremumValue;//bikeWheels.setting.SidewaysFriction.extremumValue;
        sidewayFriction.asymptoteSlip = wheelSetting.SidewaysFriction.asymptoteSlip;//bikeWheels.setting.SidewaysFriction.asymptoteSlip;
        sidewayFriction.asymptoteValue = wheelSetting.SidewaysFriction.asymptoteValue;//bikeWheels.setting.SidewaysFriction.asymptoteValue;
        sidewayFriction.stiffness = wheelSetting.SidewaysFriction.stiffness;//bikeWheels.setting.SidewaysFriction.stiffness;
        result.collider.sidewaysFriction = frictionCurve;

        //result.sphereCollider = wheel.gameObject.AddComponent<SphereCollider>();

        return result;

    }
    public override void Attached(){
        state.SetTransforms(state.Transform, transform);
        

       // state.AddCallback("Rotation",()=>transform.rotation = state.Rotation);
    }
    public override void Detached(){

    }
    public override void ControlGained(){
        isControll = true;
        GetComponent<Rigidbody>().isKinematic = false;
        ragdollObject.gameObject.SetActive(true);
        OnControllGained.OnNext(true);
        VirtualPlayerCamera.Instantiate();
        VirtualPlayerCamera.instance.FollowTarget(transform);
        VirtualPlayerCamera.instance.LookupTarget(transform);
        AddControlEventListener();
    }
    void AddControlEventListener(){
        GameCallback.OnGameReady.Subscribe(raceCountdown =>{
            isReady = raceCountdown.RaceStart;
        }).AddTo(this);
    }
    void Update(){
        PollKey();
        //UpdateWheel();
    }
    void PollKey(){
        if(!isControll || !isReady)return;
        accel = motorControl.accelerator;
        brake = motorControl.brake;
        jump = motorControl.isJump;
        isLeft = motorControl.isLeft;
        isRight = motorControl.isRight;
        UpdateWheel();
    }
    public override void SimulateController(){
        PollKey();
        IBikePlayerCommandInput input = BikePlayerCommand.Create();
        input.accel = (int)accel;
        input.brake = brake;
        input.jump = jump;
        input.left = isLeft;
        input.right = isRight;
        entity.QueueInput(input);
    }
    
    public override void ExecuteCommand(Command command, bool resetState){
        BikePlayerCommand cmd = (BikePlayerCommand)command;
       
        if(resetState){
            // print(Depug.Log("resetState  "+resetState,Color.white));
            // print(Depug.Log("cmd.Result.Position  "+cmd.Result.Position,Color.yellow));
            // print(Depug.Log("cmd.Result.Rotation  "+cmd.Result.Rotation,Color.yellow));
            // print(Depug.Log("cmd.Result.Velocity  "+cmd.Result.Velocity,Color.yellow));
            // print(Depug.Log("cmd.Result.accel  "+cmd.Input.accel,Color.yellow));
            //transform.position = cmd.Result.Position;
            //transform.rotation = cmd.Result.Rotation;
            //Rigidbody.velocity = cmd.Result.Velocity;
            // accel = cmd.Input.accel;
            // brake = cmd.Input.brake;
            // jump = cmd.Input.jump;
            // isLeft = cmd.Input.left;
            // isRight = cmd.Input.right;
            // UpdateWheel();
        }else{
            accel = cmd.Input.accel;
            brake = cmd.Input.brake;
            jump = cmd.Input.jump;
            isLeft = cmd.Input.left;
            isRight = cmd.Input.right;
            UpdateWheel();
            // cmd.Result.Position = transform.position;
            // cmd.Result.Rotation = transform.rotation;
            // cmd.Result.Velocity = Rigidbody.velocity;
        }
    }
    public override void MissingCommand(Command previous){
        // BikePlayerCommand cmd = (BikePlayerCommand)previous;
        // accel = cmd.Input.accel;
        //     brake = cmd.Input.brake;
        //     jump = cmd.Input.jump;
        //     isLeft = cmd.Input.left;
        //     isRight = cmd.Input.right;
        //     UpdateWheel();
        //     cmd.Result.Position = transform.position;
        //     cmd.Result.Rotation = transform.rotation;
        //     cmd.Result.Velocity = Rigidbody.velocity;
    }
    void UpdateWheel(){
        var indexWhell = 0;
        foreach(WheelComponent component in wheels){
            WheelHit hit;
            if(speed > currentSpeedLimit && !isBoosting){
                speed = currentSpeedLimit;
            }
            if(component.drive && grounded&&!brake){
                if(Mathf.Abs(speed) < 4 || Mathf.Sign(speed) == Mathf.Sign(accel)){
                    var torqueSpeed = isBoosting ? boostTorque.Evaluate(speed) : motorTorque.Evaluate(speed);
                    component.collider.motorTorque = accel *  motorTorque.Evaluate(speed) * diffGearing / 1;
                }else{
                    component.collider.brakeTorque = Mathf.Abs(accel) * bikeSetting.brakePower;
                }
            }
            if(component.drive && accel == 0){
               // ReleaseTorque();
            }


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
                lp.y = Mathf.Clamp(lp.y,component.startPos.y - bikeWheelSetting.wheelSettings[indexWhell].SuspensionDistance, component.startPos.y +  bikeWheelSetting.wheelSettings[indexWhell].SuspensionDistance);
            }else{
                isGround[indexWhell] = false;
            }

            component.axle.localPosition = lp;
            lp.y -= Vector3.Dot(component.wheel.position - hit.point, transform.TransformDirection(0, 1, 0)) - (component.collider.radius);
            lp.y = Mathf.Clamp(lp.y, component.startPos.y - bikeWheelSetting.wheelSettings[indexWhell].SuspensionDistance, component.startPos.y + bikeWheelSetting.wheelSettings[indexWhell].SuspensionDistance);
            indexWhell++;
            
        }
    }
}
