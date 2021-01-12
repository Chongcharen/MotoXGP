using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Wheel System Data",menuName = "Wheel System Data",order = 51)]
public class WheelSystemData : ScriptableObject
{
    public float Radius = 0.3f; // the radius of the wheels
    public float Weight = 1000.0f; // the weight of a wheel
    public float SuspensionDistance = 0.2f;
    public float ForceAppointDistance =0;
    public float DampingRate = 0.5f;
    public float shoke_update_speed = 20;
    public Vector3 WheelCenter;
    public FrictionSetting ForwardFriction;
    public FrictionSetting SidewaysFriction;
    public SuspensionSpringSetting SuspensionSpring;
    [Header("SphereCollider")]
    public bool addSphereCollider = false;
    public PhysicMaterial physicMaterial;
    [Header("Wheel Particle")]
    public GameObject wheelParticleObject;

}
