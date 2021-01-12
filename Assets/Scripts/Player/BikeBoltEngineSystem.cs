using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using TMPro;
using UniRx;
using DG.Tweening;
using UdpKit.Platform.Photon;
using Bolt.Matchmaking;
using Newtonsoft.Json;
using System.Linq;

[RequireComponent(typeof(Rigidbody),typeof(BikeMiddleware))]
public class BikeBoltEngineSystem : EntityEventListener<IPlayerBikeState>
{
    public static Subject<BoltEntity> OnEntityAttached = new Subject<BoltEntity>();
    public static Subject<BoltEntity> OnEntityDetached = new Subject<BoltEntity>();
    public static Subject<int> OnBoostChanged = new Subject<int>();
    public static Subject<Unit> OnChangeScreenBoost = new Subject<Unit>();
    public static Subject<float> OnBoostTime = new Subject<float>();
    public static Subject<float> OnBoostDelay = new Subject<float>();
    public static Subject<bool> OnGrouned = new Subject<bool>();

    public static Subject<bool> OnPlayerCrash = new Subject<bool>();

    public static Subject<bool> OnControllGained = new Subject<bool>();
    public static Subject<int> OnShowSpeed = new Subject<int>();
    public static Subject<Unit> OnReset = new Subject<Unit>();
    #region Respawn
    public Vector3 startPosition;
    Vector3 respawnPosition;
    Vector3 bikerStartPosition;
    #endregion

    #region Bike Property
    [Header("Body Setting")]
    [SerializeField]Transform bike_body;
    [SerializeField]Transform bikerMan;
    [SerializeField]Collider bodyCollider;
    [SerializeField]GameObject objectDetecter;
    [SerializeField]Transform effect_root;
    Rigidbody Rigidbody;
    Transform currentSpawn;
    [Header("MiddleWare")]
    BikeMiddleware bikeMiddleWare;
    [Header("Connect Wheel")]
    public ConnectWheel connectWheel;
    WheelComponent[] wheels;
    #endregion
    [Header("Engine Data")]
    public BikeEngineData engine_system;
    float speed;
    public float currentSpeedLimit =0;
    [Header("Nos Engine Data")]
    public NosSystemData nos_system;
    [Header("Wheel System Data")]
    public WheelSystemData[] wheel_systems;
    [Header("Control System Data")]
    public ControlSystemData control_system;
    [Header("Explode System Data")]
    public ExplodeSystemData explode_system;
    [Header("Status System Data")]
    public StatusSystemData status_system;

    

    [Header("Boost Setting")]
    BoostSystem boostSystem;
    bool isBoosting = false;
    bool isBoostDelay = false;
    float currentBoostTime = 0;
    // public int BoostLimit{
    //     get{
    //         return boostLimit;
    //     }
    //     set{
    //         boostLimit = value;
    //         OnBoostChanged.OnNext(boostLimit);
    //     }
    // }
    
