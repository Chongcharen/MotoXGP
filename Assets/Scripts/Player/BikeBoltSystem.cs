using System.Security.Cryptography.X509Certificates;
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
public class BikeBoltSystem : EntityEventListener<IPlayerBikeState>
{
    public float[] myMotorTorque = new float[2];
    public float[] myBrakeTorque = new float[2];

    public float[] dotProduct = new float[2];
    public float animationTime;
    public float idleAnimationTime;
    public float chockSpeedUpdate = 1;
    public DG.Tweening.Ease ease;
    public Vector3 DownForce;
    public Vector3[] suspensions = new Vector3[2];

    public Vector3[] wheelStartPosition = new Vector3[2];

    public PhysicMaterial physicMaterial;
    public float maxVelocity =20;
    public float boostVelocity = 40;
    public float normalVelocity = 20;

    public float downLimit = -5;
    [Header("skidmark")]
    public Skidmarks skidmarkController;
    public GameObject[] animationSkid;
    [Header("bikeComponent")]
    public Transform carParent;
    public Transform swingarmParent;
    public GameObject rearWheelJoint;
    public RearLookAt rearLookAt;
    //
    public static Subject<BoltEntity> OnEntityAttached = new Subject<BoltEntity>();
    public static Subject<BoltEntity> OnEntityDetached = new Subject<BoltEntity>();
    public static Subject<Unit> OnChangeScreenBoost = new Subject<Unit>();
    public static Subject<int> OnBoostChanged = new Subject<int>();
    public static Subject<float> OnBoostTime = new Subject<float>();
    public static Subject<float> OnBoostDelay = new Subject<float>();
    public static Subject<bool> OnGrouned = new Subject<bool>();

    public static Subject<bool> OnPlayerCrash = new Subject<bool>();

    public static Subject<bool> OnControllGained = new Subject<bool>();
    public static Subject<int> OnShowSpeed = new Subject<int>();
    public static Subject<Unit> OnReset = new Subject<Unit>();
    public static Subject<Transform> OnCameraLookup = new Subject<Transform>();
    public static Subject<BikeSettingMappingData> OnUpdateBikeSettingData = new Subject<BikeSettingMappingData>();
    [Header("BikeStatus")]
    public BikeStatus BikeStatus = BikeStatus.Normal;
    [Header("MiddleWare")]
    BikeMiddleware bikeMiddleWare;
    #region Respawn
    public Vector3 startPosition;
    Vector3 respawnPosition;
    public Vector3 bikerStartPosition;
    #endregion
    #region Bike Property
    [Header("Body Setting")]
    
    [SerializeField]Collider bodyCollider;
    [SerializeField]GameObject objectDetecter;
    public bool visualizeWheel = false;
    public bool visualizeChock = false;
    Rigidbody Rigidbody;
    Transform currentSpawn;
    public float direction = 0.5f;

