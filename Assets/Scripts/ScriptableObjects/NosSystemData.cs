using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Nos Engine Data",menuName = "Nos Engine Data",order = 51)]
public class NosSystemData : ScriptableObject
{
    #region Boost setting
    [Header ("Boost Setting")]
    public AnimationCurve boostTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));
    public float boostForce = 200;
    public float boostTimeLimit = 5;
    public float boostDelay =2;
    public float boostSpeedLimit = 30;
    public int boostLimit = 18;
    #endregion
    [Header("Lower Gear")]
    public int lower_gear_force = 200;
    public float lower_gear_time_limit = 2;
    public GameObject particle_nos;
}
