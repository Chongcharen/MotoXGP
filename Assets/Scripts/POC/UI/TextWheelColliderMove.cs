using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWheelColliderMove : MonoBehaviour
{
    // Start is called before the first frame update
    WheelCollider collider;
    void Start()
    {
        collider = GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        collider.motorTorque = 200;
    }
}
