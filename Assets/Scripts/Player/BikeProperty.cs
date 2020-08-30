using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(BoostSystem)),RequireComponent(typeof(Rigidbody))]
public class BikeProperty : MonoBehaviour
{
    [Header("Body Setting")]
    
    [SerializeField]Collider bodyCollider;
    [SerializeField]GameObject objectDetecter;
    Rigidbody Rigidbody;
    Transform currentSpawn;

    [Header("Bike Setting")]
    [SerializeField]BilkWheelSetting bikeWheelSetting;
    [SerializeField]BikeSetting bikeSetting;
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
    [Header("Canvas")]
    [SerializeField]Canvas canvas;
    [SerializeField]TextMeshProUGUI playerName_txt;
    

}
