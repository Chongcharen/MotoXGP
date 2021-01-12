using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Control System Data",menuName = "Control System Data",order = 51)]
public class ControlSystemData : ScriptableObject
{
    public float forceJump = 150;
    public float bikeRotatePower = 10;
    public float airDrag = 1.5f;
}