    [Header("Bike Setting")]
    [SerializeField]BilkWheelSetting bikeWheelSetting;
    [SerializeField]BikeSetting bikeSetting;
    WheelComponent[] wheels;
    [Header ("Speed Setting")]
    [SerializeField] AnimationCurve motorTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));
    [SerializeField] AnimationCurve boostTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));
    float speed = 0;
    public float currentSpeedLimit =0;
    public float speedLimit = 60;
    
    public float boostSpeedLimit = 120;

    [Header("Landing Setting")]
    [SerializeField] AnimationCurve landingCurve = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));
    public bool landingActive = true;
    
    [Header("Boost Setting")]
    [SerializeField] float boostForce = 200;
    [SerializeField] float brakeForce = 5000;
    [SerializeField] float boostTimeLimit = 5;
    [SerializeField] float boostDelay =2;
    BoostSystem boostSystem;
    bool isBoosting = false;
    bool isBoostDelay = false;
    int boostLimit = 18;
    float currentBoostTime = 0;
    public int BoostLimit{
        get{
            return boostLimit;
        }
        set{
            boostLimit = value;
            OnBoostChanged.OnNext(boostLimit);
        }
    }
    
    public float BoostForce { get { return boostForce; } set { boostForce = value; } }
    public float BreakForce { get { return boostForce; } set { boostForce = value; } }
    [Header("Lower Gear")]
    [SerializeField]int lower_gear_force = 200;
    [SerializeField]float lower_gear_force_time_limit = 0.2f;
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
    [SerializeField]Animator bike_animator;
    [SerializeField]RagdollCollider ragdollCollider;
    [SerializeField]GameObject ragdollObject;
    public float airDrag = 0.1f;
    float accel;
    public float forceJump = 150;
    public float bikeRotatePower = 10;
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
    PlayerProfileToken playerProfileToken;
    public float currentTorque = 0;
    void Awake(){
        bikeMiddleWare = GetComponent<BikeMiddleware>();
        Rigidbody = GetComponent<Rigidbody>();
        bikeCustomize = GetComponent<BikeCustomize>();
        wheels = new WheelComponent[2];
        wheels[0] = SetWheelComponent(bikeWheelSetting.wheels.wheelFront,bikeWheelSetting.wheels.modelWheelFront,bikeWheelSetting.wheels.AxleFront,true,0,bikeWheelSetting.wheels.AxleFront.localPosition.y,bikeWheelSetting.wheelSettings[0],animationSkid[0],false);
        wheels[1] = SetWheelComponent(bikeWheelSetting.wheels.wheelBack,bikeWheelSetting.wheels.modelWheelBack,bikeWheelSetting.wheels.AxleBack,true,0,bikeWheelSetting.wheels.AxleBack.localPosition.y,bikeWheelSetting.wheelSettings[1],animationSkid[1]);
        
    }
    void SetupBikeUIData(){
        var wheelSettings = new WheelColliderSetting[2];
        var frontWheelSetting = new WheelColliderSetting{
            mass = wheels[0].collider.mass,
            wheelDampingRate = wheels[0].collider.wheelDampingRate,
            suspensionDistance = wheels[0].collider.suspensionDistance,
            forceApppointDistance = wheels[0].collider.forceAppPointDistance,
            spring = wheels[0].collider.suspensionSpring.spring,
            damper = wheels[0].collider.suspensionSpring.damper,
            targetPosition = wheels[0].collider.suspensionSpring.targetPosition,
            extremumSlip = wheels[0].collider.forwardFriction.extremumSlip,
            extremumValue = wheels[0].collider.forwardFriction.extremumValue,
            asymptoteSlip = wheels[0].collider.forwardFriction.asymptoteSlip,
            asymptoteValue = wheels[0].collider.forwardFriction.asymptoteValue,
            stiffness = wheels[0].collider.forwardFriction.stiffness
        };
        var rearWheelSetting = new WheelColliderSetting{
            mass = wheels[1].collider.mass,
            wheelDampingRate = wheels[1].collider.wheelDampingRate,
            suspensionDistance = wheels[1].collider.suspensionDistance,
            forceApppointDistance = wheels[1].collider.forceAppPointDistance,
            spring = wheels[1].collider.suspensionSpring.spring,
            damper = wheels[1].collider.suspensionSpring.damper,
            targetPosition = wheels[1].collider.suspensionSpring.targetPosition,
            extremumSlip = wheels[1].collider.forwardFriction.extremumSlip,
            extremumValue = wheels[1].collider.forwardFriction.extremumValue,
            asymptoteSlip = wheels[1].collider.forwardFriction.asymptoteSlip,
            asymptoteValue = wheels[1].collider.forwardFriction.asymptoteValue,
            stiffness = wheels[1].collider.forwardFriction.stiffness
        };
        wheelSettings[0] = frontWheelSetting;
        wheelSettings[1] = rearWheelSetting;
        var bikeEngineSetting = new BikeEngineSetting{
            engine_max_torque = motorTorque.keys[0].value,
            engine_max_velocity = maxVelocity,
            engine_nos_velocity = boostVelocity,
            engine_jump_power = forceJump,
            engine_rotate_bike_power = bikeRotatePower,
            engine_animation_choke = chockSpeedUpdate,
            engine_decrease_torque = motorTorque.keys[1].value
        };
        var rigidbodyTuner = new RigidbodyTuner{
            mass = Rigidbody.mass,
            drag = Rigidbody.drag,
            angularDrag = Rigidbody.angularDrag
        };
        var landingTuner = new LandingTunner{
            maxFall = landingCurve.keys[0].time,
            maxSpeed = landingCurve.keys[0].value,
            minFall = landingCurve.keys[1].time,
            minSpeed = landingCurve.keys[1].value,
            active = landingActive
        };
        var mappingData = new BikeSettingMappingData{
            landing = landingTuner,
            rigidbody = rigidbodyTuner,
            engineSetting = bikeEngineSetting,
            wheelColliderSettings = wheelSettings
        };
        print(Depug.Log("SEting data ***********************************************",Color.white));
        OnUpdateBikeSettingData.OnNext(mappingData);
    }
    
    void AddControlEventListener(){
        GameHUD.OnSwitchChoke.Subscribe(_=>{
            visualizeChock = !visualizeChock;
            if(visualizeChock)
                SetupBikePhysic();
            else
                SetupBikeAnimation();
        }).AddTo(this);
        GameCallback.OnGameReady.Subscribe(raceCountdown =>{
            isReady = raceCountdown.RaceStart;
            GetComponent<PlayerGlowing>().CloseRimlight();
        }).AddTo(this);
        GameHUD.OnLowerGear.Subscribe(_=>{
                if(!grounded)return;
                if(!isReady)return;
                maxVelocity = boostVelocity;
                if(boostSystem != null)
                    boostSystem.StartBoostEffect(lower_gear_force_time_limit,false);
                Rigidbody.AddForce(transform.forward* Rigidbody.mass*lower_gear_force,ForceMode.Impulse);
                DOTween.To(()=> maxVelocity, x=> maxVelocity = x, normalVelocity, lower_gear_force_time_limit).SetEase(ease).SetAutoKill();
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
    void AddBikeSettingListener(){
        //
        GetComponent<PlayerGlowing>().ShowRimlight();
        UI_UpdateWheel.OnUpdateLandingMaxFall.Subscribe(_=>{
            SetupLandingCurve(_,landingCurve.keys[0].value,landingCurve.keys[1].time,landingCurve.keys[1].value);
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateLandingMaxSpeed.Subscribe(_=>{
            SetupLandingCurve(landingCurve.keys[0].time,_,landingCurve.keys[1].time,landingCurve.keys[1].value);
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateLandingMinFall.Subscribe(_=>{
            SetupLandingCurve(landingCurve.keys[0].time,landingCurve.keys[0].value,_,landingCurve.keys[1].value);
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateLandingMinSpeed.Subscribe(_=>{
            SetupLandingCurve(landingCurve.keys[0].time,landingCurve.keys[0].value,landingCurve.keys[1].time,_);
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateLandingActive.Subscribe(_=>{
            landingActive = _;
        }).AddTo(this);
        //rigidbody
        UI_UpdateWheel.OnUpdateRigidbodyMass.Subscribe(_=>{
            Rigidbody.mass = _;
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateRigidbodyDrag.Subscribe(_=>{
            Rigidbody.drag = _;
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateRigidbodyAngularDarg.Subscribe(_=>{
            Rigidbody.angularDrag = _;
        }).AddTo(this);
        //engine
        UI_UpdateWheel.OnUpdateMaxTorque.Subscribe(_ =>{
            var curve = new AnimationCurve(new Keyframe(0, _), new Keyframe(maxVelocity,motorTorque.keys[1].value));
            curve.preWrapMode = WrapMode.PingPong;
            curve.postWrapMode = WrapMode.PingPong;
            motorTorque = curve;
            curve = new AnimationCurve(new Keyframe(0, _*1.5f), new Keyframe(boostVelocity,0));
            curve.preWrapMode = WrapMode.PingPong;
            curve.postWrapMode = WrapMode.PingPong;
            boostTorque = curve;
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateMaxVelocity.Subscribe(_ =>{
            maxVelocity = normalVelocity =_;
            var curve = new AnimationCurve(new Keyframe(0, motorTorque.keys[0].value), new Keyframe(maxVelocity, motorTorque.keys[1].value));
            curve.preWrapMode = WrapMode.PingPong;
            curve.postWrapMode = WrapMode.PingPong;
            motorTorque = curve;
        }).AddTo(this);
        UI_UpdateWheel.OnupdateDecreaseTorque.Subscribe(_ =>{
           var curve = new AnimationCurve(new Keyframe(0, motorTorque.keys[0].value), new Keyframe(maxVelocity,_));
            curve.preWrapMode = WrapMode.PingPong;
            curve.postWrapMode = WrapMode.PingPong;
            motorTorque = curve;
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateNosVelocity.Subscribe(_ =>{
            boostVelocity = _;
            var curve = new AnimationCurve(new Keyframe(0, boostTorque.keys[0].value), new Keyframe(boostVelocity,0));
            curve.preWrapMode = WrapMode.PingPong;
            curve.postWrapMode = WrapMode.PingPong;
            boostTorque = curve;
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateJumpPower.Subscribe(_ =>{
            forceJump = _;
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateRotateBikePower.Subscribe(_ =>{
            bikeRotatePower = _;
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateAnimationChoke.Subscribe(_ =>{
            chockSpeedUpdate = _;
        }).AddTo(this);
        
        //wheel
        UI_UpdateWheel.OnUpdateWheelMass.Subscribe(_ =>{
           wheels[_.Item1].collider.mass = _.Item2; 
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateWheelDampingRate.Subscribe(_ =>{
           wheels[_.Item1].collider.wheelDampingRate = _.Item2;
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateWheelSuspensionDistance.Subscribe(_ =>{
           wheels[_.Item1].collider.suspensionDistance = _.Item2;
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateWheelForceApppointDistance.Subscribe(_ =>{
           wheels[_.Item1].collider.forceAppPointDistance = _.Item2;
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateWheelSpring.Subscribe(_ =>{
            wheels[_.Item1].collider.suspensionSpring = SetUpSuspensionSpring(_.Item2,wheels[_.Item1].collider.suspensionSpring.damper,wheels[_.Item1].collider.suspensionSpring.targetPosition);
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateWheelDamper.Subscribe(_ =>{
           wheels[_.Item1].collider.suspensionSpring = SetUpSuspensionSpring(wheels[_.Item1].collider.suspensionSpring.spring,_.Item2,wheels[_.Item1].collider.suspensionSpring.targetPosition);
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateWheelTargetPosition.Subscribe(_ =>{
           wheels[_.Item1].collider.suspensionSpring = SetUpSuspensionSpring(wheels[_.Item1].collider.suspensionSpring.spring,wheels[_.Item1].collider.suspensionSpring.damper,_.Item2);
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateWheelExtremumSlip.Subscribe(_ =>{
            var forwardFriction = wheels[_.Item1].collider.forwardFriction;
           forwardFriction = SetupForwardFriction(_.Item2,forwardFriction.extremumValue,forwardFriction.asymptoteSlip,forwardFriction.asymptoteValue,forwardFriction.stiffness);
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateWheelExtremumValue.Subscribe(_ =>{
           var forwardFriction = wheels[_.Item1].collider.forwardFriction;
           forwardFriction = SetupForwardFriction(forwardFriction.extremumSlip,_.Item2,forwardFriction.asymptoteSlip,forwardFriction.asymptoteValue,forwardFriction.stiffness);
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateWheelAsymptoteSlip.Subscribe(_ =>{
           var forwardFriction = wheels[_.Item1].collider.forwardFriction;
           forwardFriction = SetupForwardFriction(forwardFriction.extremumSlip,forwardFriction.extremumValue,_.Item2,forwardFriction.asymptoteValue,forwardFriction.stiffness);
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateWheelAsymptoteValue.Subscribe(_ =>{
           var forwardFriction = wheels[_.Item1].collider.forwardFriction;
           forwardFriction = SetupForwardFriction(forwardFriction.extremumSlip,forwardFriction.extremumValue,forwardFriction.asymptoteSlip,_.Item2,forwardFriction.stiffness);
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateWheelStiffness.Subscribe(_ =>{
           var forwardFriction = wheels[_.Item1].collider.forwardFriction;
           forwardFriction = SetupForwardFriction(forwardFriction.extremumSlip,forwardFriction.extremumValue,forwardFriction.asymptoteSlip,forwardFriction.asymptoteValue,_.Item2);
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateLandingSetting.Subscribe(_=>{
            SetupLandingCurve(_.maxFall,_.maxSpeed,_.minFall,_.minSpeed);
            landingActive = _.active;
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateRigidbodySetting.Subscribe(_ =>{
            Rigidbody.mass = _.mass;
            Rigidbody.drag = _.drag;
            Rigidbody.angularDrag = _.angularDrag;
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateBikeEngineSetting.Subscribe(_ =>{
            var curve = new AnimationCurve(new Keyframe(0, _.engine_max_torque), new Keyframe(_.engine_max_velocity,motorTorque.keys[1].value));
            curve.preWrapMode = WrapMode.PingPong;
            curve.postWrapMode = WrapMode.PingPong;
            motorTorque = curve;
            maxVelocity = _.engine_max_velocity;
            normalVelocity = maxVelocity;
            //
            boostVelocity = _.engine_nos_velocity;
            curve = new AnimationCurve(new Keyframe(0, _.engine_max_torque*1.5f), new Keyframe(boostVelocity,0));
            curve.preWrapMode = WrapMode.PingPong;
            curve.postWrapMode = WrapMode.PingPong;
            boostTorque = curve;
            //
            forceJump = _.engine_jump_power;
            bikeRotatePower = _.engine_rotate_bike_power;
            chockSpeedUpdate = _.engine_animation_choke;
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateFrontWheelSetting.Subscribe(_ =>{
            wheels[0].collider.mass = _.mass;
            wheels[0].collider.wheelDampingRate = _.wheelDampingRate;
            wheels[0].collider.suspensionDistance = _.suspensionDistance;
            wheels[0].collider.forceAppPointDistance = _.forceApppointDistance;
            wheels[0].collider.suspensionSpring = SetUpSuspensionSpring(_.spring,_.damper,_.targetPosition);
            wheels[0].collider.forwardFriction = SetupForwardFriction(_.extremumSlip,_.extremumValue,_.asymptoteSlip,_.asymptoteValue,_.stiffness);
        }).AddTo(this);
        UI_UpdateWheel.OnUpdateRearWheelSetting.Subscribe(_ =>{
            wheels[1].collider.mass = _.mass;
            wheels[1].collider.wheelDampingRate = _.wheelDampingRate;
            wheels[1].collider.suspensionDistance = _.suspensionDistance;
            wheels[1].collider.forceAppPointDistance = _.forceApppointDistance;
            wheels[1].collider.suspensionSpring = SetUpSuspensionSpring(_.spring,_.damper,_.targetPosition);
            wheels[1].collider.forwardFriction = SetupForwardFriction(_.extremumSlip,_.extremumValue,_.asymptoteSlip,_.asymptoteValue,_.stiffness);
        }).AddTo(this);
    }

    //update spring on UI_UpdateWheel
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
    void SetupBikePhysic(){
        rearWheelJoint.transform.SetParent(carParent);
        rearLookAt.enabled = true;
    }
    void SetupBikeAnimation(){
        rearWheelJoint.transform.SetParent(swingarmParent);
        rearLookAt.enabled = false;
    }
    void ExplodeBump(){
        print(Depug.Log("Explode Bump ",Color.red));
          crash = true;
               // respawnPosition = new Vector3(crashPosition.x,crashPosition.y+2.5f,startPosition.z);
            Vector3[] path = new Vector3[]{
                    new Vector3(transform.position.x,transform.position.y+2f,transform.position.z-3f),
                    new Vector3(transform.position.x,transform.position.y+0f,transform.position.z-5f),
                    new Vector3(transform.position.x,transform.position.y-2f,transform.position.z-8),
            };
            var ranBodyRotationX = UnityEngine.Random.Range(bodyRotationRageX.x,bodyRotationRageX.y);
            var ranBodyRotationY = UnityEngine.Random.Range(bodyRotationRageY.x,bodyRotationRageY.y);
            var ranBodyRotationZ = UnityEngine.Random.Range(bodyRotationRageZ.x,bodyRotationRageZ.y);
            bikeSetting.MainBody.transform.DORotate(new Vector3(ranBodyRotationX,ranBodyRotationY,ranBodyRotationZ),explosionDuration).SetAutoKill();
            transform.DOPath(path,explosionDuration,PathType.Linear).SetAutoKill();
            //myRigidbody.AddExplosionForce(explosionForce,transform.position,explosionRadius,explosionUpward,explosionMode);
            OnPlayerCrash.OnNext(crash);
            OnCrash();
    }
    void FixedUpdate(){
        UpdateWheelRotation();
        CheckGround();
        SetPlayerAnimator();
        SetBikeAnimator();
        if(!isControll || !isReady)return;
        PollKey();
        //UpdateWheel();
        BoostChecker();
        BoostUpdate();
       
        UpdatePlayerRoll();
        CheckSpeed();
        Landing();
    }
    void Landing(){
        if(!landingActive)return;
        // if(Rigidbody.velocity.y < downLimit){
        //     Rigidbody.velocity = new Vector3(Rigidbody.velocity.x,downLimit,Rigidbody.velocity.z);
        // }

        // Bit shift the index of the layer (8) to get a bit mask
        LayerMask layermask = LayerMask.GetMask("Road");

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit,5, layermask))
        {
            var landingUp = landingCurve.Evaluate(Rigidbody.velocity.y);
            Rigidbody.velocity += new Vector3(0,landingUp,0);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) *5 , Color.white);
           // Debug.Log("Did not Hit");
        }
    }
    void CheckSpeed(){
        if(isControll)
            OnShowSpeed.OnNext((int)speed);

        var wheelIndex = 0;
        foreach(WheelComponent component in wheels)
        {
            myMotorTorque[wheelIndex] = component.collider.motorTorque;
            myBrakeTorque[wheelIndex] = component.collider.brakeTorque;
            wheelIndex ++;
        }
    }
    
    
    #region  CrashEvent
    void OnCrash(){
        Debug.Log("Bolt Crash !!!!!");
        animator.applyRootMotion = false;
        animator.enabled = false;
        Rigidbody.drag = 2;
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
        //animator.gameObject.transform.SetParent(animator.gameObject.transform.parent.parent);
        objectDetecter.gameObject.SetActive(false);
        StartCoroutine(DelayRespawn());
        //StartCoroutine(DelayApplyRootMotion());
    }

    IEnumerator DelayApplyRootMotion()
    {
        yield return new WaitForSeconds(0.5f);
        bikeSetting.bikerMan.transform.localPosition = new Vector3(0,bikerStartPosition.y,0);
        
    }

    IEnumerator DelayRespawn(){
        yield return new WaitForSeconds(1);
        RestartPosition();
        yield return new WaitForSeconds(0.5f);
        //bikeSetting.bikerMan.transform.localPosition = new Vector3(0,bikerStartPosition.y,0);
        StopAllCoroutines();
        bikeSetting.bikerMan.transform.localPosition = new Vector3(0,bikerStartPosition.y,0);
    }
    void RestartPosition(){
        Debug.Log("restartPosition");
        if(!isControll)return;
        if(MapManager.Instance.isDeadzone)
            respawnPosition = new Vector3(MapManager.Instance.respawnPosition.x,MapManager.Instance.respawnPosition.y,startPosition.z);
        
        Rigidbody.drag = 0.05f;
        transform.DOKill();
        bikeSetting.MainBody.DOKill();
        bikeSetting.MainBody.transform.DORotate(new Vector3(0,90,0),0);
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
        bikeSetting.bikerMan.transform.localPosition = new Vector3(0,bikerStartPosition.y,0);
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
        //  myRigidbody.isKinematic = true;
        //         myRigidbody.isKinematic = false;
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;   
    }
    #endregion
    
    #region  Player Animation
    void SetPlayerAnimator(){
        if(animator == null)return;
           // animator.SetFloat("AxisX",axisX);
           //animator.SetBool("isLeft",isLeft);
           //animator.SetBool("isRight",isRight);

        if(isLeft)
            DOTween.To(()=> direction, x=> direction = x, 0f, animationTime).SetEase(ease).SetAutoKill();
        else if(isRight)
            DOTween.To(()=> direction, x=> direction = x, 1f,animationTime).SetEase(ease).SetAutoKill();
        else
            DOTween.To(()=> direction, x=> direction = x, 0.5f,animationTime*idleAnimationTime).SetEase(Ease.InQuad).SetAutoKill();

        animator.SetFloat("direction",direction);
        animator.SetFloat("speed",speed);

    }
    float frontWheel = -0.35f;
    void SetBikeAnimator(){
        if(bike_animator == null)return;
        bike_animator.SetFloat("direction",direction);
        bike_animator.SetFloat("speed",speed);
        if(visualizeChock){
            bike_animator.SetLayerWeight(1,0);
        }else{
            bike_animator.SetLayerWeight(1,1);
        }
        if(isGround[0]){
            DOTween.To(()=> frontWheel, x=> frontWheel = x, 0.2f,animationTime).SetEase(Ease.InQuad).SetAutoKill();
        }else
        {
            DOTween.To(()=> frontWheel, x=> frontWheel = x, 0.5f,animationTime).SetEase(Ease.InQuad).SetAutoKill();
        }
        bike_animator.SetFloat("frontwheel",frontWheel);
    }

    #endregion 
    
    
    #region  Bolt Network
    public override void Attached(){
         //change bike model // Random // ต้องไปเอาจาก หน้า custom
        //state.PlayerCustomize.BikeId = Mathf.FloorToInt(Random.Range(1,6));
        //state.PlayerCustomize.BikeTextureId = Mathf.FloorToInt(Random.Range(1,9));
        //
        //Depug.Log("-----------------Attached------------",Color.yellow);
        Debug.Log("-----------------Attached------------");
        //visualizeChock = true;
        //SetupBikePhysic();
        UI_PlayersDistance.OnPlayerColor.Subscribe(_=>{
            if(_.Item1 == entity){
                playerName_txt.color = _.Item2;
            }
        }).AddTo(this);
        playerProfileToken = entity.AttachToken as PlayerProfileToken;
        
        if(entity.Source != null){
            Debug.Log(entity.Source.ConnectToken);
        }
        playerName_txt.text = playerProfileToken.playerProfileModel.DisplayName;
        print(Depug.Log("DisplayName "+playerProfileToken.playerProfileModel.DisplayName,Color.white));
        state.SetTransforms(state.Transform, transform);
        //state.AddCallback("Name",()=>playerName_txt.text = state.Name);
        state.AddCallback("PlayerEquiped",()=> SetUpPlayerEquipment());
        state.AddCallback("BikeEquiped",()=> SetUpBikeEquipment());
        //state.AddCallback("PlayerEquiped")
        //state.AddCallback("PlayerCustomize",()=>state.PlayerCustomize);
        //state.AddCallback("PlayerCustomize",()=>bikeCustomize.SetUpBike(token.playerBikeData));
        //bikeCustomize.SetUpBike(playerProfileToken.playerBikeData);
        startPosition = transform.position;
        respawnPosition = startPosition;
        bikerStartPosition = bikeSetting.bikerMan.transform.localPosition;
        currentSpeedLimit = speedLimit;
        
        boostSystem = GetComponent<BoostSystem>();
        OnEntityAttached.OnNext(entity);
        //Send Bike Data to ui;      
          
    }
    public override void Detached(){
        OnEntityDetached.OnNext(entity);
    }
    public override void ControlGained(){
        isControll = true;
        objectDetecter.SetActive(true);
        bikeMiddleWare.ragdollCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        OnControllGained.OnNext(true);
        OnCameraLookup.OnNext(bikeSetting.MainBody);
        LoadVirtualCamera();
       
        
        AddControlEventListener();
        AddBikeSettingListener();
        SetUpPlayerData();
    }
    async void LoadVirtualCamera(){

        // var virtualCam = await AddressableManager.Instance.LoadObject<GameObject>("Bike/VirtualPlayerCamera.prefab");
        // Instantiate(virtualCam);
        // Debug.Log("A "+virtualCam.GetComponent<VirtualPlayerCamera>());
        // Debug.Log("this.transform "+this.transform);
        // virtualCam.GetComponent<VirtualPlayerCamera>().FollowTarget(this.transform);
        // virtualCam.GetComponent<VirtualPlayerCamera>().LookupTarget(this.transform);
        VirtualPlayerCamera.Instantiate();
        VirtualPlayerCamera.instance.FollowTarget(bikeSetting.MainBody);
        VirtualPlayerCamera.instance.LookupTarget(bikeSetting.MainBody);
    }
    void SetUpPlayerData(){
        //state.PlayerEquiped.Data
        bodyCollider.gameObject.name = "PlayerCollider";
        var playerEquipmentToken = new PlayerEquipmentToken();
        playerEquipmentToken.playerEquipmentMapper = SaveMockupData.GetEquipment.playerEquipmentMapper;
        var bikeEquipmentToken = new BikeEquipmentToken();
        bikeEquipmentToken.bikeEquipmentMapper = SaveMockupData.GetBikeEquipment.bikeEquipmentMapper;
        
        Debug.Log("bike token "+bikeEquipmentToken.bikeEquipmentMapper.Count);

        state.PlayerEquiped = playerEquipmentToken;
        state.BikeEquiped = bikeEquipmentToken;
        //state.Name = playerProfileToken.playerProfileModel.DisplayName;
        //state.SetAnimator(animator);
        print(Depug.Log("Setup PlayerData ",Color.blue));
        print(Depug.Log("Display name "+state.Name,Color.blue));
        foreach (var item in playerEquipmentToken.playerEquipmentMapper)
        {
            print(Depug.Log("e_ model "+item.Value.model_name,Color.blue));
            print(Depug.Log("e_ texture "+item.Value.texture_name,Color.blue));
            print(Depug.Log("---------------------------------------- "+item.Value.texture_name,Color.blue));
        }
         SetupBikeUIData();
    }
    //if playerequipment has been changed , go process in bikemiddleware
    public void SetUpPlayerEquipment(){
        print(Depug.Log("SetUpPlayerEquipment ",Color.green));
        print(Depug.Log("name "+state.Name,Color.green));
        PlayerEquipmentToken equipmentToken = state.PlayerEquiped as PlayerEquipmentToken;
        
         foreach (var item in equipmentToken.playerEquipmentMapper)
        {
            print(Depug.Log("e_ model "+item.Value.model_name,Color.green));
            print(Depug.Log("e_ texture "+item.Value.texture_name,Color.green));
            print(Depug.Log("---------------------------------------- "+item.Value.texture_name,Color.green));
        }
        bikeMiddleWare.SetupPlayerEquipment(equipmentToken);
        // foreach (var item in equipmentToken.playerEquipmentMapper)
        // {
        //     Debug.Log($"Key : {item.Key} , value : {item.Value}");
        // }
    }
    void SetUpBikeEquipment(){
        Debug.Log("Setupbike equipment");
        BikeEquipmentToken bikeEquiupmentToken = state.BikeEquiped as BikeEquipmentToken;

         foreach (var item in bikeEquiupmentToken.bikeEquipmentMapper)
        {
            print(Depug.Log("e_ model "+item.Value.model_name,Color.green));
            print(Depug.Log("e_ texture "+item.Value.texture_name,Color.green));
            print(Depug.Log("---------------------------------------- "+item.Value.texture_name,Color.green));
        }


        bikeMiddleWare.SetupBikeEquipment(bikeEquiupmentToken);
    }
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
            //UpdateWheel();
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
    //เดิมชื่อ UpdateWheel
    public override void SimulateOwner(){
        var indexWhell = 0;
        if(Rigidbody.velocity.magnitude > maxVelocity){
            Rigidbody.velocity = Vector3.ClampMagnitude(Rigidbody.velocity, maxVelocity);
        }
        speed = transform.InverseTransformDirection(Rigidbody.velocity).z * 3.6f;
        foreach(WheelComponent component in wheels){
            //WheelHit hit;
           // Debug.Log("modelwheel "+component.modelWheel);
            if(jump && isGround.Any(g => g == true)){
                Rigidbody.AddForce((grounded ? new Vector3(0,1.5f,0f) : new Vector3(0,0.5f,0.5f))* Rigidbody.mass*forceJump);
            }
           if(brake){
                //Rigidbody.velocity = new Vector3(1,Rigidbody.velocity.y,Rigidbody.velocity.z);
                component.collider.brakeTorque = bikeSetting.brakePower;
                indexWhell++;
                continue;
                // Debug.Log("BrakeTorque "+component.collider.brakeTorque);
                // Debug.Log("MotorTorque "+component.collider.motorTorque);
                // Debug.Log("---------------");
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
            if(speed > currentSpeedLimit && !isBoosting){
                speed = currentSpeedLimit;
            }
            if(component.drive &&!brake){
                if(Mathf.Abs(speed) < 4 || Mathf.Sign(speed) == Mathf.Sign(accel)){
                    var torqueSpeed = isBoosting ? boostTorque.Evaluate(speed) : motorTorque.Evaluate(speed);
                    component.collider.motorTorque = accel * torqueSpeed * diffGearing / 1;
                }else{
                    component.collider.brakeTorque = Mathf.Abs(accel) * bikeSetting.brakePower;
                }
                // var torqueSpeed = isBoosting ? boostTorque.Evaluate(speed) : motorTorque.Evaluate(speed);
                //      component.collider.motorTorque = accel * torqueSpeed * diffGearing / 1;
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
            if(visualizeWheel){
                component.wheel.localRotation = Quaternion.Euler(component.rotation,0,0);
                component.modelWheel.localRotation = Quaternion.Euler(component.rotation,0,0);
            }
            //Debug.Log(component.rotation);
            Vector3 lp = component.axle.localPosition;
             if(isGround[indexWhell] == false && component.collider.GetGroundHit(out hit)){
                 bike_animator.SetTrigger("OnGround");
             }
            isGround[indexWhell] = component.collider.GetGroundHit(out hit);
            if(isGround[indexWhell]){
                lp.y -= Vector3.Dot(component.wheel.position - hit.point , transform.TransformDirection(0, 1, 0)) - (component.collider.radius);
                dotProduct[indexWhell] = Vector3.Dot(component.wheel.position - hit.point , transform.TransformDirection(0, 1, 0)) - (component.collider.radius);
            }


              //  lp.y -= Vector3.Dot(component.wheel.position - component.startPos,transform.TransformDirection(0,1,0)) - (component.collider.radius)*chockSpeedUpdate;
            //Debug.Log("LP "+ lp.y);
            //lp.y = Mathf.Clamp(lp.y, component.startPos.y - bikeWheelSetting.wheelSettings[indexWhell].SuspensionDistance, component.startPos.y + bikeWheelSetting.wheelSettings[indexWhell].SuspensionDistance);
            var newPosition = Mathf.Clamp(lp.y, component.startPos.y - bikeWheelSetting.wheelSettings[indexWhell].SuspensionDistance, component.startPos.y + bikeWheelSetting.wheelSettings[indexWhell].SuspensionDistance);
           // DOTween.To(()=> lp.y, x=> lp.y = x, newPosition, 1f).SetEase(ease).SetAutoKill();
            
            //Debug.Log("min "+ (component.startPos.y - bikeWheelSetting.wheelSettings[indexWhell].SuspensionDistance));
            //Debug.Log("max "+ (component.startPos.y + bikeWheelSetting.wheelSettings[indexWhell].SuspensionDistance));
            if(lp.y <-0.54f){
                lp.y = -0.54f;
            }
            suspensions[indexWhell] = lp;
            if(visualizeChock)
                //component.axle.localPosition = lp;
                component.axle.localPosition = Vector3.Lerp(component.axle.localPosition,lp,Time.fixedDeltaTime*chockSpeedUpdate);
                //component.axle.DOLocalMove(lp,animationTime).SetEase(ease).SetAutoKill();
            else
            {
                if(indexWhell == 0){
                   if(isGround[indexWhell]){
                      frontWheel = lp.y;
                   }
                }else{
                    //bike_animator.SetFloat("rearwheel",lp.y);
                }
            }
            
            indexWhell++;
        }
        //Rigidbody.AddForce(transform.up*speed,ForceMode.Force);
        //DownForce = Rigidbody.velocity;
    }
    void ReleaseTorque(){
        wheels[0].collider.motorTorque = 0;
        wheels[1].collider.motorTorque = 0;
        wheels[0].collider.brakeTorque = 0;
        wheels[1].collider.brakeTorque = 0;
       // Rigidbody.velocity = new Vector3(3,Rigidbody.velocity.y,Rigidbody.velocity.z);
    }
    void ReleaseBrake(){
        wheels[0].collider.brakeTorque = 0;
        wheels[1].collider.brakeTorque = 0;
    }
    void ForceTorque(){
        wheels[0].collider.motorTorque = motorTorque.keys[0].value;
        wheels[1].collider.motorTorque = motorTorque.keys[1].value;
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
                 Rigidbody.AddTorque(Vector3.forward*bikeRotatePower,ForceMode.Acceleration);
            }
        }
        if(isRight){
            //direction = 1;
            if(!grounded){
                Rigidbody.AddTorque(-Vector3.forward*bikeRotatePower,ForceMode.Acceleration);
            }
        }
    }
    
    #endregion

    #region  Boost NOS
    void BoostChecker(){
        if(motorControl.isBoost&& boostLimit >0 && !isBoosting && !isBoostDelay){
                
                isBoosting = true;
                maxVelocity = boostVelocity;
                boostLimit -- ;    
                currentSpeedLimit = boostSpeedLimit;
                OnBoostChanged.OnNext(boostLimit);
                OnBoostTime.OnNext(boostTimeLimit);
                if(boostSystem != null)
                    boostSystem.StartBoostEffect(boostTimeLimit);
                //myRigidbody.AddExplosionForce(explosionPower,explosionTransform.position,explosionRadius,1,ForceMode.Impulse);
                if(grounded){
                    //Rigidbody.AddForce(transform.forward*boostForce,ForceMode.VelocityChange);
                    accel = 1;
                    ForceTorque();
                    Rigidbody.AddForce(transform.forward* Rigidbody.mass*boostForce,ForceMode.Impulse);
                    DOTween.To(()=> maxVelocity, x=> maxVelocity = x, normalVelocity, boostTimeLimit).SetEase(ease).SetAutoKill();
                    OnChangeScreenBoost.OnNext(default);
                }
               
            }
    }
    void BoostUpdate(){
        if(isBoosting){
            if(currentBoostTime < boostTimeLimit){
                //myRigidbody.AddForce(transform.forward*BoostForce);
                currentBoostTime += BoltNetwork.FrameDeltaTime*1;
            }else
            {

                Rigidbody.AddForce(-transform.forward*1,ForceMode.VelocityChange);
                wheels[1].collider.brakeTorque = 3000;
                wheels[0].collider.brakeTorque = 3000;
                isBoostDelay = true;
                isBoosting = false;
                Observable.Timer(System.TimeSpan.FromSeconds(boostDelay)).Subscribe(_=>{
                    isBoostDelay = false;
                }).AddTo(this);
                currentSpeedLimit = speedLimit;
                maxVelocity = normalVelocity;
                currentBoostTime = 0;
                wheels[1].collider.brakeTorque = 0;
                wheels[0].collider.brakeTorque = 0;
                OnBoostDelay.OnNext(boostDelay);
            }
        }
    }
    #endregion
    
    public void SetBikeStatus(BikeStatus newStatus){
        BikeStatus = newStatus;
        switch (BikeStatus) 
        {
        case BikeStatus.Normal:
            Rigidbody.drag = 0.05f;
            break;
        case BikeStatus.Sand:
            Rigidbody.drag = 4.5f;
            break;
        case BikeStatus.Water:
            Rigidbody.drag = 2.5f;
            break;
        }
    }

    #region Bike Setup
    WheelComponent SetWheelComponent(Transform wheel,Transform modelWheel, Transform axle, bool drive, float maxSteer, float pos_y,WheelSetting wheelSetting,GameObject skidmark,bool addSphereCollider = true)
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
        //result.collider.gameObject.AddComponent<enableWheelPhysicMaterial>();
        

        if(addSphereCollider){

            result.sphereCollider = result.collider.gameObject.AddComponent<SphereCollider>();
            result.sphereCollider.radius = 0.2f;
            result.sphereCollider.material = physicMaterial;
        }
        var wheelSkid = result.collider.gameObject.AddComponent<WheelSkid>();
        wheelSkid.rb = Rigidbody;
        wheelSkid.skidmarksController = skidmarkController;
        wheelSkid.animationSkid = skidmark;
        wheelSkid.ps = skidmark.GetComponent<ParticleSystem>();
        Rigidbody.velocity = Vector3.zero;
        return result;
    }
    void SetupLandingCurve(float maxFall,float maxSpeed,float minFall,float minSpeed){
         var curve = new AnimationCurve(new Keyframe(maxFall, maxSpeed), new Keyframe(minFall,minSpeed));
            curve.preWrapMode = WrapMode.ClampForever;
            curve.postWrapMode = WrapMode.ClampForever;
            landingCurve = curve;
    }
    #endregion

    #region AddCallback
    #endregion

    void OnDestroy(){
        
    }
}
public enum BikeStatus{
    Normal,Sand,Water,Speed
}

