using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BikeEngine Data",menuName = "Bike Engine Data",order = 51)]
public class BikeEngineData : ScriptableObject
{
    [Header ("Speed Setting")]
    public AnimationCurve motorTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));
    public float speedLimit = 60;
    [Header("Brake Setting")]
    public int brakeTorque = 8000;

}
