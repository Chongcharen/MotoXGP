using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Status System Data",menuName = "Status System Data",order = 51)]
public class StatusSystemData : ScriptableObject
{
    [Header("Normal")]
    public float normalDrag = 0.05f;
    [Header("Water")]
    public float waterDrag = 2.5f;
    [Header("Sand")]
    public float sandDrag = 3.5f;
}