    // public float BoostForce { get { return boostForce; } set { boostForce = value; } }
    // public float BreakForce { get { return boostForce; } set { boostForce = value; } }
    //[Range(0.5f, 10f)]
    // [SerializeField] float downforce = 1.0f;
    // public float Downforce { get{ return downforce; } set{ downforce = Mathf.Clamp(value, 0, 5); } }     
    #region controller
    [Header("Controller")]
    [SerializeField]Animator animator;
    [SerializeField]GameController motorControl;
    [SerializeField]RagdollCollider ragdollCollider;
    [SerializeField]GameObject ragdollObject;
    float accel;
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
    #region Bike Custom
    BikeCustomize bikeCustomize;
    public int runningTrack;
    #endregion  
    ProtocolPlayerCustomize playerCustomize;
    void Awake(){
        bikeMiddleWare = GetComponent<BikeMiddleware>();
        Rigidbody = GetComponent<Rigidbody>();
        bikeCustomize = GetComponent<BikeCustomize>();
        // wheels = new WheelComponent[2];
        // wheels[0] = SetWheelComponent(connectWheel.wheelFront,
        //                                 connectWheel.modelWheelFront,
        //                                     connectWheel.AxleFront,false,0,
        //                                         connectWheel.AxleFront.localPosition.y,
        //                                             wheel_systems[0],null);
        // wheels[1] = SetWheelComponent(connectWheel.wheelBack,
        //                                 connectWheel.modelWheelBack,
        //                                     connectWheel.AxleBack,true,0,
        //                                         connectWheel.AxleBack.localPosition.y,
        //                                             wheel_systems[1],null);

    }
    
    
    void AddControlEventListener(){
        GameCallback.OnGameReady.Subscribe(raceCountdown =>{
            isReady = raceCountdown.RaceStart;
            GetComponent<PlayerGlowing>().CloseRimlight();
        }).AddTo(this);
        GameHUD.OnLowerGear.Subscribe(_=>{
                if(!grounded)return;
                if(!isReady)return;
                currentSpeedLimit = nos_system.boostSpeedLimit;
                if(boostSystem != null)
                    boostSystem.StartBoostEffect(nos_system.lower_gear_time_limit,false);
                Rigidbody.AddForce(transform.forward* Rigidbody.mass*nos_system.lower_gear_force,ForceMode.Impulse);
                DOTween.To(()=> currentSpeedLimit, x=> currentSpeedLimit = x, engine_system.speedLimit, nos_system.lower_gear_time_limit).SetEase(Ease.Linear).SetAutoKill();
                OnChangeScreenBoost.OnNext(default);
            }).AddTo(this);
       CrashDetecter.OnPlayerCrash.Subscribe(tuple =>{
            if(tuple.Item1 != gameObject.GetInstanceID())return;
            crash = true;
            respawnPosition = new Vector3(tuple.Item2.x,tuple.Item2.y+2.5f,startPosition.z);
            OnPlayerCrash.OnNext(crash);
            OnCrash();
        }).AddTo(this);
        CrashDetecter.OnBump.Subscribe(_=>{
                ExplodeBump();
            }).AddTo(this);
        GameplayManager.OnGameEnd.Subscribe(_=>{
            isControll = false;
            brake = true;
        }).AddTo(this);
        GameplayManager.OnPlayerFinishTime.Subscribe(time=>{
            var token = entity.AttachToken as PlayerProfileToken;
            token.playerBikeData.playerFinishTime = time;
            PlayerRaceFinishEvent finishEvent = PlayerRaceFinishEvent.Create(GlobalTargets.Everyone);
            finishEvent.Entity = entity;
            finishEvent.FinishTime = time.ToString();
            finishEvent.JsonToken = JsonConvert.SerializeObject(token);
            finishEvent.Send();
        }).AddTo(this);
    }
    void Update(){
        UpdateWheelRotation();
        CheckGround();
        SetPlayerAnimator();
        SetBikeAnimator();
        if(!isControll || !isReady)return;
        PollKey();
        BoostChecker();
        BoostUpdate();
        SetPlayerAnimator();
        UpdatePlayerRoll();
        CheckSpeed();
    }
   void CheckSpeed(){
        if(isControll)
            OnShowSpeed.OnNext((int)speed);
    }
    
    
    #region  CrashEvent
    void OnCrash(){
        Debug.Log("Bolt Crash !!!!!");
        animator.applyRootMotion = false;
        animator.enabled = false;
        Rigidbody.drag = 2;
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
        objectDetecter.gameObject.SetActive(false);
        StartCoroutine(DelayRespawn());
    }
    IEnumerator DelayRespawn(){
       yield return new WaitForSeconds(1);
        RestartPosition();
        yield return new WaitForSeconds(0.5f);
        StopAllCoroutines();
        bikerMan.localPosition = new Vector3(0,bikerStartPosition.y,0);
    }
    void RestartPosition(){
        Debug.Log("restartPosition");
        if(!isControll)return;
        if(MapManager.Instance.isDeadzone)
            respawnPosition = new Vector3(MapManager.Instance.respawnPosition.x,MapManager.Instance.respawnPosition.y,startPosition.z);
        
        Rigidbody.drag = 0.05f;
        transform.DOKill();
        bike_body.DOKill();
        bike_body.transform.DORotate(new Vector3(0,90,0),0);
        transform.position = respawnPosition;
        transform.rotation = Quaternion.Euler(0,90,0);
        GetComponent<CenterOfMass>().Reset();
        crash = false;
        objectDetecter.gameObject.SetActive(true);
        foreach (WheelComponent wheel in wheels)
        {
            wheel.axle.localPosition = wheel.startPos;
        }
        //bikeSetting.bikerMan.transform.SetParent(this.gameObject.transform);
        bikerMan.localPosition = new Vector3(0,bikerStartPosition.y,0);
        animator.enabled = true;
        animator.applyRootMotion = true;
        animator.ApplyBuiltinRootMotion();
        OnReset.OnNext(default);
        //OnPlayerCrash.OnNext(crash);
        StopMotor();
    }
    void StopMotor(){
        
         foreach(WheelComponent component in wheels){
             component.collider.motorTorque = 0f;
             component.collider.brakeTorque = 0;
         }
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;   
    }
    #endregion
    
