using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float depthBeforeSubmerged = 1;
    public float displacementAmount = 3;

    void FixedUpdate(){
        if(transform.position.y < 0){
            float displacementMultiplier = Mathf.Clamp01(-transform.position.y / depthBeforeSubmerged)*displacementAmount;
            rigidbody.AddForce(new Vector3(0,Mathf.Abs(Physics.gravity.y)*displacementMultiplier,0f),ForceMode.Acceleration);
        }
    }
}
