using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float depthBeforeSubmerged = 1;
    public float displacementAmount = 3;
    Transform container;
    public float minimum = 0;
    void FixedUpdate(){
        if(container == null)return;
        Debug.Log("container.position "+container.position);
        Debug.Log("transform "+transform.position);
        if(this.transform.position.y < container.position.y){
            float displacementMultiplier = Mathf.Clamp01(-transform.position.y / depthBeforeSubmerged)*displacementAmount;
            rigidbody.AddForce(new Vector3(0,Mathf.Abs(Physics.gravity.y)*displacementMultiplier,0f),ForceMode.Acceleration);
        }
    }
    public void SetupFloaterContainer(Transform _container){
        container = _container;
    }
}