    #region  Player Animation
    void SetPlayerAnimator(){
        if(animator != null){
           // animator.SetFloat("AxisX",axisX);
           animator.SetBool("isLeft",isLeft);
           animator.SetBool("isRight",isRight);
           animator.SetFloat("speed",speed);
        }
    }

    #endregion 
    
    
    #region  Bolt Network
    public override void Attached(){
        PlayerProfileToken token = entity.AttachToken as PlayerProfileToken;
        if(entity.Source != null){
            Debug.Log(entity.Source.ConnectToken);
        }
        state.SetTransforms(state.Transform, transform);

        bikeCustomize.SetUpBike(token.playerBikeData);
        startPosition = transform.position;
        respawnPosition = startPosition;
        bikerStartPosition = bikerMan.localPosition;
        boostSystem = GetComponent<BoostSystem>();
        //LoadSystemForm data
        SetupEngine(engine_system);
        SetupNosSystem(nos_system);
        wheels = new WheelComponent[2];
        SetupFrontWheelSystem(wheel_systems[0]);
        SetupRearWheelSystem(wheel_systems[1]);
        SetupControl(control_system);
        SetupExplode(explode_system);
        SetupStatus(status_system);
        OnEntityAttached.OnNext(entity);
    }
    public override void ControlGained(){

        isControll = true;
        objectDetecter.SetActive(true);
        bikeMiddleWare.ragdollCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        OnControllGained.OnNext(true);
        LoadVirtualCamera();
        AddControlEventListener();
        AddBikeSettingListener();
        SetUpPlayerData();
       
    }
    void AddBikeSettingListener(){

    }

    #region Setup all Systems
    void SetUpPlayerData(){
        bodyCollider.gameObject.name = "PlayerCollider";
        var playerEquipmentToken = new PlayerEquipmentToken();
        playerEquipmentToken.playerEquipmentMapper = SaveMockupData.GetEquipment.playerEquipmentMapper;
        var bikeEquipmentToken = new BikeEquipmentToken();
        bikeEquipmentToken.bikeEquipmentMapper = SaveMockupData.GetBikeEquipment.bikeEquipmentMapper;
        state.PlayerEquiped = playerEquipmentToken;
        state.BikeEquiped = bikeEquipmentToken;
    }
    
