using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BikeEngine Data",menuName = "Bike Engine Data",order = 51)]
public class BikeEngineData : ScriptableObject
{
    [Header ("Speed Setting")]
    [SerializeField] AnimationCurve motorTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));
    float speed;
    public float currentSpeedLimit =0;
    public float speedLimit = 60;
    public float boostSpeedLimit = 120;

    #region Boost setting
    [Header ("Boost Setting")]
    [SerializeField] AnimationCurve boostTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));
    [SerializeField] float boostForce = 200;
    [SerializeField] float brakeForce = 5000;
    [SerializeField] float boostTimeLimit = 5;
    [SerializeField] float boostDelay =2;
    #endregion
    #region  ExplodeSetting
    [Header("ExplodeSetting")]
    public float explosionForce;
    public float explosionRadius;
    public float explosionUpward;
    public float explosionDuration = 1;
    public ForceMode explosionMode;
    public Vector2 bodyRotationRageX;
    public Vector2 bodyRotationRageY;
    public Vector2 bodyRotationRageZ;
    #endregion
    #region 
    [Header("Wheel Setting")]
    public WheelSetting[] wheelsSetting;
    #endregion
    [Header("General")]
    public float airDrag = 0.1f;
    public float forceJump = 150;
    public float bikeRotatePower = 10;
}
