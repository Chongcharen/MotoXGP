using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVMove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddTorque(Vector3.right,ForceMode.VelocityChange);
    }
}