    void SetupEngine(BikeEngineData new_engine_system){
        engine_system = new_engine_system;
        currentSpeedLimit = engine_system.speedLimit;

    }
    void SetupNosSystem(NosSystemData new_nosSystem){
        nos_system = new_nosSystem;
    }
    void SetupFrontWheelSystem(WheelSystemData new_wheelSystemData){
        wheel_systems[0] = new_wheelSystemData;
        wheels[0] = SetWheelComponent(connectWheel.wheelFront,
                                        connectWheel.modelWheelFront,
                                            connectWheel.AxleFront,false,0,
                                                connectWheel.AxleFront.localPosition.y,
                                                   new_wheelSystemData,null);
    }
    void SetupControl(ControlSystemData new_controlSystem){
        control_system = new_controlSystem;
    }
    void SetupStatus(StatusSystemData new_statusSystem){
        status_system = new_statusSystem;
    }
    void SetupExplode(ExplodeSystemData new_explodeSystem){
        explode_system = new_explodeSystem;
    }
    void SetupRearWheelSystem(WheelSystemData new_wheelSystemData){
         wheel_systems[1] = new_wheelSystemData;
         wheels[1] = SetWheelComponent(connectWheel.wheelBack,
                                        connectWheel.modelWheelBack,
                                            connectWheel.AxleBack,true,0,
                                                connectWheel.AxleBack.localPosition.y,
                                                   new_wheelSystemData,null);
    }

    void LoadVirtualCamera(){
        VirtualPlayerCamera.Instantiate();
        VirtualPlayerCamera.instance.FollowTarget(bike_body);
        VirtualPlayerCamera.instance.LookupTarget(bike_body);
    }

