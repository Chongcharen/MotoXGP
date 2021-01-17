using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New ExplodeSystem Data",menuName = "ExplodeSystem Data",order = 51)]
public class ExplodeSystemData : ScriptableObject
{
    [Header("ExplodeSetting")]
    public float explosionForce;
    public float explosionRadius;
    public float explosionUpward;
    public float explosionDuration = 1;
    public ForceMode explosionMode;
    public Vector2 bodyRotationRageX;
    public Vector2 bodyRotationRageY;
    public Vector2 bodyRotationRageZ;
}