    #endregion
    void PollKey(){
        if(!isControll || !isReady)return;
        accel = motorControl.accelerator;
        brake = motorControl.brake;
        jump = motorControl.isJump;
        isLeft = motorControl.isLeft;
        isRight = motorControl.isRight;
        
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
            UpdateWheelRotation();
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

    #endregion

    #region  Bike Movement

    public override void SimulateOwner(){
        var indexWhell = 0;
        if(Rigidbody.velocity.magnitude > currentSpeedLimit){
            Rigidbody.velocity = Vector3.ClampMagnitude(Rigidbody.velocity, currentSpeedLimit);
        }
        speed = transform.InverseTransformDirection(Rigidbody.velocity).z * 3.6f;
        foreach(WheelComponent component in wheels){
            if(jump && isGround.Any(g => g == true)){
                Rigidbody.AddForce((grounded ? new Vector3(0,1.5f,0f) : new Vector3(0,0.5f,0.5f))* Rigidbody.mass*control_system.forceJump);
            }
           if(brake){
                component.collider.brakeTorque = engine_system.brakeTorque;
                indexWhell++;
                continue;
            }else
            {

                if(component.drive){
                    if(accel == 0){
                        ReleaseTorque();   
                        indexWhell ++;
                        continue;
                    }else{
                        ReleaseBrake();
                    }
                }else{
                    if(accel != 0){
                        ReleaseBrake();
                    }
                } 
            }
            // if(speed > currentSpeedLimit && !isBoosting){
            //     speed = currentSpeedLimit;
            // }
            if(component.drive &&!brake){
                if(Mathf.Abs(speed) < 4 || Mathf.Sign(speed) == Mathf.Sign(accel)){
                    var torqueSpeed = isBoosting ? nos_system.boostTorque.Evaluate(speed) : engine_system.motorTorque.Evaluate(speed);
                    component.collider.motorTorque = accel * torqueSpeed * 4 / 1;
                }else{
                    component.collider.brakeTorque = Mathf.Abs(accel) * engine_system.brakeTorque;
                }
            }
            indexWhell++;
        }
        //Rigidbody.AddForce(-transform.up*speed,ForceMode.Force);
    }
    

    void UpdateWheelRotation(){
         var indexWhell = 0;
         foreach(WheelComponent component in wheels){
            WheelHit hit;
            Quaternion quaternion;
            Vector3 position;
            component.collider.GetWorldPose(out position,out quaternion);
            component.rotation = Mathf.Repeat(component.rotation + BoltNetwork.FrameDeltaTime * component.collider.rpm * 360.0f / 60.0f, 360.0f);
            component.wheel.localRotation = Quaternion.Euler(component.rotation,0,0);
            component.modelWheel.localRotation = Quaternion.Euler(component.rotation,0,0);
            Vector3 lp = component.axle.localPosition;
            isGround[indexWhell] = component.collider.GetGroundHit(out hit);
            if(isGround[indexWhell]){
                lp.y -= Vector3.Dot(component.wheel.position - hit.point , transform.TransformDirection(0, 1, 0)) - (component.collider.radius);
            }
            var newPosition = Mathf.Clamp(lp.y, component.startPos.y - wheel_systems[indexWhell].SuspensionDistance, component.startPos.y + wheel_systems[indexWhell].SuspensionDistance);
            if(lp.y <-0.54f){
                lp.y = -0.54f;
            }

            component.axle.localPosition = Vector3.Lerp(component.axle.localPosition,lp,Time.fixedDeltaTime*wheel_systems[indexWhell].shoke_update_speed);
            indexWhell++;
        }
    }
    void ReleaseTorque(){
        wheels[0].collider.motorTorque = 0;
        wheels[1].collider.motorTorque = 0;
        wheels[0].collider.brakeTorque = 0;
        wheels[1].collider.brakeTorque = 0;
    }
    void ReleaseBrake(){
        wheels[0].collider.brakeTorque = 0;
        wheels[1].collider.brakeTorque = 0;
    }
    void CheckGround(){
        if(grounded != (isGround[0] && isGround[1])){
            grounded = !grounded;
            OnGrouned.OnNext(grounded);
        }
    }

    void UpdatePlayerRoll(){
        if(isLeft){
            //direction = -1;
            if(!grounded){
                 Rigidbody.AddTorque(Vector3.forward*control_system.bikeRotatePower,ForceMode.Acceleration);
            }
        }
        if(isRight){
            //direction = 1;
            if(!grounded){
                Rigidbody.AddTorque(-Vector3.forward*control_system.bikeRotatePower,ForceMode.Acceleration);
            }
        }
    }
    
    #endregion

    #region  Boost NOS
    void BoostChecker(){
        if(motorControl.isBoost&& nos_system.boostLimit >0 && !isBoosting && !isBoostDelay){
                isBoosting = true;
                currentSpeedLimit =  nos_system.boostSpeedLimit;
                nos_system.boostLimit -- ;    
                OnBoostChanged.OnNext(nos_system.boostLimit);
                OnBoostTime.OnNext(nos_system.boostTimeLimit);
                if(boostSystem != null)
                    boostSystem.StartBoostEffect((nos_system.boostTimeLimit));
                if(grounded){
                    accel = 1;
                    ForceTorque();
                    Rigidbody.AddForce(transform.forward* Rigidbody.mass*nos_system.boostForce,ForceMode.Impulse);
                    DOTween.To(()=> currentSpeedLimit, x=> currentSpeedLimit = x, engine_system.speedLimit, nos_system.boostTimeLimit).SetEase(Ease.Linear).SetAutoKill();
                    OnChangeScreenBoost.OnNext(default);
                }
        } 
    }
    void ForceTorque(){
       // wheels[0].collider.motorTorque = motorTorque.keys[0].value;
        //wheels[1].collider.motorTorque = motorTorque.keys[1].value;
        wheels[0].collider.brakeTorque = 0;
        wheels[1].collider.brakeTorque = 0;
    }
    void BoostUpdate(){
        if(isBoosting){
            if(currentBoostTime < nos_system.boostTimeLimit){
                currentBoostTime += BoltNetwork.FrameDeltaTime*1;
            }else
            {

                Rigidbody.AddForce(-transform.forward*1,ForceMode.VelocityChange);
                wheels[1].collider.brakeTorque = 3000;
                wheels[0].collider.brakeTorque = 3000;
                isBoostDelay = true;
                isBoosting = false;
                Observable.Timer(System.TimeSpan.FromSeconds(nos_system.boostDelay)).Subscribe(_=>{
                    isBoostDelay = false;
                }).AddTo(this);
                currentSpeedLimit = engine_system.speedLimit;
                currentBoostTime = 0;
                wheels[1].collider.brakeTorque = 0;
                wheels[0].collider.brakeTorque = 0;
                OnBoostDelay.OnNext(nos_system.boostDelay);
            }
        }
    }
    #endregion
    
    float frontWheel = -0.35f;
    void SetBikeAnimator(){
        // if(bike_animator == null)return;
        // bike_animator.SetFloat("direction",direction);
        // bike_animator.SetFloat("speed",speed);
        // if(visualizeChock){
        //     bike_animator.SetLayerWeight(1,0);
        // }else{
        //     bike_animator.SetLayerWeight(1,1);
        // }
        // if(isGround[0]){
        //     DOTween.To(()=> frontWheel, x=> frontWheel = x, 0.2f,animationTime).SetEase(Ease.InQuad).SetAutoKill();
        // }else
        // {
        //     DOTween.To(()=> frontWheel, x=> frontWheel = x, 0.5f,animationTime).SetEase(Ease.InQuad).SetAutoKill();
        // }
        // bike_animator.SetFloat("frontwheel",frontWheel);
    }

   #region Bike Setup
    WheelComponent SetWheelComponent(Transform wheel,Transform modelWheel, Transform axle, bool drive, float maxSteer, float pos_y,WheelSystemData _wheel_system,GameObject skidmark)
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
        result.modelWheel = modelWheel;
        result.axle = axle;
        result.collider = wheelCol.GetComponent<WheelCollider>();
        result.collider.mass = _wheel_system.Weight;
        result.collider.radius = _wheel_system.Radius;
        result.collider.center = _wheel_system.WheelCenter;
        result.collider.suspensionDistance = _wheel_system.SuspensionDistance;
        result.collider.forceAppPointDistance = _wheel_system.ForceAppointDistance;
        result.collider.wheelDampingRate = _wheel_system.DampingRate;
        result.pos_y = pos_y;
        result.maxSteer = maxSteer;
        result.startPos = axle.transform.localPosition;
        
        JointSpring spring = new JointSpring();
        spring.spring = _wheel_system.SuspensionSpring.spring;//bikeWheels.setting.SuspensionSpring.spring;
        spring.damper = _wheel_system.SuspensionSpring.damper;//bikeWheels.setting.SuspensionSpring.damper;
        spring.targetPosition = _wheel_system.SuspensionSpring.targetposition;//bikeWheels.setting.SuspensionSpring.targetposition;
        result.collider.suspensionSpring = spring;    
        WheelFrictionCurve frictionCurve = new WheelFrictionCurve();
        frictionCurve.extremumSlip = _wheel_system.ForwardFriction.extremumSlip;//bikeWheels.setting.ForwardFriction.extremumSlip;
        frictionCurve.extremumValue = _wheel_system.ForwardFriction.extremumValue;//bikeWheels.setting.ForwardFriction.extremumValue;
        frictionCurve.asymptoteSlip = _wheel_system.ForwardFriction.asymptoteSlip;//bikeWheels.setting.ForwardFriction.asymptoteSlip;
        frictionCurve.asymptoteValue = _wheel_system.ForwardFriction.asymptoteValue;//bikeWheels.setting.ForwardFriction.asymptoteValue;
        frictionCurve.stiffness = _wheel_system.ForwardFriction.stiffness;//bikeWheels.setting.ForwardFriction.stiffness;
        result.collider.forwardFriction = frictionCurve;

        WheelFrictionCurve sidewayFriction = new WheelFrictionCurve();
        sidewayFriction.extremumSlip = _wheel_system.SidewaysFriction.extremumSlip;//bikeWheels.setting.SidewaysFriction.extremumSlip;
        sidewayFriction.extremumValue = _wheel_system.SidewaysFriction.extremumValue;//bikeWheels.setting.SidewaysFriction.extremumValue;
        sidewayFriction.asymptoteSlip = _wheel_system.SidewaysFriction.asymptoteSlip;//bikeWheels.setting.SidewaysFriction.asymptoteSlip;
        sidewayFriction.asymptoteValue = _wheel_system.SidewaysFriction.asymptoteValue;//bikeWheels.setting.SidewaysFriction.asymptoteValue;
        sidewayFriction.stiffness = _wheel_system.SidewaysFriction.stiffness;//bikeWheels.setting.SidewaysFriction.stiffness;
        result.collider.sidewaysFriction = frictionCurve;
        

        if(_wheel_system.addSphereCollider){

            result.sphereCollider = result.collider.gameObject.AddComponent<SphereCollider>();
            result.sphereCollider.radius = 0.2f;
            result.sphereCollider.material = _wheel_system.physicMaterial;
        }
        var wheelSkid = result.collider.gameObject.AddComponent<WheelSkid>();
        wheelSkid.rb = Rigidbody;
        wheelSkid.skidmarksController = GetComponent<Skidmarks>();
        var wheelParticle = Instantiate(_wheel_system.wheelParticleObject,Vector3.zero,Quaternion.identity,effect_root);
        wheelSkid.animationSkid = wheelParticle;
        wheelSkid.ps = wheelParticle.GetComponent<ParticleSystem>();
        Rigidbody.velocity = Vector3.zero;
        return result;
    }
    JointSpring SetUpSuspensionSpring(float springValue,float damperValue,float targetPositionValue){
        JointSpring jointSpring = new JointSpring();
        jointSpring.spring = springValue;
        jointSpring.damper = damperValue;
        jointSpring.targetPosition = targetPositionValue;
        return jointSpring;
    }
    WheelFrictionCurve SetupForwardFriction(float _extremumSlip,float _extremumValue,float _asymptoteSlip,float _asymptoteValue,float _stiffness){
        WheelFrictionCurve frictionCurve = new WheelFrictionCurve();
        frictionCurve.extremumSlip = _extremumSlip;
        frictionCurve.extremumValue = _extremumValue;
        frictionCurve.asymptoteSlip = _asymptoteSlip;
        frictionCurve.asymptoteValue = _asymptoteValue;
        frictionCurve.stiffness = _stiffness;
        return frictionCurve;
    }
    void SetupLandingCurve(float maxFall,float maxSpeed,float minFall,float minSpeed){
         var curve = new AnimationCurve(new Keyframe(maxFall, maxSpeed), new Keyframe(minFall,minSpeed));
            curve.preWrapMode = WrapMode.ClampForever;
            curve.postWrapMode = WrapMode.ClampForever;
            //landingCurve = curve;
    }
    #endregion

    #region AddCallback
    
    #endregion
    void ExplodeBump(){
        print(Depug.Log("Explode Bump ",Color.red));
          crash = true;
               // respawnPosition = new Vector3(crashPosition.x,crashPosition.y+2.5f,startPosition.z);
            Vector3[] path = new Vector3[]{
                    new Vector3(transform.position.x,transform.position.y+2f,transform.position.z-3f),
                    new Vector3(transform.position.x,transform.position.y+0f,transform.position.z-5f),
                    new Vector3(transform.position.x,transform.position.y-2f,transform.position.z-8),
            };

            var ranBodyRotationX = UnityEngine.Random.Range(explode_system.bodyRotationRageX.x,explode_system.bodyRotationRageX.y);
            var ranBodyRotationY = UnityEngine.Random.Range(explode_system.bodyRotationRageY.x,explode_system.bodyRotationRageY.y);
            var ranBodyRotationZ = UnityEngine.Random.Range(explode_system.bodyRotationRageZ.x,explode_system.bodyRotationRageZ.y);
            bike_body.DORotate(new Vector3(ranBodyRotationX,ranBodyRotationY,ranBodyRotationZ),explode_system.explosionDuration).SetAutoKill();
            transform.DOPath(path,explode_system.explosionDuration,PathType.Linear).SetAutoKill();
            OnPlayerCrash.OnNext(crash);
            OnCrash();
    }
    void OnDestroy(){
        
    }
}
